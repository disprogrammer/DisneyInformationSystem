using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UserTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void User_Constructor_WhenProvidedWithInputs_ShouldSetProperties()
        {
            // Arrange
            var expectedPin = "U1234567890";
            var expectedFirstName = "Hollywood";
            var expectedLastName = "Studios";
            var expectedPhoneNumber = "1234567890";
            var expectedEmailAddress = "epcot@center.com";
            var expectedPassword = "AnimalKingdom1998";
            var expectedHomeAddress = "1971 Magic Kingdom Boulevard, Orlando, FL 09876";

            // Act
            var user = new User(expectedPin, expectedFirstName, expectedLastName, expectedPhoneNumber, expectedEmailAddress, expectedPassword, expectedHomeAddress);

            // Assert
            Assert.AreEqual(expectedPin, user.PIN, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedFirstName, user.FirstName, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedLastName, user.LastName, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedPhoneNumber, user.PhoneNumber, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedEmailAddress, user.EmailAddress, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedPassword, user.Password, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedHomeAddress, user.HomeAddress, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void User_DefaultConstructor_WhenDefaultConstructorIsCalled_ShouldSetPropertiesToDefaultValues()
        {
            // Arrange

            // Act
            var user = new User();

            // Assert
            Assert.IsNull(user.PIN, AssertMessage.ExpectNullValue);
            Assert.IsNull(user.FirstName, AssertMessage.ExpectNullValue);
            Assert.IsNull(user.LastName, AssertMessage.ExpectNullValue);
            Assert.IsNull(user.PhoneNumber, AssertMessage.ExpectNullValue);
            Assert.IsNull(user.EmailAddress, AssertMessage.ExpectNullValue);
            Assert.IsNull(user.Password, AssertMessage.ExpectNullValue);
            Assert.IsNull(user.HomeAddress, AssertMessage.ExpectNullValue);
        }
    }
}