USE [NP_MAIN]
GO
/****** Object:  Table [dbo].[Serials]    Script Date: 10/09/2021 09:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Serials](
	[S1_ComPort] [int] NOT NULL,
	[S1_Baud] [int] NULL,
	[S1_DataBits] [int] NULL,
	[S1_StopBits] [int] NULL,
	[S1_ParityBits] [int] NULL,
	[S1_RTS_Input] [int] NULL,
	[S1_DTR_Input] [int] NULL,
	[S1_CTS_Output] [bit] NULL,
	[S1_DSR_Output] [bit] NULL,
	[S1_XonXoff_Output] [bit] NULL,
	[S1_XonXoff_Input] [bit] NULL,
 CONSTRAINT [PK_Serials] PRIMARY KEY CLUSTERED 
(
	[S1_ComPort] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
