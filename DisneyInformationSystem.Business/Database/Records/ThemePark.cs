using System;

namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Record for the ThemeParks database table.
    /// </summary>
    /// <param name="PIN">Park identification.</param>
    /// <param name="ResortID">Resort identification (FK).</param>
    /// <param name="ParkName">Park name.</param>
    /// <param name="AddressOfPark">Address of theme park.</param>
    /// <param name="Phone">Phone of theme park.</param>
    /// <param name="Transportation">Transportation to the park.</param>
    /// <param name="NumberOfLands">Number of lands.</param>
    /// <param name="NumberOfAttractions">Number of attractions.</param>
    /// <param name="NumberOfShops">Number of shops.</param>
    /// <param name="NumberOfRestaurants">Number of restaurants.</param>
    /// <param name="NumberOfTours">Number of tours.</param>
    /// <param name="NumberOfRestrooms">Number of restrooms.</param>
    /// <param name="Operating">Operating boolean.</param>
    /// <param name="OpeningDate">Opening date.</param>
    /// <param name="ClosingDate">Closing date.</param>
    public record ThemePark(
        string PIN,
        string ResortID,
        string ParkName,
        string AddressOfPark,
        string Phone,
        string Transportation,
        int NumberOfLands,
        int NumberOfAttractions,
        int NumberOfShops,
        int NumberOfRestaurants,
        int NumberOfTours,
        int NumberOfRestrooms,
        bool Operating,
        DateTime OpeningDate,
        DateTime ClosingDate) : GenericRecord(PIN)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ThemePark() : this(default, default, default, default, default, default, default, default, default, default, default, default, default, default, default) { }
    }
}