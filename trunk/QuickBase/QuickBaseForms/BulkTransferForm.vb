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

  Private _ItemTA As New ItemTableAdapter
  Private _ItemDataTable As New ItemDataTable
  Private _PartyTA As New PartyTableAdapter
  Private _PartyDataTable As New PartyDataTable
#End Region

#Region "Events"
  Private Sub BulkTransferForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim _TargetOptions() As String = {ITEM, PARTY, ITEM_CATEGORY}
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
  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
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
          Case Else
            MessageBox.Show("This option in not available yet or you have selected invalid option", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
        MyBase.SaveButtonClick(sender, e)
      End If

      If _MethodResult = Constants.MethodResult.Success Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Records were transfered successfully", QuickMessageBox.MessageBoxTypes.ShortMessage)
      ElseIf _MethodResult = Constants.MethodResult.PartialSucceded Then
        QuickMessageBox.Show(Me.LoginInfoObject, "All records were not transfered successfully", QuickMessageBox.MessageBoxTypes.ShortMessage)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "Operation was not successfull", QuickMessageBox.MessageBoxTypes.ShortMessage)
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in saving records", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

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
            _ItemRow = DirectCast(_ItemDataTable.Rows(0), ItemRow)
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
          _ItemRow.Stamp_DateTime = Now

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
            .Stamp_DateTime = Now
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
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ToolbarMode = ToolbarModes.TransferFromFile
  End Sub

End Class
