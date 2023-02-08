using DisneyInformationSystem.Business.Database.Constants;
using DisneyInformationSystem.Business.Database.Readers;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Database.Wrappers;
using DisneyInformationSystem.Business.Utilities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.Database.Gateways
{
    /// <summary>
    /// Database reader gateway that reads data from the database table for the user interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DatabaseReaderGateway : IDatabaseReaderGateway
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        private static readonly string _connectionString = ConfigurationHelper.ConnectionString();

        /// <summary>
        /// Instantiates a new instance of <see cref="DapperReaderWrapper"/> object.
        /// </summary>
        private static readonly IDapperReaderWrapper _dapperReaderWrapper = new DapperReaderWrapper();

        /// <inheritdoc />
        public Admin RetrieveAdminByEmail(string email)
        {
            var databaseReader = new DatabaseReader<Admin>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetByEmailAddress(StoredProcedureNames.AdminByEmail, email);
        }

        /// <inheritdoc />
        public Admin RetrieveAdminById(string id)
        {
            var databaseReader = new DatabaseReader<Admin>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetById(StoredProcedureNames.AdminByPin, id);
        }

        /// <inheritdoc />
        public List<Admin> RetrieveListOfAdmins()
        {
            var databaseReader = new DatabaseReader<Admin>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetAll(StoredProcedureNames.AllAdmins);
        }

        /// <inheritdoc />
        public List<AdminTypes> RetrieveListOfAdminTypes()
        {
            var databaseReader = new DatabaseReader<AdminTypes>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetAll(StoredProcedureNames.AllAdminTypes);
        }

        /// <inheritdoc />
        public List<User> RetrieveListOfUsers()
        {
            var databaseReader = new DatabaseReader<User>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetAll(StoredProcedureNames.AllUsers);
        }

        /// <inheritdoc />
        public User RetrieveUserByEmail(string email)
        {
            var databaseReader = new DatabaseReader<User>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetByEmailAddress(StoredProcedureNames.UserByEmail, email);
        }

        /// <inheritdoc />
        public User RetrieveUserById(string id)
        {
            var databaseReader = new DatabaseReader<User>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetById(StoredProcedureNames.UserByPin, id);
        }

        /// <inheritdoc />
        public List<Resort> RetrieveListOfResorts()
        {
            var databaseReader = new DatabaseReader<Resort>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetAll(StoredProcedureNames.AllResorts);
        }

        /// <inheritdoc />
        public Resort RetrieveResortByName(string input)
        {
            var databaseReader = new DatabaseReader<Resort>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetByName(StoredProcedureNames.ResortByName, input);
        }

        /// <inheritdoc />
        public Resort RetrieveResortByPin(string id)
        {
            var databaseReader = new DatabaseReader<Resort>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetById(StoredProcedureNames.ResortByPin, id);
        }

        /// <inheritdoc />
        public List<ThemePark> RetrieveListOfThemeParks()
        {
            var databaseReader = new DatabaseReader<ThemePark>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetAll(StoredProcedureNames.AllThemeParks);
        }

        /// <inheritdoc />
        public List<ThemePark> RetrieveThemeParksByResortID(string resortId)
        {
            var databaseReader = new DatabaseReader<ThemePark>(_connectionString, _dapperReaderWrapper);
            return databaseReader.GetRecordsByResortID(StoredProcedureNames.ThemeParksByResortID, resortId);
        }
    }
}