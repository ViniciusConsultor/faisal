Imports System.Windows.Forms
Imports System.Drawing

Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common

Public Class VoucherForm

    Public Sub New()
    ' This call is required by the Windows Form Designer.
        InitializeComponent()
    ' Add any initialization after the InitializeComponent() call.
  End Sub

#Region "Declaration"
  Private _VoucherTableAdapterObject As New VoucherTableAdapter
  Private _VoucherTableDetailAdapterObject As New VoucherDetailTableAdapter

  Private _VoucherTypeTableAdapter As New QuickAccountingDataSetTableAdapters.VoucherTypeTableAdapter
  ' Private _CommonStatusTableAdapter As New RecordStatusTableAdapter


  Private _AccountingCOATableAdapterObject As New QuickAccountingDataSetTableAdapters.COATableAdapter
  Private _PartyTableAdapterObject As New PartyTableAdapter

  Private _RecordStatusDataTable As New RecordStatusDataTable
  Private _PartyDataTable As New PartyDataTable

  Private _AccountingCOATable As New QuickAccountingDataSet.COADataTable

  Private _VoucherTable As New QuickAccountingDataSet.VoucherDataTable
  Private _VoucherDetailTable As New QuickAccountingDataSet.VoucherDetailDataTable

  
  Private _VoucherRow As QuickAccountingDataSet.VoucherRow
  Private _VoucherDetailRow As QuickAccountingDataSet.VoucherDetailRow

  Private _ItemCodeColumnsCount As Int32

  Private _VoucherDetailID As Integer = 0
  Dim cellTypeCombo As New FarPoint.Win.Spread.CellType.ComboBoxCellType
  Dim cellTypeText As New FarPoint.Win.Spread.CellType.TextCellType
  Private _VoucherStatus As Int16 = 1
  ' Private _FlagNewRecord As Boolean = False

  Private Const Setting_ID_CustomDate1_VoucherForm As String = "CustomDate1"
  Private Const Setting_ID_CustomDate2_VoucherForm As String = "CustomDate2"
  Private Const Setting_ID_CustomDate3_VoucherForm As String = "CustomDate3"
  Private Const Setting_ID_CustomDate4_VoucherForm As String = "CustomDate4"
  Private Const Setting_ID_CustomDate5_VoucherForm As String = "CustomDate5"
  Private Const Setting_ID_CustomDecimal1_VoucherForm As String = "CustomDecimal1"
  Private Const Setting_ID_CustomDecimal2_VoucherForm As String = "CustomDecimal2"
  Private Const Setting_ID_CustomDecimal3_VoucherForm As String = "CustomDecimal3"
  Private Const Setting_ID_CustomDecimal4_VoucherForm As String = "CustomDecimal4"
  Private Const Setting_ID_CustomDecimal5_VoucherForm As String = "CustomDecimal5"
  Private Const Setting_ID_CustomText1_VoucherForm As String = "CustomText1"
  Private Const Setting_ID_CustomText2_VoucherForm As String = "CustomText2"
  Private Const Setting_ID_CustomText3_VoucherForm As String = "CustomText3"
  Private Const Setting_ID_CustomText4_VoucherForm As String = "CustomText4"
  Private Const Setting_ID_CustomText5_VoucherForm As String = "CustomText5"
  Private Const Setting_ID_PartyID_VoucherForm As String = "VoucherForm.Party_ID"





  Private Enum VoucherDetailEnum
    DeleteRowButton
    Co_ID
    Voucher_ID
    VoucherDetail_ID
    COA_ID
    COA_Code
    COA_Desc
    Narration
    DebitAmount
    CreditAmount
    Stamp_User_Id
    Stamp_DateTime
    Upload_DateTime
    CustomDate1
    CustomDate2
    CustomDate3
    CustomDate4
    CustomDate5
    CustomDecimal1
    CustomDecimal2
    CustomDecimal3
    CustomDecimal4
    CustomDecimal5
    CustomText1
    CustomText2
    CustomText3
    CustomText4
    CustomText5
    Party_ID
    RecordStatus_id
  End Enum




#End Region

#Region "Events"
  Private Sub AccountingVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '_MaskCOACode = DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_COACode)
      Cursor = Cursors.WaitCursor

      PopulateVoucherTypeComboBox()
      PopulateVoucherDetail(-1, -1)

      Me.VoucherNoTextBox.Text = String.Empty
      Me.VoucherIDTextBox.Text = String.Empty
      Me.RemarksTextBox.Text = String.Empty
      VoucherDateCalendarCombo.Value = Now

      Me.SetGridLayout()
      SetVisibilityofColumn()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in AccountingVoucher_Load event method of VoucherEntryForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Private Sub VoucherDateCalendarCombo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles VoucherDateCalendarCombo.LostFocus
    Try
      Me.VoucherDateCalendarCombo.Format = Constants.FORMAT_DATE_FOR_REPORT
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Lost Focus of VoucherDateCombo method of VoucherForm.", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Private Sub VoucherForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If Me.VoucherDetailQuickSpread.ActiveSheet.ActiveColumn.Label = "COA Code" Then
        Dim _SearchCOAID As New SearchCoa
        Dim rowno As Int32
        rowno = Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex
        If e.KeyCode = Keys.F5 Then
          _SearchCOAID.ShowDialog()
          If _SearchCOAID.SearchResultUnTypedDataTable IsNot Nothing AndAlso _SearchCOAID.SearchResultUnTypedDataTable.Rows.Count > 0 Then

            Me.VoucherDetailQuickSpread.ActiveSheet.SetText(rowno, VoucherDetailEnum.COA_ID, CStr(_SearchCOAID.SearchResultUnTypedDataTable.Rows(0).Item("COA_ID")))
            Me.VoucherDetailQuickSpread.ActiveSheet.SetText(rowno, VoucherDetailEnum.COA_Code, CStr(_SearchCOAID.SearchResultUnTypedDataTable.Rows(0).Item("COA_Code")))
            Me.VoucherDetailQuickSpread.ActiveSheet.SetText(rowno, VoucherDetailEnum.COA_Desc, CStr(_SearchCOAID.SearchResultUnTypedDataTable.Rows(0).Item("COA_Desc")))
          Else
            QuickMessageBox.Show(Me.LoginInfoObject, "No Record Found", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          End If
        End If
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in the KeyUp Voucher Entry Form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
   
    End Try
  End Sub



  Private Sub VoucherDetailQuickSpread_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles VoucherDetailQuickSpread.EditModeOff
    Try
      If Me.VoucherDetailQuickSpread.ActiveSheet Is Nothing OrElse Me.VoucherDetailQuickSpread.ActiveSheet.ActiveCell Is Nothing Then Exit Sub
      If Me.VoucherDetailQuickSpread.ActiveSheet.ActiveColumn.Label = "COA Code" Then
        Dim _GetCoaID As QuickAccountingDataSet.COADataTable
        _GetCoaID = Me._AccountingCOATableAdapterObject.GetCOAIDbyCoaCode(LoginInfoObject.CompanyID, CStr(Me.VoucherDetailQuickSpread.ActiveSheet.GetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.COA_Code)))
        If _GetCoaID.Rows.Count > 0 Then
          Me.VoucherDetailQuickSpread.ActiveSheet.SetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRow.Index, VoucherDetailEnum.COA_ID, CStr(_GetCoaID.Rows(0).Item("COA_ID")))

          Me.VoucherDetailQuickSpread.ActiveSheet.Cells(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.COA_Code).ForeColor = Color.Green
          Me.VoucherDetailQuickSpread.ActiveSheet.SetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRow.Index, VoucherDetailEnum.COA_Desc, CStr(_GetCoaID.Rows(0).Item("COA_Desc").ToString))
        Else
          Me.VoucherDetailQuickSpread.ActiveSheet.SetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.COA_ID, CStr(0))
          Me.VoucherDetailQuickSpread.ActiveSheet.SetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.COA_Desc, String.Empty)

          Me.VoucherDetailQuickSpread.ActiveSheet.Cells(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.COA_Code).ForeColor = Color.Red

        End If
      End If

      If Me.VoucherDetailQuickSpread.ActiveSheet.ActiveColumn.Label = "Debit Amount" Then
        If CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetValue(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.DebitAmount)) > 0 Then
          Me.VoucherDetailQuickSpread.ActiveSheet.SetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.CreditAmount, CStr(0))

        End If
      End If

      If Me.VoucherDetailQuickSpread.ActiveSheet.ActiveColumn.Label = "Credit Amount" Then
        If CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetValue(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.CreditAmount)) > 0 Then
          Me.VoucherDetailQuickSpread.ActiveSheet.SetText(Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.DebitAmount, CStr(0))
        End If
      End If


      Me.ShowTotal()

      If Me.VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex = VoucherDetailQuickSpread.ActiveSheet.RowCount - 1 Then
        If VoucherDetailQuickSpread.ActiveSheet.GetText(VoucherDetailQuickSpread.ActiveSheet.ActiveRowIndex, VoucherDetailEnum.COA_Code) <> "" Then
          AddRow()
        End If
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in EditMode off on Voucher Entry Form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Private Sub VoucherDetailQuickSpread_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles VoucherDetailQuickSpread.Enter
    Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.COA_Code).TabStop = True
  End Sub

  Private Sub PostButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostButton.Click
    Try
      Me._VoucherStatus = 2
      DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry")
      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
        Me.PostButton.Enabled = False
        Me.SetReadOnlyColumns()
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
        Me._VoucherStatus = 1
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in PostButton Click  on Voucher Entry Form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try

  End Sub


#End Region

#Region "Methods"
  Private Sub AddRow()
    Dim _DetailRow As QuickAccountingDataSet.VoucherDetailRow
    _DetailRow = _VoucherDetailTable.NewVoucherDetailRow
    _DetailRow.Co_ID = Me.LoginInfoObject.CompanyID
    _DetailRow.Voucher_ID = 0
    _VoucherDetailID = _VoucherDetailID + 1
    _DetailRow.VoucherDetail_ID = CShort(_VoucherDetailID)
    _DetailRow.Stamp_DateTime = Now
    _DetailRow.Stamp_User_Id = 0
    _DetailRow.Narration = String.Empty
    _DetailRow.DebitAmount = 0
    _DetailRow.CreditAmount = 0
    _DetailRow.COA_ID = 0
    _VoucherDetailTable.Rows.Add(_DetailRow)
  End Sub

  Private Function IsValid() As Boolean
    Try
      Dim _RowNo As Integer
      Dim _debit As Double = 0.0
      Dim _credit As Double = 0.0

      Me.VoucherDetailQuickSpread.Refresh()
      ' MsgBox(Me.VoucherDetailQuickSpread.ActiveSheet.Rows.Count)
      If Me.VoucherDetailQuickSpread.ActiveSheet.Rows.Count = 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Invalid data entered to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If

      If Me._VoucherStatus = 2 Then
        For _RowNo = 0 To Me._VoucherDetailTable.Rows.Count - 2
          If CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetText(_RowNo, VoucherDetailEnum.DebitAmount)) = 0 And CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetText(_RowNo, VoucherDetailEnum.CreditAmount)) = 0 Then
            QuickMessageBox.Show(Me.LoginInfoObject, "Invalid amount entered to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Return False
          ElseIf Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.COA_Code).ForeColor = Color.Red Or Me.VoucherDetailQuickSpread.ActiveSheet.GetText(_RowNo, VoucherDetailEnum.COA_Code) = String.Empty Then
            QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Selected to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
            Return False
          End If
          _debit = CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetText(_RowNo, VoucherDetailEnum.DebitAmount)) + _debit
          _credit = CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetText(_RowNo, VoucherDetailEnum.CreditAmount)) + _credit
        Next
        If _debit <> _credit Then
          QuickMessageBox.Show(Me.LoginInfoObject, "You debit amount should be equal to credit amount to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.PostButton.Enabled = True
          Return False
        End If
      End If

      Me.VoucherDetailQuickSpread.EditMode = False
      If Me._VoucherDetailTable Is Nothing OrElse Me._VoucherDetailTable.Rows.Count < 1 Then
        MessageBox.Show("There must be atleast one chart of account to save", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.VoucherDetailQuickSpread.Focus()
        Return False
      End If

      If Me.VoucherTypeComboBox.SelectedRow Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the Voucher Type to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      ElseIf Me.VoucherDateCalendarCombo Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the Voucher date to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      End If

      For _RowNo = 0 To Me.VoucherDetailQuickSpread.ActiveSheet.Rows.Count - 2
        If Me.VoucherDetailQuickSpread.ActiveSheet.Cells(_RowNo, VoucherDetailEnum.COA_Code).ForeColor = Color.Red Or Me.VoucherDetailQuickSpread.ActiveSheet.GetText(_RowNo, VoucherDetailEnum.COA_Code) = String.Empty Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Chart of Account Selected to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Return False
        End If

        If CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetValue(_RowNo, VoucherDetailEnum.DebitAmount)) = 0 And CDbl(Me.VoucherDetailQuickSpread.ActiveSheet.GetValue(_RowNo, VoucherDetailEnum.CreditAmount)) = 0 Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Amount Entered to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Return False
        End If
      Next
      Return True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function
  Private Sub ShowTotal()
    Dim TotalDebit As Double = 0
    Dim TotalCredit As Double = 0
    Dim Balance As Double = 0

    Try
      For I As Int32 = 0 To Me.VoucherDetailQuickSpread.ActiveSheet.Rows.Count - 1
        TotalDebit += Val(Me.VoucherDetailQuickSpread_SheetView2.GetValue(I, VoucherDetailEnum.DebitAmount))
        TotalCredit += Val(Me.VoucherDetailQuickSpread_SheetView2.GetValue(I, VoucherDetailEnum.CreditAmount))
      Next
      Me.TotalDebitLabel.Text = TotalDebit.ToString
      Me.TotalCreditLabel.Text = TotalCredit.ToString
      Me.BalanceCreditLabel.Text = CStr(CDbl(TotalCreditLabel.Text) - CDbl(TotalDebitLabel.Text))

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show total", ex)
      Throw QuickExceptionObject
    End Try
  End Sub
  Private Sub SetVisibilityofColumn()
    Try

      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.VoucherDetailQuickSpread.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case VoucherDetailEnum.CustomDate1
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDate1_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDate2
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDate2_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDate3
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDate3_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDate4
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDate4_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDate5
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDate5_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDecimal1
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal1_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDecimal2
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal2_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDecimal3
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal3_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDecimal4
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal4_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomDecimal5
            If DatabaseCache.GetSettingValue(Setting_ID_CustomDecimal5_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomText1
            If DatabaseCache.GetSettingValue(Setting_ID_CustomText1_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomText2
            If DatabaseCache.GetSettingValue(Setting_ID_CustomText2_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomText3
            If DatabaseCache.GetSettingValue(Setting_ID_CustomText3_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomText4
            If DatabaseCache.GetSettingValue(Setting_ID_CustomText4_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.CustomText5
            If DatabaseCache.GetSettingValue(Setting_ID_CustomText5_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case VoucherDetailEnum.Party_ID
            If DatabaseCache.GetSettingValue(Setting_ID_PartyID_VoucherForm) = "No" Then
              SheetColumn.Visible = False
            End If
          Case Else
        End Select
      Next



      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.Party_ID).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate1).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate2).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate3).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate4).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate5).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal1).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal2).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal3).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal4).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal5).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText1).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText2).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText3).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText4).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText5).Visible = False

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Visibility Columns on Voucher Entry Form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Private Sub SetReadOnlyColumns()
    Try
      If Me._VoucherStatus = 2 Then
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.COA_Code).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.COA_Desc).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CreditAmount).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate1).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate2).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate3).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate4).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate5).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal1).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal2).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal3).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal4).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal5).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText1).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText2).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText3).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText4).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText5).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.DebitAmount).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.DeleteRowButton).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.Narration).Locked = True
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.Party_ID).Locked = True
      Else
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.COA_Code).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.COA_Desc).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CreditAmount).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate1).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate2).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate3).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate4).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate5).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal1).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal2).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal3).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal4).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal5).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText1).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText2).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText3).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText4).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText5).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.DebitAmount).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.DeleteRowButton).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.Narration).Locked = False
        Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.Party_ID).Locked = False
      End If

      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.Party_ID).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate1).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate2).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate3).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate4).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDate5).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal1).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal2).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal3).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal4).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomDecimal5).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText1).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText2).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText3).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText4).Visible = False
      Me.VoucherDetailQuickSpread.ActiveSheet.Columns(VoucherDetailEnum.CustomText5).Visible = False

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ReadOnly Columns on Voucher Entry Form", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Sub
  Protected Overrides Function ShowRecord() As Boolean
    Try
      If Me._VoucherTable.Rows.Count > 0 Then
        Me.ClearControls(Me)
        Me._VoucherRow = Me._VoucherTable(Me.CurrentRecordIndex)

        If Not _VoucherRow.IsDocumentStatus_IDNull AndAlso CDbl(_VoucherRow.DocumentStatus_ID) = 2 Then
          Me.PostButton.Enabled = False

          Me._VoucherStatus = 2
          Me.SetReadOnlyColumns()
        Else
          Me.VoucherDetailQuickSpread.Enabled = True
          Me.PostButton.Enabled = True
          Me._VoucherStatus = 1
          Me.SetReadOnlyColumns()
        End If
        'Master table filled
        Me.VoucherIDTextBox.Text = CStr(_VoucherRow.Voucher_ID)
        Me.VoucherNoTextBox.Text = _VoucherRow.Voucher_No
        Me.VoucherTypeComboBox.Value = _VoucherRow.VoucherType_ID

        Me.VoucherDateCalendarCombo.Value = _VoucherRow.Voucher_Date
        Me.RemarksTextBox.Text = _VoucherRow.Remarks


        _VoucherRow = _VoucherTable(0)
        PopulateVoucherDetail(_VoucherRow.Co_ID, _VoucherRow.Voucher_ID)
      End If
      Me.ShowTotal()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function

  Private Sub PopulateVoucherDetail(ByVal Co_ID As Short, ByVal Voucher_ID As Integer)
    _VoucherDetailTable = _VoucherTableDetailAdapterObject.GetByCoIDVoucherID(Co_ID, Voucher_ID)
    Me.VoucherDetailQuickSpread.ActiveSheet.DataSource = _VoucherDetailTable
    Me.SetVisibilityofColumn()
    _VoucherDetailTable.AcceptChanges()
    If Me._VoucherStatus <> 2 Then
      AddRow()
    End If
    Me.SetGridLayout()
  End Sub
 

  Private Function PopulateVoucherTypeComboBox() As Boolean
    Try
      Dim _VoucherTypeTable As New QuickAccountingDataSet.VoucherTypeDataTable
      VoucherTypeComboBox.DataSource = _VoucherTypeTableAdapter.GetAllByCoID(LoginInfoObject.CompanyID)
      VoucherTypeComboBox.ValueMember = _VoucherTypeTable.VoucherType_IDColumn.ColumnName
      VoucherTypeComboBox.DisplayMember = _VoucherTypeTable.VoucherType_DescColumn.ColumnName

      With VoucherTypeComboBox.DisplayLayout.Bands(0)
        For i As Int32 = 0 To .Columns.Count - 1
          If .Columns(_VoucherTypeTable.VoucherType_DescColumn.ColumnName).Index <> .Columns(i).Index And .Columns(_VoucherTypeTable.VoucherType_CodeColumn.ColumnName).Index <> .Columns(i).Index Then
            VoucherTypeComboBox.DisplayLayout.Bands(0).Columns(i).Hidden = True
          End If
          .Columns(_VoucherTypeTable.VoucherType_CodeColumn.ColumnName).Width = Constants.ITEM_DESC_CELL_WIDTH

        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the populate Voucher Type", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally

    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean

    Try
      Dim temp As String = "DocumentNoFormat.VoucherEntry"
      Dim _LastVoucherNo As Object
      Dim _LikeOperatorPattern As String


      Me.VoucherDetailQuickSpread.Update()
      Me.VoucherDetailQuickSpread.EditMode = False
      Me.VoucherDetailQuickSpread.StopCellEditing()
      Me.VoucherDetailQuickSpread_SheetView2.ActiveRowIndex = Me.VoucherDetailQuickSpread_SheetView2.RowCount - 1
      If IsValid() = True Then

        _VoucherDetailTable.Rows.RemoveAt(_VoucherDetailTable.Rows.Count - 1)
        'Update Master Accounting Voucher
        If _VoucherRow Is Nothing Then
          _VoucherRow = _VoucherTable.NewVoucherRow
          _VoucherRow.Voucher_ID = CInt(_VoucherTableAdapterObject.GetNewVoucherIDByCoID(LoginInfoObject.CompanyID))


          '_LikeOperatorPattern = Common.GenerateNextDocumentNo(String.Empty, String.Empty, DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry"), True)
          _LikeOperatorPattern = Common.GenerateNextDocumentNo(String.Empty, String.Empty, "999999", True)
          _LastVoucherNo = _VoucherTableAdapterObject.GetMaxVoucherNoByCoID(LoginInfoObject.CompanyID, _LikeOperatorPattern)
          If _LastVoucherNo Is Nothing Then
            'Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, "", DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry"), False)
            Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, "", "999999", False)
          Else
            'Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, _LastVoucherNo.ToString, DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry"), False)
            Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, _LastVoucherNo.ToString, "999999", False)
          End If

          Me._VoucherRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        Else
          If Me._VoucherRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            Me._VoucherRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If
        End If
        Me.VoucherIDTextBox.Text = CStr(_VoucherRow.Voucher_ID)

        _VoucherRow.Co_ID = LoginInfoObject.CompanyID
        _VoucherRow.Remarks = Me.RemarksTextBox.Text

        _VoucherRow.Voucher_No = Me.VoucherNoTextBox.Text

        _VoucherRow.Voucher_Date = Now
        _VoucherRow.VoucherType_ID = CInt(VoucherTypeComboBox.Value)
        'Hidden values
        _VoucherRow.Stamp_DateTime = Date.Now
        _VoucherRow.Stamp_UserID = LoginInfoObject.UserID
        _VoucherRow.Upload_DateTime = Date.Now
        _VoucherRow.DocumentStatus_ID = CByte(CShort(Me._VoucherStatus))

        If IsDBNull(VoucherDateCalendarCombo.Value) = False Then
          _VoucherRow.Voucher_Date = Convert.ToDateTime(VoucherDateCalendarCombo.Value)
        End If
        _VoucherRow.Remarks = RemarksTextBox.Text

        If _VoucherRow.RowState = DataRowState.Detached Then
          _VoucherTable.Rows.Add(_VoucherRow)
        End If


        _VoucherTableAdapterObject.Update(_VoucherTable)

        'Update Detaill Voucher Detail
        _VoucherTableDetailAdapterObject.Update(_VoucherDetailTable.Select("", "", DataViewRowState.Deleted))

        For I As Int32 = 0 To Me._VoucherDetailTable.Rows.Count - 1
          _VoucherDetailRow = Me._VoucherDetailTable(I)

          If _VoucherDetailRow.RowState = DataRowState.Added Then
            _VoucherDetailRow.VoucherDetail_ID = _VoucherTableDetailAdapterObject.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherRow.Voucher_ID).Value
            _VoucherDetailRow.Voucher_ID = _VoucherRow.Voucher_ID
            _VoucherDetailRow.RecordStatus_ID = Constants.RecordStatuses.Inserted


          ElseIf _VoucherDetailRow.RowState = DataRowState.Modified Then

            'Assign first row by filtering. There should not be more than one rows theoratically here if data is stored correctly.
            _VoucherDetailRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            _VoucherDetailRow = _VoucherDetailTable(I)
          ElseIf _VoucherDetailRow.RowState = DataRowState.Unchanged Then
            _VoucherDetailRow = _VoucherDetailTable(I)
          ElseIf _VoucherDetailRow.RowState = DataRowState.Deleted Then
            _VoucherDetailRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
            _VoucherDetailRow = _VoucherDetailTable(I)
          End If
          'Common Fields
          _VoucherDetailRow.Co_ID = LoginInfoObject.CompanyID
          _VoucherDetailRow.Stamp_DateTime = Now
          _VoucherDetailRow.Stamp_User_Id = LoginInfoObject.UserID
          _VoucherDetailRow.Upload_DateTime = Now

          If _VoucherDetailRow.RowState <> DataRowState.Unchanged Then

            If _VoucherDetailRow.RowState = DataRowState.Detached Then
              _VoucherDetailRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
              _VoucherDetailTable.Rows.Add(_VoucherDetailRow)
            End If

            'This statement should be inside loop so that we can fetch new detail id properly.
            If _VoucherTableDetailAdapterObject.Update(_VoucherDetailTable(I)) > 0 Then
              'Record is updated
              'MessageBox.Show("Updated")
            Else
              'Record is not updated.
              'QuickMessageBox.Show(Me.LoginInfoObject, "Record was not updated successfully", MessageBoxIcon.Exclamation)
            End If
          End If
        Next

        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord method of VoucherForm.", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Private Sub SetGridLayout()
    Try
      Dim _MaskCOACode As String = Nothing
      _MaskCOACode = DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_COACode)
      Dim maskcell As New FarPoint.Win.Spread.CellType.MaskCellType()
      maskcell.Mask = _MaskCOACode
      'maskcell.MaskChar = "X"
      Me.VoucherDetailQuickSpread.ActiveSheet.Cells(5, 0).CellType = maskcell

      Me.VoucherDetailQuickSpread.ShowDeleteRowButton(Me.VoucherDetailQuickSpread.ActiveSheet) = True
      Dim _visible As Boolean = False
      Dim _widthSmall As Integer = 50
      Dim _widthLarge As Integer = 80
      Dim _widthXLarge As Integer = 130

      VoucherNoTextBox.ReadOnly = True

      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.VoucherDetailQuickSpread.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case VoucherDetailEnum.DeleteRowButton
            SheetColumn.Width = QTY_CELL_WIDTH

          Case VoucherDetailEnum.Narration
            SheetColumn.Label = "Narration"
            SheetColumn.Width = QTY_CELL_WIDTH + (_widthXLarge * 2)


          Case VoucherDetailEnum.DebitAmount
            SheetColumn.Label = "Debit Amount"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall

          Case VoucherDetailEnum.CreditAmount
            SheetColumn.Label = "Credit Amount"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall

          Case VoucherDetailEnum.CustomDate1
            SheetColumn.Label = "CustomDate1"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDate2
            SheetColumn.Label = "CustomDate2"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDate3
            SheetColumn.Label = "CustomDate3"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDate4 '+ _ItemCodeColumnsCount
            SheetColumn.Label = "CustomDate4"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDate5
            SheetColumn.Label = "CustomDate5"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDecimal1
            SheetColumn.Label = "CustomDecimal1"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDecimal2
            SheetColumn.Label = "CustomDecimal2"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDecimal3
            SheetColumn.Label = "CustomDecimal3"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case VoucherDetailEnum.CustomDecimal4
            SheetColumn.Label = "CustomDecimal4"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge
          Case VoucherDetailEnum.CustomDecimal5
            SheetColumn.Label = "CustomDecimal5"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge
          Case VoucherDetailEnum.CustomText1
            SheetColumn.Label = "CustomText1"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.CellType = cellTypeText
          Case VoucherDetailEnum.CustomText2
            SheetColumn.Label = "CustomText2"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.CellType = cellTypeText
          Case VoucherDetailEnum.CustomText3
            SheetColumn.Label = "CustomText3"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.CellType = cellTypeText
          Case VoucherDetailEnum.CustomText4
            SheetColumn.Label = "CustomText4"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.CellType = cellTypeText
          Case VoucherDetailEnum.CustomText5
            SheetColumn.Label = "CustomText5"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.CellType = cellTypeText
          Case VoucherDetailEnum.Party_ID
            SheetColumn.Label = "Party ID"
            SheetColumn.Width = QTY_CELL_WIDTH
            PopulateComboinGrid(SheetColumn)
          Case VoucherDetailEnum.Stamp_DateTime
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH
          Case VoucherDetailEnum.Stamp_User_Id
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH
          Case VoucherDetailEnum.RecordStatus_id
            SheetColumn.Visible = _visible
          Case VoucherDetailEnum.Upload_DateTime
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH
          Case VoucherDetailEnum.Voucher_ID
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH
          Case VoucherDetailEnum.VoucherDetail_ID
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH
            SheetColumn.CellType = QtyCellType
          Case VoucherDetailEnum.Co_ID
            SheetColumn.Visible = _visible
            SheetColumn.CellType = QtyCellType
          Case VoucherDetailEnum.COA_ID
            SheetColumn.Label = "COA ID"
            SheetColumn.Visible = False
          Case VoucherDetailEnum.COA_Code
            '_MaskCOACode = DatabaseCache.GetSettingValue(QuickLibrary.Constants.SETTING_ID_Mask_COACode)
            SheetColumn.Label = "COA Code"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge
            'Me.VoucherDetailQuickSpread.ActiveSheet.Cells(0, 0).CellType = maskcell
            'SheetColumn.CellType = Me.VoucherDetailQuickSpread.ActiveSheet.Cells(0, 0).CellType = maskcell
            'SheetColumn.CellType = Me.VoucherDetailQuickSpread.ActiveSheet.Cells(0, 0).CellType = maskcell
          Case VoucherDetailEnum.COA_Desc
            SheetColumn.Label = "COA Desc"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthXLarge
            SheetColumn.TabStop = False
            SheetColumn.Locked = True
          Case Else
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SetGridLayout method of VoucherForm.", ex)
      Throw QuickExceptionObject
    End Try
  End Sub
  Private Sub PopulateComboinGrid(ByVal customColumn As FarPoint.Win.Spread.Column)
    Dim comboType As New FarPoint.Win.Spread.CellType.ComboBoxCellType
    customColumn.CellType = comboType

    Dim _strItem As String = String.Empty
    Dim _strItemData As String = String.Empty

    Select Case customColumn.Label
      Case "Party ID"
        _PartyDataTable = _PartyTableAdapterObject.GetByCoID(LoginInfoObject.CompanyID)
        '_strItem = New String(10)
        For Each row As PartyRow In _PartyDataTable

          _strItem = _strItem + CStr(row.Party_ID) + ","
          _strItemData = _strItemData + CStr(row.Party_Desc) + ","
        Next
        'Case "COA ID"
        '  _AccountingCOATable = _AccountingCOATableAdapterObject.GetByCoID(LoginInfoObject.CompanyID)
        '  For Each row As COARow In _AccountingCOATable
        '    _strItem = _strItem + CStr(row.COA_ID) + ","
        '    _strItemData = _strItemData + CStr(row.COA_Desc) + ","
        '  Next
        '  Case "COA Code"
        '    _AccountingCOATable = _AccountingCOATableAdapterObject.GetByCoID(LoginInfoObject.CompanyID)
        '    For Each row As QuickAccountingDataSet.COARow In _AccountingCOATable

        '      _strItem = _strItem + CStr(row.COA_Code) + ","
        '      _strItemData = _strItemData + CStr(row.COA_Desc) + ","
        '    Next
    End Select

    If _strItem.Length > 0 Then
      _strItem = _strItem.Remove(_strItem.LastIndexOf(","))
      _strItemData = _strItemData.Remove(_strItemData.LastIndexOf(","))
      comboType.Items = _strItem.Split(CChar(","))
      comboType.ItemData = _strItemData.Split(CChar(","))
    End If
    customColumn.CellType = comboType
    ' comboType.Editable = True
  End Sub


#End Region

#Region "Properties"

#End Region

#Region "Toolbar methods"

    Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
      _VoucherTable = Me._VoucherTableAdapterObject.GetFirstByCoID(LoginInfoObject.CompanyID)
            MyBase.MoveFirstButtonClick(sender, e)
        Catch ex As Exception
            Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick method of VoucherForm.", ex)
            QuickExceptionObject.Show(LoginInfoObject)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_VoucherRow Is Nothing) Then
        Me._VoucherTable = Me._VoucherTableAdapterObject.GetFirstByCoID(LoginInfoObject.CompanyID)
      Else
        _VoucherTable = _VoucherTableAdapterObject.GetNextByCoIDVoucherID(Me.LoginInfoObject.CompanyID, CInt(Me.VoucherIDTextBox.Text))
        If _VoucherTable.Count = 0 Then
          _VoucherTable = Me._VoucherTableAdapterObject.GetLastByCoID(Me.LoginInfoObject.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
    End Sub

    Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_VoucherRow Is Nothing) Then
        _VoucherTable = Me._VoucherTableAdapterObject.GetPreviousByCoIDVoucherID(LoginInfoObject.CompanyID, 0)
      Else
        _VoucherTable = Me._VoucherTableAdapterObject.GetPreviousByCoIDVoucherID(LoginInfoObject.CompanyID, CInt(Me.VoucherIDTextBox.Text))
      End If


      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
    End Sub

    Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
      _VoucherTable = Me._VoucherTableAdapterObject.GetLastByCoID(LoginInfoObject.CompanyID)
            MyBase.MoveLastButtonClick(sender, e)
        Catch ex As Exception
            Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick method of VoucherForm.", ex)
            QuickExceptionObject.Show(LoginInfoObject)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If Me._VoucherStatus = 2 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You have no permission to save this record because voucher already posted", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      End If

      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
        Me.AddRow()
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
    End Sub

    Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me._VoucherRow = Nothing
      Me._VoucherTable.Rows.Clear()
      Me._VoucherDetailTable.Rows.Clear()

      Me.VoucherTypeComboBox.Value = Nothing
      Me.VoucherDateCalendarCombo.Value = Nothing

      Me.PostButton.Enabled = False
      MyBase.CancelButtonClick(sender, e)
      VoucherDateCalendarCombo.Value = Now
      Me.AddRow()
      Me.SetGridLayout()

      Me._VoucherStatus = 1
      Me.SetReadOnlyColumns()
      Me.VoucherTypeComboBox.Focus()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
    End Sub

    Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If Me._VoucherStatus = 2 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You have no permission to delete this record because voucher already posted", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      End If
      'If Me._VoucherTable.Rows.Count = 0 Then
      '  Return
      'End If
      If Me.VoucherIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Invalid data selected to delete the record", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return
      End If
      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._VoucherRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        ' _VoucherTable.Rows(Me.CurrentRecordIndex).Delete()
        'Me._VoucherTableAdapterObject.Update(_VoucherTable)
        SaveRecord()
        Me._VoucherRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      Else
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


  'Author: Faisal Salem
  'Date Created(DD-MMM-YY): 26-Jun-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Shows voucher print preview.
  ''' </summary>
  Protected Overrides Sub PrintPreviewButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ReportViewerForm As New QuickReports.CrystalReportViewerForm
      Dim _ParameterValues As New System.Collections.Specialized.NameValueCollection
      Dim _SelectionFormula As String = String.Empty

      If _VoucherRow IsNot Nothing Then
        Me.Cursor = Cursors.WaitCursor

        _SelectionFormula = "{Accounting_Voucher.Co_ID}=" & Me.LoginInfoObject.CompanyID.ToString & " AND {Accounting_Voucher.Voucher_ID}=" & _VoucherRow.Voucher_ID.ToString
        If _VoucherRow.IsDocumentStatus_IDNull OrElse _VoucherRow.DocumentStatus_ID = Constants.DocumentStatuses.General_Unposted Then
          _ReportViewerForm.FormulaValues.Add("PostedText", "Unposted")
        Else
          _ReportViewerForm.FormulaValues.Add("PostedText", "Posted")
        End If

        '"{@Posted}"
        _ReportViewerForm.RecordSelectionFormula = _SelectionFormula
        _ReportViewerForm.Report = New QuickReports.VoucherReport
        _ReportViewerForm.WindowState = FormWindowState.Maximized

        _ReportViewerForm.Show()

        MyBase.PrintPreviewButtonClick(sender, e)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to show in the report")
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in PrintPreviewButtonClick of VoucherEntryForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region







 
End Class