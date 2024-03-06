using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Notifications.Persistence.Constants;
using Persistence.Inbox;

namespace Modules.Notifications.Persistence.Configurations;

internal sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable(TableNames.InboxMessages);

        builder.HasKey(inboxMessage => inboxMessage.Id);

        builder.Property(inboxMessage => inboxMessage.Id).ValueGeneratedNever();

        builder.Property(inboxMessage => inboxMessage.OccurredOnUtc).IsRequired();

        builder.Property(inboxMessage => inboxMessage.Type).IsRequired();

        builder.Property(inboxMessage => inboxMessage.Content).HasColumnType("json").IsRequired();

        builder.Property(inboxMessage => inboxMessage.ProcessedOnUtc).IsRequired(false);

        builder.Property(inboxMessage => inboxMessage.Error).IsRequired(false);
    }
}
