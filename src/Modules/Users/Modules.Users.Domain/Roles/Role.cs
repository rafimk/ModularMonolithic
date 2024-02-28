using Domain.Primitives;
using Modules.Users.Domain.Users;

namespace Modules.Users.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Trainer = new(2, "Trainer");
    public static readonly Role Client = new(3, "Client");
    public static readonly Role Administrator = new(100, "Administrator");

    private Role(int id, string name)
        : base(id, name)
    {
    }

    private Role()
    {
    }

    public IReadOnlyCollection<User> Users { get; } = new List<User>();
}