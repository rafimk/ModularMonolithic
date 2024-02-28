using Shared.Results;

namespace Modules.Users.Domain.UserRegistrations;

public interface IUserRegistrationRepository
{
    Task<Result<UserRegistration>> GetByIdAsync(UserRegistrationId id, CancellationToken cancellationToken = default);

    Task<Result> CheckNonePendingForEmailAsync(string email, CancellationToken cancellationToken = default);

    void Add(UserRegistration userRegistration);
}