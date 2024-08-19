CREATE TABLE [dbo].[GroupPermission]
(
	GroupPermissionId INT NOT NULL PRIMARY KEY IDENTITY,
	GroupId INT,
	ModuleId INT,
	Permission INT DEFAULT 0 CHECK (Permission IN (0, 1, 2)),
	[Description] AS CASE 
        WHEN Permission = 0 THEN 'Deny'
        WHEN Permission = 1 THEN 'Read-Write'
        WHEN Permission = 2 THEN 'Read-only'
    END PERSISTED,
	CreatedAT DATETIME DEFAULT GETUTCDATE(),
    UpdatedAT DATETIME,
	IsDeleted BIT DEFAULT 0,
	CONSTRAINT FK_Groups FOREIGN KEY (GroupId) REFERENCES Groups(GroupId),
	CONSTRAINT FK_Modules FOREIGN KEY (ModuleId) REFERENCES Modules(ModuleId),
)
