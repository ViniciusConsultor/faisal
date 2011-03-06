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
  Public Function ImportXmlFileToDatabase(ByVal _CompanyID As Int16, ByVal _UserID As Int32, ByVal _FileNameWithPath As String, ByVal _FromDate As DateTime, ByVal _ToDate As DateTime, ByVal _ExportFile As Boolean, ByVal _TargetConnectionStringPara As String) As Boolean
    Try
      Dim _TransferDataObject As New TransferData
      _TransferDataObject.PathForLogFile = Server.MapPath(DATA_TRANSFER_LOG_FILE)

      My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Common.SystemDateTime.ToString & ": ********** Web Service Started ***********", True)
      _TargetConnectionStringPara = ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString

      My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Common.SystemDateTime.ToString & ": Calling TransferTableFromXML() method", True)
      _TransferDataObject.TransferTableFromXML(_CompanyID, _UserID, _TargetConnectionStringPara, Server.MapPath("FtpFiles\") & _FileNameWithPath, _FromDate, _ToDate)

      If _ExportFile Then
        My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Environment.NewLine & Common.SystemDateTime.ToString & ": Calling ExportDataToXmlFile", True)
        _TransferDataObject.ExportDataToXmlFile(_CompanyID, _UserID, _FromDate, _ToDate, False, _TargetConnectionStringPara, Server.MapPath("FtpFiles\"))
      End If
      My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Common.SystemDateTime.ToString & ": ********** Web Service Ended ***********", True)

    Catch ex As Exception
      My.Computer.FileSystem.WriteAllText(Server.MapPath(DATA_TRANSFER_LOG_FILE), Environment.NewLine & Common.SystemDateTime.ToString & ": exception text=" & ex.Message, True)
      Throw ex
    End Try
  End Function

  ''Author: Faisal Saleem
  ''Date Created(DD-MMM-YY): 01-Apr-10
  ''***** Modification History *****
  ''                 Date      Description
  ''Name          (DD-MMM-YY) 
  ''--------------------------------------------------------------------------------
  ''
  '''' <summary>
  '''' Nothing
  '''' </summary>
  'Public Function ExportDataToXmlFile() As Boolean
  '  Try
  '    Dim _TransferDataObject As New TransferData

  '  Catch ex As Exception
  '    Throw ex
  '  End Try
  'End Function
End Class
