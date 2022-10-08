using System.Data;

namespace DisneyInformationSystem.Business.Database.Wrappers
{
    /// <summary>
    /// Dapper Writer Wrapper interface.
    /// </summary>
    public interface IDapperWriterWrapper
    {
        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        /// <param name="connection">Db Connection.</param>
        void Connect(IDbConnection connection);

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        /// <param name="connection">Db Connection.</param>
        void Disconnect(IDbConnection connection);

        /// <summary>
        /// Writes data to the database table based on the query and stored procedure.
        /// </summary>
        /// <typeparam name="T">Generic type item.</typeparam>
        /// <param name="connection">Connection.</param>
        /// <param name="sqlQuery">Sql query.</param>
        /// <param name="item">Item.</param>
        /// <param name="commandType">Command Type.</param>
        /// <returns>Integer.</returns>
        int Execute<T>(IDbConnection connection, string sqlQuery, T item, CommandType commandType);
    }
}