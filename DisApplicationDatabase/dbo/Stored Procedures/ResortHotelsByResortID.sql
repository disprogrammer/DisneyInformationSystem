-- =============================================
-- Author:		James McKinney
-- Create date: 2023-02-07
-- Description:	Gets the resort hotels from a resort by the id.
-- =============================================
CREATE PROCEDURE ResortHotelsByResortID
	(@PIN NCHAR(3))
AS
BEGIN
	SELECT * FROM ResortHotels
	WHERE ResortHotels.ResortID = @PIN
END
