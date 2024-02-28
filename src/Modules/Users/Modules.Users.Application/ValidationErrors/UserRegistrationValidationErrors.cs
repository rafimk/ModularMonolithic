using Shared.Errors;

namespace Modules.Users.Application.ValidationErrors;

internal static class UserRegistrationValidationErrors
{
    internal static Error IdentifierIsRequired => new("UserRegistration.IdentifierIsRequired", "The user identifier is required.");

    internal static Error IdentityIsRequired => new("UserRegistration.IdentityIsRequired", "The user registration identity is required.");

    internal static Error EmailIsRequired => new("UserRegistration.EmailIsRequired", "The user registration email is required.");

    internal static Error FirstNameIsRequired => new("UserRegistration.FirstNameIsRequired", "The user registration first name is required.");

    internal static Error LastNameIsRequired => new("UserRegistration.LastNameIsRequired", "The user registration last name is required.");
}