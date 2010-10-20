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


'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2009
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'
''' <summary>
''' Report Criteria Form.
''' </summary>
Public Class ReportCriteriaForm

#Region "Declarations"

  Private Const ReportNameStock As String = "Stock"
  Private Const ReportNameStockWithValue As String = "Stock (Value)"
  Private Const ReportNameDailyStock As String = "Daily Stock"
  Private Const ReportNameItemLedger As String = "Item Ledger"
  Private Const ReportNameItemLedgerWithValue As String = "Item Ledger (Value)"
  Private Const ReportNameDailyReport As String = "Daily Report"
  Private Const ReportNamePOBalance As String = "Purchase Order Balance"
  Private Const ReportNamePartyLedgerDetailWithAging As String = "Party Ledger (Aging Detail)"
  Private Const ReportNameStockInSummary As String = "Stock In Summary"
  Private Const ReportNameStockOutSummary As String = "Stock Out Summary"
  Private Const ReportNameStockInDetail As String = "Stock In Detail"
  Private Const ReportNameStockOutDetail As String = "Stock Out Detail"

  Dim _ReportViewerForm As New CrystalReportViewerForm
  Dim _ReportDocument As ReportDocument = Nothing
  Dim _ParameterValues As New System.Collections.Specialized.NameValueCollection
  Dim _SelectionFormula As String = String.Empty
  Dim _SelectionFormulaForDisplay As String = String.Empty

#End Region

#Region "Properties"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 13-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This property returns the name of the report which is currently selected.
  ''' </summary>
  Private ReadOnly Property GetSelectedReportName() As String
    Get
      Try

        If Quick_UltraTree1.SelectedNodes.Count > 0 Then
          Return Quick_UltraTree1.SelectedNodes(0).Text
        Else
          Return String.Empty
        End If

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in GetSelectedReportName of ReportCriteriaForm.", ex)
        Throw _qex
      End Try
    End Get
  End Property


#End Region

#Region "Methods"

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ToolbarMode = ToolbarModes.ReportCriteria
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will return stock without value report and stock with value report.
  ''' </summary>
  Private Sub StockReportWithAndWithoutValue()
    Try
      _ReportDocument = New StockReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4

      _SelectionFormula = "{spGetInventoryDetails;1.Co_ID}=" _
        & CompanyComboBox.CompanyID.ToString
      _SelectionFormula &= " AND {spGetInventoryDetails;1.Warehouse_ID} = " & Me.WarehouseComboBox.WarehouseID.ToString
      _SelectionFormulaForDisplay = "Date Range: "

      If Not WithoutDateCheckBox.Checked Then
        If Me.DateFromCalendarCombo.Value IsNot Nothing Then
          _SelectionFormula &= " AND {spGetInventoryDetails;1.Inventory_Date} >= #" & Me.DateFromCalendarCombo.Value.ToString & "#"
          _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
        Else
          _SelectionFormulaForDisplay &= "Start"
        End If
        If Me.DateToCalendarCombo.Value IsNot Nothing Then
          _SelectionFormula &= " AND {spGetInventoryDetails;1.Inventory_Date} <= #" & Me.DateToCalendarCombo.Value.ToString & "#"
          _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
        Else
          _SelectionFormulaForDisplay &= "to End"
        End If
      Else
        _SelectionFormulaForDisplay &= "All"
      End If

      _SelectionFormulaForDisplay &= vbTab & " Item Range: "

      If Not AllItemsCheckBox.Checked Then
        If Me.ItemFromMultiComboBox.Text <> String.Empty Then
          _SelectionFormula &= " AND {spGetInventoryDetails;1.Item_Code} >= """ & Me.ItemFromMultiComboBox.Text & """"
          _SelectionFormulaForDisplay &= Me.ItemFromMultiComboBox.Text
        Else
          _SelectionFormulaForDisplay &= "Start"
        End If
        If Me.ItemToMultiComboBox.Text <> String.Empty Then
          _SelectionFormula &= " AND {spGetInventoryDetails;1.Item_Code} <= """ & Me.ItemToMultiComboBox.Text & """"
          _SelectionFormulaForDisplay &= " to " & Me.ItemToMultiComboBox.Text
        Else
          _SelectionFormulaForDisplay &= " to End"
        End If
      Else
        _SelectionFormulaForDisplay &= "All"
      End If
      _ParameterValues.Add("ReportName", "Stock")
      _ParameterValues.Add("SelectionFormula", _SelectionFormulaForDisplay)
      If GetSelectedReportName = ReportNameStock Then
        _ParameterValues.Add("WithValue", "False")
      Else
        _ParameterValues.Add("WithValue", "True")
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in StockReportWithAndWithoutValue of ReportCriteria.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method sets properties for Daily Stock report.
  ''' </summary>
  Private Sub DailyStockReport()
    Try
      _SelectionFormulaForDisplay = "Date Range: "

      _ReportDocument = New DailyStockReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateFromCalendarCombo.Value Is DBNull.Value Then
        _ParameterValues.Add("@From_Date", "#" & Date.MinValue.ToString & "#")
        _SelectionFormulaForDisplay &= "Start"
      Else
        _ParameterValues.Add("@From_Date", "" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateFromCalendarCombo.Value, DateTime), False) & "")
        _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateToCalendarCombo.Value Is DBNull.Value Then
        _ParameterValues.Add("@To_Date", Date.MaxValue.ToString)
        _SelectionFormulaForDisplay &= " to End"
      Else
        _ParameterValues.Add("@To_Date", "" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, DateTime), True) & "")
        _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      _ParameterValues.Add("@Co_ID", CompanyComboBox.CompanyID.ToString)
      _ParameterValues.Add("ReportName", "Daily Stock")
      _ParameterValues.Add("SelectionFormula", _SelectionFormulaForDisplay)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in DailyStockReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Zakee 
  'Date Created(DD-MMM-YY): 15-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It calls stock in summary report.
  ''' </summary>
  Private Sub StockInSummaryReport()
    Try
      _ReportDocument = New StockInSummaryReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize

      _SelectionFormulaForDisplay = "Date Range: "

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateFromCalendarCombo.Value Is DBNull.Value Then
        _ParameterValues.Add("@From_Date", "#" & Format(Date.MinValue.Date, "yyyy-MM-dd") & "#")
        ' _ParameterValues.Add("@From_Date", "#" & Date.MinValue.Date & "#")
        _SelectionFormulaForDisplay &= "Start"
      Else
        '_ParameterValues.Add("@From_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateFromCalendarCombo.Value, DateTime), False) & "#")
        _ParameterValues.Add("@From_Date", "#" & Format(Me.DateFromCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateToCalendarCombo.Value Is DBNull.Value Then
        '_ParameterValues.Add("@To_Date", "#" & Date.MaxValue.Date & "#")
        _ParameterValues.Add("@To_Date", Format(Date.MaxValue.Date, "yyyy-MM-dd"))
        _SelectionFormulaForDisplay &= " to End"
      Else
        '_ParameterValues.Add("@To_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, DateTime), True) & "#")
        _ParameterValues.Add("@To_Date", "#" & Format(Me.DateToCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.AllItemsCheckBox.Checked Then
        _ParameterValues.Add("@From_Item", "")
        _ParameterValues.Add("@To_Item", "")
      Else
        _ParameterValues.Add("@From_Item", Me.ItemFromMultiComboBox.Text)
        _ParameterValues.Add("@To_Item", Me.ItemToMultiComboBox.Text)
      End If

      'If Me.AllPartiesCheckBox.Checked Then
      '  _ParameterValues.Add("@From_Party", "")
      '  _ParameterValues.Add("@To_Party", "")
      'Else
      '  _ParameterValues.Add("@From_Party", Me.PartyFromComboBox.Value.ToString)
      '  _ParameterValues.Add("@To_Item", Me.PartyFromComboBox.Value.ToString)
      'End If
      _ParameterValues.Add("@From_Party", "")
      _ParameterValues.Add("@To_Party", "")

      _ParameterValues.Add("@Co_ID", CompanyComboBox.CompanyID.ToString)
      _ParameterValues.Add("SelectionFormula", _SelectionFormulaForDisplay)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in StockInSummaryReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Zakee 
  'Date Created(DD-MMM-YY): 20-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It calls Stock Out summary report.
  ''' </summary>
  Private Sub StockOutSummaryReport()
    Try
      _ReportDocument = New StockOutSummaryReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize

      _SelectionFormulaForDisplay = "Date Range: "

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateFromCalendarCombo.Value Is DBNull.Value Then
        _ParameterValues.Add("@From_Date", "#" & Format(Date.MinValue.Date, "yyyy-MM-dd") & "#")
        ' _ParameterValues.Add("@From_Date", "#" & Date.MinValue.Date & "#")
        _SelectionFormulaForDisplay &= "Start"
      Else
        '_ParameterValues.Add("@From_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateFromCalendarCombo.Value, DateTime), False) & "#")
        _ParameterValues.Add("@From_Date", "#" & Format(Me.DateFromCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateToCalendarCombo.Value Is DBNull.Value Then
        '_ParameterValues.Add("@To_Date", "#" & Date.MaxValue.Date & "#")
        _ParameterValues.Add("@To_Date", Format(Date.MaxValue.Date, "yyyy-MM-dd"))
        _SelectionFormulaForDisplay &= " to End"
      Else
        '_ParameterValues.Add("@To_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, DateTime), True) & "#")
        _ParameterValues.Add("@To_Date", "#" & Format(Me.DateToCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.AllItemsCheckBox.Checked Then
        _ParameterValues.Add("@From_Item", "")
        _ParameterValues.Add("@To_Item", "")
      Else
        _ParameterValues.Add("@From_Item", Me.ItemFromMultiComboBox.Text)
        _ParameterValues.Add("@To_Item", Me.ItemToMultiComboBox.Text)
      End If

      'If Me.AllPartiesCheckBox.Checked Then
      '  _ParameterValues.Add("@From_Party", "")
      '  _ParameterValues.Add("@To_Party", "")
      'Else
      '  _ParameterValues.Add("@From_Party", Me.PartyFromComboBox.Value.ToString)
      '  _ParameterValues.Add("@To_Item", Me.PartyFromComboBox.Value.ToString)
      'End If
      _ParameterValues.Add("@From_Party", "")
      _ParameterValues.Add("@To_Party", "")

      _ParameterValues.Add("@Co_ID", CompanyComboBox.CompanyID.ToString)
      _ParameterValues.Add("SelectionFormula", _SelectionFormulaForDisplay)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in StockOutSummaryReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Zakee 
  'Date Created(DD-MMM-YY): 20-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It calls Stock In Detail report.
  ''' </summary>
  Private Sub StockInDetailReport()
    Try
      _ReportDocument = New StockInDetailReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize

      _SelectionFormulaForDisplay = "Date Range: "

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateFromCalendarCombo.Value Is DBNull.Value Then
        _ParameterValues.Add("@From_Date", "#" & Format(Date.MinValue.Date, "yyyy-MM-dd") & "#")
        ' _ParameterValues.Add("@From_Date", "#" & Date.MinValue.Date & "#")
        _SelectionFormulaForDisplay &= "Start"
      Else
        '_ParameterValues.Add("@From_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateFromCalendarCombo.Value, DateTime), False) & "#")
        _ParameterValues.Add("@From_Date", "#" & Format(Me.DateFromCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateToCalendarCombo.Value Is DBNull.Value Then
        '_ParameterValues.Add("@To_Date", "#" & Date.MaxValue.Date & "#")
        _ParameterValues.Add("@To_Date", Format(Date.MaxValue.Date, "yyyy-MM-dd"))
        _SelectionFormulaForDisplay &= " to End"
      Else
        '_ParameterValues.Add("@To_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, DateTime), True) & "#")
        _ParameterValues.Add("@To_Date", "#" & Format(Me.DateToCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.AllItemsCheckBox.Checked Then
        _ParameterValues.Add("@From_Item", "")
        _ParameterValues.Add("@To_Item", "")
      Else
        _ParameterValues.Add("@From_Item", Me.ItemFromMultiComboBox.Text)
        _ParameterValues.Add("@To_Item", Me.ItemToMultiComboBox.Text)
      End If

      'If Me.AllPartiesCheckBox.Checked Then
      '  _ParameterValues.Add("@From_Party", "")
      '  _ParameterValues.Add("@To_Party", "")
      'Else
      '  _ParameterValues.Add("@From_Party", Me.PartyFromComboBox.Value.ToString)
      '  _ParameterValues.Add("@To_Item", Me.PartyFromComboBox.Value.ToString)
      'End If
      _ParameterValues.Add("@From_Party", "")
      _ParameterValues.Add("@To_Party", "")

      _ParameterValues.Add("@Co_ID", CompanyComboBox.CompanyID.ToString)
      _ParameterValues.Add("SelectionFormula", _SelectionFormulaForDisplay)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in StockOutSummaryReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Zakee 
  'Date Created(DD-MMM-YY): 20-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It calls Stock Out Detail report.
  ''' </summary>
  Private Sub StockOutDetailReport()
    Try
      _ReportDocument = New StockOutDetailReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.DefaultPaperSize

      _SelectionFormulaForDisplay = "Date Range: "

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateFromCalendarCombo.Value Is DBNull.Value Then
        _ParameterValues.Add("@From_Date", "#" & Format(Date.MinValue.Date, "yyyy-MM-dd") & "#")
        ' _ParameterValues.Add("@From_Date", "#" & Date.MinValue.Date & "#")
        _SelectionFormulaForDisplay &= "Start"
      Else
        '_ParameterValues.Add("@From_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateFromCalendarCombo.Value, DateTime), False) & "#")
        _ParameterValues.Add("@From_Date", "#" & Format(Me.DateFromCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.WithoutDateCheckBox.Checked OrElse Me.DateToCalendarCombo.Value Is DBNull.Value Then
        '_ParameterValues.Add("@To_Date", "#" & Date.MaxValue.Date & "#")
        _ParameterValues.Add("@To_Date", Format(Date.MaxValue.Date, "yyyy-MM-dd"))
        _SelectionFormulaForDisplay &= " to End"
      Else
        '_ParameterValues.Add("@To_Date", "#" & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, DateTime), True) & "#")
        _ParameterValues.Add("@To_Date", "#" & Format(Me.DateToCalendarCombo.Value, "yyyy-MM-dd") & "#")
        _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
      End If

      If Me.AllItemsCheckBox.Checked Then
        _ParameterValues.Add("@From_Item", "")
        _ParameterValues.Add("@To_Item", "")
      Else
        _ParameterValues.Add("@From_Item", Me.ItemFromMultiComboBox.Text)
        _ParameterValues.Add("@To_Item", Me.ItemToMultiComboBox.Text)
      End If

      'If Me.AllPartiesCheckBox.Checked Then
      '  _ParameterValues.Add("@From_Party", "")
      '  _ParameterValues.Add("@To_Party", "")
      'Else
      '  _ParameterValues.Add("@From_Party", Me.PartyFromComboBox.Value.ToString)
      '  _ParameterValues.Add("@To_Item", Me.PartyFromComboBox.Value.ToString)
      'End If
      _ParameterValues.Add("@From_Party", "")
      _ParameterValues.Add("@To_Party", "")

      _ParameterValues.Add("@Co_ID", CompanyComboBox.CompanyID.ToString)
      _ParameterValues.Add("SelectionFormula", _SelectionFormulaForDisplay)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in StockOutSummaryReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Item Ledger reports with and without value.
  ''' </summary>
  Private Sub ItemLedgerReportWithAndWithoutValue()
    Try
      _ReportDocument = New ItemLedgerReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4

      _SelectionFormula = "{spGetInventoryDetails;1.Co_ID}=" _
        & CompanyComboBox.CompanyID.ToString
      _SelectionFormula &= " AND {spGetInventoryDetails;1.Warehouse_ID} = " & Me.WarehouseComboBox.WarehouseID.ToString
      _SelectionFormulaForDisplay = "Date Range: "

      If Not WithoutDateCheckBox.Checked Then
        If Not WithoutDateCheckBox.Checked Then
          If Me.DateFromCalendarCombo.Value IsNot Nothing Then
            _SelectionFormula &= " AND {spGetInventoryDetails;1.Inventory_Date} >= #" & Me.DateFromCalendarCombo.Value.ToString & "#"
            _SelectionFormulaForDisplay &= Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
          Else
            _SelectionFormulaForDisplay &= "Start"
          End If
          If Me.DateToCalendarCombo.Value IsNot Nothing Then
            _SelectionFormula &= " AND {spGetInventoryDetails;1.Inventory_Date} <= #" _
              & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, Date), True) & "#"
            _SelectionFormulaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay)
          Else
            _SelectionFormulaForDisplay &= "to End"
          End If
        Else
          _SelectionFormulaForDisplay &= "All"
        End If
      End If

      _SelectionFormulaForDisplay &= vbTab & " Item Range: "

      If Not AllItemsCheckBox.Checked Then
        If Me.ItemFromMultiComboBox.Text <> String.Empty Then
          _SelectionFormula &= " AND {spGetInventoryDetails;1.Item_Code} >= """ & Me.ItemFromMultiComboBox.Text & """"
          _SelectionFormulaForDisplay &= Me.ItemFromMultiComboBox.Text
        Else
          _SelectionFormulaForDisplay &= "Start"
        End If
        If Me.ItemToMultiComboBox.Text <> String.Empty Then
          _SelectionFormula &= " AND {spGetInventoryDetails;1.Item_Code} <= """ & Me.ItemToMultiComboBox.Text & """"
          _SelectionFormulaForDisplay &= " to " & Me.ItemToMultiComboBox.Text
        Else
          _SelectionFormulaForDisplay &= " to End"
        End If
      Else
        _SelectionFormulaForDisplay &= "All"
      End If
      _ParameterValues.Add("ReportName", "Item Ledger")
      _ParameterValues.Add("SelectionFormula", "Date Range: " _
        & Format(Me.DateFromCalendarCombo.Value, General.FormatDateForDisplay) & " to " _
        & Format(Me.DateToCalendarCombo.Value, General.FormatDateForDisplay) & "    " _
        & "Item Range: " & Me.ItemFromMultiComboBox.Text & " to " & Me.ItemToMultiComboBox.Text)
      If GetSelectedReportName = ReportNamePOBalance Then
        _ParameterValues.Add("WithValue", "False")
      Else
        _ParameterValues.Add("WithValue", "True")
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ItemLedgerReportWithAndWithoutValue of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 25-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Daily Report
  ''' </summary>
  Private Sub DailyReport()
    Try
      '<<<<<<<<<< Daily Report Code >>>>>>>>>>
      Dim _ReportCriteriaForDisplay As String = String.Empty

      _ReportDocument = New CashFlowReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.PaperEnvelope9

      _SelectionFormula = "{spGetAccountsDetails;1.Co_ID}=" _
        & LoginInfoObject.CompanyID.ToString
      If Not WithoutDateCheckBox.Checked Then
        If Me.DateFromCalendarCombo.Value IsNot Nothing Then
          _SelectionFormula &= " AND {spGetAccountsDetails;1.Voucher_Date} >= #" _
            & Format(DirectCast(Me.DateFromCalendarCombo.Value, DateTime), "MM-dd-yyyy") & "#"
          _ReportCriteriaForDisplay = Format(Me.DateFromCalendarCombo.Value, General.FormatDateForDisplay)
        Else
          _ReportCriteriaForDisplay = "Start"
        End If
        If Me.DateToCalendarCombo.Value IsNot Nothing Then
          _SelectionFormula &= " AND {spGetAccountsDetails;1.Voucher_Date} <= #" _
            & QuickFunctions.GetDateTimeForReportCriteria1(DirectCast(Me.DateToCalendarCombo.Value, DateTime), True) & "#"
          _ReportCriteriaForDisplay &= " to " & Format(Me.DateToCalendarCombo.Value, General.FormatDateForDisplay)
        Else
          _ReportCriteriaForDisplay &= " End"
        End If
        _ParameterValues.Add("SelectionFormula", "Date Range: " & _ReportCriteriaForDisplay)
      Else
        _ParameterValues.Add("SelectionFormula", "All Records")
      End If
      _ParameterValues.Add("ReportName", "Daily Report")

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in DailyReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' PO Balance report.
  ''' </summary>
  Private Sub POBalanceReport()
    Try
      _ReportDocument = New PurchaseOrderBalanceReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4

      _SelectionFormula = "{spGetInventoryDetails;1.Co_ID}=" _
        & LoginInfoObject.CompanyID.ToString & _
        " AND ({spGetInventoryDetails;1.DocumentType_ID}=" & enuDocumentType.Purchase & _
        " OR {spGetInventoryDetails;1.DocumentType_ID}=" & enuDocumentType.PurchaseReturn & _
        " OR {spGetInventoryDetails;1.DocumentType_ID}=" & enuDocumentType.PurchaseOrder & ")"

      If Not WithoutDateCheckBox.Checked Then
        _SelectionFormula &= " AND {spGetInventoryDetails;1.Inventory_Date} >= #" & Me.DateFromCalendarCombo.Value.ToString & "#" _
        & " AND {spGetInventoryDetails;1.Inventory_Date} <= #" & Me.DateToCalendarCombo.Value.ToString & "#"
      End If
      If Not AllItemsCheckBox.Checked Then
        _SelectionFormula &= " AND {spGetInventoryDetails;1.Item_Code} >= """ & Me.ItemFromMultiComboBox.Text & """" _
        & " AND {spGetInventoryDetails;1.Item_Code} <= """ & Me.ItemToMultiComboBox.Text & """"
      End If
      If Not AllPartiesCheckBox.Checked Then
        _SelectionFormula &= " AND {spGetInventoryDetails;1.Party_ID} >= " & Me.PartyFromComboBox.PartyID & _
        " AND {spGetInventoryDetails;1.Party_ID} <= " & Me.PartyToComboBox.PartyID
      End If
      _ParameterValues.Add("ReportName", "PO Balance")
      _ParameterValues.Add("SelectionFormula", "Date Range: " _
        & Format(Me.DateFromCalendarCombo.Value, General.FormatDateForDisplay) & " to " _
        & Format(Me.DateToCalendarCombo.Value, General.FormatDateForDisplay) & "    " _
        & "Item Range: " & Me.ItemFromMultiComboBox.Text & " to " & Me.ItemToMultiComboBox.Text)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in POBalanceReport of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Party Ledger Detail with aging
  ''' </summary>
  Private Sub PartyLedgerDetailWithAging()
    Try
      '<<<<<<<<<< Party ledger with aging >>>>>>>>>>
      _ReportDocument = New PartyLedgerWithAgingReport
      _ReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4

      _ParameterValues.Add("@Co_ID", CompanyComboBox.CompanyID.ToString)
      _ParameterValues.Add("@AgingCalculationDate", Now.Date.ToString)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in PartyLedgerDetailWithAging of ReportCriteriaForm.", ex)
      Throw _qex
    End Try
  End Sub



#End Region

#Region "Event Methods"

  Protected Overrides Sub PrintPreviewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try

      Cursor = Cursors.WaitCursor
      _SelectionFormula = String.Empty
      _ParameterValues.Clear()

      _ParameterValues.Add("CurrentCompany", Me.CompanyComboBox.Text)

      Select Case GetSelectedReportName
        Case ReportNameStock
          StockReportWithAndWithoutValue()

        Case ReportNameStockWithValue
          StockReportWithAndWithoutValue()

        Case ReportNameDailyStock
          DailyStockReport()

        Case ReportNameStockInSummary
          StockInSummaryReport()

        Case ReportNameStockOutSummary
          StockOutSummaryReport()

        Case ReportNameStockInDetail
          StockInDetailReport()

        Case ReportNameStockOutDetail
          StockOutDetailReport()

        Case ReportNameItemLedger, ReportNameItemLedgerWithValue
          ItemLedgerReportWithAndWithoutValue()

        Case ReportNameDailyReport
          DailyReport()

        Case ReportNamePOBalance
          POBalanceReport()

        Case ReportNamePartyLedgerDetailWithAging
          PartyLedgerDetailWithAging()

        Case Else
          QuickMessageBox.Show(Me.LoginInfoObject, "This option is not available yet", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      End Select

      If _ReportDocument IsNot Nothing Then
        QuickDALLibrary.General.SetDBLogonForReport(_ReportDocument)
        SetCurrentValuesForParameterField(_ReportDocument, _ParameterValues)
        If _ReportDocument.RecordSelectionFormula = String.Empty Then
          _ReportDocument.RecordSelectionFormula = _ReportDocument.RecordSelectionFormula & "" & _SelectionFormula & ""
        Else
          _ReportDocument.RecordSelectionFormula = _ReportDocument.RecordSelectionFormula & " AND (" & _SelectionFormula & ")"
        End If
        _ReportViewerForm.CrystalReportViewerObject.ReportSource = _ReportDocument
        _ReportViewerForm.WindowState = FormWindowState.Maximized
        _ReportViewerForm.Show()
      End If

      MyBase.PrintPreviewButtonClick(sender, e)

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in showing report preview.", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub ReportCriteriaForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim _Reports As New ArrayList

      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID

      Quick_UltraTree1.Override.ActiveNodeAppearance.BackColor = Color.LightBlue
      Quick_UltraTree1.Override.ActiveNodeAppearance.ForeColor = Color.Black
      Quick_UltraTree1.Nodes.Add("Inventory", "Inventory")
      Quick_UltraTree1.Nodes("Inventory").Nodes.Add("StockReports", "Stock")
      Quick_UltraTree1.GetNodeByKey("StockReports").Nodes.Add(ReportNameStock)
      Quick_UltraTree1.GetNodeByKey("StockReports").Nodes.Add(ReportNameStockWithValue)
      Quick_UltraTree1.GetNodeByKey("StockReports").Nodes.Add(ReportNameStockInSummary)
      Quick_UltraTree1.GetNodeByKey("StockReports").Nodes.Add(ReportNameStockOutSummary)
      Quick_UltraTree1.GetNodeByKey("StockReports").Nodes.Add(ReportNameStockInDetail)
      Quick_UltraTree1.GetNodeByKey("StockReports").Nodes.Add(ReportNameStockOutDetail)
      Quick_UltraTree1.Nodes("Inventory").Nodes.Add("ItemLedger", "Item Ledger")
      Quick_UltraTree1.GetNodeByKey("ItemLedger").Nodes.Add(ReportNameItemLedger)
      Quick_UltraTree1.GetNodeByKey("ItemLedger").Nodes.Add(ReportNameItemLedgerWithValue)
      Quick_UltraTree1.Nodes.Add("Accounts", "Accounts")
      Quick_UltraTree1.GetNodeByKey("Accounts").Nodes.Add(ReportNameDailyStock)
      Quick_UltraTree1.GetNodeByKey("Accounts").Nodes.Add(ReportNameDailyReport)
      Quick_UltraTree1.GetNodeByKey("Accounts").Nodes.Add(ReportNamePOBalance)
      Quick_UltraTree1.GetNodeByKey("Accounts").Nodes.Add(ReportNamePartyLedgerDetailWithAging)
      Quick_UltraTree1.ExpandAll()

      Me.DateFromCalendarCombo.Value = Now
      Me.DateToCalendarCombo.Value = Now

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

        If myParameterDiscreteValue.Value Is DBNull.Value Then
          Debug.WriteLine("Setting Parameter Value: " & myParameterFieldDefinition.ParameterFieldName & "NULL")
        Else
          Debug.WriteLine("Setting Parameter Value: " & myParameterFieldDefinition.ParameterFieldName & myParameterDiscreteValue.Value.ToString)
        End If

        currentParameterValues.Add(myParameterDiscreteValue)
        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
      Next
    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in setting report parameters.", ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
  Private Sub AllItemsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllItemsCheckBox.CheckedChanged
    Try
      Me.ItemFromMultiComboBox.Enabled = Not AllItemsCheckBox.Checked
      Me.ItemToMultiComboBox.Enabled = Not AllItemsCheckBox.Checked

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in checked changed event of " & sender.ToString, ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub WithoutDateCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithoutDateCheckBox.CheckedChanged
    Try
      Me.DateFromCalendarCombo.Enabled = Not WithoutDateCheckBox.Checked
      Me.DateToCalendarCombo.Enabled = Not WithoutDateCheckBox.Checked

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in checked changed event of " & sender.ToString, ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
  Private Sub AllPartiesCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllPartiesCheckBox.CheckedChanged
    Try
      Me.PartyFromComboBox.Enabled = Not AllPartiesCheckBox.Checked
      Me.PartyToComboBox.Enabled = Not AllPartiesCheckBox.Checked
    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in checked changed event of " & sender.ToString, ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 14-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event will display controls required for report selected.
  ''' </summary>
  Private Sub Quick_UltraTree1_AfterSelect(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles Quick_UltraTree1.AfterSelect
    Try

      Select Case GetSelectedReportName

        Case ReportNamePOBalance
          Me.AllPartiesCheckBox.Visible = True
          Me.PartyFromComboBox.Visible = True
          Me.PartyToComboBox.Visible = True
          Me.PartyTo.Visible = True
          Me.PartyFrom.Visible = True

        Case Else
          Me.AllPartiesCheckBox.Visible = False
          Me.PartyFromComboBox.Visible = False
          Me.PartyToComboBox.Visible = False
          Me.PartyTo.Visible = False
          Me.PartyFrom.Visible = False

      End Select

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in Combo Close up event of " & sender.ToString, ex)
      _ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

  Private Sub CompanyComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CompanyComboBox.ValueChanged
    Dim _ItemForComboTA As New QuickDAL.QuickInventoryDataSetTableAdapters.ItemTableAdapter
    Dim _ItemForComboDataTable As New QuickDAL.QuickInventoryDataSet.ItemDataTable

    Try
      WarehouseComboBox.LoadWarehouses(Me.CompanyComboBox.CompanyID)
      If WarehouseComboBox.Rows.Count <= 0 Then WarehouseComboBox.Enabled = False
      _ItemForComboDataTable = _ItemForComboTA.GetByCoID(Me.CompanyComboBox.CompanyID)
      Me.ItemFromMultiComboBox.SetComboBoxesOnDataTable(_ItemForComboDataTable, QuickDALLibrary.DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_ItemCode), QuickLibrary.Constants.ITEM_LEVELING_SEPERATOR, _ItemForComboDataTable.Item_CodeColumn.ColumnName, _ItemForComboDataTable.Item_IDColumn.ColumnName)
      Me.ItemToMultiComboBox.SetComboBoxesOnDataTable(_ItemForComboDataTable, QuickDALLibrary.DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_ItemCode), QuickLibrary.Constants.ITEM_LEVELING_SEPERATOR, _ItemForComboDataTable.Item_CodeColumn.ColumnName, _ItemForComboDataTable.Item_IDColumn.ColumnName)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in MethodName of ClassName.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try

  End Sub
End Class