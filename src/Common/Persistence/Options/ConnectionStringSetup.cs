using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.Options;

internal sealed class ConnectionStringSetup : IConfigureOptions<ConnectionStringOptions>
{
    private const string ConnectionStringName = "Database";
    private readonly IConfiguration _configuration;

    public ConnectionStringSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(ConnectionStringOptions options) => options.Value = _configuration.GetConnectionString(ConnectionStringName)!;
}