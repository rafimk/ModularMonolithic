using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Persistence.Constants;
using Persistence.Outbox;

namespace Modules.Training.Persistence.Configurations;

internal sealed class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        builder.ToTable(TableNames.OutboxMessageConsumers);

        builder.HasKey(outboxMessageConsumer => new { outboxMessageConsumer.Id, outboxMessageConsumer.Name });
    }
}