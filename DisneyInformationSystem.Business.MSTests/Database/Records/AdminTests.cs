using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdminTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void Admin_Constructor_WhenProvidingValuesForRecord_ShouldSetProperties()
        {
            // Arrange
            var expectedPin = "1234";
            var expectedAdminTypeCode = "TAD";
            var expectedFirstName = "James";
            var expectedLastName = "McKinney";
            var expectedEmailAddress = "james@mckinneyinc.com";
            var expectedPassword = "Waltdisney1995";
            var expectedAssessmentScore = 100;

            // Act
            var admin = new Admin(expectedPin, expectedAdminTypeCode, expectedFirstName, expectedLastName, expectedEmailAddress, expectedPassword, expectedAssessmentScore);
            var genericRecord = new GenericRecord(expectedPin);

            // Assert
            Assert.AreEqual(expectedPin, admin.PIN, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedAdminTypeCode, admin.AdminTypeCode, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedFirstName, admin.FirstName, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedLastName, admin.LastName, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedEmailAddress, admin.EmailAddress, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedPassword, admin.Password, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedAssessmentScore, admin.AssessmentScore, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(admin.PIN, genericRecord.PIN, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void Admin_DefaultConstructor_WhenDefaultConstructorIsCalled_AllPropertiesShouldBeSetToDefaultValue()
        {
            // Arrange

            // Act
            var admin = new Admin();

            // Assert
            Assert.IsNull(admin.PIN, AssertMessage.ExpectNullValue);
            Assert.IsNull(admin.AdminTypeCode, AssertMessage.ExpectNullValue);
            Assert.IsNull(admin.FirstName, AssertMessage.ExpectNullValue);
            Assert.IsNull(admin.LastName, AssertMessage.ExpectNullValue);
            Assert.IsNull(admin.EmailAddress, AssertMessage.ExpectNullValue);
            Assert.IsNull(admin.Password, AssertMessage.ExpectNullValue);
            Assert.IsTrue(admin.AssessmentScore == 0, AssertMessage.ExpectTrue);
        }
    }
}