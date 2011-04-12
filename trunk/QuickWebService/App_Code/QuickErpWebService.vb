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

      _TransferDataObject.PathForLogFile = Server.MapPath(DATA_TRANSFER_LOG_FILE)

      _TransferDataObject.AppendTextToLogFile(Environment.NewLine & Environment.NewLine & "********** Web Service Started ***********")
      _TargetConnectionStringPara = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString

      _TransferDataObject.AppendTextToLogFile("Calling TransferTableFromXML() method")
      _Succeeded = _TransferDataObject.TransferTableFromXML(_CompanyID, _UserID, _TargetConnectionStringPara, Server.MapPath("FtpFiles\") & _FileNameWithPath)

      'If _ExportFileToDownload Then
      '  _TransferDataObject.AppendTextToLogFile("Calling ExportDataToXmlFile")
      '  _TransferDataObject.ExportDataToXmlFile(_CompanyID, _UserID, False, _TargetConnectionStringPara, Server.MapPath("FtpFiles\"))
      'End If
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
End Class
