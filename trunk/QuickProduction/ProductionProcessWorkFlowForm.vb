Imports System.Windows.Forms

Imports System.Drawing
Imports QuickDalLibrary
Imports QuickDal.QuickProductiondataset
Imports QuickDAL.QuickProductionDataSetTableAdapters
Imports QuickDAL.QuickProductionDataSet.ProductionProcessDataTable
Imports QuickDAL.QuickERP
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common




Public Class ProcessWorkFlowForm

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()
    ' Add any initialization after the InitializeComponent() call.
  End Sub

#Region "Declaration"

  Private _ProcessWorkFlowTableAdapter As New ProductionProcessWorkFlowTableAdapter
  Private _ProcessWorkFlowDataTable As New ProductionProcessWorkFlowDataTable
  Private _ProcessWorkFlowDataRow As ProductionProcessWorkFlowRow


  Private _DefineProcessTableAdapter As New ProductionProcessTableAdapter
  Private _DefineProcessDataTable As New ProductionProcessDataTable
  Private _DefineProcessDataRow As ProductionProcessRow

  Private _ProcessWorkFlowDetailID As Integer = 0


  Private Enum ProcessWorkFlowEnum
    DeleteRowButton
    Co_ID
    ProcessWorkFlow_ID
    Source_Process_ID
    Destination_Process_ID
    ProcessWorkFlow_Desc
    Stamp_UserID
    Stamp_Datetime
    Upload_Datetime
   RecordStatus_ID
  End Enum

#End Region

#Region "Events"
  Private Sub ProcessWorkFlowForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor

      PopulateProcessWorkFlow()

      Me.SetGridLayout()
      'Me.AddRow()
      Me.PopulateComboinGrid()

      '      SetVisibilityofColumn()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ProcessWorkFlowForm Load event method of ProcessWorkFlow", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub
#End Region

#Region "Methods"

  'Private Sub AddRow()
  '  Dim _DetailRow As QuickDAL.QuickProductionDataSet.ProductionProcessWorkFlowRow
  '  _DetailRow = Me._ProcessWorkFlowDataTable.NewProductionProcessWorkFlowRow
  '  _DetailRow.Co_ID = Me.LoginInfoObject.CompanyID
  '  _ProcessWorkFlowDetailID = _ProcessWorkFlowDetailID + 1
  '  _DetailRow.ProcessWorkFlow_ID = _ProcessWorkFlowDetailID
  '  _DetailRow.Source_Process_ID = 0
  '  _DetailRow.Destination_Process_ID = 0
  '  _DetailRow.ProcessWorkFlow_Desc = String.Empty
  '  _DetailRow.Stamp_UserID = Me.LoginInfoObject.CompanyID
  '  _DetailRow.Stamp_DateTime = Now.Date
  '  _DetailRow.Upload_DateTime = Now.Date
  '  _DetailRow.RecordStatus_ID = 0
  '  _ProcessWorkFlowDataTable.Rows.Add(_DetailRow)
  'End Sub


  Private Sub PopulateProcessWorkFlow()
    Try
      Me._ProcessWorkFlowDataTable = Me._ProcessWorkFlowTableAdapter.GetAll
      Me.ProcessWorkFlowQuickSpread.ActiveSheet.DataSource = _ProcessWorkFlowDataTable

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in PopulateProcessWorkFlow Click  on ProcessWorkFlow Form", ex)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Private Sub SetGridLayout()
    Try

      Me.ProcessWorkFlowQuickSpread.ShowDeleteRowButton(Me.ProcessWorkFlowQuickSpread.ActiveSheet) = True

      Dim _widthSmall As Integer = 50
      Dim _widthLarge As Integer = 80
      Dim _widthXLarge As Integer = 130


      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.ProcessWorkFlowQuickSpread.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case ProcessWorkFlowEnum.DeleteRowButton
            SheetColumn.Width = QTY_CELL_WIDTH

          Case ProcessWorkFlowEnum.Co_ID
            'SheetColumn.Visible = False
            SheetColumn.CellType = QtyCellType
            'SheetColumn.Visible = False

          Case ProcessWorkFlowEnum.ProcessWorkFlow_ID
            'SheetColumn.Visible = False

          Case ProcessWorkFlowEnum.Source_Process_ID
            SheetColumn.Label = "Source Process"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall

          Case ProcessWorkFlowEnum.Destination_Process_ID
            SheetColumn.Label = "Destination Process"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case ProcessWorkFlowEnum.ProcessWorkFlow_Desc
            SheetColumn.Label = "Process Description"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case ProcessWorkFlowEnum.Stamp_Datetime
            'SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH
          Case ProcessWorkFlowEnum.Stamp_UserID
            SheetColumn.Visible = False
            'SheetColumn.Width = QTY_CELL_WIDTH
          Case ProcessWorkFlowEnum.RecordStatus_ID
            'SheetColumn.Visible = False
          Case ProcessWorkFlowEnum.Upload_Datetime
            'SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH

          Case Else
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SetGridLayout method of ProcessWorkFlow.", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Private Sub PopulateComboinGrid()
    Dim mcb As New FarPoint.Win.Spread.CellType.MultiColumnComboBoxCellType()

    Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetAllByCoID(Me.LoginInfoObject.CompanyID)
    mcb.DataSourceList = Me._DefineProcessDataTable

    'Dim cbstr As String()
    'cbstr = New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun"}
    'Dim cmbocell As New FarPoint.Win.Spread.CellType.ComboBoxCellType()
    'cmbocell.Items = cbstr
    ''  cmbocell.AutoSearch = FarPoint.Win.AutoSearch.SingleCharacter
    'cmbocell.Editable = True
    'cmbocell.MaxDrop = 6
    'ProcessWorkFlowQuickSpread.ActiveSheet.Cells(0, 4).CellType = cmbocell
    mcb.ColumnEdit = 3
    mcb.DataColumn = 1

    'mcb.ButtonAlign = FarPoint.Win.ButtonAlign.Left
    mcb.ListWidth = 500
    mcb.ListOffset = 5
    mcb.MaxDrop = 5


    Me.ProcessWorkFlowQuickSpread.ActiveSheet.Columns(4).CellType = mcb
    Me.ProcessWorkFlowQuickSpread.ActiveSheet.Columns(6).CellType = mcb
    '  Me.ProcessWorkFlowQuickSpread.ActiveSheet.Cells(0, 3).CellType = mcb
  End Sub

  Private Function IsValid() As Boolean
    Try

      Return True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function
  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() = True Then
        Me._ProcessWorkFlowDataTable.Rows.RemoveAt(_ProcessWorkFlowDataTable.Rows.Count - 1)
        'Update Master Accounting Voucher
        'If _ProcessWorkFlowDataRow Is Nothing Then
        '  _ProcessWorkFlowDataRow = Me._ProcessWorkFlowDataTable.NewProductionProcessWorkFlowRow
        '  me._
        '  _VoucherRow.Voucher_ID = CInt(_VoucherTableAdapterObject.GetNewVoucherIDByCoID(LoginInfoObject.CompanyID))


        '  '_LikeOperatorPattern = Common.GenerateNextDocumentNo(String.Empty, String.Empty, DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry"), True)
        '  _LikeOperatorPattern = Common.GenerateNextDocumentNo(String.Empty, String.Empty, "999999", True)
        '  _LastVoucherNo = _VoucherTableAdapterObject.GetMaxVoucherNoByCoID(LoginInfoObject.CompanyID, _LikeOperatorPattern)
        '  If _LastVoucherNo Is Nothing Then
        '    'Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, "", DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry"), False)
        '    Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, "", "999999", False)
        '  Else
        '    'Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, _LastVoucherNo.ToString, DatabaseCache.GetSettingValue("DocumentNoFormat.VoucherEntry"), False)
        '    Me.VoucherNoTextBox.Text = Common.GenerateNextDocumentNo(String.Empty, _LastVoucherNo.ToString, "999999", False)
        '  End If

        '  Me._VoucherRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        'Else
        '  If Me._VoucherRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
        '    Me._VoucherRow.RecordStatus_ID = Constants.RecordStatuses.Updated
        '  End If
        'End If
        'Me.VoucherIDTextBox.Text = CStr(_VoucherRow.Voucher_ID)

        '_VoucherRow.Co_ID = LoginInfoObject.CompanyID
        '_VoucherRow.Remarks = Me.RemarksTextBox.Text

        '_VoucherRow.Voucher_No = Me.VoucherNoTextBox.Text

        '_VoucherRow.Voucher_Date = Now
        '_VoucherRow.VoucherType_ID = CInt(VoucherTypeComboBox.Value)
        ''Hidden values
        '_VoucherRow.Stamp_DateTime = Date.Now
        '_VoucherRow.Stamp_UserID = LoginInfoObject.UserID
        '_VoucherRow.Upload_DateTime = Date.Now
        '_VoucherRow.DocumentStatus_ID = CByte(CShort(Me._VoucherStatus))

        'If IsDBNull(VoucherDateCalendarCombo.Value) = False Then
        '  _VoucherRow.Voucher_Date = Convert.ToDateTime(VoucherDateCalendarCombo.Value)
        'End If
        '_VoucherRow.Remarks = RemarksTextBox.Text

        'If _VoucherRow.RowState = DataRowState.Detached Then
        '  _VoucherTable.Rows.Add(_VoucherRow)
        'End If


        '_VoucherTableAdapterObject.Update(_VoucherTable)

        'Update Detaill Voucher Detail

        '_VoucherTableDetailAdapterObject.Update(_VoucherDetailTable.Select("", "", DataViewRowState.Deleted))

        For I As Int32 = 0 To Me._ProcessWorkFlowDataTable.Rows.Count - 1
          Me._ProcessWorkFlowDataRow = Me._ProcessWorkFlowDataTable(I)
          If _ProcessWorkFlowDataRow.RowState = DataRowState.Added Then
            _ProcessWorkFlowDataRow.ProcessWorkFlow_ID = 1
            _ProcessWorkFlowDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted

          ElseIf _ProcessWorkFlowDataRow.RowState = DataRowState.Modified Then
            'Assign first row by filtering. There should not be more than one rows theoratically here if data is stored correctly.
            _ProcessWorkFlowDataRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            _ProcessWorkFlowDataRow = _ProcessWorkFlowDataTable(I)
          ElseIf _ProcessWorkFlowDataRow.RowState = DataRowState.Unchanged Then
            _ProcessWorkFlowDataRow = _ProcessWorkFlowDataTable(I)
          ElseIf _ProcessWorkFlowDataRow.RowState = DataRowState.Deleted Then
            _ProcessWorkFlowDataRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
            _ProcessWorkFlowDataRow = _ProcessWorkFlowDataTable(I)
          End If
          'Common Fields
          _ProcessWorkFlowDataRow.Co_ID = LoginInfoObject.CompanyID
          '     _ProcessWorkFlowDataRow.Source_Process_ID = 1
          '     _ProcessWorkFlowDataRow.Destination_Process_ID = 2
          '    _ProcessWorkFlowDataRow.ProcessWorkFlow_Desc  = me.

          _ProcessWorkFlowDataRow.Stamp_DateTime = Now
          _ProcessWorkFlowDataRow.Stamp_UserID = LoginInfoObject.UserID
          _ProcessWorkFlowDataRow.Upload_DateTime = Now
          If _ProcessWorkFlowDataRow.RowState <> DataRowState.Unchanged Then
            If _ProcessWorkFlowDataRow.RowState = DataRowState.Detached Then
              _ProcessWorkFlowDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
              _ProcessWorkFlowDataTable.Rows.Add(_ProcessWorkFlowDataRow)
            End If

            'This statement should be inside loop so that we can fetch new detail id properly.
            If Me._ProcessWorkFlowTableAdapter.Update(Me._ProcessWorkFlowDataTable(I)) > 0 Then
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


#End Region

#Region "Properties"

#End Region

#Region "ToolBar Methods"
  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
        'Me.AddRow()
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick method of ProcessWorkFlow Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


#End Region

End Class

