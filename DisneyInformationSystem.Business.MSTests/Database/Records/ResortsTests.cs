using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ResortsTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void Resort_Constructor_WhenProvidingValuesForRecord_ShouldSetProperties()
        {
            var expectedResortId = "WDW";
            var expectedResortName = "Walt Disney World Resort";
            var expectedAddress = "1971 Disney Boulevard, Orlando, FL, 12345";
            var expectedPhoneNumber = "123-456-7890";
            var expectedNumberOfThemeParks = 4;
            var expectedNumberOfResortHotels = 20;
            var expectedNumberOfPartnerHotels = 30;
            var expectedNumberOfWaterParks = 2;
            var expectedNumberOfEntertainmentVenues = 2;
            var expectedOpeningDate = new DateTime(1971, 10, 25);
            var expectedClosingDate = DateTime.MaxValue;

            // Act
            var resort = new Resort(
                expectedResortId,
                expectedResortName,
                expectedAddress,
                expectedPhoneNumber,
                expectedNumberOfThemeParks,
                expectedNumberOfResortHotels,
                expectedNumberOfPartnerHotels,
                expectedNumberOfWaterParks,
                expectedNumberOfEntertainmentVenues,
                true,
                expectedOpeningDate,
                expectedClosingDate);
            var genericRecord = new GenericRecord(expectedResortId);

            // Assert
            Assert.AreEqual(expectedResortId, resort.PIN, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedResortName, resort.ResortName, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedAddress, resort.AddressOfResort, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedPhoneNumber, resort.Phone, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfThemeParks, resort.NumberOfThemeParks, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfResortHotels, resort.NumberOfResortHotels, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfPartnerHotels, resort.NumberOfPartnerHotels, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfWaterParks, resort.NumberOfWaterParks, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfEntertainmentVenues, resort.NumberOfEntertainmentVenues, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(resort.Operating, AssertMessage.ExpectTrue);
            Assert.AreEqual(expectedOpeningDate, resort.OpeningDate, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedClosingDate, resort.ClosingDate, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(resort.PIN, genericRecord.PIN, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Business Test")]
        public void Resort_DefaultConstructor_WhenDefaultConstructorIsCalled_AllPropertiesShouldBeSetToDefaultValue()
        {
            // Arrange

            // Act
            var resort = new Resort();

            // Assert
            Assert.IsNull(resort.PIN, AssertMessage.ExpectNullValue);
            Assert.IsNull(resort.ResortName, AssertMessage.ExpectNullValue);
            Assert.IsNull(resort.AddressOfResort, AssertMessage.ExpectNullValue);
            Assert.IsNull(resort.Phone, AssertMessage.ExpectNullValue);
            Assert.IsTrue(resort.NumberOfThemeParks == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resort.NumberOfResortHotels == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resort.NumberOfPartnerHotels == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resort.NumberOfWaterParks == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resort.NumberOfEntertainmentVenues == 0, AssertMessage.ExpectTrue);
            Assert.IsFalse(resort.Operating, AssertMessage.ExpectFalse);
            Assert.IsTrue(resort.OpeningDate == DateTime.MinValue, AssertMessage.ExpectTrue);
            Assert.IsTrue(resort.ClosingDate == DateTime.MinValue, AssertMessage.ExpectTrue);
        }
    }
}