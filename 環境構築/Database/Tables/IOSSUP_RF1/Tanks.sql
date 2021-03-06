USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Tanks]    Script Date: 10/09/2021 08:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tanks](
	[ID] [int] NOT NULL,
	[PRODUCT_ID] [int] NULL,
	[TANK_VOLUME] [int] NULL,
	[TANK_LEVEL] [int] NULL,
	[WARNING_LEVEL_%] [int] NULL,
	[MINIMUM_LEVEL_%] [int] NULL,
	[FULL_TANK_%] [int] NULL,
	[FULL_HYSTERESIS_%] [int] NULL,
	[TANK_EMPTY] [int] NULL,
	[TANK_FULL] [int] NULL,
	[TANK_ANALOG] [int] NULL,
	[TANK_LEVEL_%] [int] NULL,
 CONSTRAINT [PK_Tanks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
