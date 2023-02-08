using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Enums;
using DisneyInformationSystem.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Testing.Shared
{
    /// <summary>
    /// Used for the test projects to get database help.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DatabaseMockers
    {
        /// <summary>
        /// Creates a testable list of admins.
        /// </summary>
        /// <returns>List of Admins.</returns>
        public static List<Admin> MockSetupListOfAdmins()
        {
            return new List<Admin>
            {
                new Admin()
                {
                    PIN = "A1234567890",
                    AdminTypeCode = "TAD",
                    FirstName = "Test",
                    LastName = "Test",
                    EmailAddress = "test@test.com",
                    Password = SecurePasswordHasher.Hash("TestPassword"),
                    AssessmentScore = 100
                }
            };
        }

        /// <summary>
        /// Creates a testable list of users.
        /// </summary>
        /// <returns>List of Users.</returns>
        public static List<User> MockSetupListOfUsers()
        {
            return new List<User>
            {
                new User()
                {
                    PIN = "U1234567890",
                    FirstName = "Test",
                    LastName = "Test",
                    PhoneNumber = "1234567890",
                    EmailAddress = "test@test.com",
                    Password = SecurePasswordHasher.Hash("TestPassword"),
                    HomeAddress = "123 Test Street, TestCity, TS 12345"
                }
            };
        }

        /// <summary>
        /// Creates a testable list of resorts.
        /// </summary>
        /// <returns>List of Resorts.</returns>
        public static List<Resort> MockSetupListOfResorts()
        {
            return new List<Resort>
            {
                new Resort("WDW", "Walt Disney World Resort", "123 World Drive, Orlando, FL", "123-456-7890", 4, 30, 50, 2, 2, true, new DateTime(1971, 10, 1), DateTime.MaxValue)
            };
        }

        /// <summary>
        /// Creates a testable list of theme parks.
        /// </summary>
        /// <returns>List of theme parks.</returns>
        public static List<ThemePark> MockSetupListOfThemeParks()
        {
            return new List<ThemePark>
            {
                new ThemePark()
                {
                    PIN = "MKP",
                    ResortID = "WDW",
                    ParkName = "Magic Kingdom Park",
                    AddressOfPark = "123 Magic Way, Orlando, FL 12345",
                    Phone = "123-456-7890",
                    Transportation = "Monorail, Bus, Car, Walk",
                    NumberOfLands = 6,
                    NumberOfAttractions = 30,
                    NumberOfShops = 20,
                    NumberOfRestaurants = 15,
                    NumberOfTours = 7,
                    NumberOfRestrooms = 14,
                    Operating = true,
                    OpeningDate = new DateTime(1971, 10, 01),
                    ClosingDate = DateTime.MaxValue
                }
            };
        }

        public static List<ResortHotel> MockSetupListOfResortHotels()
        {
            return new List<ResortHotel>
            {
                new ResortHotel
                {
                    PIN = "WLR",
                    ResortID = "WDW",
                    ResortHotelName = "Disney's Wilderness Lodge Resort",
                    ResortType = ResortType.Deluxe.ToString(),
                    Area = "Magic Kingdom Park Resort Area",
                    Theme = "Pacific Northwest",
                    Description = "This is a description",
                    Address = "456 Wilderness Drive, Orlando, FL 67890",
                    PhoneNumber = "098-765-4321",
                    NumberOfRooms = 700,
                    CheckInTime = "3:00 PM",
                    CheckOutTime = "11:00 AM",
                    RoomTypes = "Room Types",
                    Transportation = "Car, Boat",
                    NumberOfBusStops = 1,
                    ParkingCost = 0.0m,
                    ValetCost = 20.00m,
                    HasInRoomDining = true,
                    HasBeach = true,
                    HasPetServices = false,
                    HasFishing = false,
                    HasCampfire = true,
                    HasShoppingDelivery = true,
                    HasChildCenter = false,
                    IsConventionResort = false,
                    OpeningDate = new DateTime(1994, 05, 28),
                    ClosingDate = DateTime.MaxValue,
                    Operating = true
                }
            };
        }
    }
}