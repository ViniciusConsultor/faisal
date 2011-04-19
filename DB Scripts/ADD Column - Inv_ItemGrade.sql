-- Add Column
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o 
	INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] = 'Inv_ItemGrade' AND c.[Name] = 'Inactive_From')
	ALTER TABLE Inv_ItemGrade
		ADD Inactive_From DateTime NULL
Go

IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o 
	INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] = 'Inv_ItemGrade' AND c.[Name] = 'Inactive_To')
	ALTER TABLE Inv_ItemGrade
		ADD Inactive_To DateTime NULL

Go 
