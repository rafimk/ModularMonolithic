using Shared.Results;

namespace Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    void Add(User user);
}