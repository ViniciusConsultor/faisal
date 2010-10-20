-- ======================================================================
-- Author:		Faisal Saleem
-- Create date: 29-Aug-10
-- Description:	This procedure returns the minimum stoc level information.
-- ======================================================================
-- ------------ Modification History ------------------------------------
-- Author	Date		Details
-- -------- ---------	-------
-- Faisal	05-Sep-10	Added two columns Warehouse_ID and Warehouse_Name
-- Faisal	20-Oct-10	Added where condition for companies.
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
		AND (it.Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_4) OR @Companies = '')
		AND its.Source_DocumentType_ID = 15	--15=Stock
		AND its.Warehouse_ID = @WarehouseID
	ORDER BY it.Item_Code, c.Co_Code
END
GO
-- ======================================================================
-- Author:		Faisal Saleem
-- Create date: 05-Sep-10
-- Description:	This procedure returns item stock.
-- ======================================================================
-- ------------ Modification History ------------------------------------
-- Author	Date		Details
-- ------   ----        -------
-- Faisal	20-Oct-10	Added @AddTotalRows parameter so that user can 
--						choose to display total rows.
-- ----------------------------------------------------------------------
ALTER PROC [dbo].[spGetStockByItemCodeCompanies]
	@ItemCode VARCHAR(20)
	, @Companies VARCHAR(8000)
	, @WarehouseID INT
	, @AddTotalRows BIT
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
		AND (@AddTotalRows = 1)

GROUP BY itsum.Warehouse_ID, Warehouse_Name, Item_Code

ORDER BY Item_Code, Co_ID
