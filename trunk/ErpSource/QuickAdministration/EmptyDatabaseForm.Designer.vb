<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmptyDatabaseForm
  Inherits QuickBaseForms.ParentBasicForm

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
    Dim TipAppearance3 As FarPoint.Win.Spread.TipAppearance = New FarPoint.Win.Spread.TipAppearance
    Dim CheckBoxCellType3 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
    Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Me.Quick_Spread1 = New QuickControls.Quick_Spread
    Me.Quick_Spread1_Sheet1 = New FarPoint.Win.Spread.SheetView
    Me.CompanyLabel = New QuickControls.Quick_Label
    Me.EmptyDatabaseButton = New QuickControls.Quick_Button
    Me.CompanyComboBox1 = New QuickBusinessControls.CompanyComboBox
    CType(Me.Quick_Spread1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread1_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Spread1
    '
    Me.Quick_Spread1.AccessibleDescription = "Quick_Spread, Sheet1, Row 0, Column 0, All Records"
    Me.Quick_Spread1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Spread1.AutoNewRow = True
    Me.Quick_Spread1.BackColor = System.Drawing.SystemColors.Control
    Me.Quick_Spread1.EditModePermanent = True
    Me.Quick_Spread1.EditModeReplace = True
    Me.Quick_Spread1.Location = New System.Drawing.Point(6, 34)
    Me.Quick_Spread1.Name = "Quick_Spread1"
    Me.Quick_Spread1.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.Quick_Spread1_Sheet1})
    Me.Quick_Spread1.Size = New System.Drawing.Size(488, 356)
    Me.Quick_Spread1.TabIndex = 0
    TipAppearance3.BackColor = System.Drawing.SystemColors.Info
    TipAppearance3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    TipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText
    Me.Quick_Spread1.TextTipAppearance = TipAppearance3
    '
    'Quick_Spread1_Sheet1
    '
    Me.Quick_Spread1_Sheet1.Reset()
    Me.Quick_Spread1_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.Quick_Spread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.Quick_Spread1_Sheet1.ColumnCount = 2
    Me.Quick_Spread1_Sheet1.RowCount = 2
    Me.Quick_Spread1_Sheet1.AutoUpdateNotes = True
    Me.Quick_Spread1_Sheet1.Cells.Get(0, 0).Value = "All Records"
    Me.Quick_Spread1_Sheet1.Cells.Get(0, 1).Value = "All Records"
    Me.Quick_Spread1_Sheet1.Cells.Get(1, 1).Value = "    Transfers"
    Me.Quick_Spread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " "
    Me.Quick_Spread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = " "
    Me.Quick_Spread1_Sheet1.Columns.Get(0).CellType = CheckBoxCellType3
    Me.Quick_Spread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
    Me.Quick_Spread1_Sheet1.Columns.Get(0).Label = " "
    Me.Quick_Spread1_Sheet1.Columns.Get(0).Width = 33.0!
    Me.Quick_Spread1_Sheet1.Columns.Get(1).Label = " "
    Me.Quick_Spread1_Sheet1.Columns.Get(1).Width = 141.0!
    Me.Quick_Spread1_Sheet1.RowHeader.Columns.Default.Resizable = False
    Me.Quick_Spread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'CompanyLabel
    '
    Me.CompanyLabel.AutoSize = True
    Me.CompanyLabel.Location = New System.Drawing.Point(12, 11)
    Me.CompanyLabel.Name = "CompanyLabel"
    Me.CompanyLabel.Size = New System.Drawing.Size(54, 13)
    Me.CompanyLabel.TabIndex = 1
    Me.CompanyLabel.Text = "Company:"
    '
    'EmptyDatabaseButton
    '
    Me.EmptyDatabaseButton.Location = New System.Drawing.Point(232, 6)
    Me.EmptyDatabaseButton.Name = "EmptyDatabaseButton"
    Me.EmptyDatabaseButton.Size = New System.Drawing.Size(98, 22)
    Me.EmptyDatabaseButton.TabIndex = 2
    Me.EmptyDatabaseButton.Text = "Delete Records"
    Me.EmptyDatabaseButton.UseVisualStyleBackColor = True
    '
    'CompanyComboBox1
    '
    Me.CompanyComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.CompanyComboBox1.CompanyID = CType(0, Short)
    Appearance25.BackColor = System.Drawing.SystemColors.Window
    Appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.CompanyComboBox1.DisplayLayout.Appearance = Appearance25
    Me.CompanyComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.CompanyComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance26.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.Appearance = Appearance26
    Appearance27.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance27
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance28.BackColor2 = System.Drawing.SystemColors.Control
    Appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance28.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.PromptAppearance = Appearance28
    Me.CompanyComboBox1.DisplayLayout.MaxColScrollRegions = 1
    Me.CompanyComboBox1.DisplayLayout.MaxRowScrollRegions = 1
    Appearance29.BackColor = System.Drawing.SystemColors.Window
    Appearance29.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CompanyComboBox1.DisplayLayout.Override.ActiveCellAppearance = Appearance29
    Appearance30.BackColor = System.Drawing.SystemColors.Highlight
    Appearance30.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.CompanyComboBox1.DisplayLayout.Override.ActiveRowAppearance = Appearance30
    Me.CompanyComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.CompanyComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance31.BackColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox1.DisplayLayout.Override.CardAreaAppearance = Appearance31
    Appearance32.BorderColor = System.Drawing.Color.Silver
    Appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.CompanyComboBox1.DisplayLayout.Override.CellAppearance = Appearance32
    Me.CompanyComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.CompanyComboBox1.DisplayLayout.Override.CellPadding = 0
    Appearance33.BackColor = System.Drawing.SystemColors.Control
    Appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance33.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox1.DisplayLayout.Override.GroupByRowAppearance = Appearance33
    Appearance34.TextHAlign = Infragistics.Win.HAlign.Left
    Me.CompanyComboBox1.DisplayLayout.Override.HeaderAppearance = Appearance34
    Me.CompanyComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.CompanyComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance35.BackColor = System.Drawing.SystemColors.Window
    Appearance35.BorderColor = System.Drawing.Color.Silver
    Me.CompanyComboBox1.DisplayLayout.Override.RowAppearance = Appearance35
    Me.CompanyComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance36.BackColor = System.Drawing.SystemColors.ControlLight
    Me.CompanyComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance36
    Me.CompanyComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.CompanyComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.CompanyComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.CompanyComboBox1.DisplayMember = ""
    Me.CompanyComboBox1.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.CompanyComboBox1.Location = New System.Drawing.Point(70, 6)
    Me.CompanyComboBox1.Name = "CompanyComboBox1"
    Me.CompanyComboBox1.Size = New System.Drawing.Size(156, 22)
    Me.CompanyComboBox1.TabIndex = 3
    Me.CompanyComboBox1.ValueMember = ""
    '
    'EmptyDatabaseForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(500, 418)
    Me.Controls.Add(Me.CompanyComboBox1)
    Me.Controls.Add(Me.EmptyDatabaseButton)
    Me.Controls.Add(Me.CompanyLabel)
    Me.Controls.Add(Me.Quick_Spread1)
    Me.Name = "EmptyDatabaseForm"
    Me.Text = "Empty Database"
    Me.Controls.SetChildIndex(Me.Quick_Spread1, 0)
    Me.Controls.SetChildIndex(Me.CompanyLabel, 0)
    Me.Controls.SetChildIndex(Me.EmptyDatabaseButton, 0)
    Me.Controls.SetChildIndex(Me.CompanyComboBox1, 0)
    CType(Me.Quick_Spread1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread1_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Quick_Spread1 As QuickControls.Quick_Spread
  Friend WithEvents Quick_Spread1_Sheet1 As FarPoint.Win.Spread.SheetView
  Friend WithEvents CompanyLabel As QuickControls.Quick_Label
  Friend WithEvents EmptyDatabaseButton As QuickControls.Quick_Button
  Friend WithEvents CompanyComboBox1 As QuickBusinessControls.CompanyComboBox
End Class
