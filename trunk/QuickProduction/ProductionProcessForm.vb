Imports System.Windows.Forms
Imports QuickDal.QuickProductionDataSetTableAdapters
Imports QuickDAL.QuickProductionDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickAccountingDataSet
Imports QuickDal
Imports QuickDALLibrary
Imports QuickLibrary

Public Class DefineProcessForm

#Region "Declartions"
  Private _DefineProcessTableAdapter As New ProductionProcessTableAdapter
  Private _DefineProcessDataTable As New ProductionProcessDataTable
  Private _CurrentDefineProcessDataRow As ProductionProcessRow

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
      Dim _IsExistProcessCode As ProductionProcessDataTable
      _IsExistProcessCode = Me._DefineProcessTableAdapter.IsExistProcessCode(Me.LoginInfoObject.CompanyID, Me.ProcessCodeTextBox.Text)

      If Me.ProcessDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the Process description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ProcessDescTextBox.Focus()
        Return False
      ElseIf Me.ProcessCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the Process Code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ProcessCodeTextBox.Focus()
      End If

      If _IsExistProcessCode.Rows.Count = 1 Then
        If _IsExistProcessCode.Rows(0).Item(_IsExistProcessCode.Process_IDColumn).ToString = CStr(Me.ProcessIDTextBox.Text) And _IsExistProcessCode.Rows(0).Item(_IsExistProcessCode.Process_CodeColumn).ToString = Me.ProcessCodeTextBox.Text Then
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Process code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.ProcessCodeTextBox.Focus()
          Return False
        End If
      End If

      _IsExistProcessCode.Rows.Clear()
      _IsExistProcessCode = Me._DefineProcessTableAdapter.IsExistProcessDesc(Me.LoginInfoObject.CompanyID, Me.ProcessDescTextBox.Text)
      If _IsExistProcessCode.Rows.Count = 1 Then
        If _IsExistProcessCode.Rows(0).Item(_IsExistProcessCode.Process_IDColumn).ToString = CStr(Me.ProcessIDTextBox.Text) And _IsExistProcessCode.Rows(0).Item(_IsExistProcessCode.Process_DescColumn).ToString = Me.ProcessDescTextBox.Text Then
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Process Description Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.ProcessDescTextBox.Focus()
          Return False
        End If
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
      If Me._DefineProcessDataTable.Rows.Count > 0 Then

        Me._CurrentDefineProcessDataRow = Me._DefineProcessDataTable(Me.CurrentRecordIndex)
        Me.ClearControls(Me)

        Me.ProcessIDTextBox.Text = Me._CurrentDefineProcessDataRow.Process_ID.ToString
        Me.ProcessCodeTextBox.Text = Me._CurrentDefineProcessDataRow.Process_Code
        Me.ProcessDescTextBox.Text = Me._CurrentDefineProcessDataRow.Process_Desc
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If Me._CurrentDefineProcessDataRow Is Nothing Then
          Me._CurrentDefineProcessDataRow = Me._DefineProcessDataTable.NewProductionProcessRow
          Me.ProcessIDTextBox.Text = Me._DefineProcessTableAdapter.GetNewProcessIDByCoID(Me.LoginInfoObject.CompanyID).Value.ToString
          Me._CurrentDefineProcessDataRow.RecordStatus_ID = 1
        Else
          If Me._CurrentDefineProcessDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            Me._CurrentDefineProcessDataRow.RecordStatus_ID = 2
          End If
        End If
        Me._CurrentDefineProcessDataRow.Co_ID = Me.LoginInfoObject.CompanyID
        Me._CurrentDefineProcessDataRow.Process_ID = Int16.Parse(Me.ProcessIDTextBox.Text)
        Me._CurrentDefineProcessDataRow.Process_Code = Me.ProcessCodeTextBox.Text
        Me._CurrentDefineProcessDataRow.Process_Desc = Me.ProcessDescTextBox.Text
        Me._CurrentDefineProcessDataRow.Stamp_UserID = Convert.ToInt16(Me.LoginInfoObject.UserID)
        Me._CurrentDefineProcessDataRow.Stamp_DateTime = Common.SystemDateTime
        Me._CurrentDefineProcessDataRow.Upload_DateTime = Common.SystemDateTime

        If Me._CurrentDefineProcessDataRow.RowState = DataRowState.Detached Then
          Me._DefineProcessDataTable.Rows.Add(Me._CurrentDefineProcessDataRow)
        End If
        Me._DefineProcessTableAdapter.Update(Me._DefineProcessDataTable)
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to Save Record", ex)
      Throw QuickExceptionObject
    End Try
  End Function

#End Region

#Region "Event Methods"
  Private Sub ProductionProcessForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      Me.ProcessIDTextBox.Text = Nothing
      Me.ProcessCodeTextBox.Text = Nothing
      Me.ProcessDescTextBox.Text = Nothing
      Me.ProcessDescTextBox.MaxLength = Me._DefineProcessDataTable.Process_DescColumn.MaxLength
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DefineProcess Load event of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub
  'Private Sub ProcessDescTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ProcessDescTextBox.KeyPress
  '  If Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
  '    e.Handled = True
  '  End If
  'End Sub

#End Region

#Region "ToolBar Methods"
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetFirstByCoID(Me.LoginInfoObject.CompanyID)

      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (Me._CurrentDefineProcessDataRow Is Nothing) Then
        Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetFirstByCoID(Me.LoginInfoObject.CompanyID)
      Else
        Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetNextByCoIDProcessID(Me.LoginInfoObject.CompanyID, Me._CurrentDefineProcessDataRow.Process_ID)
        If Me._DefineProcessDataTable.Rows.Count = 0 Then
          Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetLastByCoID(Me.LoginInfoObject.CompanyID)
        End If
      End If

      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (Me._CurrentDefineProcessDataRow Is Nothing) Then
        Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetPrevByCoIDProcessID(Me.LoginInfoObject.CompanyID, 0)
      Else
        Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetPrevByCoIDProcessID(Me.LoginInfoObject.CompanyID, CInt(Me.ProcessIDTextBox.Text))
      End If

      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetLastByCoID(Me.LoginInfoObject.CompanyID)

      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of DefineProcess Form.", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me.ProcessIDTextBox.Text = Nothing
      Me.ProcessCodeTextBox.Text = Nothing
      Me.ProcessDescTextBox.Text = Nothing
      Me._CurrentDefineProcessDataRow = Nothing
      MyBase.CancelButtonClick(sender, e)
      Me.ProcessCodeTextBox.Focus()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.ProcessIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select Invalid Record to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      End If

      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._CurrentDefineProcessDataRow.RecordStatus_ID = 4
        _CurrentDefineProcessDataRow.Stamp_DateTime = Date.Now
        _CurrentDefineProcessDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
        Me._DefineProcessTableAdapter.Update(Me._DefineProcessDataTable)
        Me._CurrentDefineProcessDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
        Me.ProcessCodeTextBox.Focus()
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of DefineProcess Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region




End Class