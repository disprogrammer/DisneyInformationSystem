using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Database.Wrappers;
using DisneyInformationSystem.Business.Database.Writers;
using DisneyInformationSystem.Business.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.Database.Gateways
{
    /// <summary>
    /// Database writer gateway that writes data from the user interface to the database table.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DatabaseWriterGateway : IDatabaseWriterGateway
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        private static readonly string _connectionString = ConfigurationHelper.ConnectionString();

        /// <summary>
        /// Instantiates a new instance of <see cref="DapperWriterWrapper"/> object.
        /// </summary>
        private static readonly IDapperWriterWrapper _dapperWriterWrapper = new DapperWriterWrapper();

        /// <summary>
        /// Instantiates a new instance of <see cref="DatabaseWriter"/> object.
        /// </summary>
        private static readonly IDatabaseWriter _databaseWriter = new DatabaseWriter(_connectionString, _dapperWriterWrapper);

        public void Delete(GenericRecord record)
        {
            _databaseWriter.Delete(record);
        }

        /// <inheritdoc />
        public void Insert(GenericRecord record)
        {
            _databaseWriter.Insert(record);
        }

        /// <inheritdoc />
        public void Update(GenericRecord record)
        {
            _databaseWriter.Update(record);
        }
    }
}