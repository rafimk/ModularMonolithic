﻿using Application.ServiceLifetimes;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Shared.Extensions;
using System.Reflection;


namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection InstallServicesFromAssemblies(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies) =>
        services.Tap(
            () => InstanceFactory
                .CreateFromAssemblies<IServiceInstaller>(assemblies)
                .ForEach(serviceInstaller => serviceInstaller.Install(services, configuration)));

    public static IServiceCollection InstallModulesFromAssemblies(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies) =>
        services.Tap(
            () => InstanceFactory
                .CreateFromAssemblies<IModuleInstaller>(assemblies)
                .ForEach(moduleInstaller => moduleInstaller.Install(services, configuration)));

    public static IServiceCollection AddTransientAsMatchingInterfaces(this IServiceCollection services, Assembly assembly) =>
        services.Scan(scan =>
            scan.FromAssemblies(assembly)
                .AddClasses(filter => filter.AssignableTo<ITransient>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithTransientLifetime());

    public static IServiceCollection AddScopedAsMatchingInterfaces(this IServiceCollection services, Assembly assembly) =>
        services.Scan(scan =>
            scan.FromAssemblies(assembly)
                .AddClasses(filter => filter.AssignableTo<IScoped>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithTransientLifetime());
}