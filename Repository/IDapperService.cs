using Dapper;
using System.Data;
using System.Data.Common;

namespace Repository
{
    public interface IDapperService
    {
        DbConnection GetConnection();

        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
