using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Inbox;
using Modules.Users.Persistence.Constants;

namespace Modules.Users.Persistence.Configurations;

internal sealed class InboxMessageConsumerConfiguration : IEntityTypeConfiguration<InboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<InboxMessageConsumer> builder)
    {
        builder.ToTable(TableNames.InboxMessageConsumers);

        builder.HasKey(inboxMessageConsumer => new { inboxMessageConsumer.Id, inboxMessageConsumer.Name });
    }
}