using Application.Messaging;

namespace Modules.Training.Application.Invitations.GetInvitations;

public sealed record GetInvitationsQuery(Guid TrainerId) : IQuery<List<InvitationResponse>>;