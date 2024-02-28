using Domain.Primitives;

namespace Modules.Users.Domain.Users.Events;

public sealed record UserChangedDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserId UserId,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : DomainEvent(Id, OccurredOnUtc);