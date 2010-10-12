<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CashFlowAccount
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
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.CompanyComboBox = New QuickBusinessControls.CompanyComboBox
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.CashFlowAccountIDTextBox = New QuickControls.Quick_TextBox
    Me.CashFlowAccountDescTextBox = New QuickControls.Quick_TextBox
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(22, 144)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(60, 13)
    Me.Quick_Label3.TabIndex = 26
    Me.Quick_Label3.Text = "Description"
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(22, 114)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(113, 13)
    Me.Quick_Label2.TabIndex = 25
    Me.Quick_Label2.Text = "Cash Flow Account ID"
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
    Me.CompanyComboBox.DropDownWidth = 227
    Me.CompanyComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.CompanyComboBox.IsMandatory = False
    Me.CompanyComboBox.IsReadonlyForExistingRecord = False
    Me.CompanyComboBox.IsReadonlyForNewRecord = False
    Me.CompanyComboBox.Location = New System.Drawing.Point(143, 79)
    Me.CompanyComboBox.Name = "CompanyComboBox"
    Me.CompanyComboBox.Size = New System.Drawing.Size(227, 22)
    Me.CompanyComboBox.TabIndex = 24
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(22, 84)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(51, 13)
    Me.Quick_Label1.TabIndex = 23
    Me.Quick_Label1.Text = "Company"
    '
    'CashFlowAccountIDTextBox
    '
    Me.CashFlowAccountIDTextBox.DefaultValue = ""
    Me.CashFlowAccountIDTextBox.IntegerNumber = 0
    Me.CashFlowAccountIDTextBox.IsMandatory = False
    Me.CashFlowAccountIDTextBox.IsReadonlyForExistingRecord = False
    Me.CashFlowAccountIDTextBox.IsReadonlyForNewRecord = False
    Me.CashFlowAccountIDTextBox.Location = New System.Drawing.Point(143, 110)
    Me.CashFlowAccountIDTextBox.Name = "CashFlowAccountIDTextBox"
    Me.CashFlowAccountIDTextBox.PercentNumber = 0
    Me.CashFlowAccountIDTextBox.ReadOnly = True
    Me.CashFlowAccountIDTextBox.Size = New System.Drawing.Size(227, 20)
    Me.CashFlowAccountIDTextBox.TabIndex = 22
    Me.CashFlowAccountIDTextBox.TabStop = False
    Me.CashFlowAccountIDTextBox.Text = "0"
    Me.CashFlowAccountIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'CashFlowAccountDescTextBox
    '
    Me.CashFlowAccountDescTextBox.DefaultValue = ""
    Me.CashFlowAccountDescTextBox.IntegerNumber = 0
    Me.CashFlowAccountDescTextBox.IsMandatory = False
    Me.CashFlowAccountDescTextBox.IsReadonlyForExistingRecord = False
    Me.CashFlowAccountDescTextBox.IsReadonlyForNewRecord = False
    Me.CashFlowAccountDescTextBox.Location = New System.Drawing.Point(143, 140)
    Me.CashFlowAccountDescTextBox.Name = "CashFlowAccountDescTextBox"
    Me.CashFlowAccountDescTextBox.PercentNumber = 0
    Me.CashFlowAccountDescTextBox.Size = New System.Drawing.Size(227, 20)
    Me.CashFlowAccountDescTextBox.TabIndex = 21
    Me.CashFlowAccountDescTextBox.Text = "0"
    Me.CashFlowAccountDescTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'CashFlowAccount
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(813, 409)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.CompanyComboBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Controls.Add(Me.CashFlowAccountIDTextBox)
    Me.Controls.Add(Me.CashFlowAccountDescTextBox)
    Me.Name = "CashFlowAccount"
    Me.Text = "Define Cash Flow Account Form"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.Controls.SetChildIndex(Me.CashFlowAccountDescTextBox, 0)
    Me.Controls.SetChildIndex(Me.CashFlowAccountIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.CompanyComboBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents CompanyComboBox As QuickBusinessControls.CompanyComboBox
  Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
  Friend WithEvents CashFlowAccountIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents CashFlowAccountDescTextBox As QuickControls.Quick_TextBox
End Class
