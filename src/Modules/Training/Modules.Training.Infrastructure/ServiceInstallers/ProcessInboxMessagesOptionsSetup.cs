﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Modules.Training.Infrastructure.BackgroundJobs.ProcessInboxMessages;

namespace Modules.Training.Infrastructure.ServiceInstallers;

internal sealed class ProcessInboxMessagesOptionsSetup : IConfigureOptions<ProcessInboxMessagesOptions>
{
    private const string ConfigurationSectionName = "Modules:Training:BackgroundJobs:ProcessInboxMessages";
    private readonly IConfiguration _configuration;

    public ProcessInboxMessagesOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(ProcessInboxMessagesOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}