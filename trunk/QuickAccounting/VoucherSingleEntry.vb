Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickControls

Public Class VoucherSingleEntry

#Region "Declaration"
  'Data Adapters
  Dim _VoucherTableAdapterObject As New VoucherTableAdapter
  Dim _VoucherDetailTableAdapterObject As New VoucherDetailTableAdapter
  Dim _VoucherTypeTableAdapterObject As New VoucherTypeTableAdapter
  Dim _SettingTableAdaperObject As New SettingTableAdapter
  Dim _CoaTableAdapter As New COATableAdapter
  Dim _PartyTableAdapter As New PartyTableAdapter
  'Data Tables
  Private _VoucherDataTable As New VoucherDataTable
  Private _VoucherDetailDataTable As New VoucherDetailDataTable
  Private _VoucherTypeDataTable As New VoucherTypeDataTable
  Private _COADataTable As New ItemDataTable
  Private _PartyDataTable As New PartyDataTable
  'Data Rows
  Private _CurrentVoucherDataRow As VoucherRow
  Private _SelectedPartyDataRow As PartyRow

  Private _VoucherType As Int32

  'Private Const EXC_PRE As String = "Exception in "
  'Private Const EXC_grdVoucher_LEAVECELL As String = EXC_PRE & "_LeaveCell"
#End Region

#Region "Properties"
  Protected Friend Property VoucherType() As Int32
    Get
      Return _VoucherType
    End Get
    Set(ByVal value As Int32)
      _VoucherType = value
    End Set
  End Property
#End Region

#Region "Methods"
  Private Function IsValid() As Boolean
    Try

      If Convert.ToDouble(Me.AmountTextBox.Value) <= 0 Then
        MessageBox.Show("Amount must be greater than 0", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return False
      ElseIf _SelectedPartyDataRow.IsCOA_IDNull Then
        MessageBox.Show("COA is not assigned to this party, you can not save record against using this party.", "Party without COA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
      Dim VoucherID As Int32
      Dim VoucherDataRow As VoucherRow
      Dim VoucherDetailDataRow As VoucherDetailRow
      'Dim ItemID As Int32
      'Dim ItemsUsedCollection As New Collection

      If Not IsValid() Then Exit Function

      If _CurrentVoucherDataRow Is Nothing Then
        'New Record
        VoucherDataRow = _VoucherDataTable.NewVoucherRow
        VoucherID = Convert.ToInt32(_VoucherTableAdapterObject.GetNewVoucherIDByCoID(LoginInfoObject.CompanyID))
        VoucherDataRow.Voucher_No = _VoucherTableAdapterObject.GetNewVoucherNoByCoIDVoucherTypeID(LoginInfoObject.CompanyID, Me.VoucherType).ToString
        VoucherDataRow.Voucher_ID = VoucherID
        VoucherDataRow.RecordStatus_ID = 7
        VoucherNoTextBox.Text = VoucherDataRow.Voucher_No
        VoucherIDLabel.Text = "Voucher ID: " & VoucherDataRow.Voucher_ID
        _CurrentVoucherDataRow = VoucherDataRow
      Else
        'In case of updated only common properties need to be set.
      End If

      'Common values to set in case of insert/ update.
      With _CurrentVoucherDataRow
        .Co_ID = LoginInfoObject.CompanyID
        .VoucherType_ID = Me.VoucherType
        .Remarks = RemarksTextBox.Text
        .Voucher_Date = Convert.ToDateTime(VoucherDateCalendar.Value)
        .Stamp_DateTime = Now
        .Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
      End With
      If _CurrentVoucherDataRow.RowState = DataRowState.Detached Then
        Me._VoucherDataTable.Rows.Add(_CurrentVoucherDataRow)
      End If

      _VoucherTableAdapterObject.Update(_CurrentVoucherDataRow)

      'Save Detail
      If _VoucherDetailDataTable.Rows.Count <= 0 Then
        'Cash Entry
        VoucherDetailDataRow = _VoucherDetailDataTable.NewVoucherDetailRow
        VoucherDetailDataRow.Co_ID = LoginInfoObject.CompanyID
        VoucherDetailDataRow.COA_ID = 1   'Cash COA
        VoucherDetailDataRow.Voucher_ID = _CurrentVoucherDataRow.Voucher_ID
        VoucherDetailDataRow.VoucherDetail_ID = 1
        VoucherDetailDataRow.CreditAmount = 0
        VoucherDetailDataRow.DebitAmount = 0
        VoucherDetailDataRow.Narration = ""
        VoucherDetailDataRow.Stamp_DateTime = Now
        VoucherDetailDataRow.Stamp_User_Id = LoginInfoObject.UserID
        _VoucherDetailDataTable.Rows.Add(VoucherDetailDataRow)

        'Party Entry
        VoucherDetailDataRow = _VoucherDetailDataTable.NewVoucherDetailRow
        VoucherDetailDataRow.Co_ID = LoginInfoObject.CompanyID
        VoucherDetailDataRow.COA_ID = _SelectedPartyDataRow.COA_ID
        VoucherDetailDataRow.Party_ID = _SelectedPartyDataRow.Party_ID
        VoucherDetailDataRow.Voucher_ID = _CurrentVoucherDataRow.Voucher_ID
        VoucherDetailDataRow.VoucherDetail_ID = 2
        VoucherDetailDataRow.CreditAmount = 0
        VoucherDetailDataRow.DebitAmount = 0
        VoucherDetailDataRow.Narration = ""
        VoucherDetailDataRow.Stamp_DateTime = Now
        VoucherDetailDataRow.Stamp_User_Id = LoginInfoObject.UserID
        _VoucherDetailDataTable.Rows.Add(VoucherDetailDataRow)
      End If

      For I As Int32 = 0 To _VoucherDetailDataTable.Rows.Count - 1
        VoucherDetailDataRow = DirectCast(_VoucherDetailDataTable.Rows(I), VoucherDetailRow)

        If Me.DocumentType = enuDocumentType.ReceiptVoucher Then
          If VoucherDetailDataRow.COA_ID = _SelectedPartyDataRow.COA_ID Then
            VoucherDetailDataRow.CreditAmount = Convert.ToDecimal(AmountTextBox.Value)
            VoucherDetailDataRow.DebitAmount = 0
          Else
            VoucherDetailDataRow.DebitAmount = Convert.ToDecimal(AmountTextBox.Value)
            VoucherDetailDataRow.CreditAmount = 0
          End If
        ElseIf Me.DocumentType = enuDocumentType.PaymentVoucher Then
          If VoucherDetailDataRow.COA_ID = _SelectedPartyDataRow.COA_ID Then
            VoucherDetailDataRow.DebitAmount = Convert.ToDecimal(AmountTextBox.Value)
            VoucherDetailDataRow.CreditAmount = 0
          Else
            VoucherDetailDataRow.CreditAmount = Convert.ToDecimal(AmountTextBox.Value)
            VoucherDetailDataRow.DebitAmount = 0
          End If
        End If
        VoucherDetailDataRow.Narration = RemarksTextBox.Text
        VoucherDetailDataRow.Stamp_DateTime = Now
        VoucherDetailDataRow.Stamp_User_Id = LoginInfoObject.UserID

        If VoucherDetailDataRow.RowState = DataRowState.Added Then
          VoucherDetailDataRow.VoucherDetail_ID = Convert.ToInt16(_VoucherDetailTableAdapterObject.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _CurrentVoucherDataRow.Voucher_ID))
          VoucherDetailDataRow.Voucher_ID = _CurrentVoucherDataRow.Voucher_ID
          VoucherDetailDataRow.Co_ID = LoginInfoObject.CompanyID
        ElseIf VoucherDetailDataRow.RowState = DataRowState.Detached Then
          'Won't execute in normal scenario, just a precaution
          _VoucherDetailDataTable.Rows.Add(VoucherDetailDataRow)
        End If
        'Update Database
        _VoucherDetailTableAdapterObject.Update(VoucherDetailDataRow)
      Next I

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      Dim VoucherDetailDataRow As VoucherDetailRow
      'Dim ItemDataRow As ItemRow

      If _VoucherDataTable IsNot Nothing AndAlso Me._VoucherDataTable.Rows.Count > 0 Then
        Me._CurrentVoucherDataRow = CType(Me._VoucherDataTable.Rows(Me.CurrentRecordIndex), VoucherRow)
        Me.CurrentRecordDataRow = Me._CurrentVoucherDataRow

        Me.ClearControls(Me)
        Me.CurrentRecordDataRow = Nothing
        'If Me._VoucherDetailDataTable IsNot Nothing Then
        '  Do While Me._VoucherDetailDataTable.Rows.Count > 0
        '    Me._VoucherDetailDataTable.Rows.RemoveAt(0)
        '  Loop
        'End If

        VoucherIDLabel.Text = "Voucher ID: " & Me._CurrentVoucherDataRow.Voucher_ID.ToString
        VoucherNoTextBox.Text = Me._CurrentVoucherDataRow.Voucher_No
        VoucherDateCalendar.Value = Me._CurrentVoucherDataRow.Voucher_Date
        RemarksTextBox.Text = Me._CurrentVoucherDataRow.Remarks

        Me._VoucherDetailDataTable = _VoucherDetailTableAdapterObject.GetByCoIDVoucherID(Me._CurrentVoucherDataRow.Co_ID, Me._CurrentVoucherDataRow.Voucher_ID)
        For I As Int32 = 0 To Me._VoucherDetailDataTable.Rows.Count - 1
          If Not Me._VoucherDetailDataTable(I).IsParty_IDNull Then
            For J As Int32 = 0 To Me.PartyComboBox.Rows.Count - 1
              VoucherDetailDataRow = Me._VoucherDetailDataTable(I)
              If VoucherDetailDataRow.Party_ID = Convert.ToInt32(PartyComboBox.Rows(J).Cells(_VoucherDetailDataTable.Party_IDColumn.ColumnName).Value) Then
                If VoucherDetailDataRow.DebitAmount > 0 Then
                  AmountTextBox.Value = VoucherDetailDataRow.DebitAmount
                Else
                  AmountTextBox.Value = VoucherDetailDataRow.CreditAmount
                End If
                PartyComboBox.ActiveRow = PartyComboBox.Rows(J)
              End If
            Next
          End If
        Next
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Sub NewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)

    PartyComboBox.SelectedRow = Nothing
  End Sub

  Protected Overrides Sub ClearControls(ByRef pControlObject As System.Windows.Forms.Control)
    Try
      MyBase.ClearControls(pControlObject)
      If pControlObject.Name = Me.VoucherIDLabel.Name Then
        VoucherIDLabel.Text = "Voucher ID:"
      End If
      'Me.grdVoucher.ActiveSheet.Rows.Remove(0, Me.grdVoucher.ActiveSheet.Rows.Count)
      'Me.grdVoucher.ActiveSheet.Rows.Add(0, 1)
      'Me.CurrentRecordDataRow = Nothing
      'Do While Me._VoucherDetailDataTable.Rows.Count > 0
      '  Me._VoucherDetailDataTable.Rows.RemoveAt(0)
      'Loop
      'AddItem()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to clear controls", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Me._VoucherDataTable = _VoucherTableAdapterObject.GetFirstByCoIDVoucherTypeID(LoginInfoObject.CompanyID, Me.VoucherType)
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

      If Me._CurrentVoucherDataRow IsNot Nothing Then
        Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetNextByCoIDVoucherIDVoucherTypeID(Me._CurrentVoucherDataRow.Co_ID, Me._CurrentVoucherDataRow.Voucher_ID, Me.VoucherType)
      Else
        Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetNextByCoIDVoucherIDVoucherTypeID(LoginInfoObject.CompanyID, 0, Me.VoucherType)
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

      If Me._CurrentVoucherDataRow IsNot Nothing Then
        Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetPreviousByCoIDVoucherIDVoucherTypeID(Me._CurrentVoucherDataRow.Co_ID, Me._CurrentVoucherDataRow.Voucher_ID, Me.VoucherType)
      Else
        Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetPreviousByCoIDVoucherIDVoucherTypeID(LoginInfoObject.CompanyID, 0, Me.VoucherType)
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

      If Me._CurrentVoucherDataRow IsNot Nothing Then
        Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetLastByCoIDVoucherTypeID(LoginInfoObject.CompanyID, Me.VoucherType)
      Else
        Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetLastByCoIDVoucherTypeID(LoginInfoObject.CompanyID, Me.VoucherType)
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
        QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
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
      Me._CurrentVoucherDataRow = Nothing
      'Me._VoucherDataTable = Nothing
      'Me._VoucherDetailDataTable = Nothing
      Me._VoucherDataTable.Rows.Clear()
      Me._VoucherDetailDataTable.Rows.Clear()
      'We should set the date to system date otherwise last record viewed date will be used.
      Me.VoucherDateCalendar.Value = Now
      MyBase.CancelButtonClick(sender, e)

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

      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._CurrentVoucherDataRow.RecordStatus_ID = RecordStatuses.Deleted

        If SaveRecord() Then
          QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.DeleteSuccessfulMessage)
          MyBase.DeleteButtonClick(sender, e)
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.DeleteUnSuccessfulMessage)
        End If
      Else
        'User cancelled the delete operation.
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to delete button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 22-Feb-10
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Protected Overrides Sub SearchButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _SearchForm As New QuickBaseForms.SearchForm

      _SearchForm.LoginInfoObject = DirectCast(Me.LoginInfoObject.Clone, LoginInfo)
      If Me.DocumentType = enuDocumentType.PaymentVoucher Then
        _SearchForm.SearchOption = QuickBaseForms.SearchForm.SearchOptionIDs.Payment
        _SearchForm.DocumentType = enuDocumentType.PaymentVoucher
      ElseIf Me.DocumentType = enuDocumentType.ReceiptVoucher Then
        _SearchForm.SearchOption = QuickBaseForms.SearchForm.SearchOptionIDs.Receipt
        _SearchForm.DocumentType = enuDocumentType.ReceiptVoucher
      End If

      _SearchForm.ShowDialog()
      If _SearchForm.SearchResultUnTypedDataTable IsNot Nothing AndAlso _SearchForm.SearchResultUnTypedDataTable.Rows.Count > 0 Then
        With _SearchForm.SearchResultUnTypedDataTable.Rows(0)
          Me._VoucherDataTable = Me._VoucherTableAdapterObject.GetByCoIDVoucherID(DirectCast(.Item(_VoucherDataTable.Co_IDColumn.ColumnName), Int16), DirectCast(.Item(_VoucherDataTable.Voucher_IDColumn.ColumnName), Int32))
          ShowRecord()
        End With
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "No Record Found", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      End If
      _SearchForm.Close()

      MyBase.SearchButtonClick(sender, e)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SearchButtonClick of VoucherSingleEntry.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

#Region "Events"

  Private Sub frmSalesInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try

      _PartyDataTable = _PartyTableAdapter.GetByCoID(LoginInfoObject.CompanyID)
      Me.PartyComboBox.DataSource = _PartyDataTable
      Me.PartyComboBox.DisplayMember = _PartyDataTable.Party_DescColumn.ColumnName
      Me.PartyComboBox.ValueMember = _PartyDataTable.Party_IDColumn.ColumnName
      For I As Int32 = 0 To Me.PartyComboBox.Rows.Band.Columns.Count - 1
        Select Case Me.PartyComboBox.Rows.Band.Columns(I).Key
          Case _PartyDataTable.Party_DescColumn.ColumnName
          Case _PartyDataTable.Party_IDColumn.ColumnName
          Case Else
            Me.PartyComboBox.Rows.Band.Columns(I).Hidden = True
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to load form", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub PartyComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PartyComboBox.ValueChanged
    Try
      Me._SelectedPartyDataRow = Nothing
      If Me.PartyComboBox.ActiveRow Is Nothing Then Exit Sub

      For I As Int32 = 0 To Me._PartyDataTable.Rows.Count - 1
        If Convert.ToInt32(_PartyDataTable.Rows(I).Item(Me._PartyDataTable.Party_IDColumn.ColumnName)) = Convert.ToInt32(Me.PartyComboBox.ActiveRow.Cells(Me._PartyDataTable.Party_IDColumn.ColumnName).Value) Then
          Me._SelectedPartyDataRow = CType(_PartyDataTable.Rows(I), PartyRow)
        End If
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to select party", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    'Me.FormDataSet.Tables.Add(Me._InventoryDataTable)
    'Me.FormDataSet.Tables.Add(Me._InventoryDetailDataTable)
    Me.CurrentRecordDataRow = Me._CurrentVoucherDataRow
    Me.VoucherDateCalendar.Value = Now
  End Sub

End Class