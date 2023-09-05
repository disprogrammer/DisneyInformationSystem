-- =============================================
-- Author:		James McKinney Jr.
-- Create date: 2022-08-21
-- Description:	Retrieves user by email address.
-- =============================================
CREATE PROCEDURE UserByEmail
	(@EmailAddress VARCHAR(MAX))
AS
BEGIN
	SELECT * FROM Users
	WHERE CONVERT(VARCHAR(MAX), EmailAddress) = @EmailAddress
END
