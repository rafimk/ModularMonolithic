using Microsoft.Extensions.Options;
using Quartz;

namespace Bootstrap.Api.ServiceInstallers.BackgroundJobs;

internal sealed class QuartzHostedServiceOptionsSetup : IConfigureOptions<QuartzHostedServiceOptions>
{
    public void Configure(QuartzHostedServiceOptions options) => options.WaitForJobsToComplete = true;
}