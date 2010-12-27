Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common
Imports System.ComponentModel
Imports QuickDAL.QuickInventoryDataSetTableAdapters

Public Class SalesInvoiceForm

#Region "Declaration"

  'Data Adapters
  Dim _InventoryTableAdapterObject As New InventoryTableAdapter
  Dim _InventoryDetailTableAdapterObject As New InventoryDetailTableAdapter
  Dim _ItemTableAdapter As New ItemTableAdapter
  'Data Tables
  Private WithEvents _InventoryDataTable As New InventoryDataTable
  Private WithEvents _InventoryDetailDataTable As New InventoryDetailDataTable
  Private WithEvents _ItemDataTable As New ItemDataTable
  'Data Rows
  Private _CurrentInventoryDataRow As InventoryRow

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
    Qty01
    Qty02
    Qty03
    Qty04
    Qty05
    Qty06
    Qty07
    Qty08
    Qty09
    Qty10
    Qty11
    Qty12
    Qty13
    Rate1
    Rate2
    Rate3
    Rate4
    Rate5
    Rate6
    Rate7
    Rate8
    Rate9
    Rate10
    Rate11
    Rate12
    Rate13
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
      If PartyControl1.IsValid <> MethodResult.Yes Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You must enter valid party code.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Asterisk)
        PartyControl1.Focus()
        Return False
      End If
      If PartyCategoryComboBox.IsValid <> MethodResult.Yes Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You must enter valid party category.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Asterisk)
        PartyCategoryComboBox.Focus()
        Return False
      End If

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to check if record is valid", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim InventoryID As Int32
      Dim InventoryDataRow As InventoryRow
      Dim _InventoryDetailDataRow As InventoryDetailRow
      Dim _LastInventoryNo As Object
      Dim _NewInventoryNo As String = String.Empty
      Dim _InventoryNoChanged As Boolean = False
      Dim ItemsUsedCollection As New Collection
      Dim _LikeOperatorPattern As String     'It will hold the value which will be used with like operator to get maximum value.

      'It is necessary to remove row before set EditMode = False because editmode increases extra blank row.
      Me.grdInventory.EditMode = False
      Me.grdInventory_Sheet1.SetActiveCell(Me.grdInventory_Sheet1.RowCount - 1, enInventoryColumns.Item_Code)  'For some unknown reason new version of farpoint is not working without this line.

            Debug.WriteLine(_InventoryDetailDataTable.Rows.Count)
      _InventoryDetailDataTable.Rows.RemoveAt(_InventoryDetailDataTable.Rows.Count - 1)
            Debug.WriteLine(_InventoryDetailDataTable.Rows.Count)

      If Not IsValid() Then Exit Function

      If _CurrentInventoryDataRow Is Nothing OrElse _
      PartyCategoryComboBox.PartyCode <> Common.GetDocumentNoSegment(DocumentNoSegments.PartyCategoryCode, _CurrentInventoryDataRow.Inventory_No, DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_SalesInvoice)).Trim _
      Then

        _LikeOperatorPattern = Common.GenerateNextDocumentNo(PartyCategoryComboBox.PartyCode, String.Empty, DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_SalesInvoice), True)
        _LastInventoryNo = _InventoryTableAdapterObject.GetMaxInventoryNoByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType, _LikeOperatorPattern)
        If _LastInventoryNo Is Nothing Then
          _NewInventoryNo = Common.GenerateNextDocumentNo(PartyCategoryComboBox.PartyCode, "", DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_SalesInvoice), False)
        Else
          _NewInventoryNo = Common.GenerateNextDocumentNo(PartyCategoryComboBox.PartyCode, _LastInventoryNo.ToString, DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_SalesInvoice), False)
        End If

      End If

      If _CurrentInventoryDataRow Is Nothing Then
        InventoryDataRow = _InventoryDataTable.NewInventoryRow
        InventoryID = _InventoryTableAdapterObject.GetNewInventoryIDByCoID(LoginInfoObject.CompanyID).Value

        InventoryDataRow.Inventory_ID = InventoryID
        InventoryDataRow.RecordStatus_ID = 1
        InventoryDataRow.DocumentType_ID = Convert.ToInt16(Me.DocumentType)
        _CurrentInventoryDataRow = InventoryDataRow
      Else
        'In case of updated only common properties need to be set.
        If _NewInventoryNo <> String.Empty Then _InventoryNoChanged = True
      End If

      'Common values to set in case of insert/ update.
      With _CurrentInventoryDataRow
        If _NewInventoryNo <> String.Empty Then .Inventory_No = _NewInventoryNo 'If _NewInventoryNo is empty then record is modified without inventory no. change
        .Co_ID = LoginInfoObject.CompanyID
        .Party_ID = Me.PartyControl1.PartyID
        .Category_Party_ID = Me.PartyCategoryComboBox.PartyID
        .Payment_Mode = 0
        .Remarks = RemarksTextBox.Text
        .LogisticService = Me.LogisticServiceTextBox.Text
        .Discount = Me.DiscountTextBox.IntegerNumber
        .SalesTax = Me.SalesTaxTextBox.IntegerNumber
        .Source_Document_Co_ID = Me.CompanyComboBox1.CompanyID
        .Source_Document_No = Cast.ToInt32(Me.SourceDocumentNoTextBox.Text)
        .Inventory_Date = Convert.ToDateTime(uccSaleDate.Value)
        .Stamp_DateTime = Common.SystemDateTime
        .Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
      End With

      'CurrentRecordDataRow is used by parent form.
      CurrentRecordDataRow = _CurrentInventoryDataRow
      If _CurrentInventoryDataRow.RowState = DataRowState.Detached Then
        _InventoryDataTable.Rows.Add(_CurrentInventoryDataRow)
      End If
      _InventoryTableAdapterObject.Update(_InventoryDataTable)

      If _NewInventoryNo <> String.Empty Then Me.txtSaleNo.Text = _NewInventoryNo

      'We should first update the deleted rows so that it does not effect next loop for update and insertion.
      _InventoryDetailTableAdapterObject.Update(_InventoryDetailDataTable.Select("", "", DataViewRowState.Deleted))

      With grdInventory_Sheet1
        For I As Int32 = 0 To _InventoryDetailDataTable.Rows.Count - 1
          _InventoryDetailDataRow = DirectCast(_InventoryDetailDataTable.Rows(I), InventoryDetailRow)

          If .IsRowDeleted(I) Then
            'Below code is done for soft deletion.
            _InventoryDetailDataRow.RejectChanges()
            _InventoryDetailDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
          ElseIf _InventoryDetailDataRow.RowState = DataRowState.Added Then
            _InventoryDetailDataRow.InventoryDetail_ID = Convert.ToInt32(_InventoryDetailTableAdapterObject.GetNewInventoryDetailIDByCoIDInventoryID(LoginInfoObject.CompanyID, _CurrentInventoryDataRow.Inventory_ID))
            _InventoryDetailDataRow.Inventory_ID = _CurrentInventoryDataRow.Inventory_ID
            _InventoryDetailDataRow.Co_ID = LoginInfoObject.CompanyID
            _InventoryDetailDataRow.DocumentType_ID = Me.DocumentType
          End If

          If _InventoryDetailDataRow.RowState <> DataRowState.Unchanged Then
            _InventoryDetailDataRow.Source_Document_Co_ID = Me.CompanyComboBox1.CompanyID
            _InventoryDetailDataRow.Source_Document_No = Cast.ToInt32(Me.SourceDocumentNoTextBox.Text)
            _InventoryDetailDataRow.Warehouse_ID = _DefaultWarehouseID
            _InventoryDetailDataRow.Stamp_DateTime = Common.SystemDateTime
            _InventoryDetailDataRow.Stamp_UserID = LoginInfoObject.UserID
            If _InventoryDetailDataRow.RowState = DataRowState.Detached Then
              _InventoryDetailDataTable.Rows.Add(_InventoryDetailDataRow)
            End If

            For J As Int32 = 0 To _InventoryDetailDataTable.Columns.Count - 1
              Debug.WriteLine(_InventoryDetailDataTable.Columns(J).ColumnName & ":" & _InventoryDetailDataTable(I).Item(J).ToString)
            Next

            'This statement should be inside loop so that we can fetch new detail id properly
            If _InventoryDetailTableAdapterObject.Update(_InventoryDetailDataTable(I)) > 0 Then
              'Record is updated
              If _InventoryNoChanged Then
                QuickMessageBox.Show(Me.LoginInfoObject, "Document No is changed.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
                Return True
              End If
            Else
              'Record is not updated.
              Return False
            End If
          End If
        Next
      End With

      'Save the voucher
      Dim _VoucherTA As New VoucherTableAdapter
      Dim _VoucherDetailTA As New VoucherDetailTableAdapter
      Dim _VoucherTable As New VoucherDataTable
      Dim _VoucherDetailTable As New VoucherDetailDataTable
      Dim _VoucherRow As VoucherRow
      Dim _VoucherDetailRow As VoucherDetailRow
      Dim _VoucherID As Int32
      Dim _VoucherTypeID As Int32
      Dim _VoucherDetailID As Int16
      Dim _SalesCoaId As Int32
      Dim _PartyCoaId As Int32
      Dim _AmountTotal As Decimal

      'Get data from setting.
      _VoucherTypeID = Convert.ToInt32(DatabaseCache.GetSettingValue(SETTING_ID_SalesInvoiceVoucherType))
      _SalesCoaId = Convert.ToInt32(DatabaseCache.GetSettingValue(SETTING_ID_SalesCoaId))
      If _SalesCoaId = 0 Then _SalesCoaId = 2

      If Me.PartyControl1.CoaID > 0 Then
        _PartyCoaId = Me.PartyControl1.CoaID
      Else
        _PartyCoaId = Convert.ToInt32(DatabaseCache.GetSettingValue(SETTING_ID_CashCoaId))
        If _PartyCoaId = 0 Then _PartyCoaId = 1
      End If
      Decimal.TryParse(Me.TotalAmountLabel.Text, _AmountTotal)

      'Get vouchers posted for this invoice.
      _VoucherTable = _VoucherTA.GetByCoIDSourceIDSourceDocumentTypeID(LoginInfoObject.CompanyID, _CurrentInventoryDataRow.Inventory_ID, enuDocumentType.SalesInvoice)
      'If voucher is already posted for this invoice.
      If _VoucherTable.Rows.Count > 0 Then
        For Each _VoucherRow In _VoucherTable.Rows
          If _VoucherRow.VoucherType_ID = _VoucherTypeID Then
            _VoucherRow.Voucher_Date = _CurrentInventoryDataRow.Inventory_Date
            _VoucherRow.Stamp_DateTime = Common.SystemDateTime
            _VoucherRow.Stamp_UserID = LoginInfoObject.UserID
            _VoucherRow.Remarks = "S. No. " & _CurrentInventoryDataRow.Inventory_No
            'Get voucher detail.
            _VoucherDetailTable = _VoucherDetailTA.GetByCoIDVoucherID(_VoucherRow.Co_ID, _VoucherRow.Voucher_ID)
            For Each _VoucherDetailRow In _VoucherDetailTable.Rows
              If _VoucherDetailRow.DebitAmount = 0 Then
                _VoucherDetailRow.CreditAmount = _AmountTotal
              Else
                _VoucherDetailRow.DebitAmount = _AmountTotal
              End If
              _VoucherDetailRow.Narration = "S. No. " & _CurrentInventoryDataRow.Inventory_No
              _VoucherDetailRow.Stamp_DateTime = Common.SystemDateTime
              _VoucherDetailRow.Stamp_User_Id = LoginInfoObject.UserID
            Next
          End If
        Next
        _VoucherTA.Update(_VoucherTable)
        _VoucherDetailTA.Update(_VoucherDetailTable)
      Else
        _VoucherID = Convert.ToInt32(_VoucherTA.GetNewVoucherIDByCoID(LoginInfoObject.CompanyID))
        _VoucherTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherTypeID, _VoucherID.ToString, _CurrentInventoryDataRow.Inventory_Date, DocumentStatuses.General_Posted, "S. No. " & _CurrentInventoryDataRow.Inventory_No, LoginInfoObject.UserID, Common.SystemDateTime, _CurrentInventoryDataRow.Inventory_ID, Convert.ToInt16(enuDocumentType.SalesInvoice), Nothing, Constants.DocumentStatuses.General_Posted)
        'Cash entry
        _VoucherDetailID = Convert.ToInt16(_VoucherDetailTA.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherID))
        _VoucherDetailTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherDetailID, _PartyCoaId, "S. No. " & _CurrentInventoryDataRow.Inventory_No, _AmountTotal, 0, LoginInfoObject.UserID, Common.SystemDateTime, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, RecordStatuses.Inserted)
        'Sales entry
        _VoucherDetailID = Convert.ToInt16(_VoucherDetailTA.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherID))
        _VoucherDetailTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherDetailID, _SalesCoaId, "S. No. " & _CurrentInventoryDataRow.Inventory_No, 0, _AmountTotal, LoginInfoObject.UserID, Common.SystemDateTime, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, RecordStatuses.Inserted)
      End If

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
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
        'Faisal (16-Feb-10): I have hard coded 0 index because typed datatable will only have one row
        '                    filtered rows will be contained in the untyped datatable of the parent form. So index is used for that.
        'Me._CurrentInventoryDataRow = CType(Me._InventoryDataTable.Rows(Me.CurrentRecordIndex), InventoryRow)
        Me._CurrentInventoryDataRow = CType(Me._InventoryDataTable.Rows(0), InventoryRow)
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
        Me.RemarksTextBox.Text = Me._CurrentInventoryDataRow.Remarks
        If Not Me._CurrentInventoryDataRow.IsLogisticServiceNull Then
          Me.LogisticServiceTextBox.Text = Me._CurrentInventoryDataRow.LogisticService
        End If
        Me.PartyControl1.PartyID = Me._CurrentInventoryDataRow.Party_ID
        If Me._CurrentInventoryDataRow.IsCategory_Party_IDNull Then
          Me.PartyCategoryComboBox.SelectedRow = Nothing
          Me.PartyCategoryComboBox.Text = ""
        Else
          Me.PartyCategoryComboBox.PartyID = Me._CurrentInventoryDataRow.Category_Party_ID
        End If
        If Not _CurrentInventoryDataRow.IsDiscountNull Then
          Me.DiscountTextBox.Text = _CurrentInventoryDataRow.Discount.ToString
        End If
        If Not _CurrentInventoryDataRow.IsSalesTaxNull Then
          Me.SalesTaxTextBox.Text = _CurrentInventoryDataRow.SalesTax.ToString
        End If
        If Not _CurrentInventoryDataRow.IsSource_Document_Co_IDNull Then
          Me.CompanyComboBox1.CompanyID = _CurrentInventoryDataRow.Source_Document_Co_ID
        End If
        If Not _CurrentInventoryDataRow.IsSource_Document_NoNull Then
          Me.SourceDocumentNoTextBox.Text = _CurrentInventoryDataRow.Source_Document_No.ToString
        End If

        _InventoryDetailDataTable = _InventoryDetailTableAdapterObject.GetByCoIDInventoryID(Me._CurrentInventoryDataRow.Co_ID, Me._CurrentInventoryDataRow.Inventory_ID)
        Me.grdInventory.SetItemCodeColumns(DirectCast(_InventoryDetailDataTable, DataTable))

        'If _InventoryDetailDataTable.Rows.Count > 0 Then
        '  If Not _InventoryDetailDataTable(0).IsSource_Document_Co_IDNull Then
        '    Me.CompanyComboBox1.CompanyID = _InventoryDetailDataTable(0).Source_Document_Co_ID
        '  End If
        '  If Not _InventoryDetailDataTable(0).IsSource_Document_NoNull Then
        '    Me.SourceDocumentNoTextBox.Text = _InventoryDetailDataTable(0).Source_Document_No.ToString
        '  End If
        'End If
        Me.grdInventory_Sheet1.DataSource = Me._InventoryDetailDataTable
        Me.grdInventory.ShowDeleteRowButton(Me.grdInventory_Sheet1) = True
        _InventoryDetailDataTable.TotalAmountColumn.Expression = TotalAmountColumnExpression
        _InventoryDetailDataTable.TotalQtyColumn.Expression = TotalQtyColumnExpression

        Me.grdInventory_Sheet1.RecalculateTotals(enInventoryColumns.Qty01)
        ShowTotal()
        Me.grdInventory.RefreshAllItemCodeAndDescriptions(False)
        _InventoryDetailDataTable.AcceptChanges() 'When updating item code and description, it sets all rows to be modified.
        AddItem()   'Add item call should be after acceptchanges otherwise it will make the last as unchnaged instead of added.
        SetGridLayout()
      End If
      'Me.addi()
      MyBase.ShowRecord()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show record", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  <Description("add new row")> _
  Private Sub AddItem()
    Try
      Dim InventoryDetailDataRow As InventoryDetailRow

      InventoryDetailDataRow = _InventoryDetailDataTable.NewInventoryDetailRow()
      With InventoryDetailDataRow
        .Co_ID = LoginInfoObject.CompanyID
        .DocumentType_ID = Convert.ToInt16(Me.DocumentType)
        .InventoryDetail_ID = _InventoryDetailDataTable.Rows.Count + 1
        .Warehouse_ID = 0
        .Inventory_ID = 0
        .Inventory_Qty_Size01 = 0
        .Inventory_Qty_Size02 = 0
        .Inventory_Qty_Size03 = 0
        .Inventory_Qty_Size04 = 0
        .Inventory_Qty_Size05 = 0
        .Inventory_Qty_Size06 = 0
        .Inventory_Qty_Size07 = 0
        .Inventory_Qty_Size08 = 0
        .Inventory_Qty_Size09 = 0
        .Inventory_Qty_Size10 = 0
        .Inventory_Qty_Size11 = 0
        .Inventory_Qty_Size12 = 0
        .Inventory_Qty_Size13 = 0
        .Inventory_Rate_Size01 = 0
        .Inventory_Rate_Size02 = 0
        .Inventory_Rate_Size03 = 0
        .Inventory_Rate_Size04 = 0
        .Inventory_Rate_Size05 = 0
        .Inventory_Rate_Size06 = 0
        .Inventory_Rate_Size07 = 0
        .Inventory_Rate_Size08 = 0
        .Inventory_Rate_Size09 = 0
        .Inventory_Rate_Size10 = 0
        .Inventory_Rate_Size11 = 0
        .Inventory_Rate_Size12 = 0
        .Inventory_Rate_Size13 = 0
        .Item_ID = 0
        .Stamp_DateTime = Common.SystemDateTime
        .Stamp_UserID = 0

        _InventoryDetailDataTable.Rows.Add(InventoryDetailDataRow)
      End With

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to add item in grid", ex)
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
        If Not Me.grdInventory_Sheet1.IsRowDeleted(I) Then
          TotalAmount += Val(Me.grdInventory_Sheet1.GetValue(I, enInventoryColumns.TotalAmount + General.ItemCodeColumnsCount))
          TotalQty += Val(Me.grdInventory_Sheet1.GetValue(I, enInventoryColumns.TotalQty + General.ItemCodeColumnsCount))
        Else
          'If row is deleted then don't include it in totals.
        End If
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
          Case enInventoryColumns.Qty01 + _ItemCodeColumnsCount   ' _InventoryDetailDataTable.Inventory_Qty_Size01Column.ColumnName
            SheetColumn.Label = "100"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty02 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size02Column.ColumnName
            SheetColumn.Label = "110"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty03 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Qty_Size03Column.ColumnName
            SheetColumn.Label = "120"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty04 + _ItemCodeColumnsCount '_InventoryDetailDataTable.Inventory_Qty_Size04Column.ColumnName
            SheetColumn.Label = "130"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty05 + _ItemCodeColumnsCount   '_InventoryDetailDataTable.Inventory_Qty_Size05Column.ColumnName
            SheetColumn.Label = "140"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty06 + _ItemCodeColumnsCount   '_InventoryDetailDataTable.Inventory_Qty_Size06Column.ColumnName
            SheetColumn.Label = "150"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty07 + _ItemCodeColumnsCount  '_InventoryDetailDataTable.Inventory_Qty_Size07Column.ColumnName
            SheetColumn.Label = "160"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty08 + _ItemCodeColumnsCount '_InventoryDetailDataTable.Inventory_Qty_Size08Column.ColumnName
            SheetColumn.Label = "170"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty09 + _ItemCodeColumnsCount   '_InventoryDetailDataTable.Inventory_Qty_Size09Column.ColumnName
            SheetColumn.Label = "180"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty10 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size10Column.ColumnName
            SheetColumn.Label = "190"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty11 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size11Column.ColumnName
            SheetColumn.Label = "CM"
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty12 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size12Column.ColumnName
            SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Qty13 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Qty_Size13Column.ColumnName
            SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case enInventoryColumns.Rate1 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size01Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate2 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size02Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate3 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size03Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate4 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size04Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate5 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size05Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate6 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Rate_Size06Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate7 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Rate_Size07Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate8 + _ItemCodeColumnsCount  ' _InventoryDetailDataTable.Inventory_Rate_Size08Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate9 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size09Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate10 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size10Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate11 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size11Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate12 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size12Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
          Case enInventoryColumns.Rate13 + _ItemCodeColumnsCount ' _InventoryDetailDataTable.Inventory_Rate_Size13Column.ColumnName
            SheetColumn.Visible = ShowRateColumns
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

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 13-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Description of the method goes here ...
  ''' </summary>
  Protected Overrides Sub SearchButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    If Me.DocumentType = enuDocumentType.SalesInvoice Then

      Try
        Dim _SearchForm As New QuickBaseForms.SearchForm

        _SearchForm.LoginInfoObject = DirectCast(Me.LoginInfoObject.Clone, LoginInfo)
        _SearchForm.SearchOption = QuickBaseForms.SearchForm.SearchOptionIDs.SalesInvoice
        _SearchForm.DocumentType = enuDocumentType.SalesInvoice

        _SearchForm.ShowDialog()
        If _SearchForm.SearchResultUnTypedDataTable IsNot Nothing AndAlso _SearchForm.SearchResultUnTypedDataTable.Rows.Count > 0 Then
          With _SearchForm.SearchResultUnTypedDataTable.Rows(0)
            Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetByCoIDInventoryID(DirectCast(.Item(_InventoryDataTable.Co_IDColumn.ColumnName), Int16), DirectCast(.Item(_InventoryDataTable.Inventory_IDColumn.ColumnName), Int32))
            ShowRecord()
          End With
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "No Record Found", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        End If
        _SearchForm.Close()

        MyBase.SearchButtonClick(sender, e)

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SearchButtonClick of SalesInvoiceForm.", ex)
        _qex.Show(Me.LoginInfoObject)
      End Try
    End If
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 16-Feb-10
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Protected Overrides Sub FilterButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    If Me.DocumentType = enuDocumentType.SalesInvoice Then

      Try
        Dim _FilterForm As New QuickBaseForms.FilterForm

        _FilterForm.LoginInfoObject = DirectCast(Me.LoginInfoObject.Clone, LoginInfo)
        _FilterForm.SearchOption = QuickBaseForms.FilterForm.SearchOptionIDs.SalesInvoice
        _FilterForm.DocumentType = enuDocumentType.SalesInvoice

        _FilterForm.ShowDialog()
        If _FilterForm.SearchResultUnTypedDataTable IsNot Nothing AndAlso _FilterForm.SearchResultUnTypedDataTable.Rows.Count > 0 Then
          Me.FormDataTable = _FilterForm.SearchResultUnTypedDataTable.Copy()
          Me.CurrentRecordIndex = 0
          MoveFirstButtonClick(sender, e)

        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "No Record Found", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)

        End If
        _FilterForm.Close()

        MyBase.FilterButtonClick(sender, e)

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in FilterButtonClick of SalesInvoiceForm.", ex)
        _qex.Show(Me.LoginInfoObject)
      End Try
    End If
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 16-Feb-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Protected Overrides Sub FilterClearButtonClick(ByVal sender As Object, ByVal e As EventArgs)
    Try
      Me._InventoryDataTable = Nothing
      Me.CurrentRecordIndex = 0
      MyBase.FilterClearButtonClick(sender, e)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in FilterClearButtonClick of SalesInvoiceForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub


  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.FormMode = FormModes.FilterMode Then
        Me.CurrentRecordIndex = 0
        With Me.FormDataTable.Rows(Me.CurrentRecordIndex)
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetByCoIDInventoryID(Convert.ToInt16(.Item(_InventoryDataTable.Co_IDColumn.ColumnName)), Convert.ToInt32(.Item(_InventoryDataTable.Inventory_IDColumn.ColumnName)))
        End With

      Else
      Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetFirstByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)

      End If

      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.FormMode = FormModes.FilterMode Then
        If Me.CurrentRecordIndex < Me.FormDataTable.Rows.Count - 1 Then
          Me.CurrentRecordIndex += 1
          With Me.FormDataTable.Rows(Me.CurrentRecordIndex)
            'MsgBox(Me.CurrentRecordIndex)
            'MsgBox(Convert.ToInt16(.Item(_InventoryDataTable.Co_IDColumn.ColumnName)).ToString & ":" & Convert.ToInt32(.Item(_InventoryDataTable.Inventory_IDColumn.ColumnName)).ToString)
            Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetByCoIDInventoryID(Convert.ToInt16(.Item(_InventoryDataTable.Co_IDColumn.ColumnName)), Convert.ToInt32(.Item(_InventoryDataTable.Inventory_IDColumn.ColumnName)))
          End With

        End If

      Else
        If Me._CurrentInventoryDataRow IsNot Nothing Then
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetNextByCoIDInventoryIDDocumentTypeID(Me._CurrentInventoryDataRow.Co_ID, Me._CurrentInventoryDataRow.Inventory_ID, Me.DocumentType)

        Else
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetNextByCoIDInventoryIDDocumentTypeID(LoginInfoObject.CompanyID, 0, Me.DocumentType)

        End If

      End If

      MyBase.MoveNextButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move next", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.FormMode = FormModes.FilterMode Then
        If Me.CurrentRecordIndex > 0 Then
          Me.CurrentRecordIndex -= 1
          With Me.FormDataTable.Rows(Me.CurrentRecordIndex)
            Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetByCoIDInventoryID(Convert.ToInt16(.Item(_InventoryDataTable.Co_IDColumn.ColumnName)), Convert.ToInt32(.Item(_InventoryDataTable.Inventory_IDColumn.ColumnName)))
          End With

        End If

      Else
        If Me._CurrentInventoryDataRow IsNot Nothing Then
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetPreviousByCoIDInventoryIDDocumentTypeID(Me._CurrentInventoryDataRow.Co_ID, Me._CurrentInventoryDataRow.Inventory_ID, Me.DocumentType)

        Else
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetPreviousByCoIDInventoryIDDocumentTypeID(LoginInfoObject.CompanyID, 0, Me.DocumentType)

        End If

      End If

      MyBase.MovePreviousButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move previous", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.FormMode = FormModes.FilterMode Then
        Me.CurrentRecordIndex = Me.FormDataTable.Rows.Count - 1
        With Me.FormDataTable.Rows(Me.CurrentRecordIndex)
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetByCoIDInventoryID(Convert.ToInt16(.Item(_InventoryDataTable.Co_IDColumn.ColumnName)), Convert.ToInt32(.Item(_InventoryDataTable.Inventory_IDColumn.ColumnName)))
        End With

      Else
        If Me._CurrentInventoryDataRow IsNot Nothing Then
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetLastByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)

        Else
          Me._InventoryDataTable = Me._InventoryTableAdapterObject.GetLastByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)

        End If

      End If

      MyBase.MoveLastButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of SalesInvoiceForm", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  'Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
  '  Try
  '    Cursor = Cursors.WaitCursor

  '    SaveRecord()

  '  Catch ex As Exception
  '    Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save button click", ex)
  '    QuickExceptionObject.Show(Me.LoginInfoObject)
  '  Finally
  '    Cursor = Cursors.Default
  '  End Try
  'End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me._CurrentInventoryDataRow = Nothing
      Me._CurrentRecordDataRow = Nothing
      Me._InventoryDataTable.Rows.Clear()
      Me._InventoryDetailDataTable.Rows.Clear()
      Me.TotalAmountLabel.Text = "0"
      Me.TotalQtyLabel.Text = "0"
      MyBase.CancelButtonClick(sender, e)
      uccSaleDate.Value = Common.SystemDateTime
      Me.grdInventory_Sheet1.RecalculateTotals(enInventoryColumns.Qty01)

      AddItem()
      SetGridLayout()
      'If Me.grdInventory.Sheets(0).DataSource Is Nothing Then MessageBox.Show("it is nothing")
      'Me.grdInventory.Sheets(0).DataSource = _InventoryDetailDataTable
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to cancel button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me._CurrentInventoryDataRow IsNot Nothing Then

        If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
          Me._CurrentInventoryDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted

          SaveRecord()
          'Below line is necessary so that parent form don't ask for record change confirmation.
          Me.CurrentRecordDataRow = Nothing
          MyBase.DeleteButtonClick(sender, e)
        Else
        End If

        QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)

      Else

        QuickMessageBox.Show(Me.LoginInfoObject, "There is no record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)

      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to delete button click", ex)
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
          Case enuDocumentType.SalesInvoice
            _ParameterValues.Add("ReportName", "GatePass")
            _ReportViewerForm.FormulaValues.Add("PartyLabel", "To:")
            _ReportViewerForm.FormulaValues.Add("InventoryNoLabel", "GP. No.:")
          Case enuDocumentType.SalesInvoiceReturn
            _ParameterValues.Add("ReportName", "Sales Invoice Return")
            _ReportViewerForm.FormulaValues.Add("@PartyLabel", "From:")
            _ReportViewerForm.FormulaValues.Add("@InventoryNoLabel", "RB. No.:")
        End Select
        _ParameterValues.Add("CurrentCompany", Me.LoginInfoObject.CompanyDesc)
        _ParameterValues.Add("@DocumentTypeID", Convert.ToInt32(Me.DocumentType).ToString)
        _ParameterValues.Add("@CoID", Me.LoginInfoObject.CompanyID.ToString)
        _ParameterValues.Add("@InventoryID", Me._CurrentInventoryDataRow.Inventory_ID.ToString)

        _SelectionFormula = ""

        _ReportViewerForm.ParameterValues = _ParameterValues
        _ReportViewerForm.RecordSelectionFormula = _SelectionFormula
        _ReportViewerForm.Report = New QuickReports.InventoryGatePassReport
        _ReportViewerForm.WindowState = FormWindowState.Maximized

        _ReportViewerForm.Show()

        MyBase.PrintPreviewButtonClick(sender, e)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to show in the report")
      End If

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in showing report preview.", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Events"
  Private Sub grdinventory_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdInventory.EditModeOff
    Try
      'Dim RowNumber As Int32
      'Dim ColumnNumber As Int32
      'Dim _ItemID As Int32

      If grdInventory.ActiveSheet Is Nothing OrElse grdInventory.ActiveSheet.ActiveCell Is Nothing Then Exit Sub

      'RowNumber = grdInventory.Sheets(0).ActiveCell.Row.Index
      'ColumnNumber = grdInventory.Sheets(0).ActiveCell.Column.Index

      'If ColumnNumber >= enInventoryColumns.Item_Code AndAlso ColumnNumber < enInventoryColumns.Item_Code + _ItemCodeColumnsCount Then
      '  Dim ItemTableAdapter As New ItemTableAdapter

      'If Int32.TryParse(Me.grdInventory.ActiveSheet.GetText(RowNumber, enInventoryColumns.Item_ID), _ItemID) Then
      '  Me.StockBar1.ShowSummary(Me.LoginInfoObject.CompanyID, _ItemID, 0, 0)
      'End If
      'End If

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
      Dim AmountColumnExpression As String
      'Dim QtyTotalColumnExpression As String

      With _InventoryDetailDataTable
        AmountColumnExpression = "ISNULL(" & .Inventory_Qty_Size01Column.ColumnName & "*" & .Inventory_Rate_Size01Column.ColumnName _
          & ")+" & .Inventory_Qty_Size02Column.ColumnName & "*" & .Inventory_Rate_Size02Column.ColumnName _
          & "+" & .Inventory_Qty_Size03Column.ColumnName & "*" & .Inventory_Rate_Size03Column.ColumnName _
          & "+" & .Inventory_Qty_Size04Column.ColumnName & "*" & .Inventory_Rate_Size04Column.ColumnName _
          & "+" & .Inventory_Qty_Size05Column.ColumnName & "*" & .Inventory_Rate_Size05Column.ColumnName _
          & "+" & .Inventory_Qty_Size06Column.ColumnName & "*" & .Inventory_Rate_Size06Column.ColumnName _
          & "+" & .Inventory_Qty_Size07Column.ColumnName & "*" & .Inventory_Rate_Size07Column.ColumnName _
          & "+" & .Inventory_Qty_Size08Column.ColumnName & "*" & .Inventory_Rate_Size08Column.ColumnName _
          & "+" & .Inventory_Qty_Size09Column.ColumnName & "*" & .Inventory_Rate_Size09Column.ColumnName _
          & "+" & .Inventory_Qty_Size10Column.ColumnName & "*" & .Inventory_Rate_Size10Column.ColumnName _
          & "+" & .Inventory_Qty_Size11Column.ColumnName & "*" & .Inventory_Rate_Size11Column.ColumnName _
          & "+" & .Inventory_Qty_Size12Column.ColumnName & "*" & .Inventory_Rate_Size12Column.ColumnName _
          & "+" & .Inventory_Qty_Size13Column.ColumnName & "*" & .Inventory_Rate_Size13Column.ColumnName
      End With

      Me._InventoryDetailDataTable.Columns.Add("Amount", System.Type.GetType("System.Double"), AmountColumnExpression)

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
        .ItemQtyFirstColumnIndex = enInventoryColumns.Qty01
        .ItemRateFirstColumnIndex = enInventoryColumns.Rate1 + General.ItemCodeColumnsCount
        .SetItemCodeColumns(DirectCast(Me._InventoryDetailDataTable, DataTable))
        .Sheets(0).DataSource = Me._InventoryDetailDataTable
        .ItemSummaryBarObject = StockBar1
      End With
      _ItemCodeColumnsCount = General.ItemCodeColumnsCount
      Dim _SettingValue As String = String.Empty
      _SettingValue = DatabaseCache.GetSettingValue(SETTING_ID_DefaultWarehouseID & SETTING_ID_SEPERATOR & Me.DocumentType.ToString)
      If Not Int32.TryParse(_SettingValue, _DefaultWarehouseID) Then _DefaultWarehouseID = 0

      Me.PartyControl1.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.EntrybyUser
      Me.PartyControl1.EntityType = Constants.EntityTypes.Customer
      Me.PartyControl1.LoadParties(Me.LoginInfoObject.CompanyID)
      Me.PartyCategoryComboBox.EntityType = EntityTypes.CustomerCategory
      Me.PartyCategoryComboBox.LoadParties(Me.LoginInfoObject.CompanyID)
      Me.CompanyComboBox1.LoadAllCompanies(Me.LoginInfoObject.ParentCompanyID)
      Me.grdInventory.ShowDeleteRowButton(Me.grdInventory_Sheet1) = True
      '_ItemCodeColumnsCount = DatabaseCache.GetItemLeveling.Rows.Count
      SetGridLayout()
      uccSaleDate.Value = Now

      AddItem()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to load form", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub DiscountTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiscountTextBox.TextChanged
    Try
      ShowTotal()

    Catch ex As Exception
      Dim _QuickException As New QuickDALLibrary.QuickExceptionAdvanced("Exception in DiscountTextBox TextChanged event method of SalesInvoiceForm.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub SalesTaxTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesTaxTextBox.TextChanged
    Try
      ShowTotal()

    Catch ex As Exception
      Dim _QuickException As New QuickDALLibrary.QuickExceptionAdvanced("Exception in SalesTaxTextBox TextChanged event method of SalesInvoiceForm.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

  Public Sub New()
    'Dim ItemMaskString As String

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DocumentType = enuDocumentType.SalesInvoice
    FormCode = "01-002"
    FormVersion = "1"

    Me.FormDataSet.Tables.Add(Me._InventoryDataTable)
    Me.FormDataSet.Tables.Add(Me._InventoryDetailDataTable)
    Me.uccSaleDate.Format = Constants.FORMAT_DATE_FOR_USER
    Me.CurrentRecordDataRow = Me._CurrentInventoryDataRow
    If DatabaseCache.GetItemLeveling IsNot Nothing Then
      Me._ItemCodeColumnsCount = DatabaseCache.GetItemLeveling.Rows.Count
    Else
      Me._ItemCodeColumnsCount = 1
    End If

    With _InventoryDetailDataTable
      TotalAmountColumnExpression = .Inventory_Qty_Size01Column.ColumnName & "*" & .Inventory_Rate_Size01Column.ColumnName _
        & "+" & .Inventory_Qty_Size02Column.ColumnName & "*" & .Inventory_Rate_Size02Column.ColumnName _
        & "+" & .Inventory_Qty_Size03Column.ColumnName & "*" & .Inventory_Rate_Size03Column.ColumnName _
        & "+" & .Inventory_Qty_Size04Column.ColumnName & "*" & .Inventory_Rate_Size04Column.ColumnName _
        & "+" & .Inventory_Qty_Size05Column.ColumnName & "*" & .Inventory_Rate_Size05Column.ColumnName _
        & "+" & .Inventory_Qty_Size06Column.ColumnName & "*" & .Inventory_Rate_Size06Column.ColumnName _
        & "+" & .Inventory_Qty_Size07Column.ColumnName & "*" & .Inventory_Rate_Size07Column.ColumnName _
        & "+" & .Inventory_Qty_Size08Column.ColumnName & "*" & .Inventory_Rate_Size08Column.ColumnName _
        & "+" & .Inventory_Qty_Size09Column.ColumnName & "*" & .Inventory_Rate_Size09Column.ColumnName _
        & "+" & .Inventory_Qty_Size10Column.ColumnName & "*" & .Inventory_Rate_Size10Column.ColumnName _
        & "+" & .Inventory_Qty_Size11Column.ColumnName & "*" & .Inventory_Rate_Size11Column.ColumnName _
        & "+" & .Inventory_Qty_Size12Column.ColumnName & "*" & .Inventory_Rate_Size12Column.ColumnName _
        & "+" & .Inventory_Qty_Size13Column.ColumnName & "*" & .Inventory_Rate_Size13Column.ColumnName

      TotalQtyColumnExpression = .Inventory_Qty_Size01Column.ColumnName _
        & "+" & .Inventory_Qty_Size02Column.ColumnName & "+" & .Inventory_Qty_Size03Column.ColumnName _
        & "+" & .Inventory_Qty_Size04Column.ColumnName & "+" & .Inventory_Qty_Size05Column.ColumnName _
        & "+" & .Inventory_Qty_Size06Column.ColumnName & "+" & .Inventory_Qty_Size07Column.ColumnName _
        & "+" & .Inventory_Qty_Size08Column.ColumnName & "+" & .Inventory_Qty_Size09Column.ColumnName _
        & "+" & .Inventory_Qty_Size10Column.ColumnName & "+" & .Inventory_Qty_Size11Column.ColumnName _
        & "+" & .Inventory_Qty_Size12Column.ColumnName & "+" & .Inventory_Qty_Size13Column.ColumnName
    End With
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