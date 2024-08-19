-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <17-08-2024>
-- Description:	<For User Groups Deletion>
-- EXEC DeleteUserGroups 0
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUserGroups]
	@GroupId INT
AS
BEGIN
	DECLARE @message NVARCHAR(100) = ''

	IF NOT EXISTS(SELECT * FROM Users WHERE GroupId = @GroupId AND IsDeleted = 0)
	BEGIN 
		UPDATE Groups SET IsDeleted = 1, UpdatedAT = GETUTCDATE() WHERE GroupId = @GroupId AND IsDeleted = 0

		UPDATE GroupPermission SET IsDeleted = 1, UpdatedAT = GETUTCDATE() WHERE GroupId = @GroupId AND IsDeleted = 0

		SELECT @message = GroupName FROM Groups WHERE GroupId = @GroupId AND IsDeleted = 1
	END

	SELECT @message AS [Message]
END
GO