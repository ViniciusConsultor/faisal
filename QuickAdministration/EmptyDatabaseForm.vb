Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDALLibrary

Public Class EmptyDatabaseForm

  Private Enum DeleteOptionRowNo
    AllRecords = 0
    Transfers
  End Enum

  Private Sub EmptyDatabaseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmptyDatabaseButton.Click
    Dim _TransferRecords, _TransferRecordsOnline As Int32
    Dim _VoucherDetailsRecords, _VoucherDetailsRecordsOnline As Int32
    Dim _VoucherRecords, _VoucherRecordsOnline As Int32
    Dim _InventorySalesInvoiceRecords, _InventorySalesInvoiceRecordsOnline As Int32
    Dim _InventoryDetailRecords, _InventoryDetailRecordsOnline As Int32
    Dim _InventoryRecords, _InventoryRecordsOnline As Int32
    Dim _ItemSummaryRecords, _ItemSummaryRecordsOnline As Int32
    Dim _ItemRecords, _ItemRecordsOnline As Int32
    Dim _PartyRecords, _PartyRecordsOnline As Int32

    Try
      'Dim _Result As Boolean
      Dim _object As Object
      Dim WebServerConnectionString As String
      Dim _TransferTableAdapter As New TransferTableAdapter
      Dim _VoucherDetailTableAdapter As New VoucherDetailTableAdapter
      Dim _VoucherTableAdapter As New VoucherTableAdapter
      Dim _InventorySalesInvoiceTableAdapter As New InventorySalesInvoiceTableAdapter
      Dim _InventoryDetailTableAdapter As New InventoryDetailTableAdapter
      Dim _InventoryTableAdapter As New InventoryTableAdapter
      Dim _ItemSummaryTableAdapter As New ItemSummaryTableAdapter
      Dim _ItemTableAdapter As New ItemTableAdapter
      Dim _PartyTableAdapter As New PartyTableAdapter

      Cursor = Cursors.WaitCursor

      QuickMessageBox.Show(Me.LoginInfoObject, "This process will delete all records of the selected company [Permanently]", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
      If MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

        With Me.Quick_Spread1_Sheet1
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString OrElse .GetText(DeleteOptionRowNo.Transfers, 0) = Boolean.TrueString Then
            _TransferRecords = (New TransferTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _VoucherDetailsRecords = (New VoucherDetailTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _VoucherRecords = (New VoucherTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _InventorySalesInvoiceRecords = (New InventorySalesInvoiceTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _object = (New InventoryDetailTableAdapter).DisableAllTriggers
            _InventoryDetailRecords = (New InventoryDetailTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
            _object = (New InventoryDetailTableAdapter).EnableAllTriggers
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _InventoryRecords = (New InventoryTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _ItemSummaryRecords = (New ItemSummaryTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _ItemRecords = (New ItemTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _PartyRecords = (New PartyTableAdapter).DeleteAll(Me.LoginInfoObject.CompanyID)
          End If

          WebServerConnectionString = Configuration.ConfigurationManager.ConnectionStrings("WebServer").ConnectionString
          _TransferTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _VoucherDetailTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _VoucherTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _InventorySalesInvoiceTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _InventoryDetailTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _InventoryTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _ItemSummaryTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _ItemTableAdapter.GetConnection.ConnectionString = WebServerConnectionString
          _PartyTableAdapter.GetConnection.ConnectionString = WebServerConnectionString

          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString OrElse .GetText(DeleteOptionRowNo.Transfers, 0) = Boolean.TrueString Then
            _TransferRecordsOnline = _TransferTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _VoucherDetailsRecordsOnline = _VoucherDetailTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _VoucherRecordsOnline = (_VoucherTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID))
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _InventorySalesInvoiceRecordsOnline = _InventorySalesInvoiceTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _object = _InventoryDetailTableAdapter.DisableAllTriggers
            _InventoryDetailRecordsOnline = _InventoryDetailTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
            _object = _InventoryDetailTableAdapter.EnableAllTriggers
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _InventoryRecordsOnline = _InventoryTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _ItemSummaryRecordsOnline = _ItemSummaryTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _ItemRecordsOnline = _ItemTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
          If .GetText(DeleteOptionRowNo.AllRecords, 0) = Boolean.TrueString Then
            _PartyRecordsOnline = _PartyTableAdapter.DeleteAll(Me.LoginInfoObject.CompanyID)
          End If
        End With
      Else
        'QuickMessageBox.Show(Me.LoginInfoObject, "Nothing to delete", QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        'User choosed not to delete records
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in EmptyDatabaseButton click event method.", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default

      QuickMessageBox.Show(Me.LoginInfoObject, "Deleted Records Summary (Local)" & Environment.NewLine & "".PadRight(50, "-"c) & Environment.NewLine _
        & "Voucher Related Records = " & _VoucherDetailsRecords + _VoucherRecords & Environment.NewLine _
        & "Inventory Related Records = " & _InventoryDetailRecords + _InventorySalesInvoiceRecords + _InventoryRecords & Environment.NewLine _
        & "Item Related Records = " & _ItemSummaryRecords + _ItemRecords & Environment.NewLine _
        & "Party Related Records = " & _PartyRecords & Environment.NewLine & Environment.NewLine _
        & "Deleted Records Summary (Online)" & Environment.NewLine & "".PadRight(50, "-"c) & Environment.NewLine _
        & "Voucher Related Records = " & _VoucherDetailsRecordsOnline + _VoucherRecordsOnline & Environment.NewLine _
        & "Inventory Related Records = " & _InventoryDetailRecordsOnline + _InventorySalesInvoiceRecordsOnline + _InventoryRecordsOnline & Environment.NewLine _
        & "Item Related Records = " & _ItemSummaryRecordsOnline + _ItemRecordsOnline & Environment.NewLine _
        & "Party Related Records = " & _PartyRecordsOnline)
    End Try
  End Sub

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.Quick_Spread1.AutoNewRow = False
    Me.Quick_Spread1_Sheet1.Columns(1).Locked = True
    Me.CompanyComboBox1.Enabled = False
    Me.Quick_Spread1.ShowDeleteRowButton(Me.Quick_Spread1_Sheet1) = False
  End Sub
End Class