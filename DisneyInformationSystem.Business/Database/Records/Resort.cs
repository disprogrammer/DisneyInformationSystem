using System;

namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Record for the Resorts database table.
    /// </summary>
    /// <param name="PIN">Resort Identification.</param>
    /// <param name="ResortName">Resort Name.</param>
    /// <param name="AddressOfResort">Address of Resort.</param>
    /// <param name="Phone">Phone number.</param>
    /// <param name="NumberOfThemeParks">Number of theme parks.</param>
    /// <param name="NumberOfResortHotels">Number of resort hotels.</param>
    /// <param name="NumberOfPartnerHotels">Number of partner hotels.</param>
    /// <param name="NumberOfWaterParks">Number of water parks.</param>
    /// <param name="NumberOfEntertainmentVenues">Number of entertainment venues.</param>
    /// <param name="Operating">Operating.</param>
    /// <param name="OpeningDate">Opening date.</param>
    /// <param name="ClosingDate">Closing date.</param>
    public record Resort(
        string PIN,
        string ResortName,
        string AddressOfResort,
        string Phone,
        int NumberOfThemeParks,
        int NumberOfResortHotels,
        int NumberOfPartnerHotels,
        int NumberOfWaterParks,
        int NumberOfEntertainmentVenues,
        bool Operating,
        DateTime OpeningDate,
        DateTime ClosingDate) : GenericRecord(PIN)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Resort() : this(default, default, default, default, default, default, default, default, default, default, default, default) { }
    }
}
