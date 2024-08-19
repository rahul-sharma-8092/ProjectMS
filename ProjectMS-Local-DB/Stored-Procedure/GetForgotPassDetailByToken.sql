-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <19-08-2024>
-- Description:	<GetForgotPassDetailByToken>
-- EXEC GetForgotPassDetailByToken 'deead69f-2e6e-4af6-8368-f863c44781ca'
-- =============================================
CREATE PROCEDURE [dbo].[GetForgotPassDetailByToken]
	@Token NVARCHAR(500)
AS
BEGIN
	SELECT Token, 
	(SELECT CONCAT(ISNULL(FirstName, ''), ' ', ISNULL(MiddleName, ''), ' ', ISNULL(LastName, '')) FROM Users U WHERE U.Email = FP.Email AND IsDeleted = 0) AS FullName 
	,FP.Email AS [Email]
	FROM ForgotPassword FP 
	WHERE Token = @Token AND IsDeleted = 0
	AND DATEDIFF(MINUTE, CreatedAT, GETUTCDATE()) < 300 --For 5hrs (60 * 5)
END
GO
