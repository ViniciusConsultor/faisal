<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColorForm
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
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.CompanyComboBox = New QuickBusinessControls.CompanyComboBox
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.ColorIDTextBox = New QuickControls.Quick_TextBox
    Me.ColorDescTextBox = New QuickControls.Quick_TextBox
    Me.ColorCodeTextBox = New QuickControls.Quick_TextBox
    Me.ColorLabel = New QuickControls.Quick_Label
    Me.Quick_Label8 = New QuickControls.Quick_Label
    Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(254, 81)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(59, 13)
    Me.Quick_Label4.TabIndex = 57
    Me.Quick_Label4.Text = "Color Code"
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(10, 111)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(60, 13)
    Me.Quick_Label3.TabIndex = 56
    Me.Quick_Label3.Text = "Description"
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(10, 81)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(45, 13)
    Me.Quick_Label2.TabIndex = 55
    Me.Quick_Label2.Text = "Color ID"
    '
    'CompanyComboBox
    '
    Me.CompanyComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.CompanyComboBox.CompanyID = CType(0, Short)
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.CompanyComboBox.DisplayLayout.Appearance = Appearance1
    Me.CompanyComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.CompanyComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.CompanyComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.CompanyComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.CompanyComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CompanyComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.CompanyComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.CompanyComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.CompanyComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.CompanyComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.CompanyComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.CompanyComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlignAsString = "Left"
    Me.CompanyComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.CompanyComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.CompanyComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.CompanyComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.CompanyComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.CompanyComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.CompanyComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.CompanyComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.CompanyComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.CompanyComboBox.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
    Me.CompanyComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.CompanyComboBox.DropDownWidth = 121
    Me.CompanyComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.CompanyComboBox.IsMandatory = False
    Me.CompanyComboBox.IsReadonlyForExistingRecord = False
    Me.CompanyComboBox.IsReadonlyForNewRecord = False
    Me.CompanyComboBox.Location = New System.Drawing.Point(115, 46)
    Me.CompanyComboBox.Name = "CompanyComboBox"
    Me.CompanyComboBox.Size = New System.Drawing.Size(121, 22)
    Me.CompanyComboBox.TabIndex = 0
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(10, 51)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(51, 13)
    Me.Quick_Label1.TabIndex = 54
    Me.Quick_Label1.Text = "Company"
    '
    'ColorIDTextBox
    '
    Me.ColorIDTextBox.DefaultValue = ""
    Me.ColorIDTextBox.IntegerNumber = 0
    Me.ColorIDTextBox.IsMandatory = False
    Me.ColorIDTextBox.IsReadonlyForExistingRecord = False
    Me.ColorIDTextBox.IsReadonlyForNewRecord = False
    Me.ColorIDTextBox.Location = New System.Drawing.Point(115, 77)
    Me.ColorIDTextBox.Name = "ColorIDTextBox"
    Me.ColorIDTextBox.PercentNumber = 0
    Me.ColorIDTextBox.ReadOnly = True
    Me.ColorIDTextBox.Size = New System.Drawing.Size(121, 20)
    Me.ColorIDTextBox.TabIndex = 51
    Me.ColorIDTextBox.TabStop = False
    Me.ColorIDTextBox.Text = "0"
    Me.ColorIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'ColorDescTextBox
    '
    Me.ColorDescTextBox.DefaultValue = ""
    Me.ColorDescTextBox.IntegerNumber = 0
    Me.ColorDescTextBox.IsMandatory = False
    Me.ColorDescTextBox.IsReadonlyForExistingRecord = False
    Me.ColorDescTextBox.IsReadonlyForNewRecord = False
    Me.ColorDescTextBox.Location = New System.Drawing.Point(115, 107)
    Me.ColorDescTextBox.Name = "ColorDescTextBox"
    Me.ColorDescTextBox.PercentNumber = 0
    Me.ColorDescTextBox.Size = New System.Drawing.Size(368, 20)
    Me.ColorDescTextBox.TabIndex = 2
    Me.ColorDescTextBox.Text = "0"
    Me.ColorDescTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'ColorCodeTextBox
    '
    Me.ColorCodeTextBox.AcceptsReturn = True
    Me.ColorCodeTextBox.DefaultValue = ""
    Me.ColorCodeTextBox.IntegerNumber = 0
    Me.ColorCodeTextBox.IsMandatory = False
    Me.ColorCodeTextBox.IsReadonlyForExistingRecord = False
    Me.ColorCodeTextBox.IsReadonlyForNewRecord = False
    Me.ColorCodeTextBox.Location = New System.Drawing.Point(362, 77)
    Me.ColorCodeTextBox.Name = "ColorCodeTextBox"
    Me.ColorCodeTextBox.PercentNumber = 0
    Me.ColorCodeTextBox.Size = New System.Drawing.Size(121, 20)
    Me.ColorCodeTextBox.TabIndex = 1
    Me.ColorCodeTextBox.Text = "0"
    Me.ColorCodeTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'ColorLabel
    '
    Me.ColorLabel.AllowClearValue = False
    Me.ColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.ColorLabel.DefaultValue = ""
    Me.ColorLabel.Location = New System.Drawing.Point(211, 133)
    Me.ColorLabel.Name = "ColorLabel"
    Me.ColorLabel.Size = New System.Drawing.Size(25, 25)
    Me.ColorLabel.TabIndex = 3
    '
    'Quick_Label8
    '
    Me.Quick_Label8.AllowClearValue = False
    Me.Quick_Label8.AutoSize = True
    Me.Quick_Label8.DefaultValue = ""
    Me.Quick_Label8.Location = New System.Drawing.Point(12, 141)
    Me.Quick_Label8.Name = "Quick_Label8"
    Me.Quick_Label8.Size = New System.Drawing.Size(143, 13)
    Me.Quick_Label8.TabIndex = 59
    Me.Quick_Label8.Text = "Click on box and select color"
    '
    'ColorForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(493, 198)
    Me.Controls.Add(Me.Quick_Label8)
    Me.Controls.Add(Me.ColorLabel)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.CompanyComboBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Controls.Add(Me.ColorIDTextBox)
    Me.Controls.Add(Me.ColorDescTextBox)
    Me.Controls.Add(Me.ColorCodeTextBox)
    Me.Name = "ColorForm"
    Me.Text = "Colors"
    Me.Controls.SetChildIndex(Me.ColorCodeTextBox, 0)
    Me.Controls.SetChildIndex(Me.ColorDescTextBox, 0)
    Me.Controls.SetChildIndex(Me.ColorIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.CompanyComboBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    Me.Controls.SetChildIndex(Me.ColorLabel, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label8, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents CompanyComboBox As QuickBusinessControls.CompanyComboBox
  Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
  Friend WithEvents ColorIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents ColorDescTextBox As QuickControls.Quick_TextBox
  Friend WithEvents ColorCodeTextBox As QuickControls.Quick_TextBox
  Friend WithEvents ColorLabel As QuickControls.Quick_Label
  Friend WithEvents Quick_Label8 As QuickControls.Quick_Label
  Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
End Class
