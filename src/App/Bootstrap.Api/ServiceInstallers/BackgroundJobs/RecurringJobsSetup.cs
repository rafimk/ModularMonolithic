using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;
using Quartz;
using Shared.Extensions;

namespace Bootstrap.Api.ServiceInstallers.BackgroundJobs;

internal sealed class RecurringJobsSetup : IConfigureOptions<QuartzOptions>
{
    private readonly IEnumerable<IRecurringJobConfiguration> _recurringJobConfigurations;

  
    public RecurringJobsSetup(IEnumerable<IRecurringJobConfiguration> recurringJobConfigurations) =>
        _recurringJobConfigurations = recurringJobConfigurations;

    public void Configure(QuartzOptions options) =>
        _recurringJobConfigurations.ForEach(configuration =>
            options
                .AddJob(
                    configuration.Type,
                    jobBuilder => jobBuilder.WithIdentity(configuration.Name))
                .AddTrigger(triggerBuilder =>
                    triggerBuilder
                        .ForJob(configuration.Name)
                        .WithSimpleSchedule(scheduleBuilder =>
                            scheduleBuilder
                                .WithIntervalInSeconds(configuration.IntervalInSeconds)
                                .RepeatForever())));
}