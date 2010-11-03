/*
Formula
Production (Qty for production for formula)
Raw Material (Issuence for production based on formula)
Production Plant (Receives issuence, production)
LAB (for testing)
	Approve
		Filling
		Warehouse
	Reject
		Production for Alteration (never waist)
---------------------------------
WorkOrder
Cutting
PreStiching Process
Stiching 1
Stiching 2
Stiching 3
Stiching 4
PostStiching Process
Fiinishing
Warehouse
*/
go
drop table Production_ProcessWorkFlow
drop table Production_Process
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_Process')
	CREATE TABLE [Production_Process](
		[Co_ID] [smallint] NOT NULL,
		[Process_ID] [smallint] NOT NULL,
		[Process_Code] [varchar](50) NOT NULL,
		[Process_Desc] [varchar](250) NOT NULL,
		[Stamp_UserID] [int] NOT NULL,
		[Stamp_DateTime] [datetime] NOT NULL,
		[Upload_DateTime] [datetime] NULL,
		[RecordStatus_ID] [int] NOT NULL,
	 CONSTRAINT [PK_Production_Process] PRIMARY KEY CLUSTERED 
	(
		[Co_ID] ASC,
		[Process_ID] ASC
	)
	)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_ProcessWorkFlow')
	CREATE TABLE [Production_ProcessWorkFlow](
		[Co_ID] [smallint] NOT NULL,
		[ProcessWorkFlow_ID] [int] NOT NULL,
		[Source_Process_ID] [smallint] NULL,
		[Destination_Process_ID] [smallint] NULL,
		[ProcessWorkFlow_Desc] [varchar](250) NOT NULL,
		[Stamp_UserID] [int] NOT NULL,
		[Stamp_DateTime] [datetime] NOT NULL,
		[Upload_DateTime] [datetime] NULL,
		[RecordStatus_ID] [int] NOT NULL,
	 CONSTRAINT [PK_Production_ProcessWorkFlow] PRIMARY KEY CLUSTERED 
	(
		[Co_ID] ASC,
		[ProcessWorkFlow_ID] ASC
	)
	)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_ProcessWorkFlow_Production_Process1')
	ALTER TABLE Production_ProcessWorkFlow
		WITH CHECK ADD  CONSTRAINT FK_Production_ProcessWorkFlow_Production_Process1
		FOREIGN KEY([Co_ID], [Source_Process_ID])
		REFERENCES Production_Process ([Co_ID], [Process_ID])
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_ProcessWorkFlow_Production_Process2')
	ALTER TABLE Production_ProcessWorkFlow
		WITH CHECK ADD  CONSTRAINT FK_Production_ProcessWorkFlow_Production_Process2
		FOREIGN KEY([Co_ID], [Destination_Process_ID])
		REFERENCES Production_Process ([Co_ID], [Process_ID])
go
