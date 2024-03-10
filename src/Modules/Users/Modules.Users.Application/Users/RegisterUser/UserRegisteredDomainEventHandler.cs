using Application.EventBus;
using Application.Messaging;
using Modules.Users.Domain.Users.Events;
using Modules.Users.IntegrationEvents;

namespace Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    private readonly IEventBus _eventBus;

    public UserRegisteredDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.UserId.Value,
                notification.UserRegistrationId?.Value,
                notification.Email,
                notification.FirstName,
                notification.LastName,
                notification.Roles),
            cancellationToken);
}