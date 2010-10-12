Imports System.Diagnostics
Imports System.Windows.Forms
Imports QuickLibrary
Imports QuickDAL
Imports QuickDalLibrary
Imports QuickDALLibrary.DatabaseCache
Imports QuickBaseForms
Imports Infragistics.Win.UltraWinToolbars

Public Class frmExplorer

    'Indicates if we are changing the selected node of the treeview programmatically
  Private ChangingSelectedNode As Boolean
  Private _LoginInfo As New LoginInfo

  Public ReadOnly Property LoginInfoObject() As LoginInfo
    Get
      Return _LoginInfo
    End Get
  End Property

#Region "New Entry"
  Private Sub LoadMenu(ByVal _MenuTool As Infragistics.Win.UltraWinToolbars.PopupMenuTool)
    ' TODO: Add code to add items to the treeview
    Dim _MenuTA As New QuickDAL.QuickERPTableAdapters.MenuTableAdapter
    Dim _MenuTable As QuickDAL.QuickERP.MenuDataTable

    If _MenuTool Is Nothing Then
      _MenuTable = _MenuTA.GetByParentMenuId(0)
    Else
      _MenuTable = _MenuTA.GetByParentMenuId(Convert.ToInt32(_MenuTool.Tag.ToString))
    End If

    For I As Int32 = 0 To _MenuTable.Rows.Count - 1
      Dim _checkTable As QuickDAL.QuickERP.MenuDataTable
      _checkTable = _MenuTA.GetByParentMenuId(_MenuTable(I).Menu_Id)

      Dim _NewTool As Infragistics.Win.UltraWinToolbars.ToolBase

      If _checkTable.Rows.Count > 0 Then
        _NewTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool(_MenuTable(I).Form_Id.ToString)
      Else
        'If there are no sub items then it should be button tool
        _NewTool = New Infragistics.Win.UltraWinToolbars.ButtonTool(_MenuTable(I).Form_Id.ToString)
      End If

      _NewTool.SharedProps.Caption = _MenuTable(I).Menu_Desc
      _NewTool.Tag = _MenuTable(I).Menu_Id

      Me.UltraToolbarsManager1.Tools.Add(_NewTool)

      If _MenuTool IsNot Nothing Then
        _MenuTool.Tools.AddTool(_NewTool.Key)
      Else
        Me.UltraToolbarsManager1.Toolbars(0).Tools.AddTool(_NewTool.Key)
      End If

      If _checkTable.Rows.Count > 0 Then
        LoadMenu(DirectCast(_NewTool, Infragistics.Win.UltraWinToolbars.PopupMenuTool))
      End If
    Next

    'Dim tvRoot As TreeNode
    'Dim tvNode As TreeNode

    'tvRoot = Me.TreeView.Nodes.Add(QuickLibrary.Entities.ERP_NAME, QuickLibrary.Entities.ERP_DESC)

    'tvNode = tvRoot.Nodes.Add(QuickLibrary.Entities.ERP_COMMON_NAME, QuickLibrary.Entities.ERP_COMMON_DESC)
    'tvNode = tvRoot.Nodes.Add(QuickLibrary.Entities.ERP_INV_NAME, QuickLibrary.Entities.ERP_INV_DESC)
    'tvNode = tvRoot.Nodes.Add(QuickLibrary.Entities.ERP_ACCOUNTS_NAME, QuickLibrary.Entities.ERP_ACCOUNTS_DESC)
    'tvNode = tvRoot.Nodes.Add(QuickLibrary.Entities.ERP_REPORTS_NAME, QuickLibrary.Entities.ERP_REPORTS_DESC)
    'tvNode = tvRoot.Nodes.Add(QuickLibrary.Entities.ERP_ADMINISTRATION_BRANCH_NAME, QuickLibrary.Entities.ERP_ADMINISTRATION_DESC)

  End Sub

  'Private Sub LoadDetail(ByVal strCategoryKey As String)
  '    ' TODO: Add code to add items to the listview based on the selected item in the treeview

  '    Dim lvItem As ListViewItem
  '    ListView.Items.Clear()

  '    Select Case strCategoryKey
  '        Case QuickLibrary.Entities.ERP_INV_NAME
  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_SALES_INVOICE_DESC)
  '    lvItem.Name = QuickLibrary.Entities.ERP_INV_SALES_INVOICE_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_SALES_INVOICE_POS_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_INV_SALES_INVOICE_POS_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_PURCHASE_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_INV_PURCHASE_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_PURCHASERETURN_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_INV_PURCHASERETURN_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_PurchaseOrder_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_INV_PurchaseOrder_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_ITEM_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_INV_ITEM_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            'lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_ITEM_BULK_ENTRY_DESC)
  '            'lvItem.Name = QuickLibrary.Entities.ERP_INV_ITEM_BULK_ENTRY_NAME
  '            'lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_INV_MINIMUM_STOCK_LEVEL_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_INV_MINIMUM_STOCK_LEVEL
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})
  '        Case QuickLibrary.Entities.ERP_COMMON_NAME
  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_COMMON_PARTY_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_COMMON_PARTY_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            'lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_COMMON_PARTY_TEBULAR_DESC)
  '            'lvItem.Name = QuickLibrary.Entities.ERP_COMMON_PARTY_TEBULAR_NAME
  '            'lvItem.SubItems.AddRange(New String() {"1.0.0.0"})
  '        Case QuickLibrary.Entities.ERP_ADMINISTRATION_NAME
  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_ADMINISTRATION_TRANSFER_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_ADMINISTRATION_TRANSFER_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_ADMINISTRATION_BRANCH_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_ADMINISTRATION_BRANCH_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_ADMINISTRATION_User_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_ADMINISTRATION_User_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_ADMINISTRATION_BULK_ENTRY_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_ADMINISTRATION_BULK_ENTRY_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})
  '        Case QuickLibrary.Entities.ERP_ACCOUNTS_NAME
  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_ACCOUNTS_RECEIPT_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_ACCOUNTS_RECEIPT_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})

  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_ACCOUNTS_PAYMENT_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_ACCOUNTS_PAYMENT_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})
  '        Case QuickLibrary.Entities.ERP_REPORTS_NAME
  '            lvItem = ListView.Items.Add(QuickLibrary.Entities.ERP_REPORTS_REPORT_DESC)
  '            lvItem.Name = QuickLibrary.Entities.ERP_REPORTS_REPORT_NAME
  '            lvItem.SubItems.AddRange(New String() {"1.0.0.0"})
  '    End Select
  'End Sub

    Private Sub LoadForm(ByVal FormPath As String)
        Dim Form As Form = Nothing
        Try

            Select Case FormPath
                '>>>>>>>>>> Inventory
        Case QuickLibrary.Entities.FORM_ID_SALES_INVOICE
          Form = New QuickInventory.SalesInvoice
        Case QuickLibrary.Entities.FORM_ID_POS_SALES_INVOICE
          Form = New QuickInventory.SalesInvoicePosForm
        Case QuickLibrary.Entities.FORM_ID_PURCHASE
          Form = New QuickInventory.Purchase
        Case QuickLibrary.Entities.FORM_ID_PURCHASE_RETURN
          Form = New QuickInventory.PurchaseReturn
        Case QuickLibrary.Entities.FORM_ID_ITEM
          Form = New QuickInventory.ItemForm
        Case QuickLibrary.Entities.FORM_ID_ITEM_BULK_ENTRY
          Form = New QuickInventory.ItemBulkEntryForm
        Case QuickLibrary.Entities.FORM_ID_PURCHASE_ORDER
          Form = New QuickInventory.PurchaseOrderForm
        Case QuickLibrary.Entities.FORM_ID_MINIMUM_ORDER_LEVEL
          Form = New QuickInventory.MinimumStockLevel
          '>>>>>>>>>> Accounts
        Case QuickLibrary.Entities.FORM_ID_RECEIPT
          Form = New QuickAccounting.Receipt
        Case QuickLibrary.Entities.FORM_ID_PAYMENT
          Form = New QuickAccounting.Payment
          '>>>>>>>>>> Common
        Case QuickLibrary.Entities.FORM_ID_PARTY
          Form = New QuickCommon.PartyForm
        Case QuickLibrary.Entities.FORM_ID_PARTY_GRID_ENTRY
          Form = New QuickCommon.PartyGridEntryForm
          '>>>>>>>>>> Administration
        Case QuickLibrary.Entities.FORM_ID_COMPANY
          Form = New QuickCommon.CompanyForm
        Case QuickLibrary.Entities.FORM_ID_USER
          Form = New UserForm
        Case QuickLibrary.Entities.FORM_ID_IMPORT_FROM_EXCEL
          Form = New frmTransferData
        Case QuickLibrary.Entities.FORM_ID_IMPORT_FROM_EXCEL
          Form = New BulkTransferForm
          '>>>>>>>>>> Report
        Case QuickLibrary.Entities.FORM_ID_REPORT_CRITERIA
          Form = New QuickReports.ReportCriteriaForm
                Case Else
                    MessageBox.Show("Please write code for: " & ListView.SelectedItems(0).Name)
            End Select

            If Form IsNot Nothing Then
                If TypeOf Form Is QuickBaseForms.QuickErpForm Then
                    With DirectCast(Form, QuickErpForm)
                        .LoginInfoObject.CompanyDesc = _LoginInfo.CompanyDesc
                        .LoginInfoObject.CompanyID = _LoginInfo.CompanyID
                        .LoginInfoObject.UserID = _LoginInfo.UserID
                        .LoginInfoObject.UserName = _LoginInfo.UserName
                    End With
                End If
                Form.MdiParent = Me.MdiParent
                Form.Show()
                Form = Nothing
            End If

        Catch ex As Exception
            Dim _QuickException As New QuickExceptionAdvanced("Exception in LoadForm() method", ex)
            Throw _QuickException
        End Try
    End Sub

    Private Sub ListView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles ListView.DoubleClick
        Try
            Cursor = Cursors.WaitCursor

            If ListView.SelectedItems.Count > 0 Then
                LoadForm(ListView.SelectedItems(0).Name)
            End If

        Catch ex As Exception
            Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in listview doubclick event method.", ex)
            QuickExceptionObject.Show(Me.LoginInfoObject)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ListView_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView.KeyPress
        Try

            If Asc(e.KeyChar) = 13 Then
                Cursor = Cursors.WaitCursor

                If ListView.SelectedItems.Count > 0 Then
                    LoadForm(ListView.SelectedItems(0).Name)
                End If
            End If

        Catch ex As Exception
            Dim _QuickExceptionObject As New QuickExceptionAdvanced("Exception in listview keypress event method. - " & Me.Name, ex)
            _QuickExceptionObject.Show(Me.LoginInfoObject)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub frmExplorer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If e.CloseReason = CloseReason.UserClosing Then
                e.Cancel = True
            End If
        Catch ex As Exception
            Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in closing form", ex)
            QuickExceptionObject.Show(Me.LoginInfoObject)
        End Try
    End Sub

    Private Sub Explorer1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim LoginFormObject As New LoginForm
        Dim DBConnectivityFormObject As New DBConnectivityForm
        Dim CompanyTA As New QuickDAL.QuickERPTableAdapters.CompanyTableAdapter
        Dim UserTA As New QuickDAL.QuickERPTableAdapters.UserTableAdapter
        Dim TotalCompanies As Int32
        Dim TotalUsers As Int32
        Dim _NeedsUpgradationResult As DatabaseVersion.NeedsUpgradationResult

        Try
            Do
                DBConnectivityFormObject.ShowForConnectivity()
            Loop While DBConnectivityFormObject.DialogResult = Windows.Forms.DialogResult.Retry

            If DBConnectivityFormObject.DialogResult = Windows.Forms.DialogResult.OK Then
                'If database needs to be created.
                If DBConnectivityFormObject.CreateDatabase Then
                    DatabaseVersion.UpgradeDatabase(True)
                End If

                _NeedsUpgradationResult = DatabaseVersion.NeedsUpgradation
                If _NeedsUpgradationResult = DatabaseVersion.NeedsUpgradationResult.IncorrectCurrentDBVersion Then
                    QuickMessageBox.Show(Me.LoginInfoObject, "Database version is corrupt", QuickMessageBox.MessageBoxTypes.LongMessage)
                ElseIf _NeedsUpgradationResult = DatabaseVersion.NeedsUpgradationResult.SoftwareIsOlderThanDB Then
                    QuickMessageBox.Show(Me.LoginInfoObject, "Your database is newer than the software, use the appropriate software version", QuickMessageBox.MessageBoxTypes.LongMessage)
                    QuickAlert.SaveAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Alert - Database upgradation", "Database was newer than the software", Constants.AlertTypes.Email)
                    QuickAlert.SaveAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Alert - Database upgradation", "Database was newer than the software", Constants.AlertTypes.SMS)
                ElseIf _NeedsUpgradationResult = DatabaseVersion.NeedsUpgradationResult.Yes Then
                    If MessageBox.Show("Your database needs to be upgraded (you can not use software unless you upgrade database), do you want to do this now?", "DB needs to be upgraded", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        DatabaseVersion.UpgradeDatabase()
                        MessageBox.Show("Your database is upgraded successfully, Please start the application again...")
                        QuickAlert.SaveAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Alert - Database Upgraded", "Database is upgraded", Constants.AlertTypes.Email)
                        QuickAlert.SaveAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Alert - Database upgraded", "Database is upgraded", Constants.AlertTypes.SMS)
                    Else
                        MessageBox.Show("You did not choose to upgrade your database. You can only use the software after the database is upgraded. Start the software again to upgrade the database.", "Application is closing...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        QuickAlert.SaveAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Alert - Database Upgradation", "User choosed not to upgrade database", Constants.AlertTypes.Email)
                        QuickAlert.SaveAlert(Me.LoginInfoObject, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Alert - Database upgradation", "User choosed not to upgrade database", Constants.AlertTypes.SMS)
                    End If
                    'End the application
                    End
                ElseIf _NeedsUpgradationResult = DatabaseVersion.NeedsUpgradationResult.No Then
                    TotalCompanies = Convert.ToInt32(CompanyTA.GetCount)
                    TotalUsers = Convert.ToInt32(UserTA.GetCount)
                    If TotalCompanies <= 0 OrElse TotalUsers <= 0 Then
                        Dim frm As New frmTransferData
                        frm.TransferOnlyCompanyAndUser = True
                        frm.ShowDialog()
                    End If
                    TotalCompanies = Convert.ToInt32(CompanyTA.GetCount)
                    TotalUsers = Convert.ToInt32(UserTA.GetCount)
                    If TotalCompanies > 0 AndAlso TotalUsers > 0 Then
                        Do
                            LoginFormObject.ShowDialog()
                        Loop While LoginFormObject.DialogResult = Windows.Forms.DialogResult.Retry

                        If LoginFormObject.DialogResult = Windows.Forms.DialogResult.OK Then
                            _LoginInfo.CompanyID = LoginFormObject.LoginInfoObject.CompanyID
                            _LoginInfo.CompanyDesc = LoginFormObject.LoginInfoObject.CompanyDesc
                            _LoginInfo.UserID = LoginFormObject.LoginInfoObject.UserID
                            _LoginInfo.UserName = LoginFormObject.LoginInfoObject.UserName
                            _LoginInfo.DatabaseServerName = UserTA.GetConnection.DataSource
                            _LoginInfo.DatabaseName = UserTA.GetConnection.Database
                            'Set up the UI
                            SetUpListViewColumns()
              LoadMenu(Nothing)
                        Else
                            End
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to load explorer", ex)
            QuickExceptionObject.show(Me.LoginInfoObject)
            MessageBox.Show("Please start the application again", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        Finally
            LoginFormObject = Nothing
            DBConnectivityFormObject = Nothing
        End Try
    End Sub

    Private Sub SetUpListViewColumns()
        ' TODO: Add code to set up listview columns
        ListView.Columns.Add("Name")
        'ListView.Columns.Add("Description")
        ListView.Columns.Add("Version")
        SetView(View.Details)
        ListView.Columns(0).Width = 150
        ListView.Columns(1).Width = 100
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        'Exit the application
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolBarToolStripMenuItem.Click
        'Toggle the visibility of the toolstrip and also the checked state of the associated menu item
        ToolBarToolStripMenuItem.Checked = Not ToolBarToolStripMenuItem.Checked
        ToolStrip.Visible = ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarToolStripMenuItem.Click
        'Toggle the visibility of the statusstrip and also the checked state of the associated menu item
        StatusBarToolStripMenuItem.Checked = Not StatusBarToolStripMenuItem.Checked
        StatusStrip.Visible = StatusBarToolStripMenuItem.Checked
    End Sub

    'Change whether or not the folders pane is visible
    Private Sub ToggleFoldersVisible()
        'First toggle the checked state of the associated menu item
        FoldersToolStripMenuItem.Checked = Not FoldersToolStripMenuItem.Checked

        'Change the Folders toolbar button to be in sync
        FoldersToolStripButton.Checked = FoldersToolStripMenuItem.Checked

        ' Collapse the Panel containing the TreeView.
        Me.SplitContainer.Panel1Collapsed = Not FoldersToolStripMenuItem.Checked
    End Sub

    Private Sub FoldersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripMenuItem.Click
        ToggleFoldersVisible()
    End Sub

    Private Sub FoldersToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FoldersToolStripButton.Click
        ToggleFoldersVisible()
    End Sub

    Private Sub SetView(ByVal View As System.Windows.Forms.View)
        'Figure out which menu item should be checked
        Dim MenuItemToCheck As ToolStripMenuItem = Nothing
        Select Case View
            Case View.Details
                MenuItemToCheck = DetailsToolStripMenuItem
            Case View.LargeIcon
                MenuItemToCheck = LargeIconsToolStripMenuItem
            Case View.List
                MenuItemToCheck = ListToolStripMenuItem
            Case View.SmallIcon
                MenuItemToCheck = SmallIconsToolStripMenuItem
            Case View.Tile
                MenuItemToCheck = TileToolStripMenuItem
            Case Else
                Debug.Fail("Unexpected View")
                View = View.Details
                MenuItemToCheck = DetailsToolStripMenuItem
        End Select

        'Check the appropriate menu item and deselect all others under the Views menu
        For Each MenuItem As ToolStripMenuItem In ListViewToolStripButton.DropDownItems
            If MenuItem Is MenuItemToCheck Then
                MenuItem.Checked = True
            Else
                MenuItem.Checked = False
            End If
        Next

        'Finally, set the view requested
        ListView.View = View
    End Sub

    Private Sub ListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListToolStripMenuItem.Click
        SetView(View.List)
    End Sub

    Private Sub DetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsToolStripMenuItem.Click
        SetView(View.Details)
    End Sub

    Private Sub LargeIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LargeIconsToolStripMenuItem.Click
        SetView(View.LargeIcon)
    End Sub

    Private Sub SmallIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmallIconsToolStripMenuItem.Click
        SetView(View.SmallIcon)
    End Sub

    Private Sub TileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileToolStripMenuItem.Click
        SetView(View.Tile)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt"
        OpenFileDialog.ShowDialog(Me)

        Dim FileName As String = OpenFileDialog.FileName
        ' TODO: Add code to open the file
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt"
        SaveFileDialog.ShowDialog(Me)

        Dim FileName As String = SaveFileDialog.FileName
        ' TODO: Add code here to save the current contents of the form to a file.
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect
        ' TODO: Add code to change the listview contents based on the currently-selected node of the treeview
    'LoadDetail(e.Node.FullPath)
    End Sub
#End Region

  Private Sub UltraToolbarsManager1_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles UltraToolbarsManager1.ToolClick
    If e.Tool.Key IsNot Nothing Then
      LoadForm(e.Tool.Key)
    End If
  End Sub
End Class
