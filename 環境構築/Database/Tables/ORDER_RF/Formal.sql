USE [ORDER_RF1]
GO
/****** Object:  Table [dbo].[Formal]    Script Date: 10/09/2021 08:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formal](
	[CCM_Paint_Name] [nvarchar](96) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Formal_Name] [nvarchar](96) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Label_Type] [int] NULL
) ON [PRIMARY]
