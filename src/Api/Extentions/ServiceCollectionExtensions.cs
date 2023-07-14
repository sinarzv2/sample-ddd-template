using Application.AccountApplication.EventHandlers;
using Application.AccountApplication.Services;
using Application.Common;
using Common.DependencyLifeTime;
using Common.Models;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;
using Infrastructure.Persistance;
using Infrastructure.Repository;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Api.Common.Swagger;
using Role = Domain.Aggregates.Identity.Role;

namespace Api.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                //options.LogTo(Console.WriteLine);
            });
        }
        public static void AddCustomIdentity(this IServiceCollection service, IdentitySettings settings)
        {
            service.AddIdentity<User, Role>(identityOptions =>
                {
                    identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                    identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                    identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic;
                    identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                    identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;
                    identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }


        public static void AddJwtAuthentication(this IServiceCollection service, JwtSettings jwtSettings)
        {
            service.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptKey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var apiResult = new FluentResult();
                        apiResult.AddError(Errors.AuthenticationFailed);
                        var result = JsonSerializer.Serialize(apiResult);
                        return context.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var apiResult = new FluentResult();
                        apiResult.AddError(Errors.YouAreNotLoggedIn);
                        var result = JsonSerializer.Serialize(apiResult);
                        return context.Response.WriteAsync(result);
                    }
                };
            });
        }
        public static void AddSwagger(this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Version = "v1", Title = " Version1" });
                options.SwaggerDoc("v2", new OpenApiInfo() { Version = "v2", Title = " Version2" });

                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();

                options.EnableAnnotations();


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri(siteSettings.LoginUrl, UriKind.Relative)
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference(){Type = ReferenceType.SecurityScheme,Id = "Bearer"}
                    }, new string[] { } }
                });


                options.OperationFilter<RemoveVersionParameters>();

                options.DocumentFilter<SetVersionInPaths>();

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo?.DeclaringType?
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return (versions ?? Array.Empty<ApiVersion>()).Any(v => $"v{v}" == docName);
                });
            });
        }

        public static void AddMapster(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

        }
        public static void AddScrutor(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<UserService>()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .FromAssemblyOf<UserRepository>()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        public static void AddDistributedCache(this IServiceCollection services, RedisSettings redisSettings)
        {
            if (redisSettings.IsEnabled)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.InstanceName = redisSettings.InstanceName;
                    options.ConfigurationOptions = new ConfigurationOptions()
                    {
                        Password = redisSettings.Password
                    };
                    options.ConnectionMultiplexerFactory = async () => await ConnectionMultiplexer.ConnectAsync(redisSettings.Connection);
                });
            }

            else
            {
                services.AddDistributedMemoryCache();
            }

        }

        public static void AddCustomMediateR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserPasswordChangedEventHandler).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));

        }

    }
}
