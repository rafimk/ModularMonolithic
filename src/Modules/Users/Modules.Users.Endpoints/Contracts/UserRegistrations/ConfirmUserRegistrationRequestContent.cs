namespace Modules.Users.Endpoints.Contracts.UserRegistrations;

public sealed record ConfirmUserRegistrationRequestContent(string Email, string FirstName, string LastName);