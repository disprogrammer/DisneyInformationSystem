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

        #region Admin
        /// <inheritdoc />
        public void DeleteAdmin(Admin admin)
        {
            _databaseWriter.Delete(admin);
        }

        /// <inheritdoc />
        public void InsertNewAdmin(Admin admin)
        {
            _databaseWriter.Insert(admin);
        }

        /// <inheritdoc />
        public void UpdateAdmin(Admin admin)
        {
            _databaseWriter.Update(admin);
        }
        #endregion

        #region User
        /// <inheritdoc />
        public void DeleteUser(User user)
        {
            _databaseWriter.Delete(user);
        }

        /// <inheritdoc />
        public void InsertNewUser(User user)
        {
            _databaseWriter.Insert(user);
        }

        /// <inheritdoc />
        public void UpdateUser(User user)
        {
            _databaseWriter.Update(user);
        }
        #endregion

        #region Resort
        /// <inheritdoc />
        public void InsertNewResort(Resort resort)
        {
            _databaseWriter.Insert(resort);
        }

        /// <inheritdoc />
        public void UpdateResort(Resort resort)
        {
            _databaseWriter.Update(resort);
        }
        #endregion

        #region Theme Park
        /// <inheritdoc />
        public void InsertNewThemePark(ThemePark themePark)
        {
            _databaseWriter.Insert(themePark);
        }

        /// <inheritdoc />
        public void UpdateThemePark(ThemePark themePark)
        {
            _databaseWriter.Update(themePark);
        }
        #endregion
    }
}