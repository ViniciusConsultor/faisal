Imports System.Windows.Forms
Imports QuickLibrary
Imports QuickDALlibrary.DatabaseCache
Imports QuickDALlibrary
Imports QuickDAL.QuickSecurityDataSet

Public Class MDIParent1
  Private _LoginInfo As New LoginInfo

#Region "Properties"
  Public ReadOnly Property LoginInfoObject() As LoginInfo
    Get
      Return _LoginInfo
    End Get
  End Property
#End Region

  Private m_ChildFormNumber As Integer = 0


  Dim _FormControlTA As New QuickDAL.QuickCommonDataSetTableAdapters.SettingFormControlsTableAdapter
  Dim _FormControlTable As New QuickDAL.QuickCommonDataSet.SettingFormControlsDataTable
  Dim _FormControlRow As QuickDAL.QuickCommonDataSet.SettingFormControlsRow

  Private Sub MDIParent1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try

      If MessageBox.Show("Are you sure you want to close the application.", "Application Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        'Do Nothing, let application close.
      Else
        e.Cancel = True
      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in FormClosing event method.", ex)
      ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  Private Sub MDIParent1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      'QuickLibrary.Common.i
      LoadApplication()

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in loading main form", ex)
      ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  Private Sub Forms_ItemSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinListBar.ItemEventArgs) Handles Forms.ItemSelected
    Try
      For Each frm As Form In Me.MdiChildren
        If frm.Name = e.Item.Key Then
          'Me.ActivateMdiChild(frm)
          frm.BringToFront()
        End If
      Next

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in loading main form", ex)
      ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  Private Sub MDIParent1_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
    Try
      Dim _Found As Boolean
      Dim I As Int32

      If Me.ActiveMdiChild IsNot Nothing Then
        If Not Me.Forms.Groups("Forms").Items.Exists(Me.ActiveMdiChild.Name) Then
          AddListItem(Me.ActiveMdiChild.Name, Me.ActiveMdiChild.Text)
        End If
      End If

      Do While I < Me.Forms.Groups("Forms").Items.Count
        _Found = False
        For J As Int32 = 0 To Me.MdiChildren.Length - 1
          If Me.MdiChildren(J).Name = Me.Forms.Groups("Forms").Items(I).Key Then
            _Found = True
          End If
        Next

        If Not _Found Then
          Me.Forms.Groups("Forms").Items.RemoveAt(I)
        Else
          I += 1
        End If
      Loop

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in checking active form", ex)
      ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  Private Sub AddListItem(ByVal KeyString As String, ByVal CaptionString As String)
    Try
      Me.Forms.Groups("Forms").Items.Add(KeyString, CaptionString)

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in adding item", ex)
      ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

#Region "Methods"
  Private Sub LoadApplication()
    Try
      Dim _UserTA As New UserTableAdapter
      'frmExplorer.MdiParent = Me

      'AddListItem(frm.Name, frm.Text)
      QuickDAL.SharedSetting.QuickErpConnectionString = General.ConfigurationRead(Constants.CONFIG_KEY_CONNECTION_STRING)
      Me.DatabaseServerStatusBarLabel.Text = _UserTA.GetConnection.DataSource
      Me.DatabaseNameStatusBarLabel.Text = _UserTA.GetConnection.Database

      'frmExplorer.Show()
      'frmExplorer.WindowState = FormWindowState.Maximized

      Dim LoginFormObject As New LoginForm
      Dim DBConnectivityFormObject As New DBConnectivityForm
      Dim CompanyTA As New CompanyTableAdapter
      Dim UserTA As New UserTableAdapter
      Dim TotalCompanies As Int32
      Dim TotalUsers As Int32
      Dim _NeedsUpgradationResult As DatabaseVersion.NeedsUpgradationResult

      Do
        DBConnectivityFormObject.ShowForConnectivity()
      Loop While DBConnectivityFormObject.DialogResult = Windows.Forms.DialogResult.Retry

      If DBConnectivityFormObject.DialogResult = Windows.Forms.DialogResult.OK Then
        'If database needs to be created.
        If DBConnectivityFormObject.CreateDatabase OrElse DBConnectivityFormObject.CreateObjectsOnly Then
          DatabaseVersion.UpgradeDatabase(DBConnectivityFormObject.CreateDatabase, DBConnectivityFormObject.CreateObjectsOnly)
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
              _LoginInfo.IsAdmin = LoginFormObject.LoginInfoObject.IsAdmin
              _LoginInfo.RoleID = LoginFormObject.LoginInfoObject.RoleID
              _LoginInfo.DatabaseServerName = UserTA.GetConnection.DataSource
              _LoginInfo.DatabaseName = UserTA.GetConnection.Database
              'Set up the UI
              'SetUpListViewColumns()
              ClearMenu()
              LoadMenu(Nothing)
            Else
              End
            End If
          End If
        End If
      End If

      Me.UserNameStatusBarLabel.Text = LoginInfoObject.UserName
      Me.CompanyNameStatusBarLabel.Text = LoginInfoObject.CompanyID.ToString _
        & "-" & LoginInfoObject.CompanyDesc
      Me.DatabaseServerStatusBarLabel.Text = LoginInfoObject.DatabaseServerName
      Me.DatabaseNameStatusBarLabel.Text = LoginInfoObject.DatabaseName

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in loading application", ex)
      ExceptionObject.Show(_LoginInfo)
    End Try

  End Sub

  Private Sub ClearMenu()
    Try
      Me.UltraToolbarsManager1.Tools.Clear()

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception is clearing menu items.", ex)
      _ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  Private Sub LoadMenu(ByVal _MenuTool As Infragistics.Win.UltraWinToolbars.PopupMenuTool)
    Try
      ' TODO: Add code to add items to the treeview
      Dim _MenuTA As New MenuTableAdapter
      Dim _MenuTable As MenuDataTable

      If _MenuTool Is Nothing Then
        If Me.LoginInfoObject.IsAdmin Then
          _MenuTable = _MenuTA.GetByParentMenuIDForAdmin(0)
        Else
          _MenuTable = _MenuTA.GetAllowedByCoIDParentMenuIdUserID(Me.LoginInfoObject.CompanyID, 0, Me.LoginInfoObject.UserID)
        End If
      Else
        If Me.LoginInfoObject.IsAdmin Then
          _MenuTable = _MenuTA.GetByParentMenuIDForAdmin(Convert.ToInt32(_MenuTool.Tag.ToString))
        Else
          _MenuTable = _MenuTA.GetAllowedByCoIDParentMenuIdUserID(Me.LoginInfoObject.CompanyID, Convert.ToInt32(_MenuTool.Tag.ToString), Me.LoginInfoObject.UserID)
        End If
      End If

      For I As Int32 = 0 To _MenuTable.Rows.Count - 1
        Dim _checkTable As MenuDataTable
        If Me.LoginInfoObject.IsAdmin Then
          _checkTable = _MenuTA.GetByParentMenuIDForAdmin(_MenuTable(I).Menu_Id)
        Else
          _checkTable = _MenuTA.GetAllowedByCoIDParentMenuIdUserID(Me.LoginInfoObject.CompanyID, _MenuTable(I).Menu_Id, Me.LoginInfoObject.UserID)
        End If

        Dim _NewTool As Infragistics.Win.UltraWinToolbars.ToolBase

        If _checkTable.Rows.Count > 0 Then
          _NewTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool(_MenuTable(I).Form_Code.ToString)
        Else
          'If there are no sub items then it should be button tool
          _NewTool = New Infragistics.Win.UltraWinToolbars.ButtonTool(_MenuTable(I).Form_Code.ToString)
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
    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception is loading the menu.", ex)
      _ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  Private Sub AddControls(ByVal _Control As Windows.Forms.Control, ByVal _FormID As Int16)
    Try

      If Not TypeOf _Control Is Label Then
        _FormControlRow = _FormControlTable.NewSettingFormControlsRow
        With _FormControlRow
          .Control_ID = _FormControlTA.GetMaxControlID(_FormID).Value + Convert.ToInt16(1)
          .Control_Name = _Control.Name
          .Form_ID = _FormID
          .RecordStatus_ID = 1
          .Stamp_DateTime = Date.UtcNow
          .Stamp_UserID = 0
        End With
        _FormControlTable.Rows.Add(_FormControlRow)
        _FormControlTA.Update(_FormControlTable)
      End If

      For Each _NestedControl As Control In _Control.Controls
        AddControls(_NestedControl, _FormID)
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in AddControls of FormControlPermissionForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2009
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  'Faisal Saleem  29-Dec-09 Previously it was written in the menu click event.
  '                         Now logoff will be populated in dynamic menu(db based),
  '                         So I made it a sub so that it can be called from other
  '                         method.
  ''' <summary>
  ''' Current user will log off and all opened forms will be closed. Login screen
  ''' will be shown to the user.
  ''' </summary>
  Private Sub LogOff()
    Try
      Dim _ChildForm As Form

      For Each _ChildForm In Me.MdiChildren
        _ChildForm.Dispose()
      Next
      LoadApplication()

    Catch ex As Exception
      Dim _ExceptionObject As New QuickExceptionAdvanced("Exception in logoff of MDIParent1", ex)
      _ExceptionObject.Show(_LoginInfo)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2009
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  'Faisal Saleem  29-Dec-09 Previously it was called on click event of menu, now 
  '                         this option is added in dynamic menu (db based). It is
  '                         now a sub so that can be called from other method(s).
  ''' <summary>
  ''' Ends the application
  ''' </summary>
  Private Sub ExitApplication()
    Try
      Global.System.Windows.Forms.Application.Exit()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ExitApplication of MDIParent1.", ex)
      Throw _qex
    End Try
  End Sub

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.VersionStatusBarLabel.Text = My.Application.Info.Version.ToString
  End Sub

#End Region

  Private Sub UltraToolbarsManager1_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles UltraToolbarsManager1.ToolClick
    If e.Tool.Key IsNot Nothing Then
      LoadForm(e.Tool.Key)
    End If
  End Sub

  Private Sub LoadForm(ByVal FormPath As String)
    Dim Form As Form = Nothing
    Dim _LastTransactionDate As DateTime

    Try
      'Disabling this check on Anjum Sb. request for 2 days.
      If 1 = 2 AndAlso Not General.IsSystemDateCorrect(_LastTransactionDate) Then
        QuickMessageBox.Show(General.LoginInfoObject, "Your system date is not correct " & _LastTransactionDate.ToString, MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)
      Else
        Select Case FormPath
          '>>>>>>>>>> File
          Case QuickLibrary.Entities.FORM_ID_LOG_OFF
            LogOff()
            Exit Sub
          Case QuickLibrary.Entities.FORM_ID_EXIT_APPLICATION
            ExitApplication()
            Exit Sub

            '>>>>>>>>>> Inventory
          Case QuickLibrary.Entities.FORM_ID_SALES_INVOICE
            Form = New QuickInventory.SalesInvoiceForm
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
          Case QuickLibrary.Entities.FORM_ID_SALES_RETURN
            Form = New QuickInventory.SalesInvoiceReturn
          Case QuickLibrary.Entities.FORM_ID_PURCHASE_WAREHOUSE
            Form = New QuickInventory.PurchaseWarehouseForm
          Case QuickLibrary.Entities.FORM_ID_STOCK_INQUIRY
            Form = New QuickInventory.StockInquiryForm

            '>>>>>>>>>> Accounts
          Case QuickLibrary.Entities.FORM_ID_RECEIPT
            Form = New QuickAccounting.Receipt
          Case QuickLibrary.Entities.FORM_ID_PAYMENT
            Form = New QuickAccounting.Payment
          Case QuickLibrary.Entities.FORM_ID_COA
            Form = New QuickAccounting.COAForm
          Case QuickLibrary.Entities.FORM_ID_VOUCHER_TYPE
            Form = New QuickAccounting.VoucherTypeForm
          Case QuickLibrary.Entities.FORM_ID_VOUCHER_ENTRY
            Form = New QuickAccounting.VoucherForm

            '>>>>>>>>>> Common
          Case QuickLibrary.Entities.FORM_ID_PARTY
            Form = New QuickCommon.PartyRegularForm
          Case QuickLibrary.Entities.FORM_ID_PARTY_GRID_ENTRY
            Form = New QuickCommon.PartyGridEntryForm

            '<<<<<<<<<< Administration
          Case QuickLibrary.Entities.FORM_ID_COMPANY
            Form = New QuickCommon.CompanyForm
          Case QuickLibrary.Entities.FORM_ID_TRANSFER_DATA
            Form = New frmTransferData
          Case QuickLibrary.Entities.FORM_ID_IMPORT_FROM_EXCEL
            Form = New QuickBaseForms.BulkTransferForm
          Case QuickLibrary.Entities.FORM_ID_ERP_CONFIGURATION
            Form = New QuickAdministration.ErpConfigurationForm
          Case QuickLibrary.Entities.FORM_ID_EMPTY_DATABASE
            Form = New QuickAdministration.EmptyDatabaseForm

            '<<<<<<<<<< Security
          Case QuickLibrary.Entities.FORM_ID_MENU_ROLE_ASSOCIATION
            Form = New QuickSecurity.MenuRoleAssociationForm
          Case QuickLibrary.Entities.FORM_ID_USER
            Form = New QuickSecurity.SecurityUserForm
          Case QuickLibrary.Entities.FORM_ID_USER_ROLE
            Form = New QuickSecurity.SecurityRoleForm
          Case QuickLibrary.Entities.FORM_ID_FORM_CONTROL_PERMISSION
            Form = New QuickSecurity.FormControlPermissionForm

            '<<<<<<<<<< Production
          Case QuickLibrary.Entities.FORM_ID_PROCESS
            Form = New QuickProduction.DefineProcessForm
          Case QuickLibrary.Entities.FORM_ID_PROCESS_WORKFLOW
            Form = New QuickProduction.ProcessWorkFlowForm
          Case QuickLibrary.Entities.FORM_ID_PRODUCTION_ORDER
            Form = New QuickProduction.ProductionOrderForm
          Case QuickLibrary.Entities.FORM_ID_PROCESS_PRODUCTION
            Form = New QuickProduction.ProcessProduction

            '>>>>>>>>>> Report
          Case QuickLibrary.Entities.FORM_ID_REPORT_CRITERIA
            Form = New QuickReports.ReportCriteriaForm

            '>>>>>>>>>> Fabrication
          Case "99-001"
            'Form = New SecurityUserForm
          Case "99-002"
            'Form = New SecurityRoleForm
            Form = New QuickProduction.ProcessWorkFlowForm
          Case "99-003"
            Form = New QuickInventory.InventoryFormSizes
          Case "99-001"
            Form = New QuickProduction.DefineProcessForm
          Case "99-002"
            Form = New QuickProduction.ProcessWorkFlowForm
          Case "03-008"
            Form = New QuickAdministration.MenuSetting
          Case "03-009"

            Form = New QuickAdministration.FormSetting
          Case Else
            'MessageBox.Show("Please write code for: " & ListView.SelectedItems(0).Name)

        End Select

        If Form IsNot Nothing Then
          If TypeOf Form Is QuickBaseForms.ParentBasicForm Then
            With DirectCast(Form, QuickBaseForms.ParentBasicForm)
              .LoginInfoObject.CompanyDesc = _LoginInfo.CompanyDesc
              .LoginInfoObject.CompanyID = _LoginInfo.CompanyID
              .LoginInfoObject.UserID = _LoginInfo.UserID
              .LoginInfoObject.UserName = _LoginInfo.UserName
              .LoginInfoObject.RoleID = _LoginInfo.RoleID
              .LoginInfoObject.DatabaseName = _LoginInfo.DatabaseName
              .LoginInfoObject.DatabaseServerName = _LoginInfo.DatabaseServerName
            End With
          End If
          Form.MdiParent = Me
          Form.Show()
          Form = Nothing
        End If
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in LoadForm() method", ex)
      _QuickException.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event is handling all db related label click.
  ''' </summary>
  Private Sub DatabaseNameStatusBarLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles DatabaseNameStatusBarLabel.Click, DatabaseServerStatusBarLabel.Click
    Try

      If Me.MdiChildren.Length > 0 Then

        QuickMessageBox.Show(Me.LoginInfoObject, "Close all forms before changing connection information", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      Else

        Dim _DBConnectivityForm As New DBConnectivityForm
        _DBConnectivityForm.ShowDialog()
        LoadApplication()
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in DatabaseNameStatusBarLabel_Click of MDIParent1.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub
End Class
