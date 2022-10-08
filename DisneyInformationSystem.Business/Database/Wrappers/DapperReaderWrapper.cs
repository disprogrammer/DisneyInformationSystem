using Dapper;
using System.Collections.Generic;
using System.Data;

namespace DisneyInformationSystem.Business.Database.Wrappers
{
    /// <summary>
    /// Dapper Reader Wrapper that queries data for reading data from the database.
    /// </summary>
    public class DapperReaderWrapper : IDapperReaderWrapper
    {
        /// <inheritdoc />
        public IEnumerable<T> Query<T>(IDbConnection connection, string sqlQuery, CommandType commandType)
        {
            return connection.Query<T>(sqlQuery, commandType);
        }

        /// <inheritdoc />
        public T QuerySingle<T>(IDbConnection connection, string sqlQuery, CommandType commandType, object parameters)
        {
            return connection.QuerySingleOrDefault<T>(sqlQuery, parameters, null, null, commandType);
        }
    }
}