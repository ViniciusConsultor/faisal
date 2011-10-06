Imports QuickLibrary
Imports QuickDALLibrary
Imports QuickDAL

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 27-Nov-10
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' Process wise production form
''' </summary>
Public Class ProcessProduction

#Region "Declarations"
  Dim _OrderTA As New QuickProductionDataSetTableAdapters.OrderTableAdapter
  Dim _ItemTA As New QuickInventoryDataSetTableAdapters.Invs_ItemTableAdapter
  Dim _ItemSizeTA As New QuickInventoryDataSetTableAdapters.ItemSizeTableAdapter
  Dim _ProcessWorkflowTA As New QuickProductionDataSetTableAdapters.ProductionProcessWorkFlowTableAdapter
  Dim _ProcessBalanceTA As New QuickProductionDataSetTableAdapters.ProcessBalanceTableAdapter
  Dim _ProcessProductionTA As New QuickProductionDataSetTableAdapters.ProcessProductionTableAdapter
  Dim _ProcessProductionDetailTA As New QuickProductionDataSetTableAdapters.ProcessProductionDetailTableAdapter
  Dim _OrderBatchTA As New QuickProductionDataSetTableAdapters.OrderBatchTableAdapter
  Dim _OrderBatchDetailTA As New QuickProductionDataSetTableAdapters.OrderBatchDetailTableAdapter
  Dim _ItemDetailTA As New QuickInventoryDataSetTableAdapters.ItemDetailTableAdapter

  Dim _OrderTable As New QuickProductionDataSet.OrderDataTable
  Dim _ProcessWorkflowTable As QuickProductionDataSet.ProductionProcessWorkFlowDataTable
  Dim _ProcessBalanceTable As QuickProductionDataSet.ProcessBalanceDataTable
  Dim _ProductionTable As QuickProductionDataSet.ProcessProductionDataTable
  Dim _ProductionDetailTable As QuickProductionDataSet.ProcessProductionDetailDataTable
  Dim _ItemDetailTable As QuickInventoryDataSet.ItemDetailDataTable
  Dim _ItemTable As QuickInventoryDataSet.Invs_ItemDataTable
  Dim _ItemSizeTable As QuickInventoryDataSet.ItemSizeDataTable
  Dim _OrderBatchTable As QuickProductionDataSet.OrderBatchDataTable
  Dim _OrderBatchDetailTable As QuickProductionDataSet.OrderBatchDetailDataTable

  Dim _ProductionRow As QuickProductionDataSet.ProcessProductionRow
  Dim _ProductionDetailRow As QuickProductionDataSet.ProcessProductionDetailRow
  Dim _OrderBatchRow As QuickProductionDataSet.OrderBatchRow
  Dim _OrderBatchDetailRow As QuickProductionDataSet.OrderBatchDetailRow

  Dim _ProcessWorkflowSourceView As DataView
  Dim _ProcessWorkflowDestinationView As DataView

#End Region

#Region "Properties"

#End Region

#Region "Methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Sep-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Initialize Controls
  ''' </summary>
  Private Function SetControlsStatus() As Boolean
    Try
      Me.SourceProcessStockSpread.Enabled = False
      Me.DestinationProcessStockSpread.Enabled = False

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetControlsStatus of ProcessProduction.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' IsValid
  ''' </summary>
  Private Function IsValid() As Boolean
    Try

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in IsValid of ProcessProduction.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Load Production IDs
  ''' </summary>
  Private Sub LoadProductionIDs()
    Try
      _ProductionTable = _ProcessProductionTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.ProductionIDComboBox.DataSource = _ProductionTable
      Me.ProductionIDComboBox.DisplayMember = _ProductionTable.Production_IDColumn.ColumnName
      Me.ProductionIDComboBox.ValueMember = _ProductionTable.Production_IDColumn.ColumnName

      For I As Int32 = 0 To _ProductionTable.Columns.Count - 1
        Select Case I
          Case Me._ProductionTable.Production_IDColumn.Ordinal
          Case Else
            Me.ProductionIDComboBox.Rows.Band.Columns(I).Hidden = True
        End Select
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in LoadProductionIDs of ProcessProduction.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Save record
  ''' </summary>
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      MyBase.CancelButtonClick(sender, e)
      SetControlsStatus()
      Me.SourceProcessStockSheetView.FrozenColumnCount = 0
      Me.DestinationProcessStockSheetView.FrozenColumnCount = 0
      Me.ProcessProductionSheetView.RowCount = 1

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in LoadProductionIDs of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Save record
  ''' </summary>
  Protected Overrides Function SaveRecord() As Boolean
    Try

      Me.ProcessProductionQuantitySpread.Update()
      Me.ProcessProductionQuantitySpread.EditMode = False
      Me.ProcessProductionQuantitySpread.ActiveSheet.SetActiveCell(0, 0)

      If Me.CurrentRecordDataRow Is Nothing Then
        _ProductionRow = _ProductionTable.NewProcessProductionRow
        With _ProductionRow
          .RecordStatus_ID = Constants.RecordStatuses.Inserted
          .Co_ID = Me.LoginInfoObject.CompanyID
          If Me.ProductionOrderCombBox.SelectedRow IsNot Nothing Then
            .Order_ID = Convert.ToInt32(Me.ProductionOrderCombBox.SelectedRow.Cells(_OrderTable.Order_IDColumn.ColumnName).Text)
            If Me.ProductionOrderBatchComboBox.Text.Trim <> String.Empty Then
              _OrderBatchTable = _OrderBatchTA.GetByCoIDOrderBatchNo(Me.LoginInfoObject.CompanyID, Me.ProductionOrderBatchComboBox.Text)
              If _OrderBatchTable.Rows.Count > 0 Then
                _OrderBatchRow = _OrderBatchTable(0)
              Else
                _OrderBatchRow = Nothing
              End If

              If _OrderBatchRow Is Nothing Then
                _OrderBatchRow = _OrderBatchTable.NewOrderBatchRow
                With _OrderBatchRow
                  .Co_ID = Me.LoginInfoObject.CompanyID
                  .Order_ID = Convert.ToInt32(Me.ProductionOrderCombBox.SelectedRow.Cells(_OrderTable.Order_IDColumn.ColumnName).Text)
                  .OrderBatch_Date = Convert.ToDateTime(Me.ProductionDateCalendarCombo.Value)
                  .OrderBatch_ID = _OrderBatchTA.GetNewOrderBatchID(Me.LoginInfoObject.CompanyID, .Order_ID).Value
                  .OrderBatch_No = Me.ProductionOrderBatchComboBox.Text
                  .RecordStatus_ID = Constants.RecordStatuses.Inserted
                  .Remarks = "Auto"
                  .Stamp_DateTime = Common.SystemDateTime
                  .Stamp_UserID = Me.LoginInfoObject.UserID
                End With
                _OrderBatchTable.Rows.Add(_OrderBatchRow)
              End If

              .OrderBatch_ID = _OrderBatchRow.OrderBatch_ID
            End If
          End If
          .Production_No = _ProcessProductionTA.GetNewProductionNoByCoID(Me.LoginInfoObject.CompanyID)
          .Production_ID = _ProcessProductionTA.GetNewProductionIDByCoID(Me.LoginInfoObject.CompanyID).Value
          .RecordStatus_ID = Constants.RecordStatuses.Inserted
        End With

        _ProductionDetailTable = New QuickProductionDataSet.ProcessProductionDetailDataTable
      Else
        _ProductionRow.RecordStatus_ID = Constants.RecordStatuses.Updated
      End If

      With _ProductionRow
        .Production_Date = Convert.ToDateTime(Me.ProductionDateCalendarCombo.Value)
        .Stamp_DateTime = Common.SystemDateTime
        .Stamp_UserID = Me.LoginInfoObject.UserID
        .Vender_Party_ID = Me.OutsourcingPartyComboBox.PartyID
      End With
      If _ProductionRow.RowState = DataRowState.Detached Then _ProductionTable.Rows.Add(_ProductionRow)

      For c As Int32 = 0 To _ItemSizeTable.Rows.Count - 1
        _ItemDetailTable = _ItemDetailTA.GetByCoIDItemCodeItemSizeID(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text, _ItemSizeTable(c).ItemSize_ID)

        If _ItemDetailTable.Rows.Count = 0 AndAlso Me.ProcessProductionSheetView.GetText(0, c) <> String.Empty AndAlso Me.ProcessProductionSheetView.GetText(0, c) <> "0" Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Item Size " & _ItemSizeTable(c).ItemSize_Desc & "is not defined", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
          Return False
        ElseIf _ItemDetailTable.Rows.Count > 0 Then
          _ProductionDetailTable.DefaultView.RowFilter = _ProductionDetailTable.Item_Detail_IDColumn.ColumnName & "=" & _ItemDetailTable(0).Item_Detail_ID.ToString

          If _ProductionDetailTable.DefaultView.Count = 0 Then
            _ProductionDetailRow = _ProductionDetailTable.NewProcessProductionDetailRow
            With _ProductionDetailRow
              .Co_ID = Me.LoginInfoObject.CompanyID
              .Production_Detail_ID = -c
              .Production_ID = _ProductionRow.Production_ID
              .RecordStatus_ID = Constants.RecordStatuses.Inserted
            End With
          Else
            _ProductionDetailRow = DirectCast(_ProductionDetailTable.DefaultView.Item(0).Row, QuickProductionDataSet.ProcessProductionDetailRow)
            _ProductionDetailRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If

          With _ProductionDetailRow
            If Me.SourceProcessComboBox.SelectedRow.Cells(_ProcessWorkflowTable.Source_Process_IDColumn.ColumnName).Value IsNot DBNull.Value Then
              .Consumption_Process_ID = Convert.ToInt32(Me.SourceProcessComboBox.SelectedRow.Cells(_ProcessWorkflowTable.Source_Process_IDColumn.ColumnName).Value)
            End If
            .Production_Process_ID = Convert.ToInt32(Me.DestinationProcessComboBox.SelectedRow.Cells(_ProcessWorkflowTable.Destination_Process_IDColumn.ColumnName).Value)
            .Item_Detail_ID = _ItemDetailTable(0).Item_Detail_ID
            If Me.ProcessProductionSheetView.GetValue(0, c) IsNot Nothing Then
              .Quantity = Convert.ToDecimal(Me.ProcessProductionSheetView.GetText(0, c))
            Else
              .Quantity = 0
            End If
            .Stamp_DateTime = Common.SystemDateTime
            .Stamp_UserID = Me.LoginInfoObject.UserID
          End With
        End If
        If _ProductionDetailRow.RowState = DataRowState.Detached Then _ProductionDetailTable.Rows.Add(_ProductionDetailRow)

      Next c

      If _OrderBatchTable IsNot Nothing Then _OrderBatchTA.Update(_OrderBatchTable)
      _ProcessProductionTA.Update(_ProductionTable)
      For I As Int32 = 0 To _ProductionDetailTable.Rows.Count - 1
        If _ProductionDetailTable(I).RowState = DataRowState.Added Then
          _ProductionDetailTable(I).Production_Detail_ID = _ProcessProductionDetailTA.GetNewProductionDetailID(Me.LoginInfoObject.CompanyID, _ProductionRow.Production_ID).Value
        End If
        _ProcessProductionDetailTA.Update(_ProductionDetailTable(I))
      Next

      LoadProductionIDs()

      Return True


    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveRecord of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Function

#End Region

#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Form Load.
  ''' </summary>
  Private Sub ProcessProduction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      _ItemSizeTable = _ItemSizeTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      _ItemTable = _ItemTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      LoadProductionIDs()

      Me.ProcessProductionSheetView.Columns.Count = _ItemSizeTable.Count + 1
      Me.ProcessProductionSheetView.Rows.Count = 1
      Me.ProcessProductionSheetView.Columns(_ItemSizeTable.Count).Locked = True

      Me.SourceProcessStockSheetView.ColumnHeaderVisible = False
      Me.SourceProcessStockSheetView.Columns.Count = _ItemSizeTable.Count + 1
      Me.SourceProcessStockSheetView.Rows.Count = 1

      Me.DestinationProcessStockSheetView.ColumnHeaderVisible = False
      Me.DestinationProcessStockSheetView.Columns.Count = _ItemSizeTable.Count + 1
      Me.DestinationProcessStockSheetView.Rows.Count = 1

      ProcessStockSpread.Enabled = False
      For I As Int32 = 0 To _ItemSizeTable.Count - 1
        Me.ProcessProductionQuantitySpread.ActiveSheet.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc
        Me.ProcessProductionSheetView.Columns(I).Label = _ItemSizeTable(I).ItemSize_Desc

        Me.SourceProcessStockSheetView.Columns(I).CellType = Common.QtyCellType
        Me.DestinationProcessStockSheetView.Columns(I).CellType = Common.QtyCellType

      Next
      Me.ProcessProductionSheetView.Columns(_ItemSizeTable.Count).Label = "Total"

      Me.ItemMultiComboBox.qSetComboBoxesOnDataTable(_ItemTable, DatabaseCache.GetSettingValue(Constants.SETTING_ID_Mask_ItemCode), Constants.ITEM_LEVELING_SEPERATOR, _ItemTable.Item_CodeColumn.ColumnName, _ItemTable.Item_IDColumn.ColumnName)

      _ProcessWorkflowTable = _ProcessWorkflowTA.GetAllByCoID(Me.LoginInfoObject.CompanyID)
      General.SetColumnCaptions(DirectCast(_ProcessWorkflowTable, DataTable), Me.Name)
      _ProcessWorkflowSourceView = _ProcessWorkflowTable.DefaultView
      _ProcessWorkflowDestinationView = _ProcessWorkflowTable.DefaultView
      SourceProcessComboBox.DataSource = _ProcessWorkflowSourceView
      SourceProcessComboBox.DisplayMember = _ProcessWorkflowTable.Source_Process_DescColumn.ColumnName
      SourceProcessComboBox.ValueMember = _ProcessWorkflowTable.ProcessWorkFlow_IDColumn.ColumnName
      SourceProcessComboBox.DropDownWidth = SourceProcessComboBox.Width * 2
      DestinationProcessComboBox.DataSource = _ProcessWorkflowDestinationView
      DestinationProcessComboBox.DisplayMember = _ProcessWorkflowTable.Destination_Process_DescColumn.ColumnName
      DestinationProcessComboBox.ValueMember = _ProcessWorkflowTable.ProcessWorkFlow_IDColumn.ColumnName
      DestinationProcessComboBox.ReadOnly = True

      Me.OutsourcingPartyComboBox.EntityType = Constants.EntityTypes.Vender
      Me.OutsourcingPartyComboBox.LoadParties(Me.LoginInfoObject.CompanyID)

      For I As Int32 = 0 To _ProcessWorkflowTable.Columns.Count - 1
        Select Case I
          Case _ProcessWorkflowTable.Source_Process_DescColumn.Ordinal
            Me.SourceProcessComboBox.Rows.Band.Columns(I).Width = Me.SourceProcessComboBox.Width - Constants.SCROLLBAR_WIDTH
          Case _ProcessWorkflowTable.Destination_Process_DescColumn.Ordinal
            Me.DestinationProcessComboBox.Rows.Band.Columns(I).Width = Me.DestinationProcessComboBox.Width - Constants.SCROLLBAR_WIDTH
          Case Else
            Me.SourceProcessComboBox.Rows.Band.Columns(I).Hidden = True
            Me.DestinationProcessComboBox.Rows.Band.Columns(I).Hidden = True
        End Select
      Next
      SetControlsStatus()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ProcessProduction_Load of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub


#End Region

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Change in item selection.
  ''' </summary>
  Private Sub ItemMultiComboBox_qValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ItemMultiComboBox.qValueChanged
    Try
      _OrderTable = _OrderTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, Me.ItemMultiComboBox.Text)

      'For some unknown reason below lines throw exception second time so suppressing exception for now due to deadline.
      Me.ProductionOrderCombBox.DataSource = New DataTable
      Me.ProductionOrderCombBox.DisplayMember = _OrderTable.Order_NoColumn.ColumnName
      Me.ProductionOrderCombBox.ValueMember = String.Empty
      Me.ProductionOrderCombBox.DataSource = _OrderTable
      Me.ProductionOrderCombBox.Rows.Band.ColHeadersVisible = False

      General.SetColumnCaptions(DirectCast(_OrderTable, DataTable), Me.Name)
      For I As Int32 = 0 To _OrderTable.Columns.Count - 1
        Select Case _OrderTable.Columns(I).ColumnName
          Case _OrderTable.Order_NoColumn.ColumnName
            Me.ProductionOrderCombBox.Rows.Band.Columns(I).Width = Me.ProductionOrderCombBox.Width - Constants.SCROLLBAR_WIDTH
          Case Else
            Me.ProductionOrderCombBox.Rows.Band.Columns(I).Hidden = True
        End Select
      Next I

      _ProcessBalanceTable = _ProcessBalanceTA.GetByCoIDProcessIDItemCode(Me.LoginInfoObject.CompanyID, Nothing, Me.ItemMultiComboBox.Text)
      Dim _ItemDetailID As Int32 = 0
      Me.ProcessStockSheetView.RowCount = 1
      Me.ProcessStockSheetView.ColumnCount = _ItemSizeTable.Rows.Count + 2
      Me.ProcessStockSheetView.Columns(0).Visible = False
      Me.ProcessStockSheetView.Columns(1).Label = _ProcessBalanceTable.Process_DescColumn.ColumnName
      For I As Int32 = 0 To _ProcessBalanceTable.Rows.Count - 1
        For r As Int32 = 0 To Me.ProcessStockSheetView.RowCount - 1
          If Me.ProcessStockSheetView.GetText(r, 0) = _ProcessBalanceTable(I).Process_ID.ToString OrElse r = ProcessStockSheetView.RowCount - 1 Then
            'Last row is empty if not found then add at last row

            Me.ProcessStockSheetView.SetValue(r, 1, _ProcessBalanceTable(I).Process_Desc)
            For c As Int32 = 0 To _ItemSizeTable.Rows.Count - 1
              Me.ProcessStockSheetView.Columns(c + 2).Label = _ItemSizeTable(c).ItemSize_Desc
              If _ProcessBalanceTable(I).ItemSize_ID = _ItemSizeTable(c).ItemSize_ID Then
                Me.ProcessStockSheetView.SetValue(r, c + 2, _ProcessBalanceTable(I).Quantity)
              End If
            Next
          End If
        Next
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ItemMultiComboBox_qValueChanged of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will populate destination process.
  ''' </summary>
  Private Sub SourceProcessComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SourceProcessComboBox.ValueChanged
    Try
      Me.SourceProcessStockSheetView.RowCount = 1
      For c As Int32 = 0 To SourceProcessStockSheetView.ColumnCount - 1
        Me.SourceProcessStockSheetView.SetValue(0, c, 0)
      Next
      Me.DestinationProcessStockSheetView.RowCount = 1
      For c As Int32 = 0 To DestinationProcessStockSheetView.ColumnCount - 1
        Me.DestinationProcessStockSheetView.SetValue(0, c, 0)
      Next
      'Me.SourceProcessStockSheetView.Columns(Me.SourceProcessStockSheetView.ColumnCount - 1).Formula = "sum(a0:k0)"

      If Me.SourceProcessComboBox.SelectedRow IsNot Nothing Then
        Me.DestinationProcessComboBox.Value = Me.SourceProcessComboBox.SelectedRow.Cells(_ProcessWorkflowTable.ProcessWorkFlow_IDColumn.ColumnName).Text

        For r As Int32 = 0 To _ProcessBalanceTable.Rows.Count - 1
          If Me.SourceProcessComboBox.SelectedRow.Cells(_ProcessWorkflowTable.Source_Process_IDColumn.ColumnName).Text = _ProcessBalanceTable(r).Process_ID.ToString Then
            'Last row is empty if not found then add at last row
            For c As Int32 = 0 To _ItemSizeTable.Rows.Count - 1
              If _ProcessBalanceTable(r).ItemSize_ID = _ItemSizeTable(c).ItemSize_ID Then
                Me.SourceProcessStockSheetView.SetValue(0, c, _ProcessBalanceTable(r).Quantity)
              End If
            Next
          End If
        Next

      Else
        Me.DestinationProcessComboBox.SelectedRow = Nothing
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SourceProcessComboBox_ValueChanged of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Update process balance
  ''' </summary>
  Private Sub DestinationProcessComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DestinationProcessComboBox.ValueChanged
    Try
      For r As Int32 = 0 To _ProcessBalanceTable.Rows.Count - 1
        If Me.DestinationProcessComboBox.SelectedRow.Cells(_ProcessWorkflowTable.Destination_Process_IDColumn.ColumnName).Text = _ProcessBalanceTable(r).Process_ID.ToString Then
          'Last row is empty if not found then add at last row
          For c As Int32 = 0 To _ItemSizeTable.Rows.Count - 1
            If _ProcessBalanceTable(r).ItemSize_ID = _ItemSizeTable(c).ItemSize_ID Then
              Me.DestinationProcessStockSheetView.SetValue(0, c, _ProcessBalanceTable(r).Quantity)
            End If
          Next
        End If
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in DestinationProcessComboBox_ValueChanged of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Populates batches
  ''' </summary>
  Private Sub ProductionOrderCombBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductionOrderCombBox.ValueChanged
    Try
      If Me.ProductionOrderCombBox.SelectedRow IsNot Nothing Then
        _OrderBatchTable = _OrderBatchTA.GetByCoIDOrderID(Me.LoginInfoObject.CompanyID, Convert.ToInt32(Me.ProductionOrderCombBox.SelectedRow.Cells(_OrderTable.Order_IDColumn.ColumnName).Value))

        Me.ProductionOrderBatchComboBox.DataSource = _OrderBatchTable
        For I As Int32 = 0 To _OrderBatchTable.Columns.Count - 1
          Select Case I
            Case _OrderBatchTable.OrderBatch_NoColumn.Ordinal
            Case Else
              Me.ProductionOrderBatchComboBox.Rows.Band.Columns(I).Hidden = False
          End Select
        Next
        Me.ProductionOrderBatchComboBox.Rows.Band.Columns(_OrderBatchTable.OrderBatch_NoColumn.Ordinal).Width = Me.ProductionOrderBatchComboBox.Width - Constants.SCROLLBAR_WIDTH
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ProductionOrderCombBox_ValueChanged of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 28-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Shows record.
  ''' </summary>
  Private Sub ProductionIDComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductionIDComboBox.ValueChanged
    Try
      If Me.ProductionIDComboBox.SelectedRow IsNot Nothing Then
        _ProductionTable = _ProcessProductionTA.GetByCoIDProductionID(Me.LoginInfoObject.CompanyID, Convert.ToInt32(Me.ProductionIDComboBox.Text))

        If _ProductionTable.Rows.Count = 0 Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Production ID not found", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)

        Else
          _ProductionRow = _ProductionTable(0)
          Me.ProductionDateCalendarCombo.Value = _ProductionRow.Production_Date
          If _ProductionRow.IsOrder_IDNull Then
            Me.ProductionOrderCombBox.Value = Nothing
          Else
            Me.ProductionOrderCombBox.Value = _ProductionRow.Order_ID
          End If
          If _ProductionRow.IsOrderBatch_IDNull Then
            Me.ProductionOrderBatchComboBox.Value = Nothing
          Else
            Me.ProductionOrderBatchComboBox.Value = _ProductionRow.OrderBatch_ID
          End If

          _ProductionDetailTable = _ProcessProductionDetailTA.GetByCoIDProductionID(Me.LoginInfoObject.CompanyID, Convert.ToInt32(Me.ProductionIDComboBox.Text))

          Dim _ItemDetailTA As New QuickInventoryDataSetTableAdapters.ItemDetailTableAdapter
          Me.ItemMultiComboBox.Text = _ItemDetailTA.GetItemCodeByCoIDItemDetailID(Me.LoginInfoObject.CompanyID, _ProductionDetailTable(0).Item_Detail_ID)

          Dim _ProcessWorkFlowTA As New QuickProductionDataSetTableAdapters.ProductionProcessWorkFlowTableAdapter
          Dim _ProcessWorkFlowID As Int32
          _ProcessWorkFlowID = Convert.ToInt32(_ProcessWorkFlowTA.GetProcessWorkFlowIDByCoIDSourceAndDestinationProcessID(Me.LoginInfoObject.CompanyID, Convert.ToInt16(_ProductionDetailTable(0).Consumption_Process_ID), Convert.ToInt16(_ProductionDetailTable(0).Production_Process_ID)))
          Me.SourceProcessComboBox.Value = _ProcessWorkFlowID

          For I As Int32 = 0 To _ProductionDetailTable.Rows.Count - 1

            For c As Int32 = 0 To _ItemSizeTable.Rows.Count - 1

              If _ProductionDetailTable(I).ItemSize_ID = _ItemSizeTable(c).ItemSize_ID Then

                Me.ProcessProductionSheetView.SetValue(0, c, _ProductionDetailTable(I).Quantity)

              End If

            Next c

          Next I

          End If

      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ProductionOrderCombBox_ValueChanged of ProcessProduction.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

End Class
