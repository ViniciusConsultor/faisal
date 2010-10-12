Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickSystemDataSetTableAdapters

Imports QuickDAL.LogicalDataSet
Imports QuickDAL.QuickSystemDataSet
Imports QuickDAL.QuickCommonDataSet
Imports System.Data

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 27-Mar-2010
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This class exports data from database to xml file and also imports data from
''' xml file to database.
''' 
''' </summary>
Public Class TransferData

#Region "Declarations"
  Private WithEvents _TransferTableAdapterTarget As New TransferTableAdapter
  Private WithEvents _TransferTableAdapterSource As New TransferTableAdapter
  Private WithEvents _CompanyTableAdapterSource As New CompanyTableAdapter

  Private WithEvents _TransferDataTableSource As New TransferDataTable
  Private WithEvents _CompanyDataTableSource As CompanyDataTable

  Private WithEvents _TransferDataRow As TransferRow

  Private _LastSuccessfulTransferDateTime As DateTime
  Dim _CollectionTableName As New Collection
  Dim _CollectionDisplayName As New Collection
  Private _TotalProgress As Int32
  Private _CurrentProgress As Int32
  Public PathForLogFile As String = String.Empty

  Public Event ProgressChanged(ByVal _CurrentValue As Int32, ByVal _TotalValue As Int32)
  Public Event StatusChanged(ByVal _StatusText As String)

  Public Const LOCAL_PATH_FOR_FTP_FILES As String = "C:\"
#If CONFIG = "Release" Or CONFIG = "Debug" Then
  Public Const FTP_URI As String = "ftp://nixon.worldispnetwork.com/"
  Public Const FTP_USER As String = "quickerpftp"
  Public Const FTP_PASSWORD As String = "faisal"
  '#ElseIf CONFIG = "Debug" Then
  '  Public Const FTP_URI As String = "ftp://faisal2k3/ftpfiles/" '"ftp://quicktijarat.com/wwwroot/QuickErpWebService/FtpFiles/"
  '  Public Const FTP_USER As String = "quickerpftp"     ' "quicktijarat.com"
  '  Public Const FTP_PASSWORD As String = "123"     ' "324725725336"
#End If

#End Region

#Region "Properties"

#End Region

#Region "Methods"


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-Jul-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will first check if path for file is provided then will add text.
  ''' </summary>
  Private Sub AppendTextToLogFile(ByVal TextToAppend As String)
    Try
      If PathForLogFile IsNot Nothing AndAlso PathForLogFile.Length > 0 Then
        My.Computer.FileSystem.WriteAllText(PathForLogFile, Environment.NewLine & Now.ToString & ": " & TextToAppend, True)
      End If

    Catch ex As Exception
      Dim _qex As New Exception("Exception in AppendTextToLogFile of TransferData.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 30-Mar-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It exports data to xml file and returns the file name with path.
  ''' </summary>
  Public Function ExportDataToXmlFile(ByVal _CompanyID As Int16, ByVal _UserID As Int32, ByVal _FromDate As DateTime, ByVal _ToDate As DateTime, ByVal TransferOnlyCompanyAndUser As Boolean, ByVal _ConnectionString As String, ByVal FilePath As String) As String
    Try
      Dim _FetchDataSucceeded As Boolean
      Dim _Dataset As New DataSet
      Dim _FileName As String = _CompanyID & "_" & Format(_FromDate, "yyMMdd") & "to" & Format(Now, "yyMMdd") & ".qerp"
      Dim _FetchDataTry As Int32 = 0

      Try
        Dim _CompanyForDataTransfer As New CompanyDataTable

        AppendTextToLogFile("Fetching current company and its child companies")
        _CurrentProgress = 0
        _CompanyForDataTransfer = _CompanyTableAdapterSource.GetParentAndChildsByCoID(_CompanyID)

        For Each _CompanyRowForDataTransfer As CompanyRow In _CompanyForDataTransfer
          If Not TransferOnlyCompanyAndUser Then
            _TotalProgress = _CollectionTableName.Count * 100
          Else
            _TotalProgress = 200
          End If
          RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)
          'Download Companies
          'Download users
          _Dataset = New DataSet

          If Not TransferOnlyCompanyAndUser Then

            _FetchDataTry = 1
            For I As Int32 = 1 To _CollectionTableName.Count
              _FetchDataSucceeded = False
              Do While Not _FetchDataSucceeded
                Try
                  RaiseEvent StatusChanged("Started " & _CollectionDisplayName(I).ToString & " ... ")
                  AppendTextToLogFile("Starting processing table " & _CollectionTableName(I).ToString)
                  _FetchDataSucceeded = FetchData(_CollectionTableName(I).ToString, _CollectionDisplayName(I).ToString, _CompanyRowForDataTransfer.Co_Id, _UserID, _FromDate, _ToDate, _ConnectionString, _Dataset)
                  AppendTextToLogFile("Finished processing table " & _CollectionTableName(I).ToString)
                  RaiseEvent StatusChanged("Completed.")

                Catch ex As Exception
                  AppendTextToLogFile("Exception in ExportDataToXmlFile() method, delta change fetch try #" & _FetchDataTry.ToString & ". Exception text=" & ex.Message)
                  If _FetchDataTry <= 3 Then
                    RaiseEvent StatusChanged("Retrying to upload table data again")
                    _FetchDataTry += 1
                  Else
                    Throw ex
                  End If
                End Try
              Loop
            Next
          End If

          AppendTextToLogFile("Finished Company = " & _CompanyRowForDataTransfer.Co_Code & "-" & _CompanyRowForDataTransfer.Co_Desc)
          RaiseEvent StatusChanged("Finished Company = " & _CompanyRowForDataTransfer.Co_Code & "-" & _CompanyRowForDataTransfer.Co_Desc)

          _CurrentProgress = _TotalProgress
          RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)
        Next

        AppendTextToLogFile("Writing Xml File")
        _Dataset.WriteXml(FilePath & _FileName)
        AppendTextToLogFile("Finished Writing Xml File")
        Return _FileName

      Catch ex As Exception
        AppendTextToLogFile("Exception in ExportDataToXmlFile() method. Exception text=" & ex.Message)
        Throw ex
      Finally
        _Dataset = Nothing
      End Try
    Catch ex As Exception
      AppendTextToLogFile("Exception in ExportDataToXmlFile() method. Exception text=" & ex.Message)
      Throw ex
    End Try
  End Function

  Private Function FetchData(ByVal _TableNamepara As String, ByVal _TableDisplayNamePara As String, ByVal CompanyID As Int16, ByVal UserID As Int32, ByVal _FromDate As DateTime, ByVal _ToDate As DateTime, ByVal _SourceConnectionStringPara As String, ByRef _DataSet As DataSet) As Boolean
    Dim _SqlConnectionSource As New SqlClient.SqlConnection
    Dim _SqlDataAdapterSource As New SqlClient.SqlDataAdapter
    Dim _SqlCommandSource As New SqlClient.SqlCommand
    Dim _DataTableSource As New DataTable
    Dim _RowSource As DataRow
    Dim tempDataTable As New DataTable

    Try
      _SqlConnectionSource.ConnectionString = _SourceConnectionStringPara
      _SqlCommandSource.Connection = _SqlConnectionSource
      _SqlDataAdapterSource.SelectCommand = _SqlCommandSource
      _SqlDataAdapterSource.UpdateCommand = GetUpdateCommand(_TableNamepara, _SqlConnectionSource)
      _SqlDataAdapterSource.InsertCommand = GetInsertCommand(_TableNamepara, _SqlConnectionSource)

      If _FromDate = Date.MinValue Then   'If equal to min value means user didn't provide value.
        _LastSuccessfulTransferDateTime = Convert.ToDateTime(_TransferTableAdapterSource.GetMaximumStartDateTimeByTableName(_TableNamepara))
      Else
        _LastSuccessfulTransferDateTime = _FromDate
      End If

      LogTransferActivity(CompanyID, UserID, True, _TableNamepara)
      _CompanyDataTableSource = New CompanyDataTable
      If _DataSet.Tables.Contains(_TableNamepara) Then _DataTableSource = _DataSet.Tables(_TableNamepara)

      If _SqlConnectionSource.State = ConnectionState.Closed Then _SqlConnectionSource.Open()
      _SqlCommandSource = GetSelectCommandForDeltaChanges(_TableNamepara, CompanyID, _LastSuccessfulTransferDateTime, _ToDate, True)
      _SqlCommandSource.Connection = _SqlConnectionSource
      _SqlDataAdapterSource.SelectCommand = _SqlCommandSource

      _SqlDataAdapterSource.Fill(_DataTableSource)
      If Not _DataSet.Tables.Contains(_TableNamepara) Then
        _DataTableSource.TableName = _TableNamepara
        _DataSet.Tables.Add(_DataTableSource)
      End If

      Return True

    Catch ex As Exception
      FetchData = False
      Throw ex
    Finally
      _SqlConnectionSource = Nothing
      _SqlDataAdapterSource = Nothing
      _SqlCommandSource = Nothing
      _DataTableSource = Nothing
      _RowSource = Nothing
      tempDataTable = Nothing
    End Try
  End Function

  Public Function UploadFile(ByVal _FileName As String) As Boolean
    Dim _LocalFileInfo As New System.IO.FileInfo(LOCAL_PATH_FOR_FTP_FILES & _FileName)
    Dim _UploadFtpWebRequest As Net.FtpWebRequest
    Dim _LocalFileStream As System.IO.FileStream = Nothing
    Dim _FtpWebRequestStream As System.IO.Stream = Nothing

    Try
      Const BUFFER_SIZE As Int16 = 2048
      Dim bytContent(BUFFER_SIZE - 1) As Byte
      Dim intDataRead As Integer = 0
      Dim strURI As String = String.Empty

      strURI = FTP_URI & _LocalFileInfo.Name

      'perform copy
      _UploadFtpWebRequest = CType(Net.FtpWebRequest.Create(strURI), Net.FtpWebRequest)
      _UploadFtpWebRequest.Credentials = New Net.NetworkCredential(FTP_USER, FTP_PASSWORD)
      _UploadFtpWebRequest.KeepAlive = False
            _UploadFtpWebRequest.UsePassive = True

      'Set request to upload a file in binary
      _UploadFtpWebRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
      _UploadFtpWebRequest.UseBinary = True

      'Notify FTP of the expected size
      _UploadFtpWebRequest.ContentLength = _LocalFileInfo.Length
      _TotalProgress = Convert.ToInt32(_LocalFileInfo.Length)
      _CurrentProgress = 0
      RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)

      'open file for reading 
      _LocalFileStream = _LocalFileInfo.OpenRead
      'open request to send
      _FtpWebRequestStream = _UploadFtpWebRequest.GetRequestStream

      Do
        intDataRead = _LocalFileStream.Read(bytContent, 0, BUFFER_SIZE)
        _FtpWebRequestStream.Write(bytContent, 0, intDataRead)
        If (_CurrentProgress + BUFFER_SIZE) < _TotalProgress Then
          _CurrentProgress += BUFFER_SIZE
        End If

        RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)
      Loop Until intDataRead < BUFFER_SIZE

      _CurrentProgress = _TotalProgress
      RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)

      Return True

    Catch ex As Exception
      Throw ex
    Finally
      Try
        _LocalFileStream.Close()
        _FtpWebRequestStream.Close()
        _LocalFileStream = Nothing
        _LocalFileInfo = Nothing
        _UploadFtpWebRequest = Nothing
        _FtpWebRequestStream = Nothing
      Catch ex As Exception

      End Try
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 5-Apr-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will download the file from ftp server. File name passed will be downloaded from ftp.
  ''' File name should not include the path.
  ''' </summary>
  Public Function DownloadFile(ByVal _FileName As String) As Boolean
    Dim _LocalFileInfo As New System.IO.FileInfo(LOCAL_PATH_FOR_FTP_FILES & _FileName)
    Dim _FtpWebRequest As Net.FtpWebRequest
    Dim _LocalFileStream As System.IO.FileStream = Nothing
    Dim _FtpWebIOStream As System.IO.Stream = Nothing
    Dim _FtpWebResponse As Net.FtpWebResponse

    Try
      Const BUFFER_SIZE As Int16 = 2048
      Dim bytBuffer(BUFFER_SIZE - 1) As Byte
      Dim intDataRead As Integer = 0
      Dim strURI As String = String.Empty

      strURI = FTP_URI & _LocalFileInfo.Name

      'perform copy
      _FtpWebRequest = CType(Net.FtpWebRequest.Create(strURI), Net.FtpWebRequest)
      _FtpWebRequest.Credentials = New Net.NetworkCredential(FTP_USER, FTP_PASSWORD)
      _FtpWebRequest.KeepAlive = False
      _FtpWebRequest.UsePassive = False
      _FtpWebRequest.UseBinary = True  'Set request to download a file in binary
      _FtpWebRequest.Method = Net.WebRequestMethods.Ftp.DownloadFile

      _FtpWebResponse = CType(_FtpWebRequest.GetResponse, System.Net.FtpWebResponse)
      'open request to send
      _FtpWebIOStream = _FtpWebResponse.GetResponseStream

      If _LocalFileInfo.Exists Then _LocalFileInfo.Delete()
      'open file for writing
      _LocalFileStream = _LocalFileInfo.OpenWrite

      'Notify FTP of the expected size
      '_FtpWebRequest.ContentLength = _LocalFileInfo.Length
      '_TotalProgress = Convert.ToInt32(_LocalFileInfo.Length)
      _CurrentProgress = 0
      RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)


      Do
        intDataRead = _FtpWebIOStream.Read(bytBuffer, 0, bytBuffer.Length)
        _LocalFileStream.Write(bytBuffer, 0, intDataRead)
        '_FtpWebRequestStream.Write(bytContent, 0, intDataRead)
        If (_CurrentProgress + BUFFER_SIZE) < _TotalProgress Then
          _CurrentProgress += BUFFER_SIZE
        End If

        RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)
      Loop Until intDataRead = 0

      _CurrentProgress = _TotalProgress
      RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)

      Return True

    Catch ex As Exception
      Throw ex
    Finally
      _LocalFileStream.Close()
      _FtpWebIOStream.Close()
      _LocalFileStream = Nothing
      _LocalFileInfo = Nothing
      _FtpWebRequest = Nothing
      _FtpWebIOStream = Nothing
    End Try
  End Function

  Public Function TransferTableFromXML(ByVal _CompanyID As Int16, ByVal _UserID As Int32, ByVal _TargetConnectionStringPara As String, ByVal _FileNameWithPath As String, ByVal _FromDate As DateTime, ByVal _ToDate As DateTime) As Boolean
    Dim _SqlConnectionTarget As New SqlClient.SqlConnection(_TargetConnectionStringPara)
    Dim _SqlDataAdapterTarget As New SqlClient.SqlDataAdapter
    Dim _SqlCommandTarget As New SqlClient.SqlCommand
    Dim _DataSetFromXML As New DataSet
    Dim _DataTableSource As DataTable
    Dim _DataTableTarget As New DataTable
    Dim _RowSource As DataRow
    Dim _RowTarget As DataRow
    Dim tempDataTable As New DataTable
    Dim _TableNamepara As String
    Dim _TableDisplayNamePara As String
    Dim _ProgressValue As Int32, _ProgressTotal As Int32
    Dim _FetchDataTry As Int32

    Try
      Dim _TransferSucceeded As Boolean

      AppendTextToLogFile("TransferTableFromXML(" & _CompanyID.ToString & "," & _UserID.ToString & "," & _TargetConnectionStringPara & "," & _FileNameWithPath & "," & _FromDate.ToString & " method started")

      _DataSetFromXML.ReadXml(_FileNameWithPath)
      _ProgressValue = 0 : _ProgressTotal = _CollectionTableName.Count
      RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)

      AppendTextToLogFile("XML is read into memory")

      For J As Int32 = 1 To _CollectionTableName.Count
        _TableNamepara = _CollectionTableName(J).ToString
        _TableDisplayNamePara = _CollectionDisplayName(J).ToString
        AppendTextToLogFile("Starting processing table " & _TableNamepara)
        If _FromDate = Date.MinValue Then   'If equal to min value means user didn't provide value.
          _LastSuccessfulTransferDateTime = Convert.ToDateTime(_TransferTableAdapterSource.GetMaximumStartDateTimeByTableName(_TableNamepara))
        Else
          _LastSuccessfulTransferDateTime = _FromDate
        End If

        If _DataSetFromXML.Tables.Contains(_TableNamepara) Then
          _DataTableSource = _DataSetFromXML.Tables(_TableNamepara)
          _DataTableTarget = Nothing : _DataTableTarget = New DataTable

          LogTransferActivity(_CompanyID, _UserID, True, _TableNamepara)

          _CompanyDataTableSource = New CompanyDataTable
          _SqlDataAdapterTarget.UpdateCommand = GetUpdateCommand(_TableNamepara, _SqlConnectionTarget)
          _SqlDataAdapterTarget.InsertCommand = GetInsertCommand(_TableNamepara, _SqlConnectionTarget)
          AppendTextToLogFile("Insert/Update queries created")
          Do
            Try
              _TransferSucceeded = False

              If _SqlConnectionTarget.State = ConnectionState.Closed Then _SqlConnectionTarget.Open()
              AppendTextToLogFile("Connection is opened")
              _SqlCommandTarget = GetSelectCommandForDeltaChanges(_TableNamepara, _CompanyID, _LastSuccessfulTransferDateTime, _ToDate, False)
              _SqlCommandTarget.Connection = _SqlConnectionTarget
              _SqlDataAdapterTarget.SelectCommand = _SqlCommandTarget
              AppendTextToLogFile("Fetching delta changes from database")
              _SqlDataAdapterTarget.Fill(_DataTableTarget)
              AppendTextToLogFile("Delta changes fetched from database")

              _TransferSucceeded = True
            Catch ex As SqlClient.SqlException
              AppendTextToLogFile("Exception in TransferTableFromXml() method, delta change fetch try #" & _FetchDataTry.ToString & ". Exception text=" & ex.Message)
              If _FetchDataTry <= 3 Then

                'Dim _exAdvanced As New QuickException("Retrying to download data", ex)
                '_exAdvanced.Save(Me.LoginInfoObject)
                '<<<<<<<<<< There will be too many emails saved if any internet connection error occurs so not saving exceptions >>>>>>>>>>

                'MessageBox.Show(ex.Number & ex.Message)
                _FetchDataTry += 1
              Else
                Throw ex
              End If
            End Try
          Loop While Not _TransferSucceeded

          _ProgressValue = 0 : _ProgressTotal = _DataTableSource.Rows.Count
          RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)

          AppendTextToLogFile("Starting row comparison")
          For I As Int32 = 0 To _DataTableSource.Rows.Count - 1
            _RowSource = _DataTableSource.Rows(I)
            _RowTarget = Nothing

            _DataTableTarget.DefaultView.RowFilter = GetPrimayKeyCriteria(_TableNamepara, _RowSource)

            If _DataTableTarget.DefaultView.Count > 0 Then
              _RowTarget = _DataTableTarget.DefaultView.Item(0).Row
            ElseIf _DataTableTarget.DefaultView.Count <= 0 Then
              _SqlCommandTarget = GetSelectCommandByPrimaryKey(_TableNamepara, _RowSource)
              _SqlCommandTarget.Connection = _SqlConnectionTarget
              _SqlDataAdapterTarget.SelectCommand = _SqlCommandTarget
              tempDataTable.Clear()
              _SqlDataAdapterTarget.Fill(tempDataTable)
              If tempDataTable.Rows.Count > 0 Then
                _DataTableTarget.ImportRow(tempDataTable.Rows(0))
                _DataTableTarget.DefaultView.RowFilter = GetPrimayKeyCriteria(_TableNamepara, _RowSource)
                If _DataTableTarget.DefaultView.Count > 0 Then
                  _RowTarget = _DataTableTarget.DefaultView.Item(0).Row
                End If
              End If
            End If

            'Row not found in target db.
            If _RowTarget Is Nothing Then
              _RowTarget = _DataTableTarget.NewRow
            End If

            ModifyOldRow(_RowSource, _RowTarget)

            If _RowTarget.RowState = DataRowState.Detached Then
              _DataTableTarget.Rows.Add(_RowTarget)
            End If

            _ProgressValue += 1
            RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)
          Next

          _DataTableSource.DefaultView.RowFilter = ""
          _DataTableTarget.DefaultView.RowFilter = ""
          _ProgressTotal += _DataTableTarget.Rows.Count
          RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)
          'ProcessUltraProgressBar.Maximum += _DataTableTarget.Rows.Count

          For I As Int32 = 0 To _DataTableTarget.Rows.Count - 1
            If _DataTableTarget.Rows(I).RowState = DataRowState.Unchanged Then
              'Skip record, it is not changed.
            Else
              Dim _DataRows() As DataRow = {_DataTableTarget.Rows(I)}
              _SqlDataAdapterTarget.Update(_DataRows)
            End If

            _ProgressValue += 1
            RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)
          Next

          LogTransferActivity(_CompanyID, _UserID, False, _DataTableSource.TableName)
          'Below line is necessary, it is mandatory when there was no data to transfer.
        End If
        'Me.OverAllUltraProgressBar1.Value += 1
        '_ProgressValue += 1
        'RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)
        AppendTextToLogFile("Finished processing table " & _TableNamepara)
      Next

      TransferTableFromXML = True

    Catch ex As Exception
      TransferTableFromXML = False
      AppendTextToLogFile("Exception in TransferTableFromXml() method. Exception text=" & ex.Message)
      Throw New Exception("Exception in TransferTableFromXML method of TransferData.", ex)
    Finally
      _SqlConnectionTarget = Nothing
      _SqlDataAdapterTarget = Nothing
      _SqlCommandTarget = Nothing
      _DataTableSource = Nothing
      _DataTableTarget = Nothing
      _RowSource = Nothing
      _RowTarget = Nothing
      tempDataTable = Nothing
    End Try
  End Function

  Private Function ModifyOldRow(ByVal RowSource As DataRow, ByVal RowTarget As DataRow) As Boolean
    Dim _RowDifferent As Boolean
    Dim _SourceIsLatest As Boolean
    Try
      For I As Int32 = 0 To RowSource.Table.Columns.Count - 1
        If RowSource(I) Is DBNull.Value AndAlso RowTarget(RowSource.Table.Columns(I).ColumnName) IsNot DBNull.Value Then
          _RowDifferent = True
          Exit For
        ElseIf RowSource(I) IsNot DBNull.Value AndAlso RowTarget(RowSource.Table.Columns(I).ColumnName) Is DBNull.Value Then
          _RowDifferent = True
          Exit For
        ElseIf RowSource(I).ToString <> RowTarget(RowSource.Table.Columns(I).ColumnName).ToString Then
          _RowDifferent = True
          Exit For
        End If
      Next

      If _RowDifferent Then
        'Check which row is modified later
        If RowSource.RowState = DataRowState.Detached Then
          _SourceIsLatest = False
        ElseIf RowTarget.RowState = DataRowState.Detached Then
          _SourceIsLatest = True
          'Used company datatable of any row because datetime stamp column name must be same in every table.
        ElseIf RowSource.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName) Is DBNull.Value Then
          If RowTarget.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName) Is DBNull.Value Then
            'Skip the row can not say which one is latest.
            _RowDifferent = False
          Else
            'If source is null but target has value then target is latest.
            _SourceIsLatest = False
          End If
        Else
          If RowTarget.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName) Is DBNull.Value Then
            'If source is not null but target has null value in datetime stamp then source is latest.
            _SourceIsLatest = True
          Else
            If Convert.ToDateTime(RowSource.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName)) > Convert.ToDateTime(RowTarget.Item(_CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName)) Then
              _SourceIsLatest = True
            Else
              _SourceIsLatest = False
            End If
          End If
        End If

        'Update values
        For I As Int32 = 0 To RowSource.Table.Columns.Count - 1
          If _SourceIsLatest Then
            RowTarget(RowSource.Table.Columns(I).ColumnName) = RowSource(RowSource.Table.Columns(I).ColumnName)
          Else
            RowSource(RowSource.Table.Columns(I).ColumnName) = RowTarget(RowSource.Table.Columns(I).ColumnName)
          End If
        Next
        If _SourceIsLatest Then
          'Added one hour so that upload delay does not prevent current downloading user to download the record.
          RowTarget(_CompanyDataTableSource.Upload_DateTimeColumn.ColumnName) = Now.AddHours(1)
        End If
      End If

      Return True
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function LogTransferActivity(ByVal CompanyID As Int16, ByVal UserID As Int32, ByVal StartOfActivity As Boolean, ByVal TableName As String) As Boolean
    Try
      Dim StartDateTime As DateTime
      Dim EndDateTime As DateTime

      If StartOfActivity Then
        StartDateTime = Now
        _TransferDataRow = _TransferDataTableSource.NewTransferRow
        With _TransferDataRow
          .Co_ID = CompanyID
          .Transfer_StartDateTime = StartDateTime
          .TableName = TableName
          '.Source_Location = General.SourceLocation
          '.Download_DateTime = Convert.ToDateTime(_TransferTableAdapterTarget.GetMaximumStartDateTimeOtherLocation(CompanyID, General.SourceLocation, TableName))
          .Stamp_DateTime = Now
          .Stamp_UserID = UserID
          .Transfer_ID = Convert.ToInt32(_TransferTableAdapterSource.GetNextTransferIDByCoID(CompanyID))
        End With
        _TransferDataTableSource.Rows.Add(_TransferDataRow)
        _TransferTableAdapterSource.Update(_TransferDataTableSource)
      Else
        EndDateTime = Now
        With _TransferDataRow
          .Stamp_DateTime = Now
          .Transfer_EndDateTime = EndDateTime
          .Transfer_Status = 1    'Transfer Completed.
        End With
        _TransferTableAdapterSource.Update(_TransferDataTableSource)
      End If

    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function GetSelectCommandByPrimaryKey(ByVal _TableNamePara As String, ByVal _DataRowPara As DataRow) As SqlClient.SqlCommand
    Try
      Dim _SelectCommand As New SqlClient.SqlCommand
      Dim _SelectQueryString As String = String.Empty

      _SelectQueryString = "SELECT * FROM [" & _TableNamePara & "] WHERE " & GetPrimayKeyCriteria(_TableNamePara, _DataRowPara)
      _SelectCommand.CommandText = _SelectQueryString

      Return _SelectCommand
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function GetPrimayKeyCriteria(ByVal _TableNamePara As String, ByVal _DataRowPara As DataRow) As String
    Dim _IndexTA As New IndexTableAdapter
    Dim _IndexDataTable As IndexDataTable

    Try
      Dim _RowFilterString As String = String.Empty

      _IndexDataTable = _IndexTA.GetPrimaryKeyByTableName(_TableNamePara)
      For Each _IndexRow As IndexRow In _IndexDataTable
        If _RowFilterString <> String.Empty Then _RowFilterString &= " AND "
        'There is no possibility of NULL in any column because it is primary key.
        If _IndexRow.ColumnType.ToLower.IndexOf("int") >= 0 Then
          _RowFilterString &= _IndexRow.ColumnName & "=" & _DataRowPara(_IndexRow.ColumnName).ToString & ""
        Else
          _RowFilterString &= _IndexRow.ColumnName & "='" & _DataRowPara(_IndexRow.ColumnName).ToString & "'"
        End If
      Next

      Return _RowFilterString
    Catch ex As Exception
      Dim ExceptionObject As New Exception("Exception in GetRowFilterString method.", ex)
      Throw ExceptionObject
    Finally
      _IndexTA = Nothing
      _IndexDataTable = Nothing
    End Try
  End Function

  Private Function GetSelectCommandForDeltaChanges(ByVal _TableNamePara As String, ByVal _CoIDPara As Int16, ByVal _LastUploadDateTimePara As DateTime, ByVal _ToDateTimePara As DateTime, ByVal SourceDatabase As Boolean) As SqlClient.SqlCommand
    Try
      Dim _SelectCommand As New SqlClient.SqlCommand
      Dim _SelectQueryString As String = String.Empty

      If SourceDatabase Then
        _SelectQueryString = "SELECT * FROM [" & _TableNamePara & "] WHERE " & _CompanyDataTableSource.Co_IdColumn.ColumnName _
          & "=@Co_ID AND " & _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName & ">=@FromDateTime AND " _
          & _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName & "<=@ToDateTime"
        _SelectCommand.CommandText = _SelectQueryString

        _SelectCommand.Parameters.Add("@Co_ID", SqlDbType.SmallInt)
        _SelectCommand.Parameters.Add("@FromDateTime", SqlDbType.DateTime)
        _SelectCommand.Parameters.Add("@ToDateTime", SqlDbType.DateTime)

        _SelectCommand.Parameters("@Co_ID").Value = _CoIDPara
        _SelectCommand.Parameters.Item("@FromDateTime").Value = _LastUploadDateTimePara
        _SelectCommand.Parameters.Item("@ToDateTime").Value = _ToDateTimePara
      Else
        _SelectQueryString = "SELECT * FROM [" & _TableNamePara & "] WHERE " & _CompanyDataTableSource.Co_IdColumn.ColumnName _
          & "=@Co_ID AND " & _CompanyDataTableSource.Upload_DateTimeColumn.ColumnName & ">=@FromDateTime AND " _
          & _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName & "<=@ToDateTime"
        _SelectCommand.CommandText = _SelectQueryString

        _SelectCommand.Parameters.Add("@Co_ID", SqlDbType.SmallInt)
        _SelectCommand.Parameters.Add("@FromDateTime", SqlDbType.DateTime)
        _SelectCommand.Parameters.Add("@ToDateTime", SqlDbType.DateTime)

        _SelectCommand.Parameters("@Co_ID").Value = _CoIDPara
        _SelectCommand.Parameters.Item("@FromDateTime").Value = _LastUploadDateTimePara
        _SelectCommand.Parameters.Item("@ToDateTime").Value = _LastUploadDateTimePara
      End If

      Return _SelectCommand
    Catch ex As Exception
      Dim ExceptionObject As New Exception("Exception in GetSelectCommandForDeltaChanges method for table " & _TableNamePara, ex)
      Throw ExceptionObject
    End Try
  End Function

  Private Function GetUpdateCommand(ByVal _TableNamePara As String, ByVal _SqlConnection As SqlClient.SqlConnection) As SqlClient.SqlCommand
    Try
      Dim _SqlSelectCommand As New SqlClient.SqlCommand("SELECT * FROM " & _TableNamePara, _SqlConnection)
      Dim _SqlUpdateCommand As SqlClient.SqlCommand
      Dim _SqlDataAdapter As New SqlClient.SqlDataAdapter(_SqlSelectCommand)
      Dim _SqlCommandBuilder As New SqlClient.SqlCommandBuilder(_SqlDataAdapter)

      _SqlCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges
      _SqlUpdateCommand = _SqlCommandBuilder.GetUpdateCommand

      Return _SqlUpdateCommand
    Catch ex As Exception
      Dim ExceptionObject As New Exception("Exception in GetUpdateCommand method for table " & _TableNamePara, ex)
      Throw ExceptionObject
    End Try
  End Function

  Private Function GetInsertCommand(ByVal _TableNamePara As String, ByVal _SqlConnection As SqlClient.SqlConnection) As SqlClient.SqlCommand
    Try
      Dim _SqlSelectCommand As New SqlClient.SqlCommand("SELECT * FROM " & _TableNamePara, _SqlConnection)
      Dim _SqlInsertCommand As SqlClient.SqlCommand
      Dim _SqlDataAdapter As New SqlClient.SqlDataAdapter(_SqlSelectCommand)
      Dim _SqlCommandBuilder As New SqlClient.SqlCommandBuilder(_SqlDataAdapter)

      _SqlCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges
      _SqlInsertCommand = _SqlCommandBuilder.GetInsertCommand

      Return _SqlInsertCommand
    Catch ex As Exception
      Dim ExceptionObject As New Exception("Exception in GetInsertCommand method.", ex)
      Throw ExceptionObject
    End Try
  End Function
#End Region

#Region "Event Methods"

#End Region

  Public Sub New()
    Try
      '_TransferTableAdapterTarget.GetConnection.ConnectionString = WebServerConnectionString

      _CollectionDisplayName.Add("Companies") : _CollectionTableName.Add("Base_Company")
      '_CollectionDisplayName.Add("Document Types") : _CollectionTableName.Add("Common_DocumentType")
      '_CollectionDisplayName.Add("Entity Types") : _CollectionTableName.Add("Common_EntityType")
      '_CollectionDisplayName.Add("Common_Status_Type") : _CollectionTableName.Add("Common_Status_Type")
      '_CollectionDisplayName.Add("Status") : _CollectionTableName.Add("Common_Status")
      _CollectionDisplayName.Add("Users") : _CollectionTableName.Add("Sec_User")
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
      '_CollectionDisplayName.Add("Voucher") : _CollectionTableName.Add("Accounting_Voucher")
      _CollectionDisplayName.Add("Roles") : _CollectionTableName.Add("Sec_Role")
      _CollectionDisplayName.Add("User Roles Association") : _CollectionTableName.Add("Sec_User_Role_Association")
      _CollectionDisplayName.Add("Transfers") : _CollectionTableName.Add("Transfer")

    Catch ex As Exception
      Dim _qex As New Exception("Exception in New of TransferData.", ex)
      Throw _qex
    End Try
  End Sub
End Class

