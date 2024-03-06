﻿using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Training.Infrastructure.ServiceInstallers;

internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .ConfigureOptions<ProcessInboxMessagesOptionsSetup>()
            .ConfigureOptions<ProcessOutboxMessagesOptionsSetup>()
            .Tap(AddRecurringJobConfigurations);

    private static void AddRecurringJobConfigurations(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(filter => filter.Where(type => type.IsAssignableTo(typeof(IRecurringJobConfiguration))), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithTransientLifetime());
}