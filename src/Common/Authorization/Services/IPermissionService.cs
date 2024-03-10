namespace Authorization.Services;

internal interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(string identityProviderId, CancellationToken cancellationToken = default);
}