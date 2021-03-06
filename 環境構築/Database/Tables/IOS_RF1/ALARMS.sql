USE [IOS_RF1]
GO
/****** Object:  Table [dbo].[ALARMS]    Script Date: 10/09/2021 08:35:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ALARMS](
	[Tagname] [nvarchar](40) COLLATE Latin1_General_CI_AS NOT NULL,
	[Device] [smallint] NULL,
	[Address] [int] NULL,
	[Datatype] [nvarchar](2) COLLATE Latin1_General_CI_AS NULL,
	[Mnemonic] [nvarchar](15) COLLATE Latin1_General_CI_AS NULL,
	[Value] [float] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ALARMS] PRIMARY KEY CLUSTERED 
(
	[Tagname] ASC,
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
