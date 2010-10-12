Imports System.Windows.Forms

Public Class frmMaster
#Region "Declaration"
  Private Const KEY_MOVE_FIRST_BUTTON As String = "MoveFirstButton"
  Private Const KEY_MOVE_PREVIOUS_BUTTON As String = "MovePreviousButton"
  Private Const KEY_MOVE_NEXT_BUTTON As String = "MoveNextButton"
  Private Const KEY_MOVE_LAST_BUTTON As String = "MoveLastButton"
  Private Const KEY_NEW_BUTTON As String = "NewButton"
  Private Const KEY_SAVE_BUTTON As String = "SaveButton"
  Private Const KEY_CANCEL_BUTTON As String = "CancelButton"
  Private Const KEY_DELETE_BUTTON As String = "DeleteButton"
  Private Const KEY_REFRESH_BUTTON As String = "RefreshButton"
  Private Const KEY_PREVIEW_BUTTON As String = "PreviewButton"
  Private Const KEY_PRINT_BUTTON As String = "PrintButton"

  Private Shared _CacheObject As New Cache
  Private Shared _ActiveToolBar As ToolBars = ToolBars.Infragistics
  Private Shared Event ActiveToolBarChanged()
  Private _DocumentType As enuDocumentType

  Protected FormDataSet As New DataSet
  Protected _CurrentRecordDataRow As DataRow
  Protected _CurrentRecordIndex As Int32
  Private _CurrentToolbarMode As ToolbarModes

  Public Enum ToolBars
    Regular
    Infragistics
  End Enum

  Public Enum enuDocumentType
    SalesInvoice = 1
    SalesInvoiceReturn
    Purchase
    PurchaseReturn
    Item
    Party
    Branch
    User
    COA
    VoucherType
    Setting
  End Enum

  Public Enum ToolbarModes
    DataEntryWithoutNew
    DataEntryWithNew
    TransferFromFile
  End Enum

  Public Enum RecordChangeConfirmationResult
    ChangeWithSave
    ChangeWithoutSave
    DontChange
  End Enum
#End Region

#Region "Methods"
  Public Sub PreSave()
    Try

    Catch ex As Exception

    End Try
  End Sub

  Public Sub PostSave()
    Try
      ClearControls(Me)
    Catch ex As Exception

    End Try
  End Sub

  Protected Overridable Function RecordChangeConfirmation() As RecordChangeConfirmationResult
    Try
      Dim MessageBoxResult As Windows.Forms.DialogResult

      MessageBoxResult = MessageBox.Show("Do you want to save record?", "Save Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
      Select Case MessageBoxResult
        Case Windows.Forms.DialogResult.Yes
          Return RecordChangeConfirmationResult.ChangeWithSave
        Case Windows.Forms.DialogResult.No
          Return RecordChangeConfirmationResult.ChangeWithoutSave
        Case Windows.Forms.DialogResult.Cancel
          Return RecordChangeConfirmationResult.DontChange
        Case Else
          Return RecordChangeConfirmationResult.DontChange
      End Select

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception to confirm record change", ex)
      QuickExceptionObject.Show()
    End Try
  End Function

  Protected Overridable Function ShowRecord() As Boolean

  End Function
#End Region

#Region "Toolbar Methods"
  Protected Overridable Sub ClearControls(ByRef pControlObject As System.Windows.Forms.Control)
    Try
      If TypeOf pControlObject Is TextBox Then
        pControlObject.Text = ""
      ElseIf TypeOf pControlObject Is Infragistics.Win.UltraWinEditors.UltraNumericEditor Then
        CType(pControlObject, Infragistics.Win.UltraWinEditors.UltraNumericEditor).Value = 0
      ElseIf TypeOf pControlObject Is Infragistics.Win.UltraWinGrid.UltraCombo Then
        CType(pControlObject, Infragistics.Win.UltraWinGrid.UltraCombo).ActiveRow = Nothing
        CType(pControlObject, Infragistics.Win.UltraWinGrid.UltraCombo).SelectedRow = Nothing
      ElseIf TypeOf pControlObject Is FarPoint.Win.Spread.FpSpread Then
        CType(pControlObject, FarPoint.Win.Spread.FpSpread).Sheets(0).Rows.Clear()
      End If

      For Each ControlObject As System.Windows.Forms.Control In pControlObject.Controls
        ClearControls(ControlObject)
      Next
    Catch ex As Exception
      'Throw ex
    End Try
  End Sub

  Protected Overridable Sub SaveButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles tsplblSave.Click
    Try
      MessageBox.Show("Record(s) were saved successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
      Dim IsClearControlAllowed As Boolean = True

      If Me.CurrentRecordDataRow IsNot Nothing Then
        If Me.RecordChangeConfirmation = RecordChangeConfirmationResult.ChangeWithoutSave Then
          IsClearControlAllowed = True
        Else
          IsClearControlAllowed = False
        End If
      Else
        IsClearControlAllowed = True
      End If

      If IsClearControlAllowed Then
        Me.CurrentRecordIndex = 0
        Me.CurrentRecordDataRow = Nothing
        Me.ClearControls(Me)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception to cancel button click", ex)
      QuickExceptionObject.Show()
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
        Case KEY_PREVIEW_BUTTON
          PrintPreviewButtonClick(sender, e)
        Case KEY_PRINT_BUTTON
          PrintButtonClick(sender, e)
        Case Else
          'There is no code for this button.
      End Select
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

#End Region

#Region "Properties"

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

  Public Shared Property CacheObject() As Cache
    Get
      Return _CacheObject
    End Get
    Set(ByVal value As Cache)
      _CacheObject = value
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
      _CurrentToolbarMode = value
    End Set
  End Property
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ToolBarInfragistics.Tools(KEY_MOVE_FIRST_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MoveFirst
    Me.ToolBarInfragistics.Tools(KEY_MOVE_PREVIOUS_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MovePrevious
    Me.ToolBarInfragistics.Tools(KEY_MOVE_NEXT_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MoveNext
    Me.ToolBarInfragistics.Tools(KEY_MOVE_LAST_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.MoveLast
    Me.ToolBarInfragistics.Tools(KEY_NEW_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.NewRecord
    Me.ToolBarInfragistics.Tools(KEY_SAVE_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.SaveRecord
    Me.ToolBarInfragistics.Tools(KEY_CANCEL_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Cancel
    Me.ToolBarInfragistics.Tools(KEY_delete_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.DeleteRecord
    Me.ToolBarInfragistics.Tools(KEY_REFRESH_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Refresh
    Me.ToolBarInfragistics.Tools(KEY_PREVIEW_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.PrintPreview
    Me.ToolBarInfragistics.Tools(KEY_PRINT_BUTTON).SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Print

    Me.ToolBarInfragistics.Visible = True
    Me.ToolBarRegular.Visible = False
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
#End Region

End Class