SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
DROP TABLE [dbo].[Tasks]
GO
CREATE TABLE [dbo].[Tasks](
	[TasksID] [int] IDENTITY(1,1),
	[Name] nVarChar(255) NOT NULL,
	[TaskDescription] nVarChar(2000) NOT NULL,
	[TaskPriority] [int] NOT NULL,
	[Added] [datetime] NOT NULL,
	[DateToComplete] [datetime] NOT NULL,
	[TaskStatus] nVarChar(10) NOT NULL,
	[FLAGS] [int] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[UserCode] [varchar](30) NOT NULL,
	[TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TasksID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_FLAGS]  DEFAULT ((0)) FOR [FLAGS]
GO

ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO

ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_Added]  DEFAULT (getdate()) FOR [Added]
GO

ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_TaskStatus]  DEFAULT ('Active') FOR [TaskStatus]
GO

ALTER TABLE Tasks ADD CONSTRAINT Tasks_TaskStatus_CH CHECK (TaskStatus='Active' Or TaskStatus='Completed');