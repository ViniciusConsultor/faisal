Imports QuickLibrary
Imports QuickDAL
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDALLibrary
Imports System.Windows.Forms

Public Class ItemSpread

#Region "Declaration"

  'Private _IncludeItemCode As Boolean = False
  Private _ItemCodeFirstColumnIndex As Int32 = -1
  Private _ItemQtyFirstColumnIndex As Int32 = -1
  Private _ItemIDColumnIndex As Int32 = -1
  Private _ItemDescColumnIndex As Int32 = -1
  Private _LoginInfoObject As LoginInfo = Nothing
  Private _ItemSizeColumnIndex As Int32 = -1
  Private _ItemRateFirstColumnIndex As Int32 = -1
  Private WithEvents _ItemSheetView As FarPoint.Win.Spread.SheetView
  Private _LastEditableColumnIndex As Int32 = -1
  Private _ItemTableAdapter As New ItemTableAdapter
  Private _ItemDataTableForColumnNames As New ItemDataTable
  Private _DataTable As DataTable
  Private _SpreadColumns As New Collection
  Private _SizesHorizontal As Boolean

#End Region

#Region "Shared Methods"

#End Region

#Region "Properties"
  'Just an indicator that Item Code related functions should be executed.
  'Public Property IncludeItemCode() As Boolean
  '  Get
  '    Return _IncludeItemCode
  '  End Get
  '  Set(ByVal value As Boolean)
  '    _IncludeItemCode = value
  '  End Set
  'End Property

  Private _ItemSummaryBarObject As ItemSummaryBar
  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 9-Jul-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' If this property is set then it will update the item summary bar automatically.
  ''' </summary>
  Public Property ItemSummaryBarObject() As ItemSummaryBar
    Get
      Try

        Return _ItemSummaryBarObject

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ItemSummaryBarObject of ItemSpreadView.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As ItemSummaryBar)
      Try

        _ItemSummaryBarObject = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ItemSummaryBarObject of ItemSpreadView.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Dec-2009
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It gets/sets the first column index of the quantity column.
  ''' </summary>
  Public Property ItemQtyFirstColumnIndex() As Int32
    Get
      Try

        Return _ItemQtyFirstColumnIndex

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ItemQtyFirstColumnIndex of ItemSpread.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Int32)
      Try

        _ItemQtyFirstColumnIndex = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ItemQtyFirstColumnIndex of ItemSpread.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Public Property SizesHorizontal() As Boolean
    Get
      Return _SizesHorizontal
    End Get
    Set(ByVal value As Boolean)
      _SizesHorizontal = value
    End Set
  End Property

  Public Property ItemSheetView() As ItemSpreadView
    Get
      Return _ItemSheetView
    End Get
    Set(ByVal value As ItemSpreadView)
      Try
        _ItemSheetView = value
        LastEditableColumnIndex()

      Catch ex As Exception
        Throw New QuickExceptionAdvanced("Exception in Set method of ItemSheetView property.", ex)
      End Try
    End Set
  End Property

  Public Property ItemCodeFirstColumnIndex() As Int32
    Get
      Return _ItemCodeFirstColumnIndex
    End Get
    Set(ByVal value As Int32)
      _ItemCodeFirstColumnIndex = value
    End Set
  End Property

  Public Property ItemIDColumnIndex() As Int32
    Get
      Return _ItemIDColumnIndex
    End Get
    Set(ByVal value As Int32)
      _ItemIDColumnIndex = value
    End Set
  End Property

  Public Property ItemDescColumnIndex() As Int32
    Get
      Return _ItemDescColumnIndex
    End Get
    Set(ByVal value As Int32)
      Try
        _ItemDescColumnIndex = value
        If ItemSheetView IsNot Nothing AndAlso ItemSheetView.ShowTotalsRow AndAlso _ItemDescColumnIndex >= 0 Then
          If _ItemSheetView IsNot Nothing Then
            _ItemSheetView.ColumnHeader.RowCount = 2
            _ItemSheetView.ColumnHeader.Cells(0, 0).Text = "Totals"
            Dim _Font As New Drawing.Font(Me.Font, Drawing.FontStyle.Bold)
            _ItemSheetView.ColumnHeader.Rows(0).Font = _Font
            _ItemSheetView.ColumnHeader.Rows(0).BackColor = Drawing.Color.White
            _ItemSheetView.Models.ColumnHeaderSpan.Add(0, 0, 1, _ItemDescColumnIndex + 1)
          End If
        End If

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ItemDescColumnIndex of ItemSpread.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Public Property ItemSizeColumnIndex() As Int32
    Get
      Return _ItemSizeColumnIndex
    End Get
    Set(ByVal value As Int32)
      _ItemSizeColumnIndex = value
    End Set
  End Property

  Public Property ItemRateFirstColumnIndex() As Int32
    Get
      Return _ItemRateFirstColumnIndex
    End Get
    Set(ByVal value As Int32)
      _ItemRateFirstColumnIndex = value
    End Set
  End Property

  Public Property LoginInfoObject() As LoginInfo
    Get
      Return _LoginInfoObject
    End Get
    Set(ByVal value As LoginInfo)
      _LoginInfoObject = value
    End Set
  End Property

#End Region

#Region "Method"
  Public Sub New()
    Try
      ' This call is required by the Windows Form Designer.
      InitializeComponent()

      ' Add any initialization after the InitializeComponent() call.
      Dim inputmap As New FarPoint.Win.Spread.InputMap
      inputmap = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
      inputmap.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
      inputmap = New FarPoint.Win.Spread.InputMap
      inputmap = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
      inputmap.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in the constructor of ItemSpread.", ex)
    End Try
  End Sub

  <System.ComponentModel.Description("This method receives datatable and generates/updates with the sizes values in the grid. You can furthure provide the column which must be autoincremented for new rows.")> _
  Public Function UpdateDatatable(ByVal _DataTablepara As Invs_InventoryDetailDataTable) As DataTable
    Try
      For r As Int32 = 0 To _ItemSheetView.RowCount - 1
        For c As Int32 = _ItemQtyFirstColumnIndex To _ItemQtyFirstColumnIndex + DatabaseCache.GetItemSizes.Count - 1
          _DataTablepara.DefaultView.RowFilter = _DataTablepara.Item_IDColumn.ColumnName & "=" _
            & _ItemSheetView.GetText(r, Convert.ToInt32(_SpreadColumns(_DataTablepara.Item_IDColumn.ColumnName))) _
            & " AND " & DatabaseCache.GetItemSizes.ItemSize_IDColumn.ColumnName & "=" & _ItemSheetView.Columns(c).Tag.ToString
          If _DataTablepara.DefaultView.Count > 0 Then
          End If
        Next
      Next

      UpdateDatatable = Nothing
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in UpdateDatatable method of ItemSpread.", ex)
      Throw _qex
    End Try
  End Function

  Public Sub SetItemCodeColumns(ByVal _AddColumns As Boolean)
    Try

      If ItemCodeFirstColumnIndex >= 0 AndAlso Me.ItemSheetView IsNot Nothing Then
        'If IncludeItemCode AndAlso ItemCodeFirstColumnIndex >= 0 Then
        'If Item Code should be in multiple columns
        If Constants.ITEM_MULTIPLE_COLUMNS Then
          'Datatable is assigned to sheet then columns must be added in datatable
          Dim _ItemCellType As New FarPoint.Win.Spread.CellType.TextCellType

          If _AddColumns Then ItemSheetView.Columns.Add(ItemCodeFirstColumnIndex, General.ItemCodeColumnsCount - 1)

          For I As Int32 = ItemCodeFirstColumnIndex To (ItemCodeFirstColumnIndex + General.ItemCodeColumnsCount - 1)
            ItemSheetView.Columns(I).Label = DatabaseCache.GetItemLeveling(I - ItemCodeFirstColumnIndex).Description
            ItemSheetView.Columns(I).CellType = _ItemCellType
            _ItemCellType.MaxLength = DatabaseCache.GetItemLeveling(I - ItemCodeFirstColumnIndex).Length
            ItemSheetView.Columns(I).Width = 60
          Next

        Else
          ItemSheetView.Columns(ItemCodeFirstColumnIndex).Label = "Item Code"
          ItemSheetView.Columns(ItemCodeFirstColumnIndex).Width = 100
          ItemSheetView.Columns(ItemCodeFirstColumnIndex).CellType = General.ItemCellType
        End If
      ElseIf ItemCodeFirstColumnIndex >= 0 AndAlso Me.ItemSheetView Is Nothing Then
        MessageBox.Show("ItemSheetView property is not set for ItemSpread, it is necessary to display the properties of columns", "ItemSpread", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If

      LastEditableColumnIndex()   'It will store the index of last editable column of itemsheetview

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in SetItemCodeColumns(Boolean) method.", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Sub SetItemCodeColumns()
    Try

      SetItemCodeColumns(True)

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in SetItemCodeColumns() method.", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Sub SetItemCodeColumns(ByRef _DataTable As DataTable)
    Try
      'Check the location where columns needs to added is valid.
      If ItemCodeFirstColumnIndex < _DataTable.Columns.Count Then
        For I As Int32 = ItemCodeFirstColumnIndex To (ItemCodeFirstColumnIndex + General.ItemCodeColumnsCount - 1)
          _DataTable.Columns.Add("ItemCode" & (I - ItemCodeFirstColumnIndex).ToString, System.Type.GetType("System.String"))
          _DataTable.Columns("ItemCode" & (I - ItemCodeFirstColumnIndex).ToString).SetOrdinal(I)
        Next
      End If

      'Set the layout of the Item Code column(s)
      SetItemCodeColumns(False)

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in SetItemColumns(DataTable) method.", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Function GetItemCode(ByVal _SheetView As FarPoint.Win.Spread.SheetView, ByVal _RowIndex As Int32) As String
    Try
      Dim _ItemCode As String = String.Empty

      If Constants.ITEM_MULTIPLE_COLUMNS Then
        For cols As Int32 = 0 To DatabaseCache.GetItemLeveling.Rows.Count - 1
          _ItemCode &= _SheetView.GetText(_RowIndex, ItemCodeFirstColumnIndex + cols).PadLeft(DatabaseCache.GetItemLeveling(cols).Length, "0") & Constants.ITEM_LEVELING_SEPERATOR
        Next
        'Truncate extra seperator at the end
        _ItemCode = _ItemCode.Substring(0, _ItemCode.Length - 1)
      Else
        _ItemCode = _SheetView.GetText(_RowIndex, ItemCodeFirstColumnIndex)
      End If

      Return _ItemCode

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in GetItemCode.", ex)
    End Try
  End Function

  Private Function LastEditableColumnIndex() As Int32
    Try
      If ItemSheetView IsNot Nothing AndAlso _LastEditableColumnIndex = -1 Then
        For I As Int32 = 0 To ItemSheetView.ColumnCount - 1
          If ItemSheetView.Columns(I).Visible AndAlso Not ItemSheetView.Columns(I).Locked Then
            _LastEditableColumnIndex = I
          End If
        Next
      End If

      Return _LastEditableColumnIndex

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in LastEditableColumnIndex method", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Function

  Public Sub ShowItemCode(ByVal _RowIndex As Int32, ByVal _ItemCode As String)
    Try
      Dim _ItemCodeParts() As String
      _ItemCodeParts = Split(_ItemCode, Constants.ITEM_LEVELING_SEPERATOR)
      If _ItemCodeParts.Length <> General.ItemCodeColumnsCount - Me.ItemCodeFirstColumnIndex + 1 Then
        For I As Int32 = 0 To _ItemCodeParts.Length - 1
          Me.ItemSheetView.SetText(_RowIndex, I + Me.ItemCodeFirstColumnIndex, _ItemCodeParts(I))
        Next
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "Item Code does not match with current columns")
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in ShowItemCode(Int32,String) method", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Sub RefreshItemCodeAndDescription(ByVal _RowNo As Int32, ByVal ExcludeItemCode As Boolean)
    Try
      Dim _ItemDescLevelCode As String
      Dim _ItemDescLevel As String
      Dim _ItemDesc As String
      Dim _ItemID As Int32
      Dim _ItemDataTable As ItemDataTable

      'Programmer provided Item Description column index
      If Me.LoginInfoObject IsNot Nothing AndAlso ItemDescColumnIndex >= 0 AndAlso _RowNo >= 0 AndAlso _ItemIDColumnIndex >= 0 AndAlso Int32.TryParse(Me.ItemSheetView.GetText(_RowNo, _ItemIDColumnIndex), _ItemID) Then
        'Get the Item Description Level Code and display its description 
        _ItemDescLevel = DatabaseCache.GetSettingValue(Constants.SETTING_ID_ITEM_DESC_LEVEL)
        If _ItemDescLevel IsNot Nothing AndAlso _ItemDescLevel <> String.Empty Then
          _ItemDataTable = _ItemTableAdapter.GetByCoIDAndItemID(Me.LoginInfoObject.CompanyID, _ItemID)
          If _ItemDataTable.Rows.Count > 0 Then
            _ItemDescLevelCode = Leveling.GetUptoSpecifiedLevel(_ItemDataTable(0).Item_Code, Convert.ToInt32(_ItemDescLevel))
            _ItemDesc = _ItemTableAdapter.GetItemDescByCoIdItemCode(Me.LoginInfoObject.CompanyID, _ItemDescLevelCode)
          Else
            _ItemDesc = String.Empty
          End If
        Else
          _ItemDataTable = _ItemTableAdapter.GetByCoIDAndItemID(Me.LoginInfoObject.CompanyID, _ItemID)
          _ItemDesc = String.Empty
        End If

        'If description is not empty then it means description for some other item code level is selected.
        If _ItemDesc Is Nothing OrElse _ItemDesc = String.Empty Then
          If _ItemDataTable.Rows.Count > 0 Then Me.ActiveSheet.SetText(_RowNo, ItemDescColumnIndex, _ItemDataTable(0).Item_Desc)
        Else
          Me.ActiveSheet.SetText(_RowNo, ItemDescColumnIndex, _ItemDesc)
        End If

        If Not ExcludeItemCode AndAlso _ItemDataTable.Rows.Count > 0 Then _
              ShowItemCode(_RowNo, _ItemDataTable(0).Item_Code)
      ElseIf Not ItemIDColumnIndex >= 0 Then
        MessageBox.Show("Provide Item ID column index in order to refresh the item code and/or description", "ItemSpread", MessageBoxButtons.OK, MessageBoxIcon.Error)
      ElseIf Not ItemDescColumnIndex >= 0 Then
        MessageBox.Show("Provide Item Desc column index in order ot refresh the item code and/or description", "ItemSpread", MessageBoxButtons.OK, MessageBoxIcon.Error)
      ElseIf Me.LoginInfoObject Is Nothing Then
        MessageBox.Show("Provide LoginInfoObject in order ot refresh the item code and/or description", "ItemSpread", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in RefreshItemCodeAndDescription(Int32,Boolean) method", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Sub RefreshItemDescription(ByVal _RowNo As Int32)
    Try
      RefreshItemCodeAndDescription(_RowNo, True)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in RefreshItemDescription(Int32) method.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Sub RefreshAllItemCodeAndDescriptions(ByVal _ExcludeItemCode As Boolean)
    Try
      For I As Int32 = 0 To Me.ActiveSheet.RowCount - 1
        RefreshItemCodeAndDescription(I, _ExcludeItemCode)
      Next

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in RefreshAllItemCodeAndDescriptions(Boolean) method.", ex)
      Throw _QuickException
    End Try
  End Sub

  Public Sub RefreshAllItemDescriptions()
    Try
      RefreshAllItemCodeAndDescriptions(True)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in RefreshAllItemDescriptions() method.", ex)
      Throw _QuickException
    End Try
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Description of the method goes here ...
  ''' </summary>
  Public Overrides Function ClearControl() As QuickLibrary.Constants.MethodResult
    Try
      MyBase.ClearControl()
      If Me.ItemQtyFirstColumnIndex >= 0 Then
        Me.ItemSheetView.RecalculateTotals(Me.ItemQtyFirstColumnIndex)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ClearControl of ItemSpread.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 9-Jul-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will update the item summary bar.
  ''' </summary>
  Private Sub UpdateItemSummaryBar()
    Try
      Dim _ItemID As Int32

      If Int32.TryParse(ItemSheetView.GetText(ItemSheetView.ActiveCell.Row.Index, ItemIDColumnIndex), _ItemID) Then
        ItemSummaryBarObject.ShowSummary(Me.LoginInfoObject.CompanyID, _ItemID, 0, 0)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in UpdateItemSummaryBar of PurchaseWarehouseForm.", ex)
      Throw _qex
    End Try
  End Sub
#End Region

#Region "Events"

  Private Sub ItemSpread_EditChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles Me.EditChange
    Try
      Debug.WriteLine("ItemSpread_EditChange event method.")

      'Programmer provided ItemID column index
      If ItemIDColumnIndex >= 0 Then
        If Me.ItemSheetView.ActiveColumnIndex >= ItemCodeFirstColumnIndex AndAlso Me.ItemSheetView.ActiveColumnIndex < General.ItemCodeColumnsCount + ItemCodeFirstColumnIndex Then
          If Me.ActiveSheet.ActiveCell.Text.Length = DatabaseCache.GetItemLeveling(Me.ItemSheetView.ActiveColumnIndex - ItemCodeFirstColumnIndex).Length Then
            SendKeys.Send("{Tab}")
          End If
        ElseIf Me.ActiveSheet.ActiveColumnIndex = ItemSizeColumnIndex Then
          If Me.ActiveSheet.ActiveCell.Text.Length = 1 Then
            SendKeys.Send("{Tab}")
          End If
        End If
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in ItemSpread_EditChange event method.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub ItemSpread_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.EditModeOff
    Try
      Try
        If LoginInfoObject Is Nothing OrElse Me.ActiveSheet.ActiveCell Is Nothing Then
          'System.Windows.Forms.MessageBox.Show("Set LoginInfo object")
          Exit Sub
        End If
      Catch
        'Had to put this try catch because in one scenario on the form activecell is throwing exception.
        Return
      End Try

      If Me.ItemCodeFirstColumnIndex >= 0 AndAlso ItemDescColumnIndex >= 0 AndAlso Me.ActiveSheet.ActiveCell IsNot Nothing Then
        Dim RowNumber As Int32
        Dim ColumnNumber As Int32

        RowNumber = Me.ActiveSheet.ActiveCell.Row.Index
        ColumnNumber = Me.ActiveSheet.ActiveCell.Column.Index

        If (ColumnNumber >= ItemCodeFirstColumnIndex _
        AndAlso ColumnNumber <= ItemCodeFirstColumnIndex + General.ItemCodeColumnsCount - 1) _
        OrElse ColumnNumber = _ItemSizeColumnIndex Then
          Dim ItemDataTable As ItemDataTable

          ItemDataTable = _ItemTableAdapter.GetByItemCodeAndCoID(GetItemCode(Me.ActiveSheet, RowNumber), Me.LoginInfoObject.CompanyID)
          If ItemDataTable.Rows.Count > 0 Then
            Me.ActiveSheet.Rows(Me.ActiveSheet.ActiveRowIndex).ForeColor = Drawing.Color.Green
            With Me.ActiveSheet
              'Programmer provided ItemID column index
              If ItemIDColumnIndex >= 0 Then
                'Setting Item ID in grid column
                .SetText(RowNumber, ItemIDColumnIndex, ItemDataTable(0).Item_ID.ToString)
                'Updating Item Summary Spread.
                If Me.ItemSummaryBarObject IsNot Nothing Then
                  Me.ItemSummaryBarObject.ShowSummary(Me.LoginInfoObject.CompanyID, ItemDataTable(0).Item_ID, 0, 0)
                End If
              End If
              'Programmer provided Item Description column index
              If ItemDescColumnIndex >= 0 Then
                RefreshItemDescription(RowNumber)
              End If
              'Programmer provided Item Size column index (means items are entered row wise in the grid)
              If ItemSizeColumnIndex >= 0 AndAlso ItemRateFirstColumnIndex >= 0 Then
                If ItemSizeColumnIndex = Me.ActiveSheet.ActiveColumnIndex AndAlso Me.ActiveSheet.GetText(Me.ActiveSheet.ActiveRowIndex, Me.ActiveSheet.ActiveColumnIndex) = General.UserInputForItemSize(11) Then
                  Me.ItemSheetView.Columns(Me.ItemRateFirstColumnIndex).Locked = False
                  Me.ItemSheetView.Columns(Me.ItemRateFirstColumnIndex).TabStop = True
                Else
                  Me.ItemSheetView.Columns(Me.ItemRateFirstColumnIndex).Locked = True
                  Me.ItemSheetView.Columns(Me.ItemRateFirstColumnIndex).TabStop = False
                End If
                Select Case .GetText(RowNumber, ItemSizeColumnIndex)
                  Case General.UserInputForItemSize(0)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size01.ToString)
                  Case General.UserInputForItemSize(1)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size02.ToString)
                  Case General.UserInputForItemSize(2)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size03.ToString)
                  Case General.UserInputForItemSize(3)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size04.ToString)
                  Case General.UserInputForItemSize(4)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size05.ToString)
                  Case General.UserInputForItemSize(5)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size06.ToString)
                  Case General.UserInputForItemSize(6)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size07.ToString)
                  Case General.UserInputForItemSize(7)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size08.ToString)
                  Case General.UserInputForItemSize(8)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size09.ToString)
                  Case General.UserInputForItemSize(9)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size10.ToString)
                  Case General.UserInputForItemSize(10)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size11.ToString)
                  Case General.UserInputForItemSize(11)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size12.ToString)
                  Case General.UserInputForItemSize(12)
                    .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size13.ToString)
                  Case Else
                    .SetText(RowNumber, ItemRateFirstColumnIndex, "0")
                End Select
              ElseIf ItemRateFirstColumnIndex >= 0 Then
                'User only provided Rate first column means sizes are columns wise.
                .SetText(RowNumber, ItemRateFirstColumnIndex, ItemDataTable(0).Item_SaleRate_Size01.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 1, ItemDataTable(0).Item_SaleRate_Size02.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 2, ItemDataTable(0).Item_SaleRate_Size03.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 3, ItemDataTable(0).Item_SaleRate_Size04.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 4, ItemDataTable(0).Item_SaleRate_Size05.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 5, ItemDataTable(0).Item_SaleRate_Size06.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 6, ItemDataTable(0).Item_SaleRate_Size07.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 7, ItemDataTable(0).Item_SaleRate_Size08.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 8, ItemDataTable(0).Item_SaleRate_Size09.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 9, ItemDataTable(0).Item_SaleRate_Size10.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 10, ItemDataTable(0).Item_SaleRate_Size11.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 11, ItemDataTable(0).Item_SaleRate_Size12.ToString)
                .SetText(RowNumber, ItemRateFirstColumnIndex + 12, ItemDataTable(0).Item_SaleRate_Size13.ToString)
              End If
            End With
          Else  'ItemDataTable Rows Count
            'QuickDALLibrary.QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Item")
            Me.ActiveSheet.Rows(Me.ActiveSheet.ActiveRowIndex).ForeColor = Drawing.Color.Red
          End If
        End If
      End If

      If Me.ItemQtyFirstColumnIndex >= 0 AndAlso (Me.ItemSheetView.ActiveColumnIndex >= Me.ItemQtyFirstColumnIndex) Then  ' AndAlso Me.ItemSheetView.ActiveColumnIndex < Me.ItemQtyFirstColumnIndex + Common.TotalSizes) Then
        '  Me.ItemSheetView.ColumnHeader.Cells(0, Me.ItemSheetView.ActiveColumnIndex).Text = Me.ItemSheetView.CalculateTotal(Me.ItemSheetView.ActiveColumnIndex)
        Me.ItemSheetView.ColumnHeader.Cells(0, Me.ItemSheetView.ActiveColumnIndex).Text = Me.ItemSheetView.CalculateTotal(Me.ItemSheetView.ActiveColumnIndex)
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in ItemSpread_EditModeOff event method.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2009
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal Saleem  28-Aug-10   Added code to update item summary spread so that when
  '                           row is changed item summary is updated accordingly.
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub ItemSpread_EnterCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EnterCellEventArgs) Handles Me.EnterCell
    Try
      If Me.ItemSummaryBarObject IsNot Nothing AndAlso ItemIDColumnIndex >= 0 Then
        Dim _ItemID As Int32
        Int32.TryParse(Me.ItemSheetView.GetText(Me.ItemSheetView.ActiveRowIndex, ItemIDColumnIndex), _ItemID)
        Me.ItemSummaryBarObject.ShowSummary(Me.LoginInfoObject.CompanyID, _ItemID, 0, 0)
      End If
      'Me.UpdateItemSummaryBar()

      '<<<<<<<<<<< 01-Aug-2009 Faisal: This event was written to move to next row when on last column but now
      ' found action MoveToNextColumnWrap which is used in constructor so it is not required.
      'Will remove it after some time if it keeps on working good.>>>>>>>>>>
      'Try
      'Dim inputmap As New FarPoint.Win.Spread.InputMap
      'inputmap = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
      'If LastEditableColumnIndex() = e.Column Then
      '  inputmap.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
      'Else
      '  inputmap.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColum)
      'End If
      'inputmap = New FarPoint.Win.Spread.InputMap
      'inputmap = Me.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
      'If LastEditableColumnIndex() = e.Column Then
      '  inputmap.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
      'Else
      '  inputmap.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumn)
      'End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in ItemSpread_EnterCell event method of ItemSpread.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

End Class

Public Class ItemSpreadView
  Inherits QuickControls.Quick_SpreadView

#Region "Declarations"
  Private _ShowTotalsRow As Boolean = True

#End Region

#Region "Methods"

  'Author: Faisal Saleem.
  'Date Created(21-DEC-09):
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This function will calculate the total of values in column given and will
  ''' show in total row and will also return the value.
  ''' </summary>
  Public Function CalculateTotal(ByVal ColumnNopara As Int32) As Double
    Try
      Dim _Total As Double = 0

      For I As Int32 = 0 To Me.RowCount - 1
        _Total += Val(Me.GetValue(I, ColumnNopara))
      Next

      Me.ColumnHeader.Cells(0, ColumnNopara).Text = _Total
      Return _Total

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in CalculateTotal of ItemSpread.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 25-Dec-2009
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will recalculate total for all quantity columns.
  ''' </summary>
  Public Sub RecalculateTotals(ByVal _FirstColumnIndexpara As Int32)
    Try

      For I As Int32 = _FirstColumnIndexpara To _FirstColumnIndexpara + Common.TotalSizes - 1
        CalculateTotal(I)
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in RecalculateTotals of ItemSpreadView.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

#Region "Properties"
  'Author: Faisal
  'Date Created(21-Dec-09): 
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This property will show the row for totoals
  ''' </summary>
  Public Property ShowTotalsRow() As Boolean
    Get
      Try
        Return _ShowTotalsRow

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in get method of ShowTotalsRow property of ItemSpread.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try
        _ShowTotalsRow = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in get method of ShowTotalsRow property of ItemSpread.", ex)
        Throw _qex
      End Try
    End Set
  End Property

#End Region

#Region "Event Methods"

#End Region

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub
End Class