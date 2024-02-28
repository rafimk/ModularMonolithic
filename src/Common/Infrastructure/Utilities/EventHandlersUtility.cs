using Application.EventBus;
using Application.Messaging;
using MediatR;

namespace Infrastructure.Utilities;

public static class EventHandlersUtility
{
    private static readonly Type NotificationHandlerType = typeof(INotificationHandler<>);
    private static readonly Type DomainEventHandlerType = typeof(IDomainEventHandler<>);
    private static readonly Type IntegrationEventHandlerType = typeof(IIntegrationEventHandler<>);

    public static bool ImplementsDomainEventHandler(Type type) =>
        type.GetInterfaces().Length > 0 &&
        type.GetInterfaces().All(interfaceType => IsNotificationHandler(interfaceType) || IsDomainEventHandler(interfaceType));

    public static bool ImplementsIntegrationEventHandler(Type type) => type.GetInterfaces().Any(IsIntegrationEventHandler);

    public static bool IsNotificationHandler(Type type) =>
        type.IsGenericType &&
        type.Name.StartsWith(NotificationHandlerType.Name, StringComparison.InvariantCulture);

    public static bool IsDomainEventHandler(Type type) =>
        type.IsGenericType &&
        type.Name.StartsWith(DomainEventHandlerType.Name, StringComparison.InvariantCulture);

    public static bool IsIntegrationEventHandler(Type type) =>
        type.IsGenericType &&
        type.Name.StartsWith(IntegrationEventHandlerType.Name, StringComparison.InvariantCulture);
}
