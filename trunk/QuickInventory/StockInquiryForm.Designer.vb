<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockInquiryForm
  Inherits QuickBaseForms.ParentToolbarForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim DefaultFocusIndicatorRenderer2 As FarPoint.Win.Spread.DefaultFocusIndicatorRenderer = New FarPoint.Win.Spread.DefaultFocusIndicatorRenderer()
    Dim DefaultScrollBarRenderer5 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer()
    Dim DefaultScrollBarRenderer6 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer()
    Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
    Dim DefaultScrollBarRenderer1 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer()
    Dim DefaultScrollBarRenderer2 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer()
    Me.StockQuickSpread = New QuickBusinessControls.ItemSpread
    Me.StockQuickSpread_Sheet1 = New QuickBusinessControls.ItemSpreadView
    Me.ItemLabel = New QuickControls.Quick_Label()
    Me.ShowButton = New QuickControls.Quick_Button()
    Me.TabControl1 = New System.Windows.Forms.TabControl()
    Me.StockTabPage = New System.Windows.Forms.TabPage()
    Me.MinimumLevelTabPage = New System.Windows.Forms.TabPage()
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox = New QuickControls.Quick_UltraComboBox()
    Me.MinimumStockLevelQuickSpread = New QuickBusinessControls.ItemSpread
    Me.MinimumStockLevelSheet = New QuickBusinessControls.ItemSpreadView
    Me.CompanyCheckedListBox1 = New QuickBusinessControls.CompanyCheckedListBox()
    Me.ExportToExcelButton = New QuickControls.Quick_Button()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    Me.ItemComboBox = New QuickBusinessControls.MultiComboBox()
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.StockQuickSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.StockQuickSpread_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabControl1.SuspendLayout()
    Me.StockTabPage.SuspendLayout()
    Me.MinimumLevelTabPage.SuspendLayout()
    CType(Me.MinimumStockLevelFilterOptionQuickUltraComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.MinimumStockLevelQuickSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.MinimumStockLevelSheet, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'StockQuickSpread
    '
    Me.StockQuickSpread.AccessibleDescription = "Quick_Spread"
    Me.StockQuickSpread.AllowColumnMove = True
    Me.StockQuickSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.StockQuickSpread.AutoNewRow = True
    Me.StockQuickSpread.EditModePermanent = True
    Me.StockQuickSpread.EditModeReplace = True
    Me.StockQuickSpread.FocusRenderer = DefaultFocusIndicatorRenderer2
    Me.StockQuickSpread.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.StockQuickSpread.HorizontalScrollBar.Name = ""
    Me.StockQuickSpread.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer5
    Me.StockQuickSpread.HorizontalScrollBar.TabIndex = 0
    Me.StockQuickSpread.Location = New System.Drawing.Point(4, 4)
    Me.StockQuickSpread.Name = "StockQuickSpread"
    Me.StockQuickSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.StockQuickSpread_Sheet1})
    Me.StockQuickSpread.Size = New System.Drawing.Size(601, 300)
    Me.StockQuickSpread.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.StockQuickSpread.TabIndex = 1
    Me.StockQuickSpread.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.StockQuickSpread.VerticalScrollBar.Name = ""
    Me.StockQuickSpread.VerticalScrollBar.Renderer = DefaultScrollBarRenderer6
    Me.StockQuickSpread.VerticalScrollBar.TabIndex = 1
    Me.StockQuickSpread.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'StockQuickSpread_Sheet1
    '
    Me.StockQuickSpread_Sheet1.Reset()
    Me.StockQuickSpread_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.StockQuickSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.StockQuickSpread_Sheet1.AllowGroup = True
    Me.StockQuickSpread_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.StockQuickSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
    Me.StockQuickSpread_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.StockQuickSpread_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
    Me.StockQuickSpread_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.StockQuickSpread_Sheet1.SheetCornerStyle.Parent = "CornerDefault"
    Me.StockQuickSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'ItemLabel
    '
    Me.ItemLabel.AllowClearValue = False
    Me.ItemLabel.AutoSize = True
    Me.ItemLabel.DefaultValue = ""
    Me.ItemLabel.Location = New System.Drawing.Point(8, 49)
    Me.ItemLabel.Name = "ItemLabel"
    Me.ItemLabel.Size = New System.Drawing.Size(30, 13)
    Me.ItemLabel.TabIndex = 4
    Me.ItemLabel.Text = "Item:"
    '
    'ShowButton
    '
    Me.ShowButton.Location = New System.Drawing.Point(436, 44)
    Me.ShowButton.Name = "ShowButton"
    Me.ShowButton.Size = New System.Drawing.Size(75, 23)
    Me.ShowButton.TabIndex = 6
    Me.ShowButton.Text = "&Show"
    Me.ShowButton.UseVisualStyleBackColor = True
    '
    'TabControl1
    '
    Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl1.Controls.Add(Me.StockTabPage)
    Me.TabControl1.Controls.Add(Me.MinimumLevelTabPage)
    Me.TabControl1.Location = New System.Drawing.Point(4, 76)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(617, 332)
    Me.TabControl1.TabIndex = 7
    '
    'StockTabPage
    '
    Me.StockTabPage.Controls.Add(Me.StockQuickSpread)
    Me.StockTabPage.Location = New System.Drawing.Point(4, 22)
    Me.StockTabPage.Name = "StockTabPage"
    Me.StockTabPage.Padding = New System.Windows.Forms.Padding(3)
    Me.StockTabPage.Size = New System.Drawing.Size(609, 306)
    Me.StockTabPage.TabIndex = 0
    Me.StockTabPage.Text = "Stock"
    Me.StockTabPage.UseVisualStyleBackColor = True
    '
    'MinimumLevelTabPage
    '
    Me.MinimumLevelTabPage.Controls.Add(Me.MinimumStockLevelFilterOptionQuickUltraComboBox)
    Me.MinimumLevelTabPage.Controls.Add(Me.MinimumStockLevelQuickSpread)
    Me.MinimumLevelTabPage.Location = New System.Drawing.Point(4, 22)
    Me.MinimumLevelTabPage.Name = "MinimumLevelTabPage"
    Me.MinimumLevelTabPage.Padding = New System.Windows.Forms.Padding(3)
    Me.MinimumLevelTabPage.Size = New System.Drawing.Size(609, 306)
    Me.MinimumLevelTabPage.TabIndex = 1
    Me.MinimumLevelTabPage.Text = "Min Level"
    Me.MinimumLevelTabPage.UseVisualStyleBackColor = True
    '
    'MinimumStockLevelFilterOptionQuickUltraComboBox
    '
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance28.BackColor = System.Drawing.SystemColors.Window
    Appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Appearance = Appearance28
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance25.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance25.BorderColor = System.Drawing.SystemColors.Window
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.GroupByBox.Appearance = Appearance25
    Appearance26.ForeColor = System.Drawing.SystemColors.GrayText
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance26
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance27.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance27.BackColor2 = System.Drawing.SystemColors.Control
    Appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance27.ForeColor = System.Drawing.SystemColors.GrayText
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance27
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance36.BackColor = System.Drawing.SystemColors.Window
    Appearance36.ForeColor = System.Drawing.SystemColors.ControlText
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance36
    Appearance31.BackColor = System.Drawing.SystemColors.Highlight
    Appearance31.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance31
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance30.BackColor = System.Drawing.SystemColors.Window
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance30
    Appearance29.BorderColor = System.Drawing.Color.Silver
    Appearance29.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.CellAppearance = Appearance29
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance33.BackColor = System.Drawing.SystemColors.Control
    Appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance33.BorderColor = System.Drawing.SystemColors.Window
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance33
    Appearance35.TextHAlignAsString = "Left"
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.HeaderAppearance = Appearance35
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance34.BackColor = System.Drawing.SystemColors.Window
    Appearance34.BorderColor = System.Drawing.Color.Silver
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.RowAppearance = Appearance34
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance32.BackColor = System.Drawing.SystemColors.ControlLight
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance32
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.DropDownWidth = 296
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.IsMandatory = False
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.IsReadonlyForExistingRecord = False
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.IsReadonlyForNewRecord = False
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Location = New System.Drawing.Point(4, 4)
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Name = "MinimumStockLevelFilterOptionQuickUltraComboBox"
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.Size = New System.Drawing.Size(296, 22)
    Me.MinimumStockLevelFilterOptionQuickUltraComboBox.TabIndex = 3
    '
    'MinimumStockLevelQuickSpread
    '
    Me.MinimumStockLevelQuickSpread.AccessibleDescription = "Quick_Spread"
    Me.MinimumStockLevelQuickSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.MinimumStockLevelQuickSpread.AutoNewRow = True
    Me.MinimumStockLevelQuickSpread.EditModePermanent = True
    Me.MinimumStockLevelQuickSpread.EditModeReplace = True
    Me.MinimumStockLevelQuickSpread.FocusRenderer = DefaultFocusIndicatorRenderer2
    Me.MinimumStockLevelQuickSpread.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.MinimumStockLevelQuickSpread.HorizontalScrollBar.Name = ""
    Me.MinimumStockLevelQuickSpread.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer1
    Me.MinimumStockLevelQuickSpread.HorizontalScrollBar.TabIndex = 2
    Me.MinimumStockLevelQuickSpread.Location = New System.Drawing.Point(4, 32)
    Me.MinimumStockLevelQuickSpread.Name = "MinimumStockLevelQuickSpread"
    Me.MinimumStockLevelQuickSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.MinimumStockLevelSheet})
    Me.MinimumStockLevelQuickSpread.Size = New System.Drawing.Size(600, 270)
    Me.MinimumStockLevelQuickSpread.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.MinimumStockLevelQuickSpread.TabIndex = 2
    Me.MinimumStockLevelQuickSpread.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.MinimumStockLevelQuickSpread.VerticalScrollBar.Name = ""
    Me.MinimumStockLevelQuickSpread.VerticalScrollBar.Renderer = DefaultScrollBarRenderer2
    Me.MinimumStockLevelQuickSpread.VerticalScrollBar.TabIndex = 3
    Me.MinimumStockLevelQuickSpread.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'MinimumStockLevelSheet
    '
    Me.MinimumStockLevelSheet.Reset()
    Me.MinimumStockLevelSheet.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.MinimumStockLevelSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.MinimumStockLevelSheet.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.MinimumStockLevelSheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
    Me.MinimumStockLevelSheet.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.MinimumStockLevelSheet.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
    Me.MinimumStockLevelSheet.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.MinimumStockLevelSheet.SheetCornerStyle.Parent = "CornerDefault"
    Me.MinimumStockLevelSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'CompanyCheckedListBox1
    '
    Me.CompanyCheckedListBox1.Location = New System.Drawing.Point(188, 45)
    Me.CompanyCheckedListBox1.Name = "CompanyCheckedListBox1"
    Me.CompanyCheckedListBox1.Size = New System.Drawing.Size(244, 21)
    Me.CompanyCheckedListBox1.TabIndex = 8
    '
    'ExportToExcelButton
    '
    Me.ExportToExcelButton.Location = New System.Drawing.Point(516, 44)
    Me.ExportToExcelButton.Name = "ExportToExcelButton"
    Me.ExportToExcelButton.Size = New System.Drawing.Size(92, 23)
    Me.ExportToExcelButton.TabIndex = 9
    Me.ExportToExcelButton.Text = "Export To Excel"
    Me.ExportToExcelButton.UseVisualStyleBackColor = True
    '
    'ItemComboBox
    '
    Me.ItemComboBox.Location = New System.Drawing.Point(40, 44)
    Me.ItemComboBox.Name = "ItemComboBox"
    Me.ItemComboBox.Size = New System.Drawing.Size(144, 22)
    Me.ItemComboBox.TabIndex = 10
    '
    'StockInquiryForm
    '
    Me.AcceptButton = Me.ShowButton
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(625, 440)
    Me.Controls.Add(Me.ItemComboBox)
    Me.Controls.Add(Me.ExportToExcelButton)
    Me.Controls.Add(Me.CompanyCheckedListBox1)
    Me.Controls.Add(Me.TabControl1)
    Me.Controls.Add(Me.ShowButton)
    Me.Controls.Add(Me.ItemLabel)
    Me.Name = "StockInquiryForm"
    Me.Text = "StockInquiryForm"
    Me.Controls.SetChildIndex(Me.ItemLabel, 0)
    Me.Controls.SetChildIndex(Me.ShowButton, 0)
    Me.Controls.SetChildIndex(Me.TabControl1, 0)
    Me.Controls.SetChildIndex(Me.CompanyCheckedListBox1, 0)
    Me.Controls.SetChildIndex(Me.ExportToExcelButton, 0)
    Me.Controls.SetChildIndex(Me.ItemComboBox, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.StockQuickSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.StockQuickSpread_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabControl1.ResumeLayout(False)
    Me.StockTabPage.ResumeLayout(False)
    Me.MinimumLevelTabPage.ResumeLayout(False)
    Me.MinimumLevelTabPage.PerformLayout()
    CType(Me.MinimumStockLevelFilterOptionQuickUltraComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.MinimumStockLevelQuickSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.MinimumStockLevelSheet, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents StockQuickSpread_Sheet1 As QuickBusinessControls.ItemSpreadView
  Friend WithEvents ItemLabel As QuickControls.Quick_Label
  Friend WithEvents ShowButton As QuickControls.Quick_Button
  Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents StockTabPage As System.Windows.Forms.TabPage
  Friend WithEvents MinimumLevelTabPage As System.Windows.Forms.TabPage
  Friend WithEvents StockQuickSpread As QuickBusinessControls.ItemSpread
  Friend WithEvents MinimumStockLevelQuickSpread As QuickBusinessControls.ItemSpread
  Friend WithEvents MinimumStockLevelSheet As QuickBusinessControls.ItemSpreadView
  Friend WithEvents CompanyCheckedListBox1 As QuickBusinessControls.CompanyCheckedListBox
  Friend WithEvents MinimumStockLevelFilterOptionQuickUltraComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents ExportToExcelButton As QuickControls.Quick_Button
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents ItemComboBox As QuickBusinessControls.MultiComboBox
End Class
