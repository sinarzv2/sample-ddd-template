using Serilog;

namespace Api.Extentions;

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