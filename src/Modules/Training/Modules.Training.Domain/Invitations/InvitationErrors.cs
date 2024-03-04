using Shared.Errors;

namespace Modules.Training.Domain.Invitations;

public static class InvitationErrors
{
    public static Error EmailIsNotUnique => new ConflictError("Invitation.EmailIsNotUnique", "The specified email is already in use.");
    public static Error PendingExists => new ConflictError("Invitation.PendingExists", "There is already an existing invitation for the client.");
    public static Error EmailDoesNotMatch => new("Invitation.EmailDoesNotMatch", "There specified email does not match the invitation's email.");

    public static Func<InvitationId, Error> NotFound => invitationId => new NotFoundError(
        "Invitation.NotFound",
        $"The invitation with the identifier '{invitationId.Value}' was not found.");

    public static Func<InvitationId, Error> Expired => invitationId => new Error(
        "Invitation.Expired",
        $"The invitation with the identifier '{invitationId.Value}' has expired.");

    public static Func<InvitationId, Error> Cancelled => invitationId => new Error(
        "Invitation.Cancelled",
        $"The invitation with the identifier '{invitationId.Value}' has been cancelled.");
}