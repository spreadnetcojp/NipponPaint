USE [NP_MAIN]
GO
/****** Object:  Table [dbo].[Plants_Settings]    Script Date: 10/09/2021 09:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plants_Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Plant_ID] [int] NOT NULL,
	[FTP_Server] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Port] [int] NULL,
	[FTP_User] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Password] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Dir_Receive] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Dir_Send] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FTP_Dir_Notify] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Enabled] [bit] NOT NULL CONSTRAINT [DF_Plants_Settings_Enabled]  DEFAULT ((0)),
	[LocalDir] [nchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Shipper_Code] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Shipper_Address_1] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Shipper_Address_2] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Shipper_Company] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Shipper_Subsidiary] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRK_Telephone_Number] [nvarchar](24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Plants_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
