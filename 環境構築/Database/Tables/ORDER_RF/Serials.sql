USE [ORDER_RF1]
GO
/****** Object:  Table [dbo].[Serials]    Script Date: 10/09/2021 08:45:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Serials](
	[Local_Plant_ID] [int] NULL,
	[S1_ComPort] [int] NULL,
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
	[S2_ComPort] [int] NULL,
	[S2_Baud] [int] NULL,
	[S2_DataBits] [int] NULL,
	[S2_StopBits] [int] NULL,
	[S2_ParityBits] [int] NULL,
	[S2_RTS_Input] [int] NULL,
	[S2_DTR_Input] [int] NULL,
	[S2_CTS_Output] [bit] NULL,
	[S2_DSR_Output] [bit] NULL,
	[S2_XonXoff_Output] [bit] NULL,
	[S2_XonXoff_Input] [bit] NULL,
	[S3_ComPort] [int] NULL,
	[S3_Baud] [int] NULL,
	[S3_DataBits] [int] NULL,
	[S3_StopBits] [int] NULL,
	[S3_ParityBits] [int] NULL,
	[S3_RTS_Input] [int] NULL,
	[S3_DTR_Input] [int] NULL,
	[S3_CTS_Output] [bit] NULL,
	[S3_DSR_Output] [bit] NULL,
	[S3_XonXoff_Output] [bit] NULL,
	[S3_XonXoff_Input] [bit] NULL,
	[Label_1_Offset_X] [int] NULL,
	[Label_1_Offset_Y] [int] NULL,
	[Label_2_Offset_X] [int] NULL,
	[Label_2_Offset_Y] [int] NULL,
	[XML_Printing] [bit] NULL
) ON [PRIMARY]
