using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.Database.Wrappers
{
    /// <summary>
    /// Dapper Reader Wrapper that queries data for writing data to the database.
    /// </summary>
    public class DapperWriterWrapper : IDapperWriterWrapper
    {
        /// <inheritdoc />
        public void Connect(IDbConnection connection)
        {
            connection.Open();
        }

        /// <inheritdoc />
        public void Disconnect(IDbConnection connection)
        {
            connection.Close();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public int Execute<T>(IDbConnection connection, string sqlQuery, T item, CommandType commandType)
        {
            return connection.Execute(sqlQuery, item, commandType: commandType);
        }
    }
}