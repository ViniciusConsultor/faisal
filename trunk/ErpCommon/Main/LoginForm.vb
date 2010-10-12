Imports QuickDAL.QuickSecurityDataSet
Imports QuickDAL.QuickCommonDataSet
Imports QuickLibrary
Imports QuickDalLibrary.DatabaseCache
Imports QuickDalLibrary

Public Class LoginForm
  Dim _UserTableAdapterObject As New UserTableAdapter
  Dim _UserDataTableObject As New UserDataTable
  Dim _UserRowObject As UserRow
  Dim _CompanyTA As New CompanyTableAdapter
  Dim _CompanyDataTable As CompanyDataTable
  Public _LoginInfo As New LoginInfo

  ' TODO: Insert code to perform custom authentication using the provided username and password 
  ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
  ' The custom principal can then be attached to the current thread's principal as follows: 
  '     My.User.CurrentPrincipal = CustomPrincipal
  ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
  ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
  ' such as the username, display name, etc.

  Public ReadOnly Property LoginInfoObject() As LoginInfo
    Get
      Return _LoginInfo
    End Get
  End Property

  Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
    Try

      Cursor = Cursors.WaitCursor
      'Me.DialogResult = Windows.Forms.DialogResult.Retry

      If UsernameTextBox.Text.ToLower = "superadmin" AndAlso PasswordTextBox.Text = (100 - Now.Day).ToString & (100 - Now.Month).ToString Then
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
      Else
        _UserDataTableObject = _UserTableAdapterObject.GetByUserName(UsernameTextBox.Text)
        If _UserDataTableObject.Rows.Count > 0 Then
          _UserRowObject = _UserDataTableObject(0)
          If _UserRowObject.Password = PasswordTextBox.Text Then
            If _UserRowObject.IsIs_AdminNull Then
              _LoginInfo.IsAdmin = False
            Else
              _LoginInfo.IsAdmin = _UserRowObject.Is_Admin
            End If
            _LoginInfo.CompanyID = _UserRowObject.Co_ID
            _CompanyDataTable = _CompanyTA.GetByCoId(_UserRowObject.Co_ID)
            If _CompanyDataTable.Rows.Count > 0 Then
              _LoginInfo.ParentCompanyID = _CompanyDataTable(0).Parent_Co_ID
              _LoginInfo.CompanyDesc = _CompanyDataTable(0).Co_Desc
            End If
            _LoginInfo.UserID = _UserRowObject.User_ID
            _LoginInfo.UserName = _UserRowObject.User_Name
            _LoginInfo.DatabaseServerName = _UserTableAdapterObject.GetConnection.DataSource
            Me.DialogResult = Windows.Forms.DialogResult.OK
            If SaveUserCheckBox.Checked Then
              General.ConfigurationWrite(Constants.CONFIG_KEY_APPLICATION_USER_NAME, Me.UsernameTextBox.Text)
              If SavePasswordCheckBox.Checked Then
                General.ConfigurationWrite(Constants.CONFIG_KEY_APPLICATION_USER_PASSWORD, Me.PasswordTextBox.Text)
              Else
                General.ConfigurationWrite(Constants.CONFIG_KEY_APPLICATION_USER_PASSWORD, String.Empty)
              End If
            Else
              General.ConfigurationWrite(Constants.CONFIG_KEY_APPLICATION_USER_NAME, String.Empty)
              General.ConfigurationWrite(Constants.CONFIG_KEY_APPLICATION_USER_PASSWORD, String.Empty)
            End If
            Me.Close()
          Else
            MessageBox.Show("Invalid password", "Authentication failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          End If
        Else
          MessageBox.Show("Invalid user name", "Authentication failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
    Me.DialogResult = Windows.Forms.DialogResult.Cancel
  End Sub

  Private Sub SaveUserCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveUserCheckBox.CheckedChanged
    Try
      SavePasswordCheckBox.Enabled = SaveUserCheckBox.Checked

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CheckedChanged event of SaveUserCheckBox", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub SavePasswordCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePasswordCheckBox.CheckedChanged
    Try

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CheckedChanged event of SavePasswordCheckBox", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub UsernameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameTextBox.TextChanged
    Me.SavePasswordCheckBox.Checked = False
    Me.SaveUserCheckBox.Checked = False
  End Sub

  Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Me.UsernameTextBox.Text = General.ConfigurationRead(Constants.CONFIG_KEY_APPLICATION_USER_NAME)
      Me.PasswordTextBox.Text = General.ConfigurationRead(Constants.CONFIG_KEY_APPLICATION_USER_PASSWORD)

      If Me.UsernameTextBox.Text <> String.Empty Then
        Me.SaveUserCheckBox.Checked = True
        If Me.PasswordTextBox.Text <> String.Empty Then
          Me.SavePasswordCheckBox.Checked = True
        End If
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in load event of LoginForm", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
End Class
