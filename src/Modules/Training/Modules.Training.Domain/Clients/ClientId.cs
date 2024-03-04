using Domain.Primitives;

namespace Modules.Training.Domain.Clients;

public sealed record ClientId(Guid Value) : IEntityId;