using Shared.Errors;

namespace Modules.Users.Domain.Users;

public static class UserErrors
{
    public static Error EmailIsNotUnique => new ConflictError("User.EmailIsNotUnique", "The specified email is already in use.");

    public static Error RegistrationIsNotConfirmed => new("User.RegistrationIsNotConfirmed", "The user registration is not confirmed.");

    public static Error RegistrationIsIncomplete => new("User.RegistrationIsIncomplete", "The user registration is incomplete.");

    public static Func<UserId, Error> NotFound => userId => new NotFoundError(
        "User.NotFound",
        $"The user with the identifier '{userId.Value}' was not found.");

    public static Func<string, Error> NotFoundByIdentity =>
        identityProviderId => new NotFoundError(
            "User.NotFoundByIdentity",
            $"The user with the identity provider identifier '{identityProviderId}' was not found.");
}