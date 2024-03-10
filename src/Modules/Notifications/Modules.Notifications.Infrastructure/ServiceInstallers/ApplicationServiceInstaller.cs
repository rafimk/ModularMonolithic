using Infrastructure.Configuration;
using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Notifications.Infrastructure.Idempotence;
using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.ServiceInstallers;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly))
            .Tap(AddAndDecorateIntegrationEventHandlersWithIdempotency);

    private static void AddAndDecorateIntegrationEventHandlersWithIdempotency(IServiceCollection services) =>
        Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(EventHandlersUtility.ImplementsIntegrationEventHandler)
            .ForEach(integrationEventHandlerType =>
            {
                Type closedIntegrationEventHandler = integrationEventHandlerType
                    .GetInterfaces()
                    .First(EventHandlersUtility.IsIntegrationEventHandler);

                Type[] arguments = closedIntegrationEventHandler.GetGenericArguments();

                Type closedIdempotentHandler = typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(arguments);

                services.AddScoped(integrationEventHandlerType);

                services.Decorate(integrationEventHandlerType, closedIdempotentHandler);
            });
}