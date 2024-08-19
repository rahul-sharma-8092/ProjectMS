-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <2024-08-10>
-- Description:	<CheckEmailExists>
-- =============================================
CREATE PROCEDURE [dbo].[CheckEmailExists]
	@email NVARCHAR(100)
AS
BEGIN
	Select Email from Users Where Email = @email
END
GO
