Imports System.Windows.Forms
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports QuickDALLibrary
Imports QuickLibrary
Public Class ItemGradeForm
#Region "Declaration"
  Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _ItemGradeTableAdapterObject As New ItemGradeTableAdapter
  Private _ItemGradeDataTable As New ItemGradeDataTable
  Private _ItemGradeDataRow As ItemGradeRow
#End Region

#Region "Events"
  Private Sub ItemGradeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      InactiveFromCalendarCombo.Value = Nothing
      InactiveToCalendarCombo.Value = Nothing

      Me.ItemGradeCodeTextBox.MaxLength = Me._ItemGradeDataTable.ItemGrade_CodeColumn.MaxLength
      Me.ItemGradeDescTextBox.MaxLength = Me._ItemGradeDataTable.ItemGrade_DescColumn.MaxLength

      Me.ItemGradeCodeTextBox.Text = String.Empty
      Me.ItemGradeDescTextBox.Text = String.Empty
      Me.ItemGradeIDTextBox.Text = String.Empty

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Load event method of ItemGradeForm.", ex)
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
      ElseIf Me.ItemGradeCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the Item Grade code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ItemGradeCodeTextBox.Focus()
        Return False
      ElseIf Me.ItemGradeDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the item Grade description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ItemGradeDescTextBox.Focus()
        Return False
      End If

      Dim _CheckDuplicateCode As String
      If Me.ItemGradeIDTextBox.Text = String.Empty Then
        _CheckDuplicateCode = CStr(Me._ItemGradeTableAdapterObject.GetCoIDDuplicateItemGradeCode(Me.CompanyComboBox.CompanyID, Me.ItemGradeCodeTextBox.Text))
      Else
        _CheckDuplicateCode = Me._ItemGradeTableAdapterObject.GetCoIDDuplicateItemGradeCodeByItemGradeID(Me.CompanyComboBox.CompanyID, CInt(Me.ItemGradeIDTextBox.Text), Me.ItemGradeCodeTextBox.Text)
      End If

      If _CheckDuplicateCode <> String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Item Grade Code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
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
      If Me._ItemGradeDataTable.Rows.Count > 0 Then
        Me.ClearControls(Me)
        Me._ItemGradeDataRow = Me._ItemGradeDataTable(Me.CurrentRecordIndex)
        Me.CompanyComboBox.CompanyID = _ItemGradeDataRow.Co_ID
        Me.ItemGradeIDTextBox.Text = _ItemGradeDataRow.ItemGrade_ID.ToString()
        Me.ItemGradeCodeTextBox.Text = _ItemGradeDataRow.ItemGrade_Code
        Me.ItemGradeDescTextBox.Text = _ItemGradeDataRow.ItemGrade_Desc
        If _ItemGradeDataRow.IsInactive_FromNull Then
          InactiveFromCalendarCombo.Value = Nothing
        Else
          InactiveFromCalendarCombo.Value = _ItemGradeDataRow.Inactive_From
        End If
        If _ItemGradeDataRow.IsInactive_ToNull Then
          InactiveToCalendarCombo.Value = Nothing
        Else
          InactiveToCalendarCombo.Value = _ItemGradeDataRow.Inactive_To
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord event method of ItemGradeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If _ItemGradeDataRow Is Nothing Then
          _ItemGradeDataRow = Me._ItemGradeDataTable.NewItemGradeRow
          Me.ItemGradeIDTextBox.Text = CStr(Me._ItemGradeTableAdapterObject.GetNewItemGradeIDByCoID(Me.CompanyComboBox.CompanyID))
          _ItemGradeDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        Else
          If _ItemGradeDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            _ItemGradeDataRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If
        End If
        With _ItemGradeDataRow
          .Co_ID = CompanyComboBox.CompanyID
          .ItemGrade_ID = CShort(CInt(Me.ItemGradeIDTextBox.Text))
          .ItemGrade_Code = Me.ItemGradeCodeTextBox.Text.Trim
          .ItemGrade_Desc = Me.ItemGradeDescTextBox.Text.Trim
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
            Me._ItemGradeDataTable.Rows.Add(Me._ItemGradeDataRow)
          End If
          Me._ItemGradeTableAdapterObject.Update(_ItemGradeDataTable)
          Me.CompanyComboBox.ReadOnly = True
        End With
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord event method of ItemGradeForm.", ex)
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
      Me._ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of ItemGradeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (Me._ItemGradeDataRow Is Nothing) Then
        Me._ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      Else
        _ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetNextByCoIDItemGradeID(Me.CompanyComboBox.CompanyID, CInt(Me.ItemGradeIDTextBox.Text))
        If _ItemGradeDataTable.Count = 0 Then
          Me._ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of ItemGradeForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_ItemGradeDataRow Is Nothing) Then
        Me._ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetPreviousByCoIDItemGradeID(Me.CompanyComboBox.CompanyID, 0)
      Else
        _ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetPreviousByCoIDItemGradeID(Me.CompanyComboBox.CompanyID, _ItemGradeDataRow.ItemGrade_ID)
      End If

      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of ItemGradeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._ItemGradeDataTable = Me._ItemGradeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of ItemGradeForm", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of ItemGradeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ActiveCompany As Int32
      Me.CompanyComboBox.ReadOnly = False
      Me.CompanyComboBox.Enabled = True
      _ActiveCompany = Me.CompanyComboBox.CompanyID
      Me._ItemGradeDataRow = Nothing
      Me._ItemGradeDataTable.Rows.Clear()
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_ActiveCompany)
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      Me.ItemGradeCodeTextBox.Focus()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of ItemGradeForm", ex)
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
      ElseIf Me.ItemGradeIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Exit Sub
      End If

      If Me._ItemGradeDataTable.Rows.Count < 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      ElseIf MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._ItemGradeDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        Me.SaveRecord()
        Me._ItemGradeDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of ItemGradeForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

  
 
End Class