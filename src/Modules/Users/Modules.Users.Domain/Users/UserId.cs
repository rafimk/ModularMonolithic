using Domain.Primitives;

namespace Modules.Users.Domain.Users;

public sealed record UserId(Guid Value) : IEntityId;