CREATE TABLE [dbo].[DraftLetter] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]      VARCHAR (8000)   NOT NULL,
    [CreationDate]   DATETIME2 (7)    NOT NULL,
    [LastModified]   DATETIME2 (7)    NOT NULL,
    [IsDeleted]      BIT              NULL,
    [Language]       INT              NULL,
    [EntityId]       UNIQUEIDENTIFIER NOT NULL,
    [Subject]        VARCHAR (8000)   NOT NULL,
    [From]           VARCHAR (8000)   NOT NULL,
    [To]             VARCHAR (8000)   NOT NULL,
    [IncomingNumber] VARCHAR (8000)   NULL,
    [IncomingDate]   DATETIME2 (7)    NULL,
    [OutgoingNumber] VARCHAR (8000)   NULL,
    [OutgoingDate]   DATETIME2 (7)    NULL,
    [Confidential]   BIT              NULL,
    [SharedWith]     VARCHAR (8000)   NULL,
    [FK_ReplyTo]     UNIQUEIDENTIFIER NOT NULL,
    [FK_Document]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DraftLetters] PRIMARY KEY CLUSTERED ([Id] ASC)
);

