using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bootstrap.Api.ServiceInstallers.Authentication;

internal sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "JwtBearer";
    private readonly IConfiguration _configuration;

    public JwtBearerOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(JwtBearerOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);

    public void Configure(string name, JwtBearerOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}