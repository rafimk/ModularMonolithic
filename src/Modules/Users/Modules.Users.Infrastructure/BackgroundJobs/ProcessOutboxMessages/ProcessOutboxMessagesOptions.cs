namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

internal sealed class ProcessOutboxMessagesOptions
{
    public int IntervalInSeconds { get; init; }

    public int RetryCount { get; init; }

    public int BatchSize { get; init; }
}