using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Users.Infrastructure.ServiceInstallers;

internal sealed class EndpointsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddControllers()
            .AddApplicationPart(Endpoints.AssemblyReference.Assembly);
}