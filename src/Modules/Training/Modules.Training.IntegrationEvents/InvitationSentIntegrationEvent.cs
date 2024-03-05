using Application.EventBus;

namespace Modules.Training.IntegrationEvents;

public sealed record InvitationSentIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid InvitationId,
    Guid TrainerId,
    string Email,
    string SenderFirstName,
    string SenderLastName) : IntegrationEvent(Id, OccurredOnUtc);
