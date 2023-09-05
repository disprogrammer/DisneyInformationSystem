-- =============================================
-- Author:		James McKinney
-- Create date: March 10, 2022
-- Description:	Updates a resort based on inputted values.
-- =============================================
CREATE PROCEDURE [dbo].[UpdateResort]
	(@PIN NCHAR(3), @ResortName TEXT, @AddressOfResort TEXT, @Phone TEXT, @NumberOfThemeParks INT, @NumberOfResortHotels INT, @NumberOfPartnerHotels INT, @NumberOfWaterParks INT, @NumberOfEntertainmentVenues INT, @Operating BIT, @OpeningDate DATE, @ClosingDate DATE)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Resorts 
	SET 
	ResortName = @ResortName,
	AddressOfResort = @AddressOfResort,
	Phone = @Phone,
	NumberOfThemeParks = @NumberOfThemeParks,
	NumberOfResortHotels = @NumberOfResortHotels,
	NumberOfPartnerHotels = @NumberOfPartnerHotels,
	NumberOfWaterParks = @NumberOfWaterParks,
	NumberOfEntertainmentVenues = @NumberOfEntertainmentVenues,
	Operating = @Operating,
	OpeningDate = @OpeningDate,
	ClosingDate = @ClosingDate
	WHERE
	PIN = @PIN
END
