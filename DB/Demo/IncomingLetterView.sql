USE [Correspondence]
GO

/****** Object:  View [dbo].[IncomingLetterView]    Script Date: 01-May-17 11:01:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[IncomingLetterView]
AS
SELECT        dbo.Correspondence.Id, dbo.Correspondence.CreatedBy, dbo.Correspondence.CreationDate, dbo.Correspondence.LastModified, dbo.Correspondence.IsDeleted, dbo.Correspondence.Language, 
                         dbo.Correspondence.EntityId, dbo.Correspondence.Subject, dbo.Correspondence.[From], dbo.Correspondence.[To], dbo.Correspondence.Confidential, dbo.Correspondence.SharedWith, 
                         dbo.Correspondence.FK_Parent, dbo.Correspondence.ThreadId, dbo.Correspondence.FK_Document, dbo.Correspondence.CopyTo, dbo.Correspondence.Brief, dbo.IncomingLetter.IncomingNumber, 
                         dbo.IncomingLetter.IncomingDate, dbo.IncomingLetter.OutgoingNumber, dbo.IncomingLetter.OutgoingDate, dbo.IncomingLetter.RegisteringDate, dbo.IncomingLetter.RegisteredBy, 
                         dbo.IncomingLetter.IndexingDate, dbo.IncomingLetter.IndexedBy, dbo.IncomingLetter.ScanningDate, dbo.IncomingLetter.ScannedBy, dbo.IncomingLetter.SendingDate, dbo.IncomingLetter.SentBy, 
                         dbo.IncomingLetter.G2GNumber, dbo.IncomingLetter.G2GDate, dbo.Correspondence.Status
FROM            dbo.Correspondence INNER JOIN
                         dbo.IncomingLetter ON dbo.Correspondence.Id = dbo.IncomingLetter.Id

GO


