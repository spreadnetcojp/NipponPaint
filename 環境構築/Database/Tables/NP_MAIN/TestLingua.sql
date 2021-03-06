USE [NP_MAIN]
GO
/****** Object:  Table [dbo].[TestLingua]    Script Date: 10/09/2021 09:15:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestLingua](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NATIVE] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FOREIGN_1] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FOREIGN_2] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TestLingua] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
