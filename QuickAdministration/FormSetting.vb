Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
'Imports QuickDAL.QuickAccountingDataSet
'Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDalLibrary
'Imports QuickERP

Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common


Public Class FormSetting

  'Author: Zakee 

  'Date Created(DD-MMM-YY):  
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  'Zakee   25-Feb-2010       New form
  ''' <summary>
  ''' Description of the class goes here ...
  ''' </summary>

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()
    ' Add any initialization after the InitializeComponent() call.
  End Sub
#Region "Declarations"
  Private _FormSettingTableAdapter As New SettingFormTableAdapter
  Private _FormSettingCompanyTableAdapter As New SettingFormCompanyAssociationTableAdapter
  Private _ControlSettingTableAdapter As New SettingFormControlsTableAdapter
  Private _ControlSettingCompanyTableAdapter As New SettingFormControlsCompanyAssociationTableAdapter
  Private _CombineControlSettingTableAdapter As New CombineControlSettingTableAdapter

  Private _FormSettingDataTable As New SettingFormDataTable
  Private _FormSettingCompanyDataTable As New SettingFormCompanyAssociationDataTable
  Private _ControlSettingDataTable As New SettingFormControlsDataTable
  Private _ControlSettingCompanyDataTable As New SettingFormControlsCompanyAssociationDataTable
  Private _CombineControlSettingDataTable As New CombineControlSettingDataTable

  Private _FormSettingRow As SettingFormRow
  Private _FormSettingCompanyRow As SettingFormCompanyAssociationRow
  Private _ControlSettingRow As SettingFormControlsRow
  Private _ControlSettingCompanyRow As SettingFormControlsCompanyAssociationRow

  Private _CombineControlSettingRow As CombineControlSettingRow
  Private _controlSettingRowData() As CombineControlSettingRow


  'Private _FormSettingTableAdapter As New QuickCommonDataSetTableAdapters.Base_SettingFormTableAdapter
  'Private _FormSettingCompanyTableAdapter As New QuickCommonDataSetTableAdapters.Base_SettingForm_Company_AssociationTableAdapter
  'Private _ControlSettingTableAdapter As New QuickCommonDataSetTableAdapters.Base_SettingFormControlsTableAdapter
  'Private _ControlSettingCompanyTableAdapter As New QuickCommonDataSetTableAdapters.Base_SettingFormControls_Company_AssociationTableAdapter
  'Private _CombineControlSettingTableAdapter As New QuickCommonDataSetTableAdapters.Base_CombineControlSettingTableAdapter

  'Private _FormSettingDataTable As New QuickCommonDataSet.Base_SettingFormDataTable
  'Private _FormSettingCompanyDataTable As New QuickCommonDataSet.Base_SettingForm_Company_AssociationDataTable
  'Private _ControlSettingDataTable As New QuickCommonDataSet.Base_SettingFormControlsDataTable
  'Private _ControlSettingCompanyDataTable As New QuickCommonDataSet.Base_SettingFormControls_Company_AssociationDataTable
  'Private _CombineControlSettingDataTable As New QuickCommonDataSet.Base_CombineControlSettingDataTable

  'Private _FormSettingRow As QuickCommonDataSet.Base_SettingFormRow
  'Private _FormSettingCompanyRow As QuickCommonDataSet.Base_SettingForm_Company_AssociationRow
  'Private _ControlSettingRow As QuickCommonDataSet.Base_SettingFormControlsRow
  'Private _ControlSettingCompanyRow As QuickCommonDataSet.Base_SettingFormControls_Company_AssociationRow
  'Private _CombineControlSettingRow As QuickCommonDataSet.Base_CombineControlSettingRow

  'Private _controlSettingRowData() As QuickCommonDataSet.Base_CombineControlSettingRow

  Private _ControlSettingCompanyRowscount As Boolean = False
  'Private _ControlSettingUntypedDatatable As DataRow
  'Private _UntypedFormRow As DataRow
  Private _ControlID As Int32 = 0
  Private _Flag As Boolean

  Private _CompanyID As Integer



#End Region

#Region "Event Methods"
  Private Sub FormSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Me.FormCodeTextBox.MaxLength = Me._FormSettingDataTable.Form_CodeColumn.MaxLength
      Me.FormNameTextBox.MaxLength = Me._FormSettingDataTable.Form_NameColumn.MaxLength
      Me.FormCaptionTextBox.MaxLength = Me._FormSettingCompanyDataTable.Form_CaptionColumn.MaxLength

      PopulateFormControlSettingGrid(-1, -1)

      Me.FormCodeTextBox.Text = String.Empty
      Me.FormNameTextBox.Text = String.Empty
      Me.FormCaptionTextBox.Text = String.Empty
      Me.FormIDTextBox.Text = String.Empty
      Me._CompanyID = Me.LoginInfoObject.CompanyID
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Load event method of FormSetting.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Enum ControlSettingEnum
    DeleteRowButton
    CO_ID
    Form_ID
    Control_ID
    Control_Name
    Control_Caption
    ReadonlyForNewRecord
    ReadonlyForExistingRecord
    Mandatory
    RecordStatus_ID
    Stamp_UserID
    Stamp_Datetime
    upload_datetime
    ExP1
  End Enum



#End Region

#Region "Properties"

#End Region

#Region "Methods"
  Private Sub PopulateFormControlSettingGrid(ByVal Co_ID As Integer, ByVal Form_ID As Integer)
    Try
      Me._CombineControlSettingDataTable = Me._CombineControlSettingTableAdapter.GetByCombineControlSetting(Co_ID, Form_ID)
      Me.FormControlSettingQuickSpread.ActiveSheet.DataSource = Me._CombineControlSettingDataTable
      Me._CombineControlSettingDataTable.AcceptChanges()

      AddRow()
      Me.SetGridLayout()
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in PopulateFormControlSettingGrid of FormSetting.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub
  Private Sub SetGridLayout()
    Try
      Me.FormControlSettingQuickSpread.ShowDeleteRowButton(Me.FormControlSettingQuickSpread.ActiveSheet) = True
      Dim _visible As Boolean = False
      Dim _widthSmall As Integer = 50
      Dim _widthLarge As Integer = 80
      Dim _widthXLarge As Integer = 130


      For Each SheetColumn As FarPoint.Win.Spread.Column In Me.FormControlSettingQuickSpread.ActiveSheet.Columns
        Select Case SheetColumn.Index
          Case ControlSettingEnum.DeleteRowButton
            SheetColumn.Width = QTY_CELL_WIDTH

          Case ControlSettingEnum.CO_ID
            SheetColumn.Label = "CO. ID"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.Visible = False
          Case ControlSettingEnum.Form_ID
            SheetColumn.Label = "Form ID"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.Visible = False
          Case ControlSettingEnum.Control_ID
            SheetColumn.Label = "Control ID"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthSmall
            SheetColumn.Visible = False
          Case ControlSettingEnum.Control_Name
            SheetColumn.Label = "Control Name"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthXLarge
          Case ControlSettingEnum.Control_Caption
            SheetColumn.Label = "Control Caption"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge + _widthXLarge
          Case ControlSettingEnum.ReadonlyForNewRecord
            SheetColumn.Label = "New Record"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthXLarge
          Case ControlSettingEnum.ReadonlyForExistingRecord
            SheetColumn.Label = "Existing Record"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case ControlSettingEnum.Mandatory
            SheetColumn.Label = "Mandatory"
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case ControlSettingEnum.RecordStatus_ID
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH + _widthLarge

          Case ControlSettingEnum.Stamp_UserID
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH

          Case ControlSettingEnum.Stamp_Datetime
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH

          Case ControlSettingEnum.upload_datetime
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH
          Case ControlSettingEnum.ExP1
            SheetColumn.Visible = _visible
            SheetColumn.Width = QTY_CELL_WIDTH

          Case Else
        End Select
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SetGridLayout method of FormSetting.", ex)
      Throw QuickExceptionObject
    End Try
  End Sub
  Private Sub AddRow()
    Try
      If Me.FormIDTextBox.Text = String.Empty Then
        Me.FormIDTextBox.Text = CStr(0)
      End If
      If _CombineControlSettingDataTable.Rows.Count > 0 Then
        If Me._Flag = True Then
          Me._ControlID = CInt(Me._ControlSettingTableAdapter.GetMaxControlID(CInt(Me.FormIDTextBox.Text)))
        End If
        Me._Flag = False
        Me._ControlID = Me._ControlID + 1
      Else

        Me._ControlID = CInt(Me._ControlSettingTableAdapter.GetMaxControlID(CInt(Me.FormIDTextBox.Text)))
        Me._ControlID = Me._ControlID + 1
      End If

      Dim _CombineControlRow As CombineControlSettingRow
      _CombineControlRow = Me._CombineControlSettingDataTable.NewCombineControlSettingRow

      _CombineControlRow.Co_ID = CShort(Me._CompanyID)
      _CombineControlRow.Form_ID = CShort(Me.FormIDTextBox.Text)
      _CombineControlRow.Control_ID = CShort(Me._ControlID)
      _CombineControlRow.Control_Caption = String.Empty
      _CombineControlRow.Control_Name = String.Empty
      _CombineControlRow.Control_Caption = String.Empty
      _CombineControlRow.ReadonlyForNewRecord = False
      _CombineControlRow.ReadonlyForExistingRecord = False
      _CombineControlRow.Mandatory = False
      _CombineControlRow.RecordStatus_ID = 0
      _CombineControlRow.Stamp_UserID = 0
      _CombineControlRow.Stamp_DateTime = Now
      _CombineControlRow.Upload_DateTime = Now
      _CombineControlRow.EXP1 = 0
      Me._CombineControlSettingDataTable.Rows.Add(_CombineControlRow)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in AddRow method of FormSetting.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Private Function IsValid() As Boolean
    Try
      '  Me._CombineControlSettingDataTable.Rows.RemoveAt(_CombineControlSettingDataTable.Rows.Count - 1)
      Me.FormControlSettingQuickSpread.Update()
      Me.FormControlSettingQuickSpread.EditMode = False

      If Me.FormCodeTextBox.Text.Trim = String.Empty Then
        MessageBox.Show("Your must enter the Form code to save the record", "Invalid Form Code", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.FormCodeTextBox.Focus()
        Return False
      ElseIf Me.FormNameTextBox.Text.Trim = String.Empty Then
        MessageBox.Show("Your must enter the Form name to save the record", "Invalid Form Name", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.FormNameTextBox.Focus()
        Return False
      ElseIf Me.FormCaptionTextBox.Text.Trim = String.Empty Then
        MessageBox.Show("Your must enter the Form caption to save the record", "Invalid Form Caption", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.FormNameTextBox.Focus()
        Return False
      ElseIf Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count < 1 Then
        MessageBox.Show("There must be atleast one control list to save", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.FormControlSettingQuickSpread.Focus()
        Return False
      End If

      ' Me.FormControlSettingQuickSpread.EditMode = False

      If Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count = 1 Then
        For i As Int16 = 0 To CShort(Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count - 1)
          If Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.Control_Name).Trim = String.Empty Then
            MessageBox.Show("Invalid Control name entered to save the record", "Invalid Control name", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
          End If
        Next
      Else
        For i As Int16 = 0 To CShort(Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count - 2)
          If Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.Control_Name).Trim = String.Empty Then
            MessageBox.Show("Invalid Control name entered to save the record", "Invalid Control name", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
          End If
        Next
      End If

      If Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count = 1 Then
        For i As Int16 = 0 To CShort(Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count - 1)
          If Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.Control_Caption).Trim = String.Empty Then
            MessageBox.Show("Invalid Control Caption entered to save the record", "Invalid Control Caption", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
          ElseIf CBool(Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.ReadonlyForExistingRecord)) = False _
            And CBool(Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.ReadonlyForNewRecord)) = False _
            And CBool(Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.Mandatory)) = False Then
            MessageBox.Show("Atleast one option must be checked", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
          End If
        Next
      Else
        For i As Int16 = 0 To CShort(Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count - 2)
          If Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.Control_Caption).Trim = String.Empty Then
            MessageBox.Show("Invalid Control Caption entered to save the record", "Invalid Control Caption", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
          ElseIf CBool(Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.ReadonlyForExistingRecord)) = False _
            And CBool(Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.ReadonlyForNewRecord)) = False _
            And CBool(Me.FormControlSettingQuickSpread.ActiveSheet.GetText(i, ControlSettingEnum.Mandatory)) = False Then
            MessageBox.Show("Atleast one option must be checked", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
          End If
        Next
      End If
      Return True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
    Return True
  End Function
  Private Function SaveRecord() As Boolean
    Try
      Dim Count As Int16
      Me._ControlSettingCompanyRowscount = False
      Me.FormControlSettingQuickSpread.Update()

      If IsValid() = True Then
        Me.FormControlSettingQuickSpread.EditMode = False
        Me._CombineControlSettingDataTable.Rows.RemoveAt(_CombineControlSettingDataTable.Rows.Count - 1)

        If Me._FormSettingRow Is Nothing Then
          'Insert Row  Base From setting
          Me._FormSettingRow = Me._FormSettingDataTable.NewSettingFormRow
          Me.FormIDTextBox.Text = CStr(Me._FormSettingTableAdapter.GetByFormID)
          _FormSettingRow.Form_ID = CShort(Me.FormIDTextBox.Text)
          _FormSettingRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
          ' Insert Base Form Setting company Association


          'Update Base_SettingForm_company_Association DB TAble
          Me._FormSettingCompanyRow = Me._FormSettingCompanyDataTable.NewSettingFormCompanyAssociationRow
          _FormSettingCompanyRow.Co_ID = CShort(Me._CompanyID)
          _FormSettingCompanyRow.Form_ID = CShort(Me.FormIDTextBox.Text)
          _FormSettingCompanyRow.RecordStatus_ID = Constants.RecordStatuses.Inserted

        Else
          'Base Form Setting
          If Me._FormSettingRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            Me._FormSettingRow.RecordStatus_ID = Constants.RecordStatuses.Updated
          End If
          ' Base Form Setting company Association 
          If Me._FormSettingCompanyRow IsNot Nothing Then
            If Me._FormSettingCompanyRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
              Me._FormSettingCompanyRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            End If
          Else
            Me._FormSettingCompanyRow = Me._FormSettingCompanyDataTable.NewSettingFormCompanyAssociationRow
            _FormSettingCompanyRow.Co_ID = CShort(Me._CompanyID)
            _FormSettingCompanyRow.Form_ID = CShort(Me.FormIDTextBox.Text)
            _FormSettingCompanyRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
          End If

        End If
        ' Base Setting Form
        _FormSettingRow.Form_Code = Me.FormCodeTextBox.Text.Trim
        _FormSettingRow.Form_Name = Me.FormNameTextBox.Text.Trim
        _FormSettingRow.Stamp_UserID = Me.LoginInfoObject.UserID
        _FormSettingRow.Stamp_DateTime = Date.Now
        _FormSettingRow.Upload_DateTime = Date.Now

        ' Base Form Setting company Association 
        _FormSettingCompanyRow.Form_Caption = Me.FormCaptionTextBox.Text.Trim
        _FormSettingCompanyRow.Stamp_UserID = Me.LoginInfoObject.UserID
        _FormSettingCompanyRow.Stamp_DateTime = Date.Now
        _FormSettingCompanyRow.Upload_DateTime = Date.Now

        If Me._FormSettingRow.RowState = DataRowState.Detached Then
          Me._FormSettingDataTable.Rows.Add(Me._FormSettingRow)
        End If
        Me._FormSettingTableAdapter.Update(Me._FormSettingDataTable)


        If Me._FormSettingCompanyRow.RowState = DataRowState.Detached Then
          Me._FormSettingCompanyDataTable.Rows.Add(Me._FormSettingCompanyRow)
        End If
        Me._FormSettingCompanyTableAdapter.Update(Me._FormSettingCompanyDataTable)


        ' Grid Data Inserted or Updated

        For I As Int32 = 0 To Me._CombineControlSettingDataTable.Rows.Count - 1
          ' MsgBox(Me._CombineControlSettingDataTable.Rows(I).Item("control_name"))
          If Me._CombineControlSettingDataTable.Rows(I).RowState <> DataRowState.Deleted Then
            '  MsgBox(Me._CombineControlSettingDataTable.Rows(I).Item("control_Name", DataRowVersion.Original))
            ' MsgBox(Me._CombineControlSettingDataTable.Rows(I).Item("control_Name", DataRowVersion.Current))

            If Me._CombineControlSettingDataTable.Rows(I).HasVersion(DataRowVersion.Original) = False And Me._CombineControlSettingDataTable.Rows(I).Item("Control_Name", DataRowVersion.Current).ToString <> String.Empty Then
              Me._ControlSettingRow = Me._ControlSettingDataTable.NewSettingFormControlsRow
              Me._ControlSettingRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
              Me._ControlSettingRow.Form_ID = CShort(Me.FormIDTextBox.Text)
              Me._ControlSettingRow.Control_ID = CShort(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Control_ID))
              Me._ControlSettingRow.Control_Name = CStr(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Control_Name))
              Me._ControlSettingRow.Stamp_UserID = Me.LoginInfoObject.UserID
              Me._ControlSettingRow.Stamp_DateTime = Now.Date
              Me._ControlSettingRow.Upload_DateTime = Now.Date
              Me._ControlSettingDataTable.Rows.Add(Me._ControlSettingRow)
            ElseIf Me._CombineControlSettingDataTable.Rows(I).Item("Control_Name", DataRowVersion.Current).ToString <> Me._CombineControlSettingDataTable.Rows(I).Item("Control_Name", DataRowVersion.Original).ToString Then
              With Me._ControlSettingDataTable.Rows(I)
                .Item(ControlSettingEnum.RecordStatus_ID) = Constants.RecordStatuses.Updated

                .Item(ControlSettingEnum.Form_ID) = CShort(Me.FormIDTextBox.Text)
                .Item(ControlSettingEnum.Control_ID) = CShort(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Control_ID))
                .Item("Control_Name") = CStr(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Control_Name))

                .Item(ControlSettingEnum.Stamp_UserID) = Me.LoginInfoObject.UserID
                .Item("Stamp_DateTime") = Now.Date
                .Item("Upload_DateTime") = Now.Date
              End With
            End If

            If Me._ControlSettingCompanyDataTable.Rows.Count = 0 Then
              Me._ControlSettingCompanyRowscount = True
            End If

            Count = CShort(Me._ControlSettingCompanyTableAdapter.GetControlSettingCompanyRows(CInt(Me._CompanyID), CInt(Me._ControlSettingDataTable.Rows(I).Item("form_ID")), CInt(Me._ControlSettingDataTable.Rows(I).Item("Control_ID"))))
            If Count = 0 Then
              Me._ControlSettingCompanyRowscount = True
            End If
            If (Me._CombineControlSettingDataTable.Rows(I).HasVersion(DataRowVersion.Original) = False And Me._CombineControlSettingDataTable.Rows(I).Item("Control_Caption", DataRowVersion.Current).ToString <> String.Empty) Or Me._ControlSettingCompanyRowscount = True Then
              Me._ControlSettingCompanyRow = Me._ControlSettingCompanyDataTable.NewSettingFormControlsCompanyAssociationRow
              Me._ControlSettingCompanyRow.Co_ID = CShort(Me._CompanyID)
              Me._ControlSettingCompanyRow.Form_ID = CShort(Me.FormIDTextBox.Text)
              Me._ControlSettingCompanyRow.Control_ID = CShort(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Control_ID))
              Me._ControlSettingCompanyRow.Control_Caption = CStr(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Control_Caption))
              Me._ControlSettingCompanyRow.ReadonlyForNewRecord = CBool(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.ReadonlyForNewRecord))
              Me._ControlSettingCompanyRow.ReadonlyForExistingRecord = CBool(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.ReadonlyForExistingRecord))
              Me._ControlSettingCompanyRow.Mandatory = CBool(Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Mandatory))
              Me._ControlSettingCompanyRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
              Me._ControlSettingCompanyRow.Stamp_UserID = Me.LoginInfoObject.UserID
              Me._ControlSettingCompanyRow.Stamp_DateTime = Now.Date
              Me._ControlSettingCompanyRow.Upload_DateTime = Now.Date
              Me._ControlSettingCompanyDataTable.Rows.Add(Me._ControlSettingCompanyRow)

            ElseIf (Me._CombineControlSettingDataTable.Rows(I).Item("Control_Caption", DataRowVersion.Current).ToString <> Me._CombineControlSettingDataTable.Rows(I).Item("Control_Caption", DataRowVersion.Original).ToString) Or (Me._CombineControlSettingDataTable.Rows(I).Item("ReadOnlyForNewRecord", DataRowVersion.Current).ToString <> Me._CombineControlSettingDataTable.Rows(I).Item("ReadOnlyForNewRecord", DataRowVersion.Original).ToString) Or _
                   (Me._CombineControlSettingDataTable.Rows(I).Item("ReadOnlyForExistingRecord", DataRowVersion.Current).ToString <> Me._CombineControlSettingDataTable.Rows(I).Item("ReadOnlyForExistingRecord", DataRowVersion.Original).ToString) Or (Me._CombineControlSettingDataTable.Rows(I).Item("Mandatory", DataRowVersion.Current).ToString <> Me._CombineControlSettingDataTable.Rows(I).Item("Mandatory", DataRowVersion.Original).ToString) Then

              With Me._ControlSettingCompanyDataTable.Rows(I)
                .Item("Control_Caption") = Me._CombineControlSettingDataTable.Rows(I).Item("Control_Caption")
                .Item("ReadOnlyForNewRecord") = Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.ReadonlyForNewRecord)
                .Item("ReadonlyForExistingRecord") = Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.ReadonlyForExistingRecord)
                .Item("Mandatory") = Me._CombineControlSettingDataTable.Rows(I).Item(ControlSettingEnum.Mandatory)
                .Item("RecordStatus_ID") = Constants.RecordStatuses.Updated
                .Item("Stamp_UserID") = Me.LoginInfoObject.UserID
                .Item("Stamp_DateTime") = Now.Date
                .Item("Upload_DateTime") = Now.Date
              End With
            End If
            '  Me._ControlSettingCompanyTableAdapter.Update(Me._ControlSettingCompanyDataTable)
          Else
            Me._ControlSettingDataTable.Rows(I).Item("RecordStatus_ID") = Constants.RecordStatuses.Deleted
            Me._ControlSettingCompanyDataTable.Rows(I).Item("RecordStatus_ID") = Constants.RecordStatuses.Deleted
          End If

          Me._ControlSettingTableAdapter.Update(Me._ControlSettingDataTable)
          Me._ControlSettingCompanyTableAdapter.Update(Me._ControlSettingCompanyDataTable)

          If Me._CombineControlSettingDataTable.Rows(I).RowState <> DataRowState.Deleted Then
            Me._CombineControlSettingDataTable.Rows(I).AcceptChanges()
          End If

          '  If Me._ControlSettingDataTable.Rows(I).RowState = DataRowState.Added Then
          '    ' Me._ControlSettingDataTable.Rows(I).SetModified()
          '    Me._ControlSettingDataTable.Rows(I).AcceptChanges()

          '  End If
          '  If Me._ControlSettingCompanyDataTable.Rows(I).RowState = DataRowState.Added Then
          '    'Me._ControlSettingCompanyDataTable.Rows(I).SetModified()
          '    Me._ControlSettingCompanyDataTable.Rows(I).AcceptChanges()
          '  End If
          'Next

          'For I As Int32 = 0 To Me._CombineControlSettingDataTable.Rows.Count - 1
          '  If Me._CombineControlSettingDataTable.Rows(I).RowState = DataRowState.Added Then
          '    Me._CombineControlSettingDataTable.Rows(I).SetModified()
          '  End If
        Next
        'Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetByFormIDDisplay(CInt(Me.FormIDTextBox.Text))
        'Me.ShowRecord()

        Return True
      Else
        '  Me.AddRow()
        Return False
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveRecord method of Form Setting", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function
  Protected Overrides Function ShowRecord() As Boolean
    Try
      'Me._ControlId = 0
      Me._Flag = True
      If Me._FormSettingDataTable.Rows.Count > 0 Then
        Me.ClearControls(Me)
        Me._FormSettingRow = Me._FormSettingDataTable(Me.CurrentRecordIndex)
        Me.FormIDTextBox.Text = CStr(Me._FormSettingRow.Form_ID)
        Me.FormCodeTextBox.Text = CStr(Me._FormSettingRow.Form_Code)
        Me.FormNameTextBox.Text = Me._FormSettingRow.Form_Name
        Me._FormSettingCompanyDataTable = Me._FormSettingCompanyTableAdapter.GetFirstByCoIDFormID(Me._CompanyID, CInt(Me.FormIDTextBox.Text))
        If Me._FormSettingCompanyDataTable.Rows.Count > 0 Then
          Me._FormSettingCompanyRow = Me._FormSettingCompanyDataTable(Me.CurrentRecordIndex)
          Me.FormCaptionTextBox.Text = Me._FormSettingCompanyRow.Form_Caption
        End If
      End If
      _ControlSettingCompanyRowscount = False
      Me.PopulateFormControlSettingGrid(Me._CompanyID, CInt(Me.FormIDTextBox.Text))
      Me._ControlSettingDataTable = Me._ControlSettingTableAdapter.GetAllByFormID(CInt(Me.FormIDTextBox.Text))
      Me._ControlSettingCompanyDataTable = Me._ControlSettingCompanyTableAdapter.GetbyCoIDFormsControlSetting(Me._CompanyID, CInt(Me.FormIDTextBox.Text))

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of FormSetting.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function


#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetFirstByFormID
      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick method of FormSetting.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      ' Me._ControlId = 0
      Cursor = Cursors.WaitCursor
      If (Me._FormSettingRow Is Nothing) Then
        Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetFirstByFormID
      Else
        Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetNextByCoIDFormID(CInt(Me.FormIDTextBox.Text))
        If Me._FormSettingDataTable.Count = 0 Then
          Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetLastByFormID
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick method of FormSetting.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If (Me._FormSettingRow Is Nothing) Then
        Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetPreviousByCoIDFormID(0)
        Exit Sub
      Else
        Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetPreviousByCoIDFormID(CInt(Me.FormIDTextBox.Text))
      End If

      MyBase.MoveNextButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick method of FormSetting.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._FormSettingDataTable = Me._FormSettingTableAdapter.GetLastByFormID

      MyBase.MoveLastButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick method of FormSetting.", ex)
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
        Me.AddRow()
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick method of FormSetting.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      _ControlSettingCompanyRowscount = False
      Me._Flag = False
      Me.FormCodeTextBox.Text = String.Empty
      Me.FormNameTextBox.Text = String.Empty
      Me.FormCaptionTextBox.Text = String.Empty

      Me._FormSettingRow = Nothing
      Me._FormSettingDataTable.Rows.Clear()
      Me._FormSettingCompanyRow = Nothing
      Me._FormSettingCompanyDataTable.Rows.Clear()
      Me._ControlSettingRow = Nothing
      Me._ControlSettingDataTable.Rows.Clear()
      Me._ControlSettingCompanyRow = Nothing
      Me._ControlSettingCompanyDataTable.Rows.Clear()
      Me._CombineControlSettingDataTable.Rows.Clear()


      MyBase.CancelButtonClick(sender, e)
      Me.FormIDTextBox.Text = CStr(0)
      Me._ControlID = 0
      Me.PopulateFormControlSettingGrid(-1, -1)

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

      Me.FormControlSettingQuickSpread.EditMode = False

      If Me.FormIDTextBox.Text = String.Empty Then
        MessageBox.Show("Invalid Record Selected for deletion", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.FormControlSettingQuickSpread.Focus()
        Exit Sub

      ElseIf Me.FormCodeTextBox.Text.Trim = String.Empty Then
        MessageBox.Show("Your must have the Form code to Delete the record", "Invalid Form Code", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.FormCodeTextBox.Focus()
      ElseIf Me.FormNameTextBox.Text.Trim = String.Empty Then
        MessageBox.Show("Your must have valid the Form name to Delete the record", "Invalid Form Name", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.FormNameTextBox.Focus()
        Exit Sub
      ElseIf Me.FormCaptionTextBox.Text.Trim = String.Empty Then
        MessageBox.Show("Your must have valid Form caption to Delete the record", "Invalid Form Caption", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.FormNameTextBox.Focus()
        Exit Sub
      ElseIf Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count < 1 Then
        MessageBox.Show("There must be atleast one control display to Delete the record", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.FormControlSettingQuickSpread.Focus()
        Exit Sub
      End If



      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._FormSettingRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        Me._FormSettingCompanyRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
        Me._FormSettingTableAdapter.Update(Me._FormSettingDataTable)
        Me._FormSettingCompanyTableAdapter.Update(Me._FormSettingCompanyDataTable)
        Me._FormSettingRow = Nothing
        Me._FormSettingCompanyRow = Nothing
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
        Me._ControlID = 0
        _ControlSettingCompanyRowscount = False
        Me._Flag = False
      Else
      End If


    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick method of VoucherForm.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
#End Region



  Private Sub FormControlSettingQuickSpread_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormControlSettingQuickSpread.EditModeOff
    If Me.FormControlSettingQuickSpread.ActiveSheet Is Nothing OrElse Me.FormControlSettingQuickSpread.ActiveSheet.ActiveCell Is Nothing Then Exit Sub

    If (Me.FormControlSettingQuickSpread.ActiveSheet.GetText(Me.FormControlSettingQuickSpread.ActiveSheet.ActiveRowIndex, ControlSettingEnum.Control_Name) <> String.Empty) Or (Me.FormControlSettingQuickSpread.ActiveSheet.GetText(Me.FormControlSettingQuickSpread.ActiveSheet.ActiveRowIndex, ControlSettingEnum.Control_Caption) <> String.Empty) Then
      If Me.FormControlSettingQuickSpread.ActiveSheet.ActiveRow.Index = Me.FormControlSettingQuickSpread.ActiveSheet.Rows.Count - 1 Then
        AddRow()
      End If
    End If

  End Sub

End Class



