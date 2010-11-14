set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
-- ======================================================================
-- Author:		Muhammad Zakee
-- Create date: 29-Sep-10
-- Description:	This procedure returns the Source Wise Item Stock
-- ======================================================================
-- ------------ Modification History ------------------------------------
-- Author	Date		Details
-- ------   ----        -------
-- ----------------------------------------------------------------------

ALTER Procedure [dbo].[SpSourceWiseItemStock]
@CO_ID as Smallint,
@From_Date AS Datetime,
@To_Date As DateTime,
@From_Item as Varchar(50),
@To_Item as Varchar(50),
@From_Party as Varchar(50),
@To_Party as VArchar(50)
AS
/*
Declare @Co_ID as Smallint
Declare @From_Date as Datetime
Declare @To_Date as Datetime
Declare @From_Item as Varchar(50)
Declare @To_Item as varchar(50)
Declare @From_Party as Varchar(50)
Declare @To_Party as Varchar(50)
Set @CO_ID = 200
Set @From_Date = '1900-09-16'
Set @To_Date = '2010-11-16'
Set @From_Item = ''
Set @To_Item = ''
Set @From_Party = ''
Set @To_Party = ''
--*/
 Select  Party_Desc	,Item_Code , Item_Desc ,(Item_Code +  ' '  + Item_Desc) Item
		,Sum(Inventory_Qty_Size01) Inventory_Qty_Size01 ,Sum(Inventory_Qty_Size02) Inventory_Qty_Size02 ,Sum(Inventory_Qty_Size03) Inventory_Qty_Size03 ,Sum(Inventory_Qty_Size04) Inventory_Qty_Size04
		,Sum(Inventory_Qty_Size05) Inventory_Qty_Size05 ,Sum(Inventory_Qty_Size06) Inventory_Qty_Size06 ,Sum(Inventory_Qty_Size07) Inventory_Qty_Size07 ,Sum(Inventory_Qty_Size08) Inventory_Qty_Size08
		,Sum(Inventory_Qty_Size09) Inventory_Qty_Size09 ,Sum(Inventory_Qty_Size10) Inventory_Qty_Size10 ,Sum(Inventory_Qty_Size11) Inventory_Qty_Size11
From Inventory_Detail as ID
	Inner Join Inventory as I ON I.Inventory_ID = ID.Inventory_ID And ID.DocumentType_ID =18 --And i.Co_ID = 200
	Inner Join Inv_Item as II ON II.Item_ID = ID.Item_ID And II.CO_ID = @CO_ID
	Inner Join Party AS P On P.Party_ID = I.Party_ID And P.CO_ID = @CO_ID And EntityType_ID = 6
Where I.CO_ID = @CO_ID And (ID.RecordStatus_ID <> 4 Or ID.RecordStatus_ID IS Null) 
	  And (Inventory_Date Between @From_Date And @To_Date)
	  And (Item_Code >= @From_Item OR @From_Item = '')
	  And  (Item_Code <=@To_Item Or @To_Item = '')
	  And (Party_Code >= @From_Party OR @From_Party = '')
	  And (Party_Code <=@To_Party OR @To_Party = '')
Group by Party_Desc ,Item_Code ,Item_Desc
Order By Party_Desc ,Item_Code



















