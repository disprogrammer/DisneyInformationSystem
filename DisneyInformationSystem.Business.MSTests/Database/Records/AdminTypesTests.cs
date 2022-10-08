using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdminTypesTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void AdminType_Constructor_WhenProvidingValuesForRecord_ShouldSetProperties()
        {
            // Arrange
            var expectedId = "TAD";
            var expectedName = "Top Admin";

            // Act
            var adminType = new AdminTypes(expectedId, expectedName);

            // Assert
            Assert.AreEqual(expectedId, adminType.ID, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedName, adminType.AdminType, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void AdminType_DefualtConstructor_WhenDefaultConstructorIsCalled_ShouldSetPropertiesWithDefaultValues()
        {
            // Arrange

            // Act
            var adminType = new AdminTypes();

            // Assert
            Assert.IsNull(adminType.ID, AssertMessage.ExpectNullValue);
            Assert.IsNull(adminType.AdminType, AssertMessage.ExpectNullValue);
        }
    }
}