Imports QuickDALLibrary
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports System.Windows.Forms


'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2010
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This form is works for different summaries related to items.
''' </summary>
Public Class StockInquiryForm
#Region "Declarations"
  Private _StockInquiryTableAdapter As New StockInquiryTableAdapter
  Private _TextCellLabel As New FarPoint.Win.Spread.CellType.TextCellType
  Private _MinimumStockLevelFilterOptionsTable As New QuickDAL.LogicalDataSet.KeyValuePairDataTable
  Private Flag As Boolean = False
#End Region

#Region "Properties"

#End Region

#Region "Methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 29-Aug-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It shows the stock information.
  ''' </summary>
  Private Sub ShowStockInformation()
    Try
      Dim _StockInquiryTable As QuickDAL.QuickInventoryDataSet.StockInquiryDataTable

      Cursor = Windows.Forms.Cursors.WaitCursor
      Me.StockQuickSpread_Sheet1.Reset()
      Me.StockQuickSpread_Sheet1.DataSource = Nothing
      '_StockInquiryTable = _StockInquiryTableAdapter.GetByItemID(Me.ItemComboBox.ItemCode)
      _StockInquiryTable = _StockInquiryTableAdapter.GetStockByItemCodeCompanies(Me.ItemComboBox.Text, Me.CompanyCheckedListBox1.CheckedKeys, 0, ShowTotalRowsCheckBox.Checked)

      SetGridLayout(Me.StockQuickSpread_Sheet1, _StockInquiryTable)
      _StockInquiryTable.Item_Code1Column.Expression = "substring(" & _StockInquiryTable.Item_CodeColumn.ColumnName & ",1,2)"
      _StockInquiryTable.Item_Code2Column.Expression = "substring(" & _StockInquiryTable.Item_CodeColumn.ColumnName & ",4,2)"

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in click event method of ShowButton of ShowInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try

  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 29-Aug-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It shows the minimum stock level information.
  ''' </summary>
  Private Sub ShowMinimumLevelInformation()
    Try
      Dim _StockInquiryTable As QuickDAL.QuickInventoryDataSet.StockInquiryDataTable
      Dim _ComparisonString As String = String.Empty

      Cursor = Windows.Forms.Cursors.WaitCursor
      Me.MinimumStockLevelSheet.Reset()
      Me.MinimumStockLevelSheet.DataSource = Nothing
      _StockInquiryTable = _StockInquiryTableAdapter.GetMinimumStockLevelByItemCodeCompanies(Me.ItemComboBox.Text, Me.CompanyCheckedListBox1.CheckedKeys, 0, ReverseSignCheckBox.Checked)
      _StockInquiryTable.Item_Code1Column.Expression = "substring(" & _StockInquiryTable.Item_CodeColumn.ColumnName & ",1,2)"
      _StockInquiryTable.Item_Code2Column.Expression = "substring(" & _StockInquiryTable.Item_CodeColumn.ColumnName & ",4,2)"

      If Me.MinimumStockLevelFilterOptionQuickUltraComboBox.SelectedRow IsNot Nothing Then
        Select Case Me.MinimumStockLevelFilterOptionQuickUltraComboBox.SelectedRow.Cells(_MinimumStockLevelFilterOptionsTable.KeyColumn.ColumnName).Text
          Case "0"
            _ComparisonString = String.Empty
          Case "1"
            _ComparisonString = "<>0"
          Case "2"
            _ComparisonString = "<0"
        End Select
      End If

      If _ComparisonString <> String.Empty Then
        With _StockInquiryTable
          _ComparisonString = .Qty_Size01Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size02Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size03Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size04Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size05Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size06Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size07Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size08Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size09Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size10Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size11Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size12Column.ColumnName _
            & _ComparisonString & " OR " & .Qty_Size13Column.ColumnName _
            & _ComparisonString
        End With
      End If
      _StockInquiryTable.DefaultView.RowFilter = _ComparisonString

      SetGridLayout(Me.MinimumStockLevelSheet, _StockInquiryTable)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in click event method of ShowButton of ShowInquiryForm.", ex)
      Throw _qex
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
    End Sub
    'Author: Muhammad Zakee
    'Date Created(DD-MMM-YY): 06-Jan-11
    '***** Modification History *****
    '                 Date      Description
    'Name          (DD-MMM-YY) 
    '--------------------------------------------------------------------------------
    '
    ''' <summary>
    ''' It shows the sales stock level information.
    ''' </summary>
    Private Sub ShowSalesStockLevelInformation()
    Try
      Cursor = Windows.Forms.Cursors.WaitCursor
      Dim _SalesStockInquiryTable As QuickDAL.QuickInventoryDataSet.StockInquiryDataTable

      Me.GetSalesQuickSpread.Reset()
      Me.GetSalesQuickSpread.DataSource = Nothing
      If Me.IncreaseQuantityTextBox.Text = String.Empty Then
        Me.IncreaseQuantityTextBox.Text = (0).ToString
      End If

      If CDate(Me.DateFromCalendarCombo.Value) > CDate(Me.DateToCalendarCombo.Value) Then
        MessageBox.Show("FromDate Calandar box date is not greater then TillDate calandar box", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Exit Sub
      End If
      If Me.Flag = True Then
        _SalesStockInquiryTable = _StockInquiryTableAdapter.GetSalesStockByItemCodeCompanies(Me.CompanyCheckedListBox1.CheckedKeys, CType(Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay), Global.System.Nullable(Of Date)), CType(Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay), Global.System.Nullable(Of Date)), Me.ItemComboBox.Text, Me.IncreaseQuantityTextBox.Text, Me.TotalRowsCheckBox.Checked)
      Else
        _SalesStockInquiryTable = _StockInquiryTableAdapter.GetSalesStockByItemCodeCompanies(Me.CompanyCheckedListBox1.CheckedKeys, CType(Format(Me.DateFromCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay), Global.System.Nullable(Of Date)), CType(Format(Me.DateToCalendarCombo.Value, QuickDALLibrary.General.FormatDateForDisplay), Global.System.Nullable(Of Date)), Me.ItemComboBox.Text, (0).ToString, Me.TotalRowsCheckBox.Checked)
      End If

      _SalesStockInquiryTable.Item_Code1Column.Expression = "substring(" & _SalesStockInquiryTable.Item_CodeColumn.ColumnName & ",1,2)"
      _SalesStockInquiryTable.Item_Code2Column.Expression = "substring(" & _SalesStockInquiryTable.Item_CodeColumn.ColumnName & ",4,2)"

      SetGridLayout(Me.GetSalesQuickSpread_Sheet1, _SalesStockInquiryTable)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in click event method of GetSalesButton of ShowInquiryForm.", ex)
      Throw _qex
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
    End Sub




  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 29-Aug-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will set the layout of the grid for selected tab.
  ''' </summary>
  Private Sub SetGridLayout(ByRef QuickSpreadSheet As QuickBusinessControls.ItemSpreadView, ByVal _StockInquiryTable As QuickDAL.QuickInventoryDataSet.StockInquiryDataTable)
    Try

      General.SetColumnCaptions(DirectCast(_StockInquiryTable, DataTable), Me.Name)
      QuickSpreadSheet.DataSource = _StockInquiryTable.DefaultView

      '<<<<< Set grid captions and column widths >>>>>
      For I As Int32 = 0 To _StockInquiryTable.Columns.Count - 1
        QuickSpreadSheet.Columns(I).Locked = True

        Select Case _StockInquiryTable.Columns(I).ColumnName
          Case _StockInquiryTable.Co_CodeColumn.ColumnName
            QuickSpreadSheet.Columns(I).Width = QuickLibrary.Constants.CO_CODE_CELL_WIDTH
            QuickSpreadSheet.Columns(I).CellType = _TextCellLabel
          Case _StockInquiryTable.Item_CategoryColumn.ColumnName
            QuickSpreadSheet.Columns(I).Width = QuickLibrary.Constants.ITEM_DESC_CELL_WIDTH
            QuickSpreadSheet.Columns(I).CellType = _TextCellLabel
          Case _StockInquiryTable.Item_Code1Column.ColumnName
            QuickSpreadSheet.Columns(I).Width = QuickLibrary.Constants.ITEM_CODECOMPLETE_CELL_WIDTH
            QuickSpreadSheet.Columns(I).CellType = _TextCellLabel
            QuickSpreadSheet.Columns(I).Label = "Cat."
          Case _StockInquiryTable.Item_Code2Column.ColumnName
            QuickSpreadSheet.Columns(I).Width = QuickLibrary.Constants.ITEM_CODECOMPLETE_CELL_WIDTH
            QuickSpreadSheet.Columns(I).CellType = _TextCellLabel
            QuickSpreadSheet.Columns(I).Label = "Item"
          Case _StockInquiryTable.Item_DescColumn.ColumnName
            QuickSpreadSheet.Columns(I).Width = 100
            QuickSpreadSheet.Columns(I).CellType = _TextCellLabel

          Case _StockInquiryTable.Qty_Size01Column.ColumnName, _StockInquiryTable.Qty_Size02Column.ColumnName _
          , _StockInquiryTable.Qty_Size03Column.ColumnName, _StockInquiryTable.Qty_Size04Column.ColumnName _
          , _StockInquiryTable.Qty_Size05Column.ColumnName, _StockInquiryTable.Qty_Size06Column.ColumnName _
          , _StockInquiryTable.Qty_Size07Column.ColumnName, _StockInquiryTable.Qty_Size08Column.ColumnName _
          , _StockInquiryTable.Qty_Size09Column.ColumnName, _StockInquiryTable.Qty_Size10Column.ColumnName _
          , _StockInquiryTable.Qty_Size11Column.ColumnName

            Dim _QtyCellType As FarPoint.Win.Spread.CellType.NumberCellType = QuickLibrary.Common.QtyCellType
            _QtyCellType.NegativeFormat = FarPoint.Win.Spread.CellType.NegativeFormat.Parentheses

            QuickSpreadSheet.Columns(I).Width = QuickLibrary.Constants.QTY_CELL_WIDTH
            QuickSpreadSheet.Columns(I).CellType = _QtyCellType

          Case _StockInquiryTable.Qty_TotalColumn.ColumnName
            Dim _QtyTotalCellType As FarPoint.Win.Spread.CellType.NumberCellType = QuickLibrary.Common.QtyTotalCellType
            _QtyTotalCellType.NegativeFormat = FarPoint.Win.Spread.CellType.NegativeFormat.Parentheses

            QuickSpreadSheet.Columns(I).Width = QuickLibrary.Constants.QTY_TOTAL_CELL_WIDTH
            QuickSpreadSheet.Columns(I).CellType = _QtyTotalCellType

          Case Else
            QuickSpreadSheet.Columns(I).Visible = False
        End Select
      Next

      '<<<<< Set fore color and back color based on quantity >>>>>
      For R As Int32 = 0 To QuickSpreadSheet.RowCount - 1
        For C As Int32 = 0 To _StockInquiryTable.Columns.Count - 1
          'If R = QuickSpreadSheet.RowCount - 1 Then
          'QuickSpreadSheet.Cells(R, C).BackColor = QuickLibrary.Constants.BackColorGrandTotalRow
          If Convert.ToInt32(QuickSpreadSheet.GetValue(R, _StockInquiryTable.Co_IDColumn.Ordinal)) < 0 Then
            QuickSpreadSheet.Rows(R).BackColor = QuickLibrary.Constants.BackColorTotalRow
            QuickSpreadSheet.Rows(R).Border = New FarPoint.Win.LineBorder(Drawing.Color.Black, 1, False, True, False, True)
            Exit For
          Else
            Select Case _StockInquiryTable.Columns(C).ColumnName
              Case _StockInquiryTable.Qty_Size01Column.ColumnName _
                  , _StockInquiryTable.Qty_Size02Column.ColumnName _
                  , _StockInquiryTable.Qty_Size03Column.ColumnName _
                  , _StockInquiryTable.Qty_Size04Column.ColumnName _
                  , _StockInquiryTable.Qty_Size05Column.ColumnName _
                  , _StockInquiryTable.Qty_Size06Column.ColumnName _
                  , _StockInquiryTable.Qty_Size07Column.ColumnName _
                  , _StockInquiryTable.Qty_Size08Column.ColumnName _
                  , _StockInquiryTable.Qty_Size09Column.ColumnName _
                  , _StockInquiryTable.Qty_Size10Column.ColumnName _
                  , _StockInquiryTable.Qty_Size11Column.ColumnName _
                  , _StockInquiryTable.Qty_TotalColumn.ColumnName

                If QuickSpreadSheet.GetValue(R, C) Is Nothing OrElse Convert.ToInt32(QuickSpreadSheet.GetValue(R, C)) = 0 Then
                  QuickSpreadSheet.Cells(R, C).ForeColor = QuickLibrary.Constants.ForeColorQtyZero
                  QuickSpreadSheet.Cells(R, C).BackColor = QuickLibrary.Constants.BackColorQtyZero
                ElseIf Convert.ToInt32(QuickSpreadSheet.GetValue(R, C)) < 0 Then
                  QuickSpreadSheet.Cells(R, C).ForeColor = QuickLibrary.Constants.ForeColorQtyNegative
                  QuickSpreadSheet.Cells(R, C).BackColor = QuickLibrary.Constants.BackColorQtyNegative
                Else
                  QuickSpreadSheet.Cells(R, C).ForeColor = QuickLibrary.Constants.ForeColorQtyPositive
                  QuickSpreadSheet.Cells(R, C).BackColor = QuickLibrary.Constants.BackColorQtyPositive
                End If
            End Select
          End If
        Next C
      Next R

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetGridLayout of StockInquiryForm.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

#Region "Event Methods"
  Private Sub StockInquiryForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    _StockInquiryTableAdapter = Nothing
  End Sub

  Private Sub StockInquiryForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim _ItemForComboTA As New QuickDAL.QuickInventoryDataSetTableAdapters.ItemTableAdapter
      Dim _ItemForComboDataTable As New QuickDAL.QuickInventoryDataSet.ItemDataTable

      _TextCellLabel.ReadOnly = True

      _ItemForComboDataTable = _ItemForComboTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.ItemComboBox.qSetComboBoxesOnDataTable(_ItemForComboDataTable, QuickDALLibrary.DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_ItemCode), QuickLibrary.Constants.ITEM_LEVELING_SEPERATOR, _ItemForComboDataTable.Item_CodeColumn.ColumnName, _ItemForComboDataTable.Item_IDColumn.ColumnName)
      Me.StockQuickSpread_Sheet1.Columns(0).CellType = _TextCellLabel
      Me.CompanyCheckedListBox1.UltraExpandableGroupBox1.Expanded = False
      Me.CompanyCheckedListBox1.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      Me.CompanyCheckedListBox1.SelectAll()

      _MinimumStockLevelFilterOptionsTable.AddKeyValuePairRow("0", "All")
      _MinimumStockLevelFilterOptionsTable.AddKeyValuePairRow("1", "Hide Items with zero quantity")
      _MinimumStockLevelFilterOptionsTable.AddKeyValuePairRow("2", "Display Items with negative quantity")
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DataSource = _MinimumStockLevelFilterOptionsTable
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayMember = _MinimumStockLevelFilterOptionsTable.ValueColumn.ColumnName
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Rows.Band.ColHeadersVisible = False
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Rows.Band.Columns(_MinimumStockLevelFilterOptionsTable.KeyColumn.ColumnName).Hidden = True
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DropDownWidth = Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Width
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Rows.Band.Columns(_MinimumStockLevelFilterOptionsTable.ValueColumn.ColumnName).Width = Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DropDownWidth
      Me.MinimumStockLevelFilterOptionQuickUltraComboBox.SelectedRow = Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Rows(0)

      Me.DateFromCalendarCombo.Value = Now.Date
      Me.DateToCalendarCombo.Value = Now.Date

      ShowButton_Click(sender, e)
      Me.StockQuickSpread.AllowUserZoom = True
      'Me.StockQuickSpread_Sheet1.AllowGroup = True
      'Me.StockQuickSpread_Sheet1.GroupBarVisible = True
      Me.StockQuickSpread.AllowColumnMove = True

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Load event method of StockInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
    End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 29-Aug-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will display information on tab change.
  ''' </summary>
  Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
    Try
      If TabControl1.SelectedTab Is StockTabPage Then
        ShowStockInformation()
      ElseIf TabControl1.SelectedTab Is MinimumLevelDeviationTabPage Then
        ShowMinimumLevelInformation()
      ElseIf TabControl1.SelectedTab Is SalesStockInquiryTabPage Then
        ShowSalesStockLevelInformation()
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in TabControl1_SelectedIndexChanged event method of StockInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 29-Aug-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will show the information based on tab page selected.
  ''' </summary>
  Private Sub ShowButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowButton.Click

    Try
      TabControl1_SelectedIndexChanged(sender, e)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ShowButton_Click event method of StockInquiryForm.", ex)
      Throw _qex
    End Try

  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 05-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will display preview of the current sheet.
  ''' </summary>
  Protected Overrides Sub PrintPreviewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      If Me.TabControl1.SelectedTab Is Me.StockTabPage Then
        Me.StockQuickSpread_Sheet1.PrintInfo.Preview = True
        Me.StockQuickSpread.PrintSheet(0)

      ElseIf Me.TabControl1.SelectedTab Is Me.MinimumLevelDeviationTabPage Then
        Me.MinimumStockLevelSheet.PrintInfo.Preview = True
        Me.MinimumStockLevelQuickSpread.PrintSheet(0)

      End If

      MyBase.PrintPreviewButtonClick(sender, e)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SubName of ClassName/FormName.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Sep-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' ExportToExcelButton click event method.
  ''' </summary>
  Private Sub ExportToExcelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelButton.Click
    Try
      Dim _FileName As String = String.Empty

      Me.SaveFileDialog1.ShowDialog()
      If SaveFileDialog1.FileName <> String.Empty Then
        _FileName = SaveFileDialog1.FileName
        If _FileName.Substring(_FileName.Length - 5).ToLower <> ".xls" Then
          _FileName &= ".xls"
        End If

        If TabControl1.SelectedTab Is StockTabPage Then
          Me.StockQuickSpread.SaveExcel(_FileName, FarPoint.Excel.ExcelSaveFlags.SaveBothCustomRowAndColumnHeaders)

        ElseIf TabControl1.SelectedTab Is MinimumLevelDeviationTabPage Then
          Me.MinimumStockLevelQuickSpread.SaveExcel(_FileName, FarPoint.Excel.ExcelSaveFlags.SaveBothCustomRowAndColumnHeaders)
        End If
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ExportToExcelButton_Click of StockInquiryForm.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Dec-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will recalculate stock.
  ''' </summary>
  Private Sub RecalculateStockButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecalculateStockButton.Click
    Try
      Cursor = Windows.Forms.Cursors.WaitCursor
      _StockInquiryTableAdapter.RecalculateItemSummary(0, 0, QuickLibrary.Constants.enuDocumentType.Stock)
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in RecalculateStockButton_Click of StockInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
  End Sub

  Private Sub ShowSalesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowSalesButton.Click
    Try
      Me.Flag = False
      Me.ShowSalesStockLevelInformation()
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in click event method of ShowButton of ShowInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
  End Sub

  Private Sub IncreaseQuantityButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncreaseQuantityButton.Click
    Try
      Me.Flag = True
      Me.ShowSalesStockLevelInformation()
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in click event method of ShowButton of ShowInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
  End Sub

  Private Sub IncreaseQuantityTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles IncreaseQuantityTextBox.KeyPress
    Try
      If Me.IncreaseQuantityTextBox.Text <> String.Empty Then
        If Not Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 37 Then
          e.Handled = True
        Else
          If (Me.IncreaseQuantityTextBox.Text.Substring(Me.IncreaseQuantityTextBox.Text.Length - 1, 1)) = "%" And Not Asc(e.KeyChar) = 8 Then
            e.Handled = True
          Else
            e.Handled = False
          End If
        End If
      End If
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in keyPress event method of IncreaseQuantityTextBox of ShowInquiryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  
End Class
