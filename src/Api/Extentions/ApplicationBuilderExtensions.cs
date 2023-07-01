using System.Linq;
using Application.GeneralServices.DataInitializer;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SampleTemplate.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static void IntializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>(); //Service locator

            dbContext.Database.Migrate();

            var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
            foreach (var dataInitializer in dataInitializers)
                dataInitializer.InitializeData();
        }
        public static void UseSwaggerAndUi(this IApplicationBuilder app)
        {
            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((document, request) =>
                {
                    var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
                    document.Paths.Clear();
                    foreach (var pathItem in paths)
                        document.Paths.Add(pathItem.Key, pathItem.Value);
                });
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc-v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Doc-v2");
                options.DocExpansion(DocExpansion.None);
                options.RoutePrefix = "";
            });
        }
    }
}
