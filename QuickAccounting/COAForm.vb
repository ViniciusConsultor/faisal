Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
'Imports QuickDAL.QuickCommonDataSet
'Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDALLibrary
Imports QuickLibrary
Imports QuickControls.Quick_UltraComboBoxWithLabel


'Author: Muhammad Zakee
'Date Created(26-Jan-10): 2010
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'Zakee   26-Jan-10        Removes Logged bugs
'Zakee   20-May-10        Removes all bugs logged by Mr. Fasial Saleem
''' <summary>
''' COA form.
''' </summary>
Public Class COAForm

#Region "Declarations"
  Private _AccountingCOATableAdapterObject As New QuickAccountingDataSetTableAdapters.COATableAdapter
  Private _CompanyTableAdapterObject As New QuickCommonDataSetTableAdapters.CompanyTableAdapter
  Private _FinancialAccountTypeTableAdapter As New QuickAccountingDataSetTableAdapters.FinancialAccountTypeTableAdapter
  Private _CashFlowAccountTableAdapterObject As New QuickAccountingDataSetTableAdapters.CashFlowAccountTableAdapter
  Private _AccountingCOATable As New QuickAccountingDataSet.COADataTable
  Private _CurrentDataTable As New QuickAccountingDataSet.COADataTable
  Private _FinancialAccountTypeDataTable As New QuickAccountingDataSet.FinancialAccountTypeDataTable
  Private _CashFlowAccountDataTable As New QuickAccountingDataSet.CashFlowAccountDataTable

  Private _CurrentAccountingCOADataRow As QuickAccountingDataSet.COARow

  Private _MaskCOACode As String = Nothing
  Private _TotalLevels As Int32
  Private _CoaCode As String
  Private _LevelNo As Int16
  Private _CheckCOACodeHaveChilds As String = String.Empty
  Private _CurrentCOACode As String
  Private _CheckValidChartOfAccount As Boolean = False
  Private _CheckValidParentCode As Boolean = False
  Private _IsShowingRecord As Boolean = False
#End Region

#Region "Properties"

#End Region

#Region "Methods"

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

  'Author: Zakee
  'Date Created(DD-MMM-YY): 26-Jan-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub ShowParentComboCOACode()
    Try
      '  If _CheckValidParentCode = True Then
      Dim _LevelNo As Int16 = 0
      Dim _CountLevel As Integer = 0
      Dim _ParentCOACode As String = String.Empty
      _CurrentCOACode = String.Empty

      If Me.COACodeTextBox.Text <> String.Empty Then
        If Me._MaskCOACode.Length <> Me.COACodeTextBox.Text.Length Then
          For i As Int32 = 0 To Me.COACodeTextBox.Text.Length - 1
            If Me.COACodeTextBox.Text.Substring(i, 1) = "-" Then
              _CurrentCOACode = Me.COACodeTextBox.Text.Substring(0, i)
              If Me.COACodeTextBox.Text.Length <> i + 1 Then
                If Me.COACodeTextBox.Text.Substring(i + 1, 1) = " " Then
                  Exit For
                Else
                  _CurrentCOACode = Me.COACodeTextBox.Text.Substring(0, i)
                End If
              End If
            End If
          Next
        Else
          _CurrentCOACode = Me.COACodeTextBox.Text
        End If

        For i As Int16 = 0 To CShort(_CurrentCOACode.Length - 1)
          If _CurrentCOACode.Substring(i, 1) = "-" Then
            _CountLevel = _CountLevel + 1
          End If
        Next
        If _CountLevel = 0 Then
          Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value = Nothing
          GoTo skip
        End If

        For i As Int16 = 1 To CShort(_CurrentCOACode.Length)
          If _CurrentCOACode.Substring(i - 1, 1) = "-" Then
            _CountLevel = _CountLevel - 1
            If _CountLevel = 0 Then
              Exit For
            End If
          Else
            _ParentCOACode = _CurrentCOACode.Substring(0, i)
          End If
        Next
        Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value = _ParentCOACode
      End If
skip:
      _CountLevel = 0
      For i As Int16 = 0 To CShort(_CurrentCOACode.Length - 1)
        If _CurrentCOACode.Substring(i, 1) = "-" Then
          _CountLevel += 1
        End If

        If _CountLevel = 1 Then
          If Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value Is Nothing Then
            Me._CheckValidChartOfAccount = True
            'QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Entered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
            Exit Sub
          Else
            Me._CheckValidChartOfAccount = False
          End If
        End If
      Next
      '   End If
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Chart of Account TextBox Leave of COAForm.", ex)
      Throw _qex
    End Try

  End Sub

  Private Function IsValid() As Boolean
    Try
      Dim _TempTable As QuickAccountingDataSet.COADataTable
      Dim _LevelCharCount As Int16   ' count the no of characters before dash
      Dim _TotolCharCount As Int16
      Dim _LastLevelNo As Int16 = 1
      Dim _LastLevelChar As Int16
      Dim _CheckSpaces As Int16 = 0
      Dim _CheckValidItem As Int16 = 0
      Dim _CheckAfterSpaceValue As Int16

      For i As Int16 = 0 To CShort(Me.COACodeTextBox.Text.Length - 1)
        If Me.COACodeTextBox.Text.Substring(i, 1) = "-" Then
          For j As Int16 = CShort(_TotolCharCount - _LevelCharCount) To CShort(_TotolCharCount - 1)
            If Me.COACodeTextBox.Text.Substring(j, 1) = " " Then
              _CheckSpaces = CShort(_CheckSpaces + 1)
            ElseIf Me.COACodeTextBox.Text.Substring(j, 1) <> " " Then
              _CheckValidItem = CShort(_CheckValidItem + 1)
            End If
          Next
          If _CheckSpaces = _LevelCharCount Then
            _CheckAfterSpaceValue = 1
          End If
          If _CheckValidItem = _LevelCharCount Then
            If _CheckAfterSpaceValue = 1 Then
              _CheckAfterSpaceValue = 2
            End If
          End If
          If _CheckSpaces = _LevelCharCount Or _CheckValidItem = _LevelCharCount Then
          Else
            QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Code entered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Me.COACodeTextBox.Focus()
            Return False
          End If
          If _CheckAfterSpaceValue = 2 Then
            QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Code entered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Me.COACodeTextBox.Focus()
            Return False
          End If
          _LevelCharCount = 0
          _TotolCharCount = CShort(_TotolCharCount + 1)
          _CheckValidItem = 0
          _CheckSpaces = 0

        Else
          _LevelCharCount = CShort(_LevelCharCount + 1)
          _TotolCharCount = CShort(_TotolCharCount + 1)
        End If
      Next

      ' Get Last level of masking text box and last level char of masked text box
      For I As Int32 = 0 To Me._MaskCOACode.Length - 1
        If _MaskCOACode.Substring(I, 1) = "-" Then
          _LastLevelNo = CShort(_LastLevelNo + 1)
          _LastLevelChar = 0
        Else
          _LastLevelChar = CShort(_LastLevelChar + 1)
        End If
      Next
      ' Check last level of masked COA code
      If Me._MaskCOACode.Length - Me.COACodeTextBox.Text.Length <> _LastLevelChar Then
        For i As Int16 = CShort(Me._MaskCOACode.Length - _LastLevelNo) To CShort((Me._MaskCOACode.Length - 1))
          If Me.COACodeTextBox.Text.Length <> Me._MaskCOACode.Length Then
            QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Code entered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Me.COACodeTextBox.Focus()
            Return False
          End If
          If Me.COACodeTextBox.Text.Substring(i, 1) = " " Then
            QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Code entered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Me.COACodeTextBox.Focus()
            Return False
          End If
        Next
      End If


      '  Dim _COACode As String = String.Empty
      '  Dim _LevelNo As Int16 = 0
      Me._CoaCode = String.Empty
      Me._LevelNo = 0
      If Me._MaskCOACode.Length <> Me.COACodeTextBox.Text.Length Then
        For i As Int32 = 0 To Me.COACodeTextBox.Text.Length - 1
          If Me.COACodeTextBox.Text.Substring(i, 1) = "-" Then
            _CoaCode = Me.COACodeTextBox.Text.Substring(0, i)
            If Me.COACodeTextBox.Text.Length <> i + 1 Then
              If Me.COACodeTextBox.Text.Substring(i + 1, 1) = " " Then
                Exit For
              Else
                _CoaCode = Me.COACodeTextBox.Text.Substring(0, i)
              End If
            End If
          End If
        Next
      Else
        _CoaCode = Me.COACodeTextBox.Text
      End If

      If Me._MaskCOACode.Length <> Me.COACodeTextBox.Text.Length Then
        For i As Int32 = 0 To _CoaCode.Length - 1
          If _CoaCode.Substring(i, 1) = "-" Then
            _LevelNo = CShort(_LevelNo + 1)
          End If
        Next
      Else
        For i As Int32 = 0 To Me.COACodeTextBox.Text.Length - 1
          If Me.COACodeTextBox.Text.Substring(i, 1) = "-" Then
            _LevelNo = CShort(_LevelNo + 1)
          End If
        Next
      End If


      ' Check Duplication of COA Code
      If Me._CurrentCOACode <> Nothing Then
        _TempTable = Me._AccountingCOATableAdapterObject.GetByCoIDCOACode(Me.CompanyComboBox.CompanyID, Me._CurrentCOACode)
        If _TempTable.Rows.Count > 0 Then
          If Me._CurrentAccountingCOADataRow Is Nothing Then
            QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate COA code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Me.COACodeTextBox.Focus()
            Return False
          End If
        End If
      End If

      ' Validation of COA code have any child
      '_TempTable.Rows.Clear()
      If Me._CheckCOACodeHaveChilds <> String.Empty Then
        Me._CheckCOACodeHaveChilds = Me._CheckCOACodeHaveChilds & "-%"
        _TempTable = Me._AccountingCOATableAdapterObject.GetByCOACodeChilds(Me.CompanyComboBox.CompanyID, Me._CheckCOACodeHaveChilds)
        If _TempTable.Rows.Count > 0 Then
          'QuickMessageBox.Show(Me.LoginInfoObject, "This Account code have childs because you cannot change chart of account code", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          'Return False
        End If
      End If



      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
        'ElseIf Me.COACodeTextBox.Text.Trim = String.Empty Then
        '  QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        '  Me.COACodeTextBox.Focus()
        '  Return False
      ElseIf Me._CoaCode = "  " Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.COACodeTextBox.Focus()
        Return False
      ElseIf Me.COADescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.COADescTextBox.Focus()
        Return False
      ElseIf (Me.OpeningDebitNumericEditor.Value IsNot Nothing AndAlso Me.OpeningCreditNumericEditor IsNot Nothing) AndAlso (Convert.ToDecimal(Me.OpeningDebitNumericEditor.Value) > 0 AndAlso Convert.ToDecimal(Me.OpeningCreditNumericEditor.Value) > 0) Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Opening Debit and Opening Credit both cannot have value in them.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.OpeningDebitNumericEditor.Focus()
        Return False
        'ElseIf Me.FinancialAccountComboBox.Text = String.Empty Then
        '  QuickMessageBox.Show(Me.LoginInfoObject, "You should Select the Financial Account to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        '  Me.FinancialAccountComboBox.Focus()
        '  Return False
        'ElseIf Me.CashFlowAccountComboBox.Text = String.Empty Then
        '  QuickMessageBox.Show(Me.LoginInfoObject, "You should Select the Cash Flow Account to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        '  Me.CashFlowAccountComboBox.Focus()
        '  Return False
      ElseIf Me._CheckValidChartOfAccount = True Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account code entered", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.COACodeTextBox.Focus()
        Return False
      Else
        Me._CheckValidChartOfAccount = False
        Return True
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      _IsShowingRecord = True

      Me._CheckCOACodeHaveChilds = String.Empty
      Me.CompanyComboBox.ReadOnly = True
      If Me._AccountingCOATable.Rows.Count > 0 Then
        Me._CurrentAccountingCOADataRow = Me._AccountingCOATable(Me.CurrentRecordIndex)
        Me.ClearControls(Me)

        Me.CompanyComboBox.CompanyID = _CurrentAccountingCOADataRow.Co_ID
        Me.COAIDTextBox.Text = _CurrentAccountingCOADataRow.COA_ID.ToString()
        If Me._CurrentAccountingCOADataRow.Parent_COA_ID <> 0 Then
          Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value = Me._AccountingCOATableAdapterObject.GetCOACodeByCoIDCOAID(Me.LoginInfoObject.CompanyID, Me._CurrentAccountingCOADataRow.Parent_COA_ID)
        End If
        Me.COACodeTextBox.Text = _CurrentAccountingCOADataRow.COA_Code
        Me._CheckCOACodeHaveChilds = _CurrentAccountingCOADataRow.COA_Code

        Me.COADescTextBox.Text = _CurrentAccountingCOADataRow.COA_Desc
        If _CurrentAccountingCOADataRow.IsInactive_FromNull Then
          InactiveFromCalendarCombo.Value = Nothing
        Else
          InactiveFromCalendarCombo.Value = _CurrentAccountingCOADataRow.Inactive_From
        End If
        If _CurrentAccountingCOADataRow.IsInactive_ToNull Then
          InactiveToCalendarCombo.Value = Nothing
        Else
          InactiveToCalendarCombo.Value = _CurrentAccountingCOADataRow.Inactive_To
        End If
        If Me._CurrentAccountingCOADataRow.IsFinancialAccountType_IDNull Then
          Me.FinancialAccountComboBox.Value = Nothing
        Else
          Me.FinancialAccountComboBox.Value = Me._CurrentAccountingCOADataRow.FinancialAccountType_ID
        End If
        If Me._CurrentAccountingCOADataRow.IsCashFlowAccount_IDNull Then
          Me.CashFlowAccountComboBox.Value = Nothing
        Else
          Me.CashFlowAccountComboBox.Value = Me._CurrentAccountingCOADataRow.CashFlowAccount_ID
        End If
        If Me._CurrentAccountingCOADataRow.IsOpening_DebitAmountNull Then
          Me.OpeningDebitNumericEditor.Value = 0
        Else
          Me.OpeningDebitNumericEditor.Value = Me._CurrentAccountingCOADataRow.Opening_DebitAmount
        End If
        If Me._CurrentAccountingCOADataRow.IsOpening_CreditAmountNull Then
          Me.OpeningCreditNumericEditor.Value = 0
        Else
          Me.OpeningCreditNumericEditor.Value = Me._CurrentAccountingCOADataRow.Opening_CreditAmount
        End If
        Me.ParentCOAIDComboBox.Quick_UltraComboBox1.ReadOnly = True
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      _IsShowingRecord = False
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      Me._CheckValidParentCode = True
      Me.ShowParentComboCOACode()
      Me._CheckValidParentCode = False

      If IsValid() Then
        If _CurrentAccountingCOADataRow Is Nothing Then
          _CurrentAccountingCOADataRow = _AccountingCOATable.NewCOARow
          Me.COAIDTextBox.Text = CStr(_AccountingCOATableAdapterObject.GetNewCoaIdByCoId(Me.CompanyComboBox.CompanyID))
          '  _CurrentAccountingCOADataRow.COA_ID = CInt(Me.COAIDTextBox.Text)
          Me._CurrentAccountingCOADataRow.RecordStatus_ID = 1
        Else
          If Me._CurrentAccountingCOADataRow.RecordStatus_ID <> QuickLibrary.Constants.RecordStatuses.Deleted Then
            Me._CurrentAccountingCOADataRow.RecordStatus_ID = 2
          End If
        End If


        _CurrentAccountingCOADataRow.Co_ID = Me.CompanyComboBox.CompanyID
        Me._CurrentAccountingCOADataRow.COA_ID = CInt(Me.COAIDTextBox.Text)
        _CurrentAccountingCOADataRow.COA_Code = _CoaCode
        _CurrentAccountingCOADataRow.COA_Desc = COADescTextBox.Text.Trim
        'Hidden values
        _CurrentAccountingCOADataRow.Stamp_DateTime = Date.Now
        _CurrentAccountingCOADataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)

                If InactiveFromCalendarCombo.Value Is DBNull.Value OrElse InactiveFromCalendarCombo.Value Is Nothing Then
                    _CurrentAccountingCOADataRow.SetInactive_FromNull()
                Else
                    _CurrentAccountingCOADataRow.Inactive_From = Convert.ToDateTime(InactiveFromCalendarCombo.Value)
                End If
                If InactiveToCalendarCombo.Value Is DBNull.Value OrElse InactiveToCalendarCombo.Value Is Nothing Then
                    _CurrentAccountingCOADataRow.SetInactive_ToNull()
                Else
                    _CurrentAccountingCOADataRow.Inactive_To = Convert.ToDateTime(InactiveToCalendarCombo.Value)
                End If
        Me._CurrentAccountingCOADataRow.FinancialAccountType_ID = CShort(Me.FinancialAccountComboBox.Value)
        Me._CurrentAccountingCOADataRow.CashFlowAccount_ID = CShort(Me.CashFlowAccountComboBox.Value)
        '  MsgBox(Me.COACodeTextBox.Text)
        If CStr(Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value) <> String.Empty Then

          Me._CurrentAccountingCOADataRow.Parent_COA_ID = CInt(Me.ParentCOAIDComboBox.Quick_UltraComboBox1.SelectedRow.Cells("COA_ID").Value)
        Else
          Me._CurrentAccountingCOADataRow.Parent_COA_ID = 0
        End If
        Me._CurrentAccountingCOADataRow.Level_No = CByte(_LevelNo + 1)
        If Me.OpeningDebitNumericEditor.Value IsNot Nothing Then
          Me._CurrentAccountingCOADataRow.Opening_DebitAmount = Convert.ToDecimal(Me.OpeningDebitNumericEditor.Value)
        Else
          Me._CurrentAccountingCOADataRow.Opening_DebitAmount = 0
        End If
        If Me.OpeningCreditNumericEditor.Value IsNot Nothing Then
          Me._CurrentAccountingCOADataRow.Opening_CreditAmount = Convert.ToDecimal(Me.OpeningCreditNumericEditor.Value)
        Else
          Me._CurrentAccountingCOADataRow.Opening_CreditAmount = 0
        End If

        If _CurrentAccountingCOADataRow.RowState = DataRowState.Detached Then
          _AccountingCOATable.Rows.Add(_CurrentAccountingCOADataRow)
        End If
        _AccountingCOATableAdapterObject.Update(_AccountingCOATable)
        Me.CompanyComboBox.ReadOnly = True
        Me.ParentCOAIDComboBox.Quick_UltraComboBox1.ReadOnly = True
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
      Throw QuickExceptionObject
    End Try
  End Function
  Private Function PopulateFinancialAccountTypeComboBox() As Boolean
    Try
      Me.FinancialAccountComboBox.DataSource = Me._FinancialAccountTypeTableAdapter.GetAll
      Me.FinancialAccountComboBox.ValueMember = Me._FinancialAccountTypeDataTable.FinancialAccountType_IDColumn.ColumnName
      Me.FinancialAccountComboBox.DisplayMember = Me._FinancialAccountTypeDataTable.FinancialAccountType_DescColumn.ColumnName


      With FinancialAccountComboBox.DisplayLayout.Bands(0)
        For i As Int32 = 0 To .Columns.Count - 1
          'If .Columns("FinancialAccountType_ID").Index <> .Columns(i).Index And .Columns("FinancialAccountType_Desc").Index <> .Columns(i).Index Then
          If .Columns(Me._FinancialAccountTypeDataTable.FinancialAccountType_DescColumn.ColumnName).Index <> .Columns(i).Index Then
            FinancialAccountComboBox.DisplayLayout.Bands(0).Columns(i).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the populate Financial Account Type ComboBox", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally

    End Try
  End Function
  Private Function PopulateCashFlowAccountComboBox() As Boolean
    Try
      Me.CashFlowAccountComboBox.DataSource = Me._CashFlowAccountTableAdapterObject.GetAll
      Me.CashFlowAccountComboBox.ValueMember = Me._CashFlowAccountDataTable.CashFlowAccount_IDColumn.ColumnName
      Me.CashFlowAccountComboBox.DisplayMember = Me._CashFlowAccountDataTable.CashFlowAccount_DescColumn.ColumnName

      With CashFlowAccountComboBox.DisplayLayout.Bands(0)
        For i As Int32 = 0 To .Columns.Count - 1
          If .Columns(Me._CashFlowAccountDataTable.CashFlowAccount_DescColumn.ColumnName).Index <> .Columns(i).Index Then
            CashFlowAccountComboBox.DisplayLayout.Bands(0).Columns(i).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the populate Cash Flow Account ComboBox(", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally

    End Try
  End Function

  Private Function PopulateParentChartofAccountCombobox() As Boolean
    Try
      Me.ParentCOAIDComboBox.Quick_UltraComboBox1.DataSource = Me._AccountingCOATableAdapterObject.GetAllByCoIDUptoLevelNo(Me.CompanyComboBox.CompanyID, Me._TotalLevels - 1)
      Me.ParentCOAIDComboBox.Quick_UltraComboBox1.ValueMember = Me._AccountingCOATable.COA_CodeColumn.ColumnName
      Me.ParentCOAIDComboBox.Quick_UltraComboBox1.DisplayMember = Me._AccountingCOATable.COA_CodeColumn.ColumnName
      Me.ParentCOAIDComboBox.ColumnNameForLabelDisplay = Me._AccountingCOATable.COA_DescColumn.ColumnName

      With ParentCOAIDComboBox.Quick_UltraComboBox1.DisplayLayout.Bands(0)
        For i As Int32 = 0 To .Columns.Count - 1
          If .Columns("COA_Code").Index <> .Columns(i).Index Then
            ParentCOAIDComboBox.Quick_UltraComboBox1.DisplayLayout.Bands(0).Columns(i).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the populate parent Chart of Account ComboBox(", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function
#End Region

#Region "Event Methods"

  Private Sub AccountingCOA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor

      'AddHandler ParentCOAIDComboBox.Quick_UltraComboBox1.RowSelected, AddressOf PartyControl1_RowSelected
      AddHandler ParentCOAIDComboBox.Quick_UltraComboBox1.RowSelected, AddressOf QuickUltraCombo_RowSelected

      _MaskCOACode = DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_COACode)
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      InactiveFromCalendarCombo.Value = Nothing
      InactiveToCalendarCombo.Value = Nothing
      Me.COACodeTextBox.Text = Nothing
      Me.COADescTextBox.Text = Nothing
      Me.COACodeTextBox.MaxLength = Me._AccountingCOATable.COA_CodeColumn.MaxLength
      Me.COADescTextBox.MaxLength = Me._AccountingCOATable.COA_DescColumn.MaxLength

      Me.FinancialAccountComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
      Me.CashFlowAccountComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList

      Me.COACodeTextBox.Mask = Me._MaskCOACode
      Me.COAIDTextBox.Text = Nothing
      _TotalLevels = QuickLibrary.Common.CountStringOccurences("-", Me._MaskCOACode) + 1
      Me.PopulateFinancialAccountTypeComboBox()
      Me.PopulateCashFlowAccountComboBox()
      Me.PopulateParentChartofAccountCombobox()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in AccountingCOA_Load event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


  Private Sub CompanyComboBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CompanyComboBox.Leave
    Me.PopulateParentChartofAccountCombobox()
  End Sub

  Private Sub COACodeTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles COACodeTextBox.KeyPress
    If Not Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
      e.Handled = True
    End If
  End Sub
  Private Sub COACodeTextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles COACodeTextBox.Leave
    Try
      Me._CheckValidParentCode = True
      Me.ShowParentComboCOACode()
      Me._CheckValidParentCode = False
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Chart of Account TextBox Leave of COAForm.", ex)
      Throw _qex
    End Try
  End Sub

  Private Sub QuickUltraCombo_RowSelected(ByVal sendere As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs)
    Try
      If _CheckValidParentCode = False Then
        Dim _COACodeChar As Int16
        Dim _LevelNo As Int16
        Dim _CheckLevel As Int16 = -1
        Dim _DigitString As String = String.Empty
        Dim _GetLastNo As String
        Dim _ParentCOACode As String
        Dim _ZeroString As String = CStr(0)
        Dim _TempTable As New QuickAccountingDataSet.COADataTable

        If Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Text <> String.Empty Then
          _LevelNo = CShort(Me.ParentCOAIDComboBox.Quick_UltraComboBox1.SelectedRow.Cells("Level_no").Value)
          For i As Int32 = 0 To Me._MaskCOACode.Length - 1
            If Me._MaskCOACode.Substring(i, 1) = "#" Then
              _COACodeChar = CShort(_COACodeChar + 1)
              _DigitString = CStr(9 & _DigitString)
            Else
              If Me._MaskCOACode.Substring(i, 1) = "-" Then
                _CheckLevel = CShort(_CheckLevel + 1)
                If _LevelNo = _CheckLevel Then
                  Exit For
                Else
                  _COACodeChar = 0
                End If
                _DigitString = String.Empty
              End If
            End If
          Next
          _ParentCOACode = CStr(Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value)

          _GetLastNo = CStr(Me._AccountingCOATableAdapterObject.GetMaxCOACodeByStringPattern1(_LevelNo + 1, Me.CompanyComboBox.CompanyID, _ParentCOACode.Length, _ParentCOACode))

          If _GetLastNo = Nothing Then
            For i As Int16 = 0 To CShort(_COACodeChar - 1)
              _GetLastNo = CStr(Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value) & "-" & _ZeroString
              _ZeroString = _ZeroString & 0
            Next
          End If

          If _LevelNo < Me._MaskCOACode.Length Then
            Me.COACodeTextBox.Text = Common.GenerateNextDocumentNo(CStr(0), _GetLastNo, CStr(Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Value) & "-" & _DigitString, False)
          End If
        End If
      End If
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ParentChart of Account Leave of COAForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Jun-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Handles TextChanged event for both OpeningDebitTextBox and OpeningCreditTextBox.
  ''' </summary>
  Private Sub OpeningDebitNumericEditorAndOpeningCreditNumericEditor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningDebitNumericEditor.ValueChanged, OpeningCreditNumericEditor.ValueChanged
    Try

      If Not _IsShowingRecord Then
        Select Case DirectCast(sender, QuickControls.Quick_UltraNumericEditor).Name
          Case OpeningDebitNumericEditor.Name
            OpeningCreditNumericEditor.Value = 0
          Case OpeningCreditNumericEditor.Name
            OpeningDebitNumericEditor.Value = 0
        End Select
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in OpeningDebitNumericEditorAndOpeningCreditNumericEditor_ValueChanged of COAForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

#Region "ToolBar Methods"
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      _AccountingCOATable = Me._AccountingCOATableAdapterObject.GetFirst(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_CurrentAccountingCOADataRow Is Nothing) Then
        Me._AccountingCOATable = Me._AccountingCOATableAdapterObject.GetFirst(Me.CompanyComboBox.CompanyID)
      Else
        _AccountingCOATable = Me._AccountingCOATableAdapterObject.GetNextByCoIdCoaId(Me.CompanyComboBox.CompanyID, Me._CurrentAccountingCOADataRow.COA_Code)
        If _AccountingCOATable.Count = 0 Then
          Me._AccountingCOATable = Me._AccountingCOATableAdapterObject.GetLast(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If (_CurrentAccountingCOADataRow Is Nothing) Then

        Me._AccountingCOATable = Me._AccountingCOATableAdapterObject.GetPreviousByCoIdCoaCode(Me.CompanyComboBox.CompanyID, "")
      Else
        _AccountingCOATable = Me._AccountingCOATableAdapterObject.GetPreviousByCoIdCoaCode(Me.CompanyComboBox.CompanyID, _CurrentAccountingCOADataRow.COA_Code)
      End If
      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      _AccountingCOATable = Me._AccountingCOATableAdapterObject.GetLast(Me.CompanyComboBox.CompanyID)

      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of COAForm.", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _CompanyActive As Int32
      Me.CompanyComboBox.ReadOnly = False
      Me._CheckCOACodeHaveChilds = String.Empty
      Me.ParentCOAIDComboBox.Quick_UltraComboBox1.ReadOnly = False
      _CompanyActive = Me.CompanyComboBox.CompanyID

      Me._CurrentAccountingCOADataRow = Nothing
      Me._AccountingCOATable.Rows.Clear()
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_CompanyActive)
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      Me.COACodeTextBox.Text = String.Empty
      Me.ParentCOAIDComboBox.Quick_UltraComboBox1.Text = String.Empty
      Me._CheckValidChartOfAccount = False
      Me.COACodeTextBox.Focus()
      Me.PopulateParentChartofAccountCombobox()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Dim _TempTable As QuickAccountingDataSet.COADataTable
      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.COACodeTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account code to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.COADescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the chart of account description to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.COAIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Exit Sub
      End If

      ' Validation of COA code have any child
      '_TempTable.Rows.Clear()
      If Me._CheckCOACodeHaveChilds <> String.Empty Then
        Me._CheckCOACodeHaveChilds = Me._CheckCOACodeHaveChilds & "-%"
        _TempTable = Me._AccountingCOATableAdapterObject.GetByCOACodeChilds(Me.CompanyComboBox.CompanyID, Me._CheckCOACodeHaveChilds)
        If _TempTable.Rows.Count > 0 Then
          QuickMessageBox.Show(Me.LoginInfoObject, "This Account code have childs because you cannot change chart of account code", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Exit Sub
        End If
      End If

      If CInt(Me._AccountingCOATableAdapterObject.GetNewCoaIdByCoId(Me.CompanyComboBox.CompanyID)) = CInt(Me.COAIDTextBox.Text) Then

        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      ElseIf MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        'Me._CurrentCompanyDataRow.Delete()
        Me._CurrentAccountingCOADataRow.RecordStatus_ID = 4
        Me.SaveRecord()
        Me._CurrentAccountingCOADataRow = Nothing
        ' _AccountingCOATable.Rows(Me.CurrentRecordIndex).Delete()
        ' _AccountingCOATableAdapterObject.Update(_AccountingCOATable)
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      Else
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


#End Region
 

End Class
