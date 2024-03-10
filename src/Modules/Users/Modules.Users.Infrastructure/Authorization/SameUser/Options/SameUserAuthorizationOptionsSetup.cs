using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Users.Infrastructure.Authorization.SameUser.Options;

internal sealed class SameUserAuthorizationOptionsSetup : IConfigureOptions<SameUserAuthorizationOptions>
{
    private const string ConfigurationSectionName = "Modules:Users:Authorization:SameUser";
    private readonly IConfiguration _configuration;

    public SameUserAuthorizationOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(SameUserAuthorizationOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}