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
drop table Production_ProcessProduction_Detail
drop table Production_ProcessProduction
drop table Production_OrderBatch_Detail
drop table Production_OrderBatch
drop table Production_Order_Detail
drop table Production_Order
drop table Production_Formula_Detail
drop table Production_Formula
drop table Production_ProcessWorkFlow
drop table Production_Process
drop table Inv_Item_Detail
drop table Inv_ItemGrade
drop table Common_Color
DELETE FROM Base_Menu WHERE Menu_ID BETWEEN 800 AND 850
alter table sec_user drop constraint FK_Country_Sec_User
alter table sec_user drop constraint FK_City_Sec_User
drop table Common_City
drop table Common_Country
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
IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inv_Item_Size_Association_History')
	DROP TABLE Inv_Item_Size_Association_History
go
IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inv_Item_Size_Association')
	DROP TABLE Inv_Item_Size_Association
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] ='Inv_ItemSize' AND c.[Name] = 'ItemSize_Code')
	ALTER TABLE Inv_ItemSize
		ADD ItemSize_Code VARCHAR(10) NOT NULL
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Common_Color')
	CREATE TABLE Common_Color(
		Co_ID SMALLINT NOT NULL,
		Color_ID SMALLINT NOT NULL,
		Color_Code VARCHAR(10) NOT NULL,
		Color_Desc VARCHAR(250) NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Common_Color] PRIMARY KEY CLUSTERED 
	(
		[Co_ID] ASC,
		[Color_ID] ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inv_ItemGrade')
	CREATE TABLE Inv_ItemGrade(
		Co_ID SMALLINT NOT NULL,
		ItemGrade_ID SMALLINT NOT NULL,
		ItemGrade_Code VARCHAR(10) NOT NULL,
		ItemGrade_Desc VARCHAR(250) NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Inv_ItemGrade] PRIMARY KEY CLUSTERED 
	(
		[Co_ID] ASC,
		[ItemGrade_ID] ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inv_Item_Detail')
	CREATE TABLE Inv_Item_Detail(
		Co_ID SMALLINT NOT NULL,
		Item_Detail_ID INT NOT NULL,
		Item_ID INT NOT NULL,
		Color_ID SMALLINT NULL,
		ItemGrade_ID SMALLINT NULL,
		ItemSize_ID SMALLINT NULL,
		Opening_Quantity DECIMAL NOT NULL,
		Opening_Value DECIMAL NOT NULL,
		Minimum_Quantity DECIMAL NOT NULL,
		Maximum_Quantity DECIMAL NOT NULL,
		Sale_Rate DECIMAL NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Inv_Item_Detail] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		Item_Detail_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='IX_Comapny_Color_ItemGrade_ItemSize')
	ALTER TABLE Inv_Item_Detail
		ADD CONSTRAINT [IX_Comapny_Color_ItemGrade_ItemSize] UNIQUE
		(
		Item_ID ASC,
		Co_ID ASC,
		Color_ID ASC,
		ItemGrade_ID ASC,
		ItemSize_ID ASC
		)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Inv_Item_Detail_Invs_Item')
	ALTER TABLE Inv_Item_Detail
		WITH CHECK ADD  CONSTRAINT FK_Inv_Item_Detail_Invs_Item
		FOREIGN KEY([Co_ID], Item_ID)
		REFERENCES Invs_Item ([Co_ID], Item_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Inv_Item_Detail_Common_Color')
	ALTER TABLE Inv_Item_Detail
		WITH CHECK ADD  CONSTRAINT FK_Inv_Item_Detail_Common_Color
		FOREIGN KEY([Co_ID], Color_ID)
		REFERENCES Common_Color ([Co_ID], [Color_ID])
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Inv_Item_Detail_Inv_ItemGrade')
	ALTER TABLE Inv_Item_Detail
		WITH CHECK ADD  CONSTRAINT FK_Inv_Item_Detail_ItemGrade
		FOREIGN KEY([Co_ID], ItemGrade_ID)
		REFERENCES Inv_ItemGrade ([Co_ID], ItemGrade_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Inv_Item_Detail_Inv_ItemSize')
	ALTER TABLE Inv_Item_Detail
		WITH CHECK ADD  CONSTRAINT FK_Inv_Item_Detail_Inv_ItemSize
		FOREIGN KEY([Co_ID], ItemSize_ID)
		REFERENCES Inv_ItemSize ([Co_ID], ItemSize_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_Formula')
	CREATE TABLE Production_Formula(
		Co_ID SMALLINT NOT NULL,
		Formula_ID INT NOT NULL,
		Formula_Code VARCHAR(50) NOT NULL,
		Formula_Description VARCHAR(500) NOT NULL,
		Output_Item_Detail_ID INT NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_Formula] PRIMARY KEY CLUSTERED
	(
		Co_ID ASC,
		Formula_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_Formula_Detail')
	CREATE TABLE Production_Formula_Detail(
		Co_ID SMALLINT NOT NULL,
		Formula_ID INT NOT NULL,
		Formula_Detail_ID INT NOT NULL,
		Remarks VARCHAR(500) NOT NULL,
		Input_Item_Detail_ID INT NOT NULL,
		Quantity DECIMAL NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_Formula_Detail] PRIMARY KEY CLUSTERED
	(
		Co_ID ASC,
		Formula_ID ASC,
		Formula_Detail_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_Formula_Formula_Detail')
	ALTER TABLE Production_Formula_Detail
		WITH CHECK ADD  CONSTRAINT FK_Production_Formula_Formula_Detail
		FOREIGN KEY(Co_ID, Formula_ID)
		REFERENCES Production_Formula (Co_ID, Formula_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_Order')
	CREATE TABLE Production_Order(
		Co_ID SMALLINT NOT NULL,
		Order_ID INT NOT NULL,
		Order_No VARCHAR(50) NOT NULL,
		Order_Date DATETIME NOT NULL,
		Remarks VARCHAR(1000) NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_Order] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		Order_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_Order_Detail')
	CREATE TABLE Production_Order_Detail(
		Co_ID SMALLINT NOT NULL,
		Order_ID INT NOT NULL,
		Order_Detail_ID INT NOT NULL,
		Item_Detail_ID INT NOT NULL,
		Formula_ID INT NOT NULL,
		Quantity DECIMAL NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_Order_Detail] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		Order_ID ASC,
		Order_Detail_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_Order_Order_Detail')
	ALTER TABLE Production_Order_Detail
		WITH CHECK ADD  CONSTRAINT FK_Production_Order_Order_Detail
		FOREIGN KEY(Co_ID, Order_ID)
		REFERENCES Production_Order (Co_ID, Order_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_Formula_Order_Detail')
	ALTER TABLE Production_Order_Detail
		WITH CHECK ADD  CONSTRAINT FK_Production_Formula_Order_Detail
		FOREIGN KEY(Co_ID, Formula_ID)
		REFERENCES Production_Formula (Co_ID, Formula_ID)
go
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 800)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(800, 'Production', '08', 8, 0
		, 0, '2010-11-11 22:02', NULL, 1, 0)
go
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 801)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(801, 'Process', '08-001', 1, 800
		, 0, '2010-11-11 22:02', NULL, 1, 0)
go
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 802)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(802, 'Process Workflow', '08-002', 2, 800
		, 0, '2010-11-11 22:02', NULL, 1, 0)
go
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 803)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(803, 'Production Order', '08-003', 3, 800
		, 0, '2010-11-11 22:02', NULL, 1, 0)
go
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 804)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(804, 'Process Production', '08-004', 4, 800
		, 0, '2010-11-11 22:02', NULL, 1, 0)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_OrderBatch')
	CREATE TABLE Production_OrderBatch(
		Co_ID SMALLINT NOT NULL,
		Order_ID INT NOT NULL,
		OrderBatch_ID INT NOT NULL,
		OrderBatch_No VARCHAR(50) NOT NULL,
		OrderBatch_Date DATETIME NOT NULL,
		Remarks VARCHAR(1000) NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_OrderBatch] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		OrderBatch_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_Order_OrderBatch')
	ALTER TABLE Production_OrderBatch
		WITH CHECK ADD  CONSTRAINT FK_Production_Order_OrderBatch
		FOREIGN KEY(Co_ID, Order_ID)
		REFERENCES Production_Order (Co_ID, Order_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_OrderBatch_Detail')
	CREATE TABLE Production_OrderBatch_Detail(
		Co_ID SMALLINT NOT NULL,
		OrderBatch_ID INT NOT NULL,
		OrderBatch_Detail_ID INT NOT NULL,
		Item_Detail_ID INT NOT NULL,
		Quantity DECIMAL NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_OrderBatch_Detail] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		OrderBatch_ID ASC,
		OrderBatch_Detail_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Production_OrderBatch_OrderBatch_Detail')
	ALTER TABLE Production_OrderBatch_Detail
		WITH CHECK ADD  CONSTRAINT FK_Production_OrderBatch_OrderBatch_Detail
		FOREIGN KEY(Co_ID, OrderBatch_ID)
		REFERENCES Production_OrderBatch (Co_ID, OrderBatch_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_ProcessProduction')
	CREATE TABLE Production_ProcessProduction(
		Co_ID SMALLINT NOT NULL,
		Production_ID INT NOT NULL,
		Production_No VARCHAR(50) NOT NULL,
		Production_Date DATETIME NOT NULL,
		Order_ID INT NULL,
		OrderBatch_ID INT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Production_ProcessProduction] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		Production_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Order_ProcessProduction')
	ALTER TABLE Production_ProcessProduction
		WITH CHECK ADD  CONSTRAINT FK_Order_ProcessProduction
		FOREIGN KEY([Co_ID], Order_ID)
		REFERENCES Production_Order ([Co_ID], Order_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_OrderBatch_ProcessProduction')
	ALTER TABLE Production_ProcessProduction
		WITH CHECK ADD  CONSTRAINT FK_OrderBatch_ProcessProduction
		FOREIGN KEY([Co_ID], OrderBatch_ID)
		REFERENCES Production_OrderBatch ([Co_ID], OrderBatch_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Production_ProcessProduction_Detail')
	CREATE TABLE Production_ProcessProduction_Detail(
		Co_ID SMALLINT NOT NULL,
		Production_ID INT NOT NULL,
		Production_Detail_ID INT NOT NULL,
		Item_Detail_ID INT NOT NULL,
		Quantity DECIMAL NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_ProcessProduction_Detail] PRIMARY KEY CLUSTERED
	(
		[Co_ID] ASC,
		Production_ID ASC,
		Production_Detail_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_ProcessProduction_ProcessProduction_Detail')
	ALTER TABLE Production_ProcessProduction_Detail
		WITH CHECK ADD  CONSTRAINT FK_ProcessProduction_ProcessProduction_Detail
		FOREIGN KEY([Co_ID], Production_ID)
		REFERENCES Production_ProcessProduction ([Co_ID], Production_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] ='Invs_Item' AND c.[Name] = 'Is_RawMaterial')
	ALTER TABLE Invs_Item
		ADD Is_RawMaterial BIT NOT NULL
go
IF NOT EXISTS (SELECT * FROM Base_Setting WHERE Setting_ID = 'DocumentNoFormat.ProductionOrder')
	INSERT INTO [Base_Setting]
		([Co_Id], [User_Id], [Setting_Id], [Setting_Desc]
		,[Setting_Value], [Stamp_User_Id], [Stamp_DateTime]
		,[Setting_Value_DataType], [Setting_Value_MinimumValue], [Setting_Value_MaximumValue]
		,[Upload_DateTime], [RecordStatus_ID])
	VALUES
		(0, 0, 'DocumentNoFormat.ProductionOrder'
		, 'Document No format used on production order form'
		, '999999', 0, '2010-11-19'
		, 'String', NULL, NULL
		, NULL, 1)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Common_Country')
	CREATE TABLE Common_Country(
		Country_ID INT NOT NULL,
		Country_Code VARCHAR(10) NOT NULL,
		Country_Desc VARCHAR(250) NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Common_Country] PRIMARY KEY CLUSTERED
	(
		Country_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Common_City')
	CREATE TABLE Common_City(
		Country_ID INT NOT NULL,
		City_ID INT NOT NULL,
		City_Code VARCHAR(10) NOT NULL,
		City_Desc VARCHAR(250) NOT NULL,
		[Stamp_UserID] INT NOT NULL,
		[Stamp_DateTime] DATETIME NOT NULL,
		[Upload_DateTime] DATETIME NULL,
		[RecordStatus_ID] INT NOT NULL,
	 CONSTRAINT [PK_Common_City] PRIMARY KEY CLUSTERED
	(
		Country_ID ASC,
		City_ID ASC
	))
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Country_City')
	ALTER TABLE Common_City
		WITH CHECK ADD  CONSTRAINT FK_Country_City
		FOREIGN KEY(Country_ID)
		REFERENCES Common_Country (Country_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] ='Sec_User' AND c.[Name] = 'Country_ID')
	ALTER TABLE Sec_User
		ADD Country_ID INT NULL
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] ='Sec_User' AND c.[Name] = 'City_ID')
	ALTER TABLE Sec_User
		ADD City_ID INT NULL
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_Country_Sec_User')
	ALTER TABLE Sec_User
		WITH CHECK ADD  CONSTRAINT FK_Country_Sec_User
		FOREIGN KEY(Country_ID)
		REFERENCES Common_Country (Country_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='FK_City_Sec_User')
	ALTER TABLE Sec_User
		WITH CHECK ADD  CONSTRAINT FK_City_Sec_User
		FOREIGN KEY(Country_ID, City_ID)
		REFERENCES Common_City (Country_ID, City_ID)
go
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] ='Sec_User' AND c.[Name] = 'Short_Name')
	ALTER TABLE Sec_User
		ADD Short_Name VARCHAR(20) NULL
/*
INSERT INTO [Inv_Item_Detail]
           ([Co_ID], [Item_Detail_ID], [Item_ID], [Color_ID], [ItemGrade_ID], [ItemSize_ID]
           ,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID])
     VALUES
           (21, 1, 1, NULL, NULL, 1
           ,0, '2010-11-01', NULL, 1)
INSERT INTO [Inv_Item_Detail]
           ([Co_ID], [Item_Detail_ID], [Item_ID], [Color_ID], [ItemGrade_ID], [ItemSize_ID]
           ,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID])
     VALUES
           (21, 2, 1, NULL, NULL, 2
           ,0, '2010-11-01', NULL, 1)
INSERT INTO [Inv_Item_Detail]
           ([Co_ID], [Item_Detail_ID], [Item_ID], [Color_ID], [ItemGrade_ID], [ItemSize_ID]
           ,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID])
     VALUES
           (21, 3, 100, NULL, NULL, 1
           ,0, '2010-11-01', NULL, 1)
INSERT INTO [Production_Formula]
           ([Co_ID], [Formula_ID], [Formula_Code], [Formula_Description], [Output_Item_Detail_ID]
           ,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID])
     VALUES
           (21, 1, '01', 'Test', 1
           , 0, '2010-01-01', NULL, 1)
INSERT INTO [Production_Formula_Detail]
           ([Co_ID], [Formula_ID], [Formula_Detail_ID], [Remarks], [Input_Item_Detail_ID]
           ,[Quantity], [Stamp_UserID], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID])
     VALUES
           (21, 1, 1, 'remarks 1', 3
           ,3, 0, '2010-11-01', NULL, 1)

*/