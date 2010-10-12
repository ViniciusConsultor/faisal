Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDALLibrary
Imports QuickDALLibrary.DatabaseCache
Imports System.Windows.Forms

Public Class ParentToolbarForm

#Region "Declaration"
  Private Const KEY_CANCEL_BUTTON As String = "CancelButton"
  Private Const KEY_DELETE_BUTTON As String = "DeleteButton"
  Private Const KEY_MOVE_FIRST_BUTTON As String = "MoveFirstButton"
  Private Const KEY_MOVE_PREVIOUS_BUTTON As String = "MovePreviousButton"
  Private Const KEY_MOVE_NEXT_BUTTON As String = "MoveNextButton"
  Private Const KEY_MOVE_LAST_BUTTON As String = "MoveLastButton"
  Private Const KEY_NEW_BUTTON As String = "NewButton"
  Private Const KEY_OPEN_FILE_BUTTON As String = "OpenFileButton"
  Private Const KEY_PREVIEW_BUTTON As String = "PreviewButton"
  Private Const KEY_PRINT_BUTTON As String = "PrintButton"
  Private Const KEY_SEARCH_BUTTON As String = "SearchButton"
  Private Const KEY_FILTER_BUTTON As String = "FilterButton"
  Private Const KEY_FILTER_CLEAR_BUTTON As String = "FilterClearButton"
  Private Const KEY_REFRESH_BUTTON As String = "RefreshButton"
  Private Const KEY_SAVE_BUTTON As String = "SaveButton"
  Private Const KEY_PASTE_BUTTON As String = "Paste"

  Private Shared _ActiveToolBar As ToolBars = ToolBars.Infragistics
  Private Shared Event ActiveToolBarChanged()
  Private _DocumentType As enuDocumentType

  Protected FormDataSet As New DataSet
  Protected FormDataTable As DataTable
  Protected _CurrentRecordDataRow As DataRow
  Protected _CurrentRecordIndex As Int32
  Private _CurrentToolbarMode As ToolbarModes
  Private _WindowState As FormWindowState
  Private _WindowStateAssigned As Boolean = False
  Private _RecordChangeConfirmationResult As RecordChangeConfirmationResults

  Public Enum ToolBars
    Regular
    Infragistics
  End Enum

  Public Enum ToolbarModes
    DataEntryWithoutNew
    DataEntryWithNew
    TransferFromFile
    ReportCriteria
  End Enum

  Public Enum RecordChangeConfirmationResults
    ChangeWithSave
    ChangeWithoutSave
    DontChange
  End Enum

  Public Enum FormModes
    EditMode
    NewMode
    FilterMode
  End Enum
#End Region

#Region "Methods"
  Public Overridable Function PreSave() As Boolean
    Try

      Return True

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in PreSave function", ex)
      Throw _QuickException
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 12-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Write your save record code in this method. Return status otherwise there
  ''' won't be message after save.
  ''' </summary>
  Protected Overridable Function SaveRecord() As Boolean
    Try

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveRecord of ParentToolbarForm.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 14-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It has the code to display status of the save, override it for custom implementation.
  ''' </summary>
  Public Overridable Function PostSave() As Boolean
    Try
      Me.ApplyControlSecurity()

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in PostSave function", ex)
      Throw _QuickException
    End Try
  End Function


  Protected Overridable Function PreCancel() As Boolean
    Try
      If Me.CurrentRecordDataRow IsNot Nothing AndAlso Me.CurrentRecordDataRow.RowState <> DataRowState.Unchanged Then
        'This will show user confirmation message and store in property
        AskRecordChangeConfirmation()
      Else
        RecordChangeConfirmationResult = RecordChangeConfirmationResults.ChangeWithoutSave
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in PreCancel function", ex)
      Throw _QuickException
    End Try
  End Function

  Protected Overridable Function PostCancel() As Boolean
    Try
      If RecordChangeConfirmationResult <> RecordChangeConfirmationResults.DontChange Then
        Me.CurrentRecordIndex = 0
        Me.CurrentRecordDataRow = Nothing
        Me.ClearControls(Me)
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in PostCancel function", ex)
      Throw _QuickException
    End Try
  End Function

  Protected Overridable Sub AskRecordChangeConfirmation()
    Try
      Dim MessageBoxResult As Windows.Forms.DialogResult

      MessageBoxResult = MessageBox.Show("Do you want to save record?", "Save Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
      Select Case MessageBoxResult
        Case Windows.Forms.DialogResult.Yes
          RecordChangeConfirmationResult = RecordChangeConfirmationResults.ChangeWithSave
        Case Windows.Forms.DialogResult.No
          RecordChangeConfirmationResult = RecordChangeConfirmationResults.ChangeWithoutSave
        Case Windows.Forms.DialogResult.Cancel
          RecordChangeConfirmationResult = RecordChangeConfirmationResults.DontChange
        Case Else
          RecordChangeConfirmationResult = RecordChangeConfirmationResults.DontChange
      End Select

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to confirm record change", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2009
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal Saleem  16-Jan-10   New implementation is added for control locking if 
  '                           modification is not allowed.
  ''' <summary>
  ''' Child forms should call MyBase.ShowRecord after loading record so that parent
  ''' form can lock control if modification is not allowed.
  ''' </summary>
  Protected Overridable Function ShowRecord() As Boolean
    Try
      If Me.FormMode <> FormModes.FilterMode Then Me.FormMode = FormModes.EditMode
      ApplyControlSecurity()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ShowRecord of frmMaster.", ex)
      Throw _qex
    End Try
  End Function

#End Region

#Region "Toolbar Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 18-Jan-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will disable the control if current mode is modifying record and
  ''' modification is not allowed. Otherwise it will enable the control.
  ''' </summary>
  Private Sub ApplyControlSecurity()
    Try
      Dim _ControlsStack As New System.Collections.Stack

      For Each _Control As Control In Me.Controls
        _ControlsStack.Push(_Control)
      Next

      Do While _ControlsStack.Count > 0

        'MessageBox.Show(CType(_ControlsStack.Peek, Control).Name)

        Dim _Control As Control = CType(_ControlsStack.Pop, Control)
        If Me.FormMode = FormModes.EditMode AndAlso GetSettingValue(SETTING_ID_ModificationAllowedPrefix & _Control.Name & SETTING_ID_SEPERATOR & Me.Name) = SETTING_VALUE_FALSE Then
          _Control.Enabled = False
        Else
          _Control.Enabled = True
        End If
        Debug.WriteLine(_Control.Text)
        'MessageBox.Show("Pop" & _Control.Name)

        'If _Control.Controls.Count > 0 Then
        For Each _ChildControl As Control In _Control.Controls
          _ControlsStack.Push(_ChildControl)
        Next
        'Else

        'End If

      Loop

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ApplyControlSecurity of frmMaster.", ex)
      Throw _qex
    End Try
  End Sub

  Protected Overridable Sub ClearControls(ByRef pControlObject As System.Windows.Forms.Control)
    Try
      If TypeOf pControlObject Is QuickControls.IClearControl Then
        DirectCast(pControlObject, QuickControls.IClearControl).ClearControl()

      ElseIf TypeOf pControlObject Is TextBox Then
        pControlObject.Text = ""

        'ElseIf TypeOf pControlObject Is QuickControls.Quick_Spread Then
        '  With DirectCast(pControlObject, QuickControls.Quick_Spread)
        '    If .DataSource IsNot Nothing AndAlso TypeOf .DataSource Is DataTable Then
        '      DirectCast(.DataSource, DataTable).Rows.Clear()
        '    Else
        '      .Sheets(0).Rows.Clear()
        '    End If
        '  End With

      ElseIf TypeOf pControlObject Is FarPoint.Win.Spread.FpSpread Then
        With CType(pControlObject, FarPoint.Win.Spread.FpSpread)
          If .DataSource IsNot Nothing AndAlso TypeOf .DataSource Is DataTable Then
            DirectCast(.DataSource, DataTable).Rows.Clear()
          Else
            .Sheets(0).Rows.Clear()
          End If
        End With

      End If

      For Each ControlObject As System.Windows.Forms.Control In pControlObject.Controls
        MyClass.ClearControls(ControlObject)
      Next

    Catch ex As Exception
      'Throw ex
    End Try
  End Sub

  Protected Overridable Sub SaveButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblSave.Click
    Try
      If PreSave() Then
        If SaveRecord() Then
          PostSave()
          QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
        End If
      End If
      PostSave()
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub MoveFirstButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblMoveFirst.Click
    Try
      'CurrentRecordIndex = 0
      Me.ShowRecord()

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub MovePreviousButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblMovePrevious.Click
    Try
      'CurrentRecordIndex -= 1
      Me.ShowRecord()

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub MoveNextButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblMoveNext.Click
    Try
      'CurrentRecordIndex += 1
      Me.ShowRecord()

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub MoveLastButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblMoveLast.Click
    Try
      'CurrentRecordIndex += 1
      Me.ShowRecord()

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub NewButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblNew.Click
    Try

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub CancelButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tspCancel.Click
    Try
      Me.FormMode = FormModes.NewMode
      ApplyControlSecurity()
      PreCancel()
      PostCancel()

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in cancel button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 11-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method is called when user clicks Search button.
  ''' </summary>
  Protected Overridable Sub SearchButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblSearch.Click
    Try

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SearchButtonClick of frmMaster.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 16-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method is called when user clicks Filter button on toolbar.
  ''' </summary>
  Protected Overridable Sub FilterButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblFilter.Click
    Try

      Me.FormMode = FormModes.FilterMode

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in FilterButtonClick of ParentToolbarForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 16-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method is called when user clicks Clear Filter button on toolbar.
  ''' </summary>
  Protected Overridable Sub FilterClearButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblFilterClear.Click
    Try

      Me.FormMode = FormModes.EditMode

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in FilterClearButtonClick of ParentToolbarForm.", ex)
      Throw _qex
    End Try
  End Sub

  Protected Overridable Sub RefreshButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblRefresh.Click
    Try

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub PrintPreviewButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblPrintPreview.Click
    Try

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub PrintButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblPrint.Click
    Try

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub DeleteButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tspbtnDelete.Click
    Try
      CancelButtonClick(sender, e)

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Protected Overridable Sub PasteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles PasteToolStipButton.Click
    Try

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in PasteButtonClick event method.", ex)
      Throw _QuickException
    End Try
  End Sub

  Protected Overridable Sub OpenFileButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsplblOpenFile.Click
    Try

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Private Sub ToolBarInfragistics_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBarInfragistics.ToolClick
    Try
      Select Case e.Tool.Key
        Case KEY_MOVE_FIRST_BUTTON
          MoveFirstButtonClick(sender, e)
        Case KEY_MOVE_PREVIOUS_BUTTON
          MovePreviousButtonClick(sender, e)
        Case KEY_MOVE_NEXT_BUTTON
          MoveNextButtonClick(sender, e)
        Case KEY_MOVE_LAST_BUTTON
          MoveLastButtonClick(sender, e)
        Case KEY_NEW_BUTTON
          NewButtonClick(sender, e)
        Case KEY_SAVE_BUTTON
          SaveButtonClick(sender, e)
        Case KEY_CANCEL_BUTTON
          CancelButtonClick(sender, e)
        Case KEY_DELETE_BUTTON
          DeleteButtonClick(sender, e)
        Case KEY_REFRESH_BUTTON
          RefreshButtonClick(sender, e)
        Case KEY_SEARCH_BUTTON
          SearchButtonClick(sender, e)
        Case KEY_FILTER_BUTTON
          FilterButtonClick(sender, e)
        Case KEY_FILTER_CLEAR_BUTTON
          FilterClearButtonClick(sender, e)
        Case KEY_PREVIEW_BUTTON
          PrintPreviewButtonClick(sender, e)
        Case KEY_PRINT_BUTTON
          PrintButtonClick(sender, e)
        Case KEY_OPEN_FILE_BUTTON
          OpenFileButtonClick(sender, e)
        Case KEY_PASTE_BUTTON
          PasteButtonClick(sender, e)

        Case Else
          'There is no code for this button.
      End Select

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ToolBarInfragistics_ToolClick event method of ParentToolbarForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region

#Region "Properties"

  Private _FormMode As FormModes
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 18-Jan-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This property determines current form mode.
  ''' </summary>
  Public Property FormMode() As FormModes
    Get
      Try

        Return _FormMode

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in FormMode of frmMaster.", ex)
        Throw _qex
      End Try
    End Get
    Private Set(ByVal value As FormModes)
      Try

        _FormMode = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in FormMode of frmMaster.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Public Shared Property ActiveToolbar() As ToolBars
    Get
      Return _ActiveToolBar

    End Get
    Set(ByVal value As ToolBars)
      _ActiveToolBar = value

      RaiseEvent ActiveToolBarChanged()
    End Set
  End Property

  Protected Overridable Property CurrentRecordDataRow() As DataRow
    Get
      Return _CurrentRecordDataRow

    End Get
    Set(ByVal value As DataRow)
      _CurrentRecordDataRow = value

    End Set
  End Property

  Protected Property CurrentRecordIndex() As Int32
    Get
      Return _CurrentRecordIndex

    End Get
    Set(ByVal value As Int32)
      _CurrentRecordIndex = value

    End Set
  End Property

  Public Property DocumentType() As enuDocumentType
    Get
      Return _DocumentType

    End Get
    Set(ByVal value As enuDocumentType)
      _DocumentType = value

    End Set
  End Property

  Public Property ToolbarMode() As ToolbarModes
    Get
      Return _CurrentToolbarMode
    End Get
    Set(ByVal value As ToolbarModes)
      Try
        _CurrentToolbarMode = value

        Select Case value
          Case ToolbarModes.DataEntryWithNew
            Me.ToolBarInfragistics.Tools(KEY_CANCEL_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_DELETE_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_LAST_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_NEW_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_OPEN_FILE_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_PREVIEW_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PRINT_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_REFRESH_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_SAVE_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PASTE_BUTTON).SharedProps.Visible = False
          Case ToolbarModes.DataEntryWithoutNew
            Me.ToolBarInfragistics.Tools(KEY_CANCEL_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_DELETE_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_MOVE_LAST_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_NEW_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_OPEN_FILE_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_PREVIEW_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PRINT_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_REFRESH_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_SAVE_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PASTE_BUTTON).SharedProps.Visible = False
          Case ToolbarModes.TransferFromFile
            Me.ToolBarInfragistics.Tools(KEY_CANCEL_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_DELETE_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_LAST_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_NEW_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_OPEN_FILE_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PREVIEW_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_PRINT_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_REFRESH_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_SAVE_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PASTE_BUTTON).SharedProps.Visible = True
          Case ToolbarModes.ReportCriteria
            Me.ToolBarInfragistics.Tools(KEY_CANCEL_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_DELETE_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_MOVE_LAST_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_NEW_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_OPEN_FILE_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_PREVIEW_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_PRINT_BUTTON).SharedProps.Visible = True
            Me.ToolBarInfragistics.Tools(KEY_REFRESH_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_SAVE_BUTTON).SharedProps.Visible = False
            Me.ToolBarInfragistics.Tools(KEY_PASTE_BUTTON).SharedProps.Visible = False
        End Select
      Catch ex As Exception
        Dim _ExceptionObject As New QuickException("Exception in setting toolbar mode.", ex)
        Throw _ExceptionObject
      End Try
    End Set
  End Property

  Public Property RecordChangeConfirmationResult() As RecordChangeConfirmationResults
    Get
      Return _RecordChangeConfirmationResult
    End Get
    Set(ByVal value As RecordChangeConfirmationResults)
      _RecordChangeConfirmationResult = value
    End Set
  End Property
#End Region

  Public Sub New()
    Try
      ' This call is required by the Windows Form Designer.
      InitializeComponent()

      ' Add any initialization after the InitializeComponent() call.

      'Below code throws exception when form is loaded in desing mode so this if condition is added.
      If QuickLibrary.Common.IsApplicationRunning Then
        With Me.ToolBarInfragistics
          .Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MoveFirst
          .Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MovePrevious
          .Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MoveNext
          .Tools(KEY_MOVE_LAST_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MoveLast
          .Tools(KEY_NEW_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.NewRecord
          .Tools(KEY_OPEN_FILE_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.OpenFile
          .Tools(KEY_SAVE_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.SaveRecord
          .Tools(KEY_CANCEL_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.cancel
          .Tools(KEY_DELETE_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.DeleteRecord
          .Tools(KEY_SEARCH_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Find
          .Tools(KEY_REFRESH_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Refresh
          .Tools(KEY_PREVIEW_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.PrintPreview
          .Tools(KEY_PRINT_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Print
          .Tools(KEY_PASTE_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Paste

          Dim _SettingValue As String

          Try
            _SettingValue = GetSettingValue(SETTING_ID_TOOLBAR_BUTTON_DISPLAY_STYLE_CANCEL & SETTING_ID_SEPERATOR & Me.Name)
            .Tools(KEY_CANCEL_BUTTON).SharedProps.Shortcut = Shortcut.F7
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_SHOW_IMAGE_ONLY Then
              .Tools(KEY_CANCEL_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            ElseIf _SettingValue = SETTING_VALUE_SHOW_TEXT_ONLY Then
              .Tools(KEY_CANCEL_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways
            ElseIf _SettingValue = SETTING_VALUE_SHOW_IMAGE_AND_TEXT Then
              .Tools(KEY_CANCEL_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText
            End If
            .Tools(KEY_CANCEL_BUTTON).SharedProps.ToolTipText = .Tools(KEY_CANCEL_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_CANCEL_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_DELETE_BUTTON & "showimageonly")
            .Tools(KEY_DELETE_BUTTON).SharedProps.Shortcut = Shortcut.F9
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_DELETE_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_DELETE_BUTTON).SharedProps.ToolTipText = .Tools(KEY_DELETE_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_DELETE_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_MOVE_FIRST_BUTTON & "showimageonly")
            .Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.Shortcut = Shortcut.F1
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.ToolTipText = .Tools(KEY_MOVE_FIRST_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_MOVE_FIRST_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_MOVE_LAST_BUTTON & "showimageonly")
            .Tools(KEY_MOVE_LAST_BUTTON).SharedProps.Shortcut = Shortcut.F4
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_MOVE_LAST_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_MOVE_LAST_BUTTON).SharedProps.ToolTipText = .Tools(KEY_MOVE_LAST_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_MOVE_LAST_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_MOVE_NEXT_BUTTON & "showimageonly")
            .Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.Shortcut = Shortcut.F3
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.ToolTipText = .Tools(KEY_MOVE_NEXT_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_MOVE_NEXT_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_MOVE_PREVIOUS_BUTTON & "showimageonly")
            .Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.Shortcut = Shortcut.F2
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.ToolTipText = .Tools(KEY_MOVE_PREVIOUS_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_MOVE_PREVIOUS_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_NEW_BUTTON & "showimageonly")
            .Tools(KEY_NEW_BUTTON).SharedProps.Shortcut = Shortcut.F5
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_NEW_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_NEW_BUTTON).SharedProps.ToolTipText = .Tools(KEY_NEW_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_NEW_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_OPEN_FILE_BUTTON & "showimageonly")
            .Tools(KEY_OPEN_FILE_BUTTON).SharedProps.Shortcut = Shortcut.CtrlO
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_OPEN_FILE_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_OPEN_FILE_BUTTON).SharedProps.ToolTipText = .Tools(KEY_OPEN_FILE_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_OPEN_FILE_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_PREVIEW_BUTTON & "showimageonly")
            .Tools(KEY_PREVIEW_BUTTON).SharedProps.Shortcut = Shortcut.F11
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_PREVIEW_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_PREVIEW_BUTTON).SharedProps.ToolTipText = .Tools(KEY_PREVIEW_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_PREVIEW_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_PRINT_BUTTON & "showimageonly")
            .Tools(KEY_PRINT_BUTTON).SharedProps.Shortcut = Shortcut.CtrlP
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_PRINT_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_PRINT_BUTTON).SharedProps.ToolTipText = .Tools(KEY_PRINT_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_PRINT_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_SEARCH_BUTTON & "showimageonly")
            .Tools(KEY_SEARCH_BUTTON).SharedProps.Shortcut = Shortcut.CtrlF
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_SEARCH_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_SEARCH_BUTTON).SharedProps.ToolTipText = .Tools(KEY_SEARCH_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_SEARCH_BUTTON).ShortcutResolved.ToString & "]"

            .Tools(KEY_REFRESH_BUTTON).SharedProps.Shortcut = Shortcut.CtrlF12
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_REFRESH_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If

            .Tools(KEY_REFRESH_BUTTON).SharedProps.ToolTipText = .Tools(KEY_REFRESH_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_REFRESH_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_SAVE_BUTTON & "showimageonly")
            .Tools(KEY_SAVE_BUTTON).SharedProps.Shortcut = Shortcut.F6
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_SAVE_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_SAVE_BUTTON).SharedProps.ToolTipText = .Tools(KEY_SAVE_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_SAVE_BUTTON).ShortcutResolved.ToString & "]"

            _SettingValue = GetSettingValue(Me.Name & SETTING_ID_SEPERATOR & KEY_PASTE_BUTTON & "showimageonly")
            .Tools(KEY_PASTE_BUTTON).SharedProps.Shortcut = Shortcut.CtrlV
            If _SettingValue Is Nothing OrElse _SettingValue = SETTING_VALUE_FALSE Then
              .Tools(KEY_PASTE_BUTTON).SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.Default
            End If
            .Tools(KEY_PASTE_BUTTON).SharedProps.ToolTipText = .Tools(KEY_PASTE_BUTTON).CaptionResolved _
            & "  [" & .Tools(KEY_PASTE_BUTTON).ShortcutResolved.ToString & "]"
          Catch ex As Exception
            MsgBox("error in fetching menu settings from database")
          End Try
        End With
      Else
        'Do nothing if application is not running
      End If

      ActiveToolbar = ToolBars.Infragistics
      Me.ToolbarMode = ToolbarModes.DataEntryWithoutNew
      Me.FormMode = FormModes.NewMode

    Catch ex As Exception
      Dim _ExceptionObject As New QuickException("Exception in instantiating the form", ex)
      Throw _ExceptionObject
    End Try
  End Sub

#Region "Events"
  Private Sub frmMaster_ActiveToolBarChanged() Handles Me.ActiveToolBarChanged
    Try
      Select Case ActiveToolbar
        Case ToolBars.Regular
          Me.ToolBarRegular.Visible = True
          Me.ToolBarInfragistics.Visible = False
        Case ToolBars.Infragistics
          Me.ToolBarRegular.Visible = False
          Me.ToolBarInfragistics.Visible = True
        Case Else
          MessageBox.Show("Invalid toolbar selection")
      End Select

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Private Sub frmMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      'Dim _DefaultWindowState As Int32

      'If Int32.TryParse(GetSettingValue(Constants.SETTING_ID_DefaultWindowState), _DefaultWindowState) Then
      '  Me.WindowState = DirectCast(_DefaultWindowState, FormWindowState)
      'Else
      '  Me.WindowState = FormWindowState.Maximized
      'End If

      '_WindowState = Me.WindowState
      'If Me.WindowState = FormWindowState.Maximized Then
      '  Me.WindowState = FormWindowState.Normal
      'End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in frmMaster_Load event method of frmMaster.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

  Private Sub frmMaster_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    Try
      If Not _WindowStateAssigned Then
        Me.WindowState = _WindowState
        _WindowStateAssigned = True
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in paint event of form", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

End Class