using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Modules.Training.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

namespace Modules.Training.Infrastructure.ServiceInstallers;

internal sealed class ProcessOutboxMessagesOptionsSetup : IConfigureOptions<ProcessOutboxMessagesOptions>
{
    private const string ConfigurationSectionName = "Modules:Training:BackgroundJobs:ProcessOutboxMessages";
    private readonly IConfiguration _configuration;

    public ProcessOutboxMessagesOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(ProcessOutboxMessagesOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}