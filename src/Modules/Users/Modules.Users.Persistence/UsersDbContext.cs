using Microsoft.EntityFrameworkCore;
using Modules.Users.Persistence.Constants;

namespace Modules.Users.Persistence;

public sealed class UsersDbContext : DbContext
{

    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}