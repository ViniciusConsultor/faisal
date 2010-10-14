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
go
-- ======================================================================
-- Author:		Faisal Saleem
-- Create date: 05-Sep-10
-- Description:	This procedure returns item stock.
-- ======================================================================
-- ------------ Modification History ------------------------------------
-- Author	Date		Details
-- ------   ----        -------
-- ----------------------------------------------------------------------
ALTER PROC [dbo].[spGetStockByItemCodeCompanies]
	@ItemCode VARCHAR(20)
	, @Companies VARCHAR(8000)
	, @WarehouseID INT
AS
SELECT itsum.Co_ID, Co_Code, itsum.Warehouse_ID, Warehouse_Name, it.Item_ID, Item_Code, dbo.fnGetItemCategoryDesc(itsum.Co_ID, Item_Code) AS Item_Category, Item_Desc
	, ItemSummary_Size01 AS Qty_Size01, ItemSummary_Size02 AS Qty_Size02, ItemSummary_Size03 AS Qty_Size03, ItemSummary_Size04 AS Qty_Size04
	, ItemSummary_Size05 AS Qty_Size05, ItemSummary_Size06 AS Qty_Size06, ItemSummary_Size07 AS Qty_Size07, ItemSummary_Size08 AS Qty_Size08
	, ItemSummary_Size09 AS Qty_Size09, ItemSummary_Size10 AS Qty_Size10, ItemSummary_Size11 AS Qty_Size11, ItemSummary_Size12 AS Qty_Size12
	, ItemSummary_Size13 AS Qty_Size13
	, ItemSummary_Size01 + ItemSummary_Size02 + ItemSummary_Size03 + ItemSummary_Size04 + ItemSummary_Size05 
		+ ItemSummary_Size06 + ItemSummary_Size07 + ItemSummary_Size08 + ItemSummary_Size09 + ItemSummary_Size10 
		+ ItemSummary_Size11 + ItemSummary_Size12 + ItemSummary_Size13 AS Qty_Total 
FROM Inv_ItemSummary itsum
	INNER JOIN Inv_Item it ON itsum.Co_ID = it.Co_ID AND itsum.Source_First_ID = it.Item_ID AND itsum.Source_DocumentType_ID = 15 --stock
	INNER JOIN Base_Company co ON itsum.Co_ID = co.Co_ID
	LEFT JOIN Inv_Warehouse wh ON itsum.Co_ID = wh.Co_ID AND itsum.Warehouse_ID = wh.Warehouse_ID
WHERE (Item_Code = @ItemCode OR @ItemCode = '') 
		AND (itsum.Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_4) OR @Companies = '') 
		AND (itsum.Warehouse_ID = @WarehouseID)

UNION ALL

SELECT 0 AS Co_ID, '' AS Co_Code, itsum.Warehouse_ID, Warehouse_Name, 0 AS Item_ID, Item_Code, '' AS Item_Category, 'Total' AS Item_Desc
	, SUM(ItemSummary_Size01) AS Qty_Size01, SUM(ItemSummary_Size02) AS Qty_Size02, SUM(ItemSummary_Size03) AS Qty_Size03
	, SUM(ItemSummary_Size04) AS Qty_Size04, SUM(ItemSummary_Size05) AS Qty_Size05, SUM(ItemSummary_Size06) AS Qty_Size06
	, SUM(ItemSummary_Size07) AS Qty_Size07, SUM(ItemSummary_Size08) AS Qty_Size08, SUM(ItemSummary_Size09) AS Qty_Size09
	, SUM(ItemSummary_Size10) AS Qty_Size10, SUM(ItemSummary_Size11) AS Qty_Size11, SUM(ItemSummary_Size12) AS Qty_Size12
	, SUM(ItemSummary_Size13) AS Qty_Size13
	, SUM(ItemSummary_Size01 + ItemSummary_Size02 + ItemSummary_Size03 + ItemSummary_Size04 + ItemSummary_Size05 
		+ ItemSummary_Size06 + ItemSummary_Size07 + ItemSummary_Size08 + ItemSummary_Size09 + ItemSummary_Size10 
		+ ItemSummary_Size11 + ItemSummary_Size12 + ItemSummary_Size13) AS Qty_Total 
FROM Inv_ItemSummary AS itsum
	INNER JOIN Inv_Item it ON itsum.Co_ID = it.Co_ID AND itsum.Source_First_ID = it.Item_ID AND itsum.Source_DocumentType_ID = 15 --stock
	INNER JOIN Base_Company co ON co.Co_ID = itsum.Co_ID
	LEFT JOIN Inv_Warehouse wh ON itsum.Co_ID = wh.Co_ID AND itsum.Warehouse_ID = wh.Warehouse_ID
WHERE (Item_Code = @ItemCode OR @ItemCode = '') 
		AND (itsum.Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_4) OR @Companies = '') 
		AND (itsum.Warehouse_ID = @WarehouseID)

GROUP BY itsum.Warehouse_ID, Warehouse_Name, Item_Code

ORDER BY Item_Code, Co_ID
go
-- ======================================================================
-- Author:		Faisal Saleem
-- Create date: 29-Aug-10
-- Description:	This procedure returns the minimum stoc level information.
-- ======================================================================
-- ------------ Modification History ------------------------------------
-- Author	Date		Details
-- -------- ---------	-------
-- Faisal	05-Sep-10	Added two columns Warehouse_ID and Warehouse_Name
-- ----------------------------------------------------------------------
ALTER PROC [dbo].[spGetMinimumStockLevelByItemCodeCompanies]
	@ItemCode AS VARCHAR(50)
	, @Companies AS VARCHAR(8000)
	, @WarehouseID INT
AS
BEGIN
	SELECT it.Co_ID
		, c.Co_Code
		, its.Warehouse_ID
		, wh.Warehouse_Name
		, it.Item_ID
		, Item_Code
		, dbo.fnGetItemCategoryDesc(it.Co_ID, Item_Code) AS Item_Category
		, Item_Desc
		, ISNULL(ItemSummary_Size01, 0) - ISNULL(Item_MinStock_Size0, 0) AS Qty_Size01
		, ISNULL(ItemSummary_Size02, 0) - ISNULL(Item_MinStock_Size1, 0) AS Qty_Size02
		, ISNULL(ItemSummary_Size03, 0) - ISNULL(Item_MinStock_Size2, 0) AS Qty_Size03
		, ISNULL(ItemSummary_Size04, 0) - ISNULL(Item_MinStock_Size3, 0) AS Qty_Size04
		, ISNULL(ItemSummary_Size05, 0) - ISNULL(Item_MinStock_Size4, 0) AS Qty_Size05
		, ISNULL(ItemSummary_Size06, 0) - ISNULL(Item_MinStock_Size5, 0) AS Qty_Size06
		, ISNULL(ItemSummary_Size07, 0) - ISNULL(Item_MinStock_Size6, 0) AS Qty_Size07
		, ISNULL(ItemSummary_Size08, 0) - ISNULL(Item_MinStock_Size7, 0) AS Qty_Size08
		, ISNULL(ItemSummary_Size09, 0) - ISNULL(Item_MinStock_Size8, 0) AS Qty_Size09
		, ISNULL(ItemSummary_Size10, 0) - ISNULL(Item_MinStock_Size9, 0) AS Qty_Size10
		, ISNULL(ItemSummary_Size11, 0) - ISNULL(Item_MinStock_Size10, 0) AS Qty_Size11
		, ISNULL(ItemSummary_Size12, 0) - ISNULL(Item_MinStock_Size11, 0) AS Qty_Size12
		, ISNULL(ItemSummary_Size13, 0) - ISNULL(Item_MinStock_Size12, 0) AS Qty_Size13
		, ISNULL(ItemSummary_Size01, 0) - ISNULL(Item_MinStock_Size0, 0)
			+ ISNULL(ItemSummary_Size02, 0) - ISNULL(Item_MinStock_Size1, 0)
			+ ISNULL(ItemSummary_Size03, 0) - ISNULL(Item_MinStock_Size2, 0)
			+ ISNULL(ItemSummary_Size04, 0) - ISNULL(Item_MinStock_Size3, 0)
			+ ISNULL(ItemSummary_Size05, 0) - ISNULL(Item_MinStock_Size4, 0)
			+ ISNULL(ItemSummary_Size06, 0) - ISNULL(Item_MinStock_Size5, 0)
			+ ISNULL(ItemSummary_Size07, 0) - ISNULL(Item_MinStock_Size6, 0)
			+ ISNULL(ItemSummary_Size08, 0) - ISNULL(Item_MinStock_Size7, 0)
			+ ISNULL(ItemSummary_Size09, 0) - ISNULL(Item_MinStock_Size8, 0)
			+ ISNULL(ItemSummary_Size10, 0) - ISNULL(Item_MinStock_Size9, 0)
			+ ISNULL(ItemSummary_Size11, 0) - ISNULL(Item_MinStock_Size10, 0)
			+ ISNULL(ItemSummary_Size12, 0) - ISNULL(Item_MinStock_Size11, 0)
			+ ISNULL(ItemSummary_Size13, 0) - ISNULL(Item_MinStock_Size12, 0) AS Qty_Total
	FROM Inv_Item it
		LEFT JOIN Inv_ItemSummary its ON it.Co_ID = its.Co_ID AND it.Item_ID = its.Source_First_ID
		INNER JOIN Base_Company c ON it.Co_ID = c.Co_ID
		LEFT JOIN Inv_Warehouse wh ON its.Warehouse_ID = wh.Warehouse_ID
	WHERE (it.Item_Code = @ItemCode OR @ItemCode = '')
		AND its.Source_DocumentType_ID = 15	--15=Stock
		AND its.Warehouse_ID = @WarehouseID
	ORDER BY it.Item_Code, c.Co_Code
END
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size01')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size01'
           ,'Qty_Size01 Caption'
           ,'100'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size02')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size02'
           ,'Qty_Size02 Caption'
           ,'110'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size03')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size03'
           ,'Qty_Size03 Caption'
           ,'120'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size04')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size04'
           ,'Qty_Size04 Caption'
           ,'130'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size05')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size05'
           ,'Qty_Size05 Caption'
           ,'140'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size06')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size06'
           ,'Qty_Size06 Caption'
           ,'150'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size07')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size07'
           ,'Qty_Size07 Caption'
           ,'160'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size08')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size08'
           ,'Qty_Size08 Caption'
           ,'170'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size09')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size09'
           ,'Qty_Size09 Caption'
           ,'180'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size10')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size10'
           ,'Qty_Size10 Caption'
           ,'190'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size11')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size11'
           ,'Qty_Size11 Caption'
           ,'CM'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size12')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size12'
           ,'Qty_Size12 Caption'
           ,'210'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
go
IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id] = 'DBColumnCaptionQty_Size13')
	INSERT INTO [Base_Setting]
           ([Co_Id]
           ,[User_Id]
           ,[Setting_Id]
           ,[Setting_Desc]
           ,[Setting_Value]
           ,[Stamp_User_Id]
           ,[Stamp_DateTime]
           ,[Setting_Value_DataType]
           ,[Setting_Value_MinimumValue]
           ,[Setting_Value_MaximumValue]
           ,[Upload_DateTime]
           ,[RecordStatus_ID])
     VALUES
           (0
           ,0
           ,'DBColumnCaptionQty_Size13'
           ,'Qty_Size13 Caption'
           ,'220'
           ,0
           ,'2010-10-14 1:38'
           ,'String'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
