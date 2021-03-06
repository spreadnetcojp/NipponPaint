USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Circuit_Head]    Script Date: 10/09/2021 08:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Circuit_Head](
	[Circuit_ID] [smallint] NOT NULL,
	[Head_ID] [smallint] NOT NULL,
 CONSTRAINT [PK_Circuit_Head] PRIMARY KEY CLUSTERED 
(
	[Circuit_ID] ASC,
	[Head_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
