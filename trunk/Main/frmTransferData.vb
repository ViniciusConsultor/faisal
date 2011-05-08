Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickSecurityDataSet
Imports QuickDAL.QuickSystemDataSet
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDalLibrary
Imports QuickDalLibrary.DatabaseCache

Public Class frmTransferData

#Region "Declaration"
#If CONFIG = "Debug" Then
  'Private WebServerConnectionString As String = "Data Source=faisalxp\sqlexpress;Initial Catalog=Quick_ERP_GS;User ID=sa;Password=123"
  Private WebServerConnectionString As String = "Data Source=sql401.worldispnetwork.com;Initial Catalog=khurram_uniformers;User ID=khurram_uniformers;Password=headoffice"
#ElseIf CONFIG = "Release" Then
  Private WebServerConnectionString As String = "Data Source=sql401.worldispnetwork.com;Initial Catalog=khurram_uniformers;User ID=khurram_uniformers;Password=headoffice"
#End If
  Private ThisServerConnectionString As String

  Private WithEvents _TransferData As New TransferData
  Private _OnlyCompanyAndUser As Boolean
  Private RecordTransfered As Int32 = 0
  Private StartDateTime As DateTime
  Private EndDateTime As DateTime
  Private _UserSpecificFromDate As DateTime
  Private _ToDate As DateTime

  Dim _CollectionTableName As New Collection
  Dim _CollectionDisplayName As New Collection

  'Source Server Table Adapters
  Private WithEvents _UserTableAdapterSource As New UserTableAdapter
  'Target Server Table Adapters
  Private WithEvents _TransferTableAdapterTarget As New TransferTableAdapter

  Private WithEvents _TransferDataRow As TransferRow

  Private _LastSuccessfulTransferDateTime As DateTime
  'Private _LastSuccessfulTransferDateTimeTarget As DateTime
  Private _EchoRecordDetails As Boolean = True
  Private Const EACH_TRANSFER_CONTRIBUTION As Int32 = 100
  Private _CompanyRowForDataTransfer As CompanyRow
  Private WithEvents _SendAlert As New QuickDALLibrary.QuickAlert
  Private AddNewMessageAtBeginning As Boolean = True

#End Region

#Region "Events"

  Private Sub frmTransferData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try

      Me.Quick_Button1.Visible = False

      'WebServerConnectionString = Configuration.ConfigurationManager.ConnectionStrings("WebServer").ConnectionString

      '_CollectionDisplayName.Add("Companies") : _CollectionTableName.Add("Base_Company")
      '_CollectionDisplayName.Add("Document Types") : _CollectionTableName.Add("Common_DocumentType")
      '_CollectionDisplayName.Add("Entity Types") : _CollectionTableName.Add("Common_EntityType")
      '_CollectionDisplayName.Add("Common_Status_Type") : _CollectionTableName.Add("Common_Status_Type")
      '_CollectionDisplayName.Add("Status") : _CollectionTableName.Add("Common_Status")
      '_CollectionDisplayName.Add("Users") : _CollectionTableName.Add("Sec_User")
      _CollectionDisplayName.Add("Items") : _CollectionTableName.Add("Inv_Item")
      _CollectionDisplayName.Add("Parties") : _CollectionTableName.Add("Party")
      _CollectionDisplayName.Add("COA(s)") : _CollectionTableName.Add("Accounting_COA")
      _CollectionDisplayName.Add("Voucher Types") : _CollectionTableName.Add("Accounting_VoucherType")
      _CollectionDisplayName.Add("Inventory") : _CollectionTableName.Add("Inventory")
      _CollectionDisplayName.Add("Inventory Details") : _CollectionTableName.Add("Inventory_Detail")
      _CollectionDisplayName.Add("Inventory Sales Invoice") : _CollectionTableName.Add("Inventory_SalesInvoice")
      _CollectionDisplayName.Add("Voucher") : _CollectionTableName.Add("Accounting_Voucher")
      _CollectionDisplayName.Add("Voucher Details") : _CollectionTableName.Add("Accounting_Voucher_Detail")
      _CollectionDisplayName.Add("Settings") : _CollectionTableName.Add("Base_Setting")
      _CollectionDisplayName.Add("Voucher") : _CollectionTableName.Add("Accounting_Voucher")
      _CollectionDisplayName.Add("Roles") : _CollectionTableName.Add("Sec_Role")
      _CollectionDisplayName.Add("User Roles Association") : _CollectionTableName.Add("Sec_User_Role_Association")
      '_CollectionDisplayName.Add("Transfers") : _CollectionTableName.Add("Transfer")

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in transfering sales data", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub _TransferData_ProgressChanged(ByVal _CurrentValue As Integer, ByVal _TotalValue As Integer) Handles _TransferData.ProgressChanged
    Try
      OverAllUltraProgressBar1.Value = _CurrentValue
      OverAllUltraProgressBar1.Maximum = _TotalValue
      My.Application.DoEvents()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ProgressChanged event method of frmTransferData.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Jul-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method shows the status text raised by transferdata class event.
  ''' </summary>
  Private Sub _TransferData_StatusChanged(ByVal _StatusText As String) Handles _TransferData.StatusChanged
    Try
      AddTextToProgressMessage(_StatusText, True)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in _TransferData_StatusChanged event method of frmTransferData.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

#Region "Methods"

  Private Sub _AlertSent_AlertSent(ByVal AlertNo As Integer, ByVal TotalAlerts As Integer) Handles _SendAlert.AlertSent
    Try
      Me.ProcessUltraProgressBar.Maximum = TotalAlerts
      Me.ProcessUltraProgressBar.Value = AlertNo

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in sending alert", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  

  Private Sub AddTextToProgressMessage(ByVal Text As String, ByVal AtBegining As Boolean)
    Try
      If AtBegining Then
        TransferStatusTextBox1.Text = Format(Now, Constants.FORMAT_DATETIME_FOR_USER) & " " & Text & Environment.NewLine & TransferStatusTextBox1.Text
      Else
        TransferStatusTextBox1.Text = TransferStatusTextBox1.Text & Environment.NewLine & Format(Now, Constants.FORMAT_DATETIME_FOR_USER) & " " & Text
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in AddTextToProgressMessage method", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Private Function LogTransferActivity(ByVal CompanyID As Int16, ByVal StartOfActivity As Boolean, ByVal TableName As String) As Boolean
  '  Try

  '    If StartOfActivity Then
  '      StartDateTime = Now
  '      _TransferDataRow = _TransferDataTableSource.NewTransferRow
  '      With _TransferDataRow
  '        .Co_ID = CompanyID
  '        .Transfer_StartDateTime = StartDateTime
  '        .TableName = TableName
  '        '.Source_Location = General.SourceLocation
  '        '.Download_DateTime = Convert.ToDateTime(_TransferTableAdapterTarget.GetMaximumStartDateTimeOtherLocation(CompanyID, General.SourceLocation, TableName))
  '        .Stamp_DateTime = Now
  '        .Stamp_UserID = Me.LoginInfoObject.UserID
  '        .Transfer_ID = Convert.ToInt32(_TransferTableAdapterSource.GetNextTransferIDByCoID(CompanyID))
  '      End With
  '      _TransferDataTableSource.Rows.Add(_TransferDataRow)
  '      _TransferTableAdapterSource.Update(_TransferDataTableSource)
  '    Else
  '      EndDateTime = Now
  '      With _TransferDataRow
  '        .Stamp_DateTime = Now
  '        .Transfer_EndDateTime = EndDateTime
  '        .Transfer_Status = Convert.ToInt16(DocumentStatuses.Transfer_Completed)
  '      End With
  '      _TransferTableAdapterSource.Update(_TransferDataTableSource)
  '    End If

  '  Catch ex As Exception
  '    Dim _QuickException As New QuickException("Exception in logging transfer activity", ex)
  '    Throw _QuickException
  '  End Try
  'End Function

  'Private Function ModifyOldRow(ByVal RowSource As DataRow, ByVal RowTarget As DataRow) As Boolean
  '  Dim _RowDifferent As Boolean
  '  Dim _SourceIsLatest As Boolean
  '  Try
  '    For I As Int32 = 0 To RowSource.Table.Columns.Count - 1
  '      If RowSource(I) Is DBNull.Value AndAlso RowTarget(RowSource.Table.Columns(I).ColumnName) IsNot DBNull.Value Then
  '        _RowDifferent = True
  '        Exit For
  '      ElseIf RowSource(I) IsNot DBNull.Value AndAlso RowTarget(RowSource.Table.Columns(I).ColumnName) Is DBNull.Value Then
  '        _RowDifferent = True
  '        Exit For
  '      ElseIf RowSource(I).ToString <> RowTarget(RowSource.Table.Columns(I).ColumnName).ToString Then
  '        _RowDifferent = True
  '        Exit For
  '      End If
  '    Next

  '    If _RowDifferent Then
  '      'Check which row is modified later
  '      If RowSource.RowState = DataRowState.Detached Then
  '        _SourceIsLatest = False
  '      ElseIf RowTarget.RowState = DataRowState.Detached Then
  '        _SourceIsLatest = True
  '        'Used company datatable of any row because datetime stamp column name must be same in every table.
  '      ElseIf RowSource.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName) Is DBNull.Value Then
  '        If RowTarget.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName) Is DBNull.Value Then
  '          'Skip the row can not say which one is latest.
  '          _RowDifferent = False
  '        Else
  '          'If source is null but target has value then target is latest.
  '          _SourceIsLatest = False
  '        End If
  '      Else
  '        If RowTarget.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName) Is DBNull.Value Then
  '          'If source is not null but target has null value in datetime stamp then source is latest.
  '          _SourceIsLatest = True
  '        Else
  '          If Convert.ToDateTime(RowSource.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName)) > Convert.ToDateTime(RowTarget.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName)) Then
  '            _SourceIsLatest = True
  '          Else
  '            _SourceIsLatest = False
  '          End If
  '        End If
  '      End If

  '      'Update values
  '      For I As Int32 = 0 To RowSource.Table.Columns.Count - 1
  '        If _SourceIsLatest Then
  '          RowTarget(RowSource.Table.Columns(I).ColumnName) = RowSource(RowSource.Table.Columns(I).ColumnName)
  '        Else
  '          RowSource(RowSource.Table.Columns(I).ColumnName) = RowTarget(RowSource.Table.Columns(I).ColumnName)
  '        End If
  '      Next
  '      If _SourceIsLatest Then
  '        'Added one hour so that upload delay does not prevent current downloading user to download the record.
  '        RowTarget(_CompanyDataTableSource.Upload_DateTimeColumn.ColumnName) = Now.AddHours(1)
  '      End If
  '    End If

  '    Return True
  '  Catch ex As Exception
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in comparing rows", ex)
  '    Throw ExceptionObject
  '  End Try
  'End Function

  'Private Function GetPrimayKeyCriteria(ByVal _TableNamePara As String, ByVal _DataRowPara As DataRow) As String
  '  Dim _IndexTA As New IndexTableAdapter
  '  Dim _IndexDataTable As IndexDataTable

  '  Try
  '    Dim _RowFilterString As String = String.Empty

  '    _IndexDataTable = _IndexTA.GetPrimaryKeyByTableName(_TableNamePara)
  '    For Each _IndexRow As IndexRow In _IndexDataTable
  '      If _RowFilterString <> String.Empty Then _RowFilterString &= " AND "
  '      'There is no possibility of NULL in any column because it is primary key.
  '      If _IndexRow.ColumnType.ToLower.IndexOf("int") >= 0 Then
  '        _RowFilterString &= _IndexRow.ColumnName & "=" & _DataRowPara(_IndexRow.ColumnName).ToString & ""
  '      Else
  '        _RowFilterString &= _IndexRow.ColumnName & "='" & _DataRowPara(_IndexRow.ColumnName).ToString & "'"
  '      End If
  '    Next

  '    Return _RowFilterString
  '  Catch ex As Exception
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in GetRowFilterString method.", ex)
  '    Throw ExceptionObject
  '  Finally
  '    _IndexTA = Nothing
  '    _IndexDataTable = Nothing
  '  End Try
  'End Function

  'Private Function GetSelectCommandByPrimaryKey(ByVal _TableNamePara As String, ByVal _DataRowPara As DataRow) As SqlClient.SqlCommand
  '  Try
  '    Dim _SelectCommand As New SqlClient.SqlCommand
  '    Dim _SelectQueryString As String = String.Empty

  '    _SelectQueryString = "SELECT * FROM [" & _TableNamePara & "] WHERE " & GetPrimayKeyCriteria(_TableNamePara, _DataRowPara)
  '    _SelectCommand.CommandText = _SelectQueryString

  '    Return _SelectCommand
  '  Catch ex As Exception
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in GetSelectCommandByPrimaryKey method.", ex)
  '    Throw ExceptionObject
  '  End Try
  'End Function

  'Private Function GetSelectCommandForDeltaChanges(ByVal _TableNamePara As String, ByVal _CoIDPara As Int16, ByVal _LastUploadDateTimePara As DateTime, ByVal _ToDateTimePara As DateTime, ByVal SourceDatabase As Boolean) As SqlClient.SqlCommand
  '  Try
  '    Dim _SelectCommand As New SqlClient.SqlCommand
  '    Dim _SelectQueryString As String = String.Empty

  '    If SourceDatabase Then
  '      _SelectQueryString = "SELECT * FROM [" & _TableNamePara & "] WHERE " & _CompanyDataTableSource.Co_IdColumn.ColumnName _
  '        & "=@Co_ID AND " & _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName & ">=@FromDateTime AND " _
  '        & _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName & "<=@ToDateTime"
  '      _SelectCommand.CommandText = _SelectQueryString

  '      _SelectCommand.Parameters.Add("@Co_ID", SqlDbType.SmallInt)
  '      _SelectCommand.Parameters.Add("@FromDateTime", SqlDbType.DateTime)
  '      _SelectCommand.Parameters.Add("@ToDateTime", SqlDbType.DateTime)

  '      _SelectCommand.Parameters("@Co_ID").Value = _CoIDPara
  '      _SelectCommand.Parameters.Item("@FromDateTime").Value = _LastUploadDateTimePara
  '      _SelectCommand.Parameters.Item("@ToDateTime").Value = _ToDateTimePara
  '    Else
  '      _SelectQueryString = "SELECT * FROM [" & _TableNamePara & "] WHERE " & _CompanyDataTableSource.Co_IdColumn.ColumnName _
  '        & "=@Co_ID AND " & _CompanyDataTableSource.Upload_DateTimeColumn.ColumnName & ">=@FromDateTime AND " _
  '        & _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName & "<=@ToDateTime"
  '      _SelectCommand.CommandText = _SelectQueryString

  '      _SelectCommand.Parameters.Add("@Co_ID", SqlDbType.SmallInt)
  '      _SelectCommand.Parameters.Add("@FromDateTime", SqlDbType.DateTime)
  '      _SelectCommand.Parameters.Add("@ToDateTime", SqlDbType.DateTime)

  '      _SelectCommand.Parameters("@Co_ID").Value = _CoIDPara
  '      _SelectCommand.Parameters.Item("@FromDateTime").Value = _LastUploadDateTimePara
  '      _SelectCommand.Parameters.Item("@ToDateTime").Value = _LastUploadDateTimePara
  '    End If

  '    Return _SelectCommand
  '  Catch ex As Exception
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in GetSelectCommandForDeltaChanges method.", ex)
  '    Throw ExceptionObject
  '  End Try
  'End Function

  'Private Function GetUpdateCommand(ByVal _TableNamePara As String, ByVal _SqlConnection As SqlClient.SqlConnection) As SqlClient.SqlCommand
  '  Try
  '    Dim _SqlSelectCommand As New SqlClient.SqlCommand("SELECT * FROM " & _TableNamePara, _SqlConnection)
  '    Dim _SqlUpdateCommand As SqlClient.SqlCommand
  '    Dim _SqlDataAdapter As New SqlClient.SqlDataAdapter(_SqlSelectCommand)
  '    Dim _SqlCommandBuilder As New SqlClient.SqlCommandBuilder(_SqlDataAdapter)

  '    _SqlCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges
  '    _SqlUpdateCommand = _SqlCommandBuilder.GetUpdateCommand

  '    Return _SqlUpdateCommand
  '  Catch ex As Exception
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in GetUpdateCommand method.", ex)
  '    Throw ExceptionObject
  '  End Try
  'End Function

  'Private Function GetInsertCommand(ByVal _TableNamePara As String, ByVal _SqlConnection As SqlClient.SqlConnection) As SqlClient.SqlCommand
  '  Try
  '    Dim _SqlSelectCommand As New SqlClient.SqlCommand("SELECT * FROM " & _TableNamePara, _SqlConnection)
  '    Dim _SqlInsertCommand As SqlClient.SqlCommand
  '    Dim _SqlDataAdapter As New SqlClient.SqlDataAdapter(_SqlSelectCommand)
  '    Dim _SqlCommandBuilder As New SqlClient.SqlCommandBuilder(_SqlDataAdapter)

  '    _SqlCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges
  '    _SqlInsertCommand = _SqlCommandBuilder.GetInsertCommand

  '    Return _SqlInsertCommand
  '  Catch ex As Exception
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in GetInsertCommand method.", ex)
  '    Throw ExceptionObject
  '  End Try
  'End Function

#End Region

#Region "XML FTP Methods"
  
  'Private Function FetchData(ByVal _TableNamepara As String, ByVal _TableDisplayNamePara As String, ByVal CompanyID As Int16, ByVal _SourceConnectionStringPara As String, ByRef _DataSet As DataSet) As Boolean
  '  Dim _SqlConnectionSource As New SqlClient.SqlConnection
  '  Dim _SqlDataAdapterSource As New SqlClient.SqlDataAdapter
  '  Dim _SqlCommandSource As New SqlClient.SqlCommand
  '  Dim _DataTableSource As New DataTable
  '  Dim _RowSource As DataRow
  '  Dim tempDataTable As New DataTable

  '  Try
  '    Dim _OverallProgressValue As Int32 = OverAllUltraProgressBar1.Value

  '    _SqlConnectionSource.ConnectionString = _SourceConnectionStringPara
  '    _SqlCommandSource.Connection = _SqlConnectionSource
  '    _SqlDataAdapterSource.SelectCommand = _SqlCommandSource
  '    _SqlDataAdapterSource.UpdateCommand = GetUpdateCommand(_TableNamepara, _SqlConnectionSource)
  '    _SqlDataAdapterSource.InsertCommand = GetInsertCommand(_TableNamepara, _SqlConnectionSource)

  '    If _UserSpecificFromDate = Date.MinValue Then   'If equal to min value means user didn't provide value.
  '      _LastSuccessfulTransferDateTime = Convert.ToDateTime(_TransferTableAdapterSource.GetMaximumStartDateTimeByTableName(_TableNamepara))
  '    Else
  '      _LastSuccessfulTransferDateTime = _UserSpecificFromDate
  '    End If

  '    LogTransferActivity(CompanyID, True, _TableNamepara)

  '    _CompanyDataTableSource = New CompanyDataTable

  '    If _SqlConnectionSource.State = ConnectionState.Closed Then _SqlConnectionSource.Open()
  '    _SqlCommandSource = GetSelectCommandForDeltaChanges(_TableNamepara, CompanyID, _LastSuccessfulTransferDateTime, _ToDate, True)
  '    _SqlCommandSource.Connection = _SqlConnectionSource
  '    _SqlDataAdapterSource.SelectCommand = _SqlCommandSource

  '    _SqlDataAdapterSource.Fill(_DataTableSource)
  '    _DataTableSource.TableName = _TableNamepara
  '    If _DataSet.Tables.Contains(_TableNamepara) Then _DataSet.Tables.Remove(_TableNamepara)
  '    _DataSet.Tables.Add(_DataTableSource)

  '    'AddTextToProgressMessage("Fetched " & _TableDisplayNamePara & " records from " & _LastSuccessfulTransferDateTime, AddNewMessageAtBeginning)

  '    Return True

  '  Catch ex As Exception
  '    FetchData = False
  '    Dim ExceptionObject As New QuickExceptionAdvanced("Exception in transfering " & _TableDisplayNamePara & " data", ex)
  '    Throw ExceptionObject
  '  Finally
  '    _SqlConnectionSource = Nothing
  '    _SqlDataAdapterSource = Nothing
  '    _SqlCommandSource = Nothing
  '    _DataTableSource = Nothing
  '    _RowSource = Nothing
  '    tempDataTable = Nothing
  '  End Try
  'End Function

  Private Sub StartTransferXmlButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartTransferXmlButton.Click
    Dim _Dataset As New DataSet
    Dim _FileName As String = String.Empty
    Dim _QuickErpWebServiceObject As New QuickERP.QuickErpWebService.Service
    Dim _AllowedCompaniesAndTablesTA As New QuickSecurityDataSetTableAdapters.LocationCompanyTableAssociationTableAdapter
    Dim _AllowedCompaniesAndTablesTable As QuickSecurityDataSet.LocationCompanyTableAssociationDataTable
    Dim _AllowedCompaniesAndTablesGeneralTable As DataTable
    Dim _DatabaseTA As New QuickCommonDataSetTableAdapters.DatabaseTableAdapter
    Dim _DatabaseGuid As System.Nullable(Of Guid)

    '_TransferData.CompressFile("c:\10.xls", "c:\10.zip")
    '_TransferData.DecompressFile("c:\10.zip", "c:\11.xls")
    'Return

    Try
      Dim _CompanyForDataTransfer As New CompanyDataTable
      Dim _WebServiceCompleted As Boolean

      Cursor = Cursors.WaitCursor
      Me.TransferStatusTextBox1.Text = String.Empty
      Me.StartTransferXmlButton.Enabled = False

      _DatabaseGuid = _DatabaseTA.GetDatabaseGuidByDatabaseName(Me.LoginInfoObject.DatabaseName)
      If _DatabaseGuid.HasValue Then
        _AllowedCompaniesAndTablesGeneralTable = _QuickErpWebServiceObject.GetAllowedTables(_DatabaseGuid.Value.ToString)
        If _AllowedCompaniesAndTablesGeneralTable IsNot Nothing AndAlso _AllowedCompaniesAndTablesGeneralTable.Rows.Count > 0 Then

          Do  'Keep on uploading while there are records to upload
            _FileName = _TransferData.ExportDataToXmlFile(Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.UserID, False, _UserTableAdapterSource.GetConnection.ConnectionString, TransferData.LOCAL_PATH_FOR_FTP_FILES, _AllowedCompaniesAndTablesGeneralTable)

            If _FileName <> TransferData.NO_RECORDS_FILE_NAME Then
              _TransferData.UploadFile(_FileName)

              'Try
              _QuickErpWebServiceObject.Timeout = 9900000
              '_QuickErpWebServiceObject.Url = "http://localhost:1367/QuickWebService/QuickErp.asmx"
              _WebServiceCompleted = _QuickErpWebServiceObject.ImportXmlFileToDatabase(Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.UserID, _FileName, False, WebServerConnectionString)
              If _WebServiceCompleted Then
                _TransferData.SetUploadedIndicatorFromXmlFileData(_UserTableAdapterSource.GetConnection.ConnectionString, TransferData.LOCAL_PATH_FOR_FTP_FILES & _FileName.Substring(0, _FileName.LastIndexOf("."c)) & ".qerp")
              End If

              '_TransferData.DownloadFile(_FileName)
              '_TransferData.TransferTableFromXML(Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.UserID, _UserTableAdapterSource.GetConnection.ConnectionString, TransferData.LOCAL_PATH_FOR_FTP_FILES & _FileName)
              'Catch ex As Exception
              '  Dim qex As New QuickExceptionAdvanced("Webservice", ex)
              '  qex.Show(Me.LoginInfoObject)
              'End Try
              My.Computer.FileSystem.DeleteFile(TransferData.LOCAL_PATH_FOR_FTP_FILES & _FileName)
              My.Computer.FileSystem.DeleteFile(TransferData.LOCAL_PATH_FOR_FTP_FILES & _FileName.Substring(0, _FileName.LastIndexOf("."c)) & ".qerp")
            End If 'Check of no records to upload
          Loop While _FileName <> TransferData.NO_RECORDS_FILE_NAME

          'QuickAlert.SendEmailAlerts()
          If ShutdownCheckBox.Checked Then
            Shell("shutdown -s -f")
          Else
            QuickMessageBox.Show(Me.LoginInfoObject, "Records successfully transfered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
          End If
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "You are not allowed to transfer data.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
        End If
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "Your database id cannot be found", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in transfering data", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
      Try
        QuickAlert.SendAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Records Transfered Exception", TransferStatusTextBox1.Text)
      Catch exInner As Exception
        'Do nothing if email is failed
      End Try
    Finally
      Cursor = Cursors.Default
      StartTransferXmlButton.Enabled = True
    End Try
  End Sub

  Private Sub ImportFromXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportFromXML.Click
    Try

      Me.ImportFromXML.Enabled = False

      OpenFileDialog1.Filter = "Erp Files|*.qerp"
      OpenFileDialog1.ShowDialog()
      If OpenFileDialog1.FileName <> String.Empty Then
        _TransferData.TransferTableFromXML(Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.UserID, _UserTableAdapterSource.GetConnection.ConnectionString, OpenFileDialog1.FileName)
      End If

      QuickDALLibrary.QuickMessageBox.Show(Me.LoginInfoObject, "Completed successfully.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Import From XML", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Me.ImportFromXML.Enabled = True
    End Try

  End Sub
#End Region

#Region "Properties"

  Public Property TransferOnlyCompanyAndUser() As Boolean
    Get
      Return _OnlyCompanyAndUser
    End Get
    Set(ByVal value As Boolean)
      _OnlyCompanyAndUser = value
    End Set
  End Property

#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

  'Private Function UploadFile(ByVal _FileName As String) As Boolean
  '  Dim _UploadFileInfo As New System.IO.FileInfo(_FileName)
  '  Dim _UploadFtpWebRequest As Net.FtpWebRequest
  '  Dim _UploadFileStream As System.IO.FileStream = Nothing
  '  Dim _FtpWebRequestStream As System.IO.Stream = Nothing

  '  Try
  '    Const BUFFER_SIZE As Int16 = 2048
  '    Dim bytContent(BUFFER_SIZE - 1) As Byte
  '    Dim intDataRead As Integer = 0
  '    Dim strURI As String = String.Empty

  '    Cursor = Cursors.WaitCursor
  '    strURI = "ftp://nixon.worldispnetwork.com/"  'wwwroot/QuickErpWebService/FtpFiles/
  '    strURI = strURI & _UploadFileInfo.Name

  '    'perform copy
  '    _UploadFtpWebRequest = CType(Net.FtpWebRequest.Create(strURI), Net.FtpWebRequest)
  '    _UploadFtpWebRequest.Credentials = New Net.NetworkCredential("quickerpftp", "faisal")
  '    _UploadFtpWebRequest.KeepAlive = False
  '    _UploadFtpWebRequest.UsePassive = False

  '    'Set request to upload a file in binary
  '    _UploadFtpWebRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
  '    _UploadFtpWebRequest.UseBinary = True

  '    'Notify FTP of the expected size
  '    _UploadFtpWebRequest.ContentLength = _UploadFileInfo.Length
  '    Me.OverAllUltraProgressBar1.Maximum = Convert.ToInt32(_UploadFileInfo.Length)
  '    Me.OverAllUltraProgressBar1.Value = 0

  '    'open file for reading 
  '    _UploadFileStream = _UploadFileInfo.OpenRead
  '    'open request to send
  '    _FtpWebRequestStream = _UploadFtpWebRequest.GetRequestStream

  '    Do
  '      intDataRead = _UploadFileStream.Read(bytContent, 0, BUFFER_SIZE)
  '      _FtpWebRequestStream.Write(bytContent, 0, intDataRead)
  '      If (Me.OverAllUltraProgressBar1.Value + BUFFER_SIZE) < Me.OverAllUltraProgressBar1.Maximum Then
  '        Me.OverAllUltraProgressBar1.Value += BUFFER_SIZE
  '      End If
  '      Application.DoEvents()

  '    Loop Until intDataRead < BUFFER_SIZE

  '    Me.OverAllUltraProgressBar1.Value = Me.OverAllUltraProgressBar1.Maximum

  '    Return True

  '  Catch ex As Exception
  '    Dim _qex As New QuickExceptionAdvanced("Exception in uploading file.", ex)
  '    Throw _qex
  '  Finally
  '    Cursor = Cursors.Default

  '    _UploadFileStream.Close()
  '    _FtpWebRequestStream.Close()
  '    _UploadFileStream = Nothing
  '    _UploadFileInfo = Nothing
  '    _UploadFtpWebRequest = Nothing
  '    _FtpWebRequestStream = Nothing
  '  End Try
  'End Function

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub

  Private Sub Quick_Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Quick_Button1.Click
    OpenFileDialog1.ShowDialog()
    If OpenFileDialog1.FileName <> String.Empty Then
      Dim _SourceFileName As String = OpenFileDialog1.FileName
      Dim _SourceFileStream As IO.FileStream = IO.File.OpenRead(_SourceFileName)
      Dim _TargetFileStream As IO.FileStream = IO.File.Create(_SourceFileName & ".rfs")
      Dim _gZip As New IO.Compression.GZipStream(_TargetFileStream, IO.Compression.CompressionMode.Compress, True)

      Dim bytes As Int32 = _SourceFileStream.ReadByte

      Do While bytes <> -1
        _gZip.WriteByte(Convert.ToByte(bytes))
        bytes = _SourceFileStream.ReadByte
      Loop

      _gZip.Close()
      _TargetFileStream.Close()
      _SourceFileStream.Close()

      MessageBox.Show("Completed")
    End If

  End Sub
End Class
