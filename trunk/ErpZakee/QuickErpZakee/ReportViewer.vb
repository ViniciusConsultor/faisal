Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
'Imports CommonClass
Public Class ReportViewerForm
  Public MyReportDocument As ReportDocument
  Public ParameterFields As System.Collections.Specialized.NameValueCollection
  Private ConnectionString As String = "Data Source=.;Security Info=True;Initial Catalog=Quick_ERP"


  Private Sub ReportViewerForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim _LogonInfo As New TableLogOnInfo

      'If Common.uid = 4 Then
      '  Me.CRPViewer.ShowExportButton = False
      'End If

      'Dim myreport As New opening_stock
      '_LogonInfo.ConnectionInfo.DatabaseName = Connection.DBName
      '_LogonInfo.ConnectionInfo.IntegratedSecurity = "false"
      '_LogonInfo.ConnectionInfo.UserID = "sa"
      '   _LogonInfo.ConnectionInfo.Password = "abc"
      '_LogonInfo.ConnectionInfo.ServerName = "."
      '_LogonInfo.ConnectionInfo.AllowCustomConnection = True
      '_LogonInfo.ConnectionInfo.Type = ConnectionInfoType.SQ

      'For Each _ReportTable As CrystalDecisions.CrystalReports.Engine.Table In myReportDocument.Database.Tables
      '  _ReportTable.ApplyLogOnInfo(_LogonInfo)
      'Next
      'myReport.RecordSelectionFormula = SelectionString

      'Me.CrystalReportViewer1.ReportSource = myReport


      SetDBLogonForReport()
      If ParameterFields IsNot Nothing Then SetCurrentValuesForParameterField()
      Me.CrystalReportViewer.ReportSource = MyReportDocument

    Catch ex As Exception
      '  MessageBox.Show("Exception generated against 'MaxIssueNo'" & Chr(13) & ex.Message, "Ideal Soft Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Try


  End Sub

  Public Sub SetCurrentValuesForParameterField()
    Try

      Dim currentParameterValues As ParameterValues = New ParameterValues()
      Dim myParameterFieldDefinitions As ParameterFieldDefinitions = MyReportDocument.DataDefinition.ParameterFields


      For Each myParameterFieldDefinition As ParameterFieldDefinition In myParameterFieldDefinitions
        Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
        myParameterDiscreteValue.Value = ParameterFields(myParameterFieldDefinition.Name)
        currentParameterValues.Add(myParameterDiscreteValue)

        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
      Next

    Catch ex As Exception
      ' MessageBox.Show("Exception generated against 'MaxIssueNo'" & Chr(13) & ex.Message, "Ideal Soft Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Try
  End Sub
  Public Sub SetDBLogonForReport()
    Try
      'Dim _SqlConnectionStringObject As New SqlClient.SqlConnectionStringBuilder(Configuration.ConfigurationManager.AppSettings("connectionstring"))
      Dim _SqlConnectionStringObject As New SqlClient.SqlConnectionStringBuilder(ConnectionString)
      Dim _ReportTables As Tables = myReportDocument.Database.Tables
      Dim _LogonInfo As New TableLogOnInfo

      For Each _ReportTable As CrystalDecisions.CrystalReports.Engine.Table In _ReportTables
        _LogonInfo.ConnectionInfo.DatabaseName = _SqlConnectionStringObject.InitialCatalog
        _LogonInfo.ConnectionInfo.IntegratedSecurity = _SqlConnectionStringObject.IntegratedSecurity
        If Not _SqlConnectionStringObject.IntegratedSecurity Then
          _LogonInfo.ConnectionInfo.UserID = _SqlConnectionStringObject.UserID
          _LogonInfo.ConnectionInfo.Password = _SqlConnectionStringObject.Password
        End If
        _LogonInfo.ConnectionInfo.ServerName = _SqlConnectionStringObject.DataSource
        _LogonInfo.ConnectionInfo.AllowCustomConnection = True
        _LogonInfo.ConnectionInfo.Type = ConnectionInfoType.SQL

        _ReportTable.ApplyLogOnInfo(_LogonInfo)
        _ReportTable.Location = _SqlConnectionStringObject.InitialCatalog & ".dbo." & _
        _ReportTable.Location.Substring(_ReportTable.Location.LastIndexOf(".") + 1)

      Next

    Catch ex As Exception
      '  MessageBox.Show("Exception generated against 'MaxIssueNo'" & Chr(13) & ex.Message, "Ideal Soft Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Try
  End Sub

End Class