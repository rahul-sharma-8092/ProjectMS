-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <18-08-2024>
-- Description:	<Save Forgot Password Token>
-- =============================================
CREATE PROCEDURE [dbo].[SaveForgotPassToken]
	@Token NVARCHAR(MAX),
	@Email NVARCHAR(100),
	@IPAddress NVARCHAR(20)
AS
BEGIN
	IF EXISTS(Select * from ForgotPassword WHERE Email = @Email AND IsDeleted = 0)
	BEGIN
		UPDATE ForgotPassword Set IsDeleted = 1 WHERE Email = @Email	
	END

	INSERT INTO ForgotPassword(Token, Email, IPAddress, CreatedAT)
	VALUES(@Token, @Email, @IPAddress, GETUTCDATE())

	SELECT Token, 
	(SELECT CONCAT(ISNULL(FirstName, ''), ' ', ISNULL(MiddleName, ''), ' ', ISNULL(LastName, '')) FROM Users WHERE Email = @Email AND IsDeleted = 0) AS FullName 
	FROM ForgotPassword WHERE Id = SCOPE_IDENTITY() AND Email = @Email;
END
GO