Imports System.Text
Imports QuickLibrary
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDALLibrary
Imports System.Windows.Forms

Public Class BulkTransferForm
#Region "Declaration"
  Private ITEM As String = "Item"
  Private PARTY As String = "Party"
  Private ITEM_CATEGORY As String = "Item Category"
  Private RAW_MATERIAL As String = "Raw Material & Production Forumla"

  Private _ItemTA As New ItemTableAdapter
  Private _ItemDataTable As New ItemDataTable
  Private _PartyTA As New PartyTableAdapter
  Private _PartyDataTable As New PartyDataTable
#End Region

#Region "Events"
  Private Sub BulkTransferForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim _TargetOptions() As String = {ITEM, PARTY, ITEM_CATEGORY, RAW_MATERIAL}
      '_TargetOptions.Add("Item", "Item")

      TargetComboBox.DataSource = _TargetOptions
    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in loading form", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub TargetComboBox_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles TargetComboBox.RowSelected
    Try
      Select Case TargetComboBox.Text
        Case ITEM
          SetItemColumns()
        Case ITEM_CATEGORY
          SetItemCategoryColumns()
        Case PARTY
          SetPartyColumns()
        Case RAW_MATERIAL
          SetRawMaterialColumns()
        Case Else
          MessageBox.Show("This option in not available yet or you have selected invalid option", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
      End Select
    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in selecting target option", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

#Region "Toolbar methods"
  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim _MethodResult As Constants.MethodResult

      Cursor = Cursors.WaitCursor

      If LoginInfoObject.CompanyID = 0 Then
        MessageBox.Show("Please log into specific compnay to save data", "Select Company", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Else
        Select Case TargetComboBox.Text
          Case ITEM
            _MethodResult = SaveItems(False)
          Case ITEM_CATEGORY
            _MethodResult = SaveItems(True)
          Case PARTY
            _MethodResult = SaveParties()
          Case RAW_MATERIAL
            _MethodResult = SaveRawMaterialAndFormula()
          Case Else
            MessageBox.Show("This option in not available yet or you have selected invalid option", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
      End If

      If _MethodResult = Constants.MethodResult.Success Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Records were transfered successfully", QuickMessageBox.MessageBoxTypes.ShortMessage)
        Return True
      ElseIf _MethodResult = Constants.MethodResult.PartialSucceded Then
        QuickMessageBox.Show(Me.LoginInfoObject, "All records were not transfered successfully", QuickMessageBox.MessageBoxTypes.ShortMessage)
        Return True
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "Operation was not successfull", QuickMessageBox.MessageBoxTypes.ShortMessage)
        Return False
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in saving records", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Function

  Protected Overrides Sub OpenFileButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim OpenFileDialog As OpenFileDialog

      Cursor = Cursors.WaitCursor

      OpenFileDialog = New OpenFileDialog
      OpenFileDialog.ShowDialog(Me)
      If OpenFileDialog.FileName <> String.Empty Then
        Me.spread.OpenExcel(OpenFileDialog.FileName)
      End If

      MyBase.CancelButtonClick(sender, e)
    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in saving records", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub PasteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      ''Me.spread.ActiveSheet.ClipboardPaste(FarPoint.Win.Spread.ClipboardPasteOptions.Values)
      'Dim ms As System.IO.MemoryStream = CType(Clipboard.GetData("CSV"), System.IO.MemoryStream)
      'Dim b(CInt(ms.Length)) As Byte
      'ms.Read(b, 0, CInt(ms.Length))
      Dim _TextCellType As New FarPoint.Win.Spread.CellType.TextCellType

      'Debug.Write(System.Text.UTF7Encoding.UTF8.GetString(b))
      Me.Cursor = Cursors.WaitCursor

      Dim Rows() As String
      Dim Cols() As String

      Rows = Split(Clipboard.GetText, vbCrLf)
      Me.Quick_UltraProgressBar1.Maximum = Rows.Length
      Me.spread_Sheet1.Rows.Count = Rows.Length
      For i As Int32 = 0 To Rows.Length - 1
        Cols = Split(Rows(i), vbTab)
        For j As Int32 = 0 To Cols.Length - 1
          If i = 0 Then Me.spread_Sheet1.Columns(j).CellType = _TextCellType
          Me.spread_Sheet1.SetText(i, j, Cols(j))
        Next
        Application.DoEvents()
        Me.Quick_UltraProgressBar1.Value = i + 1
      Next

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in PasteButtonClick event method " & Me.Name, ex)
      _QuickException.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Methods"
  Private Function SaveItems(ByVal IsCategories As Boolean) As QuickLibrary.Constants.MethodResult
    Dim _ItemCode As String = String.Empty
    Dim _ItemColumns As Int32
    Dim I As Int32

    SaveItems = Constants.MethodResult.Failed

    Try
      Dim _ItemRow As ItemRow
      Dim _ItemDataTable As ItemDataTable
      Dim _ItemTA As New ItemTableAdapter
      '<<<<<<<<<< Start New Item Table
      Dim _NewItemRow As Invs_ItemRow
      Dim _NewItemTable As Invs_ItemDataTable
      Dim _NewItemTA As New Invs_ItemTableAdapter
      Dim _ItemDetailRow As ItemDetailRow = Nothing
      Dim _ItemDetailTable As ItemDetailDataTable = Nothing
      Dim _ItemDetailTA As New ItemDetailTableAdapter
      Dim _ItemSizeTable As ItemSizeDataTable
      Dim _ItemSizeTA As New ItemSizeTableAdapter
      '>>>>>>>>>> End New Item Table

      Me.Quick_UltraProgressBar1.Maximum = Me.spread.ActiveSheet.NonEmptyRowCount

      For I = 0 To Me.spread.ActiveSheet.NonEmptyRowCount - 1
        If Not IsCategories Then
          _ItemCode = spread.GetItemCode(Me.spread.ActiveSheet, I)
          _ItemColumns = General.ItemCodeColumnsCount
        Else
          _ItemCode = Me.spread.ActiveSheet.GetText(I, 0)
          _ItemColumns = 1
        End If

        If _ItemCode = String.Empty Then
          If MessageBox.Show("There is no item code on row " & I.ToString & ", do you wish to continue?", "Empty Item Code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit For
          End If
        End If

        If _ItemCode <> String.Empty Then
          _ItemDataTable = _ItemTA.GetByItemCodeAndCoID(_ItemCode, LoginInfoObject.CompanyID)
          If _ItemDataTable.Rows.Count = 0 Then
            _ItemRow = _ItemDataTable.NewItemRow
            _ItemRow.Item_ID = Convert.ToInt32(_ItemTA.GetNewItemIDByCoID(LoginInfoObject.CompanyID))
            _ItemRow.Item_Code = _ItemCode
            _ItemRow.Co_ID = LoginInfoObject.CompanyID
            _ItemRow.Item_SaleRate_Size01 = 0 : _ItemRow.Item_SaleRate_Size02 = 0 : _ItemRow.Item_SaleRate_Size03 = 0 : _ItemRow.Item_SaleRate_Size04 = 0 : _ItemRow.Item_SaleRate_Size05 = 0 : _ItemRow.Item_SaleRate_Size06 = 0 : _ItemRow.Item_SaleRate_Size07 = 0 : _ItemRow.Item_SaleRate_Size08 = 0 : _ItemRow.Item_SaleRate_Size09 = 0 : _ItemRow.Item_SaleRate_Size10 = 0 : _ItemRow.Item_SaleRate_Size11 = 0 : _ItemRow.Item_SaleRate_Size12 = 0 : _ItemRow.Item_SaleRate_Size13 = 0

          Else
            _ItemRow = _ItemDataTable(0)
          End If
          If spread.ActiveSheet.GetText(I, _ItemColumns).Length > 50 Then
            _ItemRow.Item_Desc = spread.ActiveSheet.GetText(I, _ItemColumns).Substring(0, 50)
          Else
            _ItemRow.Item_Desc = spread.ActiveSheet.GetText(I, _ItemColumns)
          End If
          _ItemRow.Parent_Item_ID = 0
          _ItemRow.Party_ID = 0
          _ItemRow.Address_ID = 0
          _ItemRow.Stamp_UserID = LoginInfoObject.UserID
          _ItemRow.Stamp_DateTime = Common.SystemDateTime

          '<<<<<<<<<< Start New Item table
          _NewItemTable = _NewItemTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, _ItemCode)
          If _NewItemTable.Rows.Count = 0 Then
            _NewItemRow = _NewItemTable.NewInvs_ItemRow
            With _NewItemRow
              .Item_ID = _NewItemTA.GetNewItemIDByCoID(Me.LoginInfoObject.CompanyID).Value
              .Item_Code = _ItemCode
              .Co_ID = Me.LoginInfoObject.CompanyID
            End With
          Else
            _NewItemRow = _NewItemTable(0)
          End If
          With _NewItemRow
            If spread_Sheet1.GetText(I, _ItemColumns).Length > 50 Then
              .Item_Desc = spread_Sheet1.GetText(I, _ItemColumns).Substring(0, 50)
            Else
              .Item_Desc = spread.ActiveSheet.GetText(I, _ItemColumns)
            End If
            .Is_RawMaterial = False
            .Parent_Item_ID = 0
            .Party_ID = 0
            .Address_ID = 0
            .Stamp_UserID = LoginInfoObject.UserID
            .Stamp_DateTime = Common.SystemDateTime
          End With
          '>>>>>>>>>> End New Item table

          If Not IsCategories Then
            Select Case spread.ActiveSheet.GetText(I, _ItemColumns + 1)
              Case General.UserInputForItemSize(0), Constants.ITEM_SIZE_01_ALIAS
                _ItemRow.Item_SaleRate_Size01 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(1), Constants.ITEM_SIZE_02_ALIAS
                _ItemRow.Item_SaleRate_Size02 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(2), Constants.ITEM_SIZE_03_ALIAS
                _ItemRow.Item_SaleRate_Size03 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(3), Constants.ITEM_SIZE_04_ALIAS
                _ItemRow.Item_SaleRate_Size04 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(4), Constants.ITEM_SIZE_05_ALIAS
                _ItemRow.Item_SaleRate_Size05 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(5), Constants.ITEM_SIZE_06_ALIAS
                _ItemRow.Item_SaleRate_Size06 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(6), Constants.ITEM_SIZE_07_ALIAS
                _ItemRow.Item_SaleRate_Size07 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(7), Constants.ITEM_SIZE_08_ALIAS
                _ItemRow.Item_SaleRate_Size08 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(8), Constants.ITEM_SIZE_09_ALIAS
                _ItemRow.Item_SaleRate_Size09 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(9), Constants.ITEM_SIZE_10_ALIAS
                _ItemRow.Item_SaleRate_Size10 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(10), Constants.ITEM_SIZE_11_ALIAS
                _ItemRow.Item_SaleRate_Size11 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(11), Constants.ITEM_SIZE_12_ALIAS
                _ItemRow.Item_SaleRate_Size12 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
              Case General.UserInputForItemSize(12), Constants.ITEM_SIZE_13_ALIAS
                _ItemRow.Item_SaleRate_Size13 = Convert.ToDecimal(spread.ActiveSheet.GetText(I, _ItemColumns + 2))
            End Select

            '<<<<<<<<<< Start New Item Table
            _ItemSizeTable = _ItemSizeTA.GetByCoIDSizeDesc(Me.LoginInfoObject.CompanyID, spread_Sheet1.GetText(I, _ItemColumns + 1))
            If _ItemSizeTable.Rows.Count > 0 Then
              _ItemDetailTable = _ItemDetailTA.GetByCoIDItemCodeItemSizeID(Me.LoginInfoObject.CompanyID, _ItemCode, _ItemSizeTable(0).ItemSize_ID)
              If _ItemDetailTable.Rows.Count = 0 Then
                _ItemDetailRow = _ItemDetailTable.NewItemDetailRow
                _ItemDetailRow.Co_ID = Me.LoginInfoObject.CompanyID
                _ItemDetailRow.Item_ID = _NewItemRow.Item_ID
                _ItemDetailRow.Item_Detail_ID = _ItemDetailTA.GetNewItemDetailID(Me.LoginInfoObject.CompanyID).Value
                _ItemDetailRow.Maximum_Quantity = 0
                _ItemDetailRow.Minimum_Quantity = 0
                _ItemDetailRow.Opening_Quantity = 0
                _ItemDetailRow.Opening_Value = 0
                _ItemDetailRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
                _ItemDetailRow.Sale_Rate = Convert.ToDecimal(spread_Sheet1.GetText(I, _ItemColumns + 2))
              Else
                _ItemDetailRow = _ItemDetailTable(0)
                _ItemDetailRow.RecordStatus_ID = Constants.RecordStatuses.Updated
              End If

              'Set common properties for insert and update
              _ItemDetailRow.ItemSize_ID = _ItemSizeTable(0).ItemSize_ID
              _ItemDetailRow.Stamp_DateTime = Common.SystemDateTime
              _ItemDetailRow.Stamp_UserID = Me.LoginInfoObject.UserID

            Else
              If QuickMessageBox.Show(Me.LoginInfoObject, "Invalid size on row " & I.ToString & ", do you wish to continue?", MessageBoxButtons.YesNo, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit For
              End If
            End If
            '>>>>>>>>>> End New Item Table
          Else
            _ItemRow.Item_SaleRate_Size01 = 0 : _ItemRow.Item_SaleRate_Size02 = 0
            _ItemRow.Item_SaleRate_Size03 = 0 : _ItemRow.Item_SaleRate_Size04 = 0
            _ItemRow.Item_SaleRate_Size05 = 0 : _ItemRow.Item_SaleRate_Size06 = 0
            _ItemRow.Item_SaleRate_Size07 = 0 : _ItemRow.Item_SaleRate_Size08 = 0
            _ItemRow.Item_SaleRate_Size09 = 0 : _ItemRow.Item_SaleRate_Size10 = 0
            _ItemRow.Item_SaleRate_Size11 = 0 : _ItemRow.Item_SaleRate_Size12 = 0
            _ItemRow.Item_SaleRate_Size13 = 0
          End If

          'Update database
          If _ItemRow.RowState = DataRowState.Detached Then _ItemDataTable.Rows.Add(_ItemRow)
          _ItemTA.Update(_ItemDataTable)

          '<<<<<<<<<< Start New Item Table
          If _NewItemRow.RowState = DataRowState.Detached Then _NewItemTable.Rows.Add(_NewItemRow)
          _NewItemTA.Update(_NewItemRow)
          If _ItemDetailRow IsNot Nothing Then
            If _ItemDetailRow.RowState = DataRowState.Detached Then _ItemDetailTable.Rows.Add(_ItemDetailRow)
            _ItemDetailTA.Update(_ItemDetailRow)
          End If
          '>>>>>>>>>> End New Item Table
        End If

        Me.Quick_UltraProgressBar1.Value = I
        'My.Application.doevents()
        Me.Quick_UltraProgressBar1.Refresh()
      Next

      If I < Me.spread_Sheet1.NonEmptyRowCount - 1 Then
        Return Constants.MethodResult.PartialSucceded
      Else
        Return Constants.MethodResult.Success
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickException("Exception in saving item with code " & _ItemCode & ", row no. " & I, ex)
      Throw ExceptionObject
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will save raw material and formula for production
  ''' </summary>
  Private Function SaveRawMaterialAndFormula() As QuickLibrary.Constants.MethodResult
    Try
      Dim _ItemCode As String = String.Empty
      Dim _ItemSizeTA As New ItemSizeTableAdapter
      Dim _FormulaTA As New QuickProductionDataSetTableAdapters.FormulaTableAdapter
      Dim _FormulaDetailTA As New QuickProductionDataSetTableAdapters.FormulaDetailTableAdapter
      Dim _ItemDetailTA As New ItemDetailTableAdapter
      Dim _ItemTA As New Invs_ItemTableAdapter

      Dim _ItemSizeTable As ItemSizeDataTable
      Dim _FormulaTable As QuickProductionDataSet.FormulaDataTable
      Dim _FormulaDetailTable As QuickProductionDataSet.FormulaDetailDataTable
      Dim _OutputItemTable As ItemDetailDataTable
      Dim _InputItemTable As Invs_ItemDataTable
      Dim _InputItemDetailTable As ItemDetailDataTable

      Dim _InputItemRow As Invs_ItemRow
      Dim _FormulaRow As QuickProductionDataSet.FormulaRow
      Dim _FormulaDetailRow As QuickProductionDataSet.FormulaDetailRow
      Dim _InputItemDetailRow As ItemDetailRow

      SaveRawMaterialAndFormula = Constants.MethodResult.Failed

      Me.Quick_UltraProgressBar1.Maximum = Me.spread_Sheet1.NonEmptyRowCount
      _ItemSizeTable = _ItemSizeTA.GetByCoID(Me.LoginInfoObject.CompanyID)

      For r As Int32 = 1 To Me.spread_Sheet1.NonEmptyRowCount - 1
        _OutputItemTable = _ItemDetailTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, spread.GetItemCode(spread_Sheet1, r))
        If _OutputItemTable.Rows.Count > 0 Then
          'Repeat following formulas for all sizes of item
          For item1 As Int32 = 0 To _OutputItemTable.Rows.Count - 1
            _FormulaTable = _FormulaTA.GetByCoIDOutputItemID(Me.LoginInfoObject.CompanyID, _OutputItemTable(item1).Item_Detail_ID)
            If _FormulaTable.Rows.Count = 0 Then
              'Insert
              _FormulaRow = _FormulaTable.NewFormulaRow
              _FormulaRow.Co_ID = Me.LoginInfoObject.CompanyID
              _FormulaRow.Formula_Code = spread.GetItemCode(spread_Sheet1, r)
              _FormulaRow.Formula_ID = _FormulaTA.GetNewFormulaID(Me.LoginInfoObject.CompanyID).Value
              _FormulaRow.Output_Item_Detail_ID = _OutputItemTable(item1).Item_Detail_ID
              _FormulaRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
            Else
              'Update
              _FormulaRow = _FormulaTable(0)
              _FormulaRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            End If
            'Set common properties for insert and update
            _FormulaRow.Formula_Description = spread_Sheet1.GetText(r, General.ItemCodeColumnsCount + 2)
            _FormulaRow.Stamp_DateTime = Common.SystemDateTime
            _FormulaRow.Stamp_UserID = Me.LoginInfoObject.UserID
            If _FormulaRow.RowState = DataRowState.Detached Then _FormulaTable.Rows.Add(_FormulaRow)
            _FormulaTA.Update(_FormulaRow)

            For c As Int32 = General.ItemCodeColumnsCount + 4 To spread_Sheet1.NonEmptyColumnCount - 1 Step 2
              _InputItemTable = _ItemTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, Me.spread_Sheet1.GetText(0, c).Substring(0, 2))
              If _InputItemTable.Rows.Count = 0 Then
                _InputItemRow = _InputItemTable.NewInvs_ItemRow
                _InputItemRow.Address_ID = 0
                _InputItemRow.Co_ID = Me.LoginInfoObject.CompanyID
                _InputItemRow.Is_RawMaterial = True
                _InputItemRow.Item_Code = Me.spread_Sheet1.GetText(0, c).Substring(0, 2)
                _InputItemRow.Item_ID = _ItemTA.GetNewItemIDByCoID(Me.LoginInfoObject.CompanyID).Value
                _InputItemRow.Parent_Item_ID = 0
                _InputItemRow.Party_ID = 0
                _InputItemRow.RecordStatus_ID = Constants.RecordStatuses.Inserted

              Else
                _InputItemRow = _InputItemTable(0)
                _InputItemRow.RecordStatus_ID = Constants.RecordStatuses.Updated
              End If
              'Set common properties of insert and update
              _InputItemRow.Stamp_DateTime = Common.SystemDateTime
              _InputItemRow.Stamp_UserID = Me.LoginInfoObject.UserID
              _InputItemRow.Item_Desc = Me.spread_Sheet1.GetText(0, c).Substring(2, Me.spread_Sheet1.GetText(0, c).Length - 2)
              If _InputItemRow.RowState = DataRowState.Detached Then _InputItemTable.Rows.Add(_InputItemRow)
              _ItemTA.Update(_InputItemRow)

              'Insert/Update item detail
              _InputItemDetailTable = _ItemDetailTA.GetByCoIDItemCode(Me.LoginInfoObject.CompanyID, _InputItemRow.Item_Code)
              If _InputItemDetailTable.Rows.Count = 0 Then
                'Insert
                _InputItemDetailRow = _InputItemDetailTable.NewItemDetailRow
                With _InputItemDetailRow
                  .Co_ID = Me.LoginInfoObject.CompanyID
                  .SetColor_IDNull()
                  .Item_Detail_ID = _ItemDetailTA.GetNewItemDetailID(Me.LoginInfoObject.CompanyID).Value
                  .Item_ID = _InputItemRow.Item_ID
                  .SetItemGrade_IDNull()
                  .SetItemSize_IDNull()
                  .Maximum_Quantity = 0
                  .Minimum_Quantity = 0
                  .Opening_Quantity = 0
                  .Opening_Value = 0
                  .RecordStatus_ID = Constants.RecordStatuses.Inserted
                  .Sale_Rate = 0
                End With
              Else
                'Update
                _InputItemDetailRow = _InputItemDetailTable(0)
              End If
              _InputItemDetailRow.Stamp_DateTime = Common.SystemDateTime
              _InputItemDetailRow.Stamp_UserID = Me.LoginInfoObject.UserID

              If _InputItemDetailRow.RowState = DataRowState.Detached Then _InputItemDetailTable.Rows.Add(_InputItemDetailRow)
              _ItemDetailTA.Update(_InputItemDetailRow)

              'Insert/Update Formula Detail
              _FormulaDetailTable = _FormulaDetailTA.GetByCoIDFormulaIDInputItemDetailID(Me.LoginInfoObject.CompanyID, _FormulaRow.Formula_ID, _InputItemDetailRow.Item_Detail_ID)
              If _FormulaDetailTable.Rows.Count = 0 Then
                _FormulaDetailRow = _FormulaDetailTable.NewFormulaDetailRow

                With _FormulaDetailRow
                  .Co_ID = Me.LoginInfoObject.CompanyID
                  .Formula_Detail_ID = _FormulaDetailTA.GetNewFormulaDetailID(Me.LoginInfoObject.CompanyID, _FormulaRow.Formula_ID).Value
                  .Formula_ID = _FormulaRow.Formula_ID
                  .Input_Item_Detail_ID = _InputItemDetailRow.Item_Detail_ID
                  .RecordStatus_ID = Constants.RecordStatuses.Inserted
                End With
              Else
                _FormulaDetailRow = _FormulaDetailTable(0)
                _FormulaDetailRow.RecordStatus_ID = Constants.RecordStatuses.Updated
              End If

              With _FormulaDetailRow
                '.Item_Desc = spread_Sheet1.GetText(r, General.ItemCodeColumnsCount + c)
                .Quantity = 0
                Decimal.TryParse(spread_Sheet1.GetText(r, General.ItemCodeColumnsCount + c + 1), .Quantity)
                .Remarks = spread_Sheet1.GetText(r, General.ItemCodeColumnsCount + c)
                .Stamp_DateTime = Common.SystemDateTime
                .Stamp_UserID = Me.LoginInfoObject.UserID
              End With

              If _FormulaDetailRow.RowState = DataRowState.Detached Then _FormulaDetailTable.Rows.Add(_FormulaDetailRow)
              _FormulaDetailTA.Update(_FormulaDetailRow)

            Next c
          Next item1
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "Row No " & (r + 1).ToString & ", Item code is not defined", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Return Constants.MethodResult.Failed
        End If

        Me.Quick_UltraProgressBar1.Value = r
        Me.Quick_UltraProgressBar1.Refresh()
        Application.DoEvents()
      Next r

      Me.Quick_UltraProgressBar1.Value = Me.Quick_UltraProgressBar1.Maximum
      Return Constants.MethodResult.Success

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveRawMaterialAndFormula of BulkTransferForm.", ex)
      Throw _qex
    End Try
  End Function


  Private Function SaveParties() As Constants.MethodResult
    Try
      Dim _PartyRow As PartyRow
      Dim _PartyDataTable As PartyDataTable
      Dim _PartyTA As New PartyTableAdapter
      Dim _PartyCode As String
      Dim _PartyCodeColumns As Int32
      Dim I As Int32

      SaveParties = Constants.MethodResult.Failed

      Me.Quick_UltraProgressBar1.Maximum = Me.spread.ActiveSheet.NonEmptyRowCount

      For I = 0 To Me.spread.ActiveSheet.NonEmptyRowCount - 1
        _PartyCode = Me.spread.ActiveSheet.GetText(I, 0)
        _PartyCodeColumns = 1

        If _PartyCode = String.Empty Then
          If MessageBox.Show("There is no party code on row " & I.ToString & ", do you wish to continue?", "Empty Item Code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit For
          End If
        End If

        If _PartyCode <> String.Empty Then
          _PartyDataTable = _PartyTA.GetByCoIdAndPartyCode(LoginInfoObject.CompanyID, _PartyCode)
          If _PartyDataTable.Rows.Count = 0 Then
            _PartyRow = _PartyDataTable.NewPartyRow
            _PartyRow.Party_ID = Convert.ToInt32(_PartyTA.GetNewPartyIDByCoID(LoginInfoObject.CompanyID))
            _PartyRow.Party_Code = _PartyCode
            _PartyRow.Co_ID = LoginInfoObject.CompanyID
          Else
            _PartyRow = DirectCast(_PartyDataTable.Rows(0), PartyRow)
          End If
          With _PartyRow
            .Party_Desc = spread.ActiveSheet.GetText(I, 1)
            .Opening_Cr = 0
            .Opening_Dr = 0
            .Commission = 0
            .EntityType_ID = Constants.EntityTypes.CustomerAndSupplier
            .City = spread.ActiveSheet.GetText(I, 3)
            .State = spread.ActiveSheet.GetText(I, 4)
            .ZipCode = spread.ActiveSheet.GetText(I, 5)
            .Address = spread.ActiveSheet.GetText(I, 6)
            .Phone = spread.ActiveSheet.GetText(I, 7)
            .Fax = spread.ActiveSheet.GetText(I, 8)
            .Email = spread.ActiveSheet.GetText(I, 9)
            .URL = spread.ActiveSheet.GetText(I, 10)
            .Stamp_UserID = LoginInfoObject.UserID
            .Stamp_DateTime = Common.SystemDateTime
          End With

          'Update database
          If _PartyRow.RowState = DataRowState.Detached Then _PartyDataTable.Rows.Add(_PartyRow)
          _PartyTA.Update(_PartyDataTable)
        End If

        Me.Quick_UltraProgressBar1.Value = I
        'My.Application.doevents()
        Me.Quick_UltraProgressBar1.Refresh()
      Next

      If I < Me.spread_Sheet1.NonEmptyRowCount - 1 Then
        Return Constants.MethodResult.PartialSucceded
      Else
        Return Constants.MethodResult.Success
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickException("Exception in saving items", ex)
      Throw ExceptionObject
    End Try
  End Function

  Private Sub SetItemColumns()
    Try
      spread.ActiveSheet.Columns.Count = General.ItemCodeColumnsCount + 3

      spread.ItemSheetView = spread_Sheet1
      spread.ItemCodeFirstColumnIndex = 0
      spread.SetItemCodeColumns()

      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 0).Label = "Description"
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 0).Width = 200
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 1).Label = "Size"
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 1).Width = 100
      'spread.ActiveSheet.Columns(_ItemColumns + 2).Label = "Qty"
      'spread.ActiveSheet.Columns(_ItemColumns + 2).Width = 100
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 2).Label = "Sale Rate"
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 2).Width = 100

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in SetItemColumns method.", ex)
    End Try
  End Sub

  Private Sub SetItemCategoryColumns()
    Try

      spread.ActiveSheet.Columns.Count = 2

      spread.ActiveSheet.Columns(0).Label = "Item Category Code"
      spread.ActiveSheet.Columns(0).Width = 100
      spread.ActiveSheet.Columns(1).Label = "Description"
      spread.ActiveSheet.Columns(1).Width = 200

    Catch ex As Exception
      Dim ExceptionObject As New QuickException("Exception in setting item category columns appearance", ex)
      Throw ExceptionObject
    End Try
  End Sub

  Private Sub SetPartyColumns()
    Try
      spread.ActiveSheet.Columns.Count = 11

      General.SetColumnCaptions(DirectCast(_PartyDataTable, DataTable), Me.Name)

      With _PartyDataTable
        spread.ActiveSheet.Columns(0).Label = .Party_CodeColumn.Caption
        spread.ActiveSheet.Columns(0).Width = 100
        spread.ActiveSheet.Columns(1).Label = .Party_DescColumn.Caption
        spread.ActiveSheet.Columns(1).Width = 200
        spread.ActiveSheet.Columns(2).Label = "Contact"
        spread.ActiveSheet.Columns(2).Width = 200
        spread.ActiveSheet.Columns(3).Label = .CityColumn.Caption
        spread.ActiveSheet.Columns(3).Width = 200
        spread.ActiveSheet.Columns(4).Label = .StateColumn.Caption
        spread.ActiveSheet.Columns(4).Width = 200
        spread.ActiveSheet.Columns(5).Label = .ZipCodeColumn.Caption
        spread.ActiveSheet.Columns(5).Width = 200
        spread.ActiveSheet.Columns(6).Label = .AddressColumn.Caption
        spread.ActiveSheet.Columns(6).Width = 200
        spread.ActiveSheet.Columns(7).Label = .PhoneColumn.Caption
        spread.ActiveSheet.Columns(7).Width = 200
        spread.ActiveSheet.Columns(8).Label = .FaxColumn.Caption
        spread.ActiveSheet.Columns(8).Width = 200
        spread.ActiveSheet.Columns(9).Label = .EmailColumn.Caption
        spread.ActiveSheet.Columns(9).Width = 200
        spread.ActiveSheet.Columns(10).Label = .URLColumn.Caption
        spread.ActiveSheet.Columns(10).Width = 200
      End With

    Catch ex As Exception
      Dim ExceptionObject As New QuickException("Exception in setting party columns appearance", ex)
      Throw ExceptionObject
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-Nov-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will set the columns for Raw Materials import
  ''' </summary>
  Private Sub SetRawMaterialColumns()
    Try
      spread_Sheet1.Columns.Count = 200 + General.ItemCodeColumnsCount

      spread.ItemSheetView = spread_Sheet1
      spread.ItemCodeFirstColumnIndex = 0
      spread.SetItemCodeColumns()
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 0).Label = "Empty"
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 0).Width = 100
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 1).Label = "Empty"
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 1).Width = 100
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 2).Label = "Empty"
      spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + 2).Width = 100
      For I As Int32 = General.ItemCodeColumnsCount + 1 To 200 Step 2
        spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + I).Label = "Raw Material Desc"
        spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + I).Width = 100
        spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + I + 1).Label = "Raw Material Qty"
        spread.ActiveSheet.Columns(General.ItemCodeColumnsCount + I + 1).Width = 100
      Next I

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetRawMaterialColumns of BulkTransferForm.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ToolbarMode = ToolbarModes.TransferFromFile
  End Sub

End Class
