-- =============================================
-- Author:		James McKinney
-- Create date: 2023-02-07
-- Description:	Gets the theme parks from a resort by the id.
-- =============================================
CREATE PROCEDURE ThemeParksByResortID
	(@PIN NCHAR(3))
AS
BEGIN
	SELECT * FROM ThemeParks
	WHERE ThemeParks.ResortID = @PIN
END
