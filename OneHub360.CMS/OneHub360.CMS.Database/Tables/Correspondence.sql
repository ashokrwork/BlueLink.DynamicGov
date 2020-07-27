CREATE TABLE [dbo].[Correspondence] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]          VARCHAR (8000)   NOT NULL,
    [CreationDate]       DATETIME2 (7)    NOT NULL,
    [LastModified]       DATETIME2 (7)    NOT NULL,
    [IsDeleted]          BIT              NULL,
    [Language]           INT              NULL,
    [EntityId]           UNIQUEIDENTIFIER NOT NULL,
    [Subject]            NVARCHAR (4000)  NOT NULL,
    [From]               VARCHAR (8000)   NOT NULL,
    [To]                 VARCHAR (8000)   NOT NULL,
    [Confidential]       BIT              NULL,
    [SharedWith]         NVARCHAR (MAX)   NULL,
    [FK_Parent]          UNIQUEIDENTIFIER NULL,
    [ThreadId]           UNIQUEIDENTIFIER NULL,
    [FK_Document]        UNIQUEIDENTIFIER NULL,
    [CopyTo]             VARCHAR (8000)   NULL,
    [CorrespondenceType] INT              NULL,
    [Brief]              NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Correspondence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Correspondence_Correspondence] FOREIGN KEY ([FK_Parent]) REFERENCES [dbo].[Correspondence] ([Id]),
    CONSTRAINT [FK_Correspondence_IncomingMemo] FOREIGN KEY ([Id]) REFERENCES [dbo].[IncomingMemo] ([Id]),
    CONSTRAINT [FK_Correspondence_OutgoingMemo] FOREIGN KEY ([Id]) REFERENCES [dbo].[OutgoingMemo] ([Id])
);


GO
ALTER TABLE [dbo].[Correspondence] NOCHECK CONSTRAINT [FK_Correspondence_IncomingMemo];


GO
ALTER TABLE [dbo].[Correspondence] NOCHECK CONSTRAINT [FK_Correspondence_OutgoingMemo];

