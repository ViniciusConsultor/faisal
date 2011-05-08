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
  Dim _ItemTA As New QuickInventoryDataSetTableAdapters.Invs_ItemTableAdapter
  Dim _ItemSizeTA As New QuickInventoryDataSetTableAdapters.ItemSizeTableAdapter
  Dim _ItemDetailTA As New QuickInventoryDataSetTableAdapters.ItemDetailTableAdapter
  Dim _ProcessProductionTA As New ProcessProductionTableAdapter
  Dim _ProcessProductionDetailTA As New ProcessProductionDetailTableAdapter

  Dim _OrderTable As New QuickProductionDataSet.OrderDataTable
  Dim _OrderDetailTable As New QuickProductionDataSet.OrderDetailDataTable
  Dim _FormulaTable As New FormulaDataTable
  Dim _FormulaDetailTable As New FormulaDetailDataTable
  Dim _FormulaDetailToDisplay As New FormulaDetailDataTable
  Dim _ItemTable As QuickInventoryDataSet.Invs_ItemDataTable
  Dim _ItemSizeTable As QuickInventoryDataSet.ItemSizeDataTable
  Dim _ItemDetailTable As New QuickInventoryDataSet.ItemDetailDataTable
  Dim _ProcessProductionTable As New ProcessProductionDataTable
  Dim _ProcessProductionDetailTable As New ProcessProductionDetailDataTable

  Dim _OrderRow As QuickProductionDataSet.OrderRow
  Dim _ProcessProductionRow As ProcessProductionRow
  Dim _ProcessProductionDetailRow As ProcessProductionDetailRow

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
      Dim _OrderDetailRow As OrderDetailRow

      Me.ProductionOrderSpread.EditMode = False
      Me.ProductionOrderSheetView.SetActiveCell(0, 0)

      If Me.CurrentRecordDataRow Is Nothing Then
        _OrderTable = New QuickProductionDataSet.OrderDataTable
        _OrderRow = _OrderTable.NewOrderRow
        _OrderRow.Co_ID = Me.LoginInfoObject.CompanyID
        _OrderRow.Order_ID = _OrderTA.GetNewOrderIDByCoID(Me.LoginInfoObject.CompanyID).Value
        _OrderRow.Order_No = NewOrderNumber()
        _OrderRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        _OrderRow.SetUpload_DateTimeNull()  'It should always be null when inserting records.

        _OrderDetailTable = New OrderDetailDataTable
      Else
        _OrderRow.RecordStatus_ID = Constants.RecordStatuses.Updated
      End If

      _ProcessProductionTable = _ProcessProductionTA.GetByCoIDDocumentTypeIDDocumentID(Me.LoginInfoObject.CompanyID, Constants.enuDocumentType.ProductionOrder, _OrderRow.Order_ID)
      If _ProcessProductionTable.Rows.Count = 0 Then
        _ProcessProductionRow = _ProcessProductionTable.NewProcessProductionRow

        With _ProcessProductionRow
          .Co_ID = Me.LoginInfoObject.CompanyID
          .Order_ID = _OrderRow.Order_ID
          .SetOrderBatch_IDNull()
          .Production_ID = _ProcessProductionTA.GetNewProductionIDByCoID(Me.LoginInfoObject.CompanyID).Value
          .Production_No = _ProcessProductionTA.GetNewProductionNoByCoID(Me.LoginInfoObject.CompanyID)
          .RecordStatus_ID = Constants.RecordStatuses.Inserted
          .Source_Document_ID = _OrderRow.Order_ID
          .Source_DocumentType_ID = Constants.enuDocumentType.ProductionOrder
        End With
      Else
        _ProcessProductionRow = _ProcessProductionTable(0)
        _ProcessProductionRow.RecordStatus_ID = Constants.RecordStatuses.Updated
      End If
      With _ProcessProductionRow
        .Production_Date = Convert.ToDateTime(Me.OrderDateCalendarCombo.Value)
        .Stamp_DateTime = Common.SystemDateTime
        .Stamp_UserID = Me.LoginInfoObject.UserID
      End With

      _ProcessProductionDetailTable = _ProcessProductionDetailTA.GetByCoIDProductionID(Me.LoginInfoObject.CompanyID, _ProcessProductionRow.Production_ID)

      For I As Int32 = 0 To _ItemSizeTable.Count - 1
        '_ItemDetailTable = _ItemDetailTA.GetByCoIDItemCodeItemSizeID(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text, _ItemSizeTable(I).ItemSize_ID)
        'If _ItemDetailTable.Rows.Count > 0 Then
        'Get item detail id for selected item and size.
        _ItemDetailTable = _ItemDetailTA.GetByCoIDItemCodeItemSizeID(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text, _ItemSizeTable(I).ItemSize_ID)
        If _ItemDetailTable.Rows.Count = 0 Then
          If (Me.ProductionOrderSheetView.GetText(0, I) <> String.Empty AndAlso Me.ProductionOrderSheetView.GetText(0, I) <> "0") Then
            'This condition should be true only if it is new entry and size doesn't exist for which user given quantity.
            QuickMessageBox.Show(Me.LoginInfoObject, "Size " & _ItemSizeTable(I).ItemSize_Desc & " is not defined for selected item", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
            Return False
          End If
        Else
          _OrderDetailTable.DefaultView.RowFilter = _OrderDetailTable.Item_Detail_IDColumn.ColumnName & "=" & _ItemDetailTable(0).Item_Detail_ID.ToString
          If _OrderDetailTable.DefaultView.Count > 0 Then
            'Updating record
            _OrderDetailRow = DirectCast(_OrderDetailTable.DefaultView(0).Row, OrderDetailRow)
            _OrderDetailRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          Else
            'Inserting record
            If Me.ProductionOrderSheetView.GetText(0, I) = String.Empty OrElse Me.ProductionOrderSheetView.GetText(0, I) = "0" Then
              'When new record and quantity is 0 then don't create record
              _OrderDetailRow = Nothing
            Else
              _OrderDetailRow = _OrderDetailTable.NewOrderDetailRow
              With _OrderDetailRow
                .Order_Detail_ID = -(I + 1)
                .Co_ID = Me.LoginInfoObject.CompanyID
                .Order_ID = _OrderRow.Order_ID
                .RecordStatus_ID = Constants.RecordStatuses.Inserted
              End With
            End If
          End If

          If _OrderDetailRow IsNot Nothing Then    'It is nothing when quantity is 0 for new record.
            'Get the formula for selected item and size
            _FormulaTable = _FormulaTA.GetByCoIDItemCodeItemSizeID(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text, _ItemSizeTable(I).ItemSize_ID)
            If _FormulaTable.Rows.Count = 0 Then
              QuickMessageBox.Show(Me.LoginInfoObject, "Production formula is not found for this item and size " & _ItemSizeTable(I).ItemSize_Desc, MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
              Return False
            End If

            'Setting common properties for insert and update
            _OrderDetailRow.Formula_ID = _FormulaTable(0).Formula_ID
            _OrderDetailRow.Item_Detail_ID = _ItemDetailTable(0).Item_Detail_ID
            _OrderDetailRow.Quantity = 0
            Decimal.TryParse(Me.ProductionOrderSheetView.GetText(0, I), _OrderDetailRow.Quantity)
            _OrderDetailRow.Stamp_DateTime = Date.UtcNow
            _OrderDetailRow.Stamp_UserID = Me.LoginInfoObject.UserID

            If _OrderDetailRow.RowState = DataRowState.Detached Then _OrderDetailTable.Rows.Add(_OrderDetailRow)
          End If

          '<<<<<<<<<< Start Update Process Production Records
          If _OrderDetailRow IsNot Nothing Then
            _ProcessProductionDetailTable.DefaultView.RowFilter = _ProcessProductionDetailTable.Item_Detail_IDColumn.ColumnName & "=" & _ItemDetailTable(0).Item_Detail_ID.ToString
            If _ProcessProductionDetailTable.DefaultView.Count > 0 Then
              'Updating record
              _ProcessProductionDetailRow = DirectCast(_ProcessProductionDetailTable.DefaultView(0).Row, ProcessProductionDetailRow)
              _ProcessProductionDetailRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            Else
              'Insert record
              _ProcessProductionDetailRow = _ProcessProductionDetailTable.NewProcessProductionDetailRow

              With _ProcessProductionDetailRow
                .Co_ID = Me.LoginInfoObject.CompanyID
                .SetConsumption_Process_IDNull()
                .Item_Detail_ID = _ItemDetailTable(0).Item_Detail_ID
                .Production_Detail_ID = -(I + 1)
                .Production_ID = _ProcessProductionRow.Production_ID
                .Production_Process_ID = 2     'There should be a column indication order in process table
                .RecordStatus_ID = Constants.RecordStatuses.Inserted
              End With
            End If

            With _ProcessProductionDetailRow
              .Quantity = _OrderDetailRow.Quantity
              .Stamp_DateTime = Common.SystemDateTime
              .Stamp_UserID = Me.LoginInfoObject.UserID
            End With

            If _ProcessProductionDetailRow.RowState = DataRowState.Detached Then _ProcessProductionDetailTable.Rows.Add(_ProcessProductionDetailRow)
          End If
          '>>>>>>>>>> End Update Process Production Records

          End If
      Next I

      'Set common values for modify and insert
      _OrderRow.Order_Date = Convert.ToDateTime(Me.OrderDateCalendarCombo.Value)
      _OrderRow.Remarks = Me.RemarksTextBox.Text
      _OrderRow.Stamp_DateTime = Date.UtcNow
      _OrderRow.Stamp_UserID = Me.LoginInfoObject.UserID

      If _OrderRow.RowState = DataRowState.Detached Then _OrderTable.Rows.Add(_OrderRow)

      'Save data in database
      _OrderTA.Update(_OrderRow)

      For I As Int32 = 0 To _OrderDetailTable.Rows.Count - 1
        If _OrderDetailTable(I).Order_Detail_ID < 0 Then
          _OrderDetailTable(I).Order_Detail_ID = _OrderDetailTA.GetNewOrderDetailIDByCoIDOrderID(Me.LoginInfoObject.CompanyID, _OrderRow.Order_ID).Value
        End If
        _OrderDetailTA.Update(_OrderDetailTable(I))
      Next I
      Me.CurrentRecordDataRow = _OrderRow
      Me.OrderNoTextBox.Text = _OrderRow.Order_No

      '<<<<<<<<<< Start Update Process Production Records
      If _ProcessProductionRow.RowState = DataRowState.Detached Then _ProcessProductionTable.Rows.Add(_ProcessProductionRow)
      _ProcessProductionTA.Update(_ProcessProductionRow)
      For I As Int32 = 0 To _ProcessProductionDetailTable.Rows.Count - 1
        If _ProcessProductionDetailTable(I).RowState = DataRowState.Added Then
          _ProcessProductionDetailTable(I).Production_Detail_ID = _ProcessProductionDetailTA.GetNewProductionDetailID(Me.LoginInfoObject.CompanyID, _ProcessProductionRow.Production_ID).Value
        End If
        _ProcessProductionDetailTA.Update(_ProcessProductionDetailTable(I))
      Next
      '>>>>>>>>>> End Update Process Production Records

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

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Move to first record load it and show it on the form.
  ''' </summary>
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      _OrderTable = _OrderTA.GetFirstByCoID(Me.LoginInfoObject.CompanyID)
      If _OrderTable.Rows.Count > 0 Then
        _OrderRow = _OrderTable(0)
        Me.CurrentRecordDataRow = _OrderRow

        MyBase.MoveFirstButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Moves to last record, load it and show it on the form.
  ''' </summary>
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      _OrderTable = _OrderTA.GetLastByCoID(Me.LoginInfoObject.CompanyID)
      If _OrderTable.Rows.Count > 0 Then
        _OrderRow = _OrderTable(0)
        Me.CurrentRecordDataRow = _OrderRow

        MyBase.MoveLastButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in MoveLastButtonClick of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Moves to next record, load it and show it on the form.
  ''' </summary>
  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      If Me.CurrentRecordDataRow IsNot Nothing Then
        _OrderTable = _OrderTA.GetNextByCoIDOrderID(Me.LoginInfoObject.CompanyID, _OrderRow.Order_ID)
      Else
        _OrderTable = _OrderTA.GetNextByCoIDOrderID(Me.LoginInfoObject.CompanyID, 0)
      End If

      If _OrderTable.Rows.Count > 0 Then
        _OrderRow = _OrderTable(0)
        Me.CurrentRecordDataRow = _OrderRow

        MyBase.MoveNextButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in MoveNextButtonClick of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Moves to previous record, load it and show it on the form.
  ''' </summary>
  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      If Me.CurrentRecordDataRow IsNot Nothing Then
        _OrderTable = _OrderTA.GetPreviousByCoIDOrderID(Me.LoginInfoObject.CompanyID, _OrderRow.Order_ID)
      Else
        _OrderTable = _OrderTA.GetPreviousByCoIDOrderID(Me.LoginInfoObject.CompanyID, 0)
      End If

      If _OrderTable.Rows.Count > 0 Then
        _OrderRow = _OrderTable(0)
        Me.CurrentRecordDataRow = _OrderRow

        MyBase.MovePreviousButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will show the loaded record on form.
  ''' </summary>
  Protected Overrides Function ShowRecord() As Boolean
    Try
      Me.OrderDateCalendarCombo.Value = _OrderRow.Order_Date
      Me.RemarksTextBox.Text = _OrderRow.Remarks
      Me.OrderNoTextBox.Text = _OrderRow.Order_No

      _OrderDetailTable = _OrderDetailTA.GetByCoIDOrderID(Me.LoginInfoObject.CompanyID, _OrderRow.Order_ID)
      For I As Int32 = 0 To _ItemSizeTable.Rows.Count - 1
        If I = 0 Then Me.ItemMultiComboBox.Text = _OrderDetailTable(I).Item_Code
        _OrderDetailTable.DefaultView.RowFilter = _ItemDetailTable.ItemSize_IDColumn.ColumnName & "=" & _ItemSizeTable(I).ItemSize_ID
        If _OrderDetailTable.DefaultView.Count > 0 Then
          Me.ProductionOrderSpread.ActiveSheet.Cells(0, I).Value = DirectCast(_OrderDetailTable.DefaultView(0).Row, OrderDetailRow).Quantity
        Else
          Me.ProductionOrderSpread.ActiveSheet.Cells(0, I).Value = 0
        End If
      Next

      Return MyBase.ShowRecord()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Show of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It clears the form and make it ready for new entry.
  ''' </summary>
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      MyBase.CancelButtonClick(sender, e)

      Me.ProductionOrderSpread.ActiveSheet.RowCount = 1

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in CancelButtonClick of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

#Region "Event Methods"

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
      _ItemSizeTable = _ItemSizeTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      _ItemTable = _ItemTA.GetByCoID(Me.LoginInfoObject.CompanyID)

      Me.ProductionOrderSpread.ActiveSheet.Columns.Count = _ItemSizeTable.Count + 1
      Me.ProductionOrderSpread.ActiveSheet.Rows.Count = 1
      Me.ProductionOrderSpread.ActiveSheet.Columns(_ItemSizeTable.Count).Locked = True
      Me.ProductionOrderSpread.AutoNewRow = False
      Me.SummarySpread.ActiveSheet.Columns.Count = _ItemSizeTable.Count + 1
      Me.SummarySpread.ActiveSheet.RowCount = 3

      'Me.FormulaDetailSpread.ActiveSheet.Columns.Count = _ItemSizeTable.Count

      For I As Int32 = 0 To _ItemSizeTable.Count - 1
        Me.ProductionOrderSpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
        Me.SummarySpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
        'Me.FormulaDetailSpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
      Next
      Me.SummarySpread.ActiveSheet.Columns(_ItemSizeTable.Count).Label = "Total"
      Me.FormulaDetailSpread.ActiveSheet.ColumnCount = 0
      Me.FormulaDetailSpread.ActiveSheet.RowCount = 0
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

      _FormulaDetailToDisplay = Nothing
      _FormulaDetailToDisplay = New FormulaDetailDataTable

      _ItemDetailTable = _ItemDetailTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text)
      For id As Int32 = 0 To _ItemDetailTable.Rows.Count - 1
        _FormulaTable = _FormulaTA.GetByCoIDOutputItemID(Me.LoginInfoObject.CompanyID, _ItemDetailTable(id).Item_Detail_ID)
        For f As Int32 = 0 To _FormulaTable.Rows.Count - 1
          LoadFormulaDetails(_FormulaTable(f).Formula_ID)
        Next f
      Next id

      SetFormulaDetailGridLayout()
      CalculateTotalUpdateFromulaDetail()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ItemMultiComboBox_qValueChanged event method of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Display formula details.
  ''' </summary>
  Private Sub LoadFormulaDetails(ByVal _FormulaIDpara As Int32)
    Try
      Dim _FormulaDetailRow As FormulaDetailRow

      _FormulaDetailToDisplay.PrimaryKey = Nothing

      _FormulaDetailTable = _FormulaDetailTA.GetByCoIDFormulaID(Me.LoginInfoObject.CompanyID, _FormulaIDpara)
      For fd As Int32 = 0 To _FormulaDetailTable.Rows.Count - 1
        _FormulaDetailToDisplay.DefaultView.RowFilter = _FormulaDetailToDisplay.Input_Item_Detail_IDColumn.ColumnName & "=" & _FormulaDetailTable(fd).Input_Item_Detail_ID
        'For I As Int32 = 0 To _FormulaDetailTable.Rows.Count - 1

        'Next I

        If _FormulaDetailToDisplay.DefaultView.Count > 0 Then
          DirectCast(_FormulaDetailToDisplay.DefaultView(0).Row, QuickProductionDataSet.FormulaDetailRow).Quantity += _FormulaDetailTable(fd).Quantity
        Else
          _FormulaDetailRow = _FormulaDetailToDisplay.NewFormulaDetailRow

          _FormulaDetailRow.Co_ID = _FormulaDetailTable(fd).Co_ID
          _FormulaDetailRow.Formula_Detail_ID = _FormulaDetailTable(fd).Formula_Detail_ID
          _FormulaDetailRow.Formula_ID = _FormulaDetailTable(fd).Formula_ID
          _FormulaDetailRow.Input_Item_Detail_ID = _FormulaDetailTable(fd).Input_Item_Detail_ID
          _FormulaDetailRow.Item_Desc = _FormulaDetailTable(fd).Item_Desc
          _FormulaDetailRow.Quantity = _FormulaDetailTable(fd).Quantity
          _FormulaDetailRow.RecordStatus_ID = _FormulaDetailTable(fd).RecordStatus_ID
          _FormulaDetailRow.Remarks = _FormulaDetailTable(fd).Remarks
          _FormulaDetailRow.Stamp_DateTime = _FormulaDetailTable(fd).Stamp_DateTime
          _FormulaDetailRow.Stamp_UserID = _FormulaDetailTable(fd).Stamp_UserID

          _FormulaDetailToDisplay.Rows.Add(_FormulaDetailRow)
        End If
      Next fd

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in LoadFormulaDetails of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Set the layout of the forumula detail grid.
  ''' </summary>
  Private Sub SetFormulaDetailGridLayout()
    Try
      Me.FormulaDetailSpread.ActiveSheet.DataSource = _FormulaDetailToDisplay
      For I As Int32 = 0 To _FormulaDetailToDisplay.Columns.Count - 1
        FormulaDetailSheetView.Columns(I).Locked = True

        Select Case I
          Case _FormulaDetailToDisplay.RemarksColumn.Ordinal
            _FormulaDetailToDisplay.RemarksColumn.Caption = "Remarks"
          Case _FormulaDetailToDisplay.Item_DescColumn.Ordinal
            _FormulaDetailToDisplay.Columns(I).Caption = "Description"
          Case _FormulaDetailToDisplay.QuantityColumn.Ordinal
            _FormulaDetailToDisplay.Columns(I).Caption = "Quantity"
            Me.FormulaDetailSpread.ActiveSheet.Columns(I).CellType = Common.QtyCellType
          Case Else
            Me.FormulaDetailSpread.ActiveSheet.Columns(I).Visible = False
        End Select
      Next

      _FormulaDetailToDisplay.AcceptChanges() 'This is to create original row so that calculate total can use it.

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetFormulaDetailGridLayout of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will update total.
  ''' </summary>
  Private Sub ProductionOrderSpread_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductionOrderSpread.EditModeOff
    Try
      CalculateTotalUpdateFromulaDetail()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ProductionOrderSpread_EditModeOff event method of ProductionOrderForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Calculates total and add to raw material quantity
  ''' </summary>
  Private Sub CalculateTotalUpdateFromulaDetail()
    Try
      Dim _Quantity As Decimal
      Dim _TotalQuantity As Decimal

      'This condition is required because it will throw exception when cancel button is clicked.
      If ProductionOrderSheetView.Rows.Count > 0 Then
        For I As Int32 = 0 To _ItemSizeTable.Rows.Count - 1
          _Quantity = 0
          If Decimal.TryParse(ProductionOrderSheetView.Cells(0, I).Text, _Quantity) Then
            _TotalQuantity += _Quantity
          End If
        Next

        ProductionOrderSheetView.SetText(0, _ItemSizeTable.Rows.Count, _TotalQuantity.ToString)

        For I As Int32 = 0 To _FormulaDetailToDisplay.Rows.Count - 1
          _FormulaDetailToDisplay(I).Quantity = Convert.ToDecimal(_FormulaDetailToDisplay(I).Item(_FormulaDetailToDisplay.QuantityColumn.ColumnName, DataRowVersion.Original)) * _TotalQuantity    'Use original quantity so that it does not keep on adding on user change.
        Next
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in CalculateTotalUpdateFromulaDetail of ProductionOrderForm.", ex)
      Throw _qex
    End Try
  End Sub


End Class

