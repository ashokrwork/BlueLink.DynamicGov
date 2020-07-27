CREATE TABLE [dbo].[Template] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]    VARCHAR (8000)   NOT NULL,
    [CreationDate] DATETIME2 (7)    NOT NULL,
    [LastModified] DATETIME2 (7)    NOT NULL,
    [IsDeleted]    BIT              NULL,
    [Language]     INT              NULL,
    [EntityId]     UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (4000)  NOT NULL,
    [File]         VARBINARY (MAX)  NULL,
    [Status]       INT              NOT NULL,
    [FileName]     NVARCHAR (4000)  NULL,
    CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

