using AutoFixture;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    /// <summary>
    /// <see cref="RecordHelper{T}"/> tests.
    /// </summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class RecordHelperTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void ServicesHelper_AcronymIsAlreadyInUse_WhenAcronymIsAlreadyUsed_ShouldReturnTrue()
        {
            // Arrange
            var fixture = new Fixture();
            var listOfThemeParks = fixture.CreateMany<ThemePark>().ToList();
            var pin = listOfThemeParks[0].PIN;

            // Act
            var value = RecordHelper<ThemePark>.AcronymIsAlreadyInUse(listOfThemeParks, pin);

            // Assert
            Assert.IsTrue(value, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void ServicesHelper_AcronymIsAlreadyInUse_WhenAcronymIsNotAlreadyUsed_ShouldReturnFalse()
        {
            // Arrange
            var fixture = new Fixture();
            var listOfThemeParks = fixture.CreateMany<ThemePark>().ToList();
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
            var admin = DatabaseMockers.MockSetupListOfAdmins()[0];
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
            var expectedAdmin = DatabaseMockers.MockSetupListOfAdmins()[0];
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
            var user = DatabaseMockers.MockSetupListOfUsers()[0];
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
            var expectedUser = DatabaseMockers.MockSetupListOfUsers()[0];
            var emailAddress = expectedUser.EmailAddress;
            var password = "TestPassword";

            // Act
            var returnValue = RecordHelper<User>.UserSignIn(expectedUser, emailAddress, password);

            // Assert
            var actualUser = returnValue.Item1;
            Assert.AreEqual(expectedUser, actualUser, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(returnValue.Item2, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void RecordHelper_RetrieveListOfPropertiesAndValues_WhenProvidedWithRecord_ShouldReturnListOfStrings()
        {
            // Arrange
            var fixture = new Fixture();
            var themePark = fixture.Create<ThemePark>();
            var expectedNumberOfValues = 13;

            // Act
            var listOfStrings = RecordHelper<ThemePark>.RetrieveListOfPropertiesAndValues(themePark);

            // Assert
            Assert.AreEqual(expectedNumberOfValues, listOfStrings.Count, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(listOfStrings[0].Contains("Name"), AssertMessage.ExpectTrue);
            Assert.IsTrue(listOfStrings[^1].Contains("Closing"), AssertMessage.ExpectTrue);
        }
    }
}