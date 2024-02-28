using Domain.Primitives;
using Domain.Time;
using Modules.Users.Domain.Roles;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.Users.Events;
using Shared.Results;
using System.Data;

namespace Modules.Users.Domain.Users;

public sealed class User : Entity<UserId>, IAuditable
{
    private readonly HashSet<Role> _roles = new();

    private User(UserId id, string identityProviderId, string email, string firstName, string lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        IdentityProviderId = identityProviderId;
    }

    public string IdentityProviderId { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles.ToList().AsReadOnly();

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public static Result<User> Create(UserId id, string identityProviderId, string email, string firstName, string lastName)
    {
        var user = new User(id, identityProviderId, email, firstName, lastName);

        user._roles.Add(Role.Registered);
        user._roles.Add(Role.Trainer);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            user.Id,
            null,
            user.Email,
            user.FirstName,
            user.LastName,
            user._roles.Select(role => role.Name).ToHashSet()));

        return user;
    }

    public static Result<User> CreateFromRegistration(UserRegistration userRegistration)
    {
        if (userRegistration.Status != UserRegistrationStatus.Confirmed)
        {
            return Result.Failure<User>(UserErrors.RegistrationIsNotConfirmed);
        }

        if (string.IsNullOrWhiteSpace(userRegistration.IdentityProviderId) ||
            string.IsNullOrWhiteSpace(userRegistration.FirstName) ||
            string.IsNullOrWhiteSpace(userRegistration.LastName))
        {
            return Result.Failure<User>(UserErrors.RegistrationIsIncomplete);
        }

        var user = new User(
            new UserId(Guid.NewGuid()),
            userRegistration.IdentityProviderId,
            userRegistration.Email,
            userRegistration.FirstName,
            userRegistration.LastName);

        user._roles.Add(Role.Registered);
        user._roles.Add(Role.Client);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            user.Id,
            userRegistration.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user._roles.Select(role => role.Name).ToHashSet()));

        return user;
    }

    public void Change(string firstName, string lastName)
    {
        bool basicInformationChanged = FirstName != firstName || LastName != lastName;

        FirstName = firstName;
        LastName = lastName;

        if (basicInformationChanged)
        {
            RaiseDomainEvent(new UserChangedDomainEvent(
                Guid.NewGuid(),
                SystemTimeProvider.UtcNow(),
                Id,
                FirstName,
                LastName,
                _roles.Select(role => role.Name).ToHashSet()));
        }
    }
}