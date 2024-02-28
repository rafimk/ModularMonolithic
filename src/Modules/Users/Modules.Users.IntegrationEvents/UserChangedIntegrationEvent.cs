using Application.EventBus;

namespace Modules.Users.IntegrationEvents;

public sealed record UserChangedIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : IntegrationEvent(Id, OccurredOnUtc);