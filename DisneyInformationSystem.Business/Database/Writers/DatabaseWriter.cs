using DisneyInformationSystem.Business.Database.Constants;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Database.Wrappers;
using System.Data;
using System.Data.SqlClient;

namespace DisneyInformationSystem.Business.Database.Writers
{
    /// <summary>
    /// Database writer class for deleting, inserting, and updating rows in a certain table.
    /// </summary>
    public class DatabaseWriter : IDatabaseWriter
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Dapper writer wrapper interface field.
        /// </summary>
        private readonly IDapperWriterWrapper _dapperWriterWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseWriter"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="dapperWriterWrapper">Dapper writer wrapper.</param>
        public DatabaseWriter(string connectionString, IDapperWriterWrapper dapperWriterWrapper)
        {
            _connectionString = connectionString;
            _dapperWriterWrapper = dapperWriterWrapper;
        }

        /// <inheritdoc />
        public void Delete(GenericRecord record)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var storedProcedureName = StoredProcedureNames.DeleteStoredProcedures.Find(s => s[6..].Equals(record.GetType().Name));
            _dapperWriterWrapper.Connect(dbConnection);
            _ = _dapperWriterWrapper.Execute(dbConnection, storedProcedureName, record, CommandType.StoredProcedure);
            _dapperWriterWrapper.Disconnect(dbConnection);
        }

        /// <inheritdoc />
        public void Insert(GenericRecord record)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var storedProcedureName = StoredProcedureNames.InsertStoredProcedures.Find(s => s[9..].Equals(record.GetType().Name));
            _dapperWriterWrapper.Connect(dbConnection);
            _ = _dapperWriterWrapper.Execute(dbConnection, storedProcedureName, record, CommandType.StoredProcedure);
            _dapperWriterWrapper.Disconnect(dbConnection);
        }

        /// <inheritdoc />
        public void Update(GenericRecord record)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var storedProcedureName = StoredProcedureNames.UpdateStoredProcedures.Find(s => s[6..].Equals(record.GetType().Name));
            _dapperWriterWrapper.Connect(dbConnection);
            _ = _dapperWriterWrapper.Execute(dbConnection, storedProcedureName, record, CommandType.StoredProcedure);
            _dapperWriterWrapper.Disconnect(dbConnection);
        }
    }
}