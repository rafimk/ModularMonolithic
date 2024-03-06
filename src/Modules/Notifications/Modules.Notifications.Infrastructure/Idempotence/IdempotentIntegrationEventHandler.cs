using Application.Data;
using Application.EventBus;

namespace Modules.Notifications.Infrastructure.Idempotence;

internal sealed class IdempotentIntegrationEventHandler<TIntegrationEvent> : IntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    private readonly IIntegrationEventHandler<TIntegrationEvent> _decorated;
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public IdempotentIntegrationEventHandler(IIntegrationEventHandler<TIntegrationEvent> decorated, ISqlQueryExecutor sqlQueryExecutor)
    {
        _decorated = decorated;
        _sqlQueryExecutor = sqlQueryExecutor;
    }

    public override async Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        var parameters = new InboxConsumerParameters(integrationEvent.Id, _decorated.GetType().FullName!);

        if (await IsInboxMessageConsumedAsync(parameters))
        {
            return;
        }

        await _decorated.Handle(integrationEvent, cancellationToken);

        await InsertInboxMessageConsumerAsync(parameters);
    }

    private async Task<bool> IsInboxMessageConsumedAsync(InboxConsumerParameters parameters)
    {
        const string checkIfConsumedSql = @"
            SELECT EXISTS(
                SELECT 1
                FROM notifications.inbox_message_consumers
                WHERE id = @Id AND
                      name = @Name
            )";

        return await _sqlQueryExecutor.ExecuteScalarAsync<bool>(checkIfConsumedSql, parameters);
    }

    private async Task InsertInboxMessageConsumerAsync(InboxConsumerParameters parameters)
    {
        const string insertConsumedSql = @"
            INSERT INTO notifications.inbox_message_consumers(id, name)
            VALUES (@Id, @Name)";

        await _sqlQueryExecutor.ExecuteAsync(insertConsumedSql, parameters);
    }

    private sealed record InboxConsumerParameters(Guid Id, string Name);
}
