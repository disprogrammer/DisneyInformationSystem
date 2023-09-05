-- =============================================
-- Author:		James McKinney
-- Create date: March 10, 2022
-- Description:	Inserts a new resort into the Resorts table.
-- =============================================
CREATE PROCEDURE [dbo].[InsertNewResort]
	(@PIN NCHAR(3), @ResortName TEXT, @AddressOfResort TEXT, @Phone TEXT, @NumberOfThemeParks INT, @NumberOfResortHotels INT, @NumberOfPartnerHotels INT, @NumberOfWaterParks INT, @NumberOfEntertainmentVenues INT, @Operating BIT, @OpeningDate DATE, @ClosingDate DATE)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Resorts VALUES
	(@PIN, @ResortName, @AddressOfResort, @Phone, @NumberOfThemeParks, @NumberOfResortHotels, @NumberOfPartnerHotels, @NumberOfWaterParks, @NumberOfEntertainmentVenues, @Operating, @OpeningDate, @ClosingDate)
END
