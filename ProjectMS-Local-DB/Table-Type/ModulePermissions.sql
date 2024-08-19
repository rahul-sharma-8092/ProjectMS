CREATE TYPE [dbo].[ModulePermissions] AS TABLE
(
	GroupPermissionId INT PRIMARY KEY,
	GroupId INT,
	ModuleId INT,
	ModuleName NVARCHAR(100),
	Permission INT,
	[Description] NVARCHAR(255),
	IsDeleted BIT
)
GO