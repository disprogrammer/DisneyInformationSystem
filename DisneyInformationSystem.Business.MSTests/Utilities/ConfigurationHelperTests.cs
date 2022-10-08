using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConfigurationHelperTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void ConfigurationHelper_ConnectionString_WhenCallingMethod_ShouldGetConnectionStringFromConfigurationFile()
        {
            // Arrange

            // Act
            var connectionString = ConfigurationHelper.ConnectionString();

            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(connectionString), AssertMessage.ExpectTrue);
        }
    }
}