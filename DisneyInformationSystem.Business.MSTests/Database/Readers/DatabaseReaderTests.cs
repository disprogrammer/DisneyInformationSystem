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

        [TestInitialize]
        public void Initialize()
        {
            _connectionString = ConfigurationTestingHelper.GetTestingConfigurationFile();
            _mockDapperReaderWrapper = new Mock<IDapperReaderWrapper>();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseHelper_GetByEmailAddress_WhenCallingMethod_ShouldReturnObject()
        {
            // Arrange
            var expectedAdmin = DatabaseMockers.MockSetupListOfAdmins().First();
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
            var expectedResort = DatabaseMockers.MockSetupListOfResorts().First();
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
            var expectedResort = DatabaseMockers.MockSetupListOfResorts().First();
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
            var expectedListOfAdmins = DatabaseMockers.MockSetupListOfAdmins();
            var storedProcedureName = "AllAdmins";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfAdmins);

            // Act
            var adminDatabaseReader = new DatabaseReader<Admin>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfAdmins = adminDatabaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfAdmins, actualListOfAdmins, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfAdmins.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForAdminTypes_ShouldReturnListOfAdminTypes()
        {
            // Arrange
            var expectedListOfAdminTypes = ListOfAdminTypes();
            var storedProcedureName = "AllAdminTypes";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfAdminTypes);

            // Act
            var adminDatabaseReader = new DatabaseReader<AdminTypes>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfAdminTypes = adminDatabaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfAdminTypes, actualListOfAdminTypes, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfAdminTypes.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var expectedListOfUsers = DatabaseMockers.MockSetupListOfUsers();
            var storedProcedureName = "AllUsers";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfUsers);

            // Act
            var adminDatabaseReader = new DatabaseReader<User>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfUsers = adminDatabaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfUsers, actualListOfUsers, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfUsers.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForResorts_ShouldReturnListOfResorts()
        {
            // Arrange
            var expectedListOfResorts = DatabaseMockers.MockSetupListOfResorts();
            var storedProcedureName = "AllResorts";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfResorts);

            // Act
            var adminDatabaseReader = new DatabaseReader<Resort>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfResorts = adminDatabaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfResorts, actualListOfResorts, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfResorts.Count > 0, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseReader_GetAll_WhenCallingDatabaseForThemeParks_ShouldReturnListOfThemeParks()
        {
            // Arrange
            var expectedListOfThemeParks = DatabaseMockers.MockSetupListOfThemeParks();
            var storedProcedureName = "AllThemeParks";
            SetupMockDapperReaderWrapperForGetAll(storedProcedureName, expectedListOfThemeParks);

            // Act
            var adminDatabaseReader = new DatabaseReader<ThemePark>(_connectionString, _mockDapperReaderWrapper.Object);
            var actualListOfThemeParks = adminDatabaseReader.GetAll(storedProcedureName);

            // Assert
            Assert.AreEqual(expectedListOfThemeParks, actualListOfThemeParks, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(actualListOfThemeParks.Count > 0, AssertMessage.ExpectTrue);
        }

        private void SetupMockDapperReaderWrapperForGetAll<T>(string query, List<T> listOfItems)
        {
            _ = _mockDapperReaderWrapper.Setup(wrapper => wrapper.Query<T>(It.Is<IDbConnection>(db => db.ConnectionString == _connectionString), query, CommandType.StoredProcedure))
                                    .Returns(listOfItems);
        }

        private void SetupMockDapperReaderWrapperForSingleRows<T>(string query, T item)
        {
            _ = _mockDapperReaderWrapper
                .Setup(wrapper => wrapper.QuerySingle<T>(It.Is<IDbConnection>(db => db.ConnectionString == _connectionString), query, CommandType.StoredProcedure, It.IsAny<object>()))
                .Returns(item);
        }

        private static List<AdminTypes> ListOfAdminTypes()
        {
            return new List<AdminTypes>
            {
                new AdminTypes("TST", "Test Admin")
            };
        }
    }
}