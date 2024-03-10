using Microsoft.AspNetCore.Authorization;

namespace Authorization.Requirements;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    internal PermissionRequirement(string permission) => Permission = permission;

    internal string Permission { get; }
}