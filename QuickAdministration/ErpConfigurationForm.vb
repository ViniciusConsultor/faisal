Imports QuickLibrary
Imports QuickDALLibrary
Imports QuickDAL
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters

Public Class ErpConfigurationForm

#Region "Declaration"
  Dim _SettingTA As New SettingTableAdapter
  Dim _SettingTable As SettingDataTable
#End Region

#Region "Properties"

#End Region

#Region "Methods"

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.FormCode = "03-007"
    Me.FormVersion = "1"
  End Sub

  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      If Me.SettingIDComboBox.SelectedRow IsNot Nothing Then
        For I As Int32 = 0 To Me._SettingTable.Rows.Count - 1
          If Me._SettingTable(I).Setting_Id = Me.SettingIDComboBox.SelectedRow.Cells(Me._SettingTable.Setting_IdColumn.ColumnName).Text Then
            Me._SettingTable(I).Setting_Value = Me.SetttingValueTextBox.Text
          End If
        Next

        'Udate records in the database.
        Me._SettingTA.Update(Me._SettingTable)

        'Call the base method to show the success messagebox.
        MyBase.SaveButtonClick(sender, e)
      Else
        'If user had not selected any row from the combo box.
        QuickMessageBox.Show(Me.LoginInfoObject, "Select configuration from combo box to save it", MessageBoxIcon.Information)
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of ErpConfigurationForm", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

#Region "Events"
  Private Sub ErpConfigurationForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      _SettingTable = _SettingTA.GetPosSalesInvoiceReportFooter
      Me.SettingIDComboBox.DataSource = _SettingTable

      Me.SettingIDComboBox.Rows.Band.ColHeadersVisible = False
      Me.SettingIDComboBox.DisplayMember = _SettingTable.Setting_DescColumn.ColumnName
      Me.SettingIDComboBox.ValueMember = _SettingTable.Setting_DescColumn.ColumnName
      For I As Int32 = 0 To Me.SettingIDComboBox.Rows.Band.Columns.Count - 1
        If Me.SettingIDComboBox.Rows.Band.Columns(I).Key = _SettingTable.Setting_DescColumn.ColumnName Then
          Me.SettingIDComboBox.Rows.Band.Columns(I).Header.Caption = "Description"
        Else
          Me.SettingIDComboBox.Rows.Band.Columns(I).Hidden = True
        End If
      Next

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in load event method of Erp Configuration Form", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub SettingIDComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles SettingIDComboBox.Resize
    Try
      Me.SettingIDComboBox.DropDownWidth = Me.SettingIDComboBox.Width
      If Me.SettingIDComboBox.Rows.Band.Columns.Count > 0 Then
        Me.SettingIDComboBox.Rows.Band.Columns(Me._SettingTable.Setting_DescColumn.ColumnName).Width = Me.SettingIDComboBox.DropDownWidth - Constants.SCROLLBAR_WIDTH
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in resize event method of SettingIDComboBox.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub SettingIDComboBox_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles SettingIDComboBox.RowSelected
    Try
      If Me.SettingIDComboBox.SelectedRow IsNot Nothing Then
        SetttingValueTextBox.Text = Me.SettingIDComboBox.SelectedRow.Cells(_SettingTable.Setting_ValueColumn.ColumnName).Text
      Else
        'If no row is selected then don't do anything.
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in RowSelected event method of SettingIDComboBox.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

End Class