namespace Authorization.Options;

internal sealed class PermissionAuthorizationOptions
{
    public string CacheKeyPrefix { get; init; } = string.Empty;

    public int CacheTimeInMinutes { get; init; }
}