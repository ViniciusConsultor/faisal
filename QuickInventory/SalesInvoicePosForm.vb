Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports CrystalDecisions.CrystalReports.Engine

Public Class SalesInvoicePosForm

#Region "Declaration"

  'Data Adapters
  Dim _InventoryTableAdapterObject As New InventoryTableAdapter
  Dim _InventoryDetailTableAdapterObject As New InventoryDetailTableAdapter
  Dim _InventorySalesInvoiceTA As New InventorySalesInvoiceTableAdapter
  Dim _ItemTableAdapter As New ItemTableAdapter
  'Dim _PartyTA As New PartyTableAdapter
  Dim _VoucherTA As New VoucherTableAdapter
  Dim _VoucherDetailTA As New VoucherDetailTableAdapter
  'Data Tables
  Private WithEvents _SalesInvoiceDataTable As New InventoryDataTable
  Private WithEvents _SalesInvoiceDetailDataTable As New InventoryDetailDataTable
  Private WithEvents _InventorySalesInvoieDataTable As New InventorySalesInvoiceDataTable
  Private WithEvents _ItemDataTable As New ItemDataTable
  Private WithEvents _TempSalesInvoiceDataTable As New InventoryDataTable
  'Dim _PartyDataTable As New PartyDataTable
  'Data Rows
  'Private _CurrentSalesInvoiceDataRow As QuickDAL.InventoryRow

  Dim _SalesInvoiceAmountTotal As Decimal = 0
  Dim _SalesInvoiceQtyTotal As Decimal = 0
  Private _CompanyCommunicationValue As String = String.Empty
  Private _DefaultWarehouseID As Int32 = 0

  Private Const DocumentTypeReturnValue As String = "Return"
  Private Const DocumentTypeSaleValue As String = "Sale"

  Private Enum enSalesInvoiceColumns
    DeleteRowButton
    Serial
    Item_Code
    Item_Size
    Item_Description
    Sale_Type
    Sale_Qty
    Sale_Rate
    Sale_Amount
    Item_ID
  End Enum

  Private Const EXC_PRE As String = "Exception in "
  Private Const EXC_GRDSALESINVOICE_LEAVECELL As String = EXC_PRE & "grdSalesInvoice_LeaveCell"

  Private _ReportForm As New QuickReports.CrystalReportViewerForm

#End Region

#Region "Properties"

#End Region

#Region "Methods"
  Private Function IsValid() As Boolean
    Try
      Me.grdSalesInvoice.EditMode = False

      If Me.grdSalesInvoice.ActiveSheet.NonEmptyRowCount <= 1 Then
        MessageBox.Show("There must be atleast one item in the sales invoice to save", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return False
      ElseIf Me.SalesManComboBox.PartyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Sales man is not selected or is invalid", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Return False
      End If

      For I As Int32 = 0 To Me.grdSalesInvoice_Sheet1.RowCount - 2
        If Me.grdSalesInvoice_Sheet1.GetText(I, General.ItemCodeColumnsCount + enSalesInvoiceColumns.Sale_Qty - 1) = "0" OrElse Me.grdSalesInvoice_Sheet1.GetText(I, General.ItemCodeColumnsCount + enSalesInvoiceColumns.Sale_Qty - 1) = "" Then
          Me.grdSalesInvoice_Sheet1.SetActiveCell(I, General.ItemCodeColumnsCount + enSalesInvoiceColumns.Sale_Qty - 1)
          QuickMessageBox.Show(Me.LoginInfoObject, "You cannot save item without quantity", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
          Return False
        End If
      Next

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to check if record is valid", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim SalesInvoiceID As Int32
      Dim _InventoryDataRow As InventoryRow
      Dim _InventoryDetailDataRow As InventoryDetailRow
      Dim _InventorySalesInvoiceRow As InventorySalesInvoiceRow
      Dim ItemID As Int32
      Dim ItemsUsedCollection As New Collection
      Dim SalesTypeLocal As Int32
      Dim _RecordSaved As Boolean = True
      Dim _LikeOperatorPattern As String     'It will hold the value which will be used with like operator to get maximum value.
      Dim _LastInventoryNo As Object
      Dim _NewInventoryNo As String = String.Empty

      Me.grdSalesInvoice.EditMode = False

      If Not IsValid() Then Return False

      If _SalesInvoiceDataTable Is Nothing OrElse _SalesInvoiceDataTable.Rows.Count < 1 Then
        _InventoryDataRow = _SalesInvoiceDataTable.NewInventoryRow
        SalesInvoiceID = _InventoryTableAdapterObject.GetNewInventoryIDByCoID(LoginInfoObject.CompanyID).Value
        _InventoryDataRow.Inventory_ID = SalesInvoiceID

        '_InventoryDataRow.Inventory_No = _InventoryTableAdapterObject.GetNewInventoryNoByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType).Value.ToString


        _LikeOperatorPattern = Common.GenerateNextDocumentNo(String.Empty, String.Empty, DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_PosSalesInvoice), True)
        _LastInventoryNo = _InventoryTableAdapterObject.GetMaxInventoryNoByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType, _LikeOperatorPattern)
        If _LastInventoryNo Is Nothing Then
          _NewInventoryNo = Common.GenerateNextDocumentNo(String.Empty, "", DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_PosSalesInvoice), False)
        Else
          _NewInventoryNo = Common.GenerateNextDocumentNo(String.Empty, _LastInventoryNo.ToString, DatabaseCache.GetSettingValue(SETTING_ID_DocumentNoFormat_PosSalesInvoice), False)
        End If

        _InventoryDataRow.Inventory_No = _NewInventoryNo
        _InventoryDataRow.DocumentType_ID = Convert.ToInt16(Me.DocumentType)
        _InventoryDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        Me.SaleNoTextBox.Text = _InventoryDataRow.Inventory_No.ToString
        '_CurrentSalesInvoiceDataRow = _InventoryDataRow
      Else
        _InventoryDataRow = _SalesInvoiceDataTable(Me.CurrentRecordIndex)
        SalesInvoiceID = _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID
        'In case of updated only common properties need to be set.
      End If
      'Set common properties for both update an insert.
      With _InventoryDataRow
        .Co_ID = LoginInfoObject.CompanyID
        .Party_ID = 0
        .Payment_Mode = 0
        .Remarks = "remarks"
        .Inventory_Date = Convert.ToDateTime(SaleDate.Value)

        '.DocumentType_ID = Convert.ToInt16(enuDocumentType.SalesInvoice)
        .Stamp_DateTime = Common.SystemDateTime
        .Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
      End With

      If _InventoryDataRow.RowState = DataRowState.Detached Then
        CurrentRecordDataRow = _InventoryDataRow
        _SalesInvoiceDataTable.Rows.Add(_InventoryDataRow)
      End If
      'Update DataBase
      _InventoryTableAdapterObject.Update(_SalesInvoiceDataTable)

      'Set all qty and rate to zero so that if any item is deleted from the grid then it becomes zero in the row
      For I As Int32 = 0 To _SalesInvoiceDetailDataTable.Rows.Count - 1
        With _SalesInvoiceDetailDataTable(I)
          .Inventory_Rate_Size01 = 0 : .Inventory_Rate_Size02 = 0 : .Inventory_Rate_Size03 = 0
          .Inventory_Rate_Size04 = 0 : .Inventory_Rate_Size05 = 0 : .Inventory_Rate_Size06 = 0
          .Inventory_Rate_Size07 = 0 : .Inventory_Rate_Size08 = 0 : .Inventory_Rate_Size09 = 0
          .Inventory_Rate_Size10 = 0 : .Inventory_Rate_Size11 = 0 : .Inventory_Rate_Size12 = 0 : .Inventory_Rate_Size13 = 0
          .Inventory_Qty_Size01 = 0 : .Inventory_Qty_Size02 = 0 : .Inventory_Qty_Size03 = 0
          .Inventory_Qty_Size04 = 0 : .Inventory_Qty_Size05 = 0 : .Inventory_Qty_Size06 = 0
          .Inventory_Qty_Size07 = 0 : .Inventory_Qty_Size08 = 0 : .Inventory_Qty_Size09 = 0
          .Inventory_Qty_Size10 = 0 : .Inventory_Qty_Size11 = 0 : .Inventory_Qty_Size12 = 0 : .Inventory_Qty_Size13 = 0
        End With
      Next

      With grdSalesInvoice_Sheet1
        For I As Int32 = 0 To .NonEmptyRowCount - 2   '-2 is required because last row is blank
          If .GetText(I, enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1) = "Return" Then
            SalesTypeLocal = enuDocumentType.SalesInvoiceReturn
          Else
            SalesTypeLocal = enuDocumentType.SalesInvoice
          End If
          _SalesInvoiceDetailDataTable.DefaultView.RowFilter = ""
          _SalesInvoiceDetailDataTable.DefaultView.RowFilter = "Inventory_ID = " & _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID _
            & " AND Item_ID = " & .GetText(I, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1) & " AND DocumentType_ID = " & SalesTypeLocal.ToString
          'FilterInventoryDetailTableForRow(I)

          If _SalesInvoiceDetailDataTable.DefaultView.Count <= 0 Then
            _InventoryDetailDataRow = _SalesInvoiceDetailDataTable.NewInventoryDetailRow
            _InventoryDetailDataRow.InventoryDetail_ID = Convert.ToInt32(_InventoryDetailTableAdapterObject.GetNewInventoryDetailIDByCoIDInventoryID(LoginInfoObject.CompanyID, _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID))
            _InventoryDetailDataRow.Inventory_ID = _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID
            _InventoryDetailDataRow.Co_ID = LoginInfoObject.CompanyID
            _InventoryDetailDataRow.DocumentType_ID = Convert.ToInt16(SalesTypeLocal)
            _InventoryDetailDataRow.Inventory_Rate_Size01 = 0 : _InventoryDetailDataRow.Inventory_Rate_Size02 = 0
            _InventoryDetailDataRow.Inventory_Rate_Size03 = 0 : _InventoryDetailDataRow.Inventory_Rate_Size04 = 0
            _InventoryDetailDataRow.Inventory_Rate_Size05 = 0 : _InventoryDetailDataRow.Inventory_Rate_Size06 = 0
            _InventoryDetailDataRow.Inventory_Rate_Size07 = 0 : _InventoryDetailDataRow.Inventory_Rate_Size08 = 0
            _InventoryDetailDataRow.Inventory_Rate_Size09 = 0 : _InventoryDetailDataRow.Inventory_Rate_Size10 = 0
            _InventoryDetailDataRow.Inventory_Rate_Size11 = 0 : _InventoryDetailDataRow.Inventory_Rate_Size12 = 0
            _InventoryDetailDataRow.Inventory_Rate_Size13 = 0
            'ElseIf _SalesInvoiceDetailDataTable.DefaultView.Count > 1 Then
            'MessageBox.Show("Duplicate rows", "Duplicate entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '_RecordSaved = False
            'Exit For
          Else
            'For J As Int32 = 0 To _SalesInvoiceDetailDataTable.DefaultView.Count - 1
            'MsgBox(_SalesInvoiceDetailDataTable.Rows(J).Item("DocumentType_ID").ToString)
            'Next
            'Assign first row by filtering. There should not be more than one rows theoratically here if data is stored correctly.
            _InventoryDetailDataRow = DirectCast(_SalesInvoiceDetailDataTable.DefaultView.Item(0).Row, InventoryDetailRow)
          End If
          ItemID = 0
          Int32.TryParse(.GetText(I, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1), ItemID)
          _InventoryDetailDataRow.Item_ID = ItemID
          If Not ItemsUsedCollection.Contains(ItemID.ToString & "-" & SalesTypeLocal.ToString) Then
            _InventoryDetailDataRow.Inventory_Qty_Size01 = 0 : _InventoryDetailDataRow.Inventory_Qty_Size02 = 0
            _InventoryDetailDataRow.Inventory_Qty_Size03 = 0 : _InventoryDetailDataRow.Inventory_Qty_Size04 = 0
            _InventoryDetailDataRow.Inventory_Qty_Size05 = 0 : _InventoryDetailDataRow.Inventory_Qty_Size06 = 0
            _InventoryDetailDataRow.Inventory_Qty_Size07 = 0 : _InventoryDetailDataRow.Inventory_Qty_Size08 = 0
            _InventoryDetailDataRow.Inventory_Qty_Size09 = 0 : _InventoryDetailDataRow.Inventory_Qty_Size10 = 0
            _InventoryDetailDataRow.Inventory_Qty_Size11 = 0 : _InventoryDetailDataRow.Inventory_Qty_Size12 = 0
            _InventoryDetailDataRow.Inventory_Qty_Size13 = 0
            ItemsUsedCollection.Add(ItemID.ToString & "-" & SalesTypeLocal.ToString, ItemID.ToString & "-" & SalesTypeLocal.ToString)
          End If

          'If row is deleted then don't add its quantity and it will not be displayed next time.
          If Not .IsRowDeleted(I) Then
            Select Case .GetText(I, enSalesInvoiceColumns.Item_Size + General.ItemCodeColumnsCount - 1)
              Case General.UserInputForItemSize(0)
                _InventoryDetailDataRow.Inventory_Qty_Size01 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size01 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(1)
                _InventoryDetailDataRow.Inventory_Qty_Size02 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size02 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(2)
                _InventoryDetailDataRow.Inventory_Qty_Size03 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size03 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(3)
                _InventoryDetailDataRow.Inventory_Qty_Size04 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size04 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(4)
                _InventoryDetailDataRow.Inventory_Qty_Size05 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size05 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(5)
                _InventoryDetailDataRow.Inventory_Qty_Size06 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size06 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(6)
                _InventoryDetailDataRow.Inventory_Qty_Size07 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size07 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(7)
                _InventoryDetailDataRow.Inventory_Qty_Size08 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size08 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(8)
                _InventoryDetailDataRow.Inventory_Qty_Size09 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size09 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(9)
                _InventoryDetailDataRow.Inventory_Qty_Size10 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size10 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(10)
                _InventoryDetailDataRow.Inventory_Qty_Size11 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size11 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(11)
                _InventoryDetailDataRow.Inventory_Qty_Size12 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size12 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case General.UserInputForItemSize(12)
                _InventoryDetailDataRow.Inventory_Qty_Size13 += Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
                _InventoryDetailDataRow.Inventory_Rate_Size13 = Convert.ToDecimal(.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
              Case Else
                'Invalid Size, cannot assign value to any column.
            End Select
          End If

          _InventoryDetailDataRow.Stamp_DateTime = Common.SystemDateTime
          _InventoryDetailDataRow.Stamp_UserID = LoginInfoObject.UserID
          _InventoryDetailDataRow.Warehouse_ID = _DefaultWarehouseID
          If .IsRowDeleted(I) Then
            '_InventoryDetailDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
          ElseIf _InventoryDetailDataRow.RowState = DataRowState.Detached Or _InventoryDetailDataRow.RowState = DataRowState.Added Then
            _InventoryDetailDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
          Else
            _InventoryDetailDataRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If
          If _InventoryDetailDataRow.RowState = DataRowState.Detached Then
            _SalesInvoiceDetailDataTable.Rows.Add(_InventoryDetailDataRow)
          End If

          'Update database
          _InventoryDetailTableAdapterObject.Update(_InventoryDetailDataRow)
        Next
        _InventoryDetailTableAdapterObject.Update(_SalesInvoiceDetailDataTable)
      End With

      'Save sales invocie additional information
      _InventorySalesInvoieDataTable = _InventorySalesInvoiceTA.GetByCoIDInventoryID(LoginInfoObject.CompanyID, SalesInvoiceID)
      If _InventorySalesInvoieDataTable.Rows.Count > 0 Then
        _InventorySalesInvoiceRow = DirectCast(_InventorySalesInvoieDataTable(0), InventorySalesInvoiceRow)
        _InventorySalesInvoiceRow.SalesMan_ID = SalesManComboBox.PartyID
        _InventorySalesInvoiceRow.Discount = Cast.ToDecimal(DiscountTextBox.Text)
        _InventorySalesInvoiceRow.SalesTax = Cast.ToDecimal(SalesTaxTextBox.Text)
        _InventorySalesInvoiceRow.TotalCashReceived = Cast.ToDecimal(CashRecievedTextBox.Text)
        _InventorySalesInvoiceRow.Stamp_DateTime = Common.SystemDateTime
        _InventorySalesInvoiceRow.Stamp_UserID = LoginInfoObject.UserID
      Else
        _InventorySalesInvoiceTA.Insert(LoginInfoObject.CompanyID, SalesInvoiceID, SalesManComboBox.PartyID, Cast.ToDecimal(DiscountTextBox.Text), Cast.ToDecimal(SalesTaxTextBox.Text), Cast.ToDecimal(CashRecievedTextBox.Text), Common.SystemDateTime, LoginInfoObject.UserID, Nothing)
      End If
      _InventorySalesInvoiceTA.Update(_InventorySalesInvoieDataTable)

      'Save the voucher
      Dim _VoucherTable As New VoucherDataTable
      Dim _VoucherDetailTable As New VoucherDetailDataTable
      Dim _VoucherRow As VoucherRow
      Dim _VoucherDetailRow As VoucherDetailRow
      Dim _VoucherID As Int32
      Dim _VoucherTypeID As Int32
      Dim _VoucherDetailID As Int16
      Dim _SalesCoaId As Int32
      Dim _CashCoaId As Int32

      'Get data from setting.
      _VoucherTypeID = Convert.ToInt32(DatabaseCache.GetSettingValue(SETTING_ID_SalesInvoiceVoucherType))
      _SalesCoaId = Convert.ToInt32(DatabaseCache.GetSettingValue(SETTING_ID_SalesCoaId))
      If _SalesCoaId = 0 Then _SalesCoaId = 2
      _CashCoaId = Convert.ToInt32(DatabaseCache.GetSettingValue(SETTING_ID_CashCoaId))
      If _CashCoaId = 0 Then _CashCoaId = 1

      'Get vouchers posted for this invoice.
      _VoucherTable = _VoucherTA.GetByCoIDSourceIDSourceDocumentTypeID(LoginInfoObject.CompanyID, SalesInvoiceID, enuDocumentType.SalesInvoice)
      'If voucher is already posted for this invoice.
      If _VoucherTable.Rows.Count > 0 Then
        For Each _VoucherRow In _VoucherTable.Rows
          If _VoucherRow.VoucherType_ID = _VoucherTypeID Then
            _VoucherRow.Voucher_Date = _InventoryDataRow.Inventory_Date
            _VoucherRow.Stamp_DateTime = Common.SystemDateTime
            _VoucherRow.Stamp_UserID = LoginInfoObject.UserID
            _VoucherRow.Remarks = "S. No. " & _InventoryDataRow.Inventory_No
            'Get voucher detail.
            _VoucherDetailTable = _VoucherDetailTA.GetByCoIDVoucherID(_VoucherRow.Co_ID, _VoucherRow.Voucher_ID)
            For Each _VoucherDetailRow In _VoucherDetailTable.Rows
              If _VoucherDetailRow.DebitAmount = 0 Then
                _VoucherDetailRow.CreditAmount = _SalesInvoiceAmountTotal
              Else
                _VoucherDetailRow.DebitAmount = _SalesInvoiceAmountTotal
              End If
              _VoucherDetailRow.Narration = "S. No. " & _InventoryDataRow.Inventory_No
              _VoucherDetailRow.Stamp_DateTime = Common.SystemDateTime
              _VoucherDetailRow.Stamp_User_Id = LoginInfoObject.UserID
            Next
          End If
        Next
        _VoucherTA.Update(_VoucherTable)
        _VoucherDetailTA.Update(_VoucherDetailTable)
      Else
        _VoucherID = Convert.ToInt32(_VoucherTA.GetNewVoucherIDByCoID(LoginInfoObject.CompanyID))
        _VoucherTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherTypeID, _VoucherID.ToString, _InventoryDataRow.Inventory_Date, DocumentStatuses.General_Posted, "S. No. " & _InventoryDataRow.Inventory_No, LoginInfoObject.UserID, Common.SystemDateTime, SalesInvoiceID, Convert.ToInt16(enuDocumentType.SalesInvoice), Nothing, Constants.DocumentStatuses.General_Posted)
        'Cash entry
        _VoucherDetailID = Convert.ToInt16(_VoucherDetailTA.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherID))
        _VoucherDetailTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherDetailID, _CashCoaId, "S. No. " & _InventoryDataRow.Inventory_No, _SalesInvoiceAmountTotal, 0, LoginInfoObject.UserID, Common.SystemDateTime, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, RecordStatuses.Inserted)
        'Sales entry
        _VoucherDetailID = Convert.ToInt16(_VoucherDetailTA.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherID))
        _VoucherDetailTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherDetailID, _SalesCoaId, "S. No. " & _InventoryDataRow.Inventory_No, 0, _SalesInvoiceAmountTotal, LoginInfoObject.UserID, Common.SystemDateTime, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, RecordStatuses.Inserted)
      End If

      If AutoPrintWithSaveCheckBox.Checked Then PrintReport(False)

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      Dim SalesInvoiceDetailDataRow As InventoryDetailRow
      Dim ItemDataRow As ItemRow

      If Me._SalesInvoiceDataTable.Rows.Count > 0 Then
        'Me._CurrentSalesInvoiceDataRow = CType(Me._SalesInvoiceDataTable.Rows(Me.CurrentRecordIndex), InventoryRow)
        Me.ClearControls(Me)
        Me.CurrentRecordDataRow = _SalesInvoiceDataTable(Me.CurrentRecordIndex)

        With _SalesInvoiceDataTable(Me.CurrentRecordIndex)
          _InventorySalesInvoieDataTable = _InventorySalesInvoiceTA.GetByCoIDInventoryID(LoginInfoObject.CompanyID, _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID)

          Me.SaleNoTextBox.Text = .Inventory_No.ToString
          Me.SaleDate.Value = .Inventory_Date
          Me.txtRemarks.Text = .Remarks

          If _InventorySalesInvoieDataTable.Rows.Count > 0 Then
            SalesManComboBox.PartyID = _InventorySalesInvoieDataTable(0).SalesMan_ID
            DiscountTextBox.Text = _InventorySalesInvoieDataTable(0).Discount.ToString
            SalesTaxTextBox.Text = _InventorySalesInvoieDataTable(0).SalesTax.ToString
            If Not _InventorySalesInvoieDataTable(0).IsTotalCashReceivedNull Then
              CashRecievedTextBox.Text = _InventorySalesInvoieDataTable(0).TotalCashReceived.ToString
            Else
              CashRecievedTextBox.Text = "0"
            End If
          End If

          Me._SalesInvoiceDetailDataTable = Me._InventoryDetailTableAdapterObject.GetByCoIDInventoryID(.Co_ID, .Inventory_ID)
        End With

        For I As Int32 = 0 To Me._SalesInvoiceDetailDataTable.Rows.Count - 1
          SalesInvoiceDetailDataRow = CType(Me._SalesInvoiceDetailDataTable.Rows(I), InventoryDetailRow)

          _ItemDataTable = _ItemTableAdapter.GetByCoIDAndItemID(SalesInvoiceDetailDataRow.Co_ID, SalesInvoiceDetailDataRow.Item_ID)
          ItemDataRow = CType(_ItemDataTable.Rows(0), ItemRow)

          With SalesInvoiceDetailDataRow
            If .Inventory_Qty_Size01 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(0), .DocumentType_ID.ToString, .Inventory_Qty_Size01.ToString, .Inventory_Rate_Size01.ToString)
            End If
            If .Inventory_Qty_Size02 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(1), .DocumentType_ID.ToString, .Inventory_Qty_Size02.ToString, .Inventory_Rate_Size02.ToString)
            End If
            If .Inventory_Qty_Size03 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(2), .DocumentType_ID.ToString, .Inventory_Qty_Size03.ToString, .Inventory_Rate_Size03.ToString)
            End If
            If .Inventory_Qty_Size04 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(3), .DocumentType_ID.ToString, .Inventory_Qty_Size04.ToString, .Inventory_Rate_Size04.ToString)
            End If
            If .Inventory_Qty_Size05 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(4), .DocumentType_ID.ToString, .Inventory_Qty_Size05.ToString, .Inventory_Rate_Size05.ToString)
            End If
            If .Inventory_Qty_Size06 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(5), .DocumentType_ID.ToString, .Inventory_Qty_Size06.ToString, .Inventory_Rate_Size06.ToString)
            End If
            If .Inventory_Qty_Size07 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(6), .DocumentType_ID.ToString, .Inventory_Qty_Size07.ToString, .Inventory_Rate_Size07.ToString)
            End If
            If .Inventory_Qty_Size08 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(7), .DocumentType_ID.ToString, .Inventory_Qty_Size08.ToString, .Inventory_Rate_Size08.ToString)
            End If
            If .Inventory_Qty_Size09 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(8), .DocumentType_ID.ToString, .Inventory_Qty_Size09.ToString, .Inventory_Rate_Size09.ToString)
            End If
            If .Inventory_Qty_Size10 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(9), .DocumentType_ID.ToString, .Inventory_Qty_Size10.ToString, .Inventory_Rate_Size10.ToString)
            End If
            If .Inventory_Qty_Size11 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(10), .DocumentType_ID.ToString, .Inventory_Qty_Size11.ToString, .Inventory_Rate_Size11.ToString)
            End If
            If .Inventory_Qty_Size12 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(11), .DocumentType_ID.ToString, .Inventory_Qty_Size12.ToString, .Inventory_Rate_Size12.ToString)
            End If
            If .Inventory_Qty_Size13 > 0 Then
              AddItem(Me.grdSalesInvoice.ActiveSheet.Rows.Count - 1, .Item_ID.ToString, ItemDataRow.Item_Code, ItemDataRow.Item_Desc, General.UserInputForItemSize(12), .DocumentType_ID.ToString, .Inventory_Qty_Size13.ToString, .Inventory_Rate_Size13.ToString)
            End If
          End With
        Next
      Else
        'MessageBox.Show("No row to show", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      End If
      'By Default item description is shown on the form, below code will show the descriptions of specified level in settings.
      Me.grdSalesInvoice.RefreshAllItemDescriptions()
      ShowTotal()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Private Sub AddItem(ByVal Index As Int32, ByVal ItemID As String, ByVal ItemCode As String, ByVal ItemDescription As String, ByVal ItemSize As String, ByVal SalesType As String, ByVal ItemQty As String, ByVal ItemRate As String)
    Try
      Me.grdSalesInvoice.ActiveSheet.Rows.Add(Index, 1)

      Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1, ItemID)
      'Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Item_Code, ItemCode)
      Me.grdSalesInvoice.ShowItemCode(Index, ItemCode)
      Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Item_Description + General.ItemCodeColumnsCount - 1, ItemDescription)
      Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Item_Size + General.ItemCodeColumnsCount - 1, ItemSize)
      If SalesType = CType(enuDocumentType.SalesInvoiceReturn, Int32).ToString Then
        Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1, "Return")
      Else
        Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1, "Sales")
      End If
      Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1, ItemQty)
      Me.grdSalesInvoice.ActiveSheet.SetText(Index, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1, ItemRate)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to add item in grid", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Protected Overrides Sub ClearControls(ByRef pControlObject As System.Windows.Forms.Control)
    Try
      MyBase.ClearControls(pControlObject)

      Me.CurrentRecordDataRow = Nothing
      Me.grdSalesInvoice.ActiveSheet.Rows.Remove(0, Me.grdSalesInvoice.ActiveSheet.Rows.Count)
      Me.grdSalesInvoice.ActiveSheet.Rows.Add(0, 1)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to clear controls", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub ShowTotal()
    Try
      Dim _SaleAmount As Decimal
      Dim _SaleQty As Decimal

      _SalesInvoiceAmountTotal = 0
      _SalesInvoiceQtyTotal = 0
      For I As Int32 = 0 To Me.grdSalesInvoice_Sheet1.Rows.Count - 2
        'If row is deleted then it should not be included in the totals
        If Not Me.grdSalesInvoice_Sheet1.IsRowDeleted(I) Then
          If Me.grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1) = "Return" Then
            'Subtract amount from total amount.
            If Decimal.TryParse(Me.grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Amount + General.ItemCodeColumnsCount - 1), _SaleAmount) Then
              _SalesInvoiceAmountTotal -= _SaleAmount
            Else
              'If parsing is not successful then its invalid value and no need to exclude from total.
            End If
            'Subtract quantity from total quantity.
            If Decimal.TryParse(Me.grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1), _SaleQty) Then
              _SalesInvoiceQtyTotal -= _SaleQty
            Else
              'If parsing is not successful then its invalid value and no need to exclude from total.
            End If
          Else
            'Add amount to total amount.
            If Decimal.TryParse(Me.grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Amount + General.ItemCodeColumnsCount - 1), _SaleAmount) Then
              _SalesInvoiceAmountTotal += _SaleAmount
            Else
              'If parsing is not successful then its invalid value and no need to include in total.
            End If
            'Add quantity to total quantity.
            If Decimal.TryParse(Me.grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1), _SaleQty) Then
              _SalesInvoiceQtyTotal += _SaleQty
            Else
              'If parsing is not successful then its invalid value and no need to exclude from total.
            End If
          End If
        End If
      Next

      _SalesInvoiceAmountTotal += _SalesInvoiceAmountTotal * Cast.ToDecimal(SalesTaxTextBox.Text) / 100
      _SalesInvoiceAmountTotal -= _SalesInvoiceAmountTotal * Cast.ToDecimal(DiscountTextBox.Text) / 100

      Me.SalesInvoiceTotalAmountLabel.Text = Format(_SalesInvoiceAmountTotal, "##,##,##,##0.0")
      Me.SalesInvoiceTotalQtyLabel.Text = _SalesInvoiceQtyTotal.ToString

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show total", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Private Sub InitializeForm()
    Try
      'Dim _EntityTypes As String

      '_EntityTypes = DatabaseCache.GetSettingValue(SETTING_ID_SalesInvoiceEntityTypes)
      SalesManComboBox.EntityType = EntityTypes.SalesMan
      SalesManComboBox.LoadParties(LoginInfoObject.CompanyID)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in initializing form", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Private Sub InsertDataForPrinting()
    Try
      Dim _TempSalesInvoiceReportTA As New Temp_SalesInvoiceReportTableAdapter
      Dim _CompanyTA As New CompanyTableAdapter
      Dim _CommunicationTA As New CommunicationTableAdapter
      Dim _AddressTA As New AddressTableAdapter
      Dim _ItemTA As New ItemTableAdapter

      Dim _CommunicationTable As CommunicationDataTable
      Dim _AddressTable As AddressDataTable
      Dim _TempSalesInvoiceReportDataTable As New Temp_SalesInvoiceReportDataTable
      Dim _TempSalesInvoiceReportRow As Temp_SalesInvoiceReportRow

      _CommunicationTable = _CommunicationTA.GetBySource(enuDocumentType.Company, Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.CompanyID)
      _AddressTable = _AddressTA.GetBySource(enuDocumentType.Company, Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.CompanyID)

      _TempSalesInvoiceReportTA.TruncateData()
      For I As Int32 = 0 To Me.grdSalesInvoice_Sheet1.RowCount - 2
        _TempSalesInvoiceReportRow = _TempSalesInvoiceReportDataTable.NewTemp_SalesInvoiceReportRow

        With _TempSalesInvoiceReportRow
          If _AddressTable.Rows.Count > 0 Then
            .Address_Desc = _AddressTable(0).Address_Desc
          Else
            .Address_Desc = ""
          End If
          If _CommunicationTable.Rows.Count > 0 Then
            _CompanyCommunicationValue = _CommunicationTable(0).Communication_Value
          Else
            _CompanyCommunicationValue = ""
          End If
          .Co_ID = LoginInfoObject.CompanyID
          .Co_Desc = LoginInfoObject.CompanyDesc
          .Discount = Cast.ToDecimal(DiscountTextBox.Text)
          .SalesTax = Cast.ToDecimal(SalesTaxTextBox.Text)
          If grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1) = "Return" Then
            .DocumentType_ID = enuDocumentType.SalesInvoiceReturn
          Else
            .DocumentType_ID = enuDocumentType.SalesInvoice
          End If
          .Inventory_Date = Cast.ToDateTime(SaleDate.Value)
          .Inventory_No = SaleNoTextBox.Text
          .Item_ID = Cast.ToInt32(grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1))
          .Item_Code = Me.grdSalesInvoice.GetItemCode(grdSalesInvoice_Sheet1, I)
          .Item_Desc = grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Item_Description + General.ItemCodeColumnsCount - 1)
          .Party_Desc = ""
          .Party_ID = 0
          .Qty = Cast.ToDecimal(grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1))
          .Rate = Cast.ToDecimal(grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1))
          .Remarks = txtRemarks.Text
          Select Case grdSalesInvoice_Sheet1.GetText(I, enSalesInvoiceColumns.Item_Size + General.ItemCodeColumnsCount - 1)
            Case General.UserInputForItemSize(0)
              .Size_Desc = ITEM_SIZE_01_ALIAS
            Case General.UserInputForItemSize(1)
              .Size_Desc = ITEM_SIZE_02_ALIAS
            Case General.UserInputForItemSize(2)
              .Size_Desc = ITEM_SIZE_03_ALIAS
            Case General.UserInputForItemSize(3)
              .Size_Desc = ITEM_SIZE_04_ALIAS
            Case General.UserInputForItemSize(4)
              .Size_Desc = ITEM_SIZE_05_ALIAS
            Case General.UserInputForItemSize(5)
              .Size_Desc = ITEM_SIZE_06_ALIAS
            Case General.UserInputForItemSize(6)
              .Size_Desc = ITEM_SIZE_07_ALIAS
            Case General.UserInputForItemSize(7)
              .Size_Desc = ITEM_SIZE_08_ALIAS
            Case General.UserInputForItemSize(8)
              .Size_Desc = ITEM_SIZE_09_ALIAS
            Case General.UserInputForItemSize(9)
              .Size_Desc = ITEM_SIZE_10_ALIAS
            Case General.UserInputForItemSize(10)
              .Size_Desc = ITEM_SIZE_11_ALIAS
            Case General.UserInputForItemSize(11)
              .Size_Desc = ITEM_SIZE_12_ALIAS
            Case General.UserInputForItemSize(12)
              .Size_Desc = ITEM_SIZE_13_ALIAS
            Case Else
              .Size_Desc = "unknown"
          End Select
        End With

        _TempSalesInvoiceReportDataTable.Rows.Add(_TempSalesInvoiceReportRow)
      Next

      _TempSalesInvoiceReportTA.Update(_TempSalesInvoiceReportDataTable)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in inserting data for report", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub PrintReport(ByVal _PreviewOnly As Boolean)
    Try
      Dim _ParameterValues As New System.Collections.Specialized.NameValueCollection
      Dim _FormulaValues As New System.Collections.Specialized.NameValueCollection
      Dim _SelectionFormula As String = String.Empty

      InsertDataForPrinting()

      _ParameterValues.Add("RePrint", "")
      _FormulaValues.Add("SalesInvoiceNo", Me.SaleNoTextBox.Text)
      _FormulaValues.Add("CashReceived", Me.CashRecievedTextBox.Text)
      _FormulaValues.Add("BalanceAmount", Me.CashBalanceAmountLabel.Text)
      _FormulaValues.Add("SalesManName", Me.SalesManComboBox.Text)
      _FormulaValues.Add("CompanyContactInfo", _CompanyCommunicationValue)
      _FormulaValues.Add("ReportFooterText", QuickDALLibrary.DatabaseCache.GetSettingValue(SETTING_ID_ReportFooterText & SETTING_ID_SEPERATOR & "PosSalesInvoiceReport"))

      _SelectionFormula = ""
      _ReportForm.ParameterValues = _ParameterValues
      _ReportForm.FormulaValues = _FormulaValues
      _ReportForm.RecordSelectionFormula = _SelectionFormula
      _ReportForm.Report = New QuickReports.SalesInvoicePosReport
      _ReportForm.WindowState = FormWindowState.Maximized

      If Not _PreviewOnly Then
        _ReportForm.Report.PrintToPrinter(1, True, 0, 0)
      Else
        _ReportForm.Show()
      End If

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in PrintReport method.", ex)
    Finally
      _ReportForm.Report.Close()
      _ReportForm.Report.Dispose()
    End Try
  End Sub
#End Region

#Region "Toolbar methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Feb-10
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Protected Overrides Sub SearchButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    If Me.DocumentType = enuDocumentType.SalesInvoice Then

      Try
        Dim _SearchForm As New QuickBaseForms.SearchForm

        _SearchForm.LoginInfoObject = DirectCast(Me.LoginInfoObject.Clone, LoginInfo)
        _SearchForm.SearchOption = QuickBaseForms.SearchForm.SearchOptionIDs.PosSalesInvoice
        _SearchForm.DocumentType = enuDocumentType.SalesInvoice

        _SearchForm.ShowDialog()
        If _SearchForm.SearchResultUnTypedDataTable IsNot Nothing AndAlso _SearchForm.SearchResultUnTypedDataTable.Rows.Count > 0 Then
          With _SearchForm.SearchResultUnTypedDataTable.Rows(0)
            Me._SalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetByCoIDInventoryID(DirectCast(.Item(_SalesInvoiceDataTable.Co_IDColumn.ColumnName), Int16), DirectCast(.Item(_SalesInvoiceDataTable.Inventory_IDColumn.ColumnName), Int32))
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

  Protected Overrides Sub PrintPreviewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      PrintReport(True)

      MyBase.PrintPreviewButtonClick(sender, e)

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in PrintPreviewButtonClick event method.", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Me._SalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetFirstByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)
      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If _SalesInvoiceDataTable IsNot Nothing AndAlso _SalesInvoiceDataTable.Rows.Count > 0 Then
        _TempSalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetNextByCoIDInventoryIDDocumentTypeID(_SalesInvoiceDataTable(Me.CurrentRecordIndex).Co_ID, _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID, Me.DocumentType)
      Else
        _TempSalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetNextByCoIDInventoryIDDocumentTypeID(LoginInfoObject.CompanyID, 0, Me.DocumentType)
      End If

      If _TempSalesInvoiceDataTable.Count > 0 Then
        Me._SalesInvoiceDataTable = _TempSalesInvoiceDataTable
        MyBase.MoveNextButtonClick(sender, e)
      Else
        'If no record found then leave existing.
      End If

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

      If _SalesInvoiceDataTable IsNot Nothing AndAlso _SalesInvoiceDataTable.Rows.Count > 0 Then
        _TempSalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetPreviousByCoIDInventoryIDDocumentTypeID(_SalesInvoiceDataTable(Me.CurrentRecordIndex).Co_ID, _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID, Me.DocumentType)
      Else
        _TempSalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetPreviousByCoIDInventoryIDDocumentTypeID(LoginInfoObject.CompanyID, Int32.MaxValue, Me.DocumentType)
      End If

      If _TempSalesInvoiceDataTable.Count > 0 Then
        Me._SalesInvoiceDataTable = _TempSalesInvoiceDataTable
        MyBase.MovePreviousButtonClick(sender, e)
      Else
        'If no record found then leave existing.
      End If

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

      If _SalesInvoiceDataTable IsNot Nothing AndAlso _SalesInvoiceDataTable.Rows.Count > 0 Then
        Me._SalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetLastByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)
      Else
        Me._SalesInvoiceDataTable = Me._InventoryTableAdapterObject.GetLastByCoIDDocumentTypeID(LoginInfoObject.CompanyID, Me.DocumentType)
      End If
      MyBase.MoveLastButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move last", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Record is saved successfully.", QuickMessageBox.MessageBoxTypes.ShortMessage)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      'Me._CurrentSalesInvoiceDataRow = Nothing
      Me._SalesInvoiceDataTable.Clear()
      Me._SalesInvoiceDetailDataTable.Clear()
      Me.CurrentRecordDataRow = Nothing
      MyBase.CancelButtonClick(sender, e)
      SaleDate.Value = Common.SystemDateTime
      SaleDate.Enabled = False
      Me.CashBalanceAmountLabel.Text = String.Empty
      Me.SalesManComboBox.Focus()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to cancel button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    QuickMessageBox.Show(Me.LoginInfoObject, "Not Allowed", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
    Try
      'Currently it is disabled
      If False Then
        Cursor = Cursors.WaitCursor

        If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
          _SalesInvoiceDataTable(Me.CurrentRecordIndex).RecordStatus_ID = RecordStatuses.Deleted

          SaveRecord()
          _VoucherTA.UpdateStatusByCoIdSource(RecordStatuses.Deleted, LoginInfoObject.CompanyID, _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID, enuDocumentType.SalesInvoice)

          MyBase.DeleteButtonClick(sender, e)
          QuickMessageBox.Show(Me.LoginInfoObject, "Record is deleted successfully.", QuickMessageBox.MessageBoxTypes.ShortMessage)
        Else
        End If
      End If
    Catch ex As Exception
      Dim qex As New QuickExceptionAdvanced("Exception to delete button click", ex)
      qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Events"

  'Author: Faisal Salem
  'Date Created(DD-MMM-YY): 28-Jun-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' When row is deleted we need to re-calculate the totals.
  ''' </summary>
  Private Sub grdSalesInvoice_ButtonClicked(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles grdSalesInvoice.ButtonClicked
    Try
      ShowTotal()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in grdSalesInvoice_ButtonClicked of grdSalesInvoice.", ex)
      _qex.Show(LoginInfoObject)
    End Try

  End Sub

  Private Sub grdSalesInvoice_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSalesInvoice.EditModeOff
    Try
      Dim RowNumber As Int32
      Dim ColumnNumber As Int32

      'When clearing the grid access to activecell is throwing exception because row index is out of range.
      If grdSalesInvoice.ActiveSheet.ActiveRowIndex >= grdSalesInvoice.ActiveSheet.RowCount Then Return

      If grdSalesInvoice.Sheets(0).ActiveCell IsNot Nothing Then
        RowNumber = grdSalesInvoice.Sheets(0).ActiveCell.Row.Index
        ColumnNumber = grdSalesInvoice.Sheets(0).ActiveCell.Column.Index

        With grdSalesInvoice.Sheets(0)
          'If column is sale type
          If ColumnNumber = enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1 Then
            If .GetText(RowNumber, ColumnNumber) <> String.Empty AndAlso .GetText(RowNumber, ColumnNumber).ToLower = DocumentTypeReturnValue.Substring(0, .GetText(RowNumber, ColumnNumber).Length).ToLower Then
              .SetText(RowNumber, ColumnNumber, DocumentTypeReturnValue)
            Else
              .SetText(RowNumber, ColumnNumber, DocumentTypeSaleValue)
            End If
          End If
        End With

        If grdSalesInvoice.ActiveSheet.RowCount > 2 AndAlso RowNumber = grdSalesInvoice.ActiveSheet.RowCount - 2 AndAlso grdSalesInvoice.ActiveSheet.GetText(grdSalesInvoice.ActiveSheet.RowCount - 2, enSalesInvoiceColumns.Item_Code) = "" Then
          Me.grdSalesInvoice.ActiveSheet.RowCount = Me.grdSalesInvoice.ActiveSheet.RowCount - 1
        End If
        ShowTotal()
      End If

      'If Me.grdSalesInvoice.ActiveSheet.GetText(RowNumber, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1) <> String.Empty Then
      '  Me.StockBar1.ShowSummary(Me.LoginInfoObject.CompanyID, Convert.ToInt32(Me.grdSalesInvoice.ActiveSheet.GetText(RowNumber, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1)), 0, 0)
      'End If

    Catch ex As Exception
      Dim objEx As New QuickExceptionAdvanced("Exception in grdSalesInvoice_EditModeOff event method of SalesInvoicePosForm.", ex)
      objEx.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub frmSalesInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim obj As FarPoint.Win.Spread.CellType.ComboBoxCellType

      InitializeForm()

      With Me.grdSalesInvoice

        .LoginInfoObject = Me.LoginInfoObject
        .ItemSheetView = Me.grdSalesInvoice_Sheet1
        .ItemCodeFirstColumnIndex = enSalesInvoiceColumns.Item_Code
        .ItemDescColumnIndex = enSalesInvoiceColumns.Item_Description + General.ItemCodeColumnsCount - 1
        .ItemIDColumnIndex = enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1
        .ItemSizeColumnIndex = enSalesInvoiceColumns.Item_Size + General.ItemCodeColumnsCount - 1
        .ItemRateFirstColumnIndex = enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1
        .SetItemCodeColumns()
        .ItemSummaryBarObject = StockBar1
      End With

      Dim _SettingValue As String = String.Empty
      _SettingValue = DatabaseCache.GetSettingValue(SETTING_ID_DefaultWarehouseID & Me.DocumentType.ToString)
      If Not Int32.TryParse(_SettingValue, _DefaultWarehouseID) Then _DefaultWarehouseID = 0

      obj = CType(Me.grdSalesInvoice.ActiveSheet.Columns(enSalesInvoiceColumns.Sale_Type + General.ItemCodeColumnsCount - 1).CellType, FarPoint.Win.Spread.CellType.ComboBoxCellType)
      obj.Editable = True
      SaleDate.Value = Common.SystemDateTime
      SaleDate.Enabled = False

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to load form", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub CashRecievedTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
Handles CashRecievedTextBox.TextChanged, SalesInvoiceTotalAmountLabel.TextChanged
    Try
      CashBalanceAmountLabel.Text = Format(Cast.ToDecimal(CashRecievedTextBox.Text) - _SalesInvoiceAmountTotal, "##,##,##,##0.0")

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in TextChanged event method of Cash Received or Total Amount.", ex)
    End Try
  End Sub

  Private Sub grdSalesInvoice_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles grdSalesInvoice.LeaveCell
    Try
      'When clearing the form it is return the row greater than total rows for some reason.
      If e.Row < grdSalesInvoice.ActiveSheet.RowCount Then
        If e.NewRow = e.Row AndAlso e.NewColumn = enSalesInvoiceColumns.DeleteRowButton _
        AndAlso (e.Column = (enSalesInvoiceColumns.Sale_Qty + General.ItemCodeColumnsCount - 1) OrElse e.Column = (enSalesInvoiceColumns.Sale_Rate + General.ItemCodeColumnsCount - 1)) Then
          e.NewRow = e.NewRow + 1
          e.NewColumn = enSalesInvoiceColumns.Item_Code
        End If
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in SalesInvoicePosForm grdSalesInvoice_LeaveCell event method.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

  Public Sub New()
    'Dim MaskCellType As New FarPoint.Win.Spread.CellType.MaskCellType

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DocumentType = enuDocumentType.SalesInvoice
    FormCode = "01-003"
    FormVersion = "1"
    Me.FormDataSet.Tables.Add(Me._SalesInvoiceDataTable)
    Me.FormDataSet.Tables.Add(Me._SalesInvoiceDetailDataTable)
    Me.SaleDate.Format = Constants.FORMAT_DATE_FOR_USER
    Me.grdSalesInvoice_Sheet1.ShowTotalsRow = False
    'MaskCellType.Mask = DatabaseCache.GetSettingValue(Constants.SETTING_ID_Mask_ItemCode)
    'Me.grdSalesInvoice_Sheet1.Columns(enSalesInvoiceColumns.Item_Code).CellType = MaskCellType

  End Sub


  'Private Sub FilterInventoryDetailTableForRow(ByVal _RowIndex As Int32)
  '  Try
  '    _SalesInvoiceDetailDataTable.DefaultView.RowFilter = ""
  '    _SalesInvoiceDetailDataTable.DefaultView.RowFilter = "Inventory_ID = " & _SalesInvoiceDataTable(Me.CurrentRecordIndex).Inventory_ID _
  '      & " AND Item_ID = " & Me.grdSalesInvoice.ActiveSheet.GetText(_RowIndex, enSalesInvoiceColumns.Item_ID + General.ItemCodeColumnsCount - 1) & " AND DocumentType_ID = " & SalesTypeLocal.ToString

  '  Catch ex As Exception
  '    Dim _QuickException As New QuickExceptionAdvanced("Exception in FilterInventoryDetailTableForItem() method.", ex)
  '    _QuickException.Show(Me.LoginInfoObject)
  '  End Try
  'End Sub

End Class