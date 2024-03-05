using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;

namespace Modules.Training.Infrastructure.BackgroundJobs.ProcessInboxMessages;

internal sealed class ProcessInboxMessagesConfiguration : IRecurringJobConfiguration
{
    private readonly ProcessInboxMessagesOptions _options;

    public ProcessInboxMessagesConfiguration(IOptions<ProcessInboxMessagesOptions> options) => _options = options.Value;

    public string Name => typeof(ProcessInboxMessagesJob).FullName!;

    public Type Type => typeof(ProcessInboxMessagesJob);

    public int IntervalInSeconds => _options.IntervalInSeconds;
}
