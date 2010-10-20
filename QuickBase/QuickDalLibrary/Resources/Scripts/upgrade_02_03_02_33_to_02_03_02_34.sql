IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-001')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (1
			   ,'01-001'
			   ,'ItemForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-002')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (2
			   ,'01-002'
			   ,'SalesInvoiceForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-003')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (3
			   ,'01-003'
			   ,'SalesInvoicePosForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-004')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (4
			   ,'01-004'
			   ,'Purchase'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-005')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (5
			   ,'01-005'
			   ,'PurchaseReturn'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-006')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (6
			   ,'01-006'
			   ,'PurchaseOrderForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-007')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (7
			   ,'01-007'
			   ,'MinimumStockLevel'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-008')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (8
			   ,'01-008'
			   ,'SalesInvoiceReturn'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-009')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (9
			   ,'01-009'
			   ,'PurchaseWarehouseForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '01-010')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (10
			   ,'01-010'
			   ,'StockInquiryForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '02-001')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (11
			   ,'02-001'
			   ,'PartyRegularForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '03-001')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (12
			   ,'03-001'
			   ,'ImportFromExcel'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '03-002')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (13
			   ,'03-002'
			   ,'BranchForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '03-003')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (14
			   ,'03-003'
			   ,'SecurityUserForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '03-004')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (15
			   ,'03-004'
			   ,'DBConnectivityForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '03-005')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (16
			   ,'03-005'
			   ,'frmTransferData'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '03-006')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (17
			   ,'03-006'
			   ,'EmptyDatabaseForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '04-001')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (18
			   ,'04-001'
			   ,'Receipt'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '04-002')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (19
			   ,'04-002'
			   ,'Payment'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '04-003')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (20
			   ,'04-003'
			   ,'COAForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '04-004')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (21
			   ,'04-004'
			   ,'VoucherTypeForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '04-005')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (22
			   ,'04-005'
			   ,'VoucherEntryForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '05-001')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (23
			   ,'05-001'
			   ,'ReportCriteriaForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '06-001')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (24
			   ,'06-001'
			   ,'MenuRoleAssociationForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '06-002')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (25
			   ,'06-002'
			   ,'SecurityUserForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_SettingForm] WHERE [Form_Code] = '06-003')
	INSERT INTO [Base_SettingForm]
			   ([Form_ID]
			   ,[Form_Code]
			   ,[Form_Name]
			   ,[RecordStatus_ID]
			   ,[Stamp_UserID]
			   ,[Stamp_DateTime]
			   ,[Upload_DateTime])
		 VALUES
			   (26
			   ,'06-003'
			   ,'SecurityRoleForm'
			   ,1
			   ,0
			   ,'2010-10-16'
			   ,NULL)
go
IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Base_Menu' AND c.[Name]='Form_ID')
	ALTER TABLE Base_Menu ADD Form_ID SMALLINT NULL
go
UPDATE Base_Menu SET Form_ID = (SELECT Form_ID FROM Base_SettingForm WHERE Form_Code = Base_Menu.Form_Code)
	WHERE Form_ID IS NULL
go
UPDATE Base_Menu SET Form_ID = 0
	WHERE Form_ID IS NULL
go

--truncate table base_settingform

