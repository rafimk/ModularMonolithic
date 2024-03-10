using MassTransit;
using Microsoft.Extensions.Options;

namespace Bootstrap.Api.ServiceInstallers.EventBus;

internal sealed class MassTransitHostOptionsSetup : IConfigureOptions<MassTransitHostOptions>
{
    public void Configure(MassTransitHostOptions options) => options.WaitUntilStarted = true;
}