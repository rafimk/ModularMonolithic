using Microsoft.AspNetCore.Authorization;

namespace Modules.Users.Infrastructure.Authorization.SameUser.Requirements;

internal sealed class SameUserRequirement : IAuthorizationRequirement
{
}