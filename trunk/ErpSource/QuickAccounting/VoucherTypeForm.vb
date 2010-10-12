Imports System.Windows.Forms
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDALLibrary
Imports QuickLibrary

Public Class VoucherTypeForm
#Region "Declaration"
  Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _VoucherTypeTableAdapterObject As New VoucherTypeTableAdapter
  Private _VoucherTypeDataTable As New VoucherTypeDataTable
  Private _VoucherTypeDataRow As VoucherTypeRow

#End Region

#Region "Events"

  Private Sub AccountingCOA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      InactiveFromCalendarCombo.Value = Nothing
      InactiveToCalendarCombo.Value = Nothing
      Me.VoucherTypeCodeTextBox.MaxLength = Me._VoucherTypeDataTable.VoucherType_CodeColumn.MaxLength
      Me.VoucherTypeDescTextBox.MaxLength = Me._VoucherTypeDataTable.VoucherType_DescColumn.MaxLength
      Me.VoucherTypeCodeTextBox.Text = String.Empty
      Me.VoucherTypeIDTextBox.Text = String.Empty
      Me.VoucherTypeDescTextBox.Text = String.Empty
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Load event method of VoucherTypeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Methods"

  Public Sub New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call.

  End Sub

  
  Private Function IsValid() As Boolean
    Try
      Dim _TempTable As VoucherTypeDataTable
      _TempTable = Me._VoucherTypeTableAdapterObject.GetCoIDVoucherTypeCodeDuplicate(Me.CompanyComboBox.CompanyID, Me.VoucherTypeCodeTextBox.Text)

      If Me.VoucherTypeIDTextBox.Text = String.Empty Then
        If _TempTable.Rows.Count > 0 Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Voucher code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.VoucherTypeCodeTextBox.Focus()
          Return False
        End If
      ElseIf _TempTable.Rows.Count = 1 Then
        If _TempTable.Rows(0).Item(_TempTable.VoucherType_IDColumn.ColumnName).ToString = CStr(Me.VoucherTypeIDTextBox.Text) And _TempTable.Rows(0).Item(_TempTable.VoucherType_CodeColumn.ColumnName).ToString = Me.VoucherTypeCodeTextBox.Text Then
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Voucher code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.VoucherTypeCodeTextBox.Focus()
          Return False
        End If
      End If

      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      ElseIf Me.VoucherTypeCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the type of voucher code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.VoucherTypeCodeTextBox.Focus()
        Return False
      ElseIf Me.VoucherTypeDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.VoucherTypeDescTextBox.Focus()
        Return False
      End If

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try

  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      Cursor = Cursors.WaitCursor
      Me.CompanyComboBox.Enabled = False
      If Me._VoucherTypeDataTable.Rows.Count > 0 Then
        Me.ClearControls(Me)
        Me._VoucherTypeDataRow = Me._VoucherTypeDataTable(Me.CurrentRecordIndex)
        Me.CompanyComboBox.CompanyID = _VoucherTypeDataRow.Co_ID
        Me.VoucherTypeIDTextBox.Text = _VoucherTypeDataRow.VoucherType_ID.ToString()
        Me.VoucherTypeCodeTextBox.Text = _VoucherTypeDataRow.VoucherType_Code
        Me.VoucherTypeDescTextBox.Text = _VoucherTypeDataRow.VoucherType_Desc
        If _VoucherTypeDataRow.IsInactive_FromNull Then
          InactiveFromCalendarCombo.Value = Nothing
        Else
          InactiveFromCalendarCombo.Value = _VoucherTypeDataRow.Inactive_From
        End If
        If _VoucherTypeDataRow.IsInactive_ToNull Then
          InactiveToCalendarCombo.Value = Nothing
        Else
          InactiveToCalendarCombo.Value = _VoucherTypeDataRow.Inactive_To
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord event method of VoucherTypeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Function

    Protected Overrides Function SaveRecord() As Boolean
        Try
            If IsValid() Then
                If _VoucherTypeDataRow Is Nothing Then
                    _VoucherTypeDataRow = _VoucherTypeDataTable.NewVoucherTypeRow
                    Me.VoucherTypeIDTextBox.Text = CStr(Me._VoucherTypeTableAdapterObject.GetNewVoucherTypeID(Me.CompanyComboBox.CompanyID))
                    Me._VoucherTypeDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
                Else
                    If Me._VoucherTypeDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
                        Me._VoucherTypeDataRow.RecordStatus_ID = Constants.RecordStatuses.Updated
                    End If

                End If
                _VoucherTypeDataRow.Co_ID = CompanyComboBox.CompanyID
                _VoucherTypeDataRow.VoucherType_ID = CInt(VoucherTypeIDTextBox.Text)

                _VoucherTypeDataRow.VoucherType_Code = VoucherTypeCodeTextBox.Text.Trim
                _VoucherTypeDataRow.VoucherType_Desc = VoucherTypeDescTextBox.Text.Trim

                'Hidden values
                _VoucherTypeDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
                _VoucherTypeDataRow.Stamp_DateTime = Date.Now
                _VoucherTypeDataRow.Upload_DateTime = Date.Now

        If InactiveFromCalendarCombo.Value Is DBNull.Value OrElse InactiveFromCalendarCombo.Value Is Nothing Then
          _VoucherTypeDataRow.SetInactive_FromNull()
        Else

          _VoucherTypeDataRow.Inactive_From = Convert.ToDateTime(InactiveFromCalendarCombo.Value)
        End If

        If InactiveToCalendarCombo.Value Is DBNull.Value OrElse InactiveToCalendarCombo.Value Is Nothing Then
          _VoucherTypeDataRow.SetInactive_ToNull()
        Else
          _VoucherTypeDataRow.Inactive_To = Convert.ToDateTime(InactiveToCalendarCombo.Value)
        End If

                If _VoucherTypeDataRow.RowState = DataRowState.Detached Then
                    _VoucherTypeDataTable.Rows.Add(_VoucherTypeDataRow)
                End If
                _VoucherTypeTableAdapterObject.Update(_VoucherTypeDataTable)
                Me.CompanyComboBox.ReadOnly = True
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord event method of VoucherTypeForm.", ex)
            Throw QuickExceptionObject
        End Try
    End Function

#End Region

#Region "Properties"

#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      _VoucherTypeDataTable = _VoucherTypeTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of VoucherTypeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If (_VoucherTypeDataRow Is Nothing) Then
        Me._VoucherTypeDataTable  = Me._VoucherTypeTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      Else
        _VoucherTypeDataTable = Me._VoucherTypeTableAdapterObject.GetNextByCoIDVoucherTypeID(Me.CompanyComboBox.CompanyID, _VoucherTypeDataRow.VoucherType_ID)
        If _VoucherTypeDataTable.Count = 0 Then
          Me._VoucherTypeDataTable = Me._VoucherTypeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of VoucherTypeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_VoucherTypeDataRow Is Nothing) Then
        Me._VoucherTypeDataTable = Me._VoucherTypeTableAdapterObject.GetPreviousByCoIDVoucherTypeID(Me.CompanyComboBox.CompanyID, 0)
      Else
        _VoucherTypeDataTable = Me._VoucherTypeTableAdapterObject.GetPreviousByCoIDVoucherTypeID(Me.CompanyComboBox.CompanyID, _VoucherTypeDataRow.VoucherType_ID)
      End If
     
      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of VoucherTypeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      _VoucherTypeDataTable = Me._VoucherTypeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)

      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of VoucherTypeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of VoucherTypeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ActiveCompany As Int32
      Me.CompanyComboBox.ReadOnly = False
      Me._VoucherTypeDataRow = Nothing
      Me.CompanyComboBox.Enabled = True
      _ActiveCompany = Me.CompanyComboBox.CompanyID
      Me._VoucherTypeDataRow = Nothing
      Me._VoucherTypeDataTable.Rows.Clear()
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_ActiveCompany)
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      Me.VoucherTypeCodeTextBox.Focus()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of VoucherTypeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.VoucherTypeCodeTextBox.Text = "" Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account code to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.VoucherTypeDescTextBox.Text = "" Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account description to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.VoucherTypeIDTextBox.Text = "" Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Exit Sub
      End If

      If _VoucherTypeDataTable.Rows.Count < 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      ElseIf MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._VoucherTypeDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        Me.SaveRecord()
        Me._VoucherTypeDataRow = Nothing
        '_VoucherTypeDataTable.Rows(Me.CurrentRecordIndex).Delete()
        '_VoucherTypeTableAdapterObject.Update(_VoucherTypeDataTable)
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of VoucherTypeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region
End Class