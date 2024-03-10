using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Infrastructure.Authorization.SameUser.AuthorizationHandlers;
using Modules.Users.Infrastructure.Authorization.SameUser.Options;
using Modules.Users.Infrastructure.Authorization.SameUser.Services;

namespace Modules.Users.Infrastructure.ServiceInstallers;

internal sealed class AuthorizationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddHttpContextAccessor()
            .ConfigureOptions<AuthorizationOptionsSetup>()
            .ConfigureOptions<SameUserAuthorizationOptionsSetup>()
            .AddTransient<IUserChecker, UserChecker>()
            .AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();
}