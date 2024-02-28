namespace Application.Data;

public interface ISqlQueryExecutor
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = default);

    Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(
        string sql,
        Func<T1, T2, TResult> map,
        object? parameters = default,
        string splitOn = "Id");

    Task<T?> FirstOrDefaultAsync<T>(string sql, object? parameters = default);

    Task ExecuteAsync(string sql, object? parameters = default);

    Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = default);
}