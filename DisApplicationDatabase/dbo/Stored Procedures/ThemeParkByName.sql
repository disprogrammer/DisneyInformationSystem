-- =============================================
-- Author:		James McKinney
-- Create date: 2023-01-15
-- Description:	Retrieves a theme park by name.
-- =============================================
CREATE PROCEDURE ThemeParkByName
	(@Name VARCHAR(MAX))
AS
BEGIN
	SELECT * FROM ThemeParks
	WHERE CONVERT(VARCHAR(MAX), ParkName) LIKE '%' + @Name
END
