-- ========================================================
-- Author:		<Rahul Sharma>
-- Create date: <17-08-2024>
-- Description:	<Add Update User-Groups & their Permission>
-- ========================================================
CREATE PROCEDURE [dbo].[AddUpdateUserGroups]
    @GroupId INT = 0,
    @GroupName NVARCHAR(100),
    @GroupDescription NVARCHAR(255),
    @ModulePermission ModulePermissions READONLY
AS
BEGIN
    DECLARE @retVal INT = 0, @Message NVARCHAR(255) = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Add User Groups & Permission
        IF (@GroupId = 0)
        BEGIN
            INSERT INTO Groups(GroupName, [Description]) 
            VALUES(@GroupName, @GroupDescription);

            DECLARE @LastGroupID INT = SCOPE_IDENTITY();
            
            INSERT INTO GroupPermission(GroupId, ModuleId, Permission)
            SELECT @LastGroupID, ModuleId, Permission
            FROM @ModulePermission;
        END
        ELSE IF(@GroupId > 0)
        BEGIN
            -- Update User Groups & Permission
            UPDATE Groups
            SET GroupName = @GroupName, [Description] = @GroupDescription
            WHERE GroupId = @GroupId;

			DECLARE @Old_CreateAT DATETIME = GETUTCDATE();
			Select TOP 1 @Old_CreateAT = CreatedAT from GroupPermission Where GroupId = @GroupId;

            -- Delete old permissions
            DELETE FROM GroupPermission WHERE GroupId = @GroupId;

            -- Insert new permissions
            INSERT INTO GroupPermission(GroupId, ModuleId, Permission, CreatedAT, UpdatedAT)
            SELECT @GroupId, ModuleId, Permission, @Old_CreateAT, GETUTCDATE()
            FROM @ModulePermission;
        END

        COMMIT TRANSACTION;

        SET @retVal = 1; 

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

		set @Message = ERROR_MESSAGE();

    END CATCH;

    SELECT @retVal AS retVal, @Message AS [Message]
END
GO