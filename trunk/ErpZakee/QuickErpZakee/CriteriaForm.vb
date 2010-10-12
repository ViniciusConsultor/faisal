Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports QuickDAL
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickDalLibrary.DatabaseCache
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports System.Collections



Public Class CriteriaForm

#Region "Declarations"

  Private _ChartofAccountTableAdapterObject As New QuickAccountingDataSetTableAdapters.COATableAdapter
  Private _ChartofAccountDataTable As New QuickAccountingDataSet.COADataTable

#End Region

#Region "Properties"

#End Region

  Private Sub CriteriaForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Me.PopulateCOAComboBox()
    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in loading form", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub SetCurrentValuesForParameterField(ByRef myReportDocument As ReportDocument, ByVal myStringDictionary As System.Collections.Specialized.NameValueCollection)
    Try
      Dim currentParameterValues As ParameterValues = New ParameterValues()
      Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields


      For I As Int32 = 0 To myStringDictionary.Count - 1
      Next

      For Each myParameterFieldDefinition As ParameterFieldDefinition In myParameterFieldDefinitions
        ''
        currentParameterValues = myParameterFieldDefinition.CurrentValues
        ''
        Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()

        'If myParameterFieldDefinition.ValueType.ToString() Is System.Type.GetType("System.DateTime") Then
        '  myParameterDiscreteValue.Value = Convert.ToDateTime(myStringDictionary(myParameterFieldDefinition.Name))
        'ElseIf myParameterFieldDefinition.ValueType.ToString() Is System.Type.GetType("System.Int16") Then
        '  myParameterDiscreteValue.Value = Convert.ToInt16(myStringDictionary(myParameterFieldDefinition.Name))
        'ElseIf myParameterFieldDefinition.ValueType.ToString() Is System.Type.GetType("System.Int32") Then
        '  myParameterDiscreteValue.Value = Convert.ToInt32(myStringDictionary(myParameterFieldDefinition.Name))
        'Else
        '  myParameterDiscreteValue.Value = myStringDictionary(myParameterFieldDefinition.Name)
        'End If

        Select Case myParameterFieldDefinition.ValueType.ToString()
          Case "DateTimeField"
            myParameterDiscreteValue.Value = Convert.ToDateTime(myStringDictionary(myParameterFieldDefinition.Name))
          Case "NumberField"
            myParameterDiscreteValue.Value = Convert.ToInt16(myStringDictionary(myParameterFieldDefinition.Name))
          Case Else
            myParameterDiscreteValue.Value = myStringDictionary(myParameterFieldDefinition.Name)
        End Select

        currentParameterValues.Add(myParameterDiscreteValue)
        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
      Next
      Me.PopulateCOAComboBox()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the populate Voucher Type", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally

    End Try


  End Sub


#Region "Methods"

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub


  Private Function PopulateCOAComboBox() As Boolean
    Try
      Dim _VoucherTypeTable As New QuickAccountingDataSet.VoucherTypeDataTable
      Me.ChartofAccountComboBox.DataSource = Me._ChartofAccountTableAdapterObject.GetByCoID(5)
      ChartofAccountComboBox.ValueMember = Me._ChartofAccountDataTable.COA_IDColumn.ColumnName
      ChartofAccountComboBox.DisplayMember = Me._ChartofAccountDataTable.COA_DescColumn.ColumnName

      With ChartofAccountComboBox.DisplayLayout.Bands(0)
        For i As Int32 = 0 To .Columns.Count - 1
          If .Columns(_ChartofAccountDataTable.COA_DescColumn.ColumnName).Index <> .Columns(i).Index Then
            ChartofAccountComboBox.DisplayLayout.Bands(0).Columns(i).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the populate Voucher Type", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally

    End Try
  End Function


 

  

  




#End Region

#Region "Event Methods"
  
  

#End Region


 
  Private Sub ShowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowButton.Click
    Try
      Dim myReportDocument As ReportDocument
      Dim myfrmReportViewer As New ReportViewerForm
      Dim ParameterValues As New System.Collections.Specialized.NameValueCollection

      myReportDocument = New COAActivity
      myfrmReportViewer.MyReportDocument = myReportDocument
      ParameterValues.Add("@Co_ID", CStr(5))

      ParameterValues.Add("@CoA_ID", CStr(Me.ChartofAccountComboBox.Value))

      'ParameterValues.Add("@FromDate", CStr(Me.DateFromCalendarCombo.Value))
      ParameterValues.Add("@FromDate", Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay))
      ParameterValues.Add("@ToDate", Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay))
      'ParameterValues.Add("@FromDate", "2009-07-27")
      'ParameterValues.Add("@ToDate", CStr(Me.DateToCalendarCombo.Value))

      'ParameterValues.Add("@FromDate", Me.DateToCalendarCombo.Value.ToString)


      'If Me.txtid.Text = String.Empty Then
      '  ParameterValues.Add("@cid", CStr(0))
      'Else
      '  ParameterValues.Add("@cid", Me.txtid.Text)
      'End If

      myfrmReportViewer.ParameterFields = ParameterValues

      With myfrmReportViewer.MyReportDocument.DataDefinition
        For I As Int32 = 0 To .FormulaFields.Count - 1
          If .FormulaFields(I).Name = "CompanyName" Then .FormulaFields(I).Text = """" & Me.LoginInfoObject.CompanyDesc & """"
          If .FormulaFields(I).Name = "COADesc" Then .FormulaFields(I).Text = """" & Me.ChartofAccountComboBox.Text & """"
          If .FormulaFields(I).Name = "COACode" Then .FormulaFields(I).Text = """" & Me.ChartofAccountComboBox.SelectedRow.Cells("COA_Code").Text & """"

        Next
      End With

      '  If Common.chk = False Then

      myfrmReportViewer.Show()
      '  Else
      'myfrmReportViewer.SetCurrentValuesForParameterField()
      '   myReportDocument.PrintToPrinter(1, True, 0, 0)
      '    End If

 

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in showing report preview.", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
End Class