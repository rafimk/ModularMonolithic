using Application.Data;
using Application.ServiceLifetimes;
using Dapper;

namespace Persistence.Data;

internal sealed class SqlQueryExecutor : ISqlQueryExecutor, ITransient
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SqlQueryExecutor(ISqlConnectionFactory sqlConnectionFactory) => _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().QueryAsync<T>(sql, parameters);

    public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(
        string sql,
        Func<T1, T2, TResult> map,
        object? parameters = default,
        string splitOn = "Id") =>
        await _sqlConnectionFactory.GetOpenConnection().QueryAsync(sql, map, parameters, splitOn: splitOn);

    public async Task<T?> FirstOrDefaultAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().QueryFirstOrDefaultAsync<T>(sql, parameters);

    public async Task ExecuteAsync(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().ExecuteAsync(sql, parameters);

    public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = default) =>
        await _sqlConnectionFactory.GetOpenConnection().ExecuteScalarAsync<T>(sql, parameters);
}