USE [OneHub_Admin]
GO

/****** Object:  Table [dbo].[Configuration]    Script Date: 5/14/2017 1:24:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Configuration](
	[Id] [uniqueidentifier] NOT NULL,
	[ConfigurationName] [nvarchar](200) NULL,
	[ConfigurationValue] [nvarchar](2000) NULL,
	[ConfigurationGroup] [nvarchar](200) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Configuration] ADD  CONSTRAINT [DF_Configuration_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

