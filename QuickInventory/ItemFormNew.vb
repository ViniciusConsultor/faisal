Imports System.Windows.Forms
Imports System.Drawing

Imports QuickDAL
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common
Public Class ItemFormNew
  'Author: Muhammad Zakee
  'Date Created(DD-MMM-YY): 30-APR-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Declare the Variables
  ''' </summary>
#Region "Declaration"
  Private _ItemTableAdapter As New Invs_ItemTableAdapter
  Private _ItemDataTable As New Invs_ItemDataTable
  Private _ItemDetailTableAdapter As New ItemDetailTableAdapter
  Private _ItemDetailDataTable As New ItemDetailDataTable

  Private _ItemRow As Invs_ItemRow
  Private _ItemDetailRow As ItemDetailRow

  Private _DetailID As Integer = 0

  Private Enum SizesDetailEnum
    Co_ID
    ItemSize_ID
    Stamp_UserID
    Stamp_DateTime
    Upload_DateTime
    Is_Selected
    ItemSize_Code
    ItemSize_Desc
    RecordStatus_ID
    Inactive_From
    Inactive_To
  End Enum
  Private Enum GradesDetailEnum
    Co_ID
    ItemGrade_ID
    Is_Selected
    ItemGrade_Code
    ItemGrade_Desc
    Stamp_UserID
    Stamp_DateTime
    Upload_DateTime
    RecordStatus_ID
    Inactive_From
    Inactive_To
  End Enum
  Private Enum ColorsDetailEnum
    Co_ID
    Color_ID
    Is_Selected
    Color_Code
    Color_Desc
    Stamp_UserID
    Stamp_DateTime
    Upload_DateTime
    RecordStatus_ID
    ColorValue
  End Enum

#End Region

#Region "Events"
  'Author: Muhammad Zakee
  'Date Created(DD-MMM-YY): 30-APR-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Declare the Variables
  ''' </summary>
  Private Sub ItemFormNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      Me.ItemIDTextBox.Text = String.Empty
      Me.ItemCodeTextBox.Text = String.Empty
      Me.ItemDescTextBox.Text = String.Empty

      Me.PopulateParentItemComboBox()
      Me.PopulatePartyComboBox()
      Me.PopulateAddressComboBox()

      _DetailID = CInt(Me._ItemDetailTableAdapter.GetNewItemDetailID(Me.LoginInfoObject.CompanyID))

      Me.PopulateSizesDetailGrid(Me.LoginInfoObject.CompanyID)
      Me.PopulateGradesDetailGrid(Me.LoginInfoObject.CompanyID)
      Me.PopulateColorsDetailGrid(Me.LoginInfoObject.CompanyID)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ItemFormNew_Load event method of ItemForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Methods"
  'Author: Muhammad Zakee
  'Date Created(DD-MMM-YY): 30-APR-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Populate Parties Detail in combo Box
  ''' </summary>

  Private Sub PopulatePartyComboBox()
    Try
      Dim _PartyTableAdapter As New PartyTableAdapter
      Dim _PartyDataTable As New PartyDataTable
      Me.PartyComboBox.DataSource = _PartyTableAdapter.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.PartyComboBox.DisplayMember = _PartyDataTable.Party_DescColumn.ColumnName
      Me.PartyComboBox.ValueMember = _PartyDataTable.Party_IDColumn.ColumnName
      With PartyComboBox.DisplayLayout.Bands(0)
        For I As Int32 = 0 To .Columns.Count - 1
          If .Columns(_PartyDataTable.Party_DescColumn.ColumnName).Index <> .Columns(I).Index And .Columns(_PartyDataTable.Party_CodeColumn.ColumnName).Index <> .Columns(I).Index Then
            PartyComboBox.DisplayLayout.Bands(0).Columns(I).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to PopulatePartyComboBox Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub PopulateParentItemComboBox()
    Try
      Dim _ParentItemTableAdapter As New Invs_ItemTableAdapter
      Dim _ParentItemDataTable As New Invs_ItemDataTable
      Me.ParentItemComboBox.DataSource = _ParentItemTableAdapter.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.ParentItemComboBox.DisplayMember = _ParentItemDataTable.Item_DescColumn.ColumnName
      Me.ParentItemComboBox.ValueMember = _ParentItemDataTable.Item_IDColumn.ColumnName
      With ParentItemComboBox.DisplayLayout.Bands(0)
        For I As Int32 = 0 To .Columns.Count - 1
          If .Columns(_ParentItemDataTable.Item_DescColumn.ColumnName).Index <> .Columns(I).Index Then
            ParentItemComboBox.DisplayLayout.Bands(0).Columns(I).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to PopulatePartyComboBox Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub PopulateAddressComboBox()
    Try
      Dim _AddressTableAdapter As New AddressTableAdapter
      Dim _AddressDataTable As New AddressDataTable
      Me.AddressComboBox.DataSource = _AddressTableAdapter.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.AddressComboBox.DisplayMember = _AddressDataTable.Address_DescColumn.ColumnName
      Me.AddressComboBox.ValueMember = _AddressDataTable.Address_IDColumn.ColumnName
      With AddressComboBox.DisplayLayout.Bands(0)
        For I As Int32 = 0 To .Columns.Count - 1
          If .Columns(_AddressDataTable.Address_DescColumn.ColumnName).Index <> .Columns(I).Index Then
            AddressComboBox.DisplayLayout.Bands(0).Columns(I).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to PopulateAddressComboBox Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub PopulateSizesDetailGrid(ByVal Co_ID As Short)
    Try
      Dim _SizesTableAdapter As New ItemSizeTableAdapter
      Dim _SizesDataTable As New ItemSizeDataTable
      _SizesDataTable = _SizesTableAdapter.GetByCoID(Co_ID)
      Me.SizesDetailSpreadSheet.ActiveSheet.DataSource = _SizesDataTable
      SetSizesGridLayout()
      SetSizesGridReadOnlyColumns()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to PopulateSizesDetailGrid Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub SetSizesGridLayout()
    Try
      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.SizesDetailSpreadSheet.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case SizesDetailEnum.Is_Selected
            SheetColumn.Label = "Select"
            SheetColumn.Width = QTY_CELL_WIDTH * 1.25
          Case SizesDetailEnum.ItemSize_Code
            SheetColumn.Label = "Code"
            SheetColumn.Width = QTY_CELL_WIDTH * 1.5
          Case SizesDetailEnum.ItemSize_Desc
            SheetColumn.Label = "Description"
            SheetColumn.Width = QTY_CELL_WIDTH * 3.5
          Case SizesDetailEnum.Co_ID
            SheetColumn.Visible = False
          Case SizesDetailEnum.Inactive_From
            SheetColumn.Visible = False
          Case SizesDetailEnum.Inactive_To
            SheetColumn.Visible = False
          Case SizesDetailEnum.ItemSize_ID
            SheetColumn.Visible = False
          Case SizesDetailEnum.RecordStatus_ID
            SheetColumn.Visible = False
          Case SizesDetailEnum.Stamp_DateTime
            SheetColumn.Visible = False
          Case SizesDetailEnum.Stamp_UserID
            SheetColumn.Visible = False
          Case SizesDetailEnum.Upload_DateTime
            SheetColumn.Visible = False
          Case Else
        End Select
      Next
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to SetSizesGridLayout Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub PopulateGradesDetailGrid(ByVal Co_ID As Short)
    Try
      Dim _GradesTableAdapter As New ItemGradeTableAdapter
      Dim _GradesDataTable As New ItemGradeDataTable
      _GradesDataTable = _GradesTableAdapter.GetByCoID(Co_ID)
      Me.GradesDetailSpreadSheet.ActiveSheet.DataSource = _GradesDataTable
      Me.SetGradesGridLayout()
      SetGradesGridReadOnlyColumns()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to PopulateGradesDetailGrid Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub SetGradesGridLayout()
    Try
      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.GradesDetailSpreadSheet.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case GradesDetailEnum.Is_Selected
            SheetColumn.Label = "Select"
            SheetColumn.Width = QTY_CELL_WIDTH * 1.25
          Case GradesDetailEnum.ItemGrade_Code
            SheetColumn.Label = "Code"
            SheetColumn.Width = QTY_CELL_WIDTH * 1.5
          Case GradesDetailEnum.ItemGrade_Desc
            SheetColumn.Label = "Description"
            SheetColumn.Width = QTY_CELL_WIDTH * 3.5
          Case GradesDetailEnum.Co_ID
            SheetColumn.Visible = False
          Case GradesDetailEnum.Inactive_From
            SheetColumn.Visible = False
          Case GradesDetailEnum.Inactive_To
            SheetColumn.Visible = False
          Case GradesDetailEnum.ItemGrade_ID
            SheetColumn.Visible = False
          Case GradesDetailEnum.RecordStatus_ID
            SheetColumn.Visible = False
          Case GradesDetailEnum.Stamp_DateTime
            SheetColumn.Visible = False
          Case GradesDetailEnum.Stamp_UserID
            SheetColumn.Visible = False
          Case GradesDetailEnum.Upload_DateTime
            SheetColumn.Visible = False
          Case Else
        End Select
      Next
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to SetSizesGridLayout Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub PopulateColorsDetailGrid(ByVal Co_ID As Short)
    Try
      Dim _ColorsTableAdapter As New CommonColorTableAdapter
      Dim _ColorsDataTable As New CommonColorDataTable
      _ColorsDataTable = _ColorsTableAdapter.GetByCoID(Co_ID)
      Me.ColorsDetailSpreadSheet.ActiveSheet.DataSource = _ColorsDataTable
      Me.SetColorsGridLayout()
      SetGColorsGridReadOnlyColumns()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to PopulateColorsDetailGrid Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Sub SetColorsGridLayout()
    Try
      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.ColorsDetailSpreadSheet.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case ColorsDetailEnum.Is_Selected
            SheetColumn.Label = "Select"
            SheetColumn.Width = QTY_CELL_WIDTH * 1.25
          Case ColorsDetailEnum.Color_Code
            SheetColumn.Label = "Code"
            SheetColumn.Width = QTY_CELL_WIDTH * 1.5
          Case ColorsDetailEnum.Color_Desc
            SheetColumn.Label = "Description"
            SheetColumn.Width = QTY_CELL_WIDTH * 3.5
          Case ColorsDetailEnum.Color_ID
            SheetColumn.Visible = False
          Case ColorsDetailEnum.Co_ID
            SheetColumn.Visible = False
          Case ColorsDetailEnum.Stamp_UserID
            SheetColumn.Visible = False
          Case ColorsDetailEnum.Stamp_DateTime
            SheetColumn.Visible = False
          Case ColorsDetailEnum.Upload_DateTime
            SheetColumn.Visible = False
          Case ColorsDetailEnum.RecordStatus_ID
            SheetColumn.Visible = False
          Case ColorsDetailEnum.ColorValue
            SheetColumn.Visible = False
          Case Else
        End Select
      Next
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to SetColorsGridLayout Method", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub
  Private Function IsValid() As Boolean
    Try
      Dim _CountCheckedItems As Integer = 0
      Dim _CheckDuplicateCode As String

      Me.SizesDetailSpreadSheet.EditMode = False
      Me.GradesDetailSpreadSheet.EditMode = False
      Me.ColorsDetailSpreadSheet.EditMode = False
      ' Validation of Master Table Information
      If Me.PartyComboBox.SelectedRow Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the party to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.PartyComboBox.Focus()
        Return False
      ElseIf Me.AddressComboBox.SelectedRow Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the address to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.AddressComboBox.Focus()
        Return False
      ElseIf Me.ItemCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the item code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ItemCodeTextBox.Focus()
        Return False
      ElseIf Me.ItemDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the item description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ItemDescTextBox.Focus()
        Return False
        'Validation of Item_ID And ParentItem ID
      ElseIf Me.ItemIDTextBox.Text = CStr(Me.ParentItemComboBox.Value) Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Every parent Item have not same child item ,you should select the different Parent Item ", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ParentItemComboBox.Focus()
        Return False
      End If

      ' Check Duplication of Item Code

      If Me.ItemIDTextBox.Text = String.Empty Then
        _CheckDuplicateCode = CStr(Me._ItemTableAdapter.GetDuplicateItemCodeByCoID(Me.LoginInfoObject.CompanyID, Me.ItemCodeTextBox.Text))
      Else
        _CheckDuplicateCode = CStr(Me._ItemTableAdapter.GetDuplicateItemCodeByCoIDItemID(Me.LoginInfoObject.CompanyID, CInt(Me.ItemIDTextBox.Text), Me.ItemCodeTextBox.Text))
      End If

      If _CheckDuplicateCode <> String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate item code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If
      ' Validation Item Size Grid
      For I As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
        If CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected)) = True Then
          _CountCheckedItems += 1
        End If
      Next
      If _CountCheckedItems = 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "There must be atleast one item size selected to save the record", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If
      _CountCheckedItems = 0

      ' Validation Item Grades Grid
      For I As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
        If CBool(Me.GradesDetailSpreadSheet_Sheet1.GetValue(I, GradesDetailEnum.Is_Selected)) = True Then
          _CountCheckedItems += 1
        End If
      Next
      If _CountCheckedItems = 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "There must be atleast one item grade selected to save the record", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If
      _CountCheckedItems = 0

      ' Validation Item Colors Grid
      For I As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
        If CBool(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(I, ColorsDetailEnum.Is_Selected)) = True Then
          _CountCheckedItems += 1
        End If
      Next
      If _CountCheckedItems = 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "There must be atleast one item color selected to save the record", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If
      _CountCheckedItems = 0

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function
  Private Sub ShowTotal()
    Try

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show total", ex)
      Throw QuickExceptionObject
    End Try
  End Sub
  Private Sub SetVisibilityofColumn()
    Try

      'For Each SheetColumn As FarPoint.Win.Spread.Column In Me.VoucherDetailQuickSpread.ActiveSheet.Columns
      '  Select Case SheetColumn.Index
      '    Case VoucherDetailEnum.CustomDate1
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDate1_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDate2
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDate2_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDate3
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDate3_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDate4
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDate4_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDate5
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDate5_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDecimal1
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal1_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDecimal2
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal2_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDecimal3
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal3_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDecimal4
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal4_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomDecimal5
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal5_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomText1
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomText1_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomText2
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomText2_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomText3
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomText3_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomText4
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomText4_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.CustomText5
      '      If DatabaseCache.GetSettingValue(Setting_ID_CustomText5_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case VoucherDetailEnum.Party_ID
      '      If DatabaseCache.GetSettingValue(Setting_ID_PartyID_VoucherForm) = "No" Then
      '        SheetColumn.Visible = False
      '      End If
      '    Case Else
      '  End Select
      'Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Visibility Columns on Voucher Entry Form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Private Sub SetSizesGridReadOnlyColumns()
    Try
      Me.SizesDetailSpreadSheet_Sheet1.Columns(SizesDetailEnum.ItemSize_Code).Locked = True
      Me.SizesDetailSpreadSheet_Sheet1.Columns(SizesDetailEnum.ItemSize_Desc).Locked = True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ReadOnly Columns on ItemForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Private Sub SetGradesGridReadOnlyColumns()
    Try
      Me.GradesDetailSpreadSheet_Sheet1.Columns(GradesDetailEnum.ItemGrade_Code).Locked = True
      Me.GradesDetailSpreadSheet_Sheet1.Columns(GradesDetailEnum.ItemGrade_Desc).Locked = True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ReadOnly Columns on ItemForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Private Sub SetGColorsGridReadOnlyColumns()
    Try
      Me.ColorsDetailSpreadSheet_Sheet1.Columns(ColorsDetailEnum.Color_Code).Locked = True
      Me.ColorsDetailSpreadSheet_Sheet1.Columns(ColorsDetailEnum.Color_Desc).Locked = True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ReadOnly Columns on ItemForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Protected Overrides Function ShowRecord() As Boolean
    Try
      For I As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
        Me.SizesDetailSpreadSheet_Sheet1.SetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected, False)
      Next

      For I As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
        Me.GradesDetailSpreadSheet_Sheet1.SetValue(I, GradesDetailEnum.Is_Selected, False)
      Next

      For I As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
        Me.ColorsDetailSpreadSheet_Sheet1.SetValue(I, ColorsDetailEnum.Is_Selected, False)
      Next

      If Me._ItemDataTable.Rows.Count > 0 Then
        Me._ItemRow = Me._ItemDataTable(Me.CurrentRecordIndex)
        Me.ParentItemComboBox.Value = Me._ItemRow.Parent_Item_ID
        Me.AddressComboBox.Value = Me._ItemRow.Address_ID
        Me.PartyComboBox.Value = _ItemRow.Party_ID
        Me.ItemIDTextBox.Text = CStr(_ItemRow.Item_ID)
        Me.ItemCodeTextBox.Text = _ItemRow.Item_Code
        Me.ItemDescTextBox.Text = Me._ItemRow.Item_Desc

      End If

      Me._ItemDetailDataTable = Me._ItemDetailTableAdapter.GetItemByCoID(CInt(Me.ItemIDTextBox.Text), Me.LoginInfoObject.CompanyID)
      For I As Int32 = 0 To Me._ItemDetailDataTable.Rows.Count - 1
        ' Sizes Grid
        For J As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
          If CInt(Me._ItemDetailDataTable.Rows(I).Item("ItemSize_ID")) = CInt(Me.SizesDetailSpreadSheet.ActiveSheet.GetValue(J, ItemFormNew.SizesDetailEnum.ItemSize_ID)) Then
            Me.SizesDetailSpreadSheet_Sheet1.SetValue(J, ItemFormNew.SizesDetailEnum.Is_Selected, True)
          End If
        Next
        ' Grades Grid
        For K As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
          If CInt(Me._ItemDetailDataTable.Rows(I).Item("ItemGrade_ID")) = CInt(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(K, ItemFormNew.GradesDetailEnum.ItemGrade_ID)) Then
            Me.GradesDetailSpreadSheet_Sheet1.SetValue(K, GradesDetailEnum.Is_Selected, True)
          End If
        Next
        '' Grades Grid
        For L As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
          If CInt(Me._ItemDetailDataTable.Rows(I).Item("Color_ID")) = CInt(Me.ColorsDetailSpreadSheet.ActiveSheet.GetValue(L, ItemFormNew.ColorsDetailEnum.Color_ID)) Then
            Me.ColorsDetailSpreadSheet_Sheet1.SetValue(L, ColorsDetailEnum.Is_Selected, True)
          End If
        Next
      Next
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If Me.ItemIDTextBox.Text = String.Empty Then
          If _ItemRow Is Nothing Then
            _ItemRow = Me._ItemDataTable.NewInvs_ItemRow
            _ItemRow.Item_ID = CInt(Me._ItemTableAdapter.GetNewItemIDByCoID(Me.LoginInfoObject.CompanyID))
            Me.ItemIDTextBox.Text = CStr(_ItemRow.Item_ID)
            _ItemRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
          Else
            If _ItemRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
              _ItemRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            End If
          End If

          _ItemRow.Co_ID = Me.LoginInfoObject.CompanyID
          If Me.ParentItemComboBox.Text = String.Empty Then
            _ItemRow.Parent_Item_ID = 0
          Else
            _ItemRow.Parent_Item_ID = CInt(Me.ParentItemComboBox.Value)
          End If
          With _ItemRow
            .Party_ID = CInt(Me.PartyComboBox.Value)
            .Address_ID = CInt(Me.AddressComboBox.Value)
            .Item_Code = Me.ItemCodeTextBox.Text
            .Item_Desc = Me.ItemDescTextBox.Text
            .Is_RawMaterial = Me.RawMaterialCheckBox.Checked
            .Stamp_DateTime = Date.Now
            .Stamp_UserID = LoginInfoObject.UserID
            .Upload_DateTime = Date.Now

            '  Save or Updatde Record
            If .RowState = DataRowState.Detached Then
              Me._ItemDataTable.Rows.Add(Me._ItemRow)
            End If
          End With
          ' Item_Detail Table Entry 
          'Ist Loop
          For I As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
            If CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected)) Then
              ' 2nd Loop
              For J As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
                If CBool(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(J, ItemFormNew.GradesDetailEnum.Is_Selected)) Then
                  ' 3rd Loop
                  For K As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
                    If CBool(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(K, ItemFormNew.ColorsDetailEnum.Is_Selected)) Then
                      ' Add New Row
                      Me._ItemDetailRow = Me._ItemDetailDataTable.NewItemDetailRow
                      With _ItemDetailRow
                        .Co_ID = CShort(Me.LoginInfoObject.CompanyID)
                        .Item_Detail_ID = _DetailID
                        .Item_ID = CInt(Me.ItemIDTextBox.Text)
                        .ItemSize_ID = CShort(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, SizesDetailEnum.ItemSize_ID))
                        .ItemGrade_ID = CShort(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(J, GradesDetailEnum.ItemGrade_ID))
                        .Color_ID = CShort(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(K, ColorsDetailEnum.Color_ID))
                        .Stamp_UserID = LoginInfoObject.UserID
                        .Stamp_DateTime = Date.Now
                        .Upload_DateTime = Date.Now
                        .RecordStatus_ID = Constants.RecordStatuses.Inserted
                        .Opening_Quantity = 0
                        .Opening_Value = 0
                        .Minimum_Quantity = 0
                        .Maximum_Quantity = 0
                        .Sale_Rate = 0
                        If .RowState = DataRowState.Detached Then
                          Me._ItemDetailDataTable.Rows.Add(Me._ItemDetailRow)
                        End If
                        _DetailID += 1
                      End With
                    End If
                  Next
                End If
              Next
            End If
          Next
          Me._ItemTableAdapter.Update(Me._ItemDataTable)
          Me._ItemDetailTableAdapter.Update(Me._ItemDetailDataTable)
          Return True
        Else
          For I As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
            For J As Int32 = 0 To Me._ItemDetailDataTable.Rows.Count - 1
              If CInt(Me.SizesDetailSpreadSheet.ActiveSheet.GetValue(I, ItemFormNew.SizesDetailEnum.ItemSize_ID)) = CInt(Me._ItemDetailDataTable.Rows(J).Item("ItemSize_ID")) And _
                 CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected)) Then
                Exit For
              ElseIf CInt(Me.SizesDetailSpreadSheet.ActiveSheet.GetValue(I, ItemFormNew.SizesDetailEnum.ItemSize_ID)) <> CInt(Me._ItemDetailDataTable.Rows(J).Item("ItemSize_ID")) And _
                 CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected)) Then
                'Insert Record
                ' MsgBox(Me.SizesDetailSpreadSheet.ActiveSheet.GetValue(I, ItemFormNew.SizesDetailEnum.ItemSize_ID))
                'MsgBox(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected))
                'For K As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
                '  If CBool(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(K, ItemFormNew.GradesDetailEnum.Is_Selected)) Then
                '    For L As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
                '      If CBool(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(K, ItemFormNew.ColorsDetailEnum.Is_Selected)) Then
                '        Me._ItemDetailRow = Me._ItemDetailDataTable.NewItemDetailRow
                '        With _ItemDetailRow
                '          .Co_ID = CShort(Me.LoginInfoObject.CompanyID)
                '          .Item_Detail_ID = _DetailID
                '          .Item_ID = CInt(Me.ItemIDTextBox.Text)
                '          .ItemSize_ID = CShort(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, SizesDetailEnum.ItemSize_ID))
                '          .ItemGrade_ID = CShort(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(J, GradesDetailEnum.ItemGrade_ID))
                '          .Color_ID = CShort(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(K, ColorsDetailEnum.Color_ID))
                '          .Stamp_UserID = LoginInfoObject.UserID
                '          .Stamp_DateTime = Date.Now
                '          .Upload_DateTime = Date.Now
                '          .RecordStatus_ID = Constants.RecordStatuses.Inserted
                '          .Opening_Quantity = 0
                '          .Opening_Value = 0
                '          .Minimum_Quantity = 0
                '          .Maximum_Quantity = 0
                '          .Sale_Rate = 0
                '          If .RowState = DataRowState.Detached Then
                '            Me._ItemDetailDataTable.Rows.Add(Me._ItemDetailRow)
                '          End If
                '          _DetailID += 1
                '        End With
                '      End If
                '    Next
                '  End If
                'Next
              ElseIf CInt(Me.SizesDetailSpreadSheet.ActiveSheet.GetValue(I, ItemFormNew.SizesDetailEnum.ItemSize_ID)) = CInt(Me._ItemDetailDataTable.Rows(J).Item("ItemSize_ID")) And _
                           CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected)) = False Then
                ' MsgBox(CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(J, ItemFormNew.SizesDetailEnum.Is_Selected)))
                'Delete Record
                For Q As Int32 = 0 To Me._ItemDetailDataTable.Rows.Count - 1
                  If CInt(Me.SizesDetailSpreadSheet_Sheet1.GetValue(Q, SizesDetailEnum.ItemSize_ID)) = CInt(Me._ItemDetailDataTable.Rows(J).Item("ItemSize_ID")) Then
                    Me._ItemDetailDataTable.Rows(Q).Delete()
                  End If
                Next
              End If
            Next
          Next


          ''  Update Record
          'For I As Int32 = 0 To Me._ItemDetailDataTable.Rows.Count - 1
          '  For J As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
          '    'If CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(J, ItemFormNew.SizesDetailEnum.Is_Selected)) Then
          '    'End If
          '    If CInt(Me._ItemDetailDataTable.Rows(I).Item("ItemSize_ID")) = CInt(Me.SizesDetailSpreadSheet.ActiveSheet.GetValue(J, ItemFormNew.SizesDetailEnum.ItemSize_ID)) Then
          '      If CBool(Me.SizesDetailSpreadSheet_Sheet1.GetValue(J, ItemFormNew.SizesDetailEnum.Is_Selected)) Then
          '      End If


          '      For K As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
          '        If CBool(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(K, ItemFormNew.GradesDetailEnum.Is_Selected)) Then
          '          For L As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
          '            If CBool(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(K, ItemFormNew.ColorsDetailEnum.Is_Selected)) Then
          '              Me._ItemDetailRow = Me._ItemDetailDataTable.NewItemDetailRow
          '              With _ItemDetailRow
          '                .Co_ID = CShort(Me.LoginInfoObject.CompanyID)
          '                .Item_Detail_ID = _DetailID
          '                .Item_ID = CInt(Me.ItemIDTextBox.Text)
          '                .ItemSize_ID = CShort(Me.SizesDetailSpreadSheet_Sheet1.GetValue(I, SizesDetailEnum.ItemSize_ID))
          '                .ItemGrade_ID = CShort(Me.GradesDetailSpreadSheet.ActiveSheet.GetValue(J, GradesDetailEnum.ItemGrade_ID))
          '                .Color_ID = CShort(Me.ColorsDetailSpreadSheet_Sheet1.GetValue(K, ColorsDetailEnum.Color_ID))
          '                .Stamp_UserID = LoginInfoObject.UserID
          '                .Stamp_DateTime = Date.Now
          '                .Upload_DateTime = Date.Now
          '                .RecordStatus_ID = Constants.RecordStatuses.Inserted
          '                .Opening_Quantity = 0
          '                .Opening_Value = 0
          '                .Minimum_Quantity = 0
          '                .Maximum_Quantity = 0
          '                .Sale_Rate = 0
          '                If .RowState = DataRowState.Detached Then
          '                  Me._ItemDetailDataTable.Rows.Add(Me._ItemDetailRow)
          '                End If
          '                _DetailID += 1
          '              End With
          '            End If
          '          Next
          '        End If
          '      Next
          '    End If
          '  Next
          'Next

          Me._ItemTableAdapter.Update(Me._ItemDataTable)
          Me._ItemDetailTableAdapter.Update(Me._ItemDetailDataTable)
          Return True


        End If

      
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord method of ItemForm.", ex)
      Throw QuickExceptionObject
    End Try
  End Function


#End Region

#Region "Properties"

#End Region

#Region "Toolbar methods"
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._ItemDataTable = Me._ItemTableAdapter.GetFirstItemByCoID(Me.LoginInfoObject.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If Me._ItemRow Is Nothing Then
        Me._ItemDataTable = Me._ItemTableAdapter.GetFirstItemByCoID(Me.LoginInfoObject.CompanyID)
      Else
        Me._ItemDataTable = Me._ItemTableAdapter.GetNextItemByCoIDItemID(Me.LoginInfoObject.CompanyID, CInt(Me.ItemIDTextBox.Text))
        If Me._ItemDataTable.Rows.Count = 0 Then
          Me._ItemDataTable = Me._ItemTableAdapter.GetLastItemByCoID(Me.LoginInfoObject.CompanyID)
        End If
      End If

      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If _ItemRow Is Nothing Then
        _ItemDataTable = Me._ItemTableAdapter.GetPrevItemByCoIDItemID(Me.LoginInfoObject.CompanyID, 0)
      Else
        _ItemDataTable = Me._ItemTableAdapter.GetPrevItemByCoIDItemID(Me.LoginInfoObject.CompanyID, CInt(Me.ItemIDTextBox.Text))
      End If
      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._ItemDataTable = Me._ItemTableAdapter.GetLastItemByCoID(Me.LoginInfoObject.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me._ItemRow = Nothing
      Me.ParentItemComboBox.Text = String.Empty
      Me.PartyComboBox.Text = String.Empty
      Me.AddressComboBox.Text = String.Empty

      Me.ItemIDTextBox.Text = String.Empty
      Me.ItemCodeTextBox.Text = String.Empty
      Me.ItemDescTextBox.Text = String.Empty
      _DetailID = 0
      Me.PopulateParentItemComboBox()
      Me._ItemDataTable.Rows.Clear()
      _DetailID = CInt(Me._ItemDetailTableAdapter.GetNewItemDetailID(Me.LoginInfoObject.CompanyID))
      Me._ItemDetailDataTable.Rows.Clear()


      For I As Int32 = 0 To Me.SizesDetailSpreadSheet_Sheet1.RowCount - 1
        Me.SizesDetailSpreadSheet_Sheet1.SetValue(I, ItemFormNew.SizesDetailEnum.Is_Selected, False)
      Next

      For I As Int32 = 0 To Me.GradesDetailSpreadSheet_Sheet1.RowCount - 1
        Me.GradesDetailSpreadSheet_Sheet1.SetValue(I, GradesDetailEnum.Is_Selected, False)
      Next

      For I As Int32 = 0 To Me.ColorsDetailSpreadSheet_Sheet1.RowCount - 1
        Me.ColorsDetailSpreadSheet_Sheet1.SetValue(I, ColorsDetailEnum.Is_Selected, False)
      Next

      'MyBase.CancelButtonClick(sender, e)

      Me.ParentItemComboBox.Focus()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      
      If Me.ItemIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Invalid data selected to delete the record", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return
      End If
      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        _ItemDataTable.Rows(Me.CurrentRecordIndex).Delete()

        For I As Int32 = 0 To Me._ItemDetailDataTable.Rows.Count - 1
          Me._ItemDetailDataTable.Rows(I).Delete()
        Next

        Me._ItemDetailTableAdapter.Update(Me._ItemDetailDataTable)
        Me._ItemTableAdapter.Update(_ItemDataTable)

        Me._ItemRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      Else
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick method of ItemForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub



  Protected Overrides Sub PrintPreviewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in PrintPreviewButtonClick of ItemForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region


 
End Class