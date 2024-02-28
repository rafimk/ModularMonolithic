using Microsoft.AspNetCore.Authorization;

namespace Endpoints.Authorization;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(permission) =>
        Permission = permission;

    public string Permission { get; }
}