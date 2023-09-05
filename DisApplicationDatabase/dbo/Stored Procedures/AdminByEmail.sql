-- =============================================
-- Author:		James McKinney Jr.
-- Create date: 2022-08-21
-- Description:	Gets an admin by email address.
-- =============================================
CREATE PROCEDURE [dbo].[AdminByEmail]
	(@EmailAddress VARCHAR(MAX))
AS
BEGIN
	SELECT * FROM Admins
	WHERE CONVERT(VARCHAR(MAX), EmailAddress) = @EmailAddress
END
