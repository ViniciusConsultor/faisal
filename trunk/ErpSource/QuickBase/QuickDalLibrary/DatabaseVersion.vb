Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDALLibrary.DatabaseCache

'Don't use constants from application in upgradation scripts because table of column may have changed in the 
'application but the database is old and needs upgradation.
Public Class DatabaseVersion
  Private Shared _SettingTA As New SettingTableAdapter
  Private Shared _UpgradeCommand As New SqlClient.SqlCommand
  Private Shared _UpgradeConnection As New SqlClient.SqlConnection
  Private Shared _UpgradingScript As ArrayList

  Public Enum NeedsUpgradationResult
    Yes
    No
    SoftwareIsOlderThanDB
    IncorrectCurrentDBVersion
  End Enum

  Public Shared Function NeedsUpgradation() As NeedsUpgradationResult
    Try
      Dim _CurrentDbVersion As String

      _CurrentDbVersion = CurrentDbVersion()

      If _CurrentDbVersion Is Nothing OrElse _CurrentDbVersion = String.Empty Then
        Return NeedsUpgradationResult.Yes
      Else
        Dim VersionParts() As String = Split(_CurrentDbVersion, ".")
        If VersionParts.Length <> 4 Then
          Return NeedsUpgradationResult.IncorrectCurrentDBVersion
        End If

        If Convert.ToInt32(VersionParts(0)) < My.Application.Info.Version.Major Then
          Return NeedsUpgradationResult.Yes
        ElseIf Convert.ToInt32(VersionParts(0)) > My.Application.Info.Version.Major Then
          Return NeedsUpgradationResult.SoftwareIsOlderThanDB
        Else
          'Major part is same check minor
          If Convert.ToInt32(VersionParts(1)) < My.Application.Info.Version.Minor Then
            Return NeedsUpgradationResult.Yes
          ElseIf Convert.ToInt32(VersionParts(1)) > My.Application.Info.Version.Minor Then
            Return NeedsUpgradationResult.SoftwareIsOlderThanDB
          Else
            'Minor part is same check build
            If Convert.ToInt32(VersionParts(2)) < My.Application.Info.Version.Build Then
              Return NeedsUpgradationResult.Yes
            ElseIf Convert.ToInt32(VersionParts(2)) > My.Application.Info.Version.Build Then
              Return NeedsUpgradationResult.SoftwareIsOlderThanDB
            Else
              'Build part is same check revision
              If Convert.ToInt32(VersionParts(3)) < My.Application.Info.Version.Revision Then
                Return NeedsUpgradationResult.Yes
              ElseIf Convert.ToInt32(VersionParts(3)) > My.Application.Info.Version.Revision Then
                Return NeedsUpgradationResult.SoftwareIsOlderThanDB
              Else
                Return NeedsUpgradationResult.No
              End If
            End If
          End If
        End If
      End If

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in verification of version", ex)
      Throw _ExceptionObject
    End Try
  End Function

  Private Shared Function CurrentDbVersion() As String
    Dim _Command As New SqlClient.SqlCommand

    Try
      Dim _CurrentDbVersion As Object

      _Command.CommandText = "Select Setting_Value From Base_Setting Where Setting_ID='" & SETTING_ID_DbVersion & "'"
            _Command.Connection = _SettingTA.GetConnection
      _Command.Connection.Open()
      _CurrentDbVersion = _Command.ExecuteScalar

      If _CurrentDbVersion IsNot Nothing Then
        Return _CurrentDbVersion.ToString()
      End If

      Return String.Empty
    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in getting current db version", ex)
      Throw _ExceptionObject
    Finally
      _Command.Connection.Close()
    End Try
  End Function

  Public Shared Function UpgradeDatabase() As Constants.MethodResult
    Try
      UpgradeDatabase(False, False)

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database.", ex)
      Throw _ExceptionObject
    End Try
  End Function

  Public Shared Function UpgradeDatabase(ByVal CreateAndUpgradeDatabase As Boolean, ByVal CreateObjectsOnly As Boolean) As Constants.MethodResult
    Dim _SqlTransaction As SqlClient.SqlTransaction = Nothing
    Dim _UpgradingTo As String = String.Empty
    Dim _CurrentDbVersion As String = String.Empty
    Dim ProgressWindow As New ProgressWindowForm

    Try
      Dim _UpgradedDbVersion As String = String.Empty
      Dim _VersionCommand As New SqlClient.SqlCommand
      Dim _SettingDataTable As New SettingDataTable
      'Dim _SettingRow As QuickERP.SettingRow
      Dim _SqlConnectionStringBuilder As SqlClient.SqlConnectionStringBuilder
      'Dim Scripts As ArrayList
      'Dim ScriptSeperator() As Char = {">"c, "g"c, "o"c}
      Dim ScriptSeperator As String = Environment.NewLine & "go" & Environment.NewLine

      If CreateAndUpgradeDatabase OrElse CreateObjectsOnly Then
        If CreateAndUpgradeDatabase Then
          'Create database
          _SqlConnectionStringBuilder = New SqlClient.SqlConnectionStringBuilder
          _SqlConnectionStringBuilder.InitialCatalog = "master"
          _SqlConnectionStringBuilder.DataSource = ".\SqlExpress"
          _SqlConnectionStringBuilder.IntegratedSecurity = True
          _UpgradeCommand.Connection = New SqlClient.SqlConnection(_SqlConnectionStringBuilder.ConnectionString)
          _UpgradeCommand.Connection.Open()
          _UpgradeCommand.CommandText = "CREATE DATABASE Quick_Erp ON (Name = Quick_Erp_Data, FileName = 'd:\QuickErpDb\QuickErp.mdf')"
          _UpgradeCommand.ExecuteNonQuery()
          _UpgradeCommand.Connection.Close()
          QuickLibrary.Common.Wait(5)
        End If

        _UpgradingScript = Create01030200()
        _UpgradingTo = "1.3.2.0"
        _UpgradedDbVersion = _UpgradingTo
      Else

        _CurrentDbVersion = CurrentDbVersion()
        If _CurrentDbVersion Is Nothing Then _CurrentDbVersion = String.Empty

        GetUpgradeScripts(_CurrentDbVersion, _UpgradingTo, _UpgradingScript)
        _UpgradedDbVersion = _UpgradingTo
      End If

      _UpgradeCommand.Connection = _SettingTA.GetConnection
      _UpgradeCommand.Connection.Open()
      '_SqlTransaction = _UpgradeCommand.Connection.BeginTransaction

      ProgressWindow.ProcessName = "Upgrading database from version " & _CurrentDbVersion & " to " & _UpgradingTo
      ProgressWindow.Show()
      For I As Int32 = 0 To _UpgradingScript.Count - 1
        '_UpgradeCommand.Transaction = _SqlTransaction
        If _UpgradingScript(I).ToString.Trim.Length > 0 Then  'When using the split function, it leaves some string having newline character.
          ProgressWindow.ChangeProgress(I + 1, _UpgradingScript.Count, "Excecuting Script #" & (I + 1).ToString)

          'Below trim statement was supressing o if it comes at the end of any script
          '_UpgradingScript(I) = _UpgradingScript(I).ToString.Trim(New Char() {Environment.NewLine, "g"c, "o"c, Environment.NewLine})
          _UpgradeCommand.CommandText = _UpgradingScript.Item(I).ToString
          Debug.WriteLine("Script #" & I.ToString & Environment.NewLine & _UpgradingScript.Item(I).ToString & Environment.NewLine)
          'MsgBox(_UpgradingScript(I).ToString & I & "of" & _UpgradingScript.Count)
          _UpgradeCommand.ExecuteNonQuery()
        End If
      Next

      'Setting row can not fetched later when transaction is started.
      '_SettingDataTable = _SettingTA.GetSystemSetting(SETTING_ID_DbVersion)
      'If _SettingDataTable.Rows.Count > 0 Then
      If _SettingTA.SettingIdExists(SETTING_ID_DbVersion).Value Then
        '_SettingRow = _SettingDataTable(0)
        _SettingTA.UpdateSettingValue(_UpgradedDbVersion, DatabaseCache._LoginInfo.UserID, Now, 0, 0, SETTING_ID_DbVersion)
      Else
        '_SettingRow = _SettingDataTable.NewSettingRow
        _SettingTA.InsertSettingValue(0, 0, SETTING_ID_DbVersion, SETTING_DESC_DbVersion, _UpgradedDbVersion, DatabaseCache._LoginInfo.UserID, Now, SETTING_VALUE_DATATYPE_STRING, String.Empty, String.Empty)
      End If

      _UpgradeCommand.Connection.Close()
      QuickLibrary.Common.Wait(5)

      If Not CurrentDbVersion() = My.Application.Info.Version.ToString Then
        UpgradeDatabase(False, False)
      End If

    Catch ex As Exception
      'If _SqlTransaction IsNot Nothing Then _SqlTransaction.Rollback()
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from " & _CurrentDbVersion & " to " & _UpgradingTo, ex)
      Throw _ExceptionObject
    Finally
      If ProgressWindow IsNot Nothing Then
        ProgressWindow.Close()
        ProgressWindow = Nothing
      End If
    End Try
  End Function

#Region "Script Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 30-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This returns the target version number and arraylist of scripts in byref 
  ''' parameters based on the existing version of the database given to it.
  ''' </summary>
  Private Shared Sub GetUpgradeScripts(ByVal ExistingDbVersionpara As String, ByRef _UpgradingTo As String, ByRef _UpgradingScript As ArrayList)
    Try

      Select Case ExistingDbVersionpara
        Case "2.3.2.30"
          _UpgradingTo = "2.3.2.31"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_30_to_02_03_02_31, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.29"
          _UpgradingTo = "2.3.2.30"
          _UpgradingScript = New ArrayList
        Case "2.3.2.28"
          _UpgradingTo = "2.3.2.29"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_28_to_02_03_02_29, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.27"
          _UpgradingTo = "2.3.2.28"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_27_to_02_03_02_28, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.26"
          _UpgradingTo = "2.3.2.27"
          _UpgradingScript = New ArrayList
        Case "2.3.2.25"
          _UpgradingTo = "2.3.2.26"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_25_to_02_03_02_26, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.24"
          _UpgradingTo = "2.3.2.25"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_24_to_02_03_02_25, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.23"
          _UpgradingTo = "2.3.2.24"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_23_to_02_03_02_24, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.22"
          _UpgradingTo = "2.3.2.23"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_22_to_02_03_02_23, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.21"
          _UpgradingTo = "2.3.2.22"
          _UpgradingScript = New ArrayList
        Case "2.3.2.20"
          _UpgradingTo = "2.3.2.21"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_02_20_to_02_03_02_21, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.2.19"
          _UpgradingTo = "2.3.2.20"
          _UpgradingScript = New ArrayList
        Case "2.3.2.18"
          _UpgradingTo = "2.3.2.19"
          _UpgradingScript = New ArrayList
        Case "2.3.1.17"
          _UpgradingTo = "2.3.2.18"
          _UpgradingScript = New ArrayList
        Case "2.3.1.16"
          _UpgradingTo = "2.3.1.17"
          _UpgradingScript = New ArrayList
        Case "2.3.1.15"
          _UpgradingTo = "2.3.1.16"
          _UpgradingScript = New ArrayList
        Case "2.3.1.14"
          _UpgradingTo = "2.3.1.15"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_14_to_02_03_01_15, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.13"
          _UpgradingTo = "2.3.1.14"
          'No db script required for this version.
          _UpgradingScript = New ArrayList
        Case "2.3.1.12"
          _UpgradingTo = "2.3.1.13"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_12_to_02_03_01_13, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.11"
          _UpgradingTo = "2.3.1.12"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_11_to_02_03_01_12, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.10"
          _UpgradingTo = "2.3.1.11"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_10_to_02_03_01_11, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.9"
          _UpgradingTo = "2.3.1.10"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_09_to_02_03_01_10, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.8"
          _UpgradingTo = "2.3.1.9"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_08_to_02_03_01_09, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.7"
          _UpgradingTo = "2.3.1.8"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_07_to_02_03_01_08, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.6"
          _UpgradingTo = "2.3.1.7"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_06_to_02_03_01_07, Environment.NewLine & "go" & Environment.NewLine))
        Case "2.3.1.5"
          _UpgradingTo = "2.3.1.6"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_05_to_02_03_01_06, Environment.NewLine & "go" & Environment.NewLine))
        Case ("2.3.1.4")
          _UpgradingTo = "2.3.1.5"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_04_to_02_03_01_05, Environment.NewLine & "go" & Environment.NewLine))
        Case ("2.3.1.3")
          _UpgradingTo = "2.3.1.4"
          'No db script required for this version.
          _UpgradingScript = New ArrayList
        Case ("2.3.1.2")
          _UpgradingTo = "2.3.1.3"
          'No db script required for this version.
          _UpgradingScript = New ArrayList
        Case ("2.3.1.1")
          _UpgradingTo = "2.3.1.2"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_01_01_to_02_03_01_02, Environment.NewLine & "go" & Environment.NewLine))
        Case ("2.3.1.0")
          _UpgradingTo = "2.3.1.1"
          'Repeat the scripts of last release becuase db version was manually set
          'because there was error executing scripts. These scripts must execute now.
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_20_to_02_03_01_00, Environment.NewLine & "go" & Environment.NewLine))
        Case ("2.3.0.20")
          _UpgradingTo = "2.3.1.0"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_20_to_02_03_01_00, Environment.NewLine & "go" & Environment.NewLine))
        Case ("2.3.0.19")
          _UpgradingTo = "2.3.0.20"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_19_to_02_03_00_20, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.18")
          _UpgradingTo = "2.3.0.19"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.17")
          _UpgradingTo = "2.3.0.18"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.16")
          _UpgradingTo = "2.3.0.17"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_16_to_02_03_00_17, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.15")
          _UpgradingTo = "2.3.0.16"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_15_to_02_03_00_16, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.14")
          _UpgradingTo = "2.3.0.15"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_14_to_02_03_00_15, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.13")
          _UpgradingTo = "2.3.0.14"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_13_to_02_03_00_14, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.12")
          _UpgradingTo = "2.3.0.13"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_12_to_02_03_00_13, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.11")
          _UpgradingTo = "2.3.0.12"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.10")
          _UpgradingTo = "2.3.0.11"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.9")
          _UpgradingTo = "2.3.0.10"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.8")
          _UpgradingTo = "2.3.0.9"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_08_to_02_03_00_09, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.7")
          _UpgradingTo = "2.3.0.8"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.6")
          _UpgradingTo = "2.3.0.7"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.5")
          _UpgradingTo = "2.3.0.6"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.4")
          _UpgradingTo = "2.3.0.5"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.3")
          _UpgradingTo = "2.3.0.4"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.3.0.2")
          _UpgradingTo = "2.3.0.3"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_02_to_02_03_00_03, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.1")
          _UpgradingTo = "2.3.0.2"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_01_to_02_03_00_02, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.3.0.0")
          _UpgradingTo = "2.3.0.1"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_03_00_00_to_02_03_00_01, Environment.NewLine & "go" & Environment.NewLine))

        Case String.Empty
          _UpgradingTo = "1.3.0.0"
          _UpgradingScript = Upgrade01020000To01030000()
        Case "1.3.0.0"
          _UpgradingTo = "1.3.1.0"
          _UpgradingScript = Upgrade01030000To01030100()
        Case ("1.3.1.0")
          _UpgradingTo = "1.3.2.0"
          _UpgradingScript = Upgrade01030100To01030200()
        Case ("1.3.2.0")
          _UpgradingTo = "1.3.3.0"
          _UpgradingScript = Upgrade01030200To01030300()
        Case ("1.3.3.0")
          _UpgradingTo = "1.3.4.0"
          _UpgradingScript = Upgrade01030200To01030300()
        Case ("1.3.4.0")
          _UpgradingTo = "1.4.1.0"
          'In last release method call was no scripts wasn't executed succesfully
          _UpgradingScript = Upgrade01030300To01030400()
        Case ("1.3.4.1")
          _UpgradingTo = "1.3.5.0"
          'In last release method call was no scripts wasn't executed succesfully
          _UpgradingScript = Upgrade01030401To01030500()
        Case ("1.3.5.0")
          _UpgradingTo = "1.3.6.0"
          _UpgradingScript = Upgrade1350To1360()
        Case ("1.3.6.0")
          _UpgradingTo = "1.3.7.0"
          _UpgradingScript = Upgrade1360To1370()
        Case ("1.3.7.0")
          _UpgradingTo = "1.3.8.0"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade1370To1380, Environment.NewLine & "go" & Environment.NewLine))

        Case ("1.3.8.0")
          _UpgradingTo = "1.3.9.0"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade1380To1390, Environment.NewLine & "go" & Environment.NewLine))

        Case ("1.3.9.0")
          _UpgradingTo = "1.3.9.1"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade1390To1391, Environment.NewLine & "go" & Environment.NewLine))

        Case ("1.3.9.1")
          _UpgradingTo = "1.3.9.2"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade1391To1392, Environment.NewLine & "go" & Environment.NewLine))

        Case ("1.3.9.2")
          _UpgradingTo = "1.4.0.0"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade1392To1400, Environment.NewLine & "go" & Environment.NewLine))

        Case ("1.4.0.0")
          _UpgradingTo = "1.4.1.0"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade1400To1410, Environment.NewLine & "go" & Environment.NewLine))

        Case ("1.4.1.0")
          _UpgradingTo = "2.0.0.0"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade1410to2000, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.0")
          _UpgradingTo = "2.0.0.1"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade2000to20001, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.1")
          _UpgradingTo = "2.0.0.2"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.0.0.2")
          _UpgradingTo = "2.0.0.3"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.0.0.3")
          _UpgradingTo = "2.0.0.4"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade2003to2004, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.4")
          _UpgradingTo = "2.0.0.5"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.0.0.5")
          _UpgradingTo = "2.0.0.6"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.0.0.6")
          _UpgradingTo = "2.0.0.7"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade2006to2007, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.7")
          _UpgradingTo = "2.0.0.8"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade2007to2008, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.8")
          _UpgradingTo = "2.0.0.9"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.Upgrade2008to2009, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.9")
          _UpgradingTo = "2.0.0.10"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.0.0.10")
          _UpgradingTo = "2.0.0.11"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.0.0.11")
          _UpgradingTo = "2.0.0.12"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade20011to20012, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.0.0.12")
          _UpgradingTo = "2.1.0.1"
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_00_00_12_to_02_01_00_01, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.1.0.1")
          _UpgradingTo = "2.1.0.2"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.2")
          _UpgradingTo = "2.1.0.3"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.3")
          _UpgradingTo = "2.1.0.4"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.4")
          _UpgradingTo = "2.1.0.5"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.5")
          _UpgradingTo = "2.1.0.6"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.6")
          _UpgradingTo = "2.1.0.7"
          'No db script required for this version.
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_01_00_06_to_02_01_00_07, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.1.0.7")
          _UpgradingTo = "2.1.0.8"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.8")
          _UpgradingTo = "2.1.0.9"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.1.0.9")
          _UpgradingTo = "2.2.0.0"
          'No db script required for this version.
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_01_00_09_to_02_02_00_00, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.2.0.0")
          _UpgradingTo = "2.2.1.0"
          'No db script required for this version.
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_02_00_00_to_02_02_01_00, Environment.NewLine & "go" & Environment.NewLine))

        Case ("2.2.1.0")
          _UpgradingTo = "2.2.1.1"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.2.1.1")
          _UpgradingTo = "2.2.1.2"
          'No db script required for this version.
          _UpgradingScript = New ArrayList

        Case ("2.2.1.2")
          _UpgradingTo = "2.3.0.0"
          'No db script required for this version.
          _UpgradingScript = New ArrayList(Common.SplitStringToArrayList(My.Resources.upgrade_02_02_01_02_to_02_03_00_00, Environment.NewLine & "go" & Environment.NewLine))

        Case Else
          Throw New QuickException("Software vender did not included database upgradation process for this version, you can not use the software.", Nothing)
      End Select

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in GetUpgradeScripts of DatabaseVersion.", ex)
      Throw _qex
    End Try
  End Sub

  Private Shared Function Create01030200() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Accounting_COA]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Accounting_COA] ([Co_ID] smallint NOT NULL, [COA_Code] varchar (50) NOT NULL, [COA_Desc] varchar (250) NOT NULL, [COA_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Accounting_Voucher]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Accounting_Voucher] ([Co_ID] smallint NOT NULL, [Remarks] varchar (50) NOT NULL, [Source_DocumentType_ID] smallint NULL, [Source_ID] int NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL, [Status_ID] int NOT NULL, [Voucher_Date] datetime NOT NULL, [Voucher_ID] int NOT NULL, [Voucher_No] varchar (50) NOT NULL, [VoucherType_ID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Accounting_Voucher_Detail]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Accounting_Voucher_Detail] ([Co_ID] smallint NOT NULL, [COA_ID] int NOT NULL, [CreditAmount] decimal NOT NULL, [DebitAmount] decimal NOT NULL, [Narration] varchar (300) NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_User_Id] int NOT NULL, [Voucher_ID] int NOT NULL, [VoucherDetail_ID] smallint NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Accounting_VoucherType]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Accounting_VoucherType] ([Co_ID] smallint NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL, [VoucherType_Code] varchar (50) NOT NULL, [VoucherType_Desc] varchar (250) NOT NULL, [VoucherType_ID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Base_Address]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Base_Address] ([Address_Desc] varchar (300) NOT NULL, [Address_ID] bigint NOT NULL, [AddressType_ID] smallint NOT NULL, [Co_ID] smallint NOT NULL, [Parent_Address_ID] bigint NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Base_Communication]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Base_Communication] ([Communication_ID] int NOT NULL IDENTITY, [Communication_Type] int NOT NULL, [Communication_Value] varchar (250) NULL, [Source_ID] int NOT NULL, [Source_Type] smallint NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Base_Company]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Base_Company] ([Co_Code] varchar (50) NOT NULL, [Co_Desc] varchar (200) NULL, [Co_Id] smallint NOT NULL, [Inactive_From] datetime NULL, [Inactive_To] datetime NULL, [Parent_Co_ID] smallint NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Base_Setting]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Base_Setting] ([Co_Id] smallint NOT NULL, [Setting_Desc] varchar (200) NOT NULL, [Setting_Id] varchar (50) NOT NULL, [Setting_Value] varchar (300) NOT NULL, [Setting_Value_DataType] varchar (10) NOT NULL, [Setting_Value_MaximumValue] varchar (50) NULL, [Setting_Value_MinimumValue] varchar (50) NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_User_Id] int NOT NULL, [User_Id] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Common_DocumentType]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Common_DocumentType] ([DocumentType_Desc] varchar (50) NOT NULL, [DocumentType_ID] smallint NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Common_EntityType]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Common_EntityType] ([EntityType_Code] varchar (50) NOT NULL, [EntityType_Desc] varchar (100) NOT NULL, [EntityType_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Common_Status]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Common_Status] ([Status_Desc] varchar (50) NULL, [Status_ID] int NOT NULL, [Status_Type_ID] smallint NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Common_Status_Type]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Common_Status_Type] ([Status_Type_Desc] varchar (50) NULL, [Status_Type_ID] smallint NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inv_Item]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Inv_Item] ([Address_ID] bigint NOT NULL, [Co_ID] smallint NOT NULL, [Item_Code] varchar (50) NOT NULL, [Item_Desc] varchar (50) NOT NULL, [Item_ID] int NOT NULL, [Item_SaleRate_Size01] decimal NOT NULL, [Item_SaleRate_Size02] decimal NOT NULL, [Item_SaleRate_Size03] decimal NOT NULL, [Item_SaleRate_Size04] decimal NOT NULL, [Item_SaleRate_Size05] decimal NOT NULL, [Item_SaleRate_Size06] decimal NOT NULL, [Item_SaleRate_Size07] decimal NOT NULL, [Item_SaleRate_Size08] decimal NOT NULL, [Item_SaleRate_Size09] decimal NOT NULL, [Item_SaleRate_Size10] decimal NOT NULL, [Item_SaleRate_Size11] decimal NOT NULL, [Item_SaleRate_Size12] decimal NOT NULL, [Item_SaleRate_Size13] decimal NOT NULL, [Parent_Item_ID] int NOT NULL, [Party_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inv_Item_PriceHistory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Inv_Item_PriceHistory] ([Address_ID] bigint NOT NULL, [Co_ID] smallint NOT NULL, [Item_ID] int NOT NULL, [Item_Price_Size01] decimal NOT NULL, [Item_Price_Size02] decimal NOT NULL, [Item_Price_Size03] decimal NOT NULL, [Item_Price_Size04] decimal NOT NULL, [Item_Price_Size05] decimal NOT NULL, [Item_Price_Size06] decimal NOT NULL, [Item_Price_Size07] decimal NOT NULL, [Item_Price_Size08] decimal NOT NULL, [Item_Price_Size09] decimal NOT NULL, [Item_Price_Size10] decimal NOT NULL, [Item_Price_Size11] decimal NOT NULL, [Item_Price_Size12] decimal NOT NULL, [Item_Price_Size13] decimal NOT NULL, [Item_PriceHistory_ID] int NOT NULL, [Item_PriceType] varchar (50) NOT NULL, [Party_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL, [Valid_From] datetime NULL, [Valid_Till] datetime NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inventory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Inventory] ([Co_ID] smallint NOT NULL, [DocumentType_ID] smallint NOT NULL, [Inventory_Date] datetime NULL, [Inventory_ID] int NOT NULL, [Inventory_No] int NOT NULL, [Party_ID] int NULL, [Payment_Mode] smallint NULL, [Remarks] varchar (300) NULL, [Stamp_DateTime] datetime NULL, [Stamp_UserID] int NULL, [Status_ID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inventory_Detail]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Inventory_Detail] ([Co_ID] smallint NOT NULL, [DocumentType_ID] smallint NOT NULL, [Inventory_ID] int NOT NULL, [Inventory_Qty_Size01] decimal NOT NULL, [Inventory_Qty_Size02] decimal NOT NULL, [Inventory_Qty_Size03] decimal NOT NULL, [Inventory_Qty_Size04] decimal NOT NULL, [Inventory_Qty_Size05] decimal NOT NULL, [Inventory_Qty_Size06] decimal NOT NULL, [Inventory_Qty_Size07] decimal NOT NULL, [Inventory_Qty_Size08] decimal NOT NULL, [Inventory_Qty_Size09] decimal NOT NULL, [Inventory_Qty_Size10] decimal NOT NULL, [Inventory_Qty_Size11] decimal NOT NULL, [Inventory_Qty_Size12] decimal NOT NULL, [Inventory_Qty_Size13] decimal NOT NULL, [Inventory_Rate_Size01] decimal NOT NULL, [Inventory_Rate_Size02] decimal NOT NULL, [Inventory_Rate_Size03] decimal NOT NULL, [Inventory_Rate_Size04] decimal NOT NULL, [Inventory_Rate_Size05] decimal NOT NULL, [Inventory_Rate_Size06] decimal NOT NULL, [Inventory_Rate_Size07] decimal NOT NULL, [Inventory_Rate_Size08] decimal NOT NULL, [Inventory_Rate_Size09] decimal NOT NULL, [Inventory_Rate_Size10] decimal NOT NULL, [Inventory_Rate_Size11] decimal NOT NULL, [Inventory_Rate_Size12] decimal NOT NULL, [Inventory_Rate_Size13] decimal NOT NULL, [InventoryDetail_ID] int NOT NULL, [Item_ID] int NOT NULL, [Stamp_DateTime] datetime NULL, [Stamp_UserID] int NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inventory_SalesInvoice]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Inventory_SalesInvoice] ([Co_ID] smallint NOT NULL, [Discount] money NOT NULL, [Inventory_ID] int NOT NULL, [SalesMan_ID] int NOT NULL, [SalesTax] money NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Party]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Party] ([Address] varchar (300) NULL, [City] varchar (100) NULL, [Co_ID] smallint NOT NULL, [COA_ID] int NULL, [Commission] decimal NOT NULL, [Country] varchar (100) NULL, [Email] varchar (200) NULL, [EntityType_ID] int NOT NULL, [Fax] varchar (100) NULL, [Inactive_From] datetime NULL, [Inactive_To] datetime NULL, [Opening_Cr] decimal NULL, [Opening_Dr] decimal NULL, [Party_Code] varchar (50) NULL, [Party_Desc] varchar (300) NULL, [Party_ID] int NOT NULL, [Phone] varchar (100) NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL, [State] varchar (50) NULL, [URL] varchar (100) NULL, [ZipCode] varchar (50) NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Party_Detail]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Party_Detail] ([Co_ID] varchar (50) NOT NULL, [Contact_Person] varchar (100) NULL, [Contact_Person_Phone] varchar (100) NULL, [IsDefault] bit NOT NULL, [Party_Detail_ID] smallint NOT NULL, [Party_ID] varchar (50) NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Person]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Person] ([Person_FirstName] varchar (50) NULL, [Person_ID] int NOT NULL, [Person_SecondName] varchar (50) NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_User] ([Co_ID] smallint NOT NULL, [Inactive_From] datetime NULL, [Inactive_To] datetime NULL, [Password] varchar (50) NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL, [User_Desc] varchar (50) NULL, [User_ID] int NOT NULL, [User_Name] varchar (50) NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[sysdiagrams]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [sysdiagrams] ([definition] varbinary NULL, [diagram_id] int NOT NULL IDENTITY, [name] sysname NOT NULL, [principal_id] int NOT NULL, [version] int NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Temp_SalesInvoiceReport]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Temp_SalesInvoiceReport] ([Address_Desc] varchar (300) NOT NULL, [Co_Desc] varchar (200) NOT NULL, [Co_ID] smallint NOT NULL, [Discount] decimal NOT NULL, [DocumentType_ID] smallint NOT NULL, [Inventory_Date] datetime NOT NULL, [Inventory_No] varchar (50) NOT NULL, [Item_Code] varchar (50) NOT NULL, [Item_Desc] varchar (50) NOT NULL, [Item_ID] int NOT NULL, [Party_Desc] varchar (300) NOT NULL, [Party_ID] int NOT NULL, [Qty] decimal NOT NULL, [Rate] decimal NOT NULL, [Remarks] varchar (300) NOT NULL, [SalesTax] decimal NOT NULL, [Size_Desc] varchar (50) NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Transfer]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Transfer] ([Co_ID] smallint NOT NULL, [Source_Location] varchar (250) NULL, [Target_Location] varchar (250) NULL, [Transfer_EndDateTime] datetime NULL, [Transfer_ID] int NOT NULL, [Transfer_StartDateTime] datetime NOT NULL, [Transfer_Status] smallint NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Transfer_Pattern1]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Transfer_Pattern1] ([Co_ID] smallint NOT NULL, [Record_Co_ID] smallint NOT NULL, [Record_ID] int NOT NULL, [Record_Stamp_DateTime] datetime NOT NULL, [Table_Name] varchar (50) NOT NULL, [Transfer_Detail_ID] int NOT NULL, [Transfer_EndDateTime] datetime NULL, [Transfer_ID] int NOT NULL, [Transfer_StartDateTime] datetime NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Transfer_Pattern2]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Transfer_Pattern2] ([Co_ID] smallint NOT NULL, [Record_Co_ID] smallint NOT NULL, [Record_Detail_ID] int NOT NULL, [Record_ID] int NOT NULL, [Record_Stamp_DateTime] datetime NOT NULL, [Table_Name] varchar (50) NOT NULL, [Transfer_Detail_ID] int NOT NULL, [Transfer_EndDateTime] datetime NULL, [Transfer_ID] int NOT NULL, [Transfer_StartDateTime] datetime NOT NULL)")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetAccountsDetails') DROP PROCEDURE spGetAccountsDetails")
      Scripts.Add("CREATE PROCEDURE [spGetAccountsDetails] AS BEGIN SET NOCOUNT ON; SELECT [Voucher_No],v.[VoucherType_ID],vt.[VoucherType_Code],vt.[VoucherType_Desc],[Voucher_Date],[Status_ID],[Remarks],vd.[Co_ID],vd.[Voucher_ID],[VoucherDetail_ID],vd.[COA_ID],coa.[COA_Code],coa.[COA_Desc],[Narration],[DebitAmount],[CreditAmount],vd.[Stamp_User_Id],vd.[Stamp_DateTime] FROM Accounting_Voucher v RIGHT OUTER JOIN Accounting_Voucher_Detail vd ON v.Co_ID = vd.Co_ID AND v.Voucher_ID = vd.Voucher_ID LEFT OUTER JOIN Accounting_VoucherType vt ON v.Co_ID = vt.Co_ID LEFT OUTER JOIN Accounting_COA coa ON vd.Co_ID = coa.Co_ID AND vd.COA_ID = coa.COA_ID END")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetInventoryDetails') DROP PROCEDURE spGetInventoryDetails")
      Scripts.Add("CREATE PROCEDURE [dbo].[spGetInventoryDetails] AS BEGIN SET NOCOUNT ON; SELECT inv.Co_ID, Co.Co_Desc, invdet.Item_ID, inv.Party_ID, item.Item_Code, item.Item_Desc, cat.Item_Desc AS Item_Category,inv.Inventory_Date, inv.Payment_Mode, inv.Remarks, inv.Inventory_ID, inv.Inventory_No, invdet.DocumentType_ID, doc.DocumentType_Desc,invdet.Inventory_Qty_Size01, invdet.Inventory_Qty_Size02, invdet.Inventory_Qty_Size03, invdet.Inventory_Qty_Size04, invdet.Inventory_Qty_Size05, invdet.Inventory_Qty_Size06, invdet.Inventory_Qty_Size07, invdet.Inventory_Qty_Size08, invdet.Inventory_Qty_Size09, invdet.Inventory_Qty_Size10, invdet.Inventory_Qty_Size11, invdet.Inventory_Qty_Size12, invdet.Inventory_Qty_Size13, invdet.Inventory_Rate_Size01, invdet.Inventory_Rate_Size02, invdet.Inventory_Rate_Size03, invdet.Inventory_Rate_Size04, invdet.Inventory_Rate_Size05, invdet.Inventory_Rate_Size06, invdet.Inventory_Rate_Size07, invdet.Inventory_Rate_Size08, invdet.Inventory_Rate_Size09, invdet.Inventory_Rate_Size10, invdet.Inventory_Rate_Size11, invdet.Inventory_Rate_Size13, invdet.Inventory_Rate_Size12, dbo.Party.Party_Desc, dbo.Party.COA_ID, dbo.Party.City FROM dbo.Inv_Item cat RIGHT OUTER JOIN dbo.Inv_Item AS item  ON cat.Co_ID = item.Co_ID AND LEFT(item.Item_Code,2) = cat.Item_Code RIGHT JOIN dbo.Inventory_Detail AS invdet  ON item.Co_ID = invdet.Co_ID AND item.Item_ID = invdet.Item_ID  INNER JOIN dbo.Inventory AS inv  ON inv.Co_ID = invdet.Co_ID AND inv.Inventory_ID = invdet.Inventory_ID  LEFT OUTER JOIN dbo.Party  ON inv.Co_ID = dbo.Party.Co_ID AND inv.Party_ID = dbo.Party.Party_ID LEFT OUTER JOIN Base_Company Co ON inv.Co_ID = Co.Co_ID LEFT OUTER JOIN Common_DocumentType doc ON invdet.DocumentType_ID = doc.DocumentType_ID END")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM [Base_Setting] WHERE [Setting_Id]= 'DbVersion') INSERT INTO [Base_Setting] ([Co_Id], [Setting_Desc], [Setting_Id], [Setting_Value], [Setting_Value_DataType], [Setting_Value_MaximumValue], [Setting_Value_MinimumValue], [Stamp_DateTime], [Stamp_User_Id], [User_Id]) VALUES (0, 'Database Version', 'DBVersion', '1.3.2.0', 'String', '', '', GetDate(), 0, 0) ")

      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.2.0.0 to version 1.3.0.0", ex)
      Throw _ExceptionObject
    End Try

  End Function

  Private Shared Function Upgrade01020000To01030000() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[del_Inv_GRN]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[del_Inv_GRN]")
      Scripts.Add("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[del_Inv_GRN_Detail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[del_Inv_GRN_Detail]")
      Scripts.Add("IF NOT EXISTS(SELECT CO_ID FROM Base_Company WHERE Co_ID = 0) INSERT INTO [Base_Company] ([Co_Id],[Co_Code],[Co_Desc],[Inactive_From],[Inactive_To],[Parent_Co_ID]) VALUES (0,'','No Company','1900/01/01','2999/01/01',0)")
      Scripts.Add("Update Inv_Item Set Stamp_DateTime = GetDate() Where Stamp_DateTime IS NULL")
      Scripts.Add("ALTER TABLE Inv_Item DROP CONSTRAINT IX_Inv_Item")
      Scripts.Add("ALTER TABLE Inv_Item ALTER COLUMN Item_Code VARCHAR(50)")
      Scripts.Add("ALTER TABLE Inv_Item ADD CONSTRAINT IX_Inv_Item UNIQUE (Co_ID, Item_Code)")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inv_Item' AND c.[Name]='Address_ID') ALTER TABLE Inv_Item ADD Address_ID BIGINT NULL")
      Scripts.Add("UPDATE Inv_Item SET Address_ID = 0")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inv_Item' AND c.[Name]='Address_ID') ALTER TABLE Inv_Item ADD Address_ID BIGINT NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inv_Item' AND c.[Name]='Party_ID') ALTER TABLE Inv_Item ADD Party_ID INT NULL")
      Scripts.Add("UPDATE Inv_Item SET Party_ID = 0")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inv_Item' AND c.[Name]='Party_ID') ALTER TABLE Inv_Item ADD Party_ID INT NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inv_Item' AND c.[Name]='Parent_Item_ID') ALTER TABLE Inv_Item ADD Parent_Item_ID INT NULL")
      Scripts.Add("UPDATE Inv_Item SET Parent_Item_ID = 0")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inv_Item' AND c.[Name]='Parent_Item_ID') ALTER TABLE Inv_Item ADD Parent_Item_ID INT NOT NULL")
      Scripts.Add("Update Base_Setting Set Co_Id = 0 Where Co_Id IS NULL")
      Scripts.Add("Update Base_Setting Set User_Id = 0 Where User_Id IS NULL")
      Scripts.Add("Update Base_Setting Set Setting_Desc = '' Where Setting_Desc IS NULL")
      Scripts.Add("Update Base_Setting Set Setting_Value = '' Where Setting_Value IS NULL")
      Scripts.Add("Update Base_Setting Set Stamp_User_Id = 0 Where Stamp_User_Id IS NULL")
      Scripts.Add("if exists (select * from dbo.sysobjects where id = object_id(N'PK_Base_Setting')) alter table Base_Setting drop Constraint PK_Base_Setting")
      Scripts.Add("if exists (select * from dbo.sysobjects where id = object_id(N'FK_Base_Setting_Base_Company')) alter table Base_Setting drop Constraint FK_Base_Setting_Base_Company")
      Scripts.Add("Alter Table Base_Setting Alter Column Co_Id SMALLINT NOT NULL")
      Scripts.Add("Alter Table Base_Setting Alter Column User_Id INT NOT NULL")
      Scripts.Add("Alter Table Base_Setting Alter Column Setting_Desc VARCHAR(200) NOT NULL")
      Scripts.Add("Alter Table Base_Setting Alter Column Setting_Value VARCHAR(50) NOT NULL")
      Scripts.Add("Alter Table Base_Setting Alter Column Stamp_User_Id INT NOT NULL")
      Scripts.Add("if NOT exists (select * from dbo.sysobjects where id = object_id(N'FK_Base_Setting_Base_Company')) alter table Base_Setting add Constraint FK_Base_Setting_Base_Company Foreign key (Co_Id) References Base_Company (Co_Id)")
      Scripts.Add("if NOT exists (select * from dbo.sysobjects where id = object_id(N'PK_Base_Setting')) alter table Base_Setting add Constraint PK_Base_Setting PRIMARY KEY (Co_Id,User_Id,Setting_Id)")
      Scripts.Add("Update Inv_Item Set Parent_Item_ID = 0 WHERE Parent_Item_ID IS NULL")
      Scripts.Add("Update Inv_Item Set Address_ID = 0 WHERE Address_ID IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[Base_Address]')) CREATE TABLE [dbo].[Base_Address]([Co_ID] [smallint] NOT NULL,[Address_ID] [bigint] NOT NULL,	[Parent_Address_ID] [bigint] NOT NULL,[AddressType_ID] [smallint] NOT NULL,[Address_Desc] [varchar](300) NOT NULL,[Stamp_UserID] [int] NOT NULL,[Stamp_DateTime] [datetime] NOT NULL, CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED (	[Co_ID] ASC,	[Address_ID] ASC))")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Accounting_Voucher' AND c.[Name]='Source_DocumentType_ID') ALTER TABLE Accounting_Voucher ADD Source_DocumentType_ID SMALLINT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Accounting_Voucher' AND c.[Name]='Source_ID') ALTER TABLE Accounting_Voucher ADD  Source_ID INT NULL")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Communication') DROP TABLE [dbo].[Communication]")
      Scripts.Add("IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Base_Communication') CREATE TABLE [dbo].[Base_Communication]([Communication_ID] [int] IDENTITY(1,1) NOT NULL,	[Communication_Type] [int] NOT NULL,	[Source_Type] [smallint] NOT NULL,	[Source_ID] [int] NOT NULL,	[Communication_Value] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, CONSTRAINT [PK_Communication] PRIMARY KEY CLUSTERED (	[Communication_ID] ASC))")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Base_Company' AND c.[Name]='Stamp_DateTime') ALTER TABLE Base_Company ADD Stamp_DateTime DATETIME NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Base_Company' AND c.[Name]='Stamp_UserID') ALTER TABLE Base_Company ADD Stamp_UserID INT NULL")
      Scripts.Add("UPDATE Base_Company SET Stamp_DateTime = GetDate() WHERE Stamp_DateTime IS NULL")
      Scripts.Add("UPDATE Base_Company SET Stamp_UserID = 0 WHERE Stamp_UserID IS NULL")
      Scripts.Add("ALTER TABLE Base_Company ALTER COLUMN Stamp_DateTime DATETIME NOT NULL")
      Scripts.Add("ALTER TABLE Base_Company ALTER COLUMN Stamp_UserID INT NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Base_Setting' AND c.[Name]='Setting_Value_DataType') ALTER TABLE Base_Setting ADD Setting_Value_DataType VARCHAR(10) NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Base_Setting' AND c.[Name]='Setting_Value_MaximumValue') ALTER TABLE Base_Setting ADD Setting_Value_MaximumValue VARCHAR(50) NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Base_Setting' AND c.[Name]='Setting_Value_MinimumValue') ALTER TABLE Base_Setting ADD Setting_Value_MinimumValue VARCHAR(50) NULL")
      Scripts.Add("IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Common_EntityType') CREATE TABLE [dbo].[Common_EntityType]([EntityType_ID] [int] NOT NULL, [EntityType_Code] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	[EntityType_Desc] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	[Stamp_UserID] [int] NOT NULL,	[Stamp_DateTime] [datetime] NOT NULL CONSTRAINT [DF_Common_EntityType_Stamp_DateTime]  DEFAULT (getdate()), CONSTRAINT [PK_Common_EntityType] PRIMARY KEY CLUSTERED (	[EntityType_ID] ASC))")
      Scripts.Add("IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Temp_SalesInvoiceReport') CREATE TABLE [dbo].[Temp_SalesInvoiceReport](	[Co_ID] [smallint] NOT NULL,	[Co_Desc] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	[Address_Desc] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	[Inventory_No] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Temp_SalesInvoiceReport_Inventory_No]  DEFAULT ((0)),	[Inventory_Date] [datetime] NOT NULL,	[Party_ID] [int] NOT NULL,	[Party_Desc] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	[Remarks] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	[DocumentType_ID] [smallint] NOT NULL CONSTRAINT [DF_Temp_SalesInvoiceReport_DocumentType_ID]  DEFAULT ((0)),	[Item_ID] [int] NOT NULL,	[Item_Code] [varchar](50) NOT NULL,	[Item_Desc] [varchar](50) NOT NULL,	[Size_Desc] [varchar](50) NOT NULL,	[Rate] [decimal](18, 0) NOT NULL,	[Qty] [decimal](18, 0) NOT NULL,	[Discount] [decimal](18, 0) NOT NULL,	[SalesTax] [decimal](18, 0) NOT NULL)")

      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.2.0.0 to version 1.3.0.0", ex)
      Throw _ExceptionObject
    End Try

  End Function

  Private Shared Function Upgrade01030000To01030100() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inv_Item_PriceHistory') CREATE TABLE [Inv_Item_PriceHistory]([Co_ID] [smallint] NOT NULL,	[Item_ID] [int] NOT NULL,[Item_PriceHistory_ID] [int] NOT NULL,	[Valid_From] [datetime] NULL,[Valid_Till] [datetime] NULL, [Party_ID] [int] NOT NULL,[Address_ID] [bigint] NOT NULL,[Item_PriceType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Item_Price_Size01] [decimal](18, 0) NOT NULL,[Item_Price_Size02] [decimal](18, 0) NOT NULL,[Item_Price_Size03] [decimal](18, 0) NOT NULL,[Item_Price_Size04] [decimal](18, 0) NOT NULL,[Item_Price_Size05] [decimal](18, 0) NOT NULL,[Item_Price_Size06] [decimal](18, 0) NOT NULL,[Item_Price_Size07] [decimal](18, 0) NOT NULL,[Item_Price_Size08] [decimal](18, 0) NOT NULL,[Item_Price_Size09] [decimal](18, 0) NOT NULL,[Item_Price_Size10] [decimal](18, 0) NOT NULL,[Item_Price_Size11] [decimal](18, 0) NOT NULL,[Item_Price_Size12] [decimal](18, 0) NOT NULL,[Item_Price_Size13] [decimal](18, 0) NOT NULL,[Stamp_UserID] [int] NOT NULL,[Stamp_DateTime] [datetime] NOT NULL, CONSTRAINT [PK_Inv_Item_PriceHistory] PRIMARY KEY CLUSTERED ([Co_ID] ASC,[Item_ID] ASC,[Item_PriceHistory_ID] ASC)) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Commission') ALTER TABLE Party ADD Commission DECIMAL NULL")
      Scripts.Add("UPDATE Party Set Commission = 0 WHERE Commission IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Commission') ALTER TABLE Party ADD Commission DECIMAL NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='EntityType_ID') ALTER TABLE Party ADD EntityType_ID INT NULL")
      Scripts.Add("UPDATE Party Set EntityType_ID = 0 WHERE EntityType_ID IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='EntityType_ID') ALTER TABLE Party ADD EntityType_ID INT NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Party_Code') ALTER TABLE Party ADD Party_Code VARCHAR(50) NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Stamp_DateTime') ALTER TABLE Party ADD Stamp_DateTime DATETIME NULL")
      Scripts.Add("UPDATE Party Set Stamp_DateTime = GetDate() WHERE Stamp_DateTime IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Stamp_DateTime') ALTER TABLE Party ADD Stamp_DateTime DATETIME NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Stamp_UserID') ALTER TABLE Party ADD Stamp_UserID INT NULL")
      Scripts.Add("UPDATE Party Set Stamp_UserID = 0 WHERE Stamp_UserID IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='Stamp_UserID') ALTER TABLE Party ADD Stamp_UserID INT NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='State') ALTER TABLE Party ADD State VARCHAR(50) NULL")
      Scripts.Add("UPDATE Party Set State = '' WHERE State IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='State') ALTER TABLE Party ADD State VARCHAR(50) NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='URL') ALTER TABLE Party ADD URL VARCHAR(100) NULL")
      Scripts.Add("UPDATE Party Set URL = '' WHERE URL IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Party' AND c.[Name]='URL') ALTER TABLE Party ADD URL VARCHAR(100) NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Sec_User' AND c.[Name]='Stamp_DateTime') ALTER TABLE Sec_User ADD Stamp_DateTime DATETIME NULL")
      Scripts.Add("UPDATE Sec_User Set Stamp_DateTime = '' WHERE Stamp_DateTime IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Sec_User' AND c.[Name]='Stamp_DateTime') ALTER TABLE Sec_User ADD Stamp_DateTime DATETIME NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Sec_User' AND c.[Name]='Stamp_UserID') ALTER TABLE Sec_User ADD Stamp_UserID INT NULL")
      Scripts.Add("UPDATE Sec_User Set Stamp_UserID = 0 WHERE Stamp_UserID IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Sec_User' AND c.[Name]='Stamp_UserID') ALTER TABLE Sec_User ADD Stamp_UserID INT NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inventory_SalesInvoice') CREATE TABLE [Inventory_SalesInvoice]([Co_ID] [smallint] NOT NULL,[Inventory_ID] [int] NOT NULL,[SalesMan_ID] [int] NOT NULL,[Discount] [money] NOT NULL,[SalesTax] [money] NOT NULL,[Stamp_DateTime] [datetime] NOT NULL,[Stamp_UserID] [int] NOT NULL, CONSTRAINT [PK_Inventory_SalesInvoice] PRIMARY KEY CLUSTERED ([Co_ID] ASC,[Inventory_ID] ASC))")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='Inactive') DROP FUNCTION Inactive")
      Scripts.Add("CREATE FUNCTION [Inactive] (@Inactive_From datetime,@Inactive_To datetime,@CurrentDateTime DATETIME) RETURNS bit AS BEGIN	DECLARE @Result bit If @Inactive_From IS NULL And @Inactive_To IS NULL Set @Result=0 Else If @Inactive_From IS NULL And @Inactive_To > @CurrentDateTime Set @Result=1 Else If @Inactive_From < @CurrentDateTime And @Inactive_To IS NULL Set @Result=1 Else If @Inactive_From < @CurrentDateTime And @Inactive_To > @CurrentDateTime Set @Result=1 Else Set @Result=0 RETURN @Result END")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetAccountsDetails') DROP PROCEDURE spGetAccountsDetails")
      Scripts.Add("CREATE PROCEDURE [spGetAccountsDetails] AS BEGIN SET NOCOUNT ON; SELECT [Voucher_No],v.[VoucherType_ID],vt.[VoucherType_Code],vt.[VoucherType_Desc],[Voucher_Date],[Status_ID],[Remarks],vd.[Co_ID],vd.[Voucher_ID],[VoucherDetail_ID],vd.[COA_ID],coa.[COA_Code],coa.[COA_Desc],[Narration],[DebitAmount],[CreditAmount],vd.[Stamp_User_Id],vd.[Stamp_DateTime] FROM Accounting_Voucher v RIGHT OUTER JOIN Accounting_Voucher_Detail vd ON v.Co_ID = vd.Co_ID AND v.Voucher_ID = vd.Voucher_ID LEFT OUTER JOIN Accounting_VoucherType vt ON v.Co_ID = vt.Co_ID LEFT OUTER JOIN Accounting_COA coa ON vd.Co_ID = coa.Co_ID AND vd.COA_ID = coa.COA_ID END")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetInventoryDetails') DROP PROCEDURE spGetInventoryDetails")
      Scripts.Add("CREATE PROCEDURE [dbo].[spGetInventoryDetails] AS BEGIN SET NOCOUNT ON; SELECT inv.Co_ID, Co.Co_Desc, invdet.Item_ID, inv.Party_ID, item.Item_Code, item.Item_Desc, cat.Item_Desc AS Item_Category,inv.Inventory_Date, inv.Payment_Mode, inv.Remarks, inv.Inventory_ID, inv.Inventory_No, invdet.DocumentType_ID, doc.DocumentType_Desc,invdet.Inventory_Qty_Size01, invdet.Inventory_Qty_Size02, invdet.Inventory_Qty_Size03, invdet.Inventory_Qty_Size04, invdet.Inventory_Qty_Size05, invdet.Inventory_Qty_Size06, invdet.Inventory_Qty_Size07, invdet.Inventory_Qty_Size08, invdet.Inventory_Qty_Size09, invdet.Inventory_Qty_Size10, invdet.Inventory_Qty_Size11, invdet.Inventory_Qty_Size12, invdet.Inventory_Qty_Size13, invdet.Inventory_Rate_Size01, invdet.Inventory_Rate_Size02, invdet.Inventory_Rate_Size03, invdet.Inventory_Rate_Size04, invdet.Inventory_Rate_Size05, invdet.Inventory_Rate_Size06, invdet.Inventory_Rate_Size07, invdet.Inventory_Rate_Size08, invdet.Inventory_Rate_Size09, invdet.Inventory_Rate_Size10, invdet.Inventory_Rate_Size11, invdet.Inventory_Rate_Size13, invdet.Inventory_Rate_Size12, dbo.Party.Party_Desc, dbo.Party.COA_ID, dbo.Party.City FROM dbo.Inv_Item cat RIGHT OUTER JOIN dbo.Inv_Item AS item  ON cat.Co_ID = item.Co_ID AND LEFT(item.Item_Code,2) = cat.Item_Code RIGHT JOIN dbo.Inventory_Detail AS invdet  ON item.Co_ID = invdet.Co_ID AND item.Item_ID = invdet.Item_ID  INNER JOIN dbo.Inventory AS inv  ON inv.Co_ID = invdet.Co_ID AND inv.Inventory_ID = invdet.Inventory_ID  LEFT OUTER JOIN dbo.Party  ON inv.Co_ID = dbo.Party.Co_ID AND inv.Party_ID = dbo.Party.Party_ID LEFT OUTER JOIN Base_Company Co ON inv.Co_ID = Co.Co_ID LEFT OUTER JOIN Common_DocumentType doc ON invdet.DocumentType_ID = doc.DocumentType_ID END")

      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.0.0 to version 1.3.1.0", ex)
      Throw _ExceptionObject
    End Try

  End Function

  Private Shared Function Upgrade01030100To01030200() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (Select Setting_Id From [Base_Setting] Where [Co_Id]=0 AND [User_Id]=0 AND [Setting_Id]='DisplayRecordOperationMessage') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType]) VALUES(0,0,'DisplayRecordOperationMessage','Whether or not to display the record operation message such as saved, deleted etc.','1',1,GetDate(),'Boolean')")

      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.1.0 to version 1.3.2.0", ex)
      Throw _ExceptionObject
    End Try

  End Function

  Private Shared Function Upgrade01030200To01030300() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetInventoryDetails') DROP PROCEDURE spGetInventoryDetails")
      Scripts.Add("CREATE PROCEDURE [dbo].[spGetInventoryDetails] AS BEGIN SELECT inv.Co_ID, Co.Co_Desc, invdet.Item_ID, inv.Party_ID, item.Item_Code, item.Item_Desc, cat.Item_Desc AS Item_Category,inv.Inventory_Date, inv.Payment_Mode, inv.Remarks, inv.Inventory_ID, inv.Inventory_No, invdet.DocumentType_ID, doc.DocumentType_Desc,invdet.Inventory_Qty_Size01, invdet.Inventory_Qty_Size02, invdet.Inventory_Qty_Size03, invdet.Inventory_Qty_Size04, invdet.Inventory_Qty_Size05, invdet.Inventory_Qty_Size06, invdet.Inventory_Qty_Size07, invdet.Inventory_Qty_Size08, invdet.Inventory_Qty_Size09, invdet.Inventory_Qty_Size10, invdet.Inventory_Qty_Size11, invdet.Inventory_Qty_Size12, invdet.Inventory_Qty_Size13, invdet.Inventory_Rate_Size01, invdet.Inventory_Rate_Size02, invdet.Inventory_Rate_Size03, invdet.Inventory_Rate_Size04, invdet.Inventory_Rate_Size05, invdet.Inventory_Rate_Size06, invdet.Inventory_Rate_Size07, invdet.Inventory_Rate_Size08, invdet.Inventory_Rate_Size09, invdet.Inventory_Rate_Size10, invdet.Inventory_Rate_Size11, invdet.Inventory_Rate_Size13, invdet.Inventory_Rate_Size12, dbo.Party.Party_Desc, dbo.Party.COA_ID, dbo.Party.City, dbo.Party.Country, dbo.Party.Address FROM dbo.Inv_Item cat RIGHT OUTER JOIN dbo.Inv_Item AS item  ON cat.Co_ID = item.Co_ID AND LEFT(item.Item_Code,2) = cat.Item_Code RIGHT JOIN dbo.Inventory_Detail AS invdet  ON item.Co_ID = invdet.Co_ID AND item.Item_ID = invdet.Item_ID  INNER JOIN dbo.Inventory AS inv  ON inv.Co_ID = invdet.Co_ID AND inv.Inventory_ID = invdet.Inventory_ID  LEFT OUTER JOIN dbo.Party  ON inv.Co_ID = dbo.Party.Co_ID AND inv.Party_ID = dbo.Party.Party_ID LEFT OUTER JOIN Base_Company Co ON inv.Co_ID = Co.Co_ID LEFT OUTER JOIN Common_DocumentType doc ON invdet.DocumentType_ID = doc.DocumentType_ID END")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='TableName') ALTER TABLE Transfer ADD TableName VARCHAR(250)")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetAccountsDetails') DROP PROCEDURE spGetAccountsDetails")
      Scripts.Add("CREATE PROCEDURE [dbo].[spGetAccountsDetails] AS BEGIN SET NOCOUNT ON; SELECT [Voucher_No],v.[VoucherType_ID],vt.[VoucherType_Code],vt.[VoucherType_Desc],[Voucher_Date],[Status_ID],[Remarks],vd.[Co_ID],vd.[Voucher_ID],[VoucherDetail_ID],vd.[COA_ID],coa.[COA_Code],coa.[COA_Desc],[Narration],[DebitAmount],[CreditAmount],vd.[Stamp_User_Id],vd.[Stamp_DateTime] FROM Accounting_Voucher v RIGHT OUTER JOIN Accounting_Voucher_Detail vd ON v.Co_ID = vd.Co_ID AND v.Voucher_ID = vd.Voucher_ID LEFT OUTER JOIN Accounting_VoucherType vt ON v.Co_ID = vt.Co_ID AND v.VoucherType_ID = vt.VoucherType_ID LEFT OUTER JOIN Accounting_COA coa ON vd.Co_ID = coa.Co_ID AND vd.COA_ID = coa.COA_ID WHERE v.Status_ID <> 4 END")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_Company_Module_Association]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_Company_Module_Association] ([Co_ID] smallint NOT NULL, [Module_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_Module]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_Module] ([Module_Desc] varchar (250) NOT NULL, [Module_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_Module_Role_Association]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_Module_Role_Association] ([Co_ID] smallint NOT NULL, [Module_ID] int NOT NULL, [Role_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_Right]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_Right] ([Right_Desc] varchar (250) NOT NULL, [Right_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_Role]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_Role] ([Co_ID] smallint NOT NULL, [Role_Desc] varchar (250) NOT NULL, [Role_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Sec_Role_Right_Association]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [Sec_Role_Right_Association] ([Right_ID] int NOT NULL, [Role_ID] int NOT NULL, [Stamp_DateTime] datetime NOT NULL, [Stamp_UserID] int NOT NULL)")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=5) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(5,'Inventory Item')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=6) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(6,'Party')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=7) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(7,'Company')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=8) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(8,'User')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=9) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(9,'COA')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=10) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(10,'Voucher Type')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=11) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(11,'Application Setting')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=12) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(12,'Payment Voucher')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=13) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(13,'Receipt Voucher')")
      Scripts.Add("IF NOT EXISTS (Select DocumentType_ID From [Common_DocumentType] Where [DocumentType_ID]=14) INSERT INTO [Common_DocumentType] ([DocumentType_ID],[DocumentType_Desc]) VALUES(14,'Purchase Order')")
      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.2.0 to version 1.3.3.0", ex)
      Throw _ExceptionObject
    End Try

  End Function

  Private Shared Function Upgrade01030300To01030400() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Base_Alert]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[Base_Alert]([Co_ID] [int] NOT NULL,	[Alert_ID] [int] NOT NULL, [Alert_Source] [varchar](100) NOT NULL, [Alert_Destination] [varchar](500) NOT NULL, [Alert_DateTime] [datetime] NOT NULL, [Alert_Subject] [varchar](100) NOT NULL, [Alert_Body] [varchar](4000) NOT NULL, [Alert_Type] [smallint] NOT NULL, [NoOfTries] [smallint] NOT NULL, [Status_ID] [int] NOT NULL, [Stamp_UserID] [int] NOT NULL, [Stamp_DateTime] [datetime] NOT NULL, CONSTRAINT [PK_Base_Alert] PRIMARY KEY CLUSTERED ([Co_ID] ASC, [Alert_ID] ASC) ON [PRIMARY]) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='FormatDateToDisplay') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'FormatDateToDisplay','Format of the date','dd-MMM-yy',1,GetDate(),'String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='FormatDateForInput') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'FormatDateForInput','Format of the date','dd-MM-yy',1,GetDate(),'String',NULL,NULL)")
      Scripts.Add("IF EXISTS (SELECT o.[NAME] FROM sysobjects o WHERE o.[Name] ='spGetInventoryDetails') DROP PROCEDURE spGetInventoryDetails")
      Scripts.Add("CREATE PROCEDURE [dbo].[spGetInventoryDetails] AS BEGIN SELECT inv.Co_ID, Co.Co_Desc, invdet.Item_ID, inv.Party_ID, item.Item_Code, item.Item_Desc, cat.Item_Desc AS Item_Category,inv.Inventory_Date, inv.Payment_Mode, inv.Remarks, inv.Inventory_ID, inv.Inventory_No, invdet.DocumentType_ID, doc.DocumentType_Desc,invdet.Inventory_Qty_Size01, invdet.Inventory_Qty_Size02, invdet.Inventory_Qty_Size03, invdet.Inventory_Qty_Size04, invdet.Inventory_Qty_Size05, invdet.Inventory_Qty_Size06, invdet.Inventory_Qty_Size07, invdet.Inventory_Qty_Size08, invdet.Inventory_Qty_Size09, invdet.Inventory_Qty_Size10, invdet.Inventory_Qty_Size11, invdet.Inventory_Qty_Size12, invdet.Inventory_Qty_Size13, invdet.Inventory_Rate_Size01, invdet.Inventory_Rate_Size02, invdet.Inventory_Rate_Size03, invdet.Inventory_Rate_Size04, invdet.Inventory_Rate_Size05, invdet.Inventory_Rate_Size06, invdet.Inventory_Rate_Size07, invdet.Inventory_Rate_Size08, invdet.Inventory_Rate_Size09, invdet.Inventory_Rate_Size10, invdet.Inventory_Rate_Size11, invdet.Inventory_Rate_Size13, invdet.Inventory_Rate_Size12, dbo.Party.Party_Desc, dbo.Party.COA_ID, dbo.Party.City, dbo.Party.Country, dbo.Party.Address FROM dbo.Inv_Item cat RIGHT OUTER JOIN dbo.Inv_Item AS item  ON cat.Co_ID = item.Co_ID AND LEFT(item.Item_Code,2) = cat.Item_Code RIGHT JOIN dbo.Inventory_Detail AS invdet  ON item.Co_ID = invdet.Co_ID AND item.Item_ID = invdet.Item_ID  INNER JOIN dbo.Inventory AS inv  ON inv.Co_ID = invdet.Co_ID AND inv.Inventory_ID = invdet.Inventory_ID  LEFT OUTER JOIN dbo.Party  ON inv.Co_ID = dbo.Party.Co_ID AND inv.Party_ID = dbo.Party.Party_ID LEFT OUTER JOIN Base_Company Co ON inv.Co_ID = Co.Co_ID LEFT OUTER JOIN Common_DocumentType doc ON invdet.DocumentType_ID = doc.DocumentType_ID WHERE inv.Status_ID <> 4 END")
      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.3.0 to version 1.3.4.0", ex)
      Throw _ExceptionObject
    End Try
  End Function

  Private Shared Function Upgrade01030401To01030500() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inv_Stock]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[Inv_Stock]([Co_ID] [smallint] NOT NULL,	[Source_First_ID] [int] NOT NULL,[Source_Second_ID] [int] NOT NULL,[Source_DocumentType_ID] [int] NOT NULL,[Item_Stock_Size01] [decimal](18, 0) NOT NULL,[Item_Stock_Size02] [decimal](18, 0) NOT NULL,[Item_Stock_Size03] [decimal](18, 0) NOT NULL,[Item_Stock_Size04] [decimal](18, 0) NOT NULL, [Item_Stock_Size05] [decimal](18, 0) NOT NULL, [Item_Stock_Size06] [decimal](18, 0) NOT NULL, [Item_Stock_Size07] [decimal](18, 0) NOT NULL, [Item_Stock_Size08] [decimal](18, 0) NOT NULL, [Item_Stock_Size09] [decimal](18, 0) NOT NULL, [Item_Stock_Size10] [decimal](18, 0) NOT NULL, [Item_Stock_Size11] [decimal](18, 0) NOT NULL, [Item_Stock_Size12] [decimal](18, 0) NOT NULL, [Item_Stock_Size13] [decimal](18, 0) NOT NULL, [Stamp_UserID] [int] NOT NULL, [Stamp_DateTime] [datetime] NOT NULL, CONSTRAINT [PK_Inv_Stock] PRIMARY KEY CLUSTERED ([Co_ID] ASC, [Source_First_ID] ASC, [Source_Second_ID] ASC, [Source_DocumentType_ID] ASC))")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size01') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size01','Item_Stock_Size01 Caption','100',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size02') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size02','Item_Stock_Size02 Caption','120',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size03') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size03','Item_Stock_Size03 Caption','130',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size04') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size04','Item_Stock_Size04 Caption','140',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size05') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size05','Item_Stock_Size05 Caption','150',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size06') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size06','Item_Stock_Size06 Caption','160',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size07') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size07','Item_Stock_Size07 Caption','170',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size08') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size08','Item_Stock_Size08 Caption','180',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size09') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size09','Item_Stock_Size09 Caption','190',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size10') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size10','Item_Stock_Size10 Caption','CM',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size11') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size11','Item_Stock_Size11 Caption','200',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size12') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size12','Item_Stock_Size12 Caption','210',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='DBColumnCaptionItem_Stock_Size13') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'DBColumnCaptionItem_Stock_Size13','Item_Stock_Size13 Caption','220',1,'2008/08/09','String',NULL,NULL)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inv_Stock]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[Inv_Stock]([Co_ID] [smallint] NOT NULL, [Source_First_ID] [int] NOT NULL, [Source_Second_ID] [int] NOT NULL, [Source_DocumentType_ID] [int] NOT NULL, [Item_Stock_Size01] [decimal](18, 0) NOT NULL, [Item_Stock_Size02] [decimal](18, 0) NOT NULL, [Item_Stock_Size03] [decimal](18, 0) NOT NULL, [Item_Stock_Size04] [decimal](18, 0) NOT NULL, [Item_Stock_Size05] [decimal](18, 0) NOT NULL, [Item_Stock_Size06] [decimal](18, 0) NOT NULL, [Item_Stock_Size07] [decimal](18, 0) NOT NULL, [Item_Stock_Size08] [decimal](18, 0) NOT NULL, [Item_Stock_Size09] [decimal](18, 0) NOT NULL, [Item_Stock_Size10] [decimal](18, 0) NOT NULL, [Item_Stock_Size11] [decimal](18, 0) NOT NULL, [Item_Stock_Size12] [decimal](18, 0) NOT NULL, [Item_Stock_Size13] [decimal](18, 0) NOT NULL, [Stamp_UserID] [int] NOT NULL, [Stamp_DateTime] [datetime] NOT NULL, CONSTRAINT [PK_Inv_Stock] PRIMARY KEY CLUSTERED ([Co_ID] ASC, [Source_First_ID] ASC, [Source_Second_ID] ASC, [Source_DocumentType_ID] ASC)) ON [PRIMARY]")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Accounting_VoucherType')) ALTER TABLE Accounting_Voucher DROP CONSTRAINT FK_Accounting_Voucher_Accounting_VoucherType")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Inv_SalesInvoice_Base_Company')) ALTER TABLE Inventory DROP CONSTRAINT FK_Inv_SalesInvoice_Base_Company")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Inv_SalesInvoice_Detail_Base_Company')) ALTER TABLE Inventory_Detail DROP CONSTRAINT FK_Inv_SalesInvoice_Detail_Base_Company")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_VoucherType_Base_Company')) ALTER TABLE Accounting_VoucherType DROP CONSTRAINT FK_Accounting_VoucherType_Base_Company")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Base_Company')) ALTER TABLE Accounting_Voucher DROP CONSTRAINT FK_Accounting_Voucher_Base_Company")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Base_Setting_Base_Company')) ALTER TABLE Base_Setting DROP CONSTRAINT FK_Base_Setting_Base_Company")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Table_1')) ALTER TABLE dbo.Base_Company DROP CONSTRAINT PK_Table_1")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_VoucherType_Accounting_VoucherType')) ALTER TABLE dbo.Accounting_VoucherType DROP CONSTRAINT FK_Accounting_VoucherType_Accounting_VoucherType")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Acconting_VoucherType')) ALTER TABLE dbo.Accounting_VoucherType DROP CONSTRAINT PK_Acconting_VoucherType")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Common_Status_1')) ALTER TABLE Common_Status DROP CONSTRAINT PK_Common_Status_1")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Inventory_Inventory')) ALTER TABLE Inventory DROP CONSTRAINT FK_Inventory_Inventory")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_SaleInvoice_Detail')) ALTER TABLE Inventory_Detail DROP CONSTRAINT PK_SaleInvoice_Detail")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Inv_SaleInvoice')) ALTER TABLE Inventory DROP CONSTRAINT PK_Inv_SaleInvoice")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Module_Association')) ALTER TABLE Sec_Company_Module_Association DROP CONSTRAINT PK_Sec_Module_Association")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_User_1')) ALTER TABLE Sec_User DROP CONSTRAINT PK_Sec_User_1")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Base_Company')) ALTER TABLE dbo.Base_Company ADD CONSTRAINT PK_Base_Company PRIMARY KEY CLUSTERED (Co_Id)  ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Accounting_VoucherType')) ALTER TABLE dbo.Accounting_VoucherType ADD CONSTRAINT PK_Accounting_VoucherType PRIMARY KEY CLUSTERED (Co_ID, VoucherType_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Accounting_Voucher')) ALTER TABLE dbo.Accounting_Voucher ADD CONSTRAINT PK_Accounting_Voucher PRIMARY KEY CLUSTERED (Co_ID, Voucher_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Accounting_Voucher_Detail')) ALTER TABLE dbo.Accounting_Voucher_Detail ADD CONSTRAINT PK_Accounting_Voucher_Detail PRIMARY KEY CLUSTERED (Co_ID, Voucher_ID, VoucherDetail_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Accounting_COA')) ALTER TABLE dbo.Accounting_COA ADD CONSTRAINT PK_Accounting_COA PRIMARY KEY CLUSTERED (Co_ID, COA_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Base_Setting')) ALTER TABLE dbo.Base_Setting ADD CONSTRAINT PK_Base_Setting PRIMARY KEY CLUSTERED (Co_Id, Setting_Id, User_Id) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Common_DocumentType')) ALTER TABLE dbo.Common_DocumentType ADD CONSTRAINT PK_Common_DocumentType PRIMARY KEY CLUSTERED (DocumentType_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Common_EntityType')) ALTER TABLE dbo.Common_EntityType ADD CONSTRAINT PK_Common_EntityType PRIMARY KEY CLUSTERED (EntityType_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Common_Status')) ALTER TABLE dbo.Common_Status ADD CONSTRAINT PK_Common_Status PRIMARY KEY CLUSTERED (Status_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Common_Status_Type')) ALTER TABLE dbo.Common_Status_Type ADD CONSTRAINT PK_Common_Status_Type PRIMARY KEY CLUSTERED (Status_Type_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Inv_Item')) ALTER TABLE dbo.Inv_Item ADD CONSTRAINT PK_Inv_Item PRIMARY KEY CLUSTERED (Co_ID, Item_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Inventory')) ALTER TABLE dbo.Inventory ADD CONSTRAINT PK_Inventory PRIMARY KEY CLUSTERED (Co_ID, Inventory_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Inventory_Detail')) ALTER TABLE dbo.Inventory_Detail ADD CONSTRAINT PK_Inventory_Detail PRIMARY KEY CLUSTERED (Co_ID, Inventory_ID, InventoryDetail_ID) ON [PRIMARY]")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Inventory_SalesInvoice')) ALTER TABLE dbo.Inventory_SalesInvoice ADD CONSTRAINT PK_Inventory_SalesInvoice PRIMARY KEY CLUSTERED (Co_ID, Inventory_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Party')) ALTER TABLE dbo.Party ADD CONSTRAINT PK_Party PRIMARY KEY CLUSTERED (Co_ID, Party_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Party_Detail')) ALTER TABLE dbo.Party_Detail ADD CONSTRAINT PK_Party_Detail PRIMARY KEY CLUSTERED (Co_ID, Party_Detail_ID, Party_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Person')) ALTER TABLE dbo.Person ADD CONSTRAINT PK_Person PRIMARY KEY CLUSTERED (Person_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Company_Module_Association')) ALTER TABLE dbo.Sec_Company_Module_Association ADD CONSTRAINT PK_Sec_Company_Module_Association PRIMARY KEY CLUSTERED (Co_ID, Module_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Module')) ALTER TABLE dbo.Sec_Module ADD CONSTRAINT PK_Sec_Module PRIMARY KEY CLUSTERED (Module_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Module_Role_Association')) ALTER TABLE dbo.Sec_Module_Role_Association ADD CONSTRAINT PK_Sec_Module_Role_Association PRIMARY KEY CLUSTERED (Co_ID, Module_ID, Role_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Right')) ALTER TABLE dbo.Sec_Right ADD CONSTRAINT PK_Sec_Right PRIMARY KEY CLUSTERED (Right_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Role')) ALTER TABLE dbo.Sec_Role ADD CONSTRAINT PK_Sec_Role PRIMARY KEY CLUSTERED (Co_ID, Role_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'Tmp_Sec_Role_Right_Association')) CREATE TABLE dbo.Tmp_Sec_Role_Right_Association (Co_ID int NOT NULL, Right_ID int NOT NULL, Role_ID int NOT NULL, Stamp_DateTime datetime NOT NULL, Stamp_UserID int NOT NULL)")
      Scripts.Add("IF EXISTS(SELECT * FROM dbo.Sec_Role_Right_Association)	 EXEC('INSERT INTO dbo.Tmp_Sec_Role_Right_Association (Right_ID, Role_ID, Stamp_DateTime, Stamp_UserID)	SELECT Right_ID, Role_ID, Stamp_DateTime, Stamp_UserID FROM dbo.Sec_Role_Right_Association WITH (HOLDLOCK TABLOCKX)')")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'Tmp_Sec_Role_Right_Association')) DROP TABLE dbo.Sec_Role_Right_Association")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'Tmp_Sec_Role_Right_Association')) EXECUTE sp_rename N'dbo.Tmp_Sec_Role_Right_Association', N'Sec_Role_Right_Association', 'OBJECT'")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_Role_Right_Association')) ALTER TABLE dbo.Sec_Role_Right_Association ADD CONSTRAINT PK_Sec_Role_Right_Association PRIMARY KEY CLUSTERED (Co_ID,Right_ID,Role_ID)")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Sec_User')) ALTER TABLE dbo.Sec_User ADD CONSTRAINT PK_Sec_User PRIMARY KEY CLUSTERED (Co_ID,[User_ID])")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'PK_Transfer')) ALTER TABLE dbo.Transfer ADD CONSTRAINT PK_Transfer PRIMARY KEY CLUSTERED (Co_ID,Transfer_ID)")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[FK_Accounting_Voucher_Detail_Accounting_COA]')) ALTER TABLE dbo.Accounting_Voucher_Detail DROP CONSTRAINT FK_Accounting_Voucher_Detail_Accounting_COA")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Base_Company')) ALTER TABLE dbo.Accounting_Voucher ADD CONSTRAINT FK_Accounting_Voucher_Base_Company FOREIGN KEY (Co_ID) REFERENCES dbo.Base_Company (Co_Id) ON UPDATE  NO ACTION ON DELETE  NO ACTION")
      'Scripts.Add("UPDATE Accounting_Voucher SET VoucherType_ID = 1 WHERE VoucherType_ID = 0 OR VoucherType_ID IS NULL")
      Scripts.Add("DELETE FROM Accounting_Voucher_Detail WHERE Co_ID = 0 OR Co_ID IS NULL")
      Scripts.Add("DELETE FROM Accounting_Voucher WHERE Co_ID = 0 OR Co_ID IS NULL")
      'Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[FK_Accounting_Voucher_Accounting_VoucherType]')) ALTER TABLE dbo.Accounting_Voucher DROP CONSTRAINT FK_Accounting_Voucher_Accounting_VoucherType")
      'Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Accounting_VoucherType')) ALTER TABLE dbo.Accounting_Voucher ADD CONSTRAINT FK_Accounting_Voucher_Accounting_VoucherType FOREIGN KEY (Co_ID, VoucherType_ID) REFERENCES dbo.Accounting_VoucherType (Co_ID, VoucherType_ID) ON UPDATE  NO ACTION ON DELETE  NO ACTION")
      'Above scripts will be executed when data is corrented.
      Scripts.Add("DELETE From Accounting_Voucher_Detail WHERE COA_ID NOT IN (SELECT COA_ID FROM Accounting_COA)")

      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Detail_Accounting_COA')) ALTER TABLE Accounting_Voucher_Detail DROP CONSTRAINT FK_Accounting_Voucher_Detail_Accounting_COA")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Detail_Accounting_Voucher')) ALTER TABLE dbo.Accounting_Voucher_Detail ADD CONSTRAINT FK_Accounting_Voucher_Detail_Accounting_Voucher FOREIGN KEY (Co_ID, Voucher_ID) REFERENCES dbo.Accounting_Voucher (Co_ID, Voucher_ID) ON UPDATE  NO ACTION ON DELETE  NO ACTION ")
      Scripts.Add("UPDATE Accounting_Voucher_Detail SET COA_ID = 1 WHERE COA_ID = 0 OR COA_ID IS NULL")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_Voucher_Detail_Accounting_COA')) ALTER TABLE dbo.Accounting_Voucher_Detail ADD CONSTRAINT FK_Accounting_Voucher_Detail_Accounting_COA FOREIGN KEY (Co_ID, COA_ID) REFERENCES dbo.Accounting_COA (Co_ID, COA_ID) ON UPDATE NO ACTION ON DELETE NO ACTION")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_VoucherType_Accounting_VoucherType')) ALTER TABLE dbo.Accounting_VoucherType DROP CONSTRAINT FK_Accounting_VoucherType_Accounting_VoucherType")
      Scripts.Add("IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'FK_Accounting_VoucherType_Base_Company')) ALTER TABLE dbo.Accounting_VoucherType ADD CONSTRAINT FK_Accounting_VoucherType_Base_Company FOREIGN KEY (Co_ID) REFERENCES dbo.Base_Company (Co_Id) ON UPDATE  NO ACTION ON DELETE  NO ACTION")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_DocumentType' AND c.[Name]='Stamp_DateTime') ALTER TABLE Common_DocumentType ADD Stamp_DateTime datetime NULL, Stamp_UserID int NULL")
      Scripts.Add("UPDATE Common_DocumentType SET Stamp_DateTime = '2008/01/01', Stamp_UserID = 0 WHERE Stamp_DateTime IS NULL AND Stamp_UserID IS NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_DocumentType' AND c.[Name]='Stamp_DateTime') ALTER TABLE Common_DocumentType ALTER COLUMN Stamp_DateTime datetime NOT NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_DocumentType' AND c.[Name]='Stamp_UserID') ALTER TABLE Common_DocumentType ALTER COLUMN Stamp_UserID int NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status' AND c.[Name]='Stamp_DateTime') ALTER TABLE Common_Status ADD Stamp_DateTime datetime NULL, Stamp_UserID int NULL")
      Scripts.Add("UPDATE Common_Status SET Stamp_DateTime = '2008/01/01', Stamp_UserID = 0 WHERE Stamp_DateTime IS NULL AND Stamp_UserID IS NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status' AND c.[Name]='Stamp_DateTime') ALTER TABLE Common_Status ALTER COLUMN Stamp_DateTime datetime NOT NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status' AND c.[Name]='Stamp_UserID') ALTER TABLE Common_Status ALTER COLUMN Stamp_UserID int NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status_Type' AND c.[Name]='Stamp_DateTime') ALTER TABLE Common_Status_Type ADD Stamp_DateTime datetime NULL, Stamp_UserID int NULL")
      Scripts.Add("UPDATE Common_Status_Type SET Stamp_DateTime = '2008/01/01', Stamp_UserID = 0 WHERE Stamp_DateTime IS NULL AND Stamp_UserID IS NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status_Type' AND c.[Name]='Stamp_DateTime') ALTER TABLE Common_Status_Type ALTER COLUMN Stamp_DateTime datetime NOT NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status_Type' AND c.[Name]='Stamp_UserID') ALTER TABLE Common_Status_Type ALTER COLUMN Stamp_UserID int NOT NULL")
      Scripts.Add("ALTER TABLE Base_Alert ALTER COLUMN Alert_Body VARCHAR(6000) NOT NULL")

      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.4.1 to version 1.3.5.0", ex)
      Throw _ExceptionObject
    End Try
  End Function

  Private Shared Function Upgrade1350To1360() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (Select [Setting_Id] From [Base_Setting] Where [Setting_Id]='" & SETTING_ID_EMAIL_AREA_MANAGER & "') INSERT INTO [Base_Setting] ([Co_Id],[User_Id],[Setting_Id],[Setting_Desc],[Setting_Value],[Stamp_User_Id],[Stamp_DateTime],[Setting_Value_DataType],[Setting_Value_MinimumValue],[Setting_Value_MaximumValue]) VALUES (0,0,'" & SETTING_ID_EMAIL_AREA_MANAGER & "','Area Manager Email Address','quickerp@uniformers.net',1,GetDate(),'String',NULL,NULL)")
      Scripts.Add("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[Inactive]')) DROP FUNCTION Inactive")
      Scripts.Add("CREATE FUNCTION [dbo].[Inactive] (@Inactive_From datetime,@Inactive_To datetime,@CurrentDateTime DATETIME) RETURNS bit AS BEGIN	DECLARE @Result bit If @Inactive_From IS NULL And @Inactive_To IS NULL Set @Result=0 Else If @Inactive_From IS NULL And @Inactive_To > @CurrentDateTime Set @Result=1 Else If @Inactive_From < @CurrentDateTime And @Inactive_To IS NULL Set @Result=1 Else If @Inactive_From < @CurrentDateTime And @Inactive_To > @CurrentDateTime Set @Result=1 Else Set @Result=0 RETURN @Result END")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inventory_SalesInvoice' AND c.[Name]='TotalCashReceived') ALTER TABLE Inventory_SalesInvoice ADD TotalCashReceived decimal NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Inventory_Detail' AND c.[Name]='Source_DocumentType_ID') ALTER TABLE Inventory_Detail ADD Source_DocumentType_ID smallint NULL, Source_Document_No int NULL, Source_Document_Co_ID smallint NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Common_Status_Type' AND c.[Name]='Stamp_UserID') ALTER TABLE Common_Status_Type ALTER COLUMN Stamp_UserID int NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Stamp_UserID') ALTER TABLE Transfer ADD Stamp_UserID int NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Stamp_DateTime') ALTER TABLE Transfer ADD Stamp_DateTime datetime NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Stamp_UserID') UPDATE Transfer SET Stamp_UserID = 0 WHERE Stamp_UserID IS NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Stamp_DateTime') UPDATE Transfer SET Stamp_DateTime = GetDate() WHERE Stamp_DateTime IS NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Stamp_UserID') ALTER TABLE Transfer ALTER COLUMN Stamp_UserID int NOT NULL")
      Scripts.Add("IF EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Stamp_DateTime') ALTER TABLE Transfer ALTER COLUMN Stamp_DateTime datetime NOT NULL")
      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Download_DateTime') ALTER TABLE Transfer ADD Download_DateTime datetime NULL")
      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.5.0 to version 1.3.6.0", ex)
      Throw _ExceptionObject
    End Try
  End Function

  Private Shared Function Upgrade1360To1370() As ArrayList
    Try
      Dim Scripts As New ArrayList

      Scripts.Add("IF NOT EXISTS (SELECT C.[NAME] FROM sysobjects o INNER JOIN syscolumns c ON o.id=c.id WHERE o.[Name] ='Transfer' AND c.[Name]='Download_DateTime') ALTER TABLE Transfer ADD Download_DateTime datetime NULL")
      Return Scripts

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in upgrading database from version 1.3.6.0 to version 1.3.7.0", ex)
      Throw _ExceptionObject
    End Try
  End Function
#End Region

End Class