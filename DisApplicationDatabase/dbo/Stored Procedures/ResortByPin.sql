-- =============================================
-- Author:		James McKinney Jr.
-- Create date: 2022-08-22
-- Description:	Gets a resort by pin.
-- =============================================
CREATE PROCEDURE ResortByPin
	(@Pin NCHAR(3))
AS
BEGIN
	SELECT * FROM Resorts
	WHERE PIN = @Pin
END
