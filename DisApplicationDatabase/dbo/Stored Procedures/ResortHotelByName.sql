-- =============================================
-- Author:		James McKinney
-- Create date: 2023-01-15
-- Description:	Retrieves a resort hotel by name
-- =============================================
CREATE PROCEDURE ResortHotelByName
	(@Name VARCHAR(MAX))
AS
BEGIN
	SELECT * FROM ResortHotels
	WHERE ResortHotelName LIKE '%' + @Name
END
