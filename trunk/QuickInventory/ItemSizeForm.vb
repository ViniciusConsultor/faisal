Imports System.Windows.Forms
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports QuickDALLibrary
Imports QuickLibrary
Public Class ItemSizeForm

#Region "Declaration"
  Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _ItemSizeTableAdapterObject As New ItemSizeTableAdapter
  Private _ItemSizeDataTable As New ItemSizeDataTable
  Private _ItemSizeDataRow As ItemSizeRow
#End Region

#Region "Events"
  Private Sub ItemSizeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      InactiveFromCalendarCombo.Value = Nothing
      InactiveToCalendarCombo.Value = Nothing

      Me.ItemSizeCodeTextBox.MaxLength = Me._ItemSizeDataTable.ItemSize_CodeColumn.MaxLength
      Me.ItemSizeDescTextBox.MaxLength = Me._ItemSizeDataTable.ItemSize_DescColumn.MaxLength

      Me.ItemSizeCodeTextBox.Text = String.Empty
      Me.ItemSizeDescTextBox.Text = String.Empty
      Me.ItemSizeIDTextBox.Text = String.Empty

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Load event method of ItemSizeForm.", ex)
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
      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      ElseIf Me.ItemSizeCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the Item size code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ItemSizeCodeTextBox.Focus()
        Return False
      ElseIf Me.ItemSizeDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the item size description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ItemSizeDescTextBox.Focus()
        Return False
        'ElseIf Convert.ToDateTime(InactiveFromCalendarCombo.Value) > Convert.ToDateTime(InactiveToCalendarCombo.Value) Then
        '  QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the valid inactive date to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        '  InactiveFromCalendarCombo.Focus()
        '  Return False
      End If

      Dim _CheckDuplicateCode As String
      If Me.ItemSizeIDTextBox.Text = String.Empty Then
        _CheckDuplicateCode = CStr(Me._ItemSizeTableAdapterObject.GetCoIDDuplicateItemSizeCode(Me.CompanyComboBox.CompanyID, Me.ItemSizeCodeTextBox.Text))
      Else
        _CheckDuplicateCode = Me._ItemSizeTableAdapterObject.GetCoIDDuplicateItemSizeCodeByItemSizeID(Me.CompanyComboBox.CompanyID, CInt(Me.ItemSizeIDTextBox.Text), Me.ItemSizeCodeTextBox.Text)
      End If

      If _CheckDuplicateCode <> String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate item size code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
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
      If Me._ItemSizeDataTable.Rows.Count > 0 Then
        Me.ClearControls(Me)
        Me._ItemSizeDataRow = Me._ItemSizeDataTable(Me.CurrentRecordIndex)
        Me.CompanyComboBox.CompanyID = _ItemSizeDataRow.Co_ID
        Me.ItemSizeIDTextBox.Text = _ItemSizeDataRow.ItemSize_ID.ToString()
        Me.ItemSizeCodeTextBox.Text = _ItemSizeDataRow.ItemSize_Code
        Me.ItemSizeDescTextBox.Text = _ItemSizeDataRow.ItemSize_Desc
        If _ItemSizeDataRow.IsInactive_FromNull Then
          InactiveFromCalendarCombo.Value = Nothing
        Else
          InactiveFromCalendarCombo.Value = _ItemSizeDataRow.Inactive_From
        End If
        If _ItemSizeDataRow.IsInactive_ToNull Then
          InactiveToCalendarCombo.Value = Nothing
        Else
          InactiveToCalendarCombo.Value = _ItemSizeDataRow.Inactive_To
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord event method of ItemSizeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If _ItemSizeDataRow Is Nothing Then
          _ItemSizeDataRow = Me._ItemSizeDataTable.NewItemSizeRow
          Me.ItemSizeIDTextBox.Text = CStr(Me._ItemSizeTableAdapterObject.GetNewItemSizeIDbyCoID(Me.CompanyComboBox.CompanyID))
          _ItemSizeDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        Else
          If _ItemSizeDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            _ItemSizeDataRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If
        End If
        With _ItemSizeDataRow
          .Co_ID = CompanyComboBox.CompanyID
          .ItemSize_ID = CShort(CInt(Me.ItemSizeIDTextBox.Text))
          .ItemSize_Code = Me.ItemSizeCodeTextBox.Text.Trim
          .ItemSize_Desc = Me.ItemSizeDescTextBox.Text.Trim
          'Hidden values
          .Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
          .Stamp_DateTime = Date.Now
          .Upload_DateTime = Date.Now

          If InactiveFromCalendarCombo.Value Is DBNull.Value OrElse InactiveFromCalendarCombo.Value Is Nothing Then
            .SetInactive_FromNull()
          Else
            .Inactive_From = Convert.ToDateTime(InactiveFromCalendarCombo.Value)
          End If

          If InactiveToCalendarCombo.Value Is DBNull.Value OrElse InactiveToCalendarCombo.Value Is Nothing Then
            .SetInactive_ToNull()
          Else
            .Inactive_To = Convert.ToDateTime(InactiveToCalendarCombo.Value)
          End If

          If .RowState = DataRowState.Detached Then
            Me._ItemSizeDataTable.Rows.Add(Me._ItemSizeDataRow)
          End If
          Me._ItemSizeTableAdapterObject.Update(_ItemSizeDataTable)
          Me.CompanyComboBox.ReadOnly = True
        End With
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord event method of ItemSizeForm.", ex)
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
      Me._ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetFirstbyCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of ItemSizeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (Me._ItemSizeDataRow Is Nothing) Then
        Me._ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetFirstbyCoID(Me.CompanyComboBox.CompanyID)
      Else
        _ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetNextByCoIDItemSizeID(Me.CompanyComboBox.CompanyID, CInt(Me.ItemSizeIDTextBox.Text))
        If _ItemSizeDataTable.Count = 0 Then
          Me._ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of ItemSizeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_ItemSizeDataRow Is Nothing) Then
        Me._ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetPreviousByCoIDItemSizeID(Me.CompanyComboBox.CompanyID, 0)
      Else
        _ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetPreviousByCoIDItemSizeID(Me.CompanyComboBox.CompanyID, _ItemSizeDataRow.ItemSize_ID)
      End If

      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of ItemSizeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._ItemSizeDataTable = Me._ItemSizeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of ItemSizeForm", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of ItemSizeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ActiveCompany As Int32
      Me.CompanyComboBox.ReadOnly = False
      ' Me._ItemSizeDataRow = Nothing
      Me.CompanyComboBox.Enabled = True
      _ActiveCompany = Me.CompanyComboBox.CompanyID
      Me._ItemSizeDataRow = Nothing
      Me._ItemSizeDataTable.Rows.Clear()
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_ActiveCompany)
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      Me.ItemSizeCodeTextBox.Focus()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of ItemSizeForm", ex)
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
      ElseIf Me.ItemSizeIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Exit Sub
      End If

      If Me._ItemSizeDataTable.Rows.Count < 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      ElseIf MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._ItemSizeDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        Me.SaveRecord()
        Me._ItemSizeDataRow = Nothing
        '_VoucherTypeDataTable.Rows(Me.CurrentRecordIndex).Delete()
        '_VoucherTypeTableAdapterObject.Update(_VoucherTypeDataTable)
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of ItemSizeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

  
End Class