Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickSecurityDataSetTableAdapters
Imports QuickDAL.QuickSecurityDataSet
Imports QuickDALLibrary
Imports QuickLibrary
Imports Infragistics.Win.UltraWinGrid
Imports System.Text.RegularExpressions

Public Class SecurityRoleForm

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

#Region "Declaration"

  Private _SecurityRoleTableAdapterObject As New SecurityRoleTableAdapter
  Private _SecurityRoleDataTable As New SecurityRoleDataTable
  Private _CurrentSecurityDataRow As SecurityRoleRow
  Private _TempDataTable As New SecurityRoleDataTable
#End Region

#Region "Events"

  Private Sub CompanyForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CancelButtonClick(Me, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CompanyForm Load event.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Methods"

  Private Function IsValid() As Boolean
    Try
      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Please select a valid Company.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      ElseIf RoleDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Please enter role description to save record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in IsValid method of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If _CurrentSecurityDataRow Is Nothing Then
          _CurrentSecurityDataRow = _SecurityRoleDataTable.NewSecurityRoleRow
          _CurrentSecurityDataRow.Role_ID = Convert.ToInt32(_SecurityRoleTableAdapterObject.GetNewRoleID(Me.CompanyComboBox.CompanyID))
        Else
          'Only common properties needs to be set when modifying record.
        End If

        With _CurrentSecurityDataRow
          .Co_ID = CompanyComboBox.CompanyID
          .Role_Desc = RoleDescTextBox.Text
          If InactiveFromCalendarCombo.Value Is DBNull.Value OrElse InactiveFromCalendarCombo.Value Is Nothing Then
            _CurrentSecurityDataRow.SetInactive_FromNull()
          Else
            _CurrentSecurityDataRow.Inactive_From = Convert.ToDateTime(InactiveFromCalendarCombo.Value)
          End If
          If InactiveToCalendarCombo.Value Is DBNull.Value OrElse InactiveToCalendarCombo.Value Is Nothing Then
            _CurrentSecurityDataRow.SetInactive_ToNull()
          Else
            _CurrentSecurityDataRow.Inactive_To = Convert.ToDateTime(InactiveToCalendarCombo.Value)
          End If
          .Stamp_DateTime = Common.SystemDateTime()
          .Stamp_UserID = LoginInfoObject.UserID
        End With
      End If

      If _CurrentSecurityDataRow.RowState = DataRowState.Detached Then
        _SecurityRoleDataTable.Rows.Add(_CurrentSecurityDataRow)
      End If

      _SecurityRoleTableAdapterObject.Update(_SecurityRoleDataTable)
      Me.RoleIDTextBox.Text = Me._CurrentSecurityDataRow.Role_ID.ToString
      Return True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord of SecurityRoleForm.", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try

      If Me._SecurityRoleDataTable.Rows.Count > 0 Then
        CompanyComboBox.Enabled = False
        _CurrentSecurityDataRow = _SecurityRoleDataTable(0)
        CompanyComboBox.CompanyID = _CurrentSecurityDataRow.Co_ID
        RoleIDTextBox.Text = _CurrentSecurityDataRow.Role_ID.ToString
        RoleDescTextBox.Text = _CurrentSecurityDataRow.Role_Desc

        If _CurrentSecurityDataRow.IsInactive_FromNull Then
          InactiveFromCalendarCombo.Value = Nothing
        Else
          InactiveFromCalendarCombo.Value = _CurrentSecurityDataRow.Inactive_From
        End If
        If _CurrentSecurityDataRow.IsInactive_ToNull Then
          InactiveToCalendarCombo.Value = Nothing
        Else
          InactiveToCalendarCombo.Value = _CurrentSecurityDataRow.Inactive_To
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
    End Try
  End Function
#End Region

#Region "Properties"

#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      _SecurityRoleDataTable = Me._SecurityRoleTableAdapterObject.GetFirstByCoID(CompanyComboBox.CompanyID)

      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If _CurrentSecurityDataRow Is Nothing Then
        _TempDataTable = Me._SecurityRoleTableAdapterObject.GetNextByCoIDRoleID(CompanyComboBox.CompanyID, 0)
        _SecurityRoleDataTable = _TempDataTable
      Else
        _TempDataTable = Me._SecurityRoleTableAdapterObject.GetNextByCoIDRoleID(CompanyComboBox.CompanyID, Me._CurrentSecurityDataRow.Role_ID)
        If _TempDataTable.Rows.Count > 0 Then
          _SecurityRoleDataTable = _TempDataTable
        Else
          'If no next record is found then leave existing record, so no need to set anything.
        End If
      End If

      MyBase.MoveNextButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If _CurrentSecurityDataRow Is Nothing Then
        _TempDataTable = Me._SecurityRoleTableAdapterObject.GetPreviousByCoIdRoleID(CompanyComboBox.CompanyID, 0)
        _SecurityRoleDataTable = _TempDataTable
      Else
        _TempDataTable = Me._SecurityRoleTableAdapterObject.GetPreviousByCoIdRoleID(CompanyComboBox.CompanyID, Me._CurrentSecurityDataRow.Role_ID)
        If _TempDataTable.Rows.Count > 0 Then
          _SecurityRoleDataTable = _TempDataTable
        Else
          'If no next record is found then leave existing record, so no need to set anything.
        End If
      End If

      MyBase.MovePreviousButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      _SecurityRoleDataTable = Me._SecurityRoleTableAdapterObject.GetLastByCoID(CompanyComboBox.CompanyID)

      MyBase.MoveLastButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of SecurityRoleForm.", ex)
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
        QuickMessageBox.Show(Me.LoginInfoObject, "Record is not successfully saved", QuickMessageBox.MessageBoxTypes.ShortMessage)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick method of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      Me._CurrentSecurityDataRow = Nothing
      Me.CompanyComboBox.Enabled = True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick method of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If _SecurityRoleDataTable.Rows.Count < 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)

        Return
      ElseIf MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        _SecurityRoleDataTable.Rows(Me.CurrentRecordIndex).Delete()
        _SecurityRoleTableAdapterObject.Update(_SecurityRoleDataTable)
        _CurrentSecurityDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)

        QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)

      Else
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of SecurityRoleForm.", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

End Class