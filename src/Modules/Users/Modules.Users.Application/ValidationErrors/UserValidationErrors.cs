using Shared.Errors;

namespace Modules.Users.Application.ValidationErrors;

internal static class UserValidationErrors
{
    internal static Error IdentifierIsRequired => new("User.IdentifierIsRequired", "The user identifier is required.");

    internal static Error IdentityIsRequired => new("User.IdentityIsRequired", "The user's identity is required.");

    internal static Error EmailIsRequired => new("User.EmailIsRequired", "The user's email is required.");

    internal static Error FirstNameIsRequired => new("User.FirstNameIsRequired", "The user's first name is required.");

    internal static Error LastNameIsRequired => new("User.LastNameIsRequired", "The user's last name is required.");
}
