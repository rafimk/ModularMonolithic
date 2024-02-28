using Domain.Primitives;
using Domain.Time;
using Modules.Users.Domain.UserRegistrations.Events;
using Shared.Results;

namespace Modules.Users.Domain.UserRegistrations;

public sealed class UserRegistration : Entity<UserRegistrationId>, IAuditable
{
    private UserRegistration(UserRegistrationId id, string email, UserRegistrationStatus status)
        : base(id)
    {
        Email = email;
        Status = status;
    }

    public string Email { get; private set; }

    public string? IdentityProviderId { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    public UserRegistrationStatus Status { get; private set; }

    public DateTime? ConfirmedOnUtc { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

     public static Result<UserRegistration> Create(UserRegistrationId id, string email)
    {
        var userRegistration = new UserRegistration(id, email, UserRegistrationStatus.Pending);

        return userRegistration;
    }

    public Result Confirm(string identityProviderId, string email, string firstName, string lastName)
    {
        if (Email != email)
        {
            return Result.Failure(UserRegistrationErrors.EmailDoesNotMatch);
        }

        return Status switch
        {
            var status when status == UserRegistrationStatus.Confirmed => Result.Failure(UserRegistrationErrors.Confirmed(Id)),
            var status when status == UserRegistrationStatus.Cancelled => Result.Failure(UserRegistrationErrors.Cancelled(Id)),
            var status when status == UserRegistrationStatus.Expired => Result.Failure(UserRegistrationErrors.Expired(Id)),
            _ => ConfirmInternal(identityProviderId, firstName, lastName)
        };
    }

    public Result Cancel() =>
        Status switch
        {
            var status when status == UserRegistrationStatus.Cancelled => Result.Failure(UserRegistrationErrors.Cancelled(Id)),
            _ => CancelInternal()
        };

    public Result Expire() =>
        Status switch
        {
            var status when status == UserRegistrationStatus.Expired => Result.Failure(UserRegistrationErrors.Expired(Id)),
            _ => ExpireInternal()
        };

    private Result ConfirmInternal(string identityProviderId, string firstName, string lastName)
    {
        IdentityProviderId = identityProviderId;
        FirstName = firstName;
        LastName = lastName;
        Status = UserRegistrationStatus.Confirmed;
        ConfirmedOnUtc = SystemTimeProvider.UtcNow();

        RaiseDomainEvent(new UserRegistrationConfirmedDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            Id));

        return Result.Success();
    }

    private Result CancelInternal()
    {
        Status = UserRegistrationStatus.Cancelled;

        return Result.Success();
    }

    private Result ExpireInternal()
    {
        Status = UserRegistrationStatus.Expired;

        RaiseDomainEvent(new UserRegistrationExpiredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            Id));

        return Result.Success();
    }
}
