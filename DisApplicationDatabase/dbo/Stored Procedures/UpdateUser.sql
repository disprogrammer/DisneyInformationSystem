CREATE PROCEDURE UpdateUser (@PIN NCHAR(11), @FirstName TEXT, @LastName TEXT, @PhoneNumber TEXT, @EmailAddress TEXT, @Password TEXT, @HomeAddress TEXT)
AS
BEGIN
	UPDATE Users SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, Password = @Password, HomeAddress = @HomeAddress
	WHERE PIN = @PIN
END
