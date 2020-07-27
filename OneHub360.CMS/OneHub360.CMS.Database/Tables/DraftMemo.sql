CREATE TABLE [dbo].[DraftMemo] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [IncomingNumber] VARCHAR (8000)   NULL,
    [IncomingDate]   DATETIME2 (7)    NULL,
    [OutgoingNumber] VARCHAR (8000)   NULL,
    [OutgoingDate]   DATETIME2 (7)    NULL,
    [Status]         INT              CONSTRAINT [DF_DraftMemo_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_DraftMemos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DraftMemos_Correspondence] FOREIGN KEY ([Id]) REFERENCES [dbo].[Correspondence] ([Id])
);


GO
ALTER TABLE [dbo].[DraftMemo] NOCHECK CONSTRAINT [FK_DraftMemos_Correspondence];

