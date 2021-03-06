USE [NP_MAIN]
GO
/****** Object:  Table [dbo].[Main_Set]    Script Date: 10/09/2021 09:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Main_Set](
	[Main_Set_Id] [int] IDENTITY(1,1) NOT NULL,
	[Color_Station_Area_Code] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Station_Code] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RF4_Weight_Threshold] [int] NULL,
	[TRK_Current_First] [float] NULL,
	[TRK_Current_Last] [float] NULL,
	[TRK_Latest_Used] [float] NULL,
	[TRK_Next_First] [float] NULL,
	[TRK_Next_Last] [float] NULL,
	[TRK_Departure_Code] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Departure_Name] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Server] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Port] [int] NULL,
	[FTP_User] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Password] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Dir_Receive] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Dir_Send] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Dir_Notify] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Delay_Check] [int] NULL,
	[Auto_Close_Orders] [bit] NULL,
	[Archive_Permanence] [int] NULL,
	[SS_Date] [datetime] NULL,
	[SS_Sequence] [int] NULL,
	[KeepAliveDate] [datetime] NULL,
	[KeepAliveTimeOut] [int] NULL,
	[KeepAliveStatus] [int] NULL,
	[CSV_Path] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CSV_Day] [int] NULL,
	[CSV_Time] [datetime] NULL,
	[BCK_Daily_Time] [int] NULL,
	[BCK_Daily_Last] [datetime] NULL,
	[BCK_Weekly_Time] [datetime] NULL,
	[BCK_Weekly_Last] [datetime] NULL,
	[BCK_Path] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Main_Set] PRIMARY KEY CLUSTERED 
(
	[Main_Set_Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
