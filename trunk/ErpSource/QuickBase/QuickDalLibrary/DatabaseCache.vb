Imports QuickDAL
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDALLibrary

Public Class DatabaseCache
  Private Shared _ItemLeveling As LogicalDataSet.ItemLevelingDataTable
  Private Shared _SettingDataTable As QuickCommonDataSet.SettingDataTable
  Private Shared _SettingTA As New QuickCommonDataSetTableAdapters.SettingTableAdapter
  Private Shared _ItemSize As QuickInventoryDataSet.Inv_ItemSizeDataTable
    Public Shared _LoginInfo As New LoginInfo

  Shared Function GetItemLeveling() As LogicalDataSet.ItemLevelingDataTable
    Try
      If _ItemLeveling Is Nothing Then
        Dim _ItemLevelingRow As LogicalDataSet.ItemLevelingRow

        _ItemLeveling = New LogicalDataSet.ItemLevelingDataTable

        _ItemLevelingRow = _ItemLeveling.NewItemLevelingRow
        _ItemLevelingRow.LevelNo = 0
        _ItemLevelingRow.Length = 2
        _ItemLevelingRow.Description = "Cat."
        _ItemLeveling.Rows.Add(_ItemLevelingRow)

        _ItemLevelingRow = _ItemLeveling.NewItemLevelingRow
        _ItemLevelingRow.LevelNo = 1
        _ItemLevelingRow.Length = 2
        _ItemLevelingRow.Description = "Item"
        _ItemLeveling.Rows.Add(_ItemLevelingRow)
      End If

      Return _ItemLeveling
    Catch ex As Exception
      GetItemLeveling = Nothing
      Dim ExceptionObject As New QuickException("Exception in getting item leveling information", ex)
      Throw ExceptionObject
    Finally
    End Try
  End Function

  'This function will return setting value from the setting table.
  'It will return user either specific or generalized value. such as ShowSaveMessage for specific form or in general.
  'Genralized SettingID should be specified on the right most and then specific IDs on left and so on.
  'frmSalesInvoice.SaveMessage.DisplayRecordOperationMessage is an example of how general 
  'and specific IDs should be organized. (General most on right most and specific most on the left most).
  Public Shared Function GetSettingValue(ByVal SettingID As String) As String
    Try
      Dim _DataView As DataView
      Dim _RowNo As Int32

      If _SettingDataTable Is Nothing Then _SettingDataTable = _SettingTA.GetAll
      _DataView = _SettingDataTable.DefaultView

      _DataView.RowFilter = _SettingDataTable.Setting_IdColumn.ColumnName & "='" & SettingID & "'"
      Do While _DataView.Count <= 0 AndAlso SettingID.IndexOf(Constants.SETTING_ID_SEPERATOR) > -1
        SettingID = SettingID.Substring(0, SettingID.LastIndexOf(SETTING_ID_SEPERATOR))
        _DataView.RowFilter = _SettingDataTable.Setting_IdColumn.ColumnName & "='" & SettingID & "'"
      Loop

      If _DataView.Count > 0 Then
        If _DataView.Count > 1 Then
          'Search setting for current company and current user
          For _RowNo = 0 To _DataView.Count
            If _DataView.Item(_RowNo).Item(_SettingDataTable.Co_IdColumn.ColumnName).ToString = _LoginInfo.CompanyID.ToString _
            AndAlso _DataView.Item(_RowNo).Item(_SettingDataTable.User_IdColumn.ColumnName).ToString = _LoginInfo.UserID.ToString Then

              Return _DataView.Item(_RowNo).Item(_SettingDataTable.Setting_ValueColumn.ColumnName).ToString
            End If
          Next
          'Search setting for current company
          For _RowNo = 0 To _DataView.Count
            If _DataView.Item(_RowNo).Item(_SettingDataTable.Co_IdColumn.ColumnName).ToString = _LoginInfo.CompanyID.ToString Then

              Return _DataView.Item(_RowNo).Item(_SettingDataTable.Setting_ValueColumn.ColumnName).ToString
            End If
          Next
        End If

        'If not returned any value by above statement then return the first record.
        Return _DataView.Item(0).Item(_SettingDataTable.Setting_ValueColumn.ColumnName).ToString
      Else
        Return Nothing
      End If

    Catch ex As Exception
      GetSettingValue = Nothing
      Dim _ExceptionObject As New QuickException("Exception in getting item leveling information", ex)
      Throw _ExceptionObject
    End Try
  End Function

  Public Shared Function GetItemSizes() As QuickInventoryDataSet.Inv_ItemSizeDataTable
    Try
      If _ItemSize Is Nothing Then
        _ItemSize = (New Inv_ItemSizeTableAdapter).GetAll
      End If

      Return _ItemSize

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in GetItemSizes method of DatabaseCache class.", ex)
      Throw _qex
    End Try
  End Function
End Class
