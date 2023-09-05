-- =============================================
-- Author:		James McKinney
-- Create date: 08-07-2022
-- Description:	Updates a theme park in the database table.
-- =============================================
CREATE PROCEDURE [dbo].[UpdateThemePark]
	(@PIN NCHAR(3),
	@ResortID NCHAR(3),
	@ParkName TEXT,
	@AddressOfPark TEXT,
	@Phone TEXT,
	@Transportation TEXT,
	@NumberOfLands INT,
	@NumberOfAttractions INT,
	@NumberOfShops INT,
	@NumberOfRestaurants INT,
	@NumberOfTours INT,
	@NumberOfRestrooms INT,
	@Operating BIT,
	@OpeningDate DATE,
	@ClosingDate DATE)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ThemeParks
	SET
	ResortID = @ResortID,
	ParkName = @ParkName,
	AddressOfPark = @AddressOfPark,
	Phone = @Phone,
	Transportation = @Transportation,
	NumberOfLands = @NumberOfLands,
	NumberOfAttractions = @NumberOfAttractions,
	NumberOfShops = @NumberOfShops,
	NumberOfRestaurants = @NumberOfRestaurants,
	NumberOfTours = @NumberOfTours,
	NumberOfRestrooms = @NumberOfRestrooms,
	Operating = @Operating,
	OpeningDate = @OpeningDate,
	ClosingDate = @ClosingDate
	WHERE
	PIN = @PIN
END
