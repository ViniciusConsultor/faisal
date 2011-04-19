-- Add Column
IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o
	 INNER JOIN syscolumns c ON o.id = c.id WHERE o.[Name] ='Common_Color' AND c.[Name] = 'ColorValue')
	ALTER TABLE Common_Color
		ADD ColorValue INT NULL
Go
