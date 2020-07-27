CREATE TABLE [dbo].[UserAction] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]         VARCHAR (8000)   NOT NULL,
    [CreationDate]      DATETIME2 (7)    NOT NULL,
    [LastModified]      DATETIME2 (7)    NOT NULL,
    [IsDeleted]         BIT              CONSTRAINT [DF_UserAction_IsDeleted] DEFAULT ((0)) NULL,
    [Language]          INT              NULL,
    [EntityId]          UNIQUEIDENTIFIER NOT NULL,
    [Actor]             VARCHAR (8000)   NOT NULL,
    [BrowserType]       VARCHAR (8000)   NOT NULL,
    [MachineIP]         VARCHAR (8000)   NOT NULL,
    [MachineName]       VARCHAR (8000)   NOT NULL,
    [ServerName]        VARCHAR (8000)   NOT NULL,
    [RequestUrl]        VARCHAR (8000)   NOT NULL,
    [FK_Correspondence] UNIQUEIDENTIFIER NOT NULL,
    [FK_UserActionType] UNIQUEIDENTIFIER NOT NULL,
    [ThreadId]          UNIQUEIDENTIFIER NULL,
    [Destination]       NVARCHAR (MAX)   NOT NULL,
    [Notes]             NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_UserActions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserActions_Correspondence] FOREIGN KEY ([FK_Correspondence]) REFERENCES [dbo].[Correspondence] ([Id]),
    CONSTRAINT [FK_UserActions_UserActionTypes_0] FOREIGN KEY ([FK_UserActionType]) REFERENCES [dbo].[UserActionType] ([Id])
);

