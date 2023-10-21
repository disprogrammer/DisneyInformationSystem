using System.Collections.Generic;
using System.Data;

namespace DisneyInformationSystem.Business.Database.Wrappers
{
    /// <summary>
    /// Dapper Reader Wrapper interface.
    /// </summary>
    public interface IDapperReaderWrapper
    {
        /// <summary>
        /// Uses database connection and command type to run the sql query for reading data from the database.
        /// </summary>
        /// <typeparam name="T">Generic type of an item.</typeparam>
        /// <param name="connection">Db Connection.</param>
        /// <param name="sqlQuery">SQL Query.</param>
        /// <param name="commandType">Command Type.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Object</returns>
        public T QuerySingle<T>(IDbConnection connection, string sqlQuery, CommandType commandType, object parameters);

        /// <summary>
        /// Uses database connection and command type to run the sql query for reading data from the database.
        /// </summary>
        /// <typeparam name="T">Generic type of an item.</typeparam>
        /// <param name="connection">Db Connection.</param>
        /// <param name="sqlQuery">SQL Query.</param>
        /// <param name="commandType">Command Type.</param>
        /// <returns>List of items.</returns>
        public IEnumerable<T> Query<T>(IDbConnection connection, string sqlQuery, CommandType commandType);

        /// <summary>
        /// Uses database connection and command type to run the sql query for reading data from the database.
        /// </summary>
        /// <typeparam name="T">Record.</typeparam>
        /// <param name="connection">Db connection.</param>
        /// <param name="sqlQuery">SQL Query.</param>
        /// <param name="commandType">Command type.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>List of items.</returns>
        public IEnumerable<T> QueryWithParameters<T>(IDbConnection connection, string sqlQuery, CommandType commandType, object parameters);
    }
}