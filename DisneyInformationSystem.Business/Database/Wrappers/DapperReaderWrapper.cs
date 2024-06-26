﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.Database.Wrappers
{
    /// <summary>
    /// Dapper Reader Wrapper that queries data for reading data from the database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DapperReaderWrapper : IDapperReaderWrapper
    {
        /// <inheritdoc />
        public IEnumerable<T> Query<T>(IDbConnection connection, string sqlQuery, CommandType commandType)
        {
            return connection.Query<T>(sqlQuery, commandType);
        }

        /// <inheritdoc />
        public IEnumerable<T> QueryWithParameters<T>(IDbConnection connection, string sqlQuery, CommandType commandType, object parameters)
        {
            return connection.Query<T>(sqlQuery, param: parameters, null, false, null, commandType);
        }

        /// <inheritdoc />
        public T QuerySingle<T>(IDbConnection connection, string sqlQuery, CommandType commandType, object parameters)
        {
            return connection.QuerySingleOrDefault<T>(sqlQuery, parameters, null, null, commandType);
        }
    }
}