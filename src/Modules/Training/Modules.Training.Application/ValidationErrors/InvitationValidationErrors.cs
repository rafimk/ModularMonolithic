using Shared.Errors;

namespace Modules.Training.Application.ValidationErrors;

internal static class InvitationValidationErrors
{
    internal static Error IdentifierIsRequired => new("Invitation.IdentifierIsRequired", "The identifier is required.");

    internal static Error TrainerIdentifierIsRequired => new("Invitation.TrainerIdentifierIsRequired", "The trainer identifier is required.");

    internal static Error EmailIsRequired => new("Trainer.EmailIsRequired", "The email is required.");
}