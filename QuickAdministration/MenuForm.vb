Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickERP
Imports QuickLibrary.Constants
Imports QuickLibrary.Common
Imports QuickDAL.QuickSecurityDataSetTableAdapters
Imports QuickDAL.QuickSecurityDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet

Public Class MenuSetting


#Region "Declarations"
    Private _MenuTableAdapter As New MenuTableAdapter
    Private _MenuDataTable As New MenuDataTable
    Private _CurrentMenuDataRow As MenuRow
    Private _SelectedNodeMenuDataRow As MenuRow

    Private _FormSettingTableAdapter As New SettingFormTableAdapter
    Private _FormSettingDataTable As New SettingFormDataTable

  Private _CurrentNodeMenuID As Integer


  Private Enum MenuEnum
    Menu_ID
    Menu_Desc
    Display_Order
    Parent_Menu_Id
    Stamp_Userid
    Stamp_DateTime
    Upload_DateTime
    RecordStatus_ID
    Form_Code
    Form_ID
  End Enum


#End Region

#Region "Properties"

#End Region

#Region "Methods"

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

  Private Sub LoadTreeView()
    Try
      'Dim tvRoot As TreeNode
      Dim tvNode() As TreeNode
      Me.MenuTreeView.Nodes.Clear()

      Me._MenuDataTable = Me._MenuTableAdapter.GetByParentMenuIDByDisplayOrder
      For I As Int32 = 0 To Me._MenuDataTable.Rows.Count - 1
        With Me._MenuDataTable.Rows(I)
          If CInt(.Item(MenuEnum.Parent_Menu_Id)) = 0 Then
            Me.MenuTreeView.Nodes.Add(.Item(MenuEnum.Menu_ID).ToString, (Replace(.Item(MenuEnum.Menu_Desc).ToString, "&", "")))
          ElseIf CInt(.Item(MenuEnum.Parent_Menu_Id)) <> 0 Then
            tvNode = Me.MenuTreeView.Nodes.Find(.Item(MenuEnum.Parent_Menu_Id).ToString, True)
            tvNode(0).Nodes.Add(.Item(MenuEnum.Menu_ID).ToString, Replace(.Item(MenuEnum.Menu_Desc).ToString, "&", ""))
          End If
        End With
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the Load TreeView (", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Sub


    Private Function PopulateFormSettingComboBox() As Boolean
    Try
      Me.FormCodeComboBox.DataSource = Me._FormSettingTableAdapter.GetAll
      Me.FormCodeComboBox.ValueMember = Me._FormSettingDataTable.Form_IDColumn.ColumnName
      Me.FormCodeComboBox.DisplayMember = Me._FormSettingDataTable.Form_NameColumn.ColumnName


      With FormCodeComboBox.DisplayLayout.Bands(0)
        For i As Int32 = 0 To .Columns.Count - 1
          If .Columns(Me._FormSettingDataTable.Form_NameColumn.ColumnName).Index <> .Columns(i).Index Then
            FormCodeComboBox.DisplayLayout.Bands(0).Columns(i).Hidden = True
          End If
        Next
      End With
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to the Populate Form Setting ComboBox(", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally

    End Try
    End Function




  Private Function IsValid() As Boolean
    Try
      If Me.MenuDescriptionTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the menu description to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.MenuDescriptionTextBox.Focus()
        Return False
      ElseIf Me.FormCodeComboBox.SelectedRow Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the Form to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.FormCodeComboBox.Focus()
        Return False
      End If
      Return True




    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      ' QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      If Me._MenuDataTable.Rows.Count > 0 Then
                Me._CurrentMenuDataRow = CType(Me._MenuDataTable.Rows(Me.CurrentRecordIndex), MenuRow)
        Me._SelectedNodeMenuDataRow = Nothing
        Me._SelectedNodeMenuDataRow = Me._CurrentMenuDataRow
        Me.ClearControls(Me)
        Me.MenuIDTextBox.Text = CStr(Me._CurrentMenuDataRow.Menu_Id)
        Me._CurrentNodeMenuID = Me._CurrentMenuDataRow.Menu_Id
        Me.MenuDescriptionTextBox.Text = Replace(Me._CurrentMenuDataRow.Menu_Desc, "&", "")
        Me.MenuDisplayTextBox.Text = CStr(Me._CurrentMenuDataRow.Display_Order)
        Me.FormCodeComboBox.Value = CStr(Me._CurrentMenuDataRow.Form_ID)
        Me.ParentMenuIDTextBox.Text = CStr(Me._CurrentMenuDataRow.Parent_Menu_Id)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in ShowRecord method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    End Try
  End Function


  Private Function SaveRecord() As Boolean
    Try
      If IsValid() Then
        If Me._CurrentMenuDataRow Is Nothing Then
          Me._CurrentMenuDataRow = Me._MenuDataTable.NewMenuRow
          Me.MenuIDTextBox.Text = CStr(Me._MenuTableAdapter.GetNewByMenuID)
          Me._CurrentMenuDataRow.Menu_Id = CInt(Me.MenuIDTextBox.Text)
          Me._CurrentMenuDataRow.Display_Order = CShort(Me._MenuTableAdapter.GetNewDisplayOrderByParentMenuID(Me._SelectedNodeMenuDataRow.Parent_Menu_Id))
          Me._CurrentMenuDataRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
          Me._CurrentMenuDataRow.Parent_Menu_Id = Me._SelectedNodeMenuDataRow.Parent_Menu_Id
        Else
          Me._CurrentMenuDataRow.Parent_Menu_Id = CInt(Me.ParentMenuIDTextBox.Text)
          If Me._CurrentMenuDataRow.RecordStatus_ID <> Constants.RecordStatuses.Deleted Then
            Me._CurrentMenuDataRow.RecordStatus_ID = 2
          End If
        End If

        Me._CurrentMenuDataRow.Menu_Desc = Me.MenuDescriptionTextBox.Text
        Me._CurrentMenuDataRow.Form_ID = CShort(Me.FormCodeComboBox.Value)
        Me._CurrentMenuDataRow.Stamp_UserId = LoginInfoObject.UserID
        Me._CurrentMenuDataRow.Stamp_DateTime = Common.SystemDateTime
        Me._CurrentMenuDataRow.Stamp_DateTime = Common.SystemDateTime

        If _CurrentMenuDataRow.RowState = DataRowState.Detached Then
          Me._MenuDataTable.Rows.Add(_CurrentMenuDataRow)
        End If
        Me._MenuTableAdapter.Update(Me._MenuDataTable)

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

  Private Sub MenuSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Cursor = Cursors.WaitCursor
      Me.MenuIDTextBox.Text = Nothing
      Me.MenuDescriptionTextBox.Text = Nothing
      Me.MenuDescriptionTextBox.MaxLength = Me._MenuDataTable.Menu_DescColumn.MaxLength
      PopulateFormSettingComboBox()
      LoadTreeView()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in Loading Form of Menu Form", ex)
      '   QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub
  Private Sub MenuTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles MenuTreeView.AfterSelect
    Try
      Cursor = Cursors.WaitCursor
      If Me.MenuTreeView.SelectedNode IsNot Nothing Then

        Me._MenuDataTable.Rows.Clear()
        Me._MenuDataTable = Me._MenuTableAdapter.GetSelectedTreeNodeByMenuId(CInt(Me.MenuTreeView.SelectedNode.Name))
        If Me._MenuDataTable.Rows.Count > 0 Then
          Me.ShowRecord()
        End If
      End If
      
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MenuTreeView_AfterSelect event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Private Sub DownArrow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownArrow.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim _LastSelectedDisplayOrder As Int32
      Dim _MenuID As Int32
      Dim _CurrentSelectedDisplayOrder As Int32
            Dim _MenuSelectedDataTable As New MenuDataTable
      Dim tvNode() As TreeNode

      _LastSelectedDisplayOrder = Me._CurrentMenuDataRow.Display_Order
      _MenuID = Me._CurrentMenuDataRow.Menu_Id

      If Me._CurrentMenuDataRow.Display_Order <> CInt(Me._MenuTableAdapter.GetMaxByParentMenuID(Me._CurrentMenuDataRow.Parent_Menu_Id)) Then

        For i As Int16 = 0 To 2
          If i = 0 Then
            Me._CurrentMenuDataRow.Display_Order = -1
          ElseIf i = 1 Then
            Me.MenuTreeView.SelectedNode = Me.MenuTreeView.SelectedNode.NextNode
            Me.MenuTreeView.Select()
            _CurrentSelectedDisplayOrder = Me._CurrentMenuDataRow.Display_Order

            Me._CurrentMenuDataRow.Display_Order = CShort(_LastSelectedDisplayOrder)
          Else

            Me._MenuDataTable.Rows.Clear()
            _MenuDataTable = Me._MenuTableAdapter.GetByMenuID(_MenuID)
            Me._MenuDataTable.Rows(0).Item(MenuEnum.Display_Order) = CShort(_CurrentSelectedDisplayOrder)
            _MenuID = CInt(Me._MenuDataTable.Rows(0).Item(MenuEnum.Menu_ID))

          End If

          If Me._MenuDataTable.Rows(0).RowState = DataRowState.Modified Then
            Me._MenuTableAdapter.Update(Me._MenuDataTable)
          End If
        Next
        Me.LoadTreeView()

        tvNode = Me.MenuTreeView.Nodes.Find(CStr(_MenuID), True)
        Me.MenuTreeView.SelectedNode = tvNode(0)
      End If
      '


    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DownArrow_Click event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub UpArrow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpArrow.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim _LastSelectedDisplayOrder As Int32
      Dim _MenuID As Int32
      Dim _CurrentSelectedDisplayOrder As Int32
            Dim _MenuSelectedDataTable As New MenuDataTable
      Dim tvNode() As TreeNode

      If Me.MenuTreeView.SelectedNode.Index <> 0 Then

        _LastSelectedDisplayOrder = Me._CurrentMenuDataRow.Display_Order
        _MenuID = Me._CurrentMenuDataRow.Menu_Id

        For i As Int16 = 0 To 2
          If i = 0 Then
            Me._CurrentMenuDataRow.Display_Order = -1
          ElseIf i = 1 Then
            Me.MenuTreeView.SelectedNode = Me.MenuTreeView.SelectedNode.PrevNode
            Me.MenuTreeView.Select()
            _CurrentSelectedDisplayOrder = Me._CurrentMenuDataRow.Display_Order
            Me._CurrentMenuDataRow.Display_Order = CShort(_LastSelectedDisplayOrder)
          Else
            Me._MenuDataTable.Rows.Clear()
            _MenuDataTable = Me._MenuTableAdapter.GetByMenuID(_MenuID)
            Me._MenuDataTable.Rows(0).Item(MenuEnum.Display_Order) = CShort(_CurrentSelectedDisplayOrder)
            _MenuID = CInt(Me._MenuDataTable.Rows(0).Item(MenuEnum.Menu_ID))
          End If
          If Me._MenuDataTable.Rows(0).RowState = DataRowState.Modified Then
            Me._MenuTableAdapter.Update(Me._MenuDataTable)
          End If
        Next
        Me.LoadTreeView()

        tvNode = Me.MenuTreeView.Nodes.Find(CStr(_MenuID), True)
        Me.MenuTreeView.SelectedNode = tvNode(0)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DownArrow_Click event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub

  Private Sub LeftArrowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftArrowButton.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim _MenuID As Int32
      Dim _ParentMenuID As Int32

      Dim tvNode() As TreeNode
      Dim _CurrentNodeDisplayOrder As Integer

      If Me._CurrentMenuDataRow.Parent_Menu_Id <> 0 Then
        _MenuID = Me._CurrentMenuDataRow.Menu_Id
        Me.MenuTreeView.SelectedNode = Me.MenuTreeView.SelectedNode.Parent
        Me.MenuTreeView.Select()
        _CurrentNodeDisplayOrder = Me._CurrentMenuDataRow.Display_Order

        Me._MenuDataTable = Me._MenuTableAdapter.GetByMenuID(_MenuID)
        Me._MenuDataTable.Rows(0).Item(MenuEnum.Display_Order) = -1
        Me._MenuDataTable.Rows(0).Item(MenuEnum.Parent_Menu_Id) = Me._CurrentMenuDataRow.Parent_Menu_Id
        _ParentMenuID = Me._CurrentMenuDataRow.Parent_Menu_Id
        If Me._MenuDataTable.Rows(0).RowState = DataRowState.Modified Then
          Me._MenuTableAdapter.Update(Me._MenuDataTable)
        End If

        Me._MenuDataTable.Rows.Clear()
        Me._MenuDataTable = Me._MenuTableAdapter.GetByParentMenuID(_ParentMenuID, CShort(_CurrentNodeDisplayOrder))
        For i As Int32 = 0 To Me._MenuDataTable.Rows.Count - 1
          Me._MenuDataTable.Rows(i).Item(MenuEnum.Display_Order) = 9000 + i
        Next
        Me._MenuTableAdapter.Update(Me._MenuDataTable)

        For i As Int32 = 0 To Me._MenuDataTable.Rows.Count - 1
          If CInt(Me._MenuDataTable.Rows(i).Item(MenuEnum.Display_Order).ToString) >= 9000 Then
            Me._MenuDataTable.Rows(i).Item(MenuEnum.Display_Order) = _CurrentNodeDisplayOrder + 1
            _CurrentNodeDisplayOrder = _CurrentNodeDisplayOrder + 1
          End If
        Next
        Me._MenuTableAdapter.Update(Me._MenuDataTable)

        Me.LoadTreeView()
        tvNode = Me.MenuTreeView.Nodes.Find(CStr(_MenuID), True)
        Me.MenuTreeView.SelectedNode = tvNode(0)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in LeftArrowButton_Click event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Private Sub RightArrowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightArrowButton.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim _MenuID As Int32
      Dim tvNode() As TreeNode
      Dim _SelectedNodeMenuID As Integer

      If Me._CurrentMenuDataRow IsNot Nothing Then
        _MenuID = Me._CurrentMenuDataRow.Menu_Id

        Me.MenuTreeView.SelectedNode = Me.MenuTreeView.SelectedNode.PrevNode
        Me.MenuTreeView.Select()

        _SelectedNodeMenuID = Me._CurrentMenuDataRow.Menu_Id

        Me._MenuDataTable = Me._MenuTableAdapter.GetByMenuID(_MenuID)
        If Me._MenuDataTable.Rows(0).Item(MenuEnum.Parent_Menu_Id).ToString = Me._CurrentMenuDataRow.Item(MenuEnum.Parent_Menu_Id).ToString Then
          Me._MenuDataTable.Rows(0).Item(MenuEnum.Parent_Menu_Id) = _SelectedNodeMenuID
          Me._MenuDataTable.Rows(0).Item(MenuEnum.Display_Order) = Me._MenuTableAdapter.GetMaxByParentMenuID(_SelectedNodeMenuID)
          Me._MenuTableAdapter.Update(Me._MenuDataTable)
        End If

        Me.LoadTreeView()
        tvNode = Me.MenuTreeView.Nodes.Find(CStr(_MenuID), True)
        Me.MenuTreeView.SelectedNode = tvNode(0)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in RightArrowButton_Click event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub



#End Region

#Region "ToolBar Methods"
  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try

      Cursor = Cursors.WaitCursor
      Me._MenuDataTable = Me._MenuTableAdapter.GetFirstByMenuID

      MyBase.MoveFirstButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveFirstButtonClick event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_CurrentMenuDataRow Is Nothing) Then
        Me._MenuDataTable = Me._MenuTableAdapter.GetFirstByMenuID
      Else
        _MenuDataTable = Me._MenuTableAdapter.GetNextByMenuID(CInt(Me.MenuIDTextBox.Text))
        If _MenuDataTable.Count = 0 Then
          Me._MenuDataTable = Me._MenuTableAdapter.GetLastByMenuID
        End If
      End If
      MyBase.MoveNextButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveNextButtonClick event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If (_CurrentMenuDataRow Is Nothing) Then
        Me._MenuDataTable = Me._MenuTableAdapter.GetPreviousByMenuID(0)
      Else
        Me._MenuDataTable = Me._MenuTableAdapter.GetPreviousByMenuID(CInt(Me.MenuIDTextBox.Text))
      End If

      MyBase.MovePreviousButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MovePreviousButtonClick event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      Me._MenuDataTable = Me._MenuTableAdapter.GetLastByMenuID
      MyBase.MoveFirstButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in MoveLastButtonClick event method of Menu Form.", ex)
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
        Me.LoadTreeView()
      Else
        QuickMessageBox.Show(LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in SaveButtonClick event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me.FormCodeComboBox.Text = String.Empty
      Me._CurrentMenuDataRow = Nothing

      MyBase.CancelButtonClick(sender, e)
      Me.MenuIDTextBox.Text = String.Empty

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in CancelButtonClick event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      '      Dim _TempTable As QuickAccountingDataSet.COADataTable'
      If Me.MenuIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select Invalid Record to delete the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Exit Sub
      End If

      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        Me._CurrentMenuDataRow.RecordStatus_ID = 4
        Me._CurrentMenuDataRow.Stamp_UserId = Me.LoginInfoObject.UserID
        Me._CurrentMenuDataRow.Stamp_DateTime = Date.Now

        Me._MenuTableAdapter.Update(Me._MenuDataTable)
        Me._CurrentMenuDataRow = Nothing
        Me.LoadTreeView()
        MyBase.DeleteButtonClick(sender, e)
        QuickMessageBox.Show(LoginInfoObject, "Record is successfully deleted.")
        Me.MenuDescriptionTextBox.Focus()
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in DeleteButtonClick event method of Menu Form.", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub


#End Region



 
 
  
 
 
  
End Class