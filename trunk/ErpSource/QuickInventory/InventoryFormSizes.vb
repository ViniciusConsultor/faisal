Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common

Public Class InventoryFormSizes

#Region "Declaration"

  'Data Adapters
  Dim _InventoryTableAdapterObject As New Invs_InventoryTableAdapter
  Dim _InventoryDetailTableAdapterObject As New Invs_InventoryDetailTableAdapter
  Dim _ItemTableAdapter As New Invs_ItemTableAdapter
  'Data Tables
  Private WithEvents _InventoryDataTable As New Invs_InventoryDataTable
  Private WithEvents _InventoryDetailDataTable As New Invs_InventoryDetailDataTable
  Private WithEvents _ItemDataTable As New Invs_ItemDataTable
  'Data Rows
  Private _CurrentInventoryDataRow As Invs_InventoryRow

  Private _ShowRateColumns As Boolean
  Private TotalAmountColumnExpression As String
  Private TotalQtyColumnExpression As String
  Private _ItemCodeColumnsCount As Int32
  Private _DefaultWarehouseID As Int32 = 0

  Private Enum enInventoryColumns
    DeleteRowButton
    Co_ID
    Inventory_ID
    InventoryDetail_ID
    DocumentType_ID
    Item_ID
    Item_Code
    Item_Description
    ItemQty
    ItemRate
    StampDateTime
    StampUser
    SourceDocumentTypeID
    SourceDocumentCoID
    SourceDocumentNo
    TotalQty
    TotalAmount
  End Enum

  Private Const EXC_PRE As String = "Exception in "
  Private Const EXC_grdinventory_LEAVECELL As String = EXC_PRE & "_LeaveCell"

#End Region

#Region "Properties"
  Public Property ShowRateColumns() As Boolean
    Get
      Return _ShowRateColumns
    End Get
    Set(ByVal value As Boolean)
      _ShowRateColumns = value
    End Set
  End Property
#End Region

#Region "Methods"
  Private Function IsValid() As Boolean
    Try
      Me.grdInventory.EditMode = False

      If Me._InventoryDetailDataTable Is Nothing OrElse Me._InventoryDetailDataTable.Rows.Count < 1 Then
        MessageBox.Show("There must be atleast one item to save", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.grdInventory.Focus()
        Return False
      End If
      If Me.CompanyComboBox1.CompanyID <= 0 And Not Me.DocumentType = enuDocumentType.PurchaseOrder Then
        MessageBox.Show("Select a valid document company from the combo box", "Invalid Company", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        CompanyComboBox1.Focus()
        Return False
      End If
      If Me.SourceDocumentNoTextBox.Text.Trim = String.Empty AndAlso Not Me.DocumentType = enuDocumentType.PurchaseOrder Then
        MessageBox.Show("You must enter source document no. in order to save this record.", "Missing Source Document No.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        SourceDocumentNoTextBox.Focus()
        Return False
      End If
      'If Me._InventoryDataTable.Rows.Count < 1 Then
      '  MessageBox.Show("Inventory row is not created.", "Inventory data row not created.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      '  Return False
      'Else
      '  If Me._InventoryDataTable.CheckBusinessRules Then
      '    For Each _BusinessRule As LogicalDataSet.BusinessRuleRow In _InventoryDataTable(0).BrokenBusinessRule
      '      MessageBox.Show(_BusinessRule.RuleDescription, _BusinessRule.RuleName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      '    Next
      '  End If
      'End If

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in IsValid method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim InventoryID As Int32
      Dim InventoryDataRow As Invs_InventoryRow
      Dim _InventoryDetailDataRow As Invs_InventoryDetailRow
      'Dim ItemID As Int32
      Dim ItemsUsedCollection As New Collection

      'It is necessary to remove row before set EditMode = False because editmode increases extra blank row.
      Me.grdInventory.EditMode = False
      _InventoryDetailDataTable.Rows.RemoveAt(_InventoryDetailDataTable.Rows.Count - 1)

      If Not IsValid() Then Exit Function

      If _CurrentInventoryDataRow Is Nothing Then
        InventoryDataRow = _InventoryDataTable.NewInvs_InventoryRow
        InventoryID = _InventoryTableAdapterObject.GetNewInventoryIDByCoID(LoginInfoObject.CompanyID).Value
        InventoryDataRow.Inventory_No = _InventoryTableAdapterObject.GetNewInventoryNoByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType).Value
        InventoryDataRow.Inventory_ID = InventoryID
        InventoryDataRow.Status_ID = 1
        Me.txtSaleNo.Text = InventoryDataRow.Inventory_No.ToString
        _CurrentInventoryDataRow = InventoryDataRow
      Else
        'In case of updated only common properties need to be set.
      End If
      'Common values to set in case of insert/ update.
      With _CurrentInventoryDataRow
        .Co_ID = LoginInfoObject.CompanyID
        .DocumentType_ID = Convert.ToInt16(Me.DocumentType)
        .Party_ID = Me.PartyComboBox1.PartyID
        .Category_Party_ID = Me.PartyCategoryComboBox.PartyID
        .Payment_Mode = 0
        .Remarks = Me.txtRemarks.Text
        .Inventory_Date = Convert.ToDateTime(uccSaleDate.Value)
        .Stamp_DateTime = Now
        .Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
        .Discount = Me.DiscountTextBox.IntegerNumber
        .SalesTax = Me.SalesTaxTextBox.IntegerNumber
      End With

      'CurrentRecordDataRow is used by parent form.
      CurrentRecordDataRow = _CurrentInventoryDataRow
      If _CurrentInventoryDataRow.RowState = DataRowState.Detached Then
        _InventoryDataTable.Rows.Add(_CurrentInventoryDataRow)
      End If
      _InventoryTableAdapterObject.Update(_InventoryDataTable)

      'We should first update the deleted rows so that it does not effect next loop for update and insertion.
      _InventoryDetailTableAdapterObject.Update(_InventoryDetailDataTable.Select("", "", DataViewRowState.Deleted))

      With grdInventory.Sheets(0)
        For r As Int32 = 0 To Me.grdInventory_Sheet1.RowCount - 1
          For c As Int32 = enInventoryColumns.ItemQty To enInventoryColumns.ItemQty + 11
            'Dim drow As DataRow
          Next
        Next
      End With

      With grdInventory.Sheets(0)
        For I As Int32 = 0 To _InventoryDetailDataTable.Rows.Count - 1
          _InventoryDetailDataRow = _InventoryDetailDataTable(I) ' DirectCast(_InventoryDetailDataTable.Rows(I), InventoryDetailRow)

          If _InventoryDetailDataRow.RowState = DataRowState.Added Then
            _InventoryDetailDataRow.InventoryDetail_ID = _InventoryDetailTableAdapterObject.GetNewInventoryDetailIDByCoIDInventoryID(LoginInfoObject.CompanyID, _CurrentInventoryDataRow.Inventory_ID).Value
            _InventoryDetailDataRow.Inventory_ID = _CurrentInventoryDataRow.Inventory_ID
            _InventoryDetailDataRow.Co_ID = LoginInfoObject.CompanyID
            _InventoryDetailDataRow.DocumentType_ID = Convert.ToInt16(Me.DocumentType)
          Else
            'Assign first row by filtering. There should not be more than one rows theoratically here if data is stored correctly.
            _InventoryDetailDataRow = _InventoryDetailDataTable(0) ' DirectCast(_InventoryDetailDataTable.Rows(0), InventoryDetailRow)
          End If

          If _InventoryDetailDataRow.RowState <> DataRowState.Unchanged Then
            _InventoryDetailDataRow.Source_Document_Co_ID = Me.CompanyComboBox1.CompanyID
            _InventoryDetailDataRow.Source_Document_No = Cast.ToInt32(Me.SourceDocumentNoTextBox.Text)
            _InventoryDetailDataRow.Warehouse_ID = _DefaultWarehouseID
            _InventoryDetailDataRow.Stamp_DateTime = Now
            _InventoryDetailDataRow.Stamp_UserID = LoginInfoObject.UserID
            If _InventoryDetailDataRow.RowState = DataRowState.Detached Then
              _InventoryDetailDataTable.Rows.Add(_InventoryDetailDataRow)
            End If

            'This statement should be inside loop so that we can fetch new detail id properly.
            If _InventoryDetailTableAdapterObject.Update(_InventoryDetailDataTable(I)) > 0 Then
              'Record is updated
            Else
              'Record is not updated.
              QuickMessageBox.Show(Me.LoginInfoObject, "Record was not updated successfully", MessageBoxIcon.Exclamation)
            End If
          End If
        Next
      End With

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord method.", ex)
      Throw QuickExceptionObject
    Finally
      AddItem()
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      'Dim InventoryDetailDataRow As InventoryDetailRow
      'Dim ItemDataRow As ItemRow

      If Me._InventoryDataTable.Rows.Count > 0 Then
        Me._CurrentInventoryDataRow = _InventoryDataTable(Me.CurrentRecordIndex) ' CType(Me._InventoryDataTable.Rows(Me.CurrentRecordIndex), InventoryRow)
        Me.CurrentRecordDataRow = Me._CurrentInventoryDataRow

        Me.ClearControls(Me)
        Me.CurrentRecordDataRow = Nothing
        If Me._InventoryDetailDataTable IsNot Nothing Then
          Do While Me._InventoryDetailDataTable.Rows.Count > 0
            Me._InventoryDetailDataTable.Rows.RemoveAt(0)
          Loop
        End If
        'AddItem()

        Me.txtSaleNo.Text = Me._CurrentInventoryDataRow.Inventory_No.ToString
        Me.uccSaleDate.Value = Me._CurrentInventoryDataRow.Inventory_Date
        Me.txtRemarks.Text = Me._CurrentInventoryDataRow.Remarks
        Me.PartyComboBox1.PartyID = Me._CurrentInventoryDataRow.Party_ID
        If Me._CurrentInventoryDataRow.IsCategory_Party_IDNull Then
          Me.PartyCategoryComboBox.SelectedRow = Nothing
          Me.PartyCategoryComboBox.Text = ""
        Else
          Me.PartyCategoryComboBox.PartyID = Me._CurrentInventoryDataRow.Category_Party_ID
        End If
        Me.DiscountTextBox.IntegerNumber = Me._CurrentInventoryDataRow.Discount
        Me.SalesTaxTextBox.IntegerNumber = Me._CurrentInventoryDataRow.SalesTax

        Me.CompanyComboBox1.SelectedRow = Nothing
        Me.SourceDocumentNoTextBox.Text = String.Empty

        _InventoryDetailDataTable = _InventoryDetailTableAdapterObject.GetByCoIDInventoryID(Me._CurrentInventoryDataRow.Co_ID, Me._CurrentInventoryDataRow.Inventory_ID)
        Me.grdInventory.SetItemCodeColumns(DirectCast(_InventoryDetailDataTable, DataTable))

        If _InventoryDetailDataTable.Rows.Count > 0 Then
          If Not _InventoryDetailDataTable(0).IsSource_Document_Co_IDNull Then
            Me.CompanyComboBox1.CompanyID = _InventoryDetailDataTable(0).Source_Document_Co_ID
          End If
          If Not _InventoryDetailDataTable(0).IsSource_Document_NoNull Then
            Me.SourceDocumentNoTextBox.Text = _InventoryDetailDataTable(0).Source_Document_No.ToString
          End If
        End If
        Me.grdInventory_Sheet1.DataSource = Me._InventoryDetailDataTable
        Me.grdInventory.ShowDeleteRowButton(Me.grdInventory_Sheet1) = True
        _InventoryDetailDataTable.TotalAmountColumn.Expression = TotalAmountColumnExpression
        _InventoryDetailDataTable.TotalQtyColumn.Expression = TotalQtyColumnExpression

        ShowTotal()
        Me.grdInventory.RefreshAllItemCodeAndDescriptions(False)
        _InventoryDetailDataTable.AcceptChanges()
        AddItem()   'Add item call should be after acceptchanges otherwise it will make the last as unchnaged instead of added.
        SetGridLayout()
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show record", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Private Sub AddItem()
    Try
      Dim InventoryDetailDataRow As Invs_InventoryDetailRow

      If _InventoryDetailDataTable.Rows.Count > 0 AndAlso _InventoryDetailDataTable(_InventoryDetailDataTable.Rows.Count - 1).Item_ID = 0 Then
        'If last row is already new row then don't add it. It is added to avoid adding multiple new rows at the end of grid.
      Else
        InventoryDetailDataRow = _InventoryDetailDataTable.NewInvs_InventoryDetailRow()
        With InventoryDetailDataRow
          .Co_ID = LoginInfoObject.CompanyID
          .DocumentType_ID = Convert.ToInt16(Me.DocumentType)
          .InventoryDetail_ID = _InventoryDetailDataTable.Rows.Count + 1
          .Inventory_ID = 0
          .ItemQty = 0
          .ItemRate = 0
          .Item_ID = 0
          .Stamp_DateTime = Now
          .Stamp_UserID = 0

          _InventoryDetailDataTable.Rows.Add(InventoryDetailDataRow)
        End With
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in AddItem method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Protected Overrides Sub ClearControls(ByRef pControlObject As System.Windows.Forms.Control)
    Try
      MyBase.ClearControls(pControlObject)

      'Me.grdInventory.ActiveSheet.Rows.Remove(0, Me.grdInventory.ActiveSheet.Rows.Count)
      'Me.grdInventory.ActiveSheet.Rows.Add(0, 1)
      'Me.CurrentRecordDataRow = Nothing
      'Do While Me._InventoryDetailDataTable.Rows.Count > 0
      '  Me._InventoryDetailDataTable.Rows.RemoveAt(0)
      'Loop
      'AddItem()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to clear controls", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub ShowTotal()
    Dim TotalAmount As Double = 0
    Dim TotalQty As Double = 0

    Try
      For I As Int32 = 0 To Me.grdInventory_Sheet1.Rows.Count - 1
        TotalAmount += Val(Me.grdInventory_Sheet1.GetValue(I, enInventoryColumns.TotalAmount + General.ItemCodeColumnsCount))
        TotalQty += Val(Me.grdInventory_Sheet1.GetValue(I, enInventoryColumns.TotalQty + General.ItemCodeColumnsCount))
      Next
      TotalAmount -= TotalAmount * Me.DiscountTextBox.IntegerNumber / 100
      TotalAmount += TotalAmount * Me.SalesTaxTextBox.IntegerNumber / 100
      Me.TotalAmountLabel.Text = TotalAmount.ToString
      Me.TotalQtyLabel.Text = TotalQty.ToString

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show total", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Private Sub SetGridLayout()
    Try

      '_InventoryDetailDataTable.Columns.Add("Cat", System.Type.GetType("System.String"), "")
      'If grdInventory_Sheet1.ColumnCount < (_InventoryDetailDataTable.Columns.Count + _ItemCodeColumnsCount) Then
      '  Me.grdInventory_Sheet1.Columns.Add(0, _ItemCodeColumnsCount)
      '  For I As Int32 = 0 To _ItemCodeColumnsCount - 1
      '    Me.grdInventory_Sheet1.Columns(I).Label = DatabaseCache.GetItemLeveling(I).Description
      '  Next
      'End If
      Me.grdInventory.ShowDeleteRowButton(Me.grdInventory_Sheet1) = True

      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.grdInventory_Sheet1.Columns
        Select Case SheetColumn.Index
          Case enInventoryColumns.DeleteRowButton
            'Do Nothing
            'Case enInventoryColumns.Qty1 + _ItemCodeColumnsCount   ' _InventoryDetailDataTable.Inventory_Qty_Size01Column.ColumnName
            '  SheetColumn.Label = "100"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty2 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size02Column.ColumnName
            '  SheetColumn.Label = "110"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty3 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Qty_Size03Column.ColumnName
            '  SheetColumn.Label = "120"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty4 + _ItemCodeColumnsCount '_InventoryDetailDataTable.Inventory_Qty_Size04Column.ColumnName
            '  SheetColumn.Label = "130"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty5 + _ItemCodeColumnsCount   '_InventoryDetailDataTable.Inventory_Qty_Size05Column.ColumnName
            '  SheetColumn.Label = "140"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty6 + _ItemCodeColumnsCount   '_InventoryDetailDataTable.Inventory_Qty_Size06Column.ColumnName
            '  SheetColumn.Label = "150"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty7 + _ItemCodeColumnsCount  '_InventoryDetailDataTable.Inventory_Qty_Size07Column.ColumnName
            '  SheetColumn.Label = "160"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty8 + _ItemCodeColumnsCount '_InventoryDetailDataTable.Inventory_Qty_Size08Column.ColumnName
            '  SheetColumn.Label = "170"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty9 + _ItemCodeColumnsCount   '_InventoryDetailDataTable.Inventory_Qty_Size09Column.ColumnName
            '  SheetColumn.Label = "180"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty10 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size10Column.ColumnName
            '  SheetColumn.Label = "190"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty11 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size11Column.ColumnName
            '  SheetColumn.Label = "CM"
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty12 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size12Column.ColumnName
            '  SheetColumn.Visible = False
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Qty13 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size13Column.ColumnName
            '  SheetColumn.Visible = False
            '  SheetColumn.Width = QTY_CELL_WIDTH
            '  SheetColumn.CellType = QtyCellType
            'Case enInventoryColumns.Rate1 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size01Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate2 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size02Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate3 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size03Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate4 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size04Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate5 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size05Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate6 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Rate_Size06Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate7 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Rate_Size07Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate8 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Rate_Size08Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate9 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size09Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate10 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size10Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate11 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size11Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate12 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size12Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.Rate13 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size13Column.ColumnName
            '  SheetColumn.Visible = ShowRateColumns
            'Case enInventoryColumns.InventoryDetail_ID + _ItemCodeColumnsCount ' _InventoryDetailDataTable.InventoryDetail_IDColumn.ColumnName
            '  SheetColumn.Visible = False
            'Case enInventoryColumns.Item_ID + _ItemCodeColumnsCount   ' _InventoryDetailDataTable.Item_IDColumn.ColumnName
            '  SheetColumn.Visible = False
            'Case enInventoryColumns.Item_Code + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Item_CodeColumn.ColumnName
            '  SheetColumn.Label = "Item"
            '  SheetColumn.CellType = QuickDALLibrary.General.ItemCellType
            '  SheetColumn.Width = 50
          Case enInventoryColumns.Item_Description + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Item_DescColumn.ColumnName
            SheetColumn.Width = ITEM_DESC_CELL_WIDTH
            SheetColumn.Label = "Desc"
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
            SheetColumn.BackColor = Drawing.Color.Silver
            SheetColumn.Width = Constants.ITEM_DESC_CELL_WIDTH
          Case enInventoryColumns.TotalAmount + _ItemCodeColumnsCount ' _InventoryDetailDataTable.TotalAmountColumn.Caption
            SheetColumn.Width = AMOUNT_TOTAL_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
            SheetColumn.Label = "Amount"
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
            SheetColumn.BackColor = Drawing.Color.Silver
          Case enInventoryColumns.TotalQty + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.TotalQtyColumn.Caption
            SheetColumn.Width = QTY_TOTAL_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
            SheetColumn.Label = "Qty"
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
            SheetColumn.BackColor = Drawing.Color.Silver
          Case Else
            If SheetColumn.Index >= enInventoryColumns.Item_Code AndAlso SheetColumn.Index < (enInventoryColumns.Item_Code + _ItemCodeColumnsCount) Then
              'Do Nothing, Item Code properties will be set by the spread itself.
              SheetColumn.Width = Constants.ITEM_CODE_CELL_WIDTH
            Else
              SheetColumn.Visible = False
            End If
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to set grid layout", ex)
      Throw QuickExceptionObject
    End Try
  End Sub
#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetFirstByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)
      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoreFirstButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me._CurrentInventoryDataRow IsNot Nothing Then
        Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetNextByCoIDInventoryIDDocumentTypeID(Me._CurrentInventoryDataRow.Co_ID, Me._CurrentInventoryDataRow.Inventory_ID, Me.DocumentType)
      Else
        Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetNextByCoIDInventoryIDDocumentTypeID(LoginInfoObject.CompanyID, 0, Me.DocumentType)
      End If
      MyBase.MoveNextButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me._CurrentInventoryDataRow IsNot Nothing Then
        Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetPreviousByCoIDInventoryIDDocumentTypeID(Me._CurrentInventoryDataRow.Co_ID, Me._CurrentInventoryDataRow.Inventory_ID, Me.DocumentType)
      Else
        Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetPreviousByCoIDInventoryIDDocumentTypeID(LoginInfoObject.CompanyID, 0, Me.DocumentType)
      End If
      MyBase.MovePreviousButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me._CurrentInventoryDataRow IsNot Nothing Then
        Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetLastByCoIDDocumentTypeID(Me._CurrentInventoryDataRow.Co_ID, Me.DocumentType)
      Else
        Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetLastByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)
      End If
      MyBase.MoveLastButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully saved", QuickMessageBox.MessageBoxTypes.ShortMessage)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "Record was not saved", QuickMessageBox.MessageBoxTypes.ShortMessage)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me._CurrentInventoryDataRow = Nothing
      Me._CurrentRecordDataRow = Nothing
      Me._InventoryDataTable.Rows.Clear()
      Me._InventoryDetailDataTable.Rows.Clear()
      Me.TotalAmountLabel.Text = "0"
      Me.TotalQtyLabel.Text = "0"
      MyBase.CancelButtonClick(sender, e)
      uccSaleDate.Value = Now

      AddItem()
      SetGridLayout()
      'If Me.grdInventory.Sheets(0).DataSource Is Nothing Then MessageBox.Show("it is nothing")
      'Me.grdInventory.Sheets(0).DataSource = _InventoryDetailDataTable
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._CurrentInventoryDataRow.Status_ID = Constants.RecordStatuses.Deleted

        SaveRecord()
        'Below line is necessary so that parent form don't ask for record change confirmation.
        Me.CurrentRecordDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
      Else
      End If

      QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub PrintPreviewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ReportViewerForm As New QuickReports.CrystalReportViewerForm
      Dim _ParameterValues As New System.Collections.Specialized.NameValueCollection
      Dim _SelectionFormula As String = String.Empty

      If Me._CurrentInventoryDataRow IsNot Nothing Then
        Me.Cursor = Cursors.WaitCursor

        _ReportViewerForm.FormulaValues = New System.Collections.Specialized.NameValueCollection
        Select Case Me.DocumentType
          Case enuDocumentType.Purchase
            _ParameterValues.Add("ReportName", "Purchase")
            _ReportViewerForm.FormulaValues.Add("PartyLabel", "From:")
            _ReportViewerForm.FormulaValues.Add("InventoryNoLabel", "P. No.:")
          Case enuDocumentType.PurchaseReturn
            _ParameterValues.Add("ReportName", "Purchase Return")
            _ReportViewerForm.FormulaValues.Add("PartyLabel", "To:")
            _ReportViewerForm.FormulaValues.Add("InventoryNoLabel", "PR. No.:")
          Case enuDocumentType.SalesInvoice
            _ParameterValues.Add("ReportName", "GatePass")
            _ReportViewerForm.FormulaValues.Add("PartyLabel", "To:")
            _ReportViewerForm.FormulaValues.Add("InventoryNoLabel", "GP. No.:")
          Case enuDocumentType.SalesInvoiceReturn
            _ParameterValues.Add("ReportName", "Sales Invoice Return")
            _ReportViewerForm.FormulaValues.Add("PartyLabel", "From:")
            _ReportViewerForm.FormulaValues.Add("InventoryNoLabel", "RB. No.:")
        End Select
        _ParameterValues.Add("CurrentCompany", Me.LoginInfoObject.CompanyDesc)
        _ParameterValues.Add("DocumentTypeID", Convert.ToInt32(Me.DocumentType).ToString)
        _ParameterValues.Add("CoID", Me.LoginInfoObject.CompanyID.ToString)
        _ParameterValues.Add("InventoryID", Me._CurrentInventoryDataRow.Inventory_ID.ToString)
        _SelectionFormula = ""

        _ReportViewerForm.ParameterValues = _ParameterValues
        _ReportViewerForm.RecordSelectionFormula = _SelectionFormula
        _ReportViewerForm.Report = New QuickReports.InventoryReport
        _ReportViewerForm.WindowState = FormWindowState.Maximized

        _ReportViewerForm.Show()

        MyBase.PrintPreviewButtonClick(sender, e)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to show in the report")
      End If

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in PrintPreviewButtonClick method.", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Events"
  Private Sub grdinventory_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdInventory.EditModeOff
    Try
      Dim RowNumber As Int32
      Dim ColumnNumber As Int32
      Dim _ItemID As Int32

      If grdInventory.ActiveSheet Is Nothing OrElse grdInventory.ActiveSheet.ActiveCell Is Nothing Then Exit Sub

      RowNumber = grdInventory.Sheets(0).ActiveCell.Row.Index
      ColumnNumber = grdInventory.Sheets(0).ActiveCell.Column.Index

      If ColumnNumber >= enInventoryColumns.Item_Code AndAlso ColumnNumber < enInventoryColumns.Item_Code + _ItemCodeColumnsCount Then
        Dim ItemTableAdapter As New ItemTableAdapter

        If Int32.TryParse(Me.grdInventory.ActiveSheet.GetText(RowNumber, enInventoryColumns.Item_ID), _ItemID) Then
          Me.StockBar1.ShowSummary(Me.LoginInfoObject.CompanyID, _ItemID, 0, 0)
        End If
      End If

      With _InventoryDetailDataTable
        .TotalAmountColumn.Expression = TotalAmountColumnExpression
        .TotalQtyColumn.Expression = TotalQtyColumnExpression
      End With

      ShowTotal()

      If grdInventory.ActiveSheet.ActiveRowIndex = grdInventory.ActiveSheet.RowCount - 1 Then
        If grdInventory.ActiveSheet.GetText(grdInventory.ActiveSheet.ActiveRowIndex, enInventoryColumns.Item_Code) <> "" Then
          AddItem()
        End If
      End If

    Catch ex As Exception
      Dim objEx As New QuickExceptionAdvanced("Exception in EditModeOff event of grdInventory.", ex)
      objEx.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub AddAmountColumn()
    Try
      'Dim AmountColumnExpression As String
      'Dim QtyTotalColumnExpression As String

      'With _InventoryDetailDataTable
      '  AmountColumnExpression = .Inventory_Qty_Size01Column.ColumnName & "*" & .Inventory_Rate_Size01Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size02Column.ColumnName & "*" & .Inventory_Rate_Size02Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size03Column.ColumnName & "*" & .Inventory_Rate_Size03Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size04Column.ColumnName & "*" & .Inventory_Rate_Size04Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size05Column.ColumnName & "*" & .Inventory_Rate_Size05Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size06Column.ColumnName & "*" & .Inventory_Rate_Size06Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size07Column.ColumnName & "*" & .Inventory_Rate_Size07Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size08Column.ColumnName & "*" & .Inventory_Rate_Size08Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size09Column.ColumnName & "*" & .Inventory_Rate_Size09Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size10Column.ColumnName & "*" & .Inventory_Rate_Size10Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size11Column.ColumnName & "*" & .Inventory_Rate_Size11Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size12Column.ColumnName & "*" & .Inventory_Rate_Size12Column.ColumnName _
      '    & "+" & .Inventory_Qty_Size13Column.ColumnName & "*" & .Inventory_Rate_Size13Column.ColumnName
      'End With

      'Me._InventoryDetailDataTable.Columns.Add("Amount", System.Type.GetType("System.Double"), AmountColumnExpression)

    Catch ex As Exception
      Dim objEx As New QuickExceptionAdvanced("Exception in adding amount column", ex)
      Throw objEx
    End Try
  End Sub

  Private Sub frmSalesInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      'AddAmountColumn()
      With Me.grdInventory
        .LoginInfoObject = Me.LoginInfoObject
        .ItemSheetView = Me.grdInventory_Sheet1
        .ItemCodeFirstColumnIndex = enInventoryColumns.Item_Code
        .ItemDescColumnIndex = enInventoryColumns.Item_Description + General.ItemCodeColumnsCount
        .ItemIDColumnIndex = enInventoryColumns.Item_ID
        '.ItemRateFirstColumnIndex = enInventoryColumns.Rate1 + General.ItemCodeColumnsCount
        .SetItemCodeColumns(DirectCast(Me._InventoryDetailDataTable, DataTable))
        .Sheets(0).DataSource = Me._InventoryDetailDataTable
      End With
      _ItemCodeColumnsCount = General.ItemCodeColumnsCount
      Dim _SettingValue As String = String.Empty
      _SettingValue = DatabaseCache.GetSettingValue(SETTING_ID_DefaultWarehouseID & Me.DocumentType.ToString)
      If Not Int32.TryParse(_SettingValue, _DefaultWarehouseID) Then _DefaultWarehouseID = 0
      'Me.grdInventory.SetItemCodeColumns()

      Me.PartyComboBox1.EntityType = Constants.EntityTypes.Supplier
      Me.PartyComboBox1.LoadParties(Me.LoginInfoObject.CompanyID)
      Me.PartyCategoryComboBox.EntityType = EntityTypes.CustomerCategory
      Me.PartyCategoryComboBox.LoadParties(Me.LoginInfoObject.CompanyID)
      Me.CompanyComboBox1.LoadAllCompanies(Me.LoginInfoObject.ParentCompanyID)
      Me.grdInventory.ShowDeleteRowButton(Me.grdInventory_Sheet1) = True
      '_ItemCodeColumnsCount = DatabaseCache.GetItemLeveling.Rows.Count
      SetGridLayout()
      uccSaleDate.Value = Now

      AddItem()

      Me.grpMasterInformation.Width = Me.Width - Me.grpMasterInformation.Left - 20
      Me.grdInventory.Width = Me.grpMasterInformation.Width

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to load form", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub DiscountTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiscountTextBox.TextChanged
    Try
      ShowTotal()

    Catch ex As Exception
      Dim _QuickException As New QuickDALLibrary.QuickExceptionAdvanced("Exception in DiscountTextBox TextChanged event method of InventoryForm.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub SalesTaxTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesTaxTextBox.TextChanged
    Try
      ShowTotal()

    Catch ex As Exception
      Dim _QuickException As New QuickDALLibrary.QuickExceptionAdvanced("Exception in SalesTaxTextBox TextChanged event method of InventoryForm.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

  Public Sub New()
    'Dim ItemMaskString As String

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.FormDataSet.Tables.Add(Me._InventoryDataTable)
    Me.FormDataSet.Tables.Add(Me._InventoryDetailDataTable)
    Me.uccSaleDate.Format = Constants.FORMAT_DATE_FOR_USER
    Me.CurrentRecordDataRow = Me._CurrentInventoryDataRow
    If DatabaseCache.GetItemLeveling IsNot Nothing Then
      Me._ItemCodeColumnsCount = DatabaseCache.GetItemLeveling.Rows.Count
    Else
      Me._ItemCodeColumnsCount = 1
    End If

    'With _InventoryDetailDataTable
    '  TotalAmountColumnExpression = .Inventory_Qty_Size01Column.ColumnName & "*" & .Inventory_Rate_Size01Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size02Column.ColumnName & "*" & .Inventory_Rate_Size02Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size03Column.ColumnName & "*" & .Inventory_Rate_Size03Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size04Column.ColumnName & "*" & .Inventory_Rate_Size04Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size05Column.ColumnName & "*" & .Inventory_Rate_Size05Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size06Column.ColumnName & "*" & .Inventory_Rate_Size06Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size07Column.ColumnName & "*" & .Inventory_Rate_Size07Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size08Column.ColumnName & "*" & .Inventory_Rate_Size08Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size09Column.ColumnName & "*" & .Inventory_Rate_Size09Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size10Column.ColumnName & "*" & .Inventory_Rate_Size10Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size11Column.ColumnName & "*" & .Inventory_Rate_Size11Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size12Column.ColumnName & "*" & .Inventory_Rate_Size12Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size13Column.ColumnName & "*" & .Inventory_Rate_Size13Column.ColumnName

    '  TotalQtyColumnExpression = .Inventory_Qty_Size01Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size02Column.ColumnName & "+" & .Inventory_Qty_Size03Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size04Column.ColumnName & "+" & .Inventory_Qty_Size05Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size06Column.ColumnName & "+" & .Inventory_Qty_Size07Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size08Column.ColumnName & "+" & .Inventory_Qty_Size09Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size10Column.ColumnName & "+" & .Inventory_Qty_Size11Column.ColumnName _
    '    & "+" & .Inventory_Qty_Size12Column.ColumnName & "+" & .Inventory_Qty_Size13Column.ColumnName
    'End With
  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub

  'Private Sub grdInventory_EnterCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EnterCellEventArgs) Handles grdInventory.EnterCell
  '  Try
  '    If Me.grdInventory_Sheet1.ActiveColumnIndex = enInventoryColumns.Item_Code Then
  '      Me.grdInventory_Sheet1.ActiveCell.CellType = geItemCellType
  '    End If
  '  Catch ex As Exception
  '    Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to load form", ex)
  '    QuickExceptionObject.show(me.logininfoObject)
  '  End Try
  'End Sub

End Class