CREATE TABLE [dbo].[Document] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]    VARCHAR (8000)   NOT NULL,
    [CreationDate] DATETIME2 (7)    NOT NULL,
    [LastModified] DATETIME2 (7)    NOT NULL,
    [IsDeleted]    BIT              NULL,
    [Language]     INT              NULL,
    [EntityId]     UNIQUEIDENTIFIER NOT NULL,
    [FileName]     NVARCHAR (4000)  NOT NULL,
    [Title]        NVARCHAR (4000)  NOT NULL,
    [FileBinary]   VARBINARY (MAX)  NOT NULL,
    [Signed]       BIT              NOT NULL,
    [SignedBy]     NVARCHAR (4000)  NULL,
    [SigningDate]  DATETIME2 (7)    NULL,
    [FK_Template]  UNIQUEIDENTIFIER NULL,
    [PagesCount]   BIGINT           CONSTRAINT [DF_Documents_PagesCount] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Documents_Templates] FOREIGN KEY ([FK_Template]) REFERENCES [dbo].[Template] ([Id])
);

