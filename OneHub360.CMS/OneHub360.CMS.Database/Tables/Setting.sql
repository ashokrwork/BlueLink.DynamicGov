CREATE TABLE [dbo].[Setting] (
    [Name]  NVARCHAR (100) NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([Name] ASC)
);

