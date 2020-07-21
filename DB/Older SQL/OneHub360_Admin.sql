USE [master]
GO
/****** Object:  Database [OneHub_Admin]    Script Date: 1/23/2017 7:07:55 PM ******/
CREATE DATABASE [OneHub_Admin]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'App', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Admin.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'App_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Admin_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OneHub_Admin] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OneHub_Admin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OneHub_Admin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OneHub_Admin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OneHub_Admin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OneHub_Admin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OneHub_Admin] SET ARITHABORT OFF 
GO
ALTER DATABASE [OneHub_Admin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OneHub_Admin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OneHub_Admin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OneHub_Admin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OneHub_Admin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OneHub_Admin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OneHub_Admin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OneHub_Admin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OneHub_Admin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OneHub_Admin] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OneHub_Admin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OneHub_Admin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OneHub_Admin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OneHub_Admin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OneHub_Admin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OneHub_Admin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OneHub_Admin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OneHub_Admin] SET RECOVERY FULL 
GO
ALTER DATABASE [OneHub_Admin] SET  MULTI_USER 
GO
ALTER DATABASE [OneHub_Admin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OneHub_Admin] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OneHub_Admin] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OneHub_Admin] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [OneHub_Admin] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OneHub_Admin] SET QUERY_STORE = OFF
GO
USE [OneHub_Admin]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [OneHub_Admin]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [uniqueidentifier] NOT NULL,
	[ListingOrder] [int] NULL,
	[IsFavourite] [bit] NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[FK_UserInfo] [uniqueidentifier] NOT NULL,
	[FK_ContactUserInfo] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContactsGroup]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactsGroup](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[Status] [int] NOT NULL,
	[Discriminator] [nvarchar](4000) NOT NULL,
	[FK_UserInfo] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EntityType]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[Discriminator] [nvarchar](4000) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_EntityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExternalGroupsMembers]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExternalGroupsMembers](
	[FK_OrganizationUnit] [uniqueidentifier] NOT NULL,
	[FK_ExternalGroup] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ExternalGroupsMembers] PRIMARY KEY CLUSTERED 
(
	[FK_OrganizationUnit] ASC,
	[FK_ExternalGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InternalGroupsMembers]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalGroupsMembers](
	[FK_UserInfo] [uniqueidentifier] NOT NULL,
	[FK_InternalGroup] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InternalGroupsMembers] PRIMARY KEY CLUSTERED 
(
	[FK_UserInfo] ASC,
	[FK_InternalGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invitations]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitations](
	[Id] [uniqueidentifier] NOT NULL,
	[ReplayDate] [datetime2](7) NULL,
	[Status] [int] NOT NULL,
	[FK_UserInfoFrom] [uniqueidentifier] NOT NULL,
	[FK_UserInfoTo] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Invitations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobTitles]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitles](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](512) NOT NULL,
	[Responsibilities] [nvarchar](4000) NULL,
	[Rank] [int] NULL,
	[Description] [nvarchar](1024) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_JobTitles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organization]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[Id] [nvarchar](4000) NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[About] [nvarchar](4000) NULL,
	[URL] [nvarchar](4000) NULL,
	[Email] [nvarchar](4000) NOT NULL,
	[Fax] [nvarchar](4000) NULL,
	[IsLocal] [bit] NOT NULL,
	[Address] [nvarchar](4000) NULL,
	[OfficeNumber] [nvarchar](4000) NULL,
	[FK_OrganizationType] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrganizationUnit]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationUnit](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[About] [nvarchar](4000) NULL,
	[Location] [nvarchar](4000) NULL,
	[FK_OrganizationUnitParent] [uniqueidentifier] NULL,
	[FK_Manager] [uniqueidentifier] NULL,
	[FK_Organization] [nvarchar](4000) NULL,
	[FK_OrganizationUnitType] [uniqueidentifier] NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_OrganizationUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolesUsers]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesUsers](
	[FK_Role] [uniqueidentifier] NOT NULL,
	[FK_UserInfo] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RolesUsers] PRIMARY KEY CLUSTERED 
(
	[FK_Role] ASC,
	[FK_UserInfo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Signature]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Signature](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](4000) NOT NULL,
	[Image] [varbinary](max) NOT NULL,
	[EnrollmentDate] [datetime2](7) NOT NULL,
	[NotAfter] [datetime2](7) NOT NULL,
	[NotBefore] [datetime2](7) NOT NULL,
	[Certificate] [nvarchar](4000) NOT NULL,
	[Status] [int] NOT NULL,
	[SMSActivationCode] [nvarchar](4000) NOT NULL,
	[EmailActivationCode] [nvarchar](4000) NOT NULL,
	[ActivationFailureCount] [int] NOT NULL,
	[FK_UserInfo] [uniqueidentifier] NOT NULL,
	[IncludePrivateKey] [bit] NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Signature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 1/23/2017 7:07:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[LoginName] [nvarchar](4000) NOT NULL,
	[Password] [nvarchar](4000) NULL,
	[PasswordSalt] [nvarchar](4000) NULL,
	[ArabicFullName] [nvarchar](4000) NOT NULL,
	[LatinFullName] [nvarchar](4000) NULL,
	[Email] [nvarchar](4000) NOT NULL,
	[Mobile] [nvarchar](4000) NOT NULL,
	[Photo] [varbinary](max) NULL,
	[PersonalMessage] [nvarchar](4000) NULL,
	[BirthDate] [datetime2](7) NULL,
	[About] [nvarchar](4000) NULL,
	[OfficePhone] [decimal](18, 0) NULL,
	[Status] [int] NOT NULL,
	[FK_OrganizationUnit] [uniqueidentifier] NULL,
	[FK_JobTitle] [uniqueidentifier] NULL,
	[FK_ReportingTo] [uniqueidentifier] NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_UserInfos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ContactsGroup] ADD  CONSTRAINT [DF_Group_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[JobTitles] ADD  CONSTRAINT [DF_JobTitles_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Signature] ADD  CONSTRAINT [DF_Signature_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Signature] ADD  CONSTRAINT [DF_Signature_IncludePrivateKey]  DEFAULT ((1)) FOR [IncludePrivateKey]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfos_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_UserInfos_0] FOREIGN KEY([FK_UserInfo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_UserInfos_0]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_UserInfos_1] FOREIGN KEY([FK_ContactUserInfo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_UserInfos_1]
GO
ALTER TABLE [dbo].[ExternalGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_ExternalGroupsMembers_Group_1] FOREIGN KEY([FK_ExternalGroup])
REFERENCES [dbo].[ContactsGroup] ([Id])
GO
ALTER TABLE [dbo].[ExternalGroupsMembers] CHECK CONSTRAINT [FK_ExternalGroupsMembers_Group_1]
GO
ALTER TABLE [dbo].[InternalGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_InternalGroupsMembers_Group_1] FOREIGN KEY([FK_InternalGroup])
REFERENCES [dbo].[ContactsGroup] ([Id])
GO
ALTER TABLE [dbo].[InternalGroupsMembers] CHECK CONSTRAINT [FK_InternalGroupsMembers_Group_1]
GO
ALTER TABLE [dbo].[InternalGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_InternalGroupsMembers_UserInfos_0] FOREIGN KEY([FK_UserInfo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[InternalGroupsMembers] CHECK CONSTRAINT [FK_InternalGroupsMembers_UserInfos_0]
GO
ALTER TABLE [dbo].[Invitations]  WITH CHECK ADD  CONSTRAINT [FK_Invitations_UserInfos_0] FOREIGN KEY([FK_UserInfoFrom])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[Invitations] CHECK CONSTRAINT [FK_Invitations_UserInfos_0]
GO
ALTER TABLE [dbo].[Invitations]  WITH CHECK ADD  CONSTRAINT [FK_Invitations_UserInfos_1] FOREIGN KEY([FK_UserInfoTo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[Invitations] CHECK CONSTRAINT [FK_Invitations_UserInfos_1]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_EntityType_0] FOREIGN KEY([FK_OrganizationType])
REFERENCES [dbo].[EntityType] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_EntityType_0]
GO
ALTER TABLE [dbo].[OrganizationUnit]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationUnits_EntityType_3] FOREIGN KEY([FK_OrganizationUnitType])
REFERENCES [dbo].[EntityType] ([Id])
GO
ALTER TABLE [dbo].[OrganizationUnit] CHECK CONSTRAINT [FK_OrganizationUnits_EntityType_3]
GO
ALTER TABLE [dbo].[OrganizationUnit]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationUnits_Organization_2] FOREIGN KEY([FK_Organization])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[OrganizationUnit] CHECK CONSTRAINT [FK_OrganizationUnits_Organization_2]
GO
ALTER TABLE [dbo].[OrganizationUnit]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationUnits_OrganizationUnits_0] FOREIGN KEY([FK_OrganizationUnitParent])
REFERENCES [dbo].[OrganizationUnit] ([Id])
GO
ALTER TABLE [dbo].[OrganizationUnit] CHECK CONSTRAINT [FK_OrganizationUnits_OrganizationUnits_0]
GO
ALTER TABLE [dbo].[OrganizationUnit]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationUnits_UserInfos_1] FOREIGN KEY([FK_Manager])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[OrganizationUnit] CHECK CONSTRAINT [FK_OrganizationUnits_UserInfos_1]
GO
ALTER TABLE [dbo].[RolesUsers]  WITH CHECK ADD  CONSTRAINT [FK_RolesUsers_Role_0] FOREIGN KEY([FK_Role])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RolesUsers] CHECK CONSTRAINT [FK_RolesUsers_Role_0]
GO
ALTER TABLE [dbo].[RolesUsers]  WITH CHECK ADD  CONSTRAINT [FK_RolesUsers_UserInfos_1] FOREIGN KEY([FK_UserInfo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[RolesUsers] CHECK CONSTRAINT [FK_RolesUsers_UserInfos_1]
GO
ALTER TABLE [dbo].[Signature]  WITH CHECK ADD  CONSTRAINT [FK_Signature_UserInfos_0] FOREIGN KEY([FK_UserInfo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[Signature] CHECK CONSTRAINT [FK_Signature_UserInfos_0]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfos_JobTitles_1] FOREIGN KEY([FK_JobTitle])
REFERENCES [dbo].[JobTitles] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfos_JobTitles_1]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfos_OrganizationUnits_0] FOREIGN KEY([FK_OrganizationUnit])
REFERENCES [dbo].[OrganizationUnit] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfos_OrganizationUnits_0]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfos_UserInfos_2] FOREIGN KEY([FK_ReportingTo])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfos_UserInfos_2]
GO
USE [master]
GO
ALTER DATABASE [OneHub_Admin] SET  READ_WRITE 
GO
