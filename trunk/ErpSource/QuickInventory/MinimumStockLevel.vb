Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common
Imports System.Windows.Forms

Public Class MinimumStockLevel

#Region "Declaration"
  Private WithEvents _ItemDataTable As New ItemDataTable
  Private WithEvents _ItemTableAdapter As New ItemTableAdapter

  Private _ItemCodeColumnsCount As Int32
  Private _ShowRateColumns As Boolean
  Private _TotalQtyColumnExpression As String
  Private _TotalAmountColumnExpression As String
#End Region

#Region "Properties"

#End Region

#Region "Methods"
  Private Sub SetGridLayout()
    Try
      _ItemDataTable.TotalMinStockQtyColumn.Expression = _TotalQtyColumnExpression
      _ItemDataTable.TotalMinStockAmountColumn.Expression = _TotalAmountColumnExpression

      Me.grdInventory.ItemIDColumnIndex = _ItemDataTable.Item_IDColumn.Ordinal
      Me.grdInventory.ItemCodeFirstColumnIndex = _ItemDataTable.Item_CodeColumn.Ordinal
      Me.grdInventory.ItemDescColumnIndex = _ItemDataTable.Item_CodeColumn.Ordinal
      Me.grdInventory.SetItemCodeColumns(DirectCast(_ItemDataTable, DataTable))
      Me.grdInventory.Sheets(0).DataSource = Me._ItemDataTable
      Me.grdInventory.RefreshAllItemCodeAndDescriptions(False)
      General.SetColumnCaptions(DirectCast(_ItemDataTable, DataTable), Me.Name)   'This method will set the captions of the columns which are stored in the database.

      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.grdInventory_Sheet1.Columns
        Select Case SheetColumn.Index
          Case _ItemDataTable.Item_MinStock_Size0Column.Ordinal
            'SheetColumn.Label = ITEM_SIZE_01_ALIAS 
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size1Column.Ordinal
            'SheetColumn.Label = "110"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size2Column.Ordinal
            'SheetColumn.Label = "120"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size3Column.Ordinal
            'SheetColumn.Label = "130"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size4Column.Ordinal
            'SheetColumn.Label = "140"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size5Column.Ordinal
            'SheetColumn.Label = "150"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size6Column.Ordinal
            'SheetColumn.Label = "160"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size7Column.Ordinal
            'SheetColumn.Label = "170"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size8Column.Ordinal
            'SheetColumn.Label = "180"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size9Column.Ordinal
            'SheetColumn.Label = "190"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case _ItemDataTable.Item_MinStock_Size10Column.Ordinal
            'SheetColumn.Label = "CM"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
            'Case _ItemDataTable.Item_CodeColumn.Ordinal
            'SheetColumn.Label = "Item"
            'SheetColumn.CellType = QuickDALLibrary.General.ItemCellType
            'SheetColumn.Width = 50
          Case _ItemDataTable.Item_DescColumn.Ordinal
            SheetColumn.Width = ITEM_DESC_CELL_WIDTH
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
            SheetColumn.BackColor = Drawing.Color.Silver
          Case _ItemDataTable.TotalMinStockQtyColumn.Ordinal
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
            SheetColumn.BackColor = Drawing.Color.Silver
            SheetColumn.Width = Constants.QTY_TOTAL_CELL_WIDTH
          Case _ItemDataTable.TotalMinStockAmountColumn.Ordinal
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
            SheetColumn.BackColor = Drawing.Color.Silver
            SheetColumn.Width = Constants.AMOUNT_TOTAL_CELL_WIDTH
          Case Else
            'Because the extrac item code columns are added before the actual item code column thats why item code column count is subtracted.
            If SheetColumn.Index >= (_ItemDataTable.Item_CodeColumn.Ordinal - General.ItemCodeColumnsCount) AndAlso SheetColumn.Index < _ItemDataTable.Item_CodeColumn.Ordinal Then
              SheetColumn.Locked = True
              SheetColumn.Visible = True
              SheetColumn.Width = Constants.ITEM_CODE_CELL_WIDTH
              SheetColumn.BackColor = Drawing.Color.Silver
            Else
              'MessageBox.Show(SheetColumn.Label)
              SheetColumn.Visible = False
            End If
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to set grid layout", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Protected Overrides Function SaveRecord() As Boolean
    Try
      _ItemTableAdapter.Update(_ItemDataTable)

      Return True
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveRecord method.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Function
#End Region

#Region "Toolbar Methods"
  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveButtonClick event method.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Events"
  Private Sub MinimumStockLevel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      ItemFromComboBox.LoadItems(Me.LoginInfoObject.CompanyID)
      ItemToComboBox.LoadItems(Me.LoginInfoObject.CompanyID)

      Me.grdInventory.Sheets(0).DataSource = Me._ItemDataTable
      Me.grdInventory.ItemSheetView = Me.grdInventory_Sheet1
      Me.grdInventory.LoginInfoObject = Me.LoginInfoObject
      SetGridLayout()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub ShowExistingStockLevelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowExistingStockLevelButton.Click
    Try
      Cursor = Cursors.WaitCursor

      _ItemDataTable = _ItemTableAdapter.GetWithoutCategoriesByCoIDItemCode(Me.LoginInfoObject.CompanyID, ItemFromComboBox.Text, ItemToComboBox.Text)
      SetGridLayout()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowExistingStockLevelButton click event method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub ShowSalesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowSalesButton.Click
    Try
      Cursor = Cursors.WaitCursor

      _ItemDataTable = _ItemTableAdapter.GetSaleInMinStockByCriteria(Me.LoginInfoObject.CompanyID, ItemFromComboBox.Text, ItemToComboBox.Text, CType(DateFromCalendarCombo.Value, Global.System.Nullable(Of Date)), CType(DateToCalendarCombo.Value, Global.System.Nullable(Of Date)))
      SetGridLayout()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ShowSalesButton click event method.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub IncreaseAmountButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncreaseAmountButton.Click
    Try
      Cursor = Cursors.WaitCursor

      If Me.IncreaseAmountTextBox.IntegerNumber <> 0 OrElse Me.IncreaseAmountTextBox.PercentNumber <> 0 Then
        For r As Int32 = 0 To Me.grdInventory_Sheet1.RowCount - 1
          With _ItemDataTable(r)
            If Me.IncreaseAmountTextBox.IntegerNumber <> 0 Then
              If .IsItem_MinStock_Size0Null Then .Item_MinStock_Size0 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size0 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size1Null Then .Item_MinStock_Size1 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size1 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size2Null Then .Item_MinStock_Size2 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size2 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size3Null Then .Item_MinStock_Size3 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size3 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size4Null Then .Item_MinStock_Size4 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size4 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size5Null Then .Item_MinStock_Size5 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size5 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size6Null Then .Item_MinStock_Size6 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size6 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size7Null Then .Item_MinStock_Size7 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size7 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size8Null Then .Item_MinStock_Size8 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size8 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size9Null Then .Item_MinStock_Size9 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size9 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size10Null Then .Item_MinStock_Size10 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size10 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size11Null Then .Item_MinStock_Size11 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size11 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size12Null Then .Item_MinStock_Size12 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size12 += Me.IncreaseAmountTextBox.IntegerNumber
              If .IsItem_MinStock_Size13Null Then .Item_MinStock_Size13 = Me.IncreaseAmountTextBox.IntegerNumber Else .Item_MinStock_Size13 += Me.IncreaseAmountTextBox.IntegerNumber
            ElseIf Me.IncreaseAmountTextBox.PercentNumber <> 0 Then
              If .IsItem_MinStock_Size0Null Then .Item_MinStock_Size0 = 0 Else .Item_MinStock_Size0 += Convert.ToInt32(Math.Round(.Item_MinStock_Size0 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size1Null Then .Item_MinStock_Size1 = 0 Else .Item_MinStock_Size1 += Convert.ToInt32(Math.Round(.Item_MinStock_Size1 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size2Null Then .Item_MinStock_Size2 = 0 Else .Item_MinStock_Size2 += Convert.ToInt32(Math.Round(.Item_MinStock_Size2 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size3Null Then .Item_MinStock_Size3 = 0 Else .Item_MinStock_Size3 += Convert.ToInt32(Math.Round(.Item_MinStock_Size3 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size4Null Then .Item_MinStock_Size4 = 0 Else .Item_MinStock_Size4 += Convert.ToInt32(Math.Round(.Item_MinStock_Size4 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size5Null Then .Item_MinStock_Size5 = 0 Else .Item_MinStock_Size5 += Convert.ToInt32(Math.Round(.Item_MinStock_Size5 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size6Null Then .Item_MinStock_Size6 = 0 Else .Item_MinStock_Size6 += Convert.ToInt32(Math.Round(.Item_MinStock_Size6 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size7Null Then .Item_MinStock_Size7 = 0 Else .Item_MinStock_Size7 += Convert.ToInt32(Math.Round(.Item_MinStock_Size7 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size8Null Then .Item_MinStock_Size8 = 0 Else .Item_MinStock_Size8 += Convert.ToInt32(Math.Round(.Item_MinStock_Size8 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size9Null Then .Item_MinStock_Size9 = 0 Else .Item_MinStock_Size9 += Convert.ToInt32(Math.Round(.Item_MinStock_Size9 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size10Null Then .Item_MinStock_Size10 = 0 Else .Item_MinStock_Size10 += Convert.ToInt32(Math.Round(.Item_MinStock_Size10 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size11Null Then .Item_MinStock_Size11 = 0 Else .Item_MinStock_Size11 += Convert.ToInt32(Math.Round(.Item_MinStock_Size11 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size12Null Then .Item_MinStock_Size12 = 0 Else .Item_MinStock_Size12 += Convert.ToInt32(Math.Round(.Item_MinStock_Size12 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
              If .IsItem_MinStock_Size13Null Then .Item_MinStock_Size13 = 0 Else .Item_MinStock_Size13 += Convert.ToInt32(Math.Round(.Item_MinStock_Size13 * Me.IncreaseAmountTextBox.PercentNumber / 100, 0))
            End If
          End With
        Next
      End If

      _ItemDataTable.TotalMinStockQtyColumn.Expression = _TotalQtyColumnExpression
      _ItemDataTable.TotalMinStockAmountColumn.Expression = _TotalAmountColumnExpression
      Me.grdInventory.Refresh()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in IncreaseAmountButton click event.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    If DatabaseCache.GetItemLeveling IsNot Nothing Then
      Me._ItemCodeColumnsCount = DatabaseCache.GetItemLeveling.Rows.Count
    Else
      Me._ItemCodeColumnsCount = 1
    End If

    ' Add any initialization after the InitializeComponent() call.
    With _ItemDataTable
      _TotalAmountColumnExpression = "ISNULL(" & .Item_MinStock_Size0Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size01Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size1Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size02Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size2Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size03Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size3Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size04Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size4Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size05Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size5Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size06Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size6Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size07Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size7Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size08Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size8Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size09Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size9Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size10Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size10Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size11Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size11Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size12Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size12Column.ColumnName & ",0) * ISNULL(" & .Item_SaleRate_Size13Column.ColumnName _
        & ",0)"

      _TotalQtyColumnExpression = "ISNULL(" & .Item_MinStock_Size0Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size1Column.ColumnName & ",0) + ISNULL(" & .Item_MinStock_Size2Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size3Column.ColumnName & ",0) + ISNULL(" & .Item_MinStock_Size4Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size5Column.ColumnName & ",0) + ISNULL(" & .Item_MinStock_Size6Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size7Column.ColumnName & ",0) + ISNULL(" & .Item_MinStock_Size8Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size9Column.ColumnName & ",0) + ISNULL(" & .Item_MinStock_Size10Column.ColumnName _
        & ",0) + ISNULL(" & .Item_MinStock_Size11Column.ColumnName & ",0) + ISNULL(" & .Item_MinStock_Size12Column.ColumnName _
        & ",0)"

    End With
  End Sub

End Class
