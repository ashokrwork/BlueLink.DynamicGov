CREATE TABLE [dbo].[CorrespondenceDocuments] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [FK_Correspondence] UNIQUEIDENTIFIER NOT NULL,
    [FK_Document]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]         VARCHAR (8000)   NOT NULL,
    [CreationDate]      DATETIME2 (7)    NOT NULL,
    [LastModified]      DATETIME2 (7)    NOT NULL,
    [IsDeleted]         BIT              NULL,
    [Language]          INT              NULL,
    [EntityId]          UNIQUEIDENTIFIER NOT NULL,
    [IsMainDocument]    BIT              CONSTRAINT [DF_CorrespondenceDocuments_IsMainDocument] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CorrespondenceDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CorrespondenceDocuments_Correspondence] FOREIGN KEY ([FK_Correspondence]) REFERENCES [dbo].[Correspondence] ([Id]),
    CONSTRAINT [FK_CorrespondenceDocuments_Document] FOREIGN KEY ([FK_Document]) REFERENCES [dbo].[Document] ([Id])
);


GO
ALTER TABLE [dbo].[CorrespondenceDocuments] NOCHECK CONSTRAINT [FK_CorrespondenceDocuments_Correspondence];


GO
ALTER TABLE [dbo].[CorrespondenceDocuments] NOCHECK CONSTRAINT [FK_CorrespondenceDocuments_Document];

