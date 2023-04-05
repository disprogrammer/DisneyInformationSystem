using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    /// <summary>
    /// <see cref="Person"/> record tests.
    /// </summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class PersonTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void Person_Constructor_WhenProvidedWithInputs_ShouldSetProperties()
        {
            // Arrange
            var expectedPin = "P930617";
            var expectedEmailAddress = "james@mckinneyinc.com";
            var expectedPassword = "Epcot1982";

            // Act
            var person = new Person(expectedPin, expectedEmailAddress, expectedPassword);

            // Assert
            Assert.AreEqual(expectedPin, person.PIN, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedEmailAddress, person.EmailAddress, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedPassword, person.Password, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void Person_DefaultConstructor_WhenDefaultConstructorIsCalled_ShouldSetPropertiesToDefaultValues()
        {
            // Arrange

            // Act
            var person = new Person();

            // Assert
            Assert.IsNull(person.PIN, AssertMessage.ExpectNullValue);
            Assert.IsNull(person.EmailAddress, AssertMessage.ExpectNullValue);
            Assert.IsNull(person.Password, AssertMessage.ExpectNullValue);
        }
    }
}