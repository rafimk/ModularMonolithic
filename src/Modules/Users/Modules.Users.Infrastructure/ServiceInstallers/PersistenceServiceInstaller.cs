using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Modules.Users.Persistence;
using Modules.Users.Persistence.Constants;
using Persistence.Extensions;
using Persistence.Interceptors;
using Persistence.Options;
using Shared.Extensions;

namespace Modules.Users.Infrastructure.ServiceInstallers;

internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .Tap(services.TryAddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>)
            .Tap(services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>)
            .AddDbContext<UsersDbContext>((serviceProvider, options) =>
            {
                ConnectionStringOptions connectionString = serviceProvider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

                options
                    .UseNpgsql(
                        connectionString,
                        dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.Users))
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(
                        serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!,
                        serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>()!);
            });
}