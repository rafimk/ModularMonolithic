using Domain.Primitives;

namespace Modules.Training.Domain.Invitations.Events;

public sealed record InvitationCreatedDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    InvitationId InvitationId,
TrainerId TrainerId,
string Email,
    Sender Sender) : DomainEvent(Id, OccurredOnUtc);