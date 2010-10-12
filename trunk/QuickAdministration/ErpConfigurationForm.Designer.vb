<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ErpConfigurationForm
  Inherits QuickBaseForms.ParentToolbarForm

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
  'Do not modify it using the code editor.ck
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Me.TabControl1 = New System.Windows.Forms.TabControl
    Me.TabPage1 = New System.Windows.Forms.TabPage
    Me.ConfigurationValueLabel = New QuickControls.Quick_Label
    Me.CompanyLabel = New QuickControls.Quick_Label
    Me.SettingIDLabel = New QuickControls.Quick_Label
    Me.SettingIDComboBox = New QuickControls.Quick_UltraComboBox
    Me.CompanyComboBox1 = New QuickBusinessControls.CompanyComboBox
    Me.SetttingValueTextBox = New QuickControls.Quick_TextBox
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    CType(Me.SettingIDComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TabControl1
    '
    Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl1.Controls.Add(Me.TabPage1)
    Me.TabControl1.Location = New System.Drawing.Point(4, 44)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(460, 237)
    Me.TabControl1.TabIndex = 0
    Me.TabControl1.TabStop = False
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.ConfigurationValueLabel)
    Me.TabPage1.Controls.Add(Me.CompanyLabel)
    Me.TabPage1.Controls.Add(Me.SettingIDLabel)
    Me.TabPage1.Controls.Add(Me.SettingIDComboBox)
    Me.TabPage1.Controls.Add(Me.CompanyComboBox1)
    Me.TabPage1.Controls.Add(Me.SetttingValueTextBox)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(452, 211)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Reports"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'ConfigurationValueLabel
    '
    Me.ConfigurationValueLabel.AutoSize = True
    Me.ConfigurationValueLabel.Location = New System.Drawing.Point(8, 44)
    Me.ConfigurationValueLabel.Name = "ConfigurationValueLabel"
    Me.ConfigurationValueLabel.Size = New System.Drawing.Size(31, 13)
    Me.ConfigurationValueLabel.TabIndex = 2
    Me.ConfigurationValueLabel.Text = "Text:"
    '
    'CompanyLabel
    '
    Me.CompanyLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CompanyLabel.AutoSize = True
    Me.CompanyLabel.Location = New System.Drawing.Point(8, 185)
    Me.CompanyLabel.Name = "CompanyLabel"
    Me.CompanyLabel.Size = New System.Drawing.Size(44, 13)
    Me.CompanyLabel.TabIndex = 4
    Me.CompanyLabel.Text = "Branch:"
    Me.CompanyLabel.Visible = False
    '
    'SettingIDLabel
    '
    Me.SettingIDLabel.AutoSize = True
    Me.SettingIDLabel.Location = New System.Drawing.Point(8, 12)
    Me.SettingIDLabel.Name = "SettingIDLabel"
    Me.SettingIDLabel.Size = New System.Drawing.Size(72, 13)
    Me.SettingIDLabel.TabIndex = 0
    Me.SettingIDLabel.Text = "Configuration:"
    '
    'SettingIDComboBox
    '
    Me.SettingIDComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SettingIDComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.SettingIDComboBox.DisplayLayout.Appearance = Appearance1
    Me.SettingIDComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.SettingIDComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.SettingIDComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.SettingIDComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.SettingIDComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.SettingIDComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.SettingIDComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.SettingIDComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.SettingIDComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.SettingIDComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.SettingIDComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.SettingIDComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.SettingIDComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.SettingIDComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.SettingIDComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.SettingIDComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.SettingIDComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlign = Infragistics.Win.HAlign.Left
    Me.SettingIDComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.SettingIDComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.SettingIDComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.SettingIDComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.SettingIDComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.SettingIDComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.SettingIDComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.SettingIDComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.SettingIDComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.SettingIDComboBox.DisplayMember = ""
    Me.SettingIDComboBox.Location = New System.Drawing.Point(84, 8)
    Me.SettingIDComboBox.Name = "SettingIDComboBox"
    Me.SettingIDComboBox.Size = New System.Drawing.Size(360, 22)
    Me.SettingIDComboBox.TabIndex = 1
    Me.SettingIDComboBox.ValueMember = ""
    '
    'CompanyComboBox1
    '
    Me.CompanyComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CompanyComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.CompanyComboBox1.CompanyID = CType(0, Short)
    Appearance13.BackColor = System.Drawing.SystemColors.Window
    Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.CompanyComboBox1.DisplayLayout.Appearance = Appearance13
    Me.CompanyComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.CompanyComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance14.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.Appearance = Appearance14
    Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance16.BackColor2 = System.Drawing.SystemColors.Control
    Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox1.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
    Me.CompanyComboBox1.DisplayLayout.MaxColScrollRegions = 1
    Me.CompanyComboBox1.DisplayLayout.MaxRowScrollRegions = 1
    Appearance17.BackColor = System.Drawing.SystemColors.Window
    Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CompanyComboBox1.DisplayLayout.Override.ActiveCellAppearance = Appearance17
    Appearance18.BackColor = System.Drawing.SystemColors.Highlight
    Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.CompanyComboBox1.DisplayLayout.Override.ActiveRowAppearance = Appearance18
    Me.CompanyComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.CompanyComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance19.BackColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox1.DisplayLayout.Override.CardAreaAppearance = Appearance19
    Appearance20.BorderColor = System.Drawing.Color.Silver
    Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.CompanyComboBox1.DisplayLayout.Override.CellAppearance = Appearance20
    Me.CompanyComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.CompanyComboBox1.DisplayLayout.Override.CellPadding = 0
    Appearance21.BackColor = System.Drawing.SystemColors.Control
    Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance21.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox1.DisplayLayout.Override.GroupByRowAppearance = Appearance21
    Appearance22.TextHAlign = Infragistics.Win.HAlign.Left
    Me.CompanyComboBox1.DisplayLayout.Override.HeaderAppearance = Appearance22
    Me.CompanyComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.CompanyComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance23.BackColor = System.Drawing.SystemColors.Window
    Appearance23.BorderColor = System.Drawing.Color.Silver
    Me.CompanyComboBox1.DisplayLayout.Override.RowAppearance = Appearance23
    Me.CompanyComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
    Me.CompanyComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
    Me.CompanyComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.CompanyComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.CompanyComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.CompanyComboBox1.DisplayMember = ""
    Me.CompanyComboBox1.Location = New System.Drawing.Point(84, 181)
    Me.CompanyComboBox1.Name = "CompanyComboBox1"
    Me.CompanyComboBox1.Size = New System.Drawing.Size(156, 22)
    Me.CompanyComboBox1.TabIndex = 5
    Me.CompanyComboBox1.Text = "CompanyComboBox1"
    Me.CompanyComboBox1.ValueMember = ""
    Me.CompanyComboBox1.Visible = False
    '
    'SetttingValueTextBox
    '
    Me.SetttingValueTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SetttingValueTextBox.IntegerNumber = 0
    Me.SetttingValueTextBox.Location = New System.Drawing.Point(84, 40)
    Me.SetttingValueTextBox.Multiline = True
    Me.SetttingValueTextBox.Name = "SetttingValueTextBox"
    Me.SetttingValueTextBox.PercentNumber = 0
    Me.SetttingValueTextBox.Size = New System.Drawing.Size(360, 133)
    Me.SetttingValueTextBox.TabIndex = 3
    Me.SetttingValueTextBox.Text = "0"
    Me.SetttingValueTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'ErpConfigurationForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(466, 308)
    Me.Controls.Add(Me.TabControl1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "ErpConfigurationForm"
    Me.Text = "`"
    Me.Controls.SetChildIndex(Me.TabControl1, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage1.PerformLayout()
    CType(Me.SettingIDComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents SetttingValueTextBox As QuickControls.Quick_TextBox
  Friend WithEvents SettingIDComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents CompanyComboBox1 As QuickBusinessControls.CompanyComboBox
  Friend WithEvents SettingIDLabel As QuickControls.Quick_Label
  Friend WithEvents CompanyLabel As QuickControls.Quick_Label
  Friend WithEvents ConfigurationValueLabel As QuickControls.Quick_Label
End Class
