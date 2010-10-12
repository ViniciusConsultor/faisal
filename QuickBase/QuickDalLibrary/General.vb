Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Windows.Forms

Public Class General
  Private Shared _UserInputForItemSize As System.Collections.Specialized.StringCollection
  Private Shared _ItemCellType As FarPoint.Win.Spread.CellType.ICellType
  Private Shared _Configuration As QuickDAL.LogicalDataSet.ConfigurationDataTable
  Private Shared _LoginInfoObject As New LoginInfo

  Public Shared Sub SetDBLogonForReport(ByRef myReportDocument As ReportDocument)
    Try
      Dim _ItemTAObject As New ItemTableAdapter
      Dim _SqlConnectionStringObject As New SqlClient.SqlConnectionStringBuilder(_ItemTAObject.GetConnection.ConnectionString)
      Dim _ReportTables As Tables = myReportDocument.Database.Tables
      Dim _LogonInfo As New TableLogOnInfo

      For Each _ReportTable As CrystalDecisions.CrystalReports.Engine.Table In _ReportTables
        _LogonInfo.ConnectionInfo.DatabaseName = _SqlConnectionStringObject.InitialCatalog
        _LogonInfo.ConnectionInfo.IntegratedSecurity = _SqlConnectionStringObject.IntegratedSecurity
        If Not _SqlConnectionStringObject.IntegratedSecurity Then
          _LogonInfo.ConnectionInfo.UserID = _SqlConnectionStringObject.UserID
          _LogonInfo.ConnectionInfo.Password = _SqlConnectionStringObject.Password
        End If
        _LogonInfo.ConnectionInfo.ServerName = _SqlConnectionStringObject.DataSource
        _LogonInfo.ConnectionInfo.AllowCustomConnection = True
        _LogonInfo.ConnectionInfo.Type = ConnectionInfoType.SQL

        _ReportTable.ApplyLogOnInfo(_LogonInfo)
        _ReportTable.Location = _SqlConnectionStringObject.InitialCatalog & ".dbo." & _
        _ReportTable.Location.Substring(_ReportTable.Location.LastIndexOf(".") + 1)

      Next


    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in setting database login information to report.", ex)
      Throw _ExceptionObject
    End Try
  End Sub

  Public Shared Sub SetColumnCaptions(ByRef _DataTable As DataTable, ByVal _FormName As String)
    Try
      Dim _SettingValue As String

      For I As Int32 = 0 To _DataTable.Columns.Count - 1
        _SettingValue = DatabaseCache.GetSettingValue(SETTING_ID_DBColumnCaption & _DataTable.Columns(I).ColumnName & SETTING_ID_SEPERATOR & _FormName)
        If _SettingValue IsNot Nothing AndAlso _SettingValue <> String.Empty Then
          _DataTable.Columns(I).Caption = _SettingValue
        End If
      Next

    Catch ex As Exception
      Dim _QuickExceptionObject As New QuickLibrary.QuickException("Exception in setting column captions", ex)
      Throw _QuickExceptionObject
    End Try
  End Sub

  Public Shared ReadOnly Property UserInputForItemSize() As System.Collections.Specialized.StringCollection
    Get
      If _UserInputForItemSize Is Nothing Then
        Dim _UserInputValue As String

        _UserInputForItemSize = New System.Collections.Specialized.StringCollection
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize01)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("0")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize02)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("1")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize03)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("2")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize04)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("3")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize05)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("4")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize06)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("5")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize07)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("6")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize08)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("7")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize09)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("8")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize10)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("9")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize11)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("C")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize12)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("11")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
        _UserInputValue = DatabaseCache.GetSettingValue(SETTING_ID_UserInput_ItemSize13)
        If _UserInputValue = String.Empty Then
          _UserInputForItemSize.Add("12")
        Else
          _UserInputForItemSize.Add(_UserInputValue)
        End If
      End If

      Return _UserInputForItemSize
    End Get
  End Property

  Public Shared ReadOnly Property FormatDateForDisplay() As String
    Get
      Try
        Dim _FormatDateForDisplay As String
        _FormatDateForDisplay = DatabaseCache.GetSettingValue(SETTING_ID_FormatDateToDisplay)
        If _FormatDateForDisplay = String.Empty Then _FormatDateForDisplay = "dd-MMM-yy"

        Return _FormatDateForDisplay
      Catch ex As Exception
        Dim _QuickException As New QuickException("Exception in FormatDateForDisplay property", ex)
        Throw _QuickException
      End Try
    End Get
  End Property

  Public Shared ReadOnly Property ItemCodeColumnsCount() As Int32
    Get
      Try
        If Constants.ITEM_MULTIPLE_COLUMNS Then
          Return DatabaseCache.GetItemLeveling.Rows.Count
        Else
          Return 1
        End If

      Catch ex As Exception
        Throw New QuickExceptionAdvanced("Exception in ItemColumnsCount property.", ex)
      End Try
    End Get
  End Property

  Public Shared ReadOnly Property ItemCellType() As FarPoint.Win.Spread.CellType.ICellType
    Get
      If _ItemCellType Is Nothing Then
        Dim ItemMaskString As String

        ItemMaskString = DatabaseCache.GetSettingValue(Constants.SETTING_ID_Mask_ItemCode)
        If ItemMaskString Is Nothing OrElse ItemMaskString = String.Empty Then
          _ItemCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Else
          _ItemCellType = New FarPoint.Win.Spread.CellType.MaskCellType
          DirectCast(_ItemCellType, FarPoint.Win.Spread.CellType.MaskCellType).Mask = ItemMaskString
        End If
      End If

      Return _ItemCellType
    End Get
  End Property

  Public Shared Function ConfigurationRead(ByVal _Key As String) As String
    Try

      ConfigurationTable.DefaultView.RowFilter = ConfigurationTable.ConfigurationNameColumn.ColumnName & "='" & _Key & "'"

      If ConfigurationTable.DefaultView.Count <= 0 Then
        Return Nothing
      Else
        Return ConfigurationTable.DefaultView(0).Item(ConfigurationTable.ConfigurationValueColumn.ColumnName).ToString
      End If

    Catch ex As Exception
      Dim _quickException As New QuickException("Exception in ConfigurationRead(String) function", ex)
      Throw _quickException
    End Try
  End Function

  Private Shared ReadOnly Property ConfigurationTable() As LogicalDataSet.ConfigurationDataTable
    Get
      Try
        If _Configuration Is Nothing Then
          If My.Computer.FileSystem.FileExists(ConfigurationFilePath & Constants.CONFIG_FILE_NAME) Then
            _Configuration = New LogicalDataSet.ConfigurationDataTable
            _Configuration.ReadXml(ConfigurationFilePath & Constants.CONFIG_FILE_NAME)
          Else
            _Configuration = New LogicalDataSet.ConfigurationDataTable
          End If
        End If

        Return _Configuration
      Catch ex As Exception
        Dim _quickException As New QuickException("Exception in ConfigurationTable property", ex)
        Throw _quickException
      End Try
    End Get
  End Property

  Public Shared Sub ConfigurationWrite(ByVal _Key As String, ByVal _Value As String)
    Try
      ConfigurationTable.DefaultView.RowFilter = ConfigurationTable.ConfigurationNameColumn.ColumnName & "='" & _Key & "'"

      If ConfigurationTable.DefaultView.Count <= 0 Then
        Dim _ConfigurationRow As QuickDAL.LogicalDataSet.ConfigurationRow = ConfigurationTable.NewConfigurationRow
        _ConfigurationRow.ConfigurationName = _Key
        _ConfigurationRow.ConfigurationValue = _Value

        ConfigurationTable.Rows.Add(_ConfigurationRow)
      Else
        ConfigurationTable.DefaultView(0).Item(ConfigurationTable.ConfigurationValueColumn.ColumnName) = _Value
      End If

      ConfigurationTable.WriteXml(ConfigurationFilePath & Constants.CONFIG_FILE_NAME)
    Catch ex As Exception
      Dim _quickException As New QuickException("Exception in ConfigurationWrite(String) function", ex)
      Throw _quickException
    End Try
  End Sub

  Public Shared Property LoginInfoObject() As LoginInfo
    Get
      Try
        Return _LoginInfoObject

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in LoginInfoObject property get method of General.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As LoginInfo)
      Try
        _LoginInfoObject = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in LoginInfoObject property set method of General.", ex)
        Throw _qex
      End Try
    End Set
  End Property
End Class
