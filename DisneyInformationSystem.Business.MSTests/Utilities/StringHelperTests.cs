using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    /// <summary>
    /// <see cref="StringHelper"/> tests.
    /// </summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class StringHelperTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void StringHelper_SplitObjectsAndPropertiesWords_WhenProvidedWithAnyDisneyApplicationException_ShouldSplitExceptionTypeString()
        {
            // Arrange
            var expectedExceptionTypeString = "Hashed Password Not Supported Exception";
            var exception = new HashedPasswordNotSupportedException();

            // Act
            var actualExceptionTypeString = StringHelper.SplitObjectsAndPropertiesWords(exception.GetType().Name);

            // Assert
            Assert.AreEqual(expectedExceptionTypeString, actualExceptionTypeString, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        [DataRow("", "")]
        [DataRow(" ", " ")]
        [DataRow("epcot", "Epcot")]
        [DataRow("Epcot", "Epcot")]
        [DataRow("magic kingdom", "Magic Kingdom")]
        [DataRow("Magic Kingdom", "Magic Kingdom")]
        public void StringHelper_ToTitleCase_WhenProvidedWithString_ShouldCapitalizeEveryWordInString(string input, string expectedValue)
        {
            // Arrange

            // Act
            var actualValue = StringHelper.ToTitleCase(input);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void StringHelper_PersonTitleString_WhenPersonIsNull_ShouldReturnAppropriateString()
        {
            // Arrange
            var expectedString = "No one signed in.";

            // Act
            var actualString = StringHelper.PersonTitleString(null);

            // Assert
            Assert.AreEqual(expectedString, actualString, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void StringHelper_PersonTitleString_WhenPersonIsUser_ShouldReturnAppropriateString()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers()[0];
            var expectedString = $"{user.PIN}: {user.FirstName} {user.LastName}";

            // Act
            var actualString = StringHelper.PersonTitleString(user);

            // Assert
            Assert.AreEqual(expectedString, actualString, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void StringHelper_PersonTitleString_WhenPersonIsAdmin_ShouldReturnAppropriateString()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins()[0];
            var expectedString = $"{admin.PIN}: {admin.FirstName} {admin.LastName}";

            // Act
            var actualString = StringHelper.PersonTitleString(admin);

            // Assert
            Assert.AreEqual(expectedString, actualString, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}