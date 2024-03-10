using Bootstrap.Api.StartupTasks;
using Infrastructure.Configuration;

namespace Bootstrap.Api.ServiceInstallers.StartupTasks;

internal sealed class StartupTasksServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) => services.AddHostedService<MigrateDatabaseStartupTask>();
}