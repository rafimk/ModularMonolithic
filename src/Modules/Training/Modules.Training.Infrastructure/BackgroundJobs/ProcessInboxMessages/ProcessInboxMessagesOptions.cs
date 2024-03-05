
namespace Modules.Training.Infrastructure.BackgroundJobs.ProcessInboxMessages;

internal sealed class ProcessInboxMessagesOptions
{
    public int IntervalInSeconds { get; init; }

    public int RetryCount { get; init; }

    public int BatchSize { get; init; }
}