using Domain.Primitives;

namespace Modules.Users.Domain.UserRegistrations;

public sealed class UserRegistrationStatus : Enumeration<UserRegistrationStatus>
{
    public static readonly UserRegistrationStatus Pending = new(1, "Pending");
    public static readonly UserRegistrationStatus Confirmed = new(2, "Confirmed");
    public static readonly UserRegistrationStatus Cancelled = new(3, "Cancelled");
    public static readonly UserRegistrationStatus Expired = new(4, "Expired");

    private UserRegistrationStatus(int id, string name)
        : base(id, name)
    {
    }

    private UserRegistrationStatus(int id)
    {
        UserRegistrationStatus? userRegistrationStatus = FromId(id);

        Id = userRegistrationStatus?.Id ?? throw new ArgumentNullException(nameof(id));
        Name = userRegistrationStatus?.Name ?? throw new ArgumentNullException(nameof(id));
    }
}