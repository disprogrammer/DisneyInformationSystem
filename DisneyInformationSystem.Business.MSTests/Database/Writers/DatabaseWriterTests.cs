using AutoFixture;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Database.Wrappers;
using DisneyInformationSystem.Business.Database.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.MSTests.Database.Writers
{
    /// <summary>
    /// <see cref="DatabaseWriter"/> tests.
    /// </summary>
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

        /// <summary>
        /// Fixture object.
        /// </summary>
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _connectionString = ConfigurationTestingHelper.GetTestingConfigurationFile();
            _mockDapperWriterWrapper = new Mock<IDapperWriterWrapper>();
            _databaseWriter = new DatabaseWriter(_connectionString, _mockDapperWriterWrapper.Object);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DatabaseWriter_Delete_WhenProvidedWithAdmin_ShouldDeleteAdminInDatabase()
        {
            // Arrange
            var admin = _fixture.Create<Admin>();
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
            var user = _fixture.Create<User>();
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
            var admin = _fixture.Create<Admin>();
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
            var resort = _fixture.Create<Resort>();
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
            var themePark = _fixture.Create<ThemePark>();
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
            var user = _fixture.Create<User>();
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
            var admin = _fixture.Create<Admin>();
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
            var resort = _fixture.Create<Resort>();
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
            var themePark = _fixture.Create<ThemePark>();
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
            var user = _fixture.Create<User>();
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
        /// <param name="query">Stored procedure name.</param>
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