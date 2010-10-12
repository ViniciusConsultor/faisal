Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports QuickLibrary
Imports QuickDalLibrary

Public Class CrystalReportViewerForm
#Region "Declaration"
  Private _ReportDocument As ReportDocument
  Private _ParameterValues As New System.Collections.Specialized.NameValueCollection
  Private _FormulaValues As New System.Collections.Specialized.NameValueCollection
  Private _RecordSelectionFormula As String
#End Region

  Public Shared Sub SetCurrentValuesForParameterField(ByRef myReportDocument As ReportDocument, ByVal myStringDictionary As System.Collections.Specialized.NameValueCollection)
    Try
      Dim currentParameterValues As ParameterValues = New ParameterValues()
      Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields

      For Each myParameterFieldDefinition As ParameterFieldDefinition In myParameterFieldDefinitions
        Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
        myParameterDiscreteValue.Value = myStringDictionary(myParameterFieldDefinition.Name)
        currentParameterValues.Add(myParameterDiscreteValue)

        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
      Next

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in setting report parameters.", ex)
      Throw _ExceptionObject
    End Try
  End Sub

  Public Shared Sub SetValuesForFormulaFields(ByRef myReportDocument As ReportDocument, ByVal myStringDictionary As System.Collections.Specialized.NameValueCollection)
    Try
      'Dim _myFormulaValues As ParameterValues = New ParameterValues()
      Dim _FormulaFieldDefinitions As FormulaFieldDefinitions = myReportDocument.DataDefinition.FormulaFields

      'For I As Int32 = 0 To myStringDictionary.Count - 1
      'Next

      For Each _FormulaFieldDefinition As FormulaFieldDefinition In _FormulaFieldDefinitions
        'Formula Field collection contains other fields as well.
        'We need to check if the value for a formula field is added in the collection.
        If myStringDictionary(_FormulaFieldDefinition.Name) IsNot Nothing Then
          _FormulaFieldDefinition.Text = "'" & myStringDictionary(_FormulaFieldDefinition.Name) & "'"
        End If
      Next

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in SetValuesForFormulaFields(ReportDocument,System.Collections.Specialized.NameValueCollection)", ex)
      Throw _ExceptionObject
    End Try
  End Sub

  Public Property Report() As ReportDocument
    Get
      Return _ReportDocument
    End Get
    Set(ByVal value As ReportDocument)
      Try
        _ReportDocument = value
        QuickDALLibrary.General.SetDBLogonForReport(_ReportDocument)
        SetCurrentValuesForParameterField(_ReportDocument, _ParameterValues)
        SetValuesForFormulaFields(_ReportDocument, _FormulaValues)
        If _ReportDocument.RecordSelectionFormula = String.Empty AndAlso RecordSelectionFormula = String.Empty Then
          _ReportDocument.RecordSelectionFormula = String.Empty
        ElseIf _ReportDocument.RecordSelectionFormula <> String.Empty AndAlso RecordSelectionFormula = String.Empty Then
          'Nothing to do, leave what exists in the selection formula of the report.
        ElseIf _ReportDocument.RecordSelectionFormula = String.Empty AndAlso RecordSelectionFormula <> String.Empty Then
          _ReportDocument.RecordSelectionFormula = RecordSelectionFormula
        Else
          _ReportDocument.RecordSelectionFormula = _ReportDocument.RecordSelectionFormula & " AND (" & RecordSelectionFormula & ")"
        End If
        Me.CrystalReportViewerObject.ReportSource = _ReportDocument
      Catch ex As Exception
        Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in setting report document object", ex)
        Throw _ExceptionObject
      End Try
    End Set
  End Property

  Public Property ParameterValues() As System.Collections.Specialized.NameValueCollection
    Get
      Return _ParameterValues
    End Get
    Set(ByVal value As System.Collections.Specialized.NameValueCollection)
      _ParameterValues = value
    End Set
  End Property

  Public Property FormulaValues() As System.Collections.Specialized.NameValueCollection
    Get
      Return _FormulaValues
    End Get
    Set(ByVal value As System.Collections.Specialized.NameValueCollection)
      _FormulaValues = value
    End Set
  End Property

  Public Property RecordSelectionFormula() As String
    Get
      Return _RecordSelectionFormula
    End Get
    Set(ByVal value As String)
      _RecordSelectionFormula = value
    End Set
  End Property

  Private Sub CrystalReportViewerForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    e.Cancel = True
    Me.Hide()
  End Sub
End Class