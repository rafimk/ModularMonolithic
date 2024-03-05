using Application.Extensions;
using FluentValidation;
using Modules.Training.Application.ValidationErrors;

namespace Modules.Training.Application.Trainers.InviteClient;
internal sealed class InviteClientCommandValidator : AbstractValidator<InviteClientCommand>
{
    public InviteClientCommandValidator()
    {
        RuleFor(x => x.TrainerId).NotEmpty().WithError(InvitationValidationErrors.TrainerIdentifierIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(InvitationValidationErrors.EmailIsRequired);
    }
}