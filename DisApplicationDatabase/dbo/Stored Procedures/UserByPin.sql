-- =============================================
-- Author:		James McKinney Jr.
-- Create date: 2022-08-21
-- Description:	Gets user by pin.
-- =============================================
CREATE PROCEDURE UserByPin
	(@PIN NCHAR(10))
AS
BEGIN
	SELECT * FROM Users
	WHERE PIN = @PIN
END
