using Infrastructure.Configuration;
using Quartz;

namespace Bootstrap.Api.ServiceInstallers.BackgroundJobs;

internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .ConfigureOptions<RecurringJobsSetup>()
            .ConfigureOptions<QuartzHostedServiceOptionsSetup>()
            .AddQuartz(configure => configure.UseMicrosoftDependencyInjectionJobFactory())
            .AddQuartzHostedService();
}