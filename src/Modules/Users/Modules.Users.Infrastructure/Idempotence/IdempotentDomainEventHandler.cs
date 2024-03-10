using Application.Data;
using Application.Messaging;
using Domain.Primitives;

namespace Modules.Users.Infrastructure.Idempotence;

internal sealed class IdempotentDomainEventHandler<TEvent> : IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    private readonly IDomainEventHandler<TEvent> _decorated;
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public IdempotentDomainEventHandler(IDomainEventHandler<TEvent> decorated, ISqlQueryExecutor sqlQueryExecutor)
    {
        _decorated = decorated;
        _sqlQueryExecutor = sqlQueryExecutor;
    }

    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        var parameters = new OutboxConsumerParameters(notification.Id, _decorated.GetType().FullName!);

        if (await IsOutboxMessageConsumedAsync(parameters))
        {
            return;
        }

        await _decorated.Handle(notification, cancellationToken);

        await InsertOutboxMessageConsumerAsync(parameters);
    }

    private async Task<bool> IsOutboxMessageConsumedAsync(OutboxConsumerParameters parameters)
    {
        const string checkIfConsumedSql = @"
            SELECT EXISTS(
                SELECT 1
                FROM users.outbox_message_consumers
                WHERE id = @Id AND
                      name = @Name
            )";

        return await _sqlQueryExecutor.ExecuteScalarAsync<bool>(checkIfConsumedSql, parameters);
    }

    private async Task InsertOutboxMessageConsumerAsync(OutboxConsumerParameters parameters)
    {
        const string insertConsumedSql = @"
            INSERT INTO users.outbox_message_consumers(id, name)
            VALUES (@Id, @Name)";

        await _sqlQueryExecutor.ExecuteAsync(insertConsumedSql, parameters);
    }

    private sealed record OutboxConsumerParameters(Guid Id, string Name);
}