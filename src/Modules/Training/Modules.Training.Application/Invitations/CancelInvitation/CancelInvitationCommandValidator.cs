using FluentValidation;

namespace Modules.Training.Application.Invitations.CancelInvitation;

internal sealed class CancelInvitationCommandValidator : AbstractValidator<CancelInvitationCommand>
{
    public CancelInvitationCommandValidator() => RuleFor(x => x.InvitationId).NotEmpty().WithError(InvitationValidationErrors.IdentifierIsRequired);
}