USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 10/09/2021 08:41:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Data_Path] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Reply_Path] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Report_Path] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Usage_Path] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Pos_Weight_Tolerance] [smallint] NULL,
	[Neg_Weight_Tolerance] [smallint] NULL,
	[A] [bit] NULL,
	[B] [bit] NULL,
	[C] [bit] NULL,
	[D] [bit] NULL,
	[E] [bit] NULL,
	[F] [bit] NULL,
	[G] [bit] NULL,
	[H] [bit] NULL,
	[I] [bit] NULL,
	[J] [bit] NULL,
	[K] [bit] NULL,
	[L] [bit] NULL,
	[M] [bit] NULL,
	[N] [bit] NULL,
	[O] [bit] NULL,
	[P] [bit] NULL,
	[Q] [bit] NULL,
	[R] [bit] NULL,
	[S] [bit] NULL,
	[T] [bit] NULL,
	[U] [bit] NULL,
	[V] [bit] NULL,
	[W] [bit] NULL,
	[X] [bit] NULL,
	[Y] [bit] NULL,
	[Z] [bit] NULL,
	[QCP1] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP2] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP3] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP4] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP5] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP6] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP7] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP8] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[QCP9] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mix_Reduction] [int] NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
