using Application.Extensions;
using FluentValidation;
using Modules.Users.Application.ValidationErrors;

namespace Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(UserValidationErrors.IdentifierIsRequired);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithError(UserValidationErrors.FirstNameIsRequired);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithError(UserValidationErrors.LastNameIsRequired);
    }
}