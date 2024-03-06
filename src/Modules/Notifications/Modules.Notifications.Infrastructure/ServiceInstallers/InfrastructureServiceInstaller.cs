using Application.EventBus;
using Application.Time;
using Infrastructure.Configuration;
using Infrastructure.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Notifications.Application.Abstractions.Email;
using Modules.Notifications.Infrastructure.Email.Abstractions;
using Modules.Notifications.Infrastructure.Email.Configuration;
using Modules.Notifications.Infrastructure.Email.DelegatingHandlers;
using Modules.Notifications.Infrastructure.Email;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Refit;
using Shared.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Infrastructure.Time;
using Microsoft.Extensions.Options;

namespace Modules.Notifications.Infrastructure.ServiceInstallers;

internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .Tap(services.TryAddTransient<ISystemTime, SystemTime>)
            .Tap(services.TryAddTransient<IEventBus, EventBus>)
            .Tap(AddMailerSend);

    private static void AddMailerSend(IServiceCollection services) =>
        services
            .ConfigureOptions<MailersendOptionsSetup>()
            .AddTransient<MailersendAuthorizationDelegatingHandler>()
            .AddTransient<IEmailSender, EmailSender>()
            .AddRefitClient<IMailersendClient>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    })
            })
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                MailersendOptions mailersendOptions = serviceProvider.GetService<IOptions<MailersendOptions>>()!.Value;

                httpClient.BaseAddress = new Uri(mailersendOptions.BaseUrl);
            })
            .AddHttpMessageHandler<MailersendAuthorizationDelegatingHandler>();
}