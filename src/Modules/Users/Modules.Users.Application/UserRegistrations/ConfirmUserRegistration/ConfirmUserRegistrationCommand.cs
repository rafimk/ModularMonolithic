
using Application.Messaging;

namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

public sealed record ConfirmUserRegistrationCommand(
    Guid UserRegistrationId,
    string IdentityProviderId,
    string Email,
    string FirstName,
    string LastName) : ICommand;