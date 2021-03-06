USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 10/09/2021 08:42:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[Circuit] [smallint] NULL,
	[Speed] [smallint] NULL,
	[Target_1] [int] NULL,
	[Target_2] [int] NULL,
	[Target_3] [int] NULL,
	[Target_4] [int] NULL,
	[Target_5] [int] NULL,
	[Target_6] [int] NULL,
	[Target_7] [int] NULL,
	[Target_8] [int] NULL,
	[Target_9] [int] NULL,
	[Target_10] [int] NULL,
	[Actual_1] [int] NULL,
	[Actual_2] [int] NULL,
	[Actual_3] [int] NULL,
	[Actual_4] [int] NULL,
	[Actual_5] [int] NULL,
	[Actual_6] [int] NULL,
	[Actual_7] [int] NULL,
	[Actual_8] [int] NULL,
	[Actual_9] [int] NULL,
	[Actual_10] [int] NULL,
	[TestSteps_1] [int] NULL,
	[TestSteps_2] [int] NULL,
	[TestSteps_3] [int] NULL,
	[TestSteps_4] [int] NULL,
	[TestSteps_5] [int] NULL,
	[TestSteps_6] [int] NULL,
	[TestSteps_7] [int] NULL,
	[TestSteps_8] [int] NULL,
	[TestSteps_9] [int] NULL,
	[TestSteps_10] [int] NULL,
	[Tested] [datetime] NULL,
	[Error_1] [float] NULL,
	[Error_2] [float] NULL,
	[Error_3] [float] NULL,
	[Error_4] [float] NULL,
	[Error_5] [float] NULL,
	[Error_6] [float] NULL,
	[Error_7] [float] NULL,
	[Error_8] [float] NULL,
	[Error_9] [float] NULL,
	[Error_10] [float] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
