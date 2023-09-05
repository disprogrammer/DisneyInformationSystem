CREATE PROCEDURE UpdateAdmin (@PIN NCHAR(10), @AdminTypeCode NCHAR(3), @FirstName TEXT, @LastName TEXT, @EmailAddress TEXT, @Password TEXT, @AssessmentScore INT)
AS
BEGIN
	UPDATE Admins SET AdminTypeCode = @AdminTypeCode, FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, Password = @Password, AssessmentScore = @AssessmentScore
	WHERE PIN = @PIN
END
