using Application.EventBus;
using Application.Time;
using Infrastructure.Configuration;
using Infrastructure.EventBus;
using Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Extensions;


namespace Modules.Training.Infrastructure.ServiceInstallers;

internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .Tap(services.TryAddTransient<ISystemTime, SystemTime>)
            .Tap(services.TryAddTransient<IEventBus, EventBus>);
}