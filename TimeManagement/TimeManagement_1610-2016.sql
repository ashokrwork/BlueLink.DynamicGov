USE [TimeManagement]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
	[FileBinaries] [varbinary](max) NOT NULL,
	[Description] [varchar](8000) NOT NULL,
	[Type] [varchar](8000) NOT NULL,
	[Discriminator] [varchar](8000) NOT NULL,
	[FK_Issue] [uniqueidentifier] NOT NULL,
	[FK_Project] [uniqueidentifier] NOT NULL,
	[FK_Task] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CheckList]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CheckList](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
	[Description] [varchar](8000) NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
 CONSTRAINT [PK_CheckList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Classification]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classification](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_Classification] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Classification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [uniqueidentifier] NOT NULL,
	[ThreadId] [uniqueidentifier] NOT NULL,
	[Body] [ntext] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[FK_Comment] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FiscalYear]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiscalYear](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
 CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Issue]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Issue](
	[Id] [uniqueidentifier] NOT NULL,
	[ReferenceNumber] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Description] [ntext] NULL,
	[RaisedBy] [nvarchar](256) NULL,
	[Type] [tinyint] NOT NULL,
	[RaisedDate] [date] NULL,
	[SolutionDate] [date] NULL,
	[Resolution] [ntext] NULL,
	[CommentThreadId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_RelatedToIssue] [uniqueidentifier] NULL,
	[FK_Severity] [uniqueidentifier] NOT NULL,
	[FK_Priority] [uniqueidentifier] NOT NULL,
	[FK_Project] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeetingInvitations]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeetingInvitations](
	[Id] [uniqueidentifier] NOT NULL,
	[To] [varchar](8000) NOT NULL,
	[FK_Meeting] [uniqueidentifier] NOT NULL,
	[FK_InvitationStatus] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MeetingInvitations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlannedItem]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlannedItem](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Description] [ntext] NULL,
	[PlannedStartDate] [date] NOT NULL,
	[PlannedEndDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[ReferenceNumber] [nvarchar](256) NULL,
	[SignedPDF] [image] NULL,
	[CommentThreadId] [uniqueidentifier] NULL,
	[PercentComplete] [decimal](18, 0) NULL,
	[IsCompleted] [bit] NULL,
	[Subject] [varchar](8000) NULL,
	[Location] [varchar](8000) NULL,
	[Discriminator] [varchar](8000) NOT NULL,
	[FK_CheckList] [uniqueidentifier] NOT NULL,
	[FK_Project] [uniqueidentifier] NOT NULL,
	[FK_ParentTask] [uniqueidentifier] NOT NULL,
	[FK_Priority] [uniqueidentifier] NOT NULL,
	[FK_Severity] [uniqueidentifier] NOT NULL,
	[FK_AppointmentStatus] [uniqueidentifier] NOT NULL,
	[FK_MeetingStatus] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PlannedItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Priority]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Priority](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[PlannedStartDate] [date] NULL,
	[PlannedEndDate] [date] NULL,
	[ActualStartDate] [date] NULL,
	[ActualEndDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_FiscalYear] [uniqueidentifier] NULL,
	[FK_ProjectStatus] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectClassification]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectClassification](
	[FK_Project] [uniqueidentifier] NOT NULL,
	[FK_Classification] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProjectClassification] PRIMARY KEY CLUSTERED 
(
	[FK_Project] ASC,
	[FK_Classification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PublicHoliday]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PublicHoliday](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_FiscalYear] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PublicHoliday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resource](
	[Id] [uniqueidentifier] NOT NULL,
	[LoginName] [varchar](8000) NOT NULL,
	[FK_TeamworkMember] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Severity]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Severity](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
 CONSTRAINT [PK_Severity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
	[Discriminator] [varchar](8000) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaskAssignment]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaskAssignment](
	[Id] [uniqueidentifier] NOT NULL,
	[AssignedTo] [varchar](8000) NOT NULL,
	[RemainingTime] [decimal](18, 0) NOT NULL,
	[ActualStartDate] [date] NULL,
	[ActualEndDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_Task] [uniqueidentifier] NOT NULL,
	[FK_TaskAssignmentStatus] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TaskAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teamwork]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teamwork](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
	[Description] [varchar](8000) NOT NULL,
	[Objectives] [varchar](8000) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_Project] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Teamwork] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TeamworkMember]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamworkMember](
	[Id] [uniqueidentifier] NOT NULL,
	[PlannedStartDate] [date] NULL,
	[PlannedEndDate] [date] NULL,
	[ActualStartDate] [date] NULL,
	[ActualEndDate] [date] NULL,
	[FK_Teamwork] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TeamworkMember] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Timesheet]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Timesheet](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](8000) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Owner] [varchar](8000) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[FK_TimesheetStatus] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Timesheet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimesheetItem]    Script Date: 10/16/2016 7:50:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TimesheetItem](
	[Id] [uniqueidentifier] NOT NULL,
	[Comment] [varchar](8000) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Duration] [decimal](18, 0) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[FK_Timesheet] [uniqueidentifier] NOT NULL,
	[FK_PlannedItem] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TimesheetItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Classification] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FiscalYear] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Issue] ADD  DEFAULT ((1)) FOR [Type]
GO
ALTER TABLE [dbo].[Issue] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PlannedItem] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PublicHoliday] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TaskAssignment] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Teamwork] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Timesheet] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TimesheetItem] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Issue_0] FOREIGN KEY([FK_Issue])
REFERENCES [dbo].[Issue] ([Id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Issue_0]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_PlannedItem_2] FOREIGN KEY([FK_Task])
REFERENCES [dbo].[PlannedItem] ([Id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_PlannedItem_2]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Project_1] FOREIGN KEY([FK_Project])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Project_1]
GO
ALTER TABLE [dbo].[Classification]  WITH CHECK ADD  CONSTRAINT [FK_Classification_Classification_0] FOREIGN KEY([FK_Classification])
REFERENCES [dbo].[Classification] ([Id])
GO
ALTER TABLE [dbo].[Classification] CHECK CONSTRAINT [FK_Classification_Classification_0]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments_0] FOREIGN KEY([FK_Comment])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments_0]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Issue_0] FOREIGN KEY([FK_RelatedToIssue])
REFERENCES [dbo].[Issue] ([Id])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_Issue_0]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Priority_2] FOREIGN KEY([FK_Priority])
REFERENCES [dbo].[Priority] ([Id])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_Priority_2]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Project_3] FOREIGN KEY([FK_Project])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_Project_3]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Severity_1] FOREIGN KEY([FK_Severity])
REFERENCES [dbo].[Severity] ([Id])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_Severity_1]
GO
ALTER TABLE [dbo].[MeetingInvitations]  WITH CHECK ADD  CONSTRAINT [FK_MeetingInvitations_PlannedItem_0] FOREIGN KEY([FK_Meeting])
REFERENCES [dbo].[PlannedItem] ([Id])
GO
ALTER TABLE [dbo].[MeetingInvitations] CHECK CONSTRAINT [FK_MeetingInvitations_PlannedItem_0]
GO
ALTER TABLE [dbo].[MeetingInvitations]  WITH CHECK ADD  CONSTRAINT [FK_MeetingInvitations_Status_1] FOREIGN KEY([FK_InvitationStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[MeetingInvitations] CHECK CONSTRAINT [FK_MeetingInvitations_Status_1]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_CheckList_0] FOREIGN KEY([FK_CheckList])
REFERENCES [dbo].[CheckList] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_CheckList_0]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_PlannedItem_2] FOREIGN KEY([FK_ParentTask])
REFERENCES [dbo].[PlannedItem] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_PlannedItem_2]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_Priority_3] FOREIGN KEY([FK_Priority])
REFERENCES [dbo].[Priority] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_Priority_3]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_Project_1] FOREIGN KEY([FK_Project])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_Project_1]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_Severity_4] FOREIGN KEY([FK_Severity])
REFERENCES [dbo].[Severity] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_Severity_4]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_Status_5] FOREIGN KEY([FK_AppointmentStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_Status_5]
GO
ALTER TABLE [dbo].[PlannedItem]  WITH CHECK ADD  CONSTRAINT [FK_PlannedItem_Status_6] FOREIGN KEY([FK_MeetingStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[PlannedItem] CHECK CONSTRAINT [FK_PlannedItem_Status_6]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_FiscalYear_0] FOREIGN KEY([FK_FiscalYear])
REFERENCES [dbo].[FiscalYear] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_FiscalYear_0]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Status_1] FOREIGN KEY([FK_ProjectStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Status_1]
GO
ALTER TABLE [dbo].[ProjectClassification]  WITH CHECK ADD  CONSTRAINT [FK_ProjectClassification_Classification_1] FOREIGN KEY([FK_Classification])
REFERENCES [dbo].[Classification] ([Id])
GO
ALTER TABLE [dbo].[ProjectClassification] CHECK CONSTRAINT [FK_ProjectClassification_Classification_1]
GO
ALTER TABLE [dbo].[ProjectClassification]  WITH CHECK ADD  CONSTRAINT [FK_ProjectClassification_Project_0] FOREIGN KEY([FK_Project])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectClassification] CHECK CONSTRAINT [FK_ProjectClassification_Project_0]
GO
ALTER TABLE [dbo].[PublicHoliday]  WITH CHECK ADD  CONSTRAINT [FK_PublicHoliday_FiscalYear_0] FOREIGN KEY([FK_FiscalYear])
REFERENCES [dbo].[FiscalYear] ([Id])
GO
ALTER TABLE [dbo].[PublicHoliday] CHECK CONSTRAINT [FK_PublicHoliday_FiscalYear_0]
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_TeamworkMember_0] FOREIGN KEY([FK_TeamworkMember])
REFERENCES [dbo].[TeamworkMember] ([Id])
GO
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_TeamworkMember_0]
GO
ALTER TABLE [dbo].[TaskAssignment]  WITH CHECK ADD  CONSTRAINT [FK_TaskAssignment_PlannedItem_0] FOREIGN KEY([FK_Task])
REFERENCES [dbo].[PlannedItem] ([Id])
GO
ALTER TABLE [dbo].[TaskAssignment] CHECK CONSTRAINT [FK_TaskAssignment_PlannedItem_0]
GO
ALTER TABLE [dbo].[TaskAssignment]  WITH CHECK ADD  CONSTRAINT [FK_TaskAssignment_Status_1] FOREIGN KEY([FK_TaskAssignmentStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[TaskAssignment] CHECK CONSTRAINT [FK_TaskAssignment_Status_1]
GO
ALTER TABLE [dbo].[Teamwork]  WITH CHECK ADD  CONSTRAINT [FK_Teamwork_Project_0] FOREIGN KEY([FK_Project])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Teamwork] CHECK CONSTRAINT [FK_Teamwork_Project_0]
GO
ALTER TABLE [dbo].[TeamworkMember]  WITH CHECK ADD  CONSTRAINT [FK_TeamworkMember_Teamwork_0] FOREIGN KEY([FK_Teamwork])
REFERENCES [dbo].[Teamwork] ([ID])
GO
ALTER TABLE [dbo].[TeamworkMember] CHECK CONSTRAINT [FK_TeamworkMember_Teamwork_0]
GO
ALTER TABLE [dbo].[Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Status_0] FOREIGN KEY([FK_TimesheetStatus])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Timesheet] CHECK CONSTRAINT [FK_Timesheet_Status_0]
GO
ALTER TABLE [dbo].[TimesheetItem]  WITH CHECK ADD  CONSTRAINT [FK_TimesheetItem_PlannedItem_1] FOREIGN KEY([FK_PlannedItem])
REFERENCES [dbo].[PlannedItem] ([Id])
GO
ALTER TABLE [dbo].[TimesheetItem] CHECK CONSTRAINT [FK_TimesheetItem_PlannedItem_1]
GO
ALTER TABLE [dbo].[TimesheetItem]  WITH CHECK ADD  CONSTRAINT [FK_TimesheetItem_Timesheet_0] FOREIGN KEY([FK_Timesheet])
REFERENCES [dbo].[Timesheet] ([Id])
GO
ALTER TABLE [dbo].[TimesheetItem] CHECK CONSTRAINT [FK_TimesheetItem_Timesheet_0]
GO
