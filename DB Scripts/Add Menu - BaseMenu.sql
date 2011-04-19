IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 308)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(308, 'Menu Setting', '03-008', 8, 300,
		1, '2011-04-12', NULL, 1, 28)
Go

IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 309)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(309, 'Form Setting', '03-009', 9, 300,
		 1, '2011-04-12', NULL, 1, 29)

GO
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 111)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(111, 'Item Size', '01-111', 11, 100,
		 1, '2011-04-12', NULL, 1, 30)

GO
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 112)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(112, 'Item Grade', '01-112', 12, 100,
		 1, '2011-04-12', NULL, 1, 31)

GO
IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 202)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(202, 'Color', '02-002', 9, 200,
		 1, '2011-04-12', NULL, 1, 32)
Go