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
            _dapperWriterWrapper.Connect(dbConnection);

            switch (record.GetType().Name)
            {
                case "Admin":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.DeleteAdmin, record, CommandType.StoredProcedure);
                    break;

                case "User":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.DeleteUser, record, CommandType.StoredProcedure);
                    break;

                default:
                    break;
            }

            _dapperWriterWrapper.Disconnect(dbConnection);
        }

        /// <inheritdoc />
        public void Insert(GenericRecord record)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            _dapperWriterWrapper.Connect(dbConnection);

            switch (record.GetType().Name)
            {
                case "Admin":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.InsertNewAdmin, record, CommandType.StoredProcedure);
                    break;

                case "Resort":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.InsertNewResort, record, CommandType.StoredProcedure);
                    break;

                case "ThemePark":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.InsertNewThemePark, record, CommandType.StoredProcedure);
                    break;

                case "User":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.InsertNewUser, record, CommandType.StoredProcedure);
                    break;

                default:
                    break;
            }

            _dapperWriterWrapper.Disconnect(dbConnection);
        }

        /// <inheritdoc />
        public void Update(GenericRecord record)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            _dapperWriterWrapper.Connect(dbConnection);

            switch (record.GetType().Name)
            {
                case "Admin":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.UpdateAdmin, record, CommandType.StoredProcedure);
                    break;

                case "Resort":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.UpdateResort, record, CommandType.StoredProcedure);
                    break;

                case "ThemePark":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.UpdateThemePark, record, CommandType.StoredProcedure);
                    break;

                case "User":
                    _ = _dapperWriterWrapper.Execute(dbConnection, StoredProcedureNames.UpdateUser, record, CommandType.StoredProcedure);
                    break;

                default:
                    break;
            }

            _dapperWriterWrapper.Disconnect(dbConnection);
        }
    }
}