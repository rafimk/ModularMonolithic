using Serilog;

namespace Bootstrap.Api.Extensions;

internal static class HostBuilderExtensions
{ 
    internal static void UseSerilogWithConfiguration(this IHostBuilder hostBuilder) =>
        hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));
}