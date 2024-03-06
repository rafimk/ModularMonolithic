using Infrastructure.EventBus;
using MassTransit;
using Modules.Training.Infrastructure.Idempotence;
using Modules.Users.IntegrationEvents;

namespace Modules.Training.Infrastructure.Consumers;

internal sealed class ConsumerConfiguration : IConsumerConfiguration
{
    /// <inheritdoc />
    public void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();

        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserChangedIntegrationEvent>>();
    }
}
