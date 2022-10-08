using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class GenericRecordTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void GenericRecord_Constructor_WhenProvidedWithPin_ShouldSetProperty()
        {
            // Arrange
            var expectedPin = "1234";

            // Act
            var genericRecord = new GenericRecord(expectedPin);

            // Assert
            Assert.AreEqual(expectedPin, genericRecord.PIN, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void GenericRecord_DefaultConstructor_WhenCallingDefaultConstructor_ShouldSetPropertyToAnEmptyString()
        {
            // Arrange

            // Act
            var genericRecord = new GenericRecord();

            // Assert
            Assert.IsTrue(genericRecord.PIN == string.Empty, AssertMessage.ExpectTrue);
        }
    }
}