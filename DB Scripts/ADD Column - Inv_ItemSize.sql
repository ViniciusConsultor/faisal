-- Add Column
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] = 'TableName' AND c.[Name] = 'ColumnName')
	ALTER TABLE Inv_ItemSize
		ADD Inactive_From DateTime NULL
Go

IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] = 'TableName' AND c.[Name] = 'ColumnName')
	ALTER TABLE Inv_ItemSize
		ADD Inactive_To DateTime NULL

Go 
