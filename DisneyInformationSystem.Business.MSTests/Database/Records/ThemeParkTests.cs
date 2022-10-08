using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ThemeParkTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void ThemePark_Constructor_WhenProvidingValuesForRecord_ShouldSetProperties()
        {
            // Arrange
            var expectedPin = "MKP";
            var expectedResortId = "WDW";
            var expectedParkName = "Magic Kingdom";
            var expectedAddressOfThemePark = "1971 Dream Way, Orlando, FL 10015";
            var expectedPhoneNumber = "123-456-7890";
            var expectedTransporation = "Monorail, Boat, Car, Walk";
            var expectedNumberOfLands = 6;
            var expectedNumberOfAttractions = 34;
            var expectedNumberOfShops = 20;
            var expectedNumberOfRestaurants = 15;
            var expectedNumberOfTours = 7;
            var expectedNumberOfRestrooms = 13;
            var expectedOpeningDate = new DateTime(1971, 10, 01);
            var expectedClosingDate = DateTime.MaxValue;

            // Act
            var themePark = new ThemePark(
                expectedPin,
                expectedResortId,
                expectedParkName,
                expectedAddressOfThemePark,
                expectedPhoneNumber,
                expectedTransporation,
                expectedNumberOfLands,
                expectedNumberOfAttractions,
                expectedNumberOfShops,
                expectedNumberOfRestaurants,
                expectedNumberOfTours,
                expectedNumberOfRestrooms,
                true,
                expectedOpeningDate,
                expectedClosingDate);
            var genericRecord = new GenericRecord(expectedPin);

            // Assert
            Assert.AreEqual(expectedPin, themePark.PIN, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedResortId, themePark.ResortID, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedParkName, themePark.ParkName, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedAddressOfThemePark, themePark.AddressOfPark, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedPhoneNumber, themePark.Phone, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedTransporation, themePark.Transportation, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfLands, themePark.NumberOfLands, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfAttractions, themePark.NumberOfAttractions, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfShops, themePark.NumberOfShops, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfRestaurants, themePark.NumberOfRestaurants, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfTours, themePark.NumberOfTours, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedNumberOfRestrooms, themePark.NumberOfRestrooms, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedOpeningDate, themePark.OpeningDate, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(expectedClosingDate, themePark.ClosingDate, AssertMessage.ExpectValuesToBeEqual);
            Assert.AreEqual(themePark.PIN, genericRecord.PIN, AssertMessage.ExpectValuesToBeEqual);
            Assert.IsTrue(themePark.Operating, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void ThemePark_DefaultConstructor_WhenDefaultConstructorIsCalled_ShouldSetPropertiesToTheirDefaultValues()
        {
            // Assert

            // Act
            var themePark = new ThemePark();

            // Assert
            Assert.IsNull(themePark.PIN, AssertMessage.ExpectNullValue);
            Assert.IsNull(themePark.ResortID, AssertMessage.ExpectNullValue);
            Assert.IsNull(themePark.ParkName, AssertMessage.ExpectNullValue);
            Assert.IsNull(themePark.AddressOfPark, AssertMessage.ExpectNullValue);
            Assert.IsNull(themePark.Phone, AssertMessage.ExpectNullValue);
            Assert.IsNull(themePark.Transportation, AssertMessage.ExpectNullValue);
            Assert.IsTrue(themePark.NumberOfLands == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(themePark.NumberOfAttractions == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(themePark.NumberOfShops == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(themePark.NumberOfRestaurants == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(themePark.NumberOfTours == 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(themePark.NumberOfRestrooms == 0, AssertMessage.ExpectTrue);
            Assert.IsFalse(themePark.Operating, AssertMessage.ExpectFalse);
            Assert.IsTrue(themePark.OpeningDate == DateTime.MinValue, AssertMessage.ExpectTrue);
            Assert.IsTrue(themePark.ClosingDate == DateTime.MinValue, AssertMessage.ExpectTrue);
        }
    }
}