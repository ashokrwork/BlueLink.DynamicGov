CREATE TABLE [dbo].[UserActionType] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]      VARCHAR (8000)   NOT NULL,
    [CreationDate]   DATETIME2 (7)    NOT NULL,
    [LastModified]   DATETIME2 (7)    NOT NULL,
    [IsDeleted]      BIT              NULL,
    [Language]       INT              NULL,
    [EntityId]       UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (4000)  NOT NULL,
    [Message]        NVARCHAR (4000)  NOT NULL,
    [ActionCssClass] NVARCHAR (200)   NOT NULL,
    CONSTRAINT [PK_UserActionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

