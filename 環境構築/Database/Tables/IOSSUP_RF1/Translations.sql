USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Translations]    Script Date: 10/09/2021 08:42:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Translations](
	[NATIVE] [nvarchar](80) COLLATE Latin1_General_CI_AS NULL,
	[FOREIGN_1] [nvarchar](80) COLLATE Latin1_General_CI_AS NULL,
	[FOREIGN_2] [nvarchar](80) COLLATE Latin1_General_CI_AS NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Translations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
