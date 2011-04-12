Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickSystemDataSetTableAdapters
Imports QuickDAL.LogicalDataSet
Imports QuickDAL.QuickSystemDataSet
Imports QuickDAL.QuickCommonDataSet
Imports System.Data
Imports System.IO
Imports System.IO.Compression

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
  Public TableNameCollection As New Collection
  Private _CollectionDisplayName As New Collection
  Private _TotalProgress As Int32
  Private _CurrentProgress As Int32
  Public PathForLogFile As String = String.Empty

  Public Event ProgressChanged(ByVal _CurrentValue As Int32, ByVal _TotalValue As Int32)
  Public Event StatusChanged(ByVal _StatusText As String)

  Public Const LOCAL_PATH_FOR_FTP_FILES As String = "C:\"
  Public Const NO_RECORDS_FILE_NAME As String = "NoRecords"

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
  Public Sub AppendTextToLogFile(ByVal TextToAppend As String)
    Try
      If PathForLogFile IsNot Nothing AndAlso PathForLogFile.Length > 0 Then
        My.Computer.FileSystem.WriteAllText(PathForLogFile, Environment.NewLine & Date.UtcNow.ToString & ": " & TextToAppend, True)
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
  Public Function ExportDataToXmlFile(ByVal _CompanyID As Int16, ByVal _UserID As Int32, ByVal TransferOnlyCompanyAndUser As Boolean, ByVal _ConnectionString As String, ByVal FilePath As String, ByVal _AllowedCompaniesAndTablesTable As DataTable) As String
    Try
      Dim _Dataset As New DataSet
      Dim _FileName As String = _CompanyID.ToString & "_" & Format(Now, "yyMMddhhmm") & ".qerp"
      Dim _CompressedFileName As String = _FileName.Substring(0, _FileName.LastIndexOf("."c)) & ".zip"
      Dim _AllowedTablesTable As New QuickSecurityDataSet.LocationCompanyTableAssociationDataTable
      'Dim _FetchDataTry As Int32 = 0
      Dim _IsTableAllowed As Boolean
      Dim _TotalRecords As Int32 = 0

      Try
        Dim _CompanyForDataTransfer As New CompanyDataTable

        AppendTextToLogFile("Fetching current company and its child companies")
        _CurrentProgress = 0
        _CompanyForDataTransfer = _CompanyTableAdapterSource.GetParentAndChildsByCoID(_CompanyID)

        For Each _CompanyRowForDataTransfer As CompanyRow In _CompanyForDataTransfer
          If Not TransferOnlyCompanyAndUser Then
            _TotalProgress = TableNameCollection.Count * 100
          Else
            _TotalProgress = 200
          End If
          RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)
          '_Dataset = New DataSet

          If Not TransferOnlyCompanyAndUser Then
            For I As Int32 = 1 To TableNameCollection.Count
              If _TotalRecords < 500 Then
                _IsTableAllowed = False
                For r As Int32 = 0 To _AllowedCompaniesAndTablesTable.Rows.Count - 1
                  With _AllowedCompaniesAndTablesTable.Rows(r)
                    If _CompanyRowForDataTransfer.Co_Id = Convert.ToInt32(.Item(_AllowedTablesTable.Co_IDColumn.ColumnName).ToString) AndAlso TableNameCollection(I).ToString = .Item(_AllowedTablesTable.TableNameColumn.ColumnName).ToString AndAlso .Item(_AllowedTablesTable.AllowUploadedColumn.ColumnName).ToString.ToLower = "true" Then
                      _IsTableAllowed = True
                    End If
                  End With
                Next r  'Loop to check allowed table

                If _IsTableAllowed Then
                  RaiseEvent StatusChanged("Started " & _CollectionDisplayName(I).ToString & " ... ")
                  AppendTextToLogFile("Starting processing table " & TableNameCollection(I).ToString)

                  _TotalRecords += FetchData(TableNameCollection(I).ToString, TableNameCollection(I).ToString, _CompanyRowForDataTransfer.Co_Id, _UserID, _ConnectionString, _Dataset)
                  AppendTextToLogFile("Finished processing table " & TableNameCollection(I).ToString & " Recs " & _TotalRecords.ToString)
                  RaiseEvent StatusChanged("Completed. Recs " & _TotalRecords.ToString)
                  AppendTextToLogFile(String.Empty)
                  RaiseEvent StatusChanged(String.Empty)
                Else
                  'RaiseEvent StatusChanged("Skipped " & _CollectionDisplayName(I).ToString & " because it is not allowed")
                  'AppendTextToLogFile("Skipped " & TableNameCollection(I).ToString & " because it is not allowed")
                End If  'If table allowed
              End If  'check for total records
            Next I  'Loop for tables
          End If

          AppendTextToLogFile("Finished Company = " & _CompanyRowForDataTransfer.Co_Code & "-" & _CompanyRowForDataTransfer.Co_Desc)
          RaiseEvent StatusChanged("Finished Company = " & _CompanyRowForDataTransfer.Co_Code & "-" & _CompanyRowForDataTransfer.Co_Desc)

          _CurrentProgress = _TotalProgress
          RaiseEvent ProgressChanged(_CurrentProgress, _TotalProgress)
        Next  'Loop for companies

        If _TotalRecords > 0 Then
          AppendTextToLogFile("Writing Xml File")
          _Dataset.WriteXml(FilePath & _FileName)
          AppendTextToLogFile("Compressing File...")
          CompressFile(TransferData.LOCAL_PATH_FOR_FTP_FILES & _FileName, TransferData.LOCAL_PATH_FOR_FTP_FILES & _CompressedFileName)
          ''IO.File.Delete(TransferData.LOCAL_PATH_FOR_FTP_FILES & _FileName)
          AppendTextToLogFile("Finished Writing Xml File")
          Return _CompressedFileName
        Else
          Return NO_RECORDS_FILE_NAME
        End If

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

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal         27-Mar-11   Changing fetching logic from date to uploaded indicator
  '                           Previously date was compared, now Uploaded_DateTime
  '                           will be checked to null. Also from and to date logic
  '                           is being removed because it is not required anymore.
  ''' <summary>
  ''' This function differential data from given table and adds the table in provided byref dataset.
  ''' </summary>
  Private Function FetchData(ByVal _TableNamepara As String, ByVal _TableDisplayNamePara As String, ByVal CompanyID As Int16, ByVal UserID As Int32, ByVal _SourceConnectionStringPara As String, ByRef _DataSet As DataSet) As Int32
    Dim _SqlConnectionSource As New SqlClient.SqlConnection(_SourceConnectionStringPara)
    Dim _SqlDataAdapterSource As New SqlClient.SqlDataAdapter
    Dim _SqlCommandSource As New SqlClient.SqlCommand
    Dim _DataTableSource As New DataTable
    Dim _RowSource As DataRow
    Dim tempDataTable As New DataTable
    Dim _NumberOfRecords As Int32 = 0

    Try
      '_SqlConnectionSource.ConnectionString = _SourceConnectionStringPara
      '_SqlCommandSource.Connection = _SqlConnectionSource
      '_SqlDataAdapterSource.SelectCommand = _SqlCommandSource
      _SqlDataAdapterSource.UpdateCommand = GetUpdateCommand(_TableNamepara, _SqlConnectionSource)
      _SqlDataAdapterSource.InsertCommand = GetInsertCommand(_TableNamepara, _SqlConnectionSource)

      LogTransferActivity(CompanyID, UserID, True, _TableNamepara)
      _CompanyDataTableSource = New CompanyDataTable
      If _DataSet.Tables.Contains(_TableNamepara) Then _DataTableSource = _DataSet.Tables(_TableNamepara)

      If _SqlConnectionSource.State = ConnectionState.Closed Then _SqlConnectionSource.Open()
      _SqlCommandSource = GetSelectCommandForDeltaChanges(_TableNamepara, CompanyID)
      _SqlCommandSource.Connection = _SqlConnectionSource
      _SqlDataAdapterSource.SelectCommand = _SqlCommandSource

      _NumberOfRecords = _SqlDataAdapterSource.Fill(_DataTableSource)
      If Not _DataSet.Tables.Contains(_TableNamepara) Then
        _DataTableSource.TableName = _TableNamepara
        _DataSet.Tables.Add(_DataTableSource)
      End If

      Return _NumberOfRecords

    Catch ex As Exception
      FetchData = -1
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
      _UploadFtpWebRequest.UsePassive = False

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

  Public Function TransferTableFromXML(ByVal _CompanyID As Int16, ByVal _UserID As Int32, ByVal _TargetConnectionStringPara As String, ByVal _FileNameWithPath As String) As Boolean
    Dim _SqlConnectionTarget As New SqlClient.SqlConnection(_TargetConnectionStringPara)
    'Dim _SqlDataAdapterTarget As New SqlClient.SqlDataAdapter
    Dim _SqlCommandTarget As SqlClient.SqlCommand
    Dim _DbObjectTA As New QuickSystemDataSetTableAdapters.ObjectTableAdapter
    Dim _DBObjectTable As QuickSystemDataSet.ObjectDataTable
    Dim _DataSetFromXML As New DataSet
    Dim _DataTableSource As DataTable
    'Dim _DataTableTarget As New DataTable
    'Dim _RowSource As DataRow
    'Dim _RowTarget As DataRow
    'Dim tempDataTable As New DataTable
    Dim _TableNamepara As String
    Dim _TableDisplayNamePara As String
    Dim _ProgressValue As Int32, _ProgressTotal As Int32
    Dim _DecompressedFileNameWithPath As String = _FileNameWithPath.Substring(0, _FileNameWithPath.LastIndexOf("."c)) & ".qerp"
    Dim _ParametersDetailForLogFile As String

    Try
      _DbObjectTA.GetConnection.ConnectionString = _TargetConnectionStringPara

      AppendTextToLogFile("TransferTableFromXML(" & _CompanyID.ToString & "," & _UserID.ToString & "," & _TargetConnectionStringPara & "," & _FileNameWithPath & ") method started")

      AppendTextToLogFile("Decompressing " & _FileNameWithPath & " into " & _DecompressedFileNameWithPath)
      DecompressFile(_FileNameWithPath, _DecompressedFileNameWithPath)
      AppendTextToLogFile("Reading " & _DecompressedFileNameWithPath)
      _DataSetFromXML.ReadXml(_DecompressedFileNameWithPath)
      _ProgressValue = 0 : _ProgressTotal = TableNameCollection.Count
      RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)

      AppendTextToLogFile("XML is read into memory")

      For J As Int32 = 1 To TableNameCollection.Count
        _TableNamepara = TableNameCollection(J).ToString
        _TableDisplayNamePara = _CollectionDisplayName(J).ToString
        _DBObjectTable = _DbObjectTA.GetByObjectName(TableNameCollection(J).ToString)
        AppendTextToLogFile("Starting processing table " & _TableNamepara)

        'below line will clear last table command text and parameters etc.
        _SqlCommandTarget = New SqlClient.SqlCommand(String.Empty, _SqlConnectionTarget)

        If _DataSetFromXML.Tables.Contains(_TableNamepara) AndAlso _DBObjectTable.Rows.Count > 0 Then
          _DataTableSource = _DataSetFromXML.Tables(_TableNamepara)
          '_DataTableTarget = Nothing : _DataTableTarget = New DataTable

          'LogTransferActivity(_CompanyID, _UserID, True, _TableNamepara)

          _CompanyDataTableSource = New CompanyDataTable
          AppendTextToLogFile("Insert/Update queries created")

          If _SqlConnectionTarget.State = ConnectionState.Closed Then _SqlConnectionTarget.Open()
          AppendTextToLogFile("Connection is opened on target database")

          _ProgressValue = 0 : _ProgressTotal = _DataTableSource.Rows.Count
          RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)

          AppendTextToLogFile("Getting command text to update target db")
          _TransferTableAdapterTarget.Connection.ConnectionString = _TargetConnectionStringPara
          _SqlCommandTarget.CommandText = _TransferTableAdapterTarget.spGetInsertAndUpdateCommandQuery(_TableNamepara).ToString

          'AppendTextToLogFile(_SqlCommandTarget.CommandText)
          AppendTextToLogFile("Starting to syncronize data in target db")
          For I As Int32 = 0 To _DataTableSource.Rows.Count - 1

            _ParametersDetailForLogFile = String.Empty

            For C As Int32 = 0 To _DBObjectTable.Rows.Count - 1

              Try
                If Not _SqlCommandTarget.Parameters.Contains("@" & _DBObjectTable(C).ColumnName) Then
                  _SqlCommandTarget.Parameters.Add("@" & _DBObjectTable(C).ColumnName, GetSqlDbType(_DBObjectTable(C).ColumnType))
                  _ParametersDetailForLogFile &= " @" & _DBObjectTable(C).ColumnName & " " & _DBObjectTable(C).ColumnType & " " & GetSqlDbType(_DBObjectTable(C).ColumnType).ToString
                End If

                If _DBObjectTable(C).ColumnName <> _CompanyDataTableSource.Upload_DateTimeColumn.ColumnName Then
                  If _DataTableSource.Columns.Contains(_DBObjectTable(C).ColumnName) Then
                    _SqlCommandTarget.Parameters.Item("@" & _DBObjectTable(C).ColumnName).Value = _DataTableSource.Rows(I).Item(_DBObjectTable(C).ColumnName)
                    _ParametersDetailForLogFile &= " @" & _DBObjectTable(C).ColumnName & " " & _SqlCommandTarget.Parameters.Item("@" & _DBObjectTable(C).ColumnName).Value.ToString
                  Else
                    _SqlCommandTarget.Parameters.Item("@" & _DBObjectTable(C).ColumnName).Value = DBNull.Value
                    _ParametersDetailForLogFile &= " @" & _DBObjectTable(C).ColumnName & " NULL (Don't Exist in xml)"
                  End If
                Else
                  _SqlCommandTarget.Parameters.Item("@" & _DBObjectTable(C).ColumnName).Value = Date.UtcNow
                  _ParametersDetailForLogFile &= " @" & _DBObjectTable(C).ColumnName & " " & _SqlCommandTarget.Parameters.Item("@" & _DBObjectTable(C).ColumnName).Value.ToString
                End If

              Catch ex As Exception
                Throw New Exception("Exception in adding/setting parameter for row number " & I.ToString & " column number " & C.ToString & " column name " & _DBObjectTable(C).ColumnName, ex)
              End Try

            Next

            Try
              _SqlCommandTarget.ExecuteNonQuery()

            Catch ex As Exception
              Throw New Exception("Exception in executing query for row number " & I.ToString & " " & _ParametersDetailForLogFile, ex)
            End Try

            _ProgressValue += 1
            RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)
          Next

    RaiseEvent ProgressChanged(_ProgressValue, _ProgressTotal)

    'LogTransferActivity(_CompanyID, _UserID, False, _DataTableSource.TableName)
    'Below line is necessary, it is mandatory when there was no data to transfer.
        End If
    AppendTextToLogFile("Finished processing table " & _TableNamepara & Environment.NewLine)
      Next

    TransferTableFromXML = True

    Catch ex As Exception
      TransferTableFromXML = False
      AppendTextToLogFile("Exception in TransferTableFromXml() method. Exception text=" & ex.Message)
      Throw New Exception("Exception in TransferTableFromXML method of TransferData.", ex)
    Finally
      _SqlConnectionTarget = Nothing
      '_SqlDataAdapterTarget = Nothing
      _SqlCommandTarget = Nothing
      _DataTableSource = Nothing
      '_DataTableTarget = Nothing
      '_RowSource = Nothing
      '_RowTarget = Nothing
      'tempDataTable = Nothing
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

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Mar-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal Saleem  27-Mar-11   I didn't find any use of parameter "SourceDatabase"
  '                           because code is same in both parts of if condition
  '                           so removing if statement and mentioned parameter.
  ''' <summary>
  ''' This method retuns sql command to fetch delta changes for upload. The returned
  ''' command will be used to fetch delta changes from the table.
  ''' </summary>
  Private Function GetSelectCommandForDeltaChanges(ByVal _TableNamePara As String, ByVal _CoIDPara As Int16) As SqlClient.SqlCommand
    Try
      Dim _SelectCommand As New SqlClient.SqlCommand
      Dim _SelectQueryString As String = String.Empty

      _SelectQueryString = "SELECT TOP 500 * FROM [" & _TableNamePara & "] WHERE " & _CompanyDataTableSource.Co_IdColumn.ColumnName _
          & "=@Co_ID AND (Upload_DateTime IS NULL)" ' OR Stamp_DateTime > Upload_DateTime)"

      _SelectCommand.CommandText = _SelectQueryString
      _SelectCommand.Parameters.Add("@Co_ID", SqlDbType.SmallInt)
      _SelectCommand.Parameters("@Co_ID").Value = _CoIDPara
      
      Return _SelectCommand
    Catch ex As Exception
      Dim ExceptionObject As New Exception("Exception in GetSelectCommandForDeltaChanges method for table " & _TableNamePara, ex)
      Throw ExceptionObject
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 01-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This function will read xml file and set the records status to uploaded in
  ''' database.
  ''' </summary>
  Public Function SetUploadedIndicatorFromXmlFileData(ByVal _ConnectionStringPara As String, ByVal _FileNameWithPath As String) As Boolean
    Dim _DataSetFromXML As New DataSet
    Dim _SqlCommand As New SqlClient.SqlCommand(String.Empty, New SqlClient.SqlConnection(_ConnectionStringPara))
    Dim _ObjectTA As New QuickSystemDataSetTableAdapters.ObjectTableAdapter
    Dim _ObjectTable As QuickSystemDataSet.ObjectDataTable
    Dim _Where As String
    Dim _ParameterDetail As String

    Try

      _DataSetFromXML.ReadXml(_FileNameWithPath)
      For Each _Table As DataTable In _DataSetFromXML.Tables

        _ObjectTable = _ObjectTA.GetByObjectName(_Table.TableName)

        If _SqlCommand.Connection.State = ConnectionState.Closed Then _SqlCommand.Connection.Open()

        _SqlCommand.Parameters.Clear()
        _SqlCommand.CommandText = "UPDATE " & _Table.TableName & " SET " & _CompanyDataTableSource.Upload_DateTimeColumn.ColumnName & "=GetUtcDate() WHERE "

        _Where = String.Empty

        For C As Int32 = 0 To _Table.Columns.Count - 1

          For R As Int32 = 0 To _ObjectTable.Rows.Count - 1

            If _ObjectTable(R).ColumnName = _Table.Columns(C).ColumnName Then

              If _ObjectTable(R).IsPrimaryKey OrElse _ObjectTable(R).ColumnName = _CompanyDataTableSource.Stamp_DateTimeColumn.ColumnName Then
                _Where &= " AND " & _Table.Columns(C).ColumnName & "= @" & _Table.Columns(C).ColumnName
                _SqlCommand.Parameters.Add("@" & _Table.Columns(C).ColumnName, GetSqlDbType(_ObjectTable(R).ColumnType))
                Exit For
              End If
            End If
          Next R
        Next C

        _SqlCommand.CommandText &= _Where.Substring(4)

        _ParameterDetail = _SqlCommand.CommandText

        For R As Int32 = 0 To _Table.Rows.Count - 1

          For C As Int32 = 0 To _Table.Columns.Count - 1
            If _SqlCommand.Parameters.Contains("@" & _Table.Columns(C).ColumnName) Then
              _SqlCommand.Parameters.Item("@" & _Table.Columns(C).ColumnName).Value = _Table.Rows(R).Item(C)
              _ParameterDetail &= " @" & _Table.Columns(C).ColumnName & "=" & _SqlCommand.Parameters.Item("@" & _Table.Columns(C).ColumnName).Value.ToString
            Else
              'If parameter does not exist it means value is not required.
            End If
          Next

          Try
            Debug.WriteLine(_SqlCommand.ExecuteNonQuery().ToString)

          Catch ex As Exception
            Throw New Exception("Exception in setting uploaded indicator in local db on row " & R.ToString & " " & _ParameterDetail, ex)
          End Try
          '_Table.Rows(R).AcceptChanges()
          '_Table.Rows(R).Item(_CompanyDataTableSource.Upload_DateTimeColumn.ColumnName) = Date.UtcNow
          '_SqlAdapter.Update(_Table)
        Next R
      Next

      Return True
    Catch ex As Exception
      Dim ExceptionObject As New Exception("Exception in SetUploadedIndicatorFromXmlFileData method of TransferData.", ex)
      Throw ExceptionObject
    Finally
      _DataSetFromXML = Nothing
      _SqlCommand = Nothing
      _ObjectTA = Nothing
      _ObjectTable = Nothing
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

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This function will compress the file.
  ''' </summary>
  Public Function CompressFile(ByVal _SourceFileName As String, ByVal _TargetFileName As String) As Boolean
    Dim _SourceFileStream As IO.FileStream = IO.File.OpenRead(_SourceFileName)
    Dim _TargetFileStream As IO.FileStream = IO.File.Create(_TargetFileName)
    Dim _gZip As New IO.Compression.GZipStream(_TargetFileStream, IO.Compression.CompressionMode.Compress, True)
    Dim bytes As Int32 = _SourceFileStream.ReadByte

    Try
      Do While bytes <> -1
        _gZip.WriteByte(Convert.ToByte(bytes))
        bytes = _SourceFileStream.ReadByte
      Loop

    Catch ex As Exception
      Dim _qex As New Exception("Exception in CompressFile of TransferData.", ex)
      Throw _qex
    Finally
      _gZip.Close()
      _TargetFileStream.Close()
      _SourceFileStream.Close()
    End Try
  End Function

  '' Method to compress.
  'Private Sub Compress(ByVal fi As FileInfo)
  '  ' Get the stream of the source file.
  '  Using inFile As FileStream = fi.OpenRead()
  '    ' Compressing:
  '    ' Prevent compressing hidden and already compressed files.

  '    If (File.GetAttributes(fi.FullName) And FileAttributes.Hidden) _
  '        <> FileAttributes.Hidden And fi.Extension <> ".gz" Then
  '      ' Create the compressed file.
  '      Using outFile As FileStream = File.Create(fi.FullName + ".gz")
  '        Using Compress As GZipStream = _
  '         New GZipStream(outFile, CompressionMode.Compress)

  '          ' Copy the source file into the compression stream.
  '          inFile.CopyTo(Compress)

  '          Console.WriteLine("Compressed {0} from {1} to {2} bytes.", _
  '                            fi.Name, fi.Length.ToString(), outFile.Length.ToString())

  '        End Using
  '      End Using
  '    End If
  '  End Using
  'End Sub

  'Author: Faisal Salem 
  'Date Created(DD-MMM-YY): 31-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Method to decompress file
  ''' </summary>
  Public Function DecompressFile(ByVal _SourceFileName As String, ByVal _TargetFileName As String) As Boolean
    Dim sourceFile As FileStream = File.OpenRead(_SourceFileName)
    Dim destinationFile As FileStream = File.Create(_TargetFileName)
    Dim unzip As GZipStream = New GZipStream(sourceFile, CompressionMode.Decompress, False)
    Dim bytes As Int32 = unzip.ReadByte()

    Try
      Do While bytes <> -1
        destinationFile.WriteByte(Convert.ToByte(bytes))
        bytes = unzip.ReadByte
      Loop

    Catch ex As Exception
      Dim _qex As New Exception("Exception in CompressFile of TransferData.", ex)
      Throw _qex
    Finally
      sourceFile.Close()
      destinationFile.Close()
      unzip.Close()
    End Try
  End Function

#End Region

#Region "Event Methods"

#End Region

  Public Sub New()
    Try
      '_TransferTableAdapterTarget.GetConnection.ConnectionString = WebServerConnectionString

      _CollectionDisplayName.Add("Companies") : TableNameCollection.Add("Base_Company")
      '_CollectionDisplayName.Add("Document Types") : _CollectionTableName.Add("Common_DocumentType")
      '_CollectionDisplayName.Add("Entity Types") : _CollectionTableName.Add("Common_EntityType")
      '_CollectionDisplayName.Add("Common_Status_Type") : _CollectionTableName.Add("Common_Status_Type")
      '_CollectionDisplayName.Add("Status") : _CollectionTableName.Add("Common_Status")
      _CollectionDisplayName.Add("Users") : TableNameCollection.Add("Sec_User")
      _CollectionDisplayName.Add("Items") : TableNameCollection.Add("Inv_Item")
      _CollectionDisplayName.Add("Parties") : TableNameCollection.Add("Party")
      _CollectionDisplayName.Add("COA(s)") : TableNameCollection.Add("Accounting_COA")
      _CollectionDisplayName.Add("Voucher Types") : TableNameCollection.Add("Accounting_VoucherType")
      _CollectionDisplayName.Add("Inventory") : TableNameCollection.Add("Inventory")
      _CollectionDisplayName.Add("Inventory Details") : TableNameCollection.Add("Inventory_Detail")
      _CollectionDisplayName.Add("Inventory Sales Invoice") : TableNameCollection.Add("Inventory_SalesInvoice")
      _CollectionDisplayName.Add("Voucher") : TableNameCollection.Add("Accounting_Voucher")
      _CollectionDisplayName.Add("Voucher Details") : TableNameCollection.Add("Accounting_Voucher_Detail")
      _CollectionDisplayName.Add("Settings") : TableNameCollection.Add("Base_Setting")
      '_CollectionDisplayName.Add("Voucher") : _CollectionTableName.Add("Accounting_Voucher")
      _CollectionDisplayName.Add("Roles") : TableNameCollection.Add("Sec_Role")
      _CollectionDisplayName.Add("User Roles Association") : TableNameCollection.Add("Sec_User_Role_Association")
      _CollectionDisplayName.Add("Transfers") : TableNameCollection.Add("Transfer")

    Catch ex As Exception
      Dim _qex As New Exception("Exception in New of TransferData.", ex)
      Throw _qex
    End Try
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 09-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This function receives sql data type as string and returns vb.net sqldbtype
  ''' enumerication value.
  ''' </summary>
  Private Function GetSqlDbType(ByVal _SqlType As String) As SqlDbType
    Try
      If _SqlType.ToLower = "int" Then
        SqlDbType.BigInt.ToString()
        Return SqlDbType.Int
      ElseIf _SqlType.ToLower = "smallint" Then
        Return SqlDbType.SmallInt
      ElseIf _SqlType.ToLower = "bigint" Then
        Return SqlDbType.BigInt
      ElseIf _SqlType.ToLower = "datetime" Then
        Return SqlDbType.DateTime
      ElseIf _SqlType.ToLower = "money" Then
        Return SqlDbType.Money
      ElseIf _SqlType.ToLower = "decimal" Then
        Return SqlDbType.Decimal
      Else
        Return SqlDbType.VarChar
      End If

    Catch ex As Exception
      Dim _qex As New Exception("Exception in GetSqlDbType of TransferData.", ex)
      Throw _qex
    End Try
  End Function


End Class
