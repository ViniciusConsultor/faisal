set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
-- ======================================================================
-- Author:		Muhammad Zakee
-- Create date: 31-MAR-11
-- Description:	
-- ======================================================================
-- ------------ Modification History ------------------------------------
-- Author	Date		Details
-- ------   ----        -------
-- ----------------------------------------------------------------------

ALTER Procedure [dbo].[SpGetStockByItemCodeAndDateCompany]
@ItemCode as Varchar(50),
@Companies as Varchar(8000),
@StockDate AS DateTime,
@AddTotalRows AS BIT
AS
/*
Declare @Companies as Varchar(8000)
Declare @ItemCode AS VARCHAR(50)
Declare @AddTotalRows AS BIT
Declare @StockDate AS DateTime

SET @Companies = '5'
SET @ItemCode = '' 
SET @AddTotalRows = 0
SET @StockDate = '2009-10-28'
--*/
			Select  ID.Co_ID ,Co_Code ,0 AS Warehouse_ID ,ID.Item_ID ,Item_Code 
			 ,dbo.fnGetItemCategoryDesc(ID.Co_ID, Item_Code) AS Item_Category, II.Item_Desc
		 	 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size01 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size01 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size01 else 0 End)) Qty_Size01 
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size02 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size02 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size02 else 0 End)) Qty_Size02
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size03 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size03 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size03 else 0 End)) Qty_Size03
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size04 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size04 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size04 else 0 End)) Qty_Size04
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size05 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size05 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size05 else 0 End)) Qty_Size05
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size06 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size06 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size06 else 0 End)) Qty_Size06
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size07 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size07 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size07 else 0 End)) Qty_Size07
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size08 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size08 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size08 else 0 End)) Qty_Size08
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size09 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size09 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size09 else 0 End)) Qty_Size09
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size10 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size10 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size10 else 0 End)) Qty_Size10
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size11 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size11 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size11 else 0 End)) Qty_Size11
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size12 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size12 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size12 else 0 End)) Qty_Size12
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size13 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size13 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size13 else 0 End)) Qty_Size13
													
			From Inventory_Detail as ID
				INNER Join Inventory as I ON I.Inventory_ID = ID.Inventory_ID And ID.DocumentType_ID IN (1,2,3) 
				INNER Join Base_Company c ON ID.Co_ID = c.Co_ID
				Left JOIN Inv_Item as II ON II.Item_ID = ID.Item_ID And II.CO_ID = ID.Co_ID
				Where (II.Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_4) OR @Companies = '')
				  AND (Item_Code = @ItemCode OR @ItemCode = '')
				  AND (ID.RecordStatus_ID <> 4 Or ID.RecordStatus_ID IS Null) 
				  AND (Inventory_Date <= @StockDate)

			GROUP BY ID.CO_ID ,Co_Code ,WareHouse_ID ,ID.Item_ID ,Item_Code ,II.Item_Desc

UNION ALL
				Select -1 AS Co_ID ,'' As Co_Code ,0 AS Warehouse_ID ,0 AS Item_ID ,Item_Code 
					,'' AS Item_Category ,'Total' AS Item_Desc
				 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size01 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size01 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size01 else 0 End)) Qty_Size01 
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size02 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size02 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size02 else 0 End)) Qty_Size02
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size03 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size03 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size03 else 0 End)) Qty_Size03
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size04 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size04 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size04 else 0 End)) Qty_Size04
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size05 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size05 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size05 else 0 End)) Qty_Size05
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size06 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size06 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size06 else 0 End)) Qty_Size06
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size07 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size07 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size07 else 0 End)) Qty_Size07
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size08 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size08 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size08 else 0 End)) Qty_Size08
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size09 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size09 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size09 else 0 End)) Qty_Size09
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size10 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size10 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size10 else 0 End)) Qty_Size10
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size11 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size11 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size11 else 0 End)) Qty_Size11
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size12 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size12 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size12 else 0 End)) Qty_Size12
			 ,(SUM(CASE ID.DocumentType_ID WHEN 3 Then Inventory_Qty_Size13 else 0 End) +
			   SUM(CASE ID.DocumentType_ID WHEN 2 Then Inventory_Qty_Size13 else 0 End)-
			   SUM(CASE ID.DocumentType_ID WHEN 1 Then Inventory_Qty_Size13 else 0 End)) Qty_Size13
					
			From Inventory_Detail as ID
				INNER Join Inventory as I ON I.Inventory_ID = ID.Inventory_ID And ID.DocumentType_ID IN (1,2,3) 
				INNER Join Base_Company c ON ID.Co_ID = c.Co_ID
				Left JOIN Inv_Item as II ON II.Item_ID = ID.Item_ID And II.CO_ID = ID.Co_ID
				Where (II.Co_ID IN (SELECT MyValue FROM dbo.fn_Split(@Companies, ',') AS fn_Split_4) OR @Companies = '')
				  AND (Item_Code = @ItemCode OR @ItemCode = '')
				  AND (@AddTotalRows = 1)
				  AND (ID.RecordStatus_ID <> 4 Or ID.RecordStatus_ID IS Null) 
				  AND (Inventory_Date <= @StockDate)
				GROUP BY  WareHouse_ID ,Item_Code 
				ORDER BY II.Item_Code ,ID.CO_ID	DESC


