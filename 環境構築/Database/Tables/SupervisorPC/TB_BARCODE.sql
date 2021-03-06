CREATE TABLE [dbo].[TB_BARCODE](
[BARCODE] [nvarchar](50) NOT NULL,
[PROCESS_CODE] [nvarchar](12) NOT NULL,
[BRC_TIME_INSERTED] [datetime] NULL,
[BRC_TIME_PROCESSED] [datetime] NULL,
[BRC_STATUS] [int] NULL,
[BRC_ERR_1] [nvarchar](255) NULL,
[BRC_ERR_2] [nvarchar](255) NULL,
[BRC_ERR_3] [nvarchar](255) NULL,
CONSTRAINT [PK_MaskBarcode] PRIMARY KEY CLUSTERED
(
[BARCODE] ASC,
[PROCESS_CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS
= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
