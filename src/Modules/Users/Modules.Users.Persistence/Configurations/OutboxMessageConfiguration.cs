using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Outbox;
using Modules.Users.Persistence.Constants;

namespace Modules.Users.Persistence.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);

        builder.HasKey(outboxMessage => outboxMessage.Id);

        builder.Property(outboxMessage => outboxMessage.Id).ValueGeneratedNever();

        builder.Property(outboxMessage => outboxMessage.OccurredOnUtc).IsRequired();

        builder.Property(outboxMessage => outboxMessage.Type).IsRequired();

        builder.Property(outboxMessage => outboxMessage.Content).HasColumnType("json").IsRequired();

        builder.Property(outboxMessage => outboxMessage.ProcessedOnUtc).IsRequired(false);

        builder.Property(outboxMessage => outboxMessage.Error).IsRequired(false);
    }
}