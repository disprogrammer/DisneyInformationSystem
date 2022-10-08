using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Database.Wrappers;
using DisneyInformationSystem.Business.Database.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Writers
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DatabaseWriterTests
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Mocks the dapper writer wrapper interface.
        /// </summary>
        private Mock<IDapperWriterWrapper> _mockDapperWriterWrapper;

        /// <summary>
        /// Class to test.
        /// </summary>
        private DatabaseWriter _databaseWriter;

        [TestInitialize]
        public void Initialize()
        {
            _connectionString = ConfigurationTestingHelper.GetTestingConfigurationFile();
            _mockDapperWriterWrapper = new Mock<IDapperWriterWrapper>();
            _databaseWriter = new DatabaseWriter(_connectionString, _mockDapperWriterWrapper.Object);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Delete_WhenProvidedWithAdmin_ShouldDeleteAdminInDatabase()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins().First();
            MockDapperWriterWrapperSetup("DeleteAdmin", admin);

            // Act
            _databaseWriter.Delete(admin);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Delete_WhenProvidedWithUser_ShouldDeleteUserInDatabase()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers().First();
            MockDapperWriterWrapperSetup("DeleteUser", user);

            // Act
            _databaseWriter.Delete(user);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Delete_WhenProvidedWithNotValidType_ShouldNotPerformQuery()
        {
            // Arrange
            var genericRecord = new GenericRecord();

            // Act
            _databaseWriter.Delete(genericRecord);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Insert_WhenProvidedWithAdmin_ShouldAddToDatabase()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins().First();
            MockDapperWriterWrapperSetup("InsertNewAdmin", admin);

            // Act
            _databaseWriter.Insert(admin);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Insert_WhenProvidedWithResort_ShouldAddToDatabase()
        {
            // Arrange
            var resort = DatabaseMockers.MockSetupListOfResorts().First();
            MockDapperWriterWrapperSetup("InsertNewResort", resort);

            // Act
            _databaseWriter.Insert(resort);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Insert_WhenProvidedWithThemePark_ShouldAddToDatabase()
        {
            // Arrange
            var themePark = DatabaseMockers.MockSetupListOfThemeParks().First();
            MockDapperWriterWrapperSetup("InsertNewThemePark", themePark);

            // Act
            _databaseWriter.Insert(themePark);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Insert_WhenProvidedWithUser_ShouldAddToDatabase()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers().First();
            MockDapperWriterWrapperSetup("InsertNewUser", user);

            // Act
            _databaseWriter.Insert(user);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Insert_WhenProvidedWithUnvalidType_ShouldNotAddToDatabase()
        {
            // Arrange
            var genericRecord = new GenericRecord();

            // Act
            _databaseWriter.Insert(genericRecord);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Update_WhenProvidedWithAdmin_ShouldUpdateAdminInDatabase()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins().First();
            MockDapperWriterWrapperSetup("UpdateAdmin", admin);

            // Act
            _databaseWriter.Update(admin);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Update_WhenProvidedWithResort_ShouldUpdateResortInDatabase()
        {
            // Arrange
            var resort = DatabaseMockers.MockSetupListOfResorts().First();
            MockDapperWriterWrapperSetup("UpdateResort", resort);

            // Act
            _databaseWriter.Update(resort);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Update_WhenProvidedWithThemePark_ShouldUpdateThemeParkInDatabase()
        {
            // Arrange
            var themePark = DatabaseMockers.MockSetupListOfThemeParks().First();
            MockDapperWriterWrapperSetup("UpdateThemePark", themePark);

            // Act
            _databaseWriter.Update(themePark);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Update_WhenProvidedWithUser_ShouldUpdateUserInDatabase()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers().First();
            MockDapperWriterWrapperSetup("UpdateUser", user);

            // Act
            _databaseWriter.Update(user);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Update_WhenProvidedWithUnvalidType_ShouldNotUpdateDatabase()
        {
            // Arrange
            var genericRecord = new GenericRecord();

            // Act
            _databaseWriter.Update(genericRecord);

            // Assert
            _mockDapperWriterWrapper.Verify();
        }

        /// <summary>
        /// Sets up the mock dapper writer wrapper.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <param name="record">Record.</param>
        private void MockDapperWriterWrapperSetup(string query, GenericRecord record)
        {
            _mockDapperWriterWrapper.Setup(wrapper => wrapper.Execute(
                It.Is<IDbConnection>(db => db.ConnectionString == _connectionString),
                query,
                record,
                CommandType.StoredProcedure))
                .Verifiable();
        }
    }
}