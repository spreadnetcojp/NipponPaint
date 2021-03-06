USE [IOS_RF1]
GO
/****** Object:  Table [dbo].[ACTALMS]    Script Date: 10/09/2021 08:34:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACTALMS](
	[Tagname] [nvarchar](40) COLLATE Latin1_General_CI_AS NOT NULL,
	[Bit] [smallint] NULL,
	[Device] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[Msg] [nvarchar](80) COLLATE Latin1_General_CI_AS NULL,
	[Time] [datetime] NULL,
	[DataType] [nvarchar](2) COLLATE Latin1_General_CI_AS NULL,
	[Active] [bit] NULL,
	[Reset] [bit] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ACTALMS] PRIMARY KEY CLUSTERED 
(
	[Tagname] ASC,
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
