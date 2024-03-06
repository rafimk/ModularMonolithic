using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Persistence.Constants;
using Persistence.Inbox;

namespace Modules.Training.Persistence.Configurations;

internal sealed class InboxMessageConsumerConfiguration : IEntityTypeConfiguration<InboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<InboxMessageConsumer> builder)
    {
        builder.ToTable(TableNames.InboxMessageConsumers);

        builder.HasKey(inboxMessageConsumer => new { inboxMessageConsumer.Id, inboxMessageConsumer.Name });
    }
}