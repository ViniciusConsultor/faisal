IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 308)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(308, 'Menu Setting', '03-008',8,300
		, 1, '2011-04-12', NULL, 1,0 )

Go

IF NOT EXISTS (SELECT * FROM Base_Menu WHERE Menu_ID = 309)
	INSERT INTO [Base_Menu]
		([Menu_Id], [Menu_Desc], [Form_Code], [Display_Order], [Parent_Menu_Id]
		,[Stamp_UserId], [Stamp_DateTime], [Upload_DateTime], [RecordStatus_ID], [Form_ID])
	VALUES
		(309, 'Form Setting', '03-008',9,300
		, 1, '2011-04-12', NULL, 1,0)