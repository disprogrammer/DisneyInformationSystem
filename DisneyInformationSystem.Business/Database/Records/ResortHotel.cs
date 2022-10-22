using System;

namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Resort Hotel record.
    /// </summary>
    /// <param name="PIN">PIN.</param>
    /// <param name="ResortID">Resort Id.</param>
    /// <param name="ResortHotelName">Resort hotel name.</param>
    /// <param name="ResortType">Resort type.</param>
    /// <param name="Area">Area.</param>
    /// <param name="Theme">Theme.</param>
    /// <param name="Description">Description.</param>
    /// <param name="Address">Address.</param>
    /// <param name="PhoneNumber">Phone number.</param>
    /// <param name="NumberOfRooms">Number of rooms.</param>
    /// <param name="CheckInTime">Check in time.</param>
    /// <param name="CheckOutTime">Check out time.</param>
    /// <param name="RoomTypes">Room types.</param>
    /// <param name="Transportation">Transportation.</param>
    /// <param name="NumberOfBusStops">Number of bus stops.</param>
    /// <param name="ParkingCost">Parking costs.</param>
    /// <param name="ValetCost">Valet cost.</param>
    /// <param name="HasInRoomDining">Has in room dining.</param>
    /// <param name="HasBeach">Has beach.</param>
    /// <param name="HasPetServices">Has pet services.</param>
    /// <param name="HasFishing">Has fishing.</param>
    /// <param name="HasCampfire">Has campfire.</param>
    /// <param name="HasShoppingDelivery">Has shopping delivery.</param>
    /// <param name="HasChildCenter">Has child center.</param>
    /// <param name="IsConventionResort">Is convention resort.</param>
    /// <param name="OpeningDate">Opening date.</param>
    /// <param name="ClosingDate">Closing date.</param>
    /// <param name="Operating">Operating.</param>
    public record ResortHotel(
        string PIN,
        string ResortID,
        string ResortHotelName,
        string ResortType,
        string Area,
        string Theme,
        string Description,
        string Address,
        string PhoneNumber,
        int NumberOfRooms,
        DateTime CheckInTime,
        DateTime CheckOutTime,
        string RoomTypes,
        string Transportation,
        int NumberOfBusStops,
        string ParkingCost,
        string ValetCost,
        bool HasInRoomDining,
        bool HasBeach,
        bool HasPetServices,
        bool HasFishing,
        bool HasCampfire,
        bool HasShoppingDelivery,
        bool HasChildCenter,
        bool IsConventionResort,
        DateTime OpeningDate,
        DateTime ClosingDate,
        bool Operating) : GenericRecord(PIN)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResortHotel() : this(default, default, default, default, default, default, default,
                                    default, default, default, default, default, default, default,
                                    default, default, default, default, default, default, default,
                                    default, default, default, default, default, default, default) { }
    }
}