using Microsoft.AspNetCore.Builder;
using Serilog;

namespace SampleTemplate.Extentions
{
    public static class ConfigureHostBuilderExtentions
    {
        public static void UseCustomSerilog(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
