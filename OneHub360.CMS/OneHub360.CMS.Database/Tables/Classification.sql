CREATE TABLE [dbo].[Classification] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]    VARCHAR (8000)   NOT NULL,
    [CreationDate] DATETIME2 (7)    NOT NULL,
    [LastModified] DATETIME2 (7)    NOT NULL,
    [IsDeleted]    BIT              NULL,
    [Language]     INT              NULL,
    [EntityId]     UNIQUEIDENTIFIER NOT NULL,
    [Title]        VARCHAR (8000)   NOT NULL,
    [FK_Parent]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Classifications] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Classifications_Classifications_0] FOREIGN KEY ([FK_Parent]) REFERENCES [dbo].[Classification] ([Id])
);

