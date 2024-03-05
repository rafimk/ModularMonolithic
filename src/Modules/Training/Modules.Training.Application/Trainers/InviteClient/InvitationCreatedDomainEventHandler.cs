using Application.EventBus;
using Application.Messaging;
using Modules.Training.Domain.Invitations.Events;
using Modules.Training.IntegrationEvents;

namespace Modules.Training.Application.Trainers.InviteClient;

internal sealed class InvitationCreatedDomainEventHandler : IDomainEventHandler<InvitationCreatedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public InvitationCreatedDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    public async Task Handle(InvitationCreatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new InvitationSentIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.InvitationId.Value,
                notification.TrainerId.Value,
                notification.Email,
                notification.Sender.FirstName,
                notification.Sender.LastName),
            cancellationToken);
}