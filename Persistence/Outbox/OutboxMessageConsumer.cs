namespace Persistence.Outbox;

public sealed class OutboxMessageConsumer
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
}
