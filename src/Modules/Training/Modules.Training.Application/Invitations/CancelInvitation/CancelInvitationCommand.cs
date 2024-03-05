
using Application.Messaging;

namespace Modules.Training.Application.Invitations.CancelInvitation;

public sealed record CancelInvitationCommand(Guid InvitationId) : ICommand;