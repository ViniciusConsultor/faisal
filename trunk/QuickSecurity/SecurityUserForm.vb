Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickSecurityDataSet
Imports QuickDAL.QuickSecurityDataSetTableAdapters
Imports QuickDALLibrary
Imports QuickLibrary
Imports Infragistics.Win.UltraWinGrid

Public Class SecurityUserForm

#Region "Declaration"

  Private _SecurityUserTableAdapterObject As New UserTableAdapter
  private _UserRoleAssociationTA As New QuickSecurityDataSetTableAdapters.UserRoleAssociationTableAdapter
  Private _UserRoleAssociationTable As New QuickSecurityDataSet.UserRoleAssociationDataTable
  ' Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _SecurityUserDataTable As New UserDataTable
  Private _CurrentDataTable As UserDataTable
  Private _CurrentSecurityUserDataRow As UserRow
  Private Const _CompanyId As String = "Co_ID"
  Private Const _CompanyCode As String = "Co_Code"
  Private Const _CompanyDescription As String = "Co_Desc"


#End Region

#Region "Events"

  Private Sub CompanyForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      LoadRoles()

      InactiveFromCalendarCombo.Value = Nothing
      InactiveToCalendarCombo.Value = Nothing

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception on the Load form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 12-Jun-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It populates and formats user roles combo box.
  ''' </summary>
  Private Sub LoadRoles()
    Try
      Dim _SecurityRoleTA As New QuickSecurityDataSetTableAdapters.SecurityRoleTableAdapter
      Dim _SecurityRoleTable As QuickSecurityDataSet.SecurityRoleDataTable = Nothing

      _SecurityRoleTable = _SecurityRoleTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.RolesComboBox1.DataSource = _SecurityRoleTable
      Me.RolesComboBox1.DisplayMember = _SecurityRoleTable.Role_DescColumn.ColumnName
      Me.RolesComboBox1.ValueMember = _SecurityRoleTable.Role_IDColumn.ColumnName

      General.SetColumnCaptions(DirectCast(_SecurityRoleTable, DataTable), Me.Name)

      For I As Int32 = 0 To Me.RolesComboBox1.Rows.Band.Columns.BoundColumnsCount - 1
        With Me.RolesComboBox1.Rows.Band.Columns(I)
          Select Case .Key
            Case _SecurityRoleTable.Role_DescColumn.ColumnName
              Me.RolesComboBox1.Rows.Band.Columns(I).Width = Me.RolesComboBox1.Width - Constants.SCROLLBAR_WIDTH
            Case Else
              .Hidden = True
          End Select
        End With
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in LoadRoles of SecurityUserForm.", ex)
      Throw _qex
    End Try
  End Sub


  Private Function IsValid() As Boolean
    Try
      Dim _Valid As Boolean = False

      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should the company to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False

      ElseIf _CurrentSecurityUserDataRow Is Nothing AndAlso _SecurityUserTableAdapterObject.GetByUserName(UserNameTextBox.Text).Count > 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "User name already exists, you should change the user name", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False

      ElseIf RolesComboBox1.SelectedRow Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "User must have a role.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.RolesComboBox1.Focus()
        Return False

      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
    Return True
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      If Me._SecurityUserDataTable.Rows.Count > 0 Then
        Me._CurrentSecurityUserDataRow = Me._SecurityUserDataTable(Me.CurrentRecordIndex)

        Me.ClearControls(Me)

        Me.CompanyComboBox.CompanyID = _CurrentSecurityUserDataRow.Co_ID
        Me.UserIDTextBox.Text = _CurrentSecurityUserDataRow.User_ID.ToString()
        Me.UserNameTextBox.Text = _CurrentSecurityUserDataRow.User_Name
        Me.PasswordTextBox.Text = _CurrentSecurityUserDataRow.Password
        If _CurrentSecurityUserDataRow.IsUser_DescNull Then
          Me.UserDescTextBox.Text = String.Empty
        Else
          Me.UserDescTextBox.Text = _CurrentSecurityUserDataRow.User_Desc
        End If
        Me.IsAdminCheckBox.Checked = _CurrentSecurityUserDataRow.Is_Admin
        If _CurrentSecurityUserDataRow.IsInactive_FromNull Then
          InactiveFromCalendarCombo.Value = Nothing
        Else
          InactiveFromCalendarCombo.Value = _CurrentSecurityUserDataRow.Inactive_From
        End If
        If _CurrentSecurityUserDataRow.IsInactive_ToNull Then
          InactiveToCalendarCombo.Value = Nothing
        Else
          InactiveToCalendarCombo.Value = _CurrentSecurityUserDataRow.Inactive_To
        End If

        _UserRoleAssociationTable = _UserRoleAssociationTA.GetByCoIDUserID(CompanyComboBox.CompanyID, _CurrentSecurityUserDataRow.User_ID)
        If _UserRoleAssociationTable.Rows.Count > 0 Then
          RolesComboBox1.SelectedRow = Nothing
          RolesComboBox1.Value = _UserRoleAssociationTable(0).Role_ID
        Else
          RolesComboBox1.Value = Nothing
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim _UserRoleAssociationRow As QuickSecurityDataSet.UserRoleAssociationRow

      If IsValid() Then

        If _CurrentSecurityUserDataRow Is Nothing Then
          _CurrentSecurityUserDataRow = _SecurityUserDataTable.NewUserRow
          _CurrentSecurityUserDataRow.User_ID = _SecurityUserTableAdapterObject.GetNewUserID.Value
        Else
          'Nothing to do here, because existing row is properties will be set outside if.
        End If

        _CurrentSecurityUserDataRow.Co_ID = CompanyComboBox.CompanyID
        _CurrentSecurityUserDataRow.User_Name = UserNameTextBox.Text
        _CurrentSecurityUserDataRow.Password = PasswordTextBox.Text
        _CurrentSecurityUserDataRow.User_Desc = UserDescTextBox.Text
        _CurrentSecurityUserDataRow.Is_Admin = IsAdminCheckBox.Checked
        'Hidden values
        _CurrentSecurityUserDataRow.Stamp_UserID = LoginInfoObject.UserID
        _CurrentSecurityUserDataRow.Stamp_DateTime = Date.Now
        _CurrentSecurityUserDataRow.Upload_DateTime = Date.Now

        If InactiveFromCalendarCombo.Value Is DBNull.Value OrElse InactiveFromCalendarCombo.Value Is Nothing Then
          _CurrentSecurityUserDataRow.SetInactive_FromNull()
        Else
          _CurrentSecurityUserDataRow.Inactive_From = Convert.ToDateTime(InactiveFromCalendarCombo.Value)
        End If

        If InactiveToCalendarCombo.Value Is DBNull.Value OrElse InactiveToCalendarCombo.Value Is Nothing Then
          _CurrentSecurityUserDataRow.SetInactive_ToNull()
        Else
          _CurrentSecurityUserDataRow.Inactive_To = Convert.ToDateTime(InactiveToCalendarCombo.Value)
        End If

        If _CurrentSecurityUserDataRow.RowState = DataRowState.Detached Then
          _SecurityUserDataTable.Rows.Add(_CurrentSecurityUserDataRow)
        End If

        _SecurityUserTableAdapterObject.Update(_SecurityUserDataTable)
        UserIDTextBox.Text = _CurrentSecurityUserDataRow.User_ID.ToString

        'Update role association table user.
        If _UserRoleAssociationTable.Rows.Count = 0 Then
          _UserRoleAssociationRow = _UserRoleAssociationTable.NewUserRoleAssociationRow
          _UserRoleAssociationRow.Co_ID = CompanyComboBox.CompanyID
          _UserRoleAssociationRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
          _UserRoleAssociationRow.User_ID = Convert.ToInt16(_CurrentSecurityUserDataRow.User_ID)
        Else
          _UserRoleAssociationRow = _UserRoleAssociationTable(0)
        End If

        _UserRoleAssociationRow.Role_ID = Convert.ToInt32(RolesComboBox1.Value)
        _UserRoleAssociationRow.Stamp_DateTime = Now
        _UserRoleAssociationRow.Stamp_UserID = LoginInfoObject.UserID

        If _UserRoleAssociationRow.RowState = DataRowState.Detached Then
          _UserRoleAssociationTable.Rows.Add(_UserRoleAssociationRow)
        End If
        _UserRoleAssociationTA.Update(_UserRoleAssociationRow)

        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
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

      _SecurityUserDataTable = Me._SecurityUserTableAdapterObject.GetFirstByCoID(CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Dim _TempTable As UserDataTable

      If (_CurrentSecurityUserDataRow Is Nothing) Then
        _TempTable = (Me._SecurityUserTableAdapterObject.GetNextByCoIDUserID(CompanyComboBox.CompanyID, 0))
      Else
        _TempTable = Me._SecurityUserTableAdapterObject.GetNextByCoIDUserID(Me._CurrentSecurityUserDataRow.Co_ID, _CurrentSecurityUserDataRow.User_ID)
        If _TempTable.Count = 0 Then
          _TempTable = Me._SecurityUserTableAdapterObject.GetLastByCoID(LoginInfoObject.CompanyID)
        End If
      End If

      _SecurityUserDataTable = _TempTable
      MyBase.MoveNextButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Dim _TempTable As UserDataTable

      If (_CurrentSecurityUserDataRow Is Nothing) Then
        _TempTable = (Me._SecurityUserTableAdapterObject.GetPreviousByCoIDUserID(CompanyComboBox.CompanyID, 0))
      Else
        _TempTable = Me._SecurityUserTableAdapterObject.GetPreviousByCoIDUserID(Me._CurrentSecurityUserDataRow.Co_ID, _CurrentSecurityUserDataRow.User_ID)
        If _TempTable.Count = 0 Then
          _TempTable = Me._SecurityUserTableAdapterObject.GetFirstByCoID(LoginInfoObject.CompanyID)
        End If
      End If

      _SecurityUserDataTable = _TempTable
      MyBase.MovePreviousButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      _SecurityUserDataTable = Me._SecurityUserTableAdapterObject.GetLastByCoID(LoginInfoObject.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully saved", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      Else
        QuickMessageBox.Show(LoginInfoObject, "Record is not successfully saved", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me._CurrentSecurityUserDataRow = Nothing
      Me._SecurityUserDataTable.Rows.Clear()
      If Not _CurrentDataTable Is Nothing Then
        Me._CurrentDataTable.Rows.Clear()
      End If
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      Me.IsAdminCheckBox.Checked = False
      MyBase.CancelButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If _SecurityUserDataTable.Rows.Count < 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      ElseIf _CurrentSecurityUserDataRow.User_ID = Me.LoginInfoObject.UserID Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You can not delete current user", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
      Else
        If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
          _SecurityUserDataTable.Rows(Me.CurrentRecordIndex).Delete()
          _SecurityUserTableAdapterObject.Update(_SecurityUserDataTable)
          MyBase.DeleteButtonClick(sender, e)

          QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)
        Else
          'User did not choose to delete the row.
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of SecurityUserForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.FormCode = "06-002"
    Me.FormVersion = "0.0.0.0"

  End Sub
End Class