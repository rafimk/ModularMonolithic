using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Persistence.Constants;

namespace Persistence.Extensions;

public static class NpgsqlDbContextOptionsBuilderExtensions
{
    public static NpgsqlDbContextOptionsBuilder WithMigrationHistoryTableInSchema(
        this NpgsqlDbContextOptionsBuilder dbContextOptionsBuilder,
        string schema) =>
        dbContextOptionsBuilder.MigrationsHistoryTable(TableNames.MigrationHistory, schema);
}