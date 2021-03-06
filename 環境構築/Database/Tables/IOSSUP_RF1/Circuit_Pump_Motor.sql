USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Circuit_Pump_Motor]    Script Date: 10/09/2021 08:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Circuit_Pump_Motor](
	[Circuit_ID] [smallint] NOT NULL,
	[Pump_Motor_ID] [smallint] NOT NULL,
 CONSTRAINT [PK_Circuit_Pump_Motor] PRIMARY KEY CLUSTERED 
(
	[Circuit_ID] ASC,
	[Pump_Motor_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
