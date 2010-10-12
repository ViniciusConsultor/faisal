IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Accounting_CashFlowAccount' AND c.[Name]='Co_ID')
	BEGIN
	TRUNCATE TABLE Accounting_CashFlowAccount
	ALTER TABLE Accounting_CashFlowAccount ADD Co_ID SMALLINT NOT NULL
	ALTER TABLE Accounting_CashFlowAccount
		DROP CONSTRAINT PK_Accounting_CashFlowAccount
	ALTER TABLE Accounting_CashFlowAccount ADD CONSTRAINT
		PK_Accounting_CashFlowAccount PRIMARY KEY CLUSTERED 
		(
		CO_ID,
		CashFlowAccount_ID
		)
	END
go
IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Accounting_FinancialAccountType' AND c.[Name]='Co_ID')
	BEGIN
	TRUNCATE TABLE Accounting_FinancialAccountType
	ALTER TABLE Accounting_FinancialAccountType ADD Co_ID SMALLINT NOT NULL
	ALTER TABLE Accounting_FinancialAccountType
		DROP CONSTRAINT PK_Accounting_FinancialAccountType
	ALTER TABLE Accounting_FinancialAccountType ADD CONSTRAINT
		PK_Accounting_FinancialAccountType PRIMARY KEY CLUSTERED 
		(
		CO_ID,
		FinancialAccountType_ID
		)
	END
