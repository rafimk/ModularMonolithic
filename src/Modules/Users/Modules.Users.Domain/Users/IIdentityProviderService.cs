using Shared.Results;

namespace Modules.Users.Domain.Users;

public interface IIdentityProviderService
{
    Task<Result> ExistsAsync(string identityProviderId, CancellationToken cancellationToken = default);
}