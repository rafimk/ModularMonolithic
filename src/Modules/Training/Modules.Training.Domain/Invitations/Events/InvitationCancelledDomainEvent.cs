using Domain.Primitives;

namespace Modules.Training.Domain.Invitations.Events;

public sealed record InvitationCancelledDomainEvent(Guid Id, DateTime OccurredOnUtc, InvitationId InvitationId) : DomainEvent(Id, OccurredOnUtc);