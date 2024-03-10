namespace Modules.Users.Infrastructure.Authorization.SameUser.Services;

internal interface IUserChecker
{
    Task<bool> ExistsAsync(Guid userId, string identityProviderId, CancellationToken cancellationToken = default);
}