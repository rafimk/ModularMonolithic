using Domain.Primitives;

namespace Modules.Training.Domain.Invitations;

public sealed record InvitationId(Guid Value) : IEntityId;