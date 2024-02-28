using Domain.Primitives;
using Modules.Users.Domain.UserRegistrations;

namespace Modules.Users.Domain.Users.Events;

public sealed record UserRegisteredDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserId UserId,
    UserRegistrationId? UserRegistrationId,
    string Email,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : DomainEvent(Id, OccurredOnUtc);