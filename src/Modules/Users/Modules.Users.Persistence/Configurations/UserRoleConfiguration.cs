using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Roles;
using Modules.Users.Domain.Users;
using Shared.Extensions;
using Modules.Users.Persistence.Constants;

namespace Modules.Users.Persistence.Configurations;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(TableNames.UserRoles);

        builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
    }

    private static void ConfigureRelationships(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(userRole => userRole.RoleId)
            .IsRequired();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<UserRole> builder) =>
        builder.HasIndex(userRole => userRole.UserId);
}