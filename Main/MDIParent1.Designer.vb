<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIParent1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim Group1 As Infragistics.Win.UltraWinListBar.Group = New Infragistics.Win.UltraWinListBar.Group(True)
    Dim Group2 As Infragistics.Win.UltraWinListBar.Group = New Infragistics.Win.UltraWinListBar.Group
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIParent1))
    Dim DockAreaPane1 As Infragistics.Win.UltraWinDock.DockAreaPane = New Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedLeft, New System.Guid("ebbabc99-9887-4c11-b4fa-7e346caa9504"))
    Dim DockableControlPane1 As Infragistics.Win.UltraWinDock.DockableControlPane = New Infragistics.Win.UltraWinDock.DockableControlPane(New System.Guid("a5a86c19-eaff-4eb3-8bf0-3ac2ef92a952"), New System.Guid("00000000-0000-0000-0000-000000000000"), -1, New System.Guid("ebbabc99-9887-4c11-b4fa-7e346caa9504"), -1)
    Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("MenuToolbar")
    Me.Forms = New Infragistics.Win.UltraWinListBar.UltraListBar
    Me.ToolStrip = New System.Windows.Forms.ToolStrip
    Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.PrintPreviewToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton
    Me.StatusStrip = New System.Windows.Forms.StatusStrip
    Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.DatabaseNameStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.DatabaseServerStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.UserNameStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.VersionStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.CompanyNameStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
    Me.UltraDockManager1 = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components)
    Me._MDIParent1UnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Me._MDIParent1UnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Me._MDIParent1UnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Me._MDIParent1UnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Me._MDIParent1AutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl
    Me.DockableWindow1 = New Infragistics.Win.UltraWinDock.DockableWindow
    Me.WindowDockingArea1 = New Infragistics.Win.UltraWinDock.WindowDockingArea
    Me._frmExplorer_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Me.UltraToolbarsManager1 = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
    Me._MDIParent1_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Me._MDIParent1_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Me._MDIParent1_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Me._MDIParent1_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Me.UltraTabbedMdiManager1 = New Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(Me.components)
    Me.ToolStrip.SuspendLayout()
    Me.StatusStrip.SuspendLayout()
    CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me._MDIParent1AutoHideControl.SuspendLayout()
    Me.DockableWindow1.SuspendLayout()
    CType(Me.UltraToolbarsManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.UltraTabbedMdiManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Forms
    '
    Group1.Key = "Forms"
    Group1.Text = "Forms"
    Group2.Key = "Reports"
    Group2.Text = "Reports"
    Me.Forms.Groups.Add(Group1)
    Me.Forms.Groups.Add(Group2)
    Me.Forms.Location = New System.Drawing.Point(0, 18)
    Me.Forms.Name = "Forms"
    Me.Forms.Size = New System.Drawing.Size(133, 342)
    '
    'ToolStrip
    '
    Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.ToolStripSeparator1, Me.PrintToolStripButton, Me.PrintPreviewToolStripButton, Me.ToolStripSeparator2, Me.HelpToolStripButton})
    Me.ToolStrip.Location = New System.Drawing.Point(0, 48)
    Me.ToolStrip.Name = "ToolStrip"
    Me.ToolStrip.Size = New System.Drawing.Size(632, 25)
    Me.ToolStrip.TabIndex = 6
    Me.ToolStrip.Text = "ToolStrip"
    Me.ToolStrip.Visible = False
    '
    'NewToolStripButton
    '
    Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
    Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.NewToolStripButton.Name = "NewToolStripButton"
    Me.NewToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.NewToolStripButton.Text = "New"
    '
    'OpenToolStripButton
    '
    Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
    Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.OpenToolStripButton.Name = "OpenToolStripButton"
    Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.OpenToolStripButton.Text = "Open"
    '
    'SaveToolStripButton
    '
    Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
    Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.SaveToolStripButton.Name = "SaveToolStripButton"
    Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.SaveToolStripButton.Text = "Save"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'PrintToolStripButton
    '
    Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
    Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintToolStripButton.Name = "PrintToolStripButton"
    Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.PrintToolStripButton.Text = "Print"
    '
    'PrintPreviewToolStripButton
    '
    Me.PrintPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.PrintPreviewToolStripButton.Image = CType(resources.GetObject("PrintPreviewToolStripButton.Image"), System.Drawing.Image)
    Me.PrintPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.PrintPreviewToolStripButton.Name = "PrintPreviewToolStripButton"
    Me.PrintPreviewToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.PrintPreviewToolStripButton.Text = "Print Preview"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
    '
    'HelpToolStripButton
    '
    Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
    Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
    Me.HelpToolStripButton.Name = "HelpToolStripButton"
    Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
    Me.HelpToolStripButton.Text = "Help"
    '
    'StatusStrip
    '
    Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel, Me.DatabaseNameStatusBarLabel, Me.DatabaseServerStatusBarLabel, Me.UserNameStatusBarLabel, Me.VersionStatusBarLabel, Me.CompanyNameStatusBarLabel})
    Me.StatusStrip.Location = New System.Drawing.Point(0, 431)
    Me.StatusStrip.Name = "StatusStrip"
    Me.StatusStrip.Size = New System.Drawing.Size(632, 22)
    Me.StatusStrip.TabIndex = 7
    Me.StatusStrip.Text = "StatusStrip"
    '
    'ToolStripStatusLabel
    '
    Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
    Me.ToolStripStatusLabel.Size = New System.Drawing.Size(308, 17)
    Me.ToolStripStatusLabel.Spring = True
    Me.ToolStripStatusLabel.Text = "Status"
    Me.ToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'DatabaseNameStatusBarLabel
    '
    Me.DatabaseNameStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.DatabaseNameStatusBarLabel.Name = "DatabaseNameStatusBarLabel"
    Me.DatabaseNameStatusBarLabel.Size = New System.Drawing.Size(54, 17)
    Me.DatabaseNameStatusBarLabel.Text = "DB Name"
    '
    'DatabaseServerStatusBarLabel
    '
    Me.DatabaseServerStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.DatabaseServerStatusBarLabel.Name = "DatabaseServerStatusBarLabel"
    Me.DatabaseServerStatusBarLabel.Size = New System.Drawing.Size(59, 17)
    Me.DatabaseServerStatusBarLabel.Text = "DB Server"
    '
    'UserNameStatusBarLabel
    '
    Me.UserNameStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.UserNameStatusBarLabel.Name = "UserNameStatusBarLabel"
    Me.UserNameStatusBarLabel.Size = New System.Drawing.Size(63, 17)
    Me.UserNameStatusBarLabel.Text = "User Name"
    '
    'VersionStatusBarLabel
    '
    Me.VersionStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.VersionStatusBarLabel.Name = "VersionStatusBarLabel"
    Me.VersionStatusBarLabel.Size = New System.Drawing.Size(47, 17)
    Me.VersionStatusBarLabel.Text = "0.0.0.0"
    '
    'CompanyNameStatusBarLabel
    '
    Me.CompanyNameStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.CompanyNameStatusBarLabel.Name = "CompanyNameStatusBarLabel"
    Me.CompanyNameStatusBarLabel.Size = New System.Drawing.Size(86, 17)
    Me.CompanyNameStatusBarLabel.Text = "Company Name"
    '
    'UltraDockManager1
    '
    DockAreaPane1.ChildPaneStyle = Infragistics.Win.UltraWinDock.ChildPaneStyle.VerticalSplit
    DockableControlPane1.Control = Me.Forms
    DockableControlPane1.FlyoutSize = New System.Drawing.Size(133, -1)
    DockableControlPane1.OriginalControlBounds = New System.Drawing.Rectangle(104, 124, 120, 256)
    DockableControlPane1.Pinned = False
    DockableControlPane1.Size = New System.Drawing.Size(100, 100)
    DockableControlPane1.Text = "UltraListBar1"
    DockAreaPane1.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane1})
    DockAreaPane1.Size = New System.Drawing.Size(151, 382)
    Me.UltraDockManager1.DockAreas.AddRange(New Infragistics.Win.UltraWinDock.DockAreaPane() {DockAreaPane1})
    Me.UltraDockManager1.HostControl = Me
    Me.UltraDockManager1.Visible = False
    '
    '_MDIParent1UnpinnedTabAreaLeft
    '
    Me._MDIParent1UnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me._MDIParent1UnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._MDIParent1UnpinnedTabAreaLeft.Location = New System.Drawing.Point(0, 48)
    Me._MDIParent1UnpinnedTabAreaLeft.Name = "_MDIParent1UnpinnedTabAreaLeft"
    Me._MDIParent1UnpinnedTabAreaLeft.Owner = Me.UltraDockManager1
    Me._MDIParent1UnpinnedTabAreaLeft.Size = New System.Drawing.Size(21, 383)
    Me._MDIParent1UnpinnedTabAreaLeft.TabIndex = 9
    '
    '_MDIParent1UnpinnedTabAreaRight
    '
    Me._MDIParent1UnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right
    Me._MDIParent1UnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._MDIParent1UnpinnedTabAreaRight.Location = New System.Drawing.Point(632, 48)
    Me._MDIParent1UnpinnedTabAreaRight.Name = "_MDIParent1UnpinnedTabAreaRight"
    Me._MDIParent1UnpinnedTabAreaRight.Owner = Me.UltraDockManager1
    Me._MDIParent1UnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 383)
    Me._MDIParent1UnpinnedTabAreaRight.TabIndex = 10
    '
    '_MDIParent1UnpinnedTabAreaTop
    '
    Me._MDIParent1UnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top
    Me._MDIParent1UnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._MDIParent1UnpinnedTabAreaTop.Location = New System.Drawing.Point(21, 48)
    Me._MDIParent1UnpinnedTabAreaTop.Name = "_MDIParent1UnpinnedTabAreaTop"
    Me._MDIParent1UnpinnedTabAreaTop.Owner = Me.UltraDockManager1
    Me._MDIParent1UnpinnedTabAreaTop.Size = New System.Drawing.Size(611, 0)
    Me._MDIParent1UnpinnedTabAreaTop.TabIndex = 11
    '
    '_MDIParent1UnpinnedTabAreaBottom
    '
    Me._MDIParent1UnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me._MDIParent1UnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._MDIParent1UnpinnedTabAreaBottom.Location = New System.Drawing.Point(21, 431)
    Me._MDIParent1UnpinnedTabAreaBottom.Name = "_MDIParent1UnpinnedTabAreaBottom"
    Me._MDIParent1UnpinnedTabAreaBottom.Owner = Me.UltraDockManager1
    Me._MDIParent1UnpinnedTabAreaBottom.Size = New System.Drawing.Size(611, 0)
    Me._MDIParent1UnpinnedTabAreaBottom.TabIndex = 12
    '
    '_MDIParent1AutoHideControl
    '
    Me._MDIParent1AutoHideControl.Controls.Add(Me.DockableWindow1)
    Me._MDIParent1AutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._MDIParent1AutoHideControl.Location = New System.Drawing.Point(21, 71)
    Me._MDIParent1AutoHideControl.Name = "_MDIParent1AutoHideControl"
    Me._MDIParent1AutoHideControl.Owner = Me.UltraDockManager1
    Me._MDIParent1AutoHideControl.Size = New System.Drawing.Size(8, 360)
    Me._MDIParent1AutoHideControl.TabIndex = 13
    '
    'DockableWindow1
    '
    Me.DockableWindow1.Controls.Add(Me.Forms)
    Me.DockableWindow1.Location = New System.Drawing.Point(-80, 0)
    Me.DockableWindow1.Name = "DockableWindow1"
    Me.DockableWindow1.Owner = Me.UltraDockManager1
    Me.DockableWindow1.Size = New System.Drawing.Size(133, 383)
    Me.DockableWindow1.TabIndex = 0
    '
    'WindowDockingArea1
    '
    Me.WindowDockingArea1.Dock = System.Windows.Forms.DockStyle.Left
    Me.WindowDockingArea1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.WindowDockingArea1.Location = New System.Drawing.Point(21, 49)
    Me.WindowDockingArea1.Name = "WindowDockingArea1"
    Me.WindowDockingArea1.Owner = Me.UltraDockManager1
    Me.WindowDockingArea1.Size = New System.Drawing.Size(156, 382)
    Me.WindowDockingArea1.TabIndex = 15
    '
    '_frmExplorer_Toolbars_Dock_Area_Top
    '
    Me._frmExplorer_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
    Me._frmExplorer_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
    Me._frmExplorer_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
    Me._frmExplorer_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
    Me._frmExplorer_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 24)
    Me._frmExplorer_Toolbars_Dock_Area_Top.Name = "_frmExplorer_Toolbars_Dock_Area_Top"
    Me._frmExplorer_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(632, 24)
    Me._frmExplorer_Toolbars_Dock_Area_Top.ToolbarsManager = Me.UltraToolbarsManager1
    '
    'UltraToolbarsManager1
    '
    Me.UltraToolbarsManager1.DesignerFlags = 1
    Me.UltraToolbarsManager1.DockWithinContainer = Me
    Me.UltraToolbarsManager1.MdiMergeable = False
    UltraToolbar1.DockedColumn = 0
    UltraToolbar1.DockedRow = 0
    UltraToolbar1.Text = "MenuToolbar"
    Me.UltraToolbarsManager1.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
    '
    '_MDIParent1_Toolbars_Dock_Area_Left
    '
    Me._MDIParent1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
    Me._MDIParent1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control
    Me._MDIParent1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
    Me._MDIParent1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
    Me._MDIParent1_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 48)
    Me._MDIParent1_Toolbars_Dock_Area_Left.Name = "_MDIParent1_Toolbars_Dock_Area_Left"
    Me._MDIParent1_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 383)
    Me._MDIParent1_Toolbars_Dock_Area_Left.ToolbarsManager = Me.UltraToolbarsManager1
    '
    '_MDIParent1_Toolbars_Dock_Area_Right
    '
    Me._MDIParent1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
    Me._MDIParent1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control
    Me._MDIParent1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
    Me._MDIParent1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
    Me._MDIParent1_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(632, 48)
    Me._MDIParent1_Toolbars_Dock_Area_Right.Name = "_MDIParent1_Toolbars_Dock_Area_Right"
    Me._MDIParent1_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 383)
    Me._MDIParent1_Toolbars_Dock_Area_Right.ToolbarsManager = Me.UltraToolbarsManager1
    '
    '_MDIParent1_Toolbars_Dock_Area_Top
    '
    Me._MDIParent1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
    Me._MDIParent1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
    Me._MDIParent1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
    Me._MDIParent1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
    Me._MDIParent1_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
    Me._MDIParent1_Toolbars_Dock_Area_Top.Name = "_MDIParent1_Toolbars_Dock_Area_Top"
    Me._MDIParent1_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(632, 24)
    Me._MDIParent1_Toolbars_Dock_Area_Top.ToolbarsManager = Me.UltraToolbarsManager1
    '
    '_MDIParent1_Toolbars_Dock_Area_Bottom
    '
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 431)
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.Name = "_MDIParent1_Toolbars_Dock_Area_Bottom"
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(632, 0)
    Me._MDIParent1_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.UltraToolbarsManager1
    '
    'UltraTabbedMdiManager1
    '
    Me.UltraTabbedMdiManager1.BorderStyle = Infragistics.Win.UltraWinTabbedMdi.MdiClientBorderStyle.Inset
    Me.UltraTabbedMdiManager1.MdiParent = Me
    Me.UltraTabbedMdiManager1.ViewStyle = Infragistics.Win.UltraWinTabbedMdi.ViewStyle.VisualStudio2005
    '
    'MDIParent1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(632, 453)
    Me.Controls.Add(Me._MDIParent1AutoHideControl)
    Me.Controls.Add(Me.WindowDockingArea1)
    Me.Controls.Add(Me.ToolStrip)
    Me.Controls.Add(Me._MDIParent1UnpinnedTabAreaTop)
    Me.Controls.Add(Me._MDIParent1UnpinnedTabAreaBottom)
    Me.Controls.Add(Me._MDIParent1UnpinnedTabAreaRight)
    Me.Controls.Add(Me._MDIParent1UnpinnedTabAreaLeft)
    Me.Controls.Add(Me._MDIParent1_Toolbars_Dock_Area_Left)
    Me.Controls.Add(Me._MDIParent1_Toolbars_Dock_Area_Right)
    Me.Controls.Add(Me._frmExplorer_Toolbars_Dock_Area_Top)
    Me.Controls.Add(Me._MDIParent1_Toolbars_Dock_Area_Top)
    Me.Controls.Add(Me._MDIParent1_Toolbars_Dock_Area_Bottom)
    Me.Controls.Add(Me.StatusStrip)
    Me.IsMdiContainer = True
    Me.Name = "MDIParent1"
    Me.Text = "ERP"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.ToolStrip.ResumeLayout(False)
    Me.ToolStrip.PerformLayout()
    Me.StatusStrip.ResumeLayout(False)
    Me.StatusStrip.PerformLayout()
    CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me._MDIParent1AutoHideControl.ResumeLayout(False)
    Me.DockableWindow1.ResumeLayout(False)
    CType(Me.UltraToolbarsManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.UltraTabbedMdiManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents PrintPreviewToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
  Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
  Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
  Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents UltraDockManager1 As Infragistics.Win.UltraWinDock.UltraDockManager
  Friend WithEvents _MDIParent1AutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl
  Friend WithEvents _MDIParent1UnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea
  Friend WithEvents _MDIParent1UnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea
  Friend WithEvents _MDIParent1UnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea
  Friend WithEvents _MDIParent1UnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea
  Friend WithEvents Forms As Infragistics.Win.UltraWinListBar.UltraListBar
  Friend WithEvents WindowDockingArea1 As Infragistics.Win.UltraWinDock.WindowDockingArea
  Friend WithEvents DockableWindow1 As Infragistics.Win.UltraWinDock.DockableWindow
  Friend WithEvents UserNameStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents VersionStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents CompanyNameStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents DatabaseServerStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents DatabaseNameStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents _frmExplorer_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
  Friend WithEvents _MDIParent1_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
  Friend WithEvents UltraToolbarsManager1 As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
  Friend WithEvents _MDIParent1_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
  Friend WithEvents _MDIParent1_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
  Friend WithEvents _MDIParent1_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
  Friend WithEvents UltraTabbedMdiManager1 As Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager

End Class
