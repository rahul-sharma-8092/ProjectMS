-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <12-08-2024>
-- Description:	<Get All user groups.>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUserGroups]
AS
BEGIN
	SELECT GroupId, GroupName, [Description], IsDeleted, 
	(SELECT COUNT(GroupId) FROM Users WHERE GroupId = G.GroupId) AS NoOfUserAssociated
	FROM Groups G WHERE IsDeleted = 0
END
GO
