-- =============================================
-- Author:		James McKinney Jr.
-- Create date: 2022-08-22
-- Description:	Gets resort by resort name.
-- =============================================
CREATE PROCEDURE [dbo].[ResortByName]
	(@Name VARCHAR(MAX))
AS
BEGIN
	SELECT * FROM Resorts
	WHERE CONVERT(VARCHAR(MAX), ResortName) LIKE '%' + @Name
END
