-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <2024-08-10>
-- Description:	<Get User Detail by Email>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserDetailbyEmailID]
	@email NVARCHAR(100)
AS
BEGIN
	Select * from Users Where Email = @email
END
GO
