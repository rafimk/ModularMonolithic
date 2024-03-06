using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Notifications.Infrastructure.Email.Configuration;

internal sealed class MailersendOptionsSetup : IConfigureOptions<MailersendOptions>
{
    private const string ConfigurationSectionName = "Modules:Notifications:Mailersend";
    private readonly IConfiguration _configuration;

    public MailersendOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(MailersendOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}