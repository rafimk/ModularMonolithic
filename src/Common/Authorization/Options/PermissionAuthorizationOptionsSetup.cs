using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Authorization.Options;

internal sealed class PermissionAuthorizationOptionsSetup : IConfigureOptions<PermissionAuthorizationOptions>
{
    private const string ConfigurationSectionName = "Authorization:Permissions";
    private readonly IConfiguration _configuration;

    public PermissionAuthorizationOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(PermissionAuthorizationOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}