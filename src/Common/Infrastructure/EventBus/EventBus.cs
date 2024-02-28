using Application.EventBus;
using Application.ServiceLifetimes;
using MassTransit;

namespace Infrastructure.EventBus;

public sealed class EventBus : IEventBus, ITransient
{
    private readonly IBus _bus;

    public EventBus(IBus bus) => _bus = bus;

    /// <inheritdoc />
    public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent =>
        await _bus.Publish(integrationEvent, cancellationToken);
}