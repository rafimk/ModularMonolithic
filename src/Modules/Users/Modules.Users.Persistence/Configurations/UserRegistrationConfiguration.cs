using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.UserRegistrations;
using Shared.Extensions;
using Modules.Users.Persistence.Constants;


namespace Modules.Users.Persistence.Configurations;

internal sealed class UserRegistrationConfiguration : IEntityTypeConfiguration<UserRegistration>
{
    public void Configure(EntityTypeBuilder<UserRegistration> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<UserRegistration> builder)
    {
        builder.ToTable(TableNames.UserRegistrations);

        builder.HasKey(userRegistration => userRegistration.Id);

        builder.Property(userRegistration => userRegistration.Id)
            .ValueGeneratedNever()
            .HasConversion(userRegistrationId => userRegistrationId.Value, value => new UserRegistrationId(value));

        builder.Property(userRegistration => userRegistration.Email).IsRequired().HasMaxLength(300);

        builder.Property(userRegistration => userRegistration.IdentityProviderId).HasMaxLength(500);

        builder.Property(userRegistration => userRegistration.FirstName).HasMaxLength(100);

        builder.Property(userRegistration => userRegistration.LastName).HasMaxLength(100);

        builder.Property(userRegistration => userRegistration.Status)
            .IsRequired()
            .HasConversion(status => status.Id, value => UserRegistrationStatus.FromId(value)!);

        builder.Property(userRegistration => userRegistration.ConfirmedOnUtc).IsRequired(false);

        builder.Property(userRegistration => userRegistration.CreatedOnUtc).IsRequired();

        builder.Property(userRegistration => userRegistration.ModifiedOnUtc).IsRequired(false);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<UserRegistration> builder) =>
        builder.HasIndex(userRegistration => new { userRegistration.Email, userRegistration.Status });
}