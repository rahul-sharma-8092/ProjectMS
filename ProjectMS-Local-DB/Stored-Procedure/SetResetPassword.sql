-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <19-08-2024>
-- Description:	<Change User Password Form Forgot passsword>
-- EXEC SetResetPassword '', ''
-- =============================================
CREATE PROCEDURE [dbo].[SetResetPassword]
	@Email NVARCHAR(100),
	@Password NVARCHAR(MAX)
AS
BEGIN
	UPDATE Users SET [Password] = @Password, ModifiedAT = GETUTCDATE() WHERE Email = @Email AND IsDeleted = 0

	UPDATE ForgotPassword SET IsDeleted = 1 WHERE Email = @Email AND IsDeleted = 0

	Select Email, CONCAT(ISNULL(FirstName,''), ' ', ISNULL(MiddleName,''), ' ', ISNULL(LastName,'')) AS [FullName]
	FROM Users WHERE Email = @Email AND IsDeleted = 0
END
GO