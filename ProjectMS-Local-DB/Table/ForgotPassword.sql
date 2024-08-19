CREATE TABLE [dbo].[ForgotPassword]
(
	Id BIGINT Identity(1,1) PRIMARY KEY,
	Token NVARCHAR(MAX),
	Email NVARCHAR(100),
	IPAddress NVARCHAR(20),
	CreatedAT DATETIME DEFAULT GETUTCDATE(),
	IsDeleted BIT DEFAULT 0
)
GO

-- =============================================
-- Author:		<Rahul Sharma>
-- Create date: <18-08-2024>
-- Description:	<Delete Expired Token from Table>
-- =============================================
CREATE TRIGGER [dbo].[Tracking_ForgotPassword]
   ON  [dbo].[ForgotPassword]
   AFTER INSERT
AS 
BEGIN
	DELETE FROM ForgotPassword WHERE DATEDIFF(MINUTE, CreatedAT, GETUTCDATE()) > 1440
END
GO