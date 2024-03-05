using Application.EventBus;
using Application.Messaging;
using Modules.Training.Domain.Invitations.Events;
using Modules.Training.IntegrationEvents;

namespace Modules.Training.Application.Invitations.CancelInvitation;

internal sealed class InvitationCancelledDomainEventHandler : IDomainEventHandler<InvitationCancelledDomainEvent>
{
    private readonly IEventBus _eventBus;

    public InvitationCancelledDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    public async Task Handle(InvitationCancelledDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new InvitationCancelledIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.InvitationId.Value),
            cancellationToken);
}