namespace Modules.Users.Infrastructure.Authorization.SameUser.Options;

internal sealed class SameUserAuthorizationOptions
{
    public string CacheKeyPrefix { get; init; } = string.Empty;

    public int CacheTimeInMinutes { get; init; }
}