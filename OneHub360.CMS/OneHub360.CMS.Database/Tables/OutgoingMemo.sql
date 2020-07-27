CREATE TABLE [dbo].[OutgoingMemo] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [OutgoingNumber] VARCHAR (8000)   NULL,
    [OutgoingDate]   DATETIME2 (7)    NULL,
    [FK_Draft]       UNIQUEIDENTIFIER NOT NULL,
    [FK_Incoming]    UNIQUEIDENTIFIER NULL,
    [Status]         INT              CONSTRAINT [DF_OutgoingMemo_Status] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_OutgoingMemos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OutgoingMemo_Correspondence] FOREIGN KEY ([FK_Draft]) REFERENCES [dbo].[Correspondence] ([Id]),
    CONSTRAINT [FK_OutgoingMemo_Correspondence1] FOREIGN KEY ([FK_Draft]) REFERENCES [dbo].[Correspondence] ([Id])
);


GO
ALTER TABLE [dbo].[OutgoingMemo] NOCHECK CONSTRAINT [FK_OutgoingMemo_Correspondence];


GO
ALTER TABLE [dbo].[OutgoingMemo] NOCHECK CONSTRAINT [FK_OutgoingMemo_Correspondence1];

