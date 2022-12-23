using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Database.Records
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ResortHotelTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void ResortHotel_DefaultConstructor_WhenDefaultConstructorIsCalled_ShouldSetAllPropertiesToDefaultValues()
        {
            // Arrange

            // Act
            var resortHotel = new ResortHotel();

            // Assert
            Assert.IsNull(resortHotel.PIN, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.ResortID, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.ResortHotelName, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.ResortType, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.Area, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.Theme, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.Description, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.Address, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.PhoneNumber, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.CheckInTime, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.CheckOutTime, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.RoomTypes, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.Transportation, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.ParkingCost, AssertMessage.ExpectNullValue);
            Assert.IsNull(resortHotel.ValetCost, AssertMessage.ExpectNullValue);
            Assert.IsFalse(resortHotel.HasInRoomDining, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasBeach, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasPetServices, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasFishing, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasCampfire, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasShoppingDelivery, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasChildCenter, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.IsConventionResort, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.Operating, AssertMessage.ExpectFalse);
            Assert.That.ValueIsZero(resortHotel.NumberOfRooms);
            Assert.That.ValueIsZero(resortHotel.NumberOfBusStops);
            Assert.That.DateTimeIsMinimumValue(resortHotel.OpeningDate);
            Assert.That.DateTimeIsMinimumValue(resortHotel.ClosingDate);
        }

        [TestMethod, TestCategory("Business Test")]
        public void ResortHotel_Constructor_WhenCallingRecordWithValues_ShouldSetAllPropertiesToTheValuesPassedIn()
        {
            // Arrange
            var pin = "WLR";
            var resortId = "WDW";
            var resortHotelName = "Disney's Wilderness Lodge Resort";
            var resortType = ResortType.Deluxe.ToString();
            var area = "Magic Kingdom Theme Park";
            var theme = "Pacific Northwest";
            var description = "This is a description.";
            var address = "901 Timberline Dr., Lake Buena Vista, FL 32830";
            var phoneNumber = "407-939-1936";
            var numberOfRooms = 728;
            var checkInTime = "3:00 PM";
            var checkOutTime = "11:00 AM";
            var roomTypes = "RoomTypes.xml";
            var transportation = "Ferry boat, Car";
            var numberOfBusStops = 1;
            var parkingCost = 25.00m;
            var valetCost = 33.00m;
            var openingDate = new DateTime(1994, 05, 28);
            var closingDate = DateTime.MaxValue;

            // Act
            var resortHotel = new ResortHotel(
                pin,
                resortId,
                resortHotelName,
                resortType,
                area,
                theme,
                description,
                address,
                phoneNumber,
                numberOfRooms,
                checkInTime,
                checkOutTime,
                roomTypes,
                transportation,
                numberOfBusStops,
                parkingCost,
                valetCost,
                true,
                true,
                false,
                false,
                true,
                true,
                true,
                false,
                openingDate,
                closingDate,
                true);

            // Assert
            Assert.IsNotNull(resortHotel.PIN, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.ResortID, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.ResortHotelName, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.ResortType, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.Area, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.Theme, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.Description, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.Address, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.PhoneNumber, AssertMessage.ExpectNotNullValue);
            Assert.IsTrue(resortHotel.NumberOfRooms > 0, AssertMessage.ExpectTrue);
            Assert.IsNotNull(resortHotel.CheckInTime, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.CheckOutTime, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.RoomTypes, AssertMessage.ExpectNotNullValue);
            Assert.IsNotNull(resortHotel.Transportation, AssertMessage.ExpectNotNullValue);
            Assert.IsTrue(resortHotel.NumberOfBusStops > 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.ParkingCost > 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.ValetCost > 0, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.HasInRoomDining, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.HasBeach, AssertMessage.ExpectTrue);
            Assert.IsFalse(resortHotel.HasPetServices, AssertMessage.ExpectFalse);
            Assert.IsFalse(resortHotel.HasFishing, AssertMessage.ExpectFalse);
            Assert.IsTrue(resortHotel.HasCampfire, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.HasShoppingDelivery, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.HasChildCenter, AssertMessage.ExpectTrue);
            Assert.IsFalse(resortHotel.IsConventionResort, AssertMessage.ExpectFalse);
            Assert.IsTrue(resortHotel.OpeningDate != DateTime.MinValue, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.ClosingDate == DateTime.MaxValue, AssertMessage.ExpectTrue);
            Assert.IsTrue(resortHotel.Operating, AssertMessage.ExpectTrue);
        }
    }
}