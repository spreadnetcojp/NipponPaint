USE [IOSSUP_RF1]
GO
/****** Object:  Table [dbo].[Circuits]    Script Date: 10/09/2021 08:40:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Circuits](
	[ID] [smallint] NOT NULL,
	[FLOWMETER_ENCODER] [smallint] NULL,
	[STIRRING_IN_DISPENSING] [smallint] NULL,
	[SPEED_1_RAMP_ENABLED] [smallint] NULL,
	[SPEED_2_RAMP_ENABLED] [smallint] NULL,
	[SPEED_3_RAMP_ENABLED] [smallint] NULL,
	[SPEED_4_RAMP_ENABLED] [smallint] NULL,
	[CIRCUIT_DISABLED] [smallint] NULL,
	[PUMP_FLOW] [int] NULL,
	[FLOWMETER_MM3_PULSE] [int] NULL,
	[ENCODER_PULSES_TURN] [smallint] NULL,
	[MAX_PRESSURE] [smallint] NULL,
	[TEST_VALVE_TIME] [smallint] NULL,
	[CIRCUIT_PRIORITY] [smallint] NULL,
	[PRE_DISP_SPEED_1] [smallint] NULL,
	[PRE_DISP_SPEED_2] [smallint] NULL,
	[PRE_DISP_SPEED_3] [smallint] NULL,
	[PRE_DISP_SPEED_4] [smallint] NULL,
	[SPEED_RAMP] [smallint] NULL,
	[SPEED_1] [smallint] NULL,
	[SPEED_2] [smallint] NULL,
	[SPEED_3] [smallint] NULL,
	[SPEED_4] [smallint] NULL,
	[VALVE_TIMEOUT] [smallint] NULL,
	[DISPENSING_TIMEOUT] [smallint] NULL,
	[PULSES_TIMEOUT] [smallint] NULL,
	[CIRCUIT_PRESSURE] [smallint] NULL,
	[PRESSURE_LIMIT] [smallint] NULL,
	[ANALOG_PRESSURE] [smallint] NULL,
	[EMPTY_PRESSURE] [smallint] NULL,
	[ROTATIONS] [int] NULL,
	[PUMP_MAINTENANCE] [int] NULL,
	[CIRCUIT_MIN] [int] NULL,
	[CIRCUIT_MAX] [int] NULL,
	[THRESHOLD_1] [int] NULL,
	[THRESHOLD_2] [int] NULL,
	[THRESHOLD_3] [int] NULL,
	[THRESHOLD_4] [int] NULL,
	[RAMP_ON_STEPS_1] [int] NULL,
	[RAMP_OFF_STEPS_1] [int] NULL,
	[PULSES_01_01] [int] NULL,
	[PULSES_01_02] [int] NULL,
	[PULSES_01_03] [int] NULL,
	[PULSES_01_04] [int] NULL,
	[PULSES_01_05] [int] NULL,
	[PULSES_01_06] [int] NULL,
	[PULSES_01_07] [int] NULL,
	[PULSES_01_08] [int] NULL,
	[PULSES_01_09] [int] NULL,
	[PULSES_01_10] [int] NULL,
	[QUANTITY_01_01] [int] NULL,
	[QUANTITY_01_02] [int] NULL,
	[QUANTITY_01_03] [int] NULL,
	[QUANTITY_01_04] [int] NULL,
	[QUANTITY_01_05] [int] NULL,
	[QUANTITY_01_06] [int] NULL,
	[QUANTITY_01_07] [int] NULL,
	[QUANTITY_01_08] [int] NULL,
	[QUANTITY_01_09] [int] NULL,
	[QUANTITY_01_10] [int] NULL,
	[RAMP_ON_STEPS_2] [int] NULL,
	[RAMP_OFF_STEPS_2] [int] NULL,
	[PULSES_02_01] [int] NULL,
	[PULSES_02_02] [int] NULL,
	[PULSES_02_03] [int] NULL,
	[PULSES_02_04] [int] NULL,
	[PULSES_02_05] [int] NULL,
	[PULSES_02_06] [int] NULL,
	[PULSES_02_07] [int] NULL,
	[PULSES_02_08] [int] NULL,
	[PULSES_02_09] [int] NULL,
	[PULSES_02_10] [int] NULL,
	[QUANTITY_02_01] [int] NULL,
	[QUANTITY_02_02] [int] NULL,
	[QUANTITY_02_03] [int] NULL,
	[QUANTITY_02_04] [int] NULL,
	[QUANTITY_02_05] [int] NULL,
	[QUANTITY_02_06] [int] NULL,
	[QUANTITY_02_07] [int] NULL,
	[QUANTITY_02_08] [int] NULL,
	[QUANTITY_02_09] [int] NULL,
	[QUANTITY_02_10] [int] NULL,
	[RAMP_ON_STEPS_3] [int] NULL,
	[RAMP_OFF_STEPS_3] [int] NULL,
	[PULSES_03_01] [int] NULL,
	[PULSES_03_02] [int] NULL,
	[PULSES_03_03] [int] NULL,
	[PULSES_03_04] [int] NULL,
	[PULSES_03_05] [int] NULL,
	[PULSES_03_06] [int] NULL,
	[PULSES_03_07] [int] NULL,
	[PULSES_03_08] [int] NULL,
	[PULSES_03_09] [int] NULL,
	[PULSES_03_10] [int] NULL,
	[QUANTITY_03_01] [int] NULL,
	[QUANTITY_03_02] [int] NULL,
	[QUANTITY_03_03] [int] NULL,
	[QUANTITY_03_04] [int] NULL,
	[QUANTITY_03_05] [int] NULL,
	[QUANTITY_03_06] [int] NULL,
	[QUANTITY_03_07] [int] NULL,
	[QUANTITY_03_08] [int] NULL,
	[QUANTITY_03_09] [int] NULL,
	[QUANTITY_03_10] [int] NULL,
	[RAMP_ON_STEPS_4] [int] NULL,
	[RAMP_OFF_STEPS_4] [int] NULL,
	[PULSES_04_01] [int] NULL,
	[PULSES_04_02] [int] NULL,
	[PULSES_04_03] [int] NULL,
	[PULSES_04_04] [int] NULL,
	[PULSES_04_05] [int] NULL,
	[PULSES_04_06] [int] NULL,
	[PULSES_04_07] [int] NULL,
	[PULSES_04_08] [int] NULL,
	[PULSES_04_09] [int] NULL,
	[PULSES_04_10] [int] NULL,
	[QUANTITY_04_01] [int] NULL,
	[QUANTITY_04_02] [int] NULL,
	[QUANTITY_04_03] [int] NULL,
	[QUANTITY_04_04] [int] NULL,
	[QUANTITY_04_05] [int] NULL,
	[QUANTITY_04_06] [int] NULL,
	[QUANTITY_04_07] [int] NULL,
	[QUANTITY_04_08] [int] NULL,
	[QUANTITY_04_09] [int] NULL,
	[QUANTITY_04_10] [int] NULL,
 CONSTRAINT [PK_Circuits] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
