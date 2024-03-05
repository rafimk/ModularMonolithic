using Application.EventBus;
using Domain.Primitives;
using Microsoft.Extensions.Logging;
using Shared.Errors;

namespace Application.Extensions;

public static class LoggerExtensions
{
    public static void LogError(this ILogger logger, IDomainEvent domainEvent, Error error) =>
        logger.LogError("Error while processing domain event: {DomainEventId} - {Error}", domainEvent.Id, error);

    public static void LogError(this ILogger logger, IIntegrationEvent integrationEvent, Error error) =>
        logger.LogError("Error while processing integration event: {IntegrationEventId} - {Error}", integrationEvent.Id, error);
}