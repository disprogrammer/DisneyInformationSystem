-- =============================================
-- Author:		James McKinney
-- Create date: 08-01-2022
-- Description:	Insert new theme park into the ThemeParks table.
-- =============================================
CREATE PROCEDURE [dbo].[InsertNewThemePark]
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
	INSERT INTO ThemeParks VALUES 
	(@PIN,
	@ResortID,
	@ParkName,
	@AddressOfPark,
	@Phone,
	@Transportation,
	@NumberOfLands,
	@NumberOfAttractions,
	@NumberOfShops,
	@NumberOfRestaurants,
	@NumberOfTours,
	@NumberOfRestrooms,
	@Operating,
	@OpeningDate,
	@ClosingDate)
END
