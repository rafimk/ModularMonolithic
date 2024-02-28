using Application.EventBus;

namespace Modules.Users.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    Guid? UserRegistrationId,
    string Email,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : IntegrationEvent(Id, OccurredOnUtc);