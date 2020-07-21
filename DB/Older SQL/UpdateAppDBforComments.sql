USE [App]
GO

/****** Object:  View [dbo].[CommentsView]    Script Date: 28-Apr-17 12:13:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CommentsView]
AS
SELECT        Id, FK_Feed, FK_Parent, FK_User, Body, Private, IsDeleted, CreationDate, CreatedBy, LastModified, EntityId, Language, ThreadId
FROM            dbo.Comment

GO


