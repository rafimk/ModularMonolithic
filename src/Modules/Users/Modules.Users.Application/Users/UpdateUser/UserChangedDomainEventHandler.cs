using Application.EventBus;
using Application.Messaging;
using Modules.Users.Domain.Users.Events;
using Modules.Users.IntegrationEvents;

namespace Modules.Users.Application.Users.UpdateUser;

internal sealed class UserChangedDomainEventHandler : IDomainEventHandler<UserChangedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public UserChangedDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    public async Task Handle(UserChangedDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new UserChangedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.UserId.Value,
                notification.FirstName,
                notification.LastName,
                notification.Roles),
            cancellationToken);
}