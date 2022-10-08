using Dapper;
using DisneyInformationSystem.Business.Database.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DisneyInformationSystem.Business.Database.Readers
{
    /// <summary>
    /// Reads data from the admin database table.
    /// </summary>
    public class DatabaseReader<T> : IDatabaseReader<T>
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Dapper reader wrapper interface field.
        /// </summary>
        private readonly IDapperReaderWrapper _dapperReaderWrapper;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminDatabaseReader"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="dapperReaderWrapper">Dapper Reader Wrapper.</param>
        public DatabaseReader(string connectionString, IDapperReaderWrapper dapperReaderWrapper)
        {
            _connectionString = connectionString;
            _dapperReaderWrapper = dapperReaderWrapper;
        }

        /// <inheritdoc />
        public T GetByEmailAddress(string storedProcedureName, string parameter)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var parameters = new { EmailAddress = parameter };
            return _dapperReaderWrapper.QuerySingle<T>(connection, storedProcedureName, CommandType.StoredProcedure, parameters);
        }

        /// <inheritdoc />
        public T GetByName(string storedProcedureName, string parameter)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var parameters = new { Name = parameter };
            return _dapperReaderWrapper.QuerySingle<T>(connection, storedProcedureName, CommandType.StoredProcedure, parameters);
        }

        /// <inheritdoc />
        public T GetById(string storedProcedureName, string parameter)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var parameters = new { Pin = parameter };
            return _dapperReaderWrapper.QuerySingle<T>(connection, storedProcedureName, CommandType.StoredProcedure, parameters);
        }

        /// <inheritdoc />
        public List<T> GetAll(string storedProcedureName)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            return _dapperReaderWrapper.Query<T>(connection, storedProcedureName, CommandType.StoredProcedure).AsList();
        }
    }
}