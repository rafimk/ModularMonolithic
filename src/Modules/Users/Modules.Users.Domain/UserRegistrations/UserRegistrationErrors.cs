using Shared.Errors;

namespace Modules.Users.Domain.UserRegistrations;

public static class UserRegistrationErrors
{
    public static Error PendingExists => new ConflictError(
        "UserRegistration.PendingExists",
        "There is already an existing user registration pending for the specified email.");

    public static Error EmailDoesNotMatch => new(
        "UserRegistration.EmailDoesNotMatch",
        "There specified email does not match the user registration's email.");

    public static Error EmailHasBeenTaken => new ConflictError("UserRegistration.EmailHasBeenTaken", "The specified email has been taken.");

    public static Func<UserRegistrationId, Error> NotFound => userRegistrationId => new NotFoundError(
        "UserRegistration.NotFound",
        $"The user registration with the identifier '{userRegistrationId.Value}' was not found.");

    public static Func<string, Error> NotFoundByIdentity =>
        identityProviderId => new NotFoundError(
            "User.NotFoundByIdentity",
            $"The user with the identity provider identifier '{identityProviderId}' was not found.");

    public static Func<UserRegistrationId, Error> Confirmed => userRegistrationId => new(
        "UserRegistration.Confirmed",
        $"The user registration with the identifier '{userRegistrationId.Value}' has been confirmed.");

    public static Func<UserRegistrationId, Error> Cancelled => userRegistrationId => new(
        "UserRegistration.Cancelled",
        $"The user registration with the identifier '{userRegistrationId.Value}' has been cancelled.");

    public static Func<UserRegistrationId, Error> Expired => userRegistrationId => new(
        "UserRegistration.Expired",
        $"The user registration with the identifier '{userRegistrationId.Value}' has expired.");
}