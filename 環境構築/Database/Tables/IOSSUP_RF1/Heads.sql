USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Heads]    Script Date: 10/09/2021 08:40:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Heads](
	[ID] [smallint] NOT NULL,
	[AUTOCAP_DELAY] [smallint] NULL,
	[HORIZONTAL_TIMEOUT] [smallint] NULL,
	[VERTICAL_TIMEOUT] [smallint] NULL,
	[AUTOCAP_TIMEOUT] [smallint] NULL,
	[VERTICAL_MOVEMENT] [bit] NULL,
	[POSITIONS] [smallint] NULL,
	[AUTOCAP_H] [bit] NULL,
	[AUTOCAP_V] [bit] NULL,
 CONSTRAINT [PK_Heads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
