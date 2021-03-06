USE [ORDER_RF1]
GO
/****** Object:  Table [dbo].[Labels]    Script Date: 10/09/2021 08:45:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Labels](
	[Label_Type] [int] NULL,
	[Label_Description] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[JIS_Logo] [bit] NULL,
	[FLA_Logo] [bit] NULL,
	[TOX_Logo] [bit] NULL,
	[NPC_Logo] [bit] NULL
) ON [PRIMARY]
