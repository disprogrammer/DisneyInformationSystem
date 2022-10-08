using DisneyInformationSystem.Business.Database.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Wrappers
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DapperWriterWrapperTests
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString;

        [TestInitialize]
        public void Initialize()
        {
            _connectionString = ConfigurationTestingHelper.GetTestingConfigurationFile();
        }

        [TestMethod, TestCategory("Business Test")]
        public void DapperWriterWrapper_Connect_WhenProvidedWithDbConnection_ShouldOpenConnection()
        {
            // Arrange
            var expectedConnectionState = ConnectionState.Open;
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            var dapperWriterWrapper = new DapperWriterWrapper();

            // Act
            dapperWriterWrapper.Connect(dbConnection);

            // Assert
            var actualConnectionState = dbConnection.State;
            Assert.AreEqual(expectedConnectionState, actualConnectionState, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void DapperWriterWrapper_Disconnect_WhenProvidedWithDbConnection_ShouldCloseConnection()
        {
            // Arrange
            var expectedConnectionState = ConnectionState.Closed;
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            var dapperWriterWrapper = new DapperWriterWrapper();

            // Act
            dapperWriterWrapper.Disconnect(dbConnection);

            // Assert
            var actualConnectionString = dbConnection.State;
            Assert.AreEqual(expectedConnectionState, actualConnectionString, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}