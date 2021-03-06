USE [IOS_RF1]
GO
/****** Object:  Table [dbo].[VARIABLES]    Script Date: 10/09/2021 08:37:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VARIABLES](
	[Tagname] [nvarchar](40) COLLATE Latin1_General_CI_AS NOT NULL,
	[Device] [smallint] NULL,
	[Address] [int] NULL,
	[Datatype] [nvarchar](2) COLLATE Latin1_General_CI_AS NULL,
	[Mnemonic] [nvarchar](15) COLLATE Latin1_General_CI_AS NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_VARIABLES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
