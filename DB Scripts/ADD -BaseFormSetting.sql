IF NOT EXISTS (SELECT * FROM Base_SettingForm WHERE Form_ID = 28)
	INSERT INTO [Base_SettingForm]
		([Form_ID], [Form_Code], [Form_Name], [RecordStatus_ID]
		,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime])
	VALUES
		(28, '03-008', 'MenuSetting', 1
		,0, '2011/01/01', NULL)
Go
IF NOT EXISTS (SELECT * FROM Base_SettingForm WHERE Form_ID = 29)
	INSERT INTO [Base_SettingForm]
		([Form_ID], [Form_Code], [Form_Name], [RecordStatus_ID]
		,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime])
	VALUES
		(29, '03-009', 'FormSetting', 1
		,0, '2011/01/01', NULL)
Go
IF NOT EXISTS (SELECT * FROM Base_SettingForm WHERE Form_ID = 30)
	INSERT INTO [Base_SettingForm]
		([Form_ID], [Form_Code], [Form_Name], [RecordStatus_ID]
		,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime])
	VALUES
		(30, '01-111', 'ItemSizeForm', 1
		,0, '2011/01/01', NULL)
Go
IF NOT EXISTS (SELECT * FROM Base_SettingForm WHERE Form_ID = 31)
	INSERT INTO [Base_SettingForm]
		([Form_ID], [Form_Code], [Form_Name], [RecordStatus_ID]
		,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime])
	VALUES
		(31, '01-112', 'ItemGradeForm', 1
		,0, '2011/01/01', NULL)

Go
IF NOT EXISTS (SELECT * FROM Base_SettingForm WHERE Form_ID = 32)
	INSERT INTO [Base_SettingForm]
		([Form_ID], [Form_Code], [Form_Name], [RecordStatus_ID]
		,[Stamp_UserID], [Stamp_DateTime], [Upload_DateTime])
	VALUES
		(32, '02-002', 'ColorForm', 1
		,0, '2011/01/01', NULL)

