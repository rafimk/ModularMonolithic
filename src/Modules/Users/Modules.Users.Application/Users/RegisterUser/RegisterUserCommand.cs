using Application.Messaging;

namespace Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string IdentityProviderId, string Email, string FirstName, string LastName) : ICommand<Guid>;