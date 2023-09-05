CREATE PROCEDURE [dbo].[InsertNewUser] (@PIN NCHAR(11), @FirstName TEXT, @LastName TEXT, @PhoneNumber TEXT, @EmailAddress TEXT, @Password TEXT, @HomeAddress TEXT)
AS
BEGIN
INSERT INTO Users VALUES (@PIN, @FirstName, @LastName, @PhoneNumber, @EmailAddress, @Password, @HomeAddress)
END