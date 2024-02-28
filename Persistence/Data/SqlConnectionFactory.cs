using Application.ServiceLifetimes;
using Microsoft.Extensions.Options;
using Npgsql;
using Persistence.Options;
using System.Data;

namespace Persistence.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory, IDisposable, ITransient
{
    private readonly ConnectionStringOptions _connectionString;
    private Npgsql.NpgsqlConnection? _connection;

    public SqlConnectionFactory(IOptions<ConnectionStringOptions> connectionString) => _connectionString = connectionString.Value;

    /// <inheritdoc />
    public IDbConnection GetOpenConnection()
    {
        if ((_connection ??= new NpgsqlConnection(_connectionString)).State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }

    /// <inheritdoc />
    public void Dispose() => _connection?.Dispose();
}