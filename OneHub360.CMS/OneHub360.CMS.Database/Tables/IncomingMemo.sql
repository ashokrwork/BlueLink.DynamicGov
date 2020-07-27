CREATE TABLE [dbo].[IncomingMemo] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [IncomingNumber] VARCHAR (8000)   NOT NULL,
    [IncomingDate]   DATETIME2 (7)    NOT NULL,
    [ScanningDate]   DATETIME2 (7)    NULL,
    [ScannedBy]      VARCHAR (8000)   NULL,
    [IndexingDate]   DATETIME2 (7)    NULL,
    [IndexedBy]      VARCHAR (8000)   NULL,
    [FK_Draft]       UNIQUEIDENTIFIER NULL,
    [FK_Outgoing]    UNIQUEIDENTIFIER NULL,
    [Status]         INT              CONSTRAINT [DF_IncomingMemo_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_IncomingMemos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncomingMemo_Correspondence] FOREIGN KEY ([FK_Draft]) REFERENCES [dbo].[Correspondence] ([Id]),
    CONSTRAINT [FK_IncomingMemo_Correspondence1] FOREIGN KEY ([FK_Outgoing]) REFERENCES [dbo].[Correspondence] ([Id])
);


GO
ALTER TABLE [dbo].[IncomingMemo] NOCHECK CONSTRAINT [FK_IncomingMemo_Correspondence];


GO
ALTER TABLE [dbo].[IncomingMemo] NOCHECK CONSTRAINT [FK_IncomingMemo_Correspondence1];

