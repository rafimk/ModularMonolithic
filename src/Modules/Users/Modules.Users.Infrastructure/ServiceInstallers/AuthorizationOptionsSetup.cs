using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Modules.Users.Endpoints;
using Modules.Users.Infrastructure.Authorization.SameUser.Requirements;

namespace Modules.Users.Infrastructure.ServiceInstallers;

internal sealed class AuthorizationOptionsSetup : IConfigureOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options) =>
        options.AddPolicy(
            Policies.SameUser,
            policy => policy.AddRequirements(new SameUserRequirement()));
}