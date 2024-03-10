using Application.Data;
using Application.EventBus;
using MassTransit;
using Newtonsoft.Json;
using Persistence.Inbox;

namespace Modules.Users.Infrastructure.Idempotence;

internal sealed class IntegrationEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : class, IIntegrationEvent
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public IntegrationEventConsumer(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage(
            integrationEvent.Id,
            integrationEvent.OccurredOnUtc,
            integrationEvent.GetType().Name,
            JsonConvert.SerializeObject(
                integrationEvent,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));

        const string insertInboxMessageSql = @"
            INSERT INTO users.inbox_messages(id, occurred_on_utc, type, content)
            VALUES (@Id, @OccurredOnUtc, @Type, @Content::json)";

        await _sqlQueryExecutor.ExecuteAsync(insertInboxMessageSql, inboxMessage);
    }
}