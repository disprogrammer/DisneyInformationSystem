using AutoFixture;
using DisneyInformationSystem.Business.Database.Readers;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Database.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Readers
{
    /// <summary>
    /// <see cref="DatabaseReader{T}"/> tests.
    /// </summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class DatabaseReaderTests
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Mocks the dapper reader wrapper interface.
        /// </summary>
        private Mock<IDapperReaderWrapper> _mockDapperReaderWrapper;

        /// <summary>
        /// Fixture object.
        /// </summary>
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _connectionString = ConfigurationTestingHelper.GetTestingConfigurationFile();
            _mockDapperReaderWrapper = new Mock<IDapperReaderWrapper>();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseHelper_GetByEmailAddress_WhenCallingMethod_ShouldReturnObject()
        {
            // Arrange
            var expectedAdmin = _fixture.Create<Admin>();
            var storedProcedureName = "AdminByEmail";
            SetupMockDapperReaderWrapperForSingleRows(storedProcedureName, expectedAdmin);
            var databaseReader = new DatabaseReader<Admin>(_connectionString, _mockDapperReaderWrapper.Object);

            // Act
            var actualAdmin = databaseReader.GetByEmailAddress(storedProcedureName, expectedAdmin.EmailAddress);

            // Assert
            Assert.AreEqual(expectedAdmin, actualAdmin, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseHelper_GetByName_WhenCallingMethod_ShouldReturnObject()
        {
            // Arrange
            var expectedResort = _fixture.Create<Resort>();
            var storedProcedureName = "ResortByName";
            SetupMockDapperReaderWrapperForSingleRows(storedProcedureName, expectedResort);
            var databaseReader = new DatabaseReader<Resort>(_connectionString, _mockDapperReaderWrapper.Object);

            // Act
            var actualResort = databaseReader.GetByName(storedProcedureName, expectedResort.ResortName);

            // Assert
            Assert.AreEqual(expectedResort, actualResort, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseHelper_GetById_WhenCallingMethod_ShouldReturnObject()
        {
            // Arrange
            var expectedResort = _fixture.Create<Resort>();
            var storedProcedureName = "ResortByPin";
            SetupMockDapperReaderWrapperForSingleRows(storedProcedureName, expectedResort);
            var databaseReader = new DatabaseReader<Resort>(_connectionString, _mockDapperReaderWrapper.Object);

            // Act
            var actualResort = databaseReader.GetById(storedProcedureName, expectedResort.PIN);

            // Assert
            Assert.AreEqual(expectedResort, actualResort, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForAdmins_ShouldReturnListOfAdmins()
        {
            // Arrange
            var expectedListOfAdmins = _fixture.CreateMany<Admin>().ToList();
            var storedProcedureName = "AllAdmins";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfAdmins);

            // Act
            var databaseReader = new DatabaseReader<Admin>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfAdmins = databaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfAdmins, actualListOfAdmins, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfAdmins.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForAdminTypes_ShouldReturnListOfAdminTypes()
        {
            // Arrange
            var expectedListOfAdminTypes = _fixture.CreateMany<AdminTypes>().ToList();
            var storedProcedureName = "AllAdminTypes";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfAdminTypes);

            // Act
            var databaseReader = new DatabaseReader<AdminTypes>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfAdminTypes = databaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfAdminTypes, actualListOfAdminTypes, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfAdminTypes.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var expectedListOfUsers = _fixture.CreateMany<User>().ToList();
            var storedProcedureName = "AllUsers";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfUsers);

            // Act
            var databaseReader = new DatabaseReader<User>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfUsers = databaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfUsers, actualListOfUsers, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfUsers.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForResorts_ShouldReturnListOfResorts()
        {
            // Arrange
            var expectedListOfResorts = _fixture.CreateMany<Resort>().ToList();
            var storedProcedureName = "AllResorts";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfResorts);

            // Act
            var databaseReader = new DatabaseReader<Resort>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfResorts = databaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfResorts, actualListOfResorts, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfResorts.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForThemeParks_ShouldReturnListOfThemeParks()
        {
            // Arrange
            var expectedListOfThemeParks = _fixture.CreateMany<ThemePark>().ToList();
            var storedProcedureName = "AllThemeParks";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfThemeParks);

            // Act
            var databaseReader = new DatabaseReader<ThemePark>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfThemeParks = databaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfThemeParks, actualListOfThemeParks, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfThemeParks.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetRecordsByResortID_WhenCallingDatabaseForThemeParksByResortId_ShouldReturnListOfThemeParks()
        {
            // Arrange
            var expectedListOfThemeParks = _fixture.CreateMany<ThemePark>().ToList();
            var storedProcedureName = "ThemeParksByResortID";
            SetupMockDapperReaderWrapperForGettingRecordsByResortId(storedProcedureName, expectedListOfThemeParks);

            // Act
            var databaseReader = new DatabaseReader<ThemePark>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfThemeParks = databaseReader.GetRecordsByResortID(storedProcedureName, "WDW");

            // Assert
            Assert.AreEqual(expectedListOfThemeParks, actualListOfThemeParks, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfThemeParks.Count > 0, AssertMessage.ExpectTrue);
        }

        /// <summary>
        /// Mock setup of the dapper reader for get all rows from database.
        /// </summary>
        /// <typeparam name="T">Record type.</typeparam>
        /// <param name="query">Stored procedure name.</param>
        /// <param name="listOfItems">List of records.</param>
        private void SetupMockDapperReaderWrapperForGetAll<T>(string query, List<T> listOfItems)
        {
            _ = _mockDapperReaderWrapper.Setup(wrapper => wrapper.Query<T>(It.Is<IDbConnection>(db => db.ConnectionString == _connectionString), query, CommandType.StoredProcedure))
                                    .Returns(listOfItems);
        }

        /// <summary>
        /// Mock setup of the dapper reader for getting database rows by resort id.
        /// </summary>
        /// <typeparam name="T">Record type.</typeparam>
        /// <param name="query">Stored procedure name.</param>
        /// <param name="listOfItems">List of records.</param>
        private void SetupMockDapperReaderWrapperForGettingRecordsByResortId<T>(string query, List<T> listOfItems)
        {
            _ = _mockDapperReaderWrapper.Setup(wrapper => wrapper.QueryWithParameters<T>(It.Is<IDbConnection>(db => db.ConnectionString == _connectionString), query, CommandType.StoredProcedure, It.IsAny<object>()))
                .Returns(listOfItems);
        }

        /// <summary>
        /// Mock setup of the dapper reader for getting single rows from database.
        /// </summary>
        /// <typeparam name="T">Record type.</typeparam>
        /// <param name="query">Stored procedure name.</param>
        /// <param name="item">Record.</param>
        private void SetupMockDapperReaderWrapperForSingleRows<T>(string query, T item)
        {
            _ = _mockDapperReaderWrapper
                .Setup(wrapper => wrapper.QuerySingle<T>(It.Is<IDbConnection>(db => db.ConnectionString == _connectionString), query, CommandType.StoredProcedure, It.IsAny<object>()))
                .Returns(item);
        }
    }
}