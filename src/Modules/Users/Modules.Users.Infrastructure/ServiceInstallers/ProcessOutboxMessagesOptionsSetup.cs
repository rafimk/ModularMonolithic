using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

namespace Modules.Users.Infrastructure.ServiceInstallers;

internal sealed class ProcessOutboxMessagesOptionsSetup : IConfigureOptions<ProcessOutboxMessagesOptions>
{
    private const string ConfigurationSectionName = "Modules:Users:BackgroundJobs:ProcessOutboxMessages";
    private readonly IConfiguration _configuration;

    public ProcessOutboxMessagesOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(ProcessOutboxMessagesOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}