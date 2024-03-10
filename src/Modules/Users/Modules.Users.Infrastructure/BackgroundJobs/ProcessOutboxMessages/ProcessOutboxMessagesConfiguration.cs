using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;

namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

internal sealed class ProcessOutboxMessagesConfiguration : IRecurringJobConfiguration
{
    private readonly ProcessOutboxMessagesOptions _options;

    public ProcessOutboxMessagesConfiguration(IOptions<ProcessOutboxMessagesOptions> options) => _options = options.Value;

    public string Name => typeof(ProcessOutboxMessagesJob).FullName!;

    public Type Type => typeof(ProcessOutboxMessagesJob);

    public int IntervalInSeconds => _options.IntervalInSeconds;
}