namespace Modules.Notifications.Application.Abstractions.Email;

public sealed record ClientInvitationEmailRequest(string Email, Guid InvitationId, string Trainer);