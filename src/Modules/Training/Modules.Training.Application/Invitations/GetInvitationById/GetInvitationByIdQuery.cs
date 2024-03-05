using Application.Messaging;

namespace Modules.Training.Application.Invitations.GetInvitationById;

public sealed record GetInvitationByIdQuery(Guid InvitationId) : IQuery<InvitationResponse>;