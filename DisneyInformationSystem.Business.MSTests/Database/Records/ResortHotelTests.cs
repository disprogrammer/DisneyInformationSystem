using DisneyInformationSystem.Business.Database.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.That.DateTimeIsMinimumValue(resortHotel.CheckInTime);
            Assert.That.DateTimeIsMinimumValue(resortHotel.CheckOutTime);
            Assert.That.DateTimeIsMinimumValue(resortHotel.OpeningDate);
            Assert.That.DateTimeIsMinimumValue(resortHotel.ClosingDate);
        }

        [TestMethod, TestCategory("Business Test")]
        public void ResortHotel_Constructor_WhenCallingRecordWithValues_ShouldSetAllPropertiesToTheValuesPassedIn()
        {
            // Arrange
            var pin = "WLR";
            var resortId = "WDW";
            var resortHotelname = "Disney's Wilderness Lodge Resort";

            // Act

            // Assert
        }
    }
}