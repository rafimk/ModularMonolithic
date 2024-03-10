using Infrastructure.Configuration;
using Infrastructure.Extensions;
using MassTransit;

namespace Bootstrap.Api.ServiceInstallers.EventBus;

internal sealed class EventBusServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .ConfigureOptions<MassTransitHostOptionsSetup>()
            .AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.SetKebabCaseEndpointNameFormatter();

                bussConfigurator.AddConsumersFromAssemblies(
                    Modules.Users.Infrastructure.AssemblyReference.Assembly,
                    Modules.Training.Infrastructure.AssemblyReference.Assembly,
                    Modules.Notifications.Infrastructure.AssemblyReference.Assembly);

                bussConfigurator.AddRequestClientsFromAssemblies(
                    Authorization.AssemblyReference.Assembly);

                bussConfigurator.UsingInMemory((context, configurator) => configurator.ConfigureEndpoints(context));
            });
}