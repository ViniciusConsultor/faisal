Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
'Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary

Public Class CashFlowAccount

#Region "Declarations"

  Private _CashFlowAccountTableAdapterObject As New CashFlowAccountTableAdapter
  Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _CashFlowAccountDataTable As New CashFlowAccountDataTable
  Private _CurrentCashFlowAccountDataRow As CashFlowAccountRow

#End Region

#Region "Properties"

#End Region
 

#Region "Methods"

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub
 

  Private Function IsValid() As Boolean
    Try
      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      ElseIf Me.CashFlowAccountDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the CashAccountFlow description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.CashFlowAccountDescTextBox.Focus()
        Return False
      End If
      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      Me.CompanyComboBox.ReadOnly = True
      If Me._CashFlowAccountDataTable.Rows.Count > 0 Then
        Me._CurrentCashFlowAccountDataRow = Me._CashFlowAccountDataTable(Me.CurrentRecordIndex)
        Me.ClearControls(Me)

        Me.CompanyComboBox.CompanyID = Me._CurrentCashFlowAccountDataRow.Co_ID
        Me.CashFlowAccountIDTextBox.Text = Me._CurrentCashFlowAccountDataRow.CashFlowAccount_ID.ToString
        Me.CashFlowAccountDescTextBox.Text = Me._CurrentCashFlowAccountDataRow.CashFlowAccount_Desc
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of COAForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function

  Private Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If Me._CurrentCashFlowAccountDataRow Is Nothing Then
          _CurrentCashFlowAccountDataRow = Me._CashFlowAccountDataTable.NewCashFlowAccountRow
          Me.CashFlowAccountIDTextBox.Text = CStr(Me._CashFlowAccountTableAdapterObject.GetNewCashFlowAccountIDByCoID(Me.CompanyComboBox.CompanyID))

          Me._CurrentCashFlowAccountDataRow.RecordStatus_ID = 1
        Else
          If Me._CurrentCashFlowAccountDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            Me._CurrentCashFlowAccountDataRow.RecordStatus_ID = 2
          End If
        End If
        Me._CurrentCashFlowAccountDataRow.Co_ID = Me.CompanyComboBox.CompanyID
        Me._CurrentCashFlowAccountDataRow.CashFlowAccount_ID = CShort(Me.CashFlowAccountIDTextBox.Text)
        Me._CurrentCashFlowAccountDataRow.CashFlowAccount_Desc = Me.CashFlowAccountDescTextBox.Text.Trim
        _CurrentCashFlowAccountDataRow.Stamp_DateTime = Date.Now
        _CurrentCashFlowAccountDataRow.Upload_DateTime = Date.Now
        _CurrentCashFlowAccountDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)


        If _CurrentCashFlowAccountDataRow.RowState = DataRowState.Detached Then
          Me._CashFlowAccountDataTable.Rows.Add(_CurrentCashFlowAccountDataRow)
        End If

        Me._CashFlowAccountTableAdapterObject.Update(Me._CashFlowAccountDataTable)
        Me.CompanyComboBox.ReadOnly = True
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
      Throw QuickExceptionObject
    End Try
  End Function
 



#End Region

#Region "Event Methods"
  Private Sub CashFlowAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor

      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID

      Me.CashFlowAccountIDTextBox.Text = Nothing
      Me.CashFlowAccountDescTextBox.Text = Nothing
      Me.CashFlowAccountDescTextBox.MaxLength = Me._CashFlowAccountDataTable.CashFlowAccount_DescColumn.MaxLength

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CashFlowAccount Load event of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Private Sub CashFlowAccountDescTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CashFlowAccountDescTextBox.KeyPress
    If Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
      e.Handled = True
    End If
  End Sub


#End Region

#Region "ToolBar Methods"
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_CurrentCashFlowAccountDataRow Is Nothing) Then
        Me._CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      Else
        _CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetNextByCoIDCashFlowAccountID(Me.CompanyComboBox.CompanyID, Me._CurrentCashFlowAccountDataRow.CashFlowAccount_ID)
        If _CashFlowAccountDataTable.Count = 0 Then
          Me._CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If (_CurrentCashFlowAccountDataRow Is Nothing) Then
        Me._CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetPreviousByCoIDCashFlowAccountID(Me.CompanyComboBox.CompanyID, 0)
      Else
        _CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetPreviousByCoIDCashFlowAccountID(Me.CompanyComboBox.CompanyID, CInt(Me.CashFlowAccountIDTextBox.Text))
      End If
      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._CashFlowAccountDataTable = Me._CashFlowAccountTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of CashFlowAccount Form.", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ActiveCompany As Integer
      _ActiveCompany = Me.CompanyComboBox.CompanyID
      Me.CompanyComboBox.ReadOnly = False
      Me.CashFlowAccountDescTextBox.Text = Nothing
      Me._CurrentCashFlowAccountDataRow = Nothing
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_ActiveCompany)
      Me.CashFlowAccountIDTextBox.Text = Nothing

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      '      Dim _TempTable As QuickAccountingDataSet.COADataTable'
      If Me.CashFlowAccountIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select Invalid Record to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      End If

      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._CurrentCashFlowAccountDataRow.RecordStatus_ID = 4
        _CurrentCashFlowAccountDataRow.Stamp_DateTime = Date.Now
        _CurrentCashFlowAccountDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
        Me._CashFlowAccountTableAdapterObject.Update(Me._CashFlowAccountDataTable)
        Me.CompanyComboBox.Text = String.Empty
        Me._CurrentCashFlowAccountDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
        Me.CompanyComboBox.Focus()
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of CashFlowAccount Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


#End Region

 

  
  
End Class