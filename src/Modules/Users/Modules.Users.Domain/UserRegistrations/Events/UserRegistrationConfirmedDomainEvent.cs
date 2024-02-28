using Domain.Primitives;

namespace Modules.Users.Domain.UserRegistrations.Events;

public sealed record UserRegistrationConfirmedDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserRegistrationId UserRegistrationId) : DomainEvent(Id, OccurredOnUtc);