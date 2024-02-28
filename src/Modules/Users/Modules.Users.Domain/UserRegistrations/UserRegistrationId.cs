using Domain.Primitives;

namespace Modules.Users.Domain.UserRegistrations;

public sealed record UserRegistrationId(Guid Value) : IEntityId;