using Application.Messaging;

namespace Modules.Users.Application.Users.GetUserByIdentityProviderId;

public sealed record GetUserByIdentityProviderIdQuery(string IdentityProviderId) : IQuery<UserResponse>;