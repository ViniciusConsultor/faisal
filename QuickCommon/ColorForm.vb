Imports System.Windows.Forms
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickInventoryDataSetTableAdapters
Imports QuickDALLibrary
Imports QuickLibrary


Public Class ColorForm

#Region "Declaration"
  Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _ColorTableAdapterObject As New CommonColorTableAdapter
  Private _ColorDataTable As New CommonColorDataTable
  Private _ColorDataRow As CommonColorRow
  Private _SelectedColorValue As Integer
#End Region

#Region "Events"
  Private Sub ColorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      CompanyComboBox.CompanyID = Me.LoginInfoObject.CompanyID
      Me.ColorCodeTextBox.MaxLength = Me._ColorDataTable.Color_CodeColumn.MaxLength
      Me.ColorDescTextBox.MaxLength = Me._ColorDataTable.Color_DescColumn.MaxLength

      Me.ColorCodeTextBox.Text = String.Empty
      Me.ColorDescTextBox.Text = String.Empty
      Me.ColorIDTextBox.Text = String.Empty

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Load event method of ColorForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub ColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorLabel.Click
    Try
      Me.ColorDialog1.FullOpen = True
      Me.ColorDialog1.Color = Me.ColorLabel.BackColor
      Me.ColorDialog1.ShowDialog()
      Me.ColorLabel.BackColor = Me.ColorDialog1.Color
      Me._SelectedColorValue = Drawing.ColorTranslator.ToWin32(ColorDialog1.Color)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Load event method of ColorLabelClick.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

#Region "Methods"
  Public Sub New()
    'This call is required by the Windows Form Designer.
    InitializeComponent()
    'Add any initialization after the InitializeComponent() call.
  End Sub


  Private Function IsValid() As Boolean
    Try
      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False
      ElseIf Me.ColorCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the color code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ColorCodeTextBox.Focus()
        Return False
      ElseIf Me.ColorDescTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the color description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.ColorDescTextBox.Focus()
        Return False
      End If

      Dim _CheckDuplicateCode As String
      If Me.ColorIDTextBox.Text = String.Empty Then
        _CheckDuplicateCode = CStr(Me._ColorTableAdapterObject.GetCoIDDuplicateColorCode(Me.CompanyComboBox.CompanyID, Me.ColorCodeTextBox.Text))
      Else
        _CheckDuplicateCode = CStr(Me._ColorTableAdapterObject.GetCoIDDuplicateColorCodeByColorID(Me.CompanyComboBox.CompanyID, CInt(Me.ColorIDTextBox.Text), Me.ColorCodeTextBox.Text))
      End If

      If _CheckDuplicateCode <> String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate color code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
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
      Cursor = Cursors.WaitCursor
      Me.CompanyComboBox.Enabled = False
      If Me._ColorDataTable.Rows.Count > 0 Then
        Me.ClearControls(Me)
        Me._ColorDataRow = Me._ColorDataTable(Me.CurrentRecordIndex)
        Me.CompanyComboBox.CompanyID = _ColorDataRow.Co_ID
        Me.ColorIDTextBox.Text = _ColorDataRow.Color_ID.ToString()
        Me.ColorCodeTextBox.Text = _ColorDataRow.Color_Code
        Me.ColorDescTextBox.Text = _ColorDataRow.Color_Desc
        Me._SelectedColorValue = _ColorDataRow.ColorValue
        Me.ColorLabel.BackColor = Drawing.ColorTranslator.FromWin32(CInt(_SelectedColorValue))
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord event method of ColorForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If _ColorDataRow Is Nothing Then
          _ColorDataRow = Me._ColorDataTable.NewCommonColorRow
          Me.ColorIDTextBox.Text = CStr(Me._ColorTableAdapterObject.GetNewColorIDByCoID(Me.CompanyComboBox.CompanyID))
          _ColorDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        Else
          If _ColorDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            _ColorDataRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If
        End If
        With _ColorDataRow
          .Co_ID = CompanyComboBox.CompanyID
          .Color_ID = CShort(CInt(Me.ColorIDTextBox.Text))
          .Color_Code = Me.ColorCodeTextBox.Text.Trim
          .Color_Desc = Me.ColorDescTextBox.Text.Trim
          .ColorValue = Me._SelectedColorValue
          'Hidden values
          .Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)
          .Stamp_DateTime = Date.Now
          .Upload_DateTime = Date.Now

          If .RowState = DataRowState.Detached Then
            Me._ColorDataTable.Rows.Add(Me._ColorDataRow)
          End If
          Me._ColorTableAdapterObject.Update(_ColorDataTable)
          Me.CompanyComboBox.ReadOnly = True
        End With
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord event method of ColorForm.", ex)
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
      Me._ColorDataTable = Me._ColorTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of ColorForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (Me._ColorDataRow Is Nothing) Then
        Me._ColorDataTable = Me._ColorTableAdapterObject.GetFirstByCoID(Me.CompanyComboBox.CompanyID)
      Else
        _ColorDataTable = Me._ColorTableAdapterObject.GetNextByCoIDColorID(Me.CompanyComboBox.CompanyID, CInt(Me.ColorIDTextBox.Text))
        If _ColorDataTable.Count = 0 Then
          Me._ColorDataTable = Me._ColorTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of ColorForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_ColorDataRow Is Nothing) Then
        Me._ColorDataTable = Me._ColorTableAdapterObject.GetPreviousByCoIDColorID(Me.CompanyComboBox.CompanyID, 0)
      Else
        _ColorDataTable = Me._ColorTableAdapterObject.GetPreviousByCoIDColorID(Me.CompanyComboBox.CompanyID, _ColorDataRow.Color_ID)
      End If

      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of ColorForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._ColorDataTable = Me._ColorTableAdapterObject.GetLastByCoID(Me.CompanyComboBox.CompanyID)
      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of ColorForm", ex)
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
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of ColorForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _ActiveCompany As Int32
      Me.CompanyComboBox.ReadOnly = False
      Me.CompanyComboBox.Enabled = True
      Me.ColorLabel.BackColor = Drawing.ColorTranslator.FromWin32(CInt(14215660))
      _ActiveCompany = Me.CompanyComboBox.CompanyID
      Me._ColorDataRow = Nothing
      Me._SelectedColorValue = Nothing

      Me._ColorDataTable.Rows.Clear()
      MyBase.CancelButtonClick(sender, e)
      Me.CompanyComboBox.CompanyID = CShort(_ActiveCompany)
      Me.ColorCodeTextBox.Focus()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of ColorForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If CompanyComboBox.CompanyID <= 0 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the company to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      ElseIf Me.ColorIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Exit Sub
      End If

      If Me._ColorDataTable.Rows.Count < 1 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      ElseIf MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._ColorDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        Me.SaveRecord()
        Me._ColorDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of ColorForm", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region


  
  
  
 
End Class