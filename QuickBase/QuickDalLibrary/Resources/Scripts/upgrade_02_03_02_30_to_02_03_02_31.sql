IF EXISTS (SELECT O.[NAME] FROM sysobjects o WHERE o.[Name] ='vwStockInquiry')
	DROP VIEW vwStockInquiry
go
CREATE VIEW vwStockInquiry
AS
SELECT     Co_ID, Co_Code, Warehouse_ID, ISNULL(Warehouse_Name, 'Default') AS Warehouse_Name, Item_ID, Item_Code, dbo.fnGetItemCategoryDesc(Co_ID, Item_Code) AS Item_Category, Item_Desc, SUM(Inventory_Qty_Size01) AS Inventory_Qty_Size01
	, SUM(Inventory_Qty_Size02) AS Inventory_Qty_Size02, SUM(Inventory_Qty_Size03) AS Inventory_Qty_Size03
	, SUM(Inventory_Qty_Size04) AS Inventory_Qty_Size04, SUM(Inventory_Qty_Size05) AS Inventory_Qty_Size05
	, SUM(Inventory_Qty_Size06) AS Inventory_Qty_Size06, SUM(Inventory_Qty_Size07) AS Inventory_Qty_Size07
	, SUM(Inventory_Qty_Size08) AS Inventory_Qty_Size08, SUM(Inventory_Qty_Size09) AS Inventory_Qty_Size09
	, SUM(Inventory_Qty_Size10) AS Inventory_Qty_Size10, SUM(Inventory_Qty_Size11) AS Inventory_Qty_Size11
	, SUM(Inventory_Qty_Size12) AS Inventory_Qty_Size12, SUM(Inventory_Qty_Size13) AS Inventory_Qty_Size13
FROM 
	(SELECT     inv.Co_ID, Co.Co_Code, invdet.Item_ID, item.Item_Code, item.Item_Desc, invdet.Warehouse_ID, wh.Warehouse_Name
		, - invdet.Inventory_Qty_Size01 AS Inventory_Qty_Size01, - invdet.Inventory_Qty_Size02 AS Inventory_Qty_Size02
		, - invdet.Inventory_Qty_Size03 AS Inventory_Qty_Size03, - invdet.Inventory_Qty_Size04 AS Inventory_Qty_Size04
		, - invdet.Inventory_Qty_Size05 AS Inventory_Qty_Size05, - invdet.Inventory_Qty_Size06 AS Inventory_Qty_Size06
		, - invdet.Inventory_Qty_Size07 AS Inventory_Qty_Size07, - invdet.Inventory_Qty_Size08 AS Inventory_Qty_Size08
		, - invdet.Inventory_Qty_Size09 AS Inventory_Qty_Size09, - invdet.Inventory_Qty_Size10 AS Inventory_Qty_Size10
		, - invdet.Inventory_Qty_Size11 AS Inventory_Qty_Size11, - invdet.Inventory_Qty_Size12 AS Inventory_Qty_Size12
		, - invdet.Inventory_Qty_Size13 AS Inventory_Qty_Size13
	FROM	dbo.Inv_Item AS item  RIGHT OUTER JOIN
		Inventory_Detail AS invdet ON item.Co_ID = invdet.Co_ID AND item.Item_ID = invdet.Item_ID INNER JOIN
		Inventory AS inv ON inv.Co_ID = invdet.Co_ID AND inv.Inventory_ID = invdet.Inventory_ID LEFT OUTER JOIN
		Party ON inv.Co_ID = dbo.Party.Co_ID AND inv.Party_ID = dbo.Party.Party_ID LEFT OUTER JOIN
		Base_Company AS Co ON inv.Co_ID = Co.Co_Id LEFT OUTER JOIN
		Common_DocumentType AS doc ON invdet.DocumentType_ID = doc.DocumentType_ID
		LEFT JOIN Inv_Warehouse AS wh ON invdet.Co_ID = wh.Co_ID AND invdet.Warehouse_ID = wh.Warehouse_ID
	WHERE      (inv.RecordStatus_ID <> 4) AND (invdet.DocumentType_ID IN (1, 4))
	
	UNION ALL

	SELECT     inv.Co_ID, Co.Co_Code, invdet.Item_ID, item.Item_Code, item.Item_Desc, invdet.Warehouse_ID, wh.Warehouse_Name, 
		invdet.Inventory_Qty_Size01, invdet.Inventory_Qty_Size02, invdet.Inventory_Qty_Size03, invdet.Inventory_Qty_Size04, 
		invdet.Inventory_Qty_Size05, invdet.Inventory_Qty_Size06, invdet.Inventory_Qty_Size07, invdet.Inventory_Qty_Size08, 
		invdet.Inventory_Qty_Size09, invdet.Inventory_Qty_Size10, invdet.Inventory_Qty_Size11, invdet.Inventory_Qty_Size12, 
		invdet.Inventory_Qty_Size13
	FROM	dbo.Inv_Item AS item RIGHT OUTER JOIN
		dbo.Inventory_Detail AS invdet ON item.Co_ID = invdet.Co_ID AND item.Item_ID = invdet.Item_ID INNER JOIN
		dbo.Inventory AS inv ON inv.Co_ID = invdet.Co_ID AND inv.Inventory_ID = invdet.Inventory_ID LEFT OUTER JOIN
		dbo.Party AS Party_1 ON inv.Co_ID = Party_1.Co_ID AND inv.Party_ID = Party_1.Party_ID LEFT OUTER JOIN
		dbo.Base_Company AS Co ON inv.Co_ID = Co.Co_Id LEFT OUTER JOIN
		dbo.Common_DocumentType AS doc ON invdet.DocumentType_ID = doc.DocumentType_ID
		LEFT JOIN Inv_Warehouse AS wh ON invdet.Co_ID = wh.Co_ID AND invdet.Warehouse_ID = wh.Warehouse_ID
	WHERE     (inv.RecordStatus_ID <> 4) AND (invdet.DocumentType_ID IN (2, 3, 18))
	) AS a
GROUP BY Co_ID, Co_Code, Warehouse_ID, Warehouse_Name, Item_ID, Item_Code, dbo.fnGetItemCategoryDesc(Co_ID, Item_Code), Item_Desc
go
IF EXISTS (SELECT O.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetStockByItemCodeCompanies')
	DROP PROC spGetStockByItemCodeCompanies
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
CREATE PROC spGetStockByItemCodeCompanies
	@ItemCode VARCHAR(20)
	,@Companies VARCHAR(8000)
AS
SELECT Co_ID, Co_Code, Warehouse_ID, Warehouse_Name, Item_ID, Item_Code, Item_Category, Item_Desc
	, Inventory_Qty_Size01, Inventory_Qty_Size02, Inventory_Qty_Size03, Inventory_Qty_Size04, Inventory_Qty_Size05
	, Inventory_Qty_Size06, Inventory_Qty_Size07, Inventory_Qty_Size08, Inventory_Qty_Size09, Inventory_Qty_Size10
	, Inventory_Qty_Size11, Inventory_Qty_Size12, Inventory_Qty_Size13
	, Inventory_Qty_Size01 + Inventory_Qty_Size02 + Inventory_Qty_Size03 + Inventory_Qty_Size04 + Inventory_Qty_Size05 
		+ Inventory_Qty_Size06 + Inventory_Qty_Size07 + Inventory_Qty_Size08 + Inventory_Qty_Size09 + Inventory_Qty_Size10 
		+ Inventory_Qty_Size11 + Inventory_Qty_Size12 + Inventory_Qty_Size13 AS Qty_Total 
FROM vwStockInquiry 
WHERE (Item_Code = @ItemCode) AND (Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_4)) 
		OR (Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_3)) AND (@ItemCode = '') 

UNION ALL

SELECT 0 AS Co_ID, '' AS Co_Code, Warehouse_ID, Warehouse_Name, 0 AS Item_ID, Item_Code, '' AS Item_Category, 'Total' AS Item_Desc
	, SUM(Inventory_Qty_Size01) AS Inventory_Qty_Size01, SUM(Inventory_Qty_Size02) AS Inventory_Qty_Size02, SUM(Inventory_Qty_Size03) AS Inventory_Qty_Size03
	, SUM(Inventory_Qty_Size04) AS Expr4, SUM(Inventory_Qty_Size05) AS Expr5, SUM(Inventory_Qty_Size06) AS Expr6
	, SUM(Inventory_Qty_Size07) AS Expr7, SUM(Inventory_Qty_Size08) AS Expr8, SUM(Inventory_Qty_Size09) AS Expr9
	, SUM(Inventory_Qty_Size10) AS Expr10, SUM(Inventory_Qty_Size11) AS Expr11, SUM(Inventory_Qty_Size12) AS Expr12
	, SUM(Inventory_Qty_Size13) AS Expr13
	, SUM(Inventory_Qty_Size01 + Inventory_Qty_Size02 + Inventory_Qty_Size03 + Inventory_Qty_Size04 + Inventory_Qty_Size05 
		+ Inventory_Qty_Size06 + Inventory_Qty_Size07 + Inventory_Qty_Size08 + Inventory_Qty_Size09 + Inventory_Qty_Size10 
		+ Inventory_Qty_Size11 + Inventory_Qty_Size12 + Inventory_Qty_Size13) AS Qty_Total 
FROM vwStockInquiry AS vwStockInquiry_1 
WHERE (Item_Code = @ItemCode) AND (Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_2)) 
		OR (Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_1)) AND (@ItemCode = '') 

GROUP BY Warehouse_ID, Warehouse_Name, Item_Code

ORDER BY Item_Code, Co_ID
GO
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
AS
BEGIN
	SELECT it.Co_ID
		, c.Co_Code
		, its.Warehouse_ID
		, ISNULL(wh.Warehouse_Name, 'Default') AS Warehouse_Name
		, it.Item_ID
		, Item_Code
		, dbo.fnGetItemCategoryDesc(it.Co_ID, Item_Code) AS Item_Category
		, Item_Desc
		, ISNULL(ItemSummary_Size01, 0) - ISNULL(Item_MinStock_Size0, 0) AS Inventory_Qty_Size01
		, ISNULL(ItemSummary_Size02, 0) - ISNULL(Item_MinStock_Size1, 0) AS Inventory_Qty_Size02
		, ISNULL(ItemSummary_Size03, 0) - ISNULL(Item_MinStock_Size2, 0) AS Inventory_Qty_Size03
		, ISNULL(ItemSummary_Size04, 0) - ISNULL(Item_MinStock_Size3, 0) AS Inventory_Qty_Size04
		, ISNULL(ItemSummary_Size05, 0) - ISNULL(Item_MinStock_Size4, 0) AS Inventory_Qty_Size05
		, ISNULL(ItemSummary_Size06, 0) - ISNULL(Item_MinStock_Size5, 0) AS Inventory_Qty_Size06
		, ISNULL(ItemSummary_Size07, 0) - ISNULL(Item_MinStock_Size6, 0) AS Inventory_Qty_Size07
		, ISNULL(ItemSummary_Size08, 0) - ISNULL(Item_MinStock_Size7, 0) AS Inventory_Qty_Size08
		, ISNULL(ItemSummary_Size09, 0) - ISNULL(Item_MinStock_Size8, 0) AS Inventory_Qty_Size09
		, ISNULL(ItemSummary_Size10, 0) - ISNULL(Item_MinStock_Size9, 0) AS Inventory_Qty_Size10
		, ISNULL(ItemSummary_Size11, 0) - ISNULL(Item_MinStock_Size10, 0) AS Inventory_Qty_Size11
		, ISNULL(ItemSummary_Size12, 0) - ISNULL(Item_MinStock_Size11, 0) AS Inventory_Qty_Size12
		, ISNULL(ItemSummary_Size13, 0) - ISNULL(Item_MinStock_Size12, 0) AS Inventory_Qty_Size13
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
	WHERE (it.Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',')))
		AND (it.Item_Code = @ItemCode OR @ItemCode = '')
		AND its.Source_DocumentType_ID = 15	--15=Stock
	ORDER BY it.Item_Code, c.Co_Code
END
