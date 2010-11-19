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

  Dim ComboType As New FarPoint.Win.Spread.CellType.ComboBoxCellType

  Private _ProcessWorkFlowDetailID As Integer = 0


  Private Enum ProcessWorkFlowEnum
    DeleteRowButton
    Co_ID
    ProcessWorkFlow_ID
    Source_Process_ID
    Source_Process_Desc
    Destination_Process_ID
    Destination_Process_Desc
    ProcessWorkFlow_Desc
    Stamp_UserID
    Stamp_Datetime
    Upload_Datetime
    RecordStatus_ID
  End Enum

#End Region

#Region "Events"
  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub ProcessWorkFlowForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor

      PopulateProcessWorkFlow()

      Me.SetGridLayout()
      Me._ProcessWorkFlowDetailID = Me._ProcessWorkFlowTableAdapter.IsMaxByProcessWorkFlowID(Me.LoginInfoObject.CompanyID)
      Me.AddRow()
      Me.PopulateComboinGrid()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ProcessWorkFlowForm Load event method of ProcessWorkFlow", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub

  ' Catch ex As Exception
  'Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ProcessWorkFlowForm Load event method of ProcessWorkFlow", ex)
  '    QuickExceptionObject.Show(LoginInfoObject)
  '  Finally
  '    Cursor = Cursors.Default
  '  End Try


#End Region

#Region "Methods"
  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub AddRow()
    Try
      Dim _DetailRow As QuickDAL.QuickProductionDataSet.ProductionProcessWorkFlowRow
      _DetailRow = Me._ProcessWorkFlowDataTable.NewProductionProcessWorkFlowRow
      _DetailRow.Co_ID = Me.LoginInfoObject.CompanyID
      _ProcessWorkFlowDetailID = _ProcessWorkFlowDetailID + 1
      _DetailRow.ProcessWorkFlow_ID = _ProcessWorkFlowDetailID
      _DetailRow.Source_Process_ID = 0
      _DetailRow.Destination_Process_ID = 0
      _DetailRow.ProcessWorkFlow_Desc = String.Empty
      _DetailRow.Stamp_UserID = Me.LoginInfoObject.CompanyID
      _DetailRow.Stamp_DateTime = Now.Date
      _DetailRow.Upload_DateTime = Now.Date
      _DetailRow.RecordStatus_ID = 0
      _ProcessWorkFlowDataTable.Rows.Add(_DetailRow)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in AddRow method of ProcessWorkFlow.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub

  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub PopulateProcessWorkFlow()
    Try
      Me._ProcessWorkFlowDataTable = Me._ProcessWorkFlowTableAdapter.GetAllByCoID(Me.LoginInfoObject.CompanyID)
      Me.ProcessWorkflowSheetView.DataSource = _ProcessWorkFlowDataTable

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in PopulateProcessWorkFlow Click  on ProcessWorkFlow Form", ex)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub SetGridLayout()
    Try
      Me.ProcessWorkFlowQuickSpread.ShowDeleteRowButton(Me.ProcessWorkflowSheetView) = True

      Dim _widthSmall As Integer = 50
      Dim _widthLarge As Integer = 80
      Dim _widthXLarge As Integer = 130


      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.ProcessWorkflowSheetView.Columns
        Select Case SheetColumn.Index
          Case ProcessWorkFlowEnum.DeleteRowButton
            SheetColumn.Width = QTY_CELL_WIDTH
          Case ProcessWorkFlowEnum.Co_ID
            SheetColumn.Visible = False
            SheetColumn.CellType = QtyCellType
          Case ProcessWorkFlowEnum.ProcessWorkFlow_ID
            SheetColumn.Visible = False
          Case ProcessWorkFlowEnum.Source_Process_ID
            SheetColumn.Label = "SourceProcess_ID"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.Visible = False
          Case ProcessWorkFlowEnum.Source_Process_Desc
            SheetColumn.Label = "Source Process"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
          Case ProcessWorkFlowEnum.Destination_Process_ID
            SheetColumn.Label = "DescriptionProcess_ID"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge
            SheetColumn.Visible = False
          Case ProcessWorkFlowEnum.Destination_Process_Desc
            SheetColumn.Label = "Destination Process"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge
          Case ProcessWorkFlowEnum.ProcessWorkFlow_Desc
            SheetColumn.Label = "Work Flow Destination"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge
          Case ProcessWorkFlowEnum.Stamp_Datetime
            SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH
          Case ProcessWorkFlowEnum.Stamp_UserID
            SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH
          Case ProcessWorkFlowEnum.RecordStatus_ID
            SheetColumn.Visible = False
          Case ProcessWorkFlowEnum.Upload_Datetime
            SheetColumn.Visible = False
            SheetColumn.Width = QTY_CELL_WIDTH
          Case Else
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SetGridLayout method of ProcessWorkFlow.", ex)
      Throw QuickExceptionObject
    End Try
  End Sub
  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub PopulateComboinGrid()
    Try
      Me._DefineProcessDataTable = Me._DefineProcessTableAdapter.GetAllByCoID(Me.LoginInfoObject.CompanyID)
      Dim _Items() As String

      ReDim _Items(_DefineProcessDataTable.Rows.Count - 1)
      For i As Int32 = 0 To _DefineProcessDataTable.Rows.Count - 1
        _Items(i) = _DefineProcessDataTable(i).Process_Desc
      Next
      ComboType.Items = _Items
      Me.ProcessWorkflowSheetView.Columns(4).CellType = ComboType
      Me.ProcessWorkflowSheetView.Columns(6).CellType = ComboType
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in PopulateComboinGrid method of ProcessWorkFlow.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  'Author:  Zakee
  'Date Created(DD-MMM-YY):  14-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Function IsValid() As Boolean
    Try
      Me.ProcessWorkFlowQuickSpread.EditMode = False
      For i As Int32 = 0 To Me.ProcessWorkflowSheetView.Rows.Count - 2
        If Me.ProcessWorkflowSheetView.GetText(i, ProcessWorkFlowEnum.Destination_Process_ID) = String.Empty Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Invalid Destination Process Selected to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.ProcessWorkflowSheetView.SetActiveCell(i, ProcessWorkFlowForm.ProcessWorkFlowEnum.Destination_Process_Desc)
          SendKeys.Send("{F4}")
          Return False
        ElseIf Me.ProcessWorkflowSheetView.GetText(i, ProcessWorkFlowEnum.ProcessWorkFlow_Desc) = String.Empty Then
          MsgBox(Me.ProcessWorkflowSheetView.GetText(i, ProcessWorkFlowEnum.ProcessWorkFlow_Desc))
          QuickMessageBox.Show(Me.LoginInfoObject, "You must entered the process work flow description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.ProcessWorkflowSheetView.SetActiveCell(i - 0, ProcessWorkFlowForm.ProcessWorkFlowEnum.ProcessWorkFlow_Desc)
          SendKeys.Send("{F2}")
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

  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-NOV-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Protected Overrides Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        Me.ProcessWorkFlowQuickSpread.EditMode = False

        Me.ProcessWorkflowSheetView.SetActiveCell(Me.ProcessWorkflowSheetView.RowCount - 1, 0) 'For some unknown reason new version of farpoint is not working without this line.
        Me._ProcessWorkFlowDataTable.Rows.RemoveAt(_ProcessWorkFlowDataTable.Rows.Count - 1)

        Me.ProcessWorkFlowQuickSpread.Update()
        For I As Int32 = 0 To _ProcessWorkFlowDataTable.Rows.Count - 1
          With _ProcessWorkFlowDataTable(I)
            If .RowState = DataRowState.Added Then
              .RecordStatus_ID = Constants.RecordStatuses.Inserted
            ElseIf .RowState = DataRowState.Modified Then
              'Assign first row by filtering. There should not be more than one rows theoratically here if data is stored correctly.
              .RecordStatus_ID = Constants.RecordStatuses.Updated
            ElseIf .RowState = DataRowState.Deleted Then
              .RecordStatus_ID = Constants.RecordStatuses.Deleted
            End If

            'Common Fields
            .Co_ID = LoginInfoObject.CompanyID
            .Stamp_DateTime = Now
            .Stamp_UserID = LoginInfoObject.UserID

            'Debug.WriteLine(I.ToString & "/" & _ProcessWorkFlowDataTable.Rows.Count.ToString & "=" & .Co_ID.ToString & "-" & .Source_Process_ID.ToString & "-" & .Destination_Process_ID.ToString)
          End With

          _ProcessWorkFlowTableAdapter.Update(_ProcessWorkFlowDataTable(I))
        Next

        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord method of ProcessWorkFlowForm.", ex)
      Throw QuickExceptionObject
    End Try
  End Function



#End Region

#Region "Properties"

#End Region

#Region "ToolBar Methods"
  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-OCT-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
        Me._ProcessWorkFlowDetailID = Me._ProcessWorkFlowTableAdapter.IsMaxByProcessWorkFlowID(Me.LoginInfoObject.CompanyID)
        Me.AddRow()
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


  'Author:  Zakee
  'Date Created(DD-MMM-YY):  10-OCT-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------

  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub ProcessWorkFlowQuickSpread_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProcessWorkFlowQuickSpread.EditModeOff
    Try
      If Me.ProcessWorkFlowQuickSpread.ActiveSheet Is Nothing OrElse Me.ProcessWorkFlowQuickSpread.ActiveSheet.ActiveCell Is Nothing Then Exit Sub

      If Me.ProcessWorkFlowQuickSpread.ActiveSheet.ActiveColumn.Label = "Source Process" Then
        _DefineProcessDataTable.DefaultView.RowFilter = _DefineProcessDataTable.Process_DescColumn.ColumnName & "='" & Me.ProcessWorkflowSheetView.GetText(Me.ProcessWorkflowSheetView.ActiveRow.Index, 4) & "'"
        If _DefineProcessDataTable.DefaultView.Count > 0 Then
          Me.ProcessWorkflowSheetView.SetText(Me.ProcessWorkflowSheetView.ActiveRow.Index, 3, _DefineProcessDataTable.DefaultView(0)(_DefineProcessDataTable.Process_IDColumn.ColumnName).ToString)
        End If
      End If

      If Me.ProcessWorkFlowQuickSpread.ActiveSheet.ActiveColumn.Label = "Destination Process" Then
        _DefineProcessDataTable.DefaultView.RowFilter = _DefineProcessDataTable.Process_DescColumn.ColumnName & "='" & Me.ProcessWorkflowSheetView.GetText(Me.ProcessWorkflowSheetView.ActiveRow.Index, 6) & "'"
        If _DefineProcessDataTable.DefaultView.Count > 0 Then
          Me.ProcessWorkflowSheetView.SetText(Me.ProcessWorkflowSheetView.ActiveRow.Index, 5, _DefineProcessDataTable.DefaultView(0)(_DefineProcessDataTable.Process_IDColumn.ColumnName).ToString)
        End If
      End If

      If Me.ProcessWorkflowSheetView.ActiveRowIndex = ProcessWorkflowSheetView.RowCount - 1 Then
        If ProcessWorkflowSheetView.GetText(ProcessWorkflowSheetView.ActiveRowIndex, ProcessWorkFlowForm.ProcessWorkFlowEnum.Destination_Process_Desc) <> String.Empty Then
          AddRow()
        End If
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ProcessWorkFlowForm EditMofeOff event method of ProcessWorkFlow", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
End Class

