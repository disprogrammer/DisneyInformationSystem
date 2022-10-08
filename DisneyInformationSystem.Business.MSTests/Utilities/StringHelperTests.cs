using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StringHelperTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void StringHelper_ExceptionTypeStringSplit_WhenProvidedWithAnyDisneyApplicationException_ShouldSplitExceptionTypeString()
        {
            // Arrange
            var expectedExceptionTypeString = "Hashed Password Not Supported Exception";
            var exception = new HashedPasswordNotSupportedException();

            // Act
            var actualExceptionTypeString = StringHelper.ExceptionTypeStringSplit(exception);

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
    }
}