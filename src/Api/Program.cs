using Common.Models;
using SampleTemplate.Common.Middlewares;
using SampleTemplate.Extentions;

var builder = WebApplication.CreateBuilder(args);


var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

builder.Host.UseCustomSerilog();

builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddJwtAuthentication(siteSettings.JwtSettings);

builder.Services.AddSwagger(siteSettings);

builder.Services.AddApiVersioning();

builder.Services.AddMapster();

builder.Services.AddScrutor();

builder.Services.AddDistributedCache(siteSettings.RedisSettings);

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerAndUi();

app.IntializeDatabase();

app.MapControllers();

app.Run();
