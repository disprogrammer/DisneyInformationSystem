CREATE PROCEDURE [dbo].[InsertNewAdmin] (@PIN NCHAR(10), @AdminTypeCode NCHAR(3), @FirstName TEXT, @LastName TEXT, @EmailAddress TEXT, @Password TEXT, @AssessmentScore INT)
AS
BEGIN
INSERT INTO Admins VALUES (@PIN, @AdminTypeCode, @FirstName, @LastName, @EmailAddress, @Password, @AssessmentScore)
END