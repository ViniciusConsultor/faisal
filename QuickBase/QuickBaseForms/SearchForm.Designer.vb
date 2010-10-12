<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchForm
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
    Me.SearchItemLabel = New QuickControls.Quick_Label
    Me.SearchItemComboBox = New QuickControls.Quick_UltraComboBox
    Me.ComparisonTypeLabel = New QuickControls.Quick_Label
    Me.SearchComparisonTypeComboBox = New QuickControls.Quick_UltraComboBox
    Me.SearchValueLabel = New QuickControls.Quick_Label
    Me.SearchValueTextBox = New QuickControls.Quick_TextBox
    Me.Quick_GroupBox1 = New QuickControls.Quick_GroupBox
    Me.CancelSearchButton = New QuickControls.Quick_Button
    Me.OkButton = New QuickControls.Quick_Button
    CType(Me.SearchItemComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.SearchComparisonTypeComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Quick_GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'SearchItemLabel
    '
    Me.SearchItemLabel.AllowClearValue = False
    Me.SearchItemLabel.AutoSize = True
    Me.SearchItemLabel.DefaultValue = ""
    Me.SearchItemLabel.Location = New System.Drawing.Point(12, 24)
    Me.SearchItemLabel.Name = "SearchItemLabel"
    Me.SearchItemLabel.Size = New System.Drawing.Size(67, 13)
    Me.SearchItemLabel.TabIndex = 4
    Me.SearchItemLabel.Text = "Search Item:"
    '
    'SearchItemComboBox
    '
    Me.SearchItemComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SearchItemComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.SearchItemComboBox.DisplayLayout.Appearance = Appearance1
    Me.SearchItemComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.SearchItemComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.SearchItemComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.SearchItemComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.SearchItemComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.SearchItemComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.SearchItemComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.SearchItemComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.SearchItemComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.SearchItemComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.SearchItemComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.SearchItemComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.SearchItemComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.SearchItemComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.SearchItemComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.SearchItemComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.SearchItemComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlign = Infragistics.Win.HAlign.Left
    Me.SearchItemComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.SearchItemComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.SearchItemComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.SearchItemComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.SearchItemComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.SearchItemComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.SearchItemComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.SearchItemComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.SearchItemComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.SearchItemComboBox.DisplayMember = ""
    Me.SearchItemComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.SearchItemComboBox.DropDownWidth = 200
    Me.SearchItemComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.SearchItemComboBox.Location = New System.Drawing.Point(88, 20)
    Me.SearchItemComboBox.Name = "SearchItemComboBox"
    Me.SearchItemComboBox.Size = New System.Drawing.Size(200, 22)
    Me.SearchItemComboBox.TabIndex = 5
    Me.SearchItemComboBox.ValueMember = ""
    '
    'ComparisonTypeLabel
    '
    Me.ComparisonTypeLabel.AllowClearValue = False
    Me.ComparisonTypeLabel.AutoSize = True
    Me.ComparisonTypeLabel.DefaultValue = ""
    Me.ComparisonTypeLabel.Location = New System.Drawing.Point(12, 52)
    Me.ComparisonTypeLabel.Name = "ComparisonTypeLabel"
    Me.ComparisonTypeLabel.Size = New System.Drawing.Size(65, 13)
    Me.ComparisonTypeLabel.TabIndex = 6
    Me.ComparisonTypeLabel.Text = "Comparison:"
    '
    'SearchComparisonTypeComboBox
    '
    Me.SearchComparisonTypeComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SearchComparisonTypeComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance13.BackColor = System.Drawing.SystemColors.Window
    Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.SearchComparisonTypeComboBox.DisplayLayout.Appearance = Appearance13
    Me.SearchComparisonTypeComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.SearchComparisonTypeComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance14.BorderColor = System.Drawing.SystemColors.Window
    Me.SearchComparisonTypeComboBox.DisplayLayout.GroupByBox.Appearance = Appearance14
    Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
    Me.SearchComparisonTypeComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
    Me.SearchComparisonTypeComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance16.BackColor2 = System.Drawing.SystemColors.Control
    Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
    Me.SearchComparisonTypeComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
    Me.SearchComparisonTypeComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.SearchComparisonTypeComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance17.BackColor = System.Drawing.SystemColors.Window
    Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance17
    Appearance18.BackColor = System.Drawing.SystemColors.Highlight
    Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance18
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance19.BackColor = System.Drawing.SystemColors.Window
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance19
    Appearance20.BorderColor = System.Drawing.Color.Silver
    Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.CellAppearance = Appearance20
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance21.BackColor = System.Drawing.SystemColors.Control
    Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance21.BorderColor = System.Drawing.SystemColors.Window
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance21
    Appearance22.TextHAlign = Infragistics.Win.HAlign.Left
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.HeaderAppearance = Appearance22
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance23.BackColor = System.Drawing.SystemColors.Window
    Appearance23.BorderColor = System.Drawing.Color.Silver
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.RowAppearance = Appearance23
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
    Me.SearchComparisonTypeComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
    Me.SearchComparisonTypeComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.SearchComparisonTypeComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.SearchComparisonTypeComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.SearchComparisonTypeComboBox.DisplayMember = ""
    Me.SearchComparisonTypeComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.SearchComparisonTypeComboBox.DropDownWidth = 200
    Me.SearchComparisonTypeComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.SearchComparisonTypeComboBox.Location = New System.Drawing.Point(88, 48)
    Me.SearchComparisonTypeComboBox.Name = "SearchComparisonTypeComboBox"
    Me.SearchComparisonTypeComboBox.Size = New System.Drawing.Size(200, 22)
    Me.SearchComparisonTypeComboBox.TabIndex = 7
    Me.SearchComparisonTypeComboBox.ValueMember = ""
    '
    'SearchValueLabel
    '
    Me.SearchValueLabel.AllowClearValue = False
    Me.SearchValueLabel.AutoSize = True
    Me.SearchValueLabel.DefaultValue = ""
    Me.SearchValueLabel.Location = New System.Drawing.Point(12, 80)
    Me.SearchValueLabel.Name = "SearchValueLabel"
    Me.SearchValueLabel.Size = New System.Drawing.Size(37, 13)
    Me.SearchValueLabel.TabIndex = 8
    Me.SearchValueLabel.Text = "Value:"
    '
    'SearchValueTextBox
    '
    Me.SearchValueTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SearchValueTextBox.DefaultValue = ""
    Me.SearchValueTextBox.IntegerNumber = 0
    Me.SearchValueTextBox.Location = New System.Drawing.Point(88, 76)
    Me.SearchValueTextBox.Name = "SearchValueTextBox"
    Me.SearchValueTextBox.PercentNumber = 0
    Me.SearchValueTextBox.Size = New System.Drawing.Size(200, 20)
    Me.SearchValueTextBox.TabIndex = 9
    Me.SearchValueTextBox.Text = "0"
    Me.SearchValueTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'Quick_GroupBox1
    '
    Me.Quick_GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_GroupBox1.Controls.Add(Me.CancelSearchButton)
    Me.Quick_GroupBox1.Controls.Add(Me.OkButton)
    Me.Quick_GroupBox1.Controls.Add(Me.SearchItemComboBox)
    Me.Quick_GroupBox1.Controls.Add(Me.SearchValueTextBox)
    Me.Quick_GroupBox1.Controls.Add(Me.SearchItemLabel)
    Me.Quick_GroupBox1.Controls.Add(Me.SearchValueLabel)
    Me.Quick_GroupBox1.Controls.Add(Me.SearchComparisonTypeComboBox)
    Me.Quick_GroupBox1.Controls.Add(Me.ComparisonTypeLabel)
    Me.Quick_GroupBox1.Location = New System.Drawing.Point(4, 8)
    Me.Quick_GroupBox1.Name = "Quick_GroupBox1"
    Me.Quick_GroupBox1.Size = New System.Drawing.Size(296, 144)
    Me.Quick_GroupBox1.TabIndex = 10
    Me.Quick_GroupBox1.TabStop = False
    '
    'CancelSearchButton
    '
    Me.CancelSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Top
    Me.CancelSearchButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.CancelSearchButton.Location = New System.Drawing.Point(151, 112)
    Me.CancelSearchButton.Name = "CancelSearchButton"
    Me.CancelSearchButton.Size = New System.Drawing.Size(75, 23)
    Me.CancelSearchButton.TabIndex = 11
    Me.CancelSearchButton.Text = "&Cancel"
    Me.CancelSearchButton.UseVisualStyleBackColor = True
    '
    'OkButton
    '
    Me.OkButton.Anchor = System.Windows.Forms.AnchorStyles.Top
    Me.OkButton.Location = New System.Drawing.Point(71, 112)
    Me.OkButton.Name = "OkButton"
    Me.OkButton.Size = New System.Drawing.Size(75, 23)
    Me.OkButton.TabIndex = 10
    Me.OkButton.Text = "&OK"
    Me.OkButton.UseVisualStyleBackColor = True
    '
    'SearchForm
    '
    Me.AcceptButton = Me.OkButton
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(307, 180)
    Me.Controls.Add(Me.Quick_GroupBox1)
    Me.Name = "SearchForm"
    Me.Text = "Search"
    Me.Controls.SetChildIndex(Me.Quick_GroupBox1, 0)
    CType(Me.SearchItemComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.SearchComparisonTypeComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Quick_GroupBox1.ResumeLayout(False)
    Me.Quick_GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents SearchItemLabel As QuickControls.Quick_Label
  Friend WithEvents SearchItemComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents ComparisonTypeLabel As QuickControls.Quick_Label
  Friend WithEvents SearchComparisonTypeComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents SearchValueLabel As QuickControls.Quick_Label
  Friend WithEvents SearchValueTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_GroupBox1 As QuickControls.Quick_GroupBox
  Friend WithEvents CancelSearchButton As QuickControls.Quick_Button
  Friend WithEvents OkButton As QuickControls.Quick_Button
End Class
