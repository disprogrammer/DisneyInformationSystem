using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RecordHelperTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void ServicesHelper_AcronymIsAlreadyInUse_WhenAcronymIsAlreadyUsed_ShouldReturnTrue()
        {
            // Arrange
            var listOfThemeParks = DatabaseMockers.MockSetupListOfThemeParks();
            var pin = listOfThemeParks.First().PIN;

            // Act
            var value = RecordHelper<ThemePark>.AcronymIsAlreadyInUse(listOfThemeParks, pin);

            // Assert
            Assert.IsTrue(value, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void ServicesHelper_AcronymIsAlreadyInUse_WhenAcronymIsNotAlreadyUsed_ShouldReturnFalse()
        {
            // Arrange
            var listOfThemeParks = DatabaseMockers.MockSetupListOfThemeParks();
            var pin = "DLR";

            // Act
            var value = RecordHelper<ThemePark>.AcronymIsAlreadyInUse(listOfThemeParks, pin);

            // Assert
            Assert.IsFalse(value, AssertMessage.ExpectFalse);
        }

        [TestMethod, TestCategory("Business Test")]
        [ExpectedException(typeof(EmailNotFoundException))]
        public void RecordHelper_AdminSignIn_WhenEmailIsNotFound_ShouldThrowEmailNotFoundException()
        {
            // Arrange
            var emailAddress = "abc@gmail.com";
            var password = "password";

            // Act
            _ = RecordHelper<Admin>.AdminSignIn(null, emailAddress, password);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void RecordHelper_AdminSignIn_WhenPasswordIsIncorrect_ShouldThrowInvalidPasswordException()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins().First();
            var emailAddress = "abc@gmail.com";
            var password = "TestPassword1";

            // Act
            _ = RecordHelper<Admin>.AdminSignIn(admin, emailAddress, password);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RecordHelper_AdminSignIn_WhenValidAdmin_ShouldReturnCorrectTuple()
        {
            // Arrange
            var expectedAdmin = DatabaseMockers.MockSetupListOfAdmins().First();
            var emailAddress = expectedAdmin.EmailAddress;
            var password = "TestPassword";

            // Act
            var returnValue = RecordHelper<Admin>.AdminSignIn(expectedAdmin, emailAddress, password);

            // Assert
            var actualAdmin = returnValue.Item1;
            Assert.AreEqual(expectedAdmin, actualAdmin, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(returnValue.Item2, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        [ExpectedException(typeof(EmailNotFoundException))]
        public void RecordHelper_UserSignIn_WhenEmailIsNotFound_ShouldThrowEmailNotFoundException()
        {
            // Arrange
            var emailAddress = "abc@gmail.com";
            var password = "password";

            // Act
            _ = RecordHelper<User>.UserSignIn(null, emailAddress, password);
        }

        [TestMethod, TestCategory("Business Test")]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void RecordHelper_UserSignIn_WhenPasswordIsIncorrect_ShouldThrowInvalidPasswordException()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers().First();
            var emailAddress = "abc@gmail.com";
            var password = "TestPassword1";

            // Act
            _ = RecordHelper<User>.UserSignIn(user, emailAddress, password);

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void RecordHelper_UserSignIn_WhenValidUser_ShouldReturnCorrectTuple()
        {
            // Arrange
            var expectedUser = DatabaseMockers.MockSetupListOfUsers().First();
            var emailAddress = expectedUser.EmailAddress;
            var password = "TestPassword";

            // Act
            var returnValue = RecordHelper<User>.UserSignIn(expectedUser, emailAddress, password);

            // Assert
            var actualUser = returnValue.Item1;
            Assert.AreEqual(expectedUser, actualUser, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(returnValue.Item2, AssertMessage.ExpectTrue);
        }
    }
}