-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <17-08-2024>
-- Description:	<Description,,>
-- EXEC GetUserGroupsById 1
-- =============================================
CREATE PROCEDURE [dbo].[GetUserGroupsById]
	@GroupId INT = 0
AS
BEGIN
	IF(@GroupId = 0)
	BEGIN 
		Select DISTINCT ModuleName, M.ModuleId, ISNULL(M.[Description],'') AS [Description], 0 AS GroupPermissionId, 0 AS Permission 
		,0 as GroupId, '' AS GroupName, '' AS [Description], 0 AS IsDeleted, 0 AS [NoOfUserAssociated]
		from Modules M
		LEFT JOIN GroupPermission GP ON GP.ModuleId = M.ModuleId
		ORDER BY M.ModuleName
	END
	ELSE IF(@GroupId > 0)
	BEGIN
		
		SELECT G.GroupId, G.GroupName, G.[Description], G.IsDeleted, (SELECT COUNT(1) FROM Users U WHERE U.GroupId = @GroupId) AS [NoOfUserAssociated],
		GP.GroupPermissionId, GP.ModuleId, M.ModuleName, GP.Permission, ISNULL(M.[Description],'') AS [Description]
		FROM GroupPermission GP
		LEFT JOIN Modules M ON GP.ModuleId = M.ModuleId
		LEFT JOIN Groups G ON GP.GroupId = G.GroupId 
		Where GP.GroupId = @GroupId AND GP.IsDeleted = 0
		ORDER BY M.ModuleName
	END
END
GO
