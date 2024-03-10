using Infrastructure.Configuration;

namespace Bootstrap.Api.ServiceInstallers.Swagger;

internal sealed class SwaggerServiceInstaller : IServiceInstaller
{
    void IServiceInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<SwaggerGenOptionsSetup>();

        services.ConfigureOptions<SwaggerUiOptionsSetup>();

        services.AddSwaggerGen();
    }
}