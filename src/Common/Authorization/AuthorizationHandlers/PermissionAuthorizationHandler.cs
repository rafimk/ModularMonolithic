using Authorization.Extensions;
using Authorization.Requirements;
using Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.AuthorizationHandlers;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider.GetService<IPermissionService>()!;

        HashSet<string> permissions = await permissionService.GetPermissionsAsync(context.User.GetIdentityProviderId());

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}