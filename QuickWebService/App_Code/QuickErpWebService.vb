Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports QuickDAL
Imports QuickLibrary

<Assembly: system.Security.AllowPartiallyTrustedCallers()> 

<WebService(Namespace:="http://quicktijarat.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Service
  Inherits System.Web.Services.WebService

  Private DATA_TRANSFER_LOG_FILE As String = "bin\DataTransferLog_" & Format(Common.SystemDateTime, "yyMM") & ".txt"

  <WebMethod()> _
  Public Function HelloWorld() As String
    Return "Hello World"
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY)
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  <WebMethod()> _
  Public Function ImportXmlFileToDatabase(ByVal _CompanyID As Int16, ByVal _UserID As Int32, ByVal _FileNameWithPath As String, ByVal _ExportFileToDownload As Boolean, ByVal _TargetConnectionStringPara As String) As Boolean
    Try
      Dim _TransferDataObject As New TransferData
      Dim _Succeeded As Boolean
      Dim _AlertTA As New QuickDAL.QuickCommonDataSetTableAdapters.AlertTableAdapter(_TargetConnectionStringPara)
      Dim _CompanyTA As New QuickDAL.QuickCommonDataSetTableAdapters.CompanyTableAdapter
      Dim _CompanyTable As QuickDAL.QuickCommonDataSet.CompanyDataTable

      _TransferDataObject.PathForLogFile = Server.MapPath(DATA_TRANSFER_LOG_FILE)
      _TransferDataObject.AppendTextToLogFile(Environment.NewLine & Environment.NewLine & "********** Web Service Started ***********")
      'QuickDAL.SharedSetting.QuickErpConnectionString = _TargetConnectionStringPara
      QuickDAL.SharedSetting.QuickErpConnectionString = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString
      _TargetConnectionStringPara = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString


      'Try
      '_CompanyTA.GetConnection.ConnectionString = _TargetConnectionStringPara
      '_TransferDataObject.AppendTextToLogFile(_TargetConnectionStringPara)
      '_CompanyTable = _CompanyTA.GetByCoId(_CompanyID)
      '_TransferDataObject.AppendTextToLogFile("get company")
      '_AlertTA.GetConnection.ConnectionString = _TargetConnectionStringPara
      '_AlertTA.SendSms(_CompanyID, _UserID, "03009455050", "03009455050", "DataTransfer", "Transfering data by " & _CompanyTable(0).Co_Code, Nothing)
      '_TransferDataObject.AppendTextToLogFile("sms added")

      'Catch ex As Exception
      '  _TransferDataObject.AppendTextToLogFile("Sms " & ex.Message)
      'End Try
      _TransferDataObject.LogTransferActivity(_CompanyID, _UserID, True, "All")

      _TransferDataObject.AppendTextToLogFile("Calling TransferTableFromXML() method")
      _Succeeded = _TransferDataObject.TransferTableFromXML(_CompanyID, _UserID, _TargetConnectionStringPara, Server.MapPath("FtpFiles\") & _FileNameWithPath)

      'If _ExportFileToDownload Then
      '  _TransferDataObject.AppendTextToLogFile("Calling ExportDataToXmlFile")
      '  _TransferDataObject.ExportDataToXmlFile(_CompanyID, _UserID, False, _TargetConnectionStringPara, Server.MapPath("FtpFiles\"))
      'End If
      _TransferDataObject.LogTransferActivity(_CompanyID, _UserID, False, "All")

      _TransferDataObject.AppendTextToLogFile("********** Web Service Ended ***********")

      Return _Succeeded
    Catch ex As Exception
      My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Common.SystemDateTime.ToString & ": exception text=" & ex.Message, True)
      Throw ex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 03-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will return the datatable containing allowed companies and
  ''' tables.
  ''' </summary>
  <WebMethod()> _
  Public Function GetAllowedTables(ByVal DatabaseServiceBrokerGuidpara As String) As Data.DataTable
    Try
      Dim _LocationCompanyTableAssociationTA As New QuickSecurityDataSetTableAdapters.LocationCompanyTableAssociationTableAdapter
      Dim _LocationCompanyTableAssociationTable As QuickSecurityDataSet.LocationCompanyTableAssociationDataTable

      _LocationCompanyTableAssociationTA.GetConnection.ConnectionString = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString
      _LocationCompanyTableAssociationTable = _LocationCompanyTableAssociationTA.GetByDatabaseGuid(DatabaseServiceBrokerGuidpara)

      Return _LocationCompanyTableAssociationTable

    Catch ex As Exception
      Dim _qex As New Exception("Exception in GetAllwedTables of QuickErpWebService.", ex)
      Throw _qex
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
  ''' This function will export the data to xml file for downloading.
  ''' </summary>
  Private Function ExportDataToFileForDownload() As Boolean
    Try

    Catch ex As Exception
      My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Common.SystemDateTime.ToString & ": exception text=" & ex.Message, True)
      Throw ex
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will be called by mobile device to retrieve messsages for sending.
  ''' </summary>
  <WebMethod()> _
  Public Function GetSmsToSend(ByRef ErrorMessage As String) As Data.DataTable
    Try
      Dim _AlertTA As New QuickDAL.QuickCommonDataSetTableAdapters.AlertTableAdapter
      Dim _AlertTable As QuickDAL.QuickCommonDataSet.AlertDataTable
      SharedSetting.QuickErpConnectionString = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString

      _AlertTable = _AlertTA.Get5NotSentSms
      _AlertTable.PrimaryKey = Nothing

      For I As Int32 = _AlertTable.Columns.Count - 1 To 0
        Select Case _AlertTable.Columns(I).ColumnName
          Case _AlertTable.Co_IDColumn.ColumnName, _AlertTable.Alert_IDColumn.ColumnName, _AlertTable.Alert_BodyColumn.ColumnName, _AlertTable.Alert_DestinationColumn.ColumnName
          Case Else
            _AlertTable.Columns.RemoveAt(I)
        End Select
      Next

      Return _AlertTable

    Catch ex As Exception
      ErrorMessage = ex.Message
      Return Nothing
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will update database for the messages with their status.
  ''' </summary>
  <WebMethod()> _
  Public Function UpdateSmsStatus(ByVal SendSmsIDs As String, ByVal FailedSmsIDs As String) As String
    Try
      Dim _CoID As String = String.Empty
      Dim _AlertID As String = String.Empty
      Dim _IsCoID As Boolean
      Dim _AlertTA As New QuickDAL.QuickCommonDataSetTableAdapters.AlertTableAdapter
      SharedSetting.QuickErpConnectionString = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString
      Dim _ReturnString As String = String.Empty

      _IsCoID = True
      For I As Int32 = 0 To SendSmsIDs.Length - 1
        If SendSmsIDs.Substring(I, 1) <> "-" AndAlso SendSmsIDs.Substring(I, 1) <> "," Then
          If _IsCoID Then
            _CoID &= SendSmsIDs.Substring(I, 1)
          Else
            _AlertID &= SendSmsIDs.Substring(I, 1)
          End If
        End If

        If SendSmsIDs.Substring(I, 1) = "-" Then
          _IsCoID = False
        ElseIf SendSmsIDs.Substring(I, 1) = "," OrElse I = SendSmsIDs.Length - 1 Then
          _IsCoID = True
          _ReturnString &= "updating sent sms co_id, alertid: " & _CoID & "," & _AlertID
          _ReturnString &= _AlertTA.UpdateStatus(QuickLibrary.Constants.DocumentStatuses.Alert_Send, Convert.ToInt32(_CoID), Convert.ToInt32(_AlertID)).ToString & " rows effected"
          _CoID = String.Empty
          _AlertID = String.Empty
        End If
        '_ReturnString &= "i, co_id, alert_id, char :" & I.ToString & "," & _CoID & "," & _AlertID & "," & SendSmsIDs.Substring(I, 1)
      Next

      _CoID = String.Empty
      _AlertID = String.Empty

      For I As Int32 = 0 To FailedSmsIDs.Length - 1

        If FailedSmsIDs.Substring(I, 1) <> "-" AndAlso FailedSmsIDs.Substring(I, 1) <> "," Then
          If _IsCoID Then
            _CoID &= FailedSmsIDs.Substring(I, 1)
          Else
            _AlertID &= FailedSmsIDs.Substring(I, 1)
          End If
        End If

        If FailedSmsIDs.Substring(I, 1) = "-" Then
          _IsCoID = False
        ElseIf FailedSmsIDs.Substring(I, 1) = "," OrElse I = FailedSmsIDs.Length - 1 Then
          _IsCoID = True
          _ReturnString &= "updating failed sms co_id, alertid: " & _CoID & "," & _AlertID
          _ReturnString &= _AlertTA.UpdateStatus(QuickLibrary.Constants.DocumentStatuses.Alert_NotSent, Convert.ToInt32(_CoID), Convert.ToInt32(_AlertID)).ToString & " rows effected"
          _CoID = String.Empty
          _AlertID = String.Empty
        Else
        End If
      Next

      _ReturnString &= "All sms updated sucessfully"
      Return _ReturnString

    Catch ex As Exception
      Return ex.Message
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 29-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will process recieved sms.
  ''' </summary>
  <WebMethod()> _
  Public Function MessageReceived(ByVal Sender As String, ByVal SmsText As String, ByVal SmsDateTime As DateTime) As String
    Try
      Dim _AlertTA As New QuickDAL.QuickCommonDataSetTableAdapters.AlertTableAdapter

      _AlertTA.MessageReceived(Sender, SmsText, SmsDateTime)

      Return "Received message added in database"
    Catch ex As Exception
      Return ex.Message
    End Try
  End Function


End Class
