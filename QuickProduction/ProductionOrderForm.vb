Imports QuickDALLibrary
Imports QuickDAL
Imports QuickLibrary
Imports QuickDAL.QuickProductionDataSet
Imports QuickDAL.QuickProductionDataSetTableAdapters

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 18-Nov-10
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' Production Order form.
''' </summary>
Public Class ProductionOrderForm

#Region "Declarations"
  Dim _OrderTA As New QuickProductionDataSetTableAdapters.OrderTableAdapter
  Dim _OrderDetailTA As New QuickProductionDataSetTableAdapters.OrderDetailTableAdapter
  Dim _FormulaTA As New FormulaTableAdapter
  Dim _FormulaDetailTA As New FormulaDetailTableAdapter

  Dim _OrderTable As New QuickProductionDataSet.OrderDataTable
  Dim _OrderDetailTable As New QuickProductionDataSet.OrderDetailDataTable
  Dim _FormulaTable As New FormulaDataTable
  Dim _FormulaDetailTable As New FormulaDetailDataTable

  Dim _OrderRow As QuickProductionDataSet.OrderRow

#End Region

#Region "Properties"

#End Region

#Region "Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This function will generate and return new order number.
  ''' </summary>
  Private Function NewOrderNumber() As String
    Try
      Dim _LikeOperatorPattern As String     'It will hold the value which will be used with like operator to get maximum value.
      Dim _LastOrderNo As Object
      Dim _NewOrderNo As String

      _LikeOperatorPattern = Common.GenerateNextDocumentNo(String.Empty, String.Empty, DatabaseCache.GetSettingValue(Constants.SETTING_ID_DocumentNoFormat_ProductionOrder), True)
      _LastOrderNo = _OrderTA.GetMaxOrderNoByCoID(LoginInfoObject.CompanyID, _LikeOperatorPattern)
      If _LastOrderNo Is Nothing Then
        _NewOrderNo = Common.GenerateNextDocumentNo(String.Empty, String.Empty, DatabaseCache.GetSettingValue(Constants.SETTING_ID_DocumentNoFormat_ProductionOrder), False)
      Else
        _NewOrderNo = Common.GenerateNextDocumentNo(String.Empty, _LastOrderNo.ToString, DatabaseCache.GetSettingValue(Constants.SETTING_ID_DocumentNoFormat_ProductionOrder), False)
      End If

      Return _NewOrderNo

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in NewOrderNumber of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 18-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Saves record.
  ''' </summary>
  Protected Overrides Function SaveRecord() As Boolean
    Try
      If Me.CurrentRecordDataRow Is Nothing Then
        _OrderTable = New QuickProductionDataSet.OrderDataTable
        _OrderRow = _OrderTable.NewOrderRow
        _OrderRow.Co_ID = Me.LoginInfoObject.CompanyID
        _OrderRow.Order_ID = _OrderTA.GetNewOrderID(Me.LoginInfoObject.CompanyID).Value
        _OrderRow.Order_No = NewOrderNumber()
        _OrderRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        _OrderRow.SetUpload_DateTimeNull()  'It should always be nothing when inserting records.

        _OrderTable.Rows.Add(_OrderRow)
        Me.CurrentRecordDataRow = _OrderRow

        'Set Order Detail properties
        Dim _OrderDetailRow As QuickProductionDataSet.OrderDetailRow = _OrderDetailTable.NewOrderDetailRow
        _OrderDetailRow.Co_ID = Me.LoginInfoObject.CompanyID
        '_OrderDetailRow.Formula_ID
        '_OrderDetailRow.Item_Detail_ID = 

      Else
        _OrderRow.RecordStatus_ID = Constants.RecordStatuses.Updated
      End If

      'Set common values for modify and insert
      _OrderRow.Order_Date = Convert.ToDateTime(Me.OrderDateCalendarCombo.Value).ToUniversalTime
      _OrderRow.Remarks = Me.RemarksTextBox.Text
      _OrderRow.Stamp_DateTime = Date.UtcNow
      _OrderRow.Stamp_UserID = Me.LoginInfoObject.UserID



      'Save data in database
      _OrderTA.Update(_OrderRow)
      Me.OrderNoTextBox.Text = _OrderRow.Order_No

      Return True
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveRecord of ProductionOrderForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 18-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Validates values and return true if they are valid otherwise return false. If
  ''' there is any invalid value it will also alert user.
  ''' </summary>
  Private Function IsValid() As QuickLibrary.Constants.MethodResult
    Try

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in IsValid of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Function


#End Region

#Region "Event Methods"
  Dim _ItemTA As New QuickInventoryDataSetTableAdapters.Invs_ItemTableAdapter
  Dim _ItemSizeTA As New QuickInventoryDataSetTableAdapters.Inv_ItemSizeTableAdapter
  Dim _ItemTable As QuickInventoryDataSet.Invs_ItemDataTable
  Dim _ItemSizeTable As QuickInventoryDataSet.Inv_ItemSizeDataTable

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 18-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Load event of production order form.
  ''' </summary>
  Private Sub ProductionOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      _ItemSizeTable = _ItemSizeTA.GetByCoID(21)
      _ItemTable = _ItemTA.GetByCoID(Me.LoginInfoObject.CompanyID)

      Me.ProductionOrderSpread.ActiveSheet.Columns.Count = _ItemSizeTable.Count
      Me.ProductionOrderSpread.ActiveSheet.Rows.Count = 1
      Me.ProductionOrderSpread.AutoNewRow = False
      Me.SummarySpread.ActiveSheet.Columns.Count = _ItemSizeTable.Count
      Me.SummarySpread.ActiveSheet.RowCount = 3

      'Me.FormulaDetailSpread.ActiveSheet.Columns.Count = _ItemSizeTable.Count

      For I As Int32 = 0 To _ItemSizeTable.Count - 1
        Me.ProductionOrderSpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
        Me.SummarySpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
        'Me.FormulaDetailSpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
      Next

      Me.ItemMultiComboBox.qSetComboBoxesOnDataTable(_ItemTable, DatabaseCache.GetSettingValue(Constants.SETTING_ID_Mask_ItemCode), Constants.ITEM_LEVELING_SEPERATOR, _ItemTable.Item_CodeColumn.ColumnName, _ItemTable.Item_IDColumn.ColumnName)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ProductionOrder_Load event method of ProductionOrder.", ex)
      Throw _qex
    End Try
  End Sub

#End Region


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' ValueChanged event.
  ''' </summary>
  Private Sub ItemMultiComboBox_qValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ItemMultiComboBox.qValueChanged
    Try
      Dim _ItemTA As New QuickInventoryDataSetTableAdapters.Invs_ItemTableAdapter
      Dim _ItemTable As New QuickInventoryDataSet.Invs_ItemDataTable

      _ItemTable = _ItemTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text)
      If _ItemTable.Rows.Count > 0 Then
        'Item codes are unique so there will be only one item.
        _FormulaTable = _FormulaTA.GetByCoIDOutputItemID(Me.LoginInfoObject.CompanyID, _ItemTable(0).Item_ID)
        If _FormulaTable.Rows.Count > 0 Then
          _FormulaDetailTable = _FormulaDetailTA.GetByCoIDFormulaID(Me.LoginInfoObject.CompanyID, _FormulaTable(0).Formula_ID)
          Me.FormulaDetailSpread.ActiveSheet.DataSource = _FormulaDetailTable
        End If
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ItemMultiComboBox_qValueChanged event method of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub
End Class

