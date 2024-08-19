﻿CREATE TABLE [dbo].[Users]
(
	UserId BIGINT IDENTITY PRIMARY KEY,
	NamePrefix NVARCHAR(20),
	FirstName NVARCHAR(100),
	MiddleName NVARCHAR(100),
	LastName NVARCHAR(100),
	Email NVARCHAR(100) NOT NULL UNIQUE, 
	[Password] NVARCHAR(MAX),
	Phone NVARCHAR(13),
	[Address] NVARCHAR(500),
	PinCode NVARCHAR(10),
	Country NVARCHAR(10),
	Religion NVARCHAR(50),
	DOB DATE,
	Salary DECIMAL(12,2),
	JobRole NVARCHAR(100),
	GroupId INT,
	TimeZone NVARCHAR(100),
	CreatedAT DATETIME DEFAULT GETUTCDATE(),
	ModifiedAT DATETIME,
	IsDeleted BIT DEFAULT 0,
	CONSTRAINT FK_GroupPermission FOREIGN KEY (GroupId) REFERENCES Groups(GroupId),
)