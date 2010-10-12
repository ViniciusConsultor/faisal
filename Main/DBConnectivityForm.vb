Imports QuickDALLibrary
Imports QuickLibrary


'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2009
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'Faisal Saleem  16-Jan-10 _SqlConnectionObject was set to nothing at the time of 
'                         declaration and it is giving error when shown to change
'                         connection string. So now it is initialized with 
'                         declaration.
''' <summary>
''' This form is used to change connection string and store it.
''' </summary>
Public Class DBConnectivityForm
  Private _UserTAObject As New UserTableAdapter
  Private _SQLConnectionObject As SqlClient.SqlConnection = New SqlClient.SqlConnection()
  Private _ConnectionStringBuilderObject As New SqlClient.SqlConnectionStringBuilder
  Private _CreateDatabase As Boolean

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    FormCode = "03-004"
    FormVersion = "1"
  End Sub

#Region "Properties"

  Public Property CreateDatabase() As Boolean
    Get
      Return _CreateDatabase
    End Get
    Set(ByVal value As Boolean)
      _CreateDatabase = value
    End Set
  End Property


  Private _CreateObjectsOnly As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 23-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Property CreateObjectsOnly() As Boolean
    Get
      Try

        Return _CreateObjectsOnly

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in CreateObjectsOnly of DBConnectivityForm.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _CreateObjectsOnly = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in CreateObjectsOnly of DBConnectivityForm.", ex)
        Throw _qex
      End Try
    End Set
  End Property


  Public ReadOnly Property ConnectionStringBuilderObject() As SqlClient.SqlConnectionStringBuilder
    Get
      Return _ConnectionStringBuilderObject
    End Get
  End Property
#End Region

#Region "Methods"
  Friend Function ShowForConnectivity() As DialogResult
    Try
      Dim _SQLConnectionStringBuilder As New SqlClient.SqlConnectionStringBuilder
      Dim _ObjectTA As New QuickDAL.QuickSystemDataSetTableAdapters.ObjectTableAdapter
      Dim _ObjectTable As QuickDAL.QuickSystemDataSet.ObjectDataTable = Nothing

      _SQLConnectionObject = _UserTAObject.GetConnection

      Try
        _SQLConnectionObject.Open()
        _ObjectTable = _ObjectTA.GetByObjectType(Constants.OBJECTTYPE_USER_TABLE)
        If _ObjectTable.Rows.Count = 0 Then
          'If there is no table then create tables
          CreateDatabase = False
          CreateObjectsOnly = True
        Else
          'If there are tables then just connect and use.
        End If
      Catch ex As Exception
        Try
          _SQLConnectionStringBuilder.InitialCatalog = "master"
          _SQLConnectionStringBuilder.DataSource = ".\SqlExpress"
          _SQLConnectionStringBuilder.IntegratedSecurity = True

          _SQLConnectionObject.ConnectionString = _SQLConnectionStringBuilder.ConnectionString
          _SQLConnectionObject.Open()
          If MessageBox.Show("Database could not be found on the sql server, if you want to create new database choose yes.", "Database not found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
            CreateDatabase = True
          Else
            _SQLConnectionObject.Close()
          End If
        Catch exLocal As Exception
          MessageBox.Show(ex.Message)
        End Try
      Finally
        'Do Nothing
      End Try

      If _SQLConnectionObject.State = ConnectionState.Open Then
        _SQLConnectionObject.Close()
        Me.DialogResult = Windows.Forms.DialogResult.OK
      Else
        Do
          Me.ShowDialog()
        Loop While Me.DialogResult = Windows.Forms.DialogResult.Retry
      End If

      Return Me.DialogResult
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'Nothing
    End Try
  End Function

  Private Function GetConnectionString(ByVal DataBaseName As String) As String
    Try
      Return (GetConnectionString() & ";Initial Catalog=" & DataBaseName)

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Return String.Empty
    End Try
  End Function

  Private Function GetConnectionString() As String
    Try
      With _ConnectionStringBuilderObject
        .Clear()
        .DataSource = SQLServerNameComboBox.Text
        .UserID = UserNameTextBox.Text
        .Password = PasswordTextBox.Text
        .InitialCatalog = DatabaseNameComboBox.Text

        If ConnectionTypeOptionSet.CheckedIndex = 0 Then
          .UserInstance = False
          If WindowsSecurityCheckBox.Checked Then
            .IntegratedSecurity = True
          Else
            .IntegratedSecurity = False
          End If
        ElseIf ConnectionTypeOptionSet.CheckedIndex = 1 Then
          .UserInstance = True
          .IntegratedSecurity = True
          .AttachDBFilename = PrimaryFilePathTextBox.Text
        End If
      End With

      Return _ConnectionStringBuilderObject.ConnectionString
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Return String.Empty
    End Try
  End Function

  Private Sub SetDatabaseNameComboBoxColumnWidth()
    Try
      Me.DatabaseNameComboBox.DropDownWidth = Me.DatabaseNameComboBox.Width
      If Me.DatabaseNameComboBox.Rows.Band.Columns.Count > 0 Then
        Me.DatabaseNameComboBox.Rows.Band.Columns(0).Width = Me.DatabaseNameComboBox.DropDownWidth - Constants.SCROLLBAR_WIDTH
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetDatabaseNameCombBoxColumnWidth method.", ex)
      Throw _qex
    End Try

  End Sub
#End Region

#Region "Events"
  Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
    Try

      _SQLConnectionObject.ConnectionString = GetConnectionString()
      Try
        _SQLConnectionObject.Open()

      Catch ex As Exception
        Dim ExceptionObject As New QuickExceptionAdvanced("Exception attempting to connect to database", ex)
        ExceptionObject.Show(Me.LoginInfoObject)
      End Try

      If _SQLConnectionObject.State = ConnectionState.Open Then
        'This will save the connection string to custom configuration file.
        General.ConfigurationWrite(Constants.CONFIG_KEY_CONNECTION_STRING, GetConnectionString())
        'This will set the connection string which will be used by all table adapters.
        QuickDAL.SharedSetting.QuickErpConnectionString = GetConnectionString()

        Me.DialogResult = Windows.Forms.DialogResult.OK
        MessageBox.Show("Connection is successfully established with database", "Connection Established", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
      Else
        'Me.DialogResult = Windows.Forms.DialogResult.Retry
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    Finally
      If _SQLConnectionObject IsNot Nothing AndAlso _SQLConnectionObject.State = ConnectionState.Open Then _SQLConnectionObject.Close()
    End Try
  End Sub

  Private Sub DBConnectivityForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      ConnectionTypeOptionSet.CheckedIndex = 0

      QuickDAL.SharedSetting.QuickErpConnectionString = General.ConfigurationRead(Constants.CONFIG_KEY_CONNECTION_STRING)
      _ConnectionStringBuilderObject.ConnectionString = QuickDAL.SharedSetting.QuickErpConnectionString

      SQLServerNameComboBox.Text = _ConnectionStringBuilderObject.DataSource
      UserNameTextBox.Text = _ConnectionStringBuilderObject.UserID
      PasswordTextBox.Text = _ConnectionStringBuilderObject.Password
      DatabaseNameComboBox.Text = _ConnectionStringBuilderObject.InitialCatalog
      SetDatabaseNameComboBoxColumnWidth()

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  Private Sub DatabaseNameComboBox_BeforeDropDown(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DatabaseNameComboBox.BeforeDropDown
    Try
      DatabaseNameComboBox.DataSource = Database.GetDatabasesNames(GetConnectionString("master"))
      SetDatabaseNameComboBoxColumnWidth()

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  Private Sub PrimaryFilePathButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrimaryFilePathButton.Click
    OpenFileDialog.ShowDialog()
    PrimaryFilePathTextBox.Text = OpenFileDialog.FileName
  End Sub

  Private Sub CancelButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelButton1.Click
    Try
      Me.DialogResult = Windows.Forms.DialogResult.Cancel

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  Private Sub SQLServerNameComboBox_BeforeDropDown(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SQLServerNameComboBox.BeforeDropDown
    Try

      SQLServerNameComboBox.DataSource = Database.GetSQLServerInstancesAvailable()
    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in filling sql server instances", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub WindowsSecurityCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WindowsSecurityCheckBox.CheckedChanged
    Try
      UserNameTextBox.Enabled = Not WindowsSecurityCheckBox.Checked
      PasswordTextBox.Enabled = Not WindowsSecurityCheckBox.Checked

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in selecting windows security option", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub ConnectionTypeOptionSet_PropertyChanged(ByVal sender As Object, ByVal e As Infragistics.Win.PropertyChangedEventArgs) Handles ConnectionTypeOptionSet.PropertyChanged
  End Sub

  Private Sub ConnectionTypeOptionSet_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConnectionTypeOptionSet.ValueChanged
    Try
      If ConnectionTypeOptionSet.CheckedIndex = 0 Then
        WindowsSecurityCheckBox.Enabled = True
        PrimaryFilePathButton.Enabled = False
        PrimaryFilePathTextBox.Enabled = False
      ElseIf ConnectionTypeOptionSet.CheckedIndex = 1 Then
        WindowsSecurityCheckBox.Checked = True
        WindowsSecurityCheckBox.Enabled = False
        PrimaryFilePathButton.Enabled = True
        PrimaryFilePathTextBox.Enabled = True
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in selecting connection type", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

  Private Sub DatabaseNameComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles DatabaseNameComboBox.Resize
    Try
      SetDatabaseNameComboBoxColumnWidth()

    Catch ex As Exception
      Dim _QuickException As QuickExceptionAdvanced = New QuickExceptionAdvanced("Exception in DatabaseNameComboBox Resize event method.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub DatabaseNameComboBox_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles DatabaseNameComboBox.InitializeLayout

  End Sub
End Class