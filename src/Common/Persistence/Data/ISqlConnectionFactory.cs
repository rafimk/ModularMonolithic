using System.Data;

namespace Persistence.Data;

internal interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}