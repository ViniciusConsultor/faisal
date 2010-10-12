Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
'Imports QuickDAL.QuickCommonDataSet
'Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary

Public Class FinancialAccountType

#Region "Declarations"
  Private _FinancialAccountTypeTableAdapterObject As New AccountsDataSetTableAdapters.Accounting_FinancialAccountTypeTableAdapter
  Private _CompanyTableAdapterObject As New QuickDAL.QuickCommonDataSetTableAdapters.CompanyTableAdapter
  Private _FinancialAccountTypeDataTable As New AccountsDataSet.Accounting_FinancialAccountTypeDataTable
  Private _CurrentFinancialAccountTypeDataRow As AccountsDataSet.Accounting_FinancialAccountTypeRow
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
      ElseIf Me.FinancialAccountTypeDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the FinancialAccountType description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.FinancialAccountTypeDescTextBox.Focus()
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
      If Me._FinancialAccountTypeDataTable.Rows.Count > 0 Then
        Me._CurrentFinancialAccountTypeDataRow = Me._FinancialAccountTypeDataTable(Me.CurrentRecordIndex)
        Me.ClearControls(Me)

        Me.CompanyComboBox.CompanyID = Me._CurrentFinancialAccountTypeDataRow.Co_ID
        Me.FinancialAccountTypeIDTextBox.Text = Me._CurrentFinancialAccountTypeDataRow.FinancialAccountType_ID.ToString
        Me.FinancialAccountTypeDescTextBox.Text = Me._CurrentFinancialAccountTypeDataRow.FinancialAccountType_Desc
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function

  Private Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If Me._CurrentFinancialAccountTypeDataRow Is Nothing Then
          _CurrentFinancialAccountTypeDataRow = Me._FinancialAccountTypeDataTable.NewAccounting_FinancialAccountTypeRow
          Me.FinancialAccountTypeIDTextBox.Text = CStr(Me._FinancialAccountTypeTableAdapterObject.GetNewFinancialAccountTypeIDByCoID(Me.CompanyComboBox.CompanyID))
          Me._CurrentFinancialAccountTypeDataRow.RecordStatus_ID = 1
        Else
          If Me._CurrentFinancialAccountTypeDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            Me._CurrentFinancialAccountTypeDataRow.RecordStatus_ID = 2
          End If
        End If
        Me._CurrentFinancialAccountTypeDataRow.Co_ID = Me.CompanyComboBox.CompanyID
        Me._CurrentFinancialAccountTypeDataRow.FinancialAccountType_ID = CShort(Me.FinancialAccountTypeIDTextBox.Text)
        Me._CurrentFinancialAccountTypeDataRow.FinancialAccountType_Desc = Me.FinancialAccountTypeDescTextBox.Text.Trim
        _CurrentFinancialAccountTypeDataRow.Stamp_DateTime = Date.Now
        _CurrentFinancialAccountTypeDataRow.Upload_DateTime = Date.Now
        _CurrentFinancialAccountTypeDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)

        If _CurrentFinancialAccountTypeDataRow.RowState = DataRowState.Detached Then
          Me._FinancialAccountTypeDataTable.Rows.Add(_CurrentFinancialAccountTypeDataRow)
        End If

        Me._FinancialAccountTypeTableAdapterObject.Update(Me._FinancialAccountTypeDataTable)
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
  Private Sub FinancialAccountType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID

      Me.FinancialAccountTypeIDTextBox.Text = Nothing
      Me.FinancialAccountTypeDescTextBox.Text = Nothing
      Me.FinancialAccountTypeDescTextBox.MaxLength = Me._FinancialAccountTypeDataTable.FinancialAccountType_DescColumn.MaxLength

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in FinancialAccountType Load event of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub
  Private Sub FinancialAccountTypeDescTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FinancialAccountTypeDescTextBox.KeyPress
    If Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
      e.Handled = True
    End If
  End Sub

#End Region

#Region "ToolBar Methods"
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_CurrentFinancialAccountTypeDataRow Is Nothing) Then
        Me._FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      Else
        _FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetNextByCoIDFinancialAccountTypeID(Me.CompanyComboBox.CompanyID, Me._CurrentFinancialAccountTypeDataRow.FinancialAccountType_ID)
        If _FinancialAccountTypeDataTable.Count = 0 Then
          Me._FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If (_CurrentFinancialAccountTypeDataRow Is Nothing) Then
        Me._FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetPreviousByCoIDFinancialAccountTypeID(Me.CompanyComboBox.CompanyID, 0)
      Else
        _FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetPreviousByCoIDFinancialAccountTypeID(Me.CompanyComboBox.CompanyID, CInt(Me.FinancialAccountTypeIDTextBox.Text))
      End If
      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._FinancialAccountTypeDataTable = Me._FinancialAccountTypeTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of FinancialAccountType Form.", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of FinancialAccountType Form.", ex)
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
      Me.FinancialAccountTypeDescTextBox.Text = Nothing
      Me._CurrentFinancialAccountTypeDataRow = Nothing
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_ActiveCompany)
      Me.FinancialAccountTypeIDTextBox.Text = Nothing

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If Me.FinancialAccountTypeIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select Invalid Record to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      End If

      '
      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

        Me._CurrentFinancialAccountTypeDataRow.RecordStatus_ID = 4
        _CurrentFinancialAccountTypeDataRow.Stamp_DateTime = Date.Now
        _CurrentFinancialAccountTypeDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
        Me._FinancialAccountTypeTableAdapterObject.Update(Me._FinancialAccountTypeDataTable)
        Me.CompanyComboBox.Text = String.Empty
        Me._CurrentFinancialAccountTypeDataRow = Nothing
        Me.CompanyComboBox.Focus()
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")

      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of FinancialAccountType Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


#End Region

  

  
 
End Class