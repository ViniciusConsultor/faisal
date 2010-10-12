<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuSetting
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
    Me.MenuTreeView = New System.Windows.Forms.TreeView
    Me.MenuDisplayTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.MenuDescriptionTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.MenuIDTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.ParentMenuIDTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.FormCodeComboBox = New QuickBusinessControls.CompanyComboBox
    Me.Quick_Label5 = New QuickControls.Quick_Label
    Me.UpArrow = New System.Windows.Forms.Button
    Me.DownArrow = New System.Windows.Forms.Button
    Me.RightArrowButton = New System.Windows.Forms.Button
    Me.LeftArrowButton = New System.Windows.Forms.Button
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.FormCodeComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'MenuTreeView
    '
    Me.MenuTreeView.Location = New System.Drawing.Point(0, 117)
    Me.MenuTreeView.Name = "MenuTreeView"
    Me.MenuTreeView.Size = New System.Drawing.Size(277, 580)
    Me.MenuTreeView.TabIndex = 2
    '
    'MenuDisplayTextBox
    '
    Me.MenuDisplayTextBox.DefaultValue = ""
    Me.MenuDisplayTextBox.IntegerNumber = 0
    Me.MenuDisplayTextBox.IsMandatory = False
    Me.MenuDisplayTextBox.IsReadonlyForExistingRecord = False
    Me.MenuDisplayTextBox.IsReadonlyForNewRecord = False
    Me.MenuDisplayTextBox.Location = New System.Drawing.Point(481, 195)
    Me.MenuDisplayTextBox.Name = "MenuDisplayTextBox"
    Me.MenuDisplayTextBox.PercentNumber = 0
    Me.MenuDisplayTextBox.ReadOnly = True
    Me.MenuDisplayTextBox.Size = New System.Drawing.Size(179, 20)
    Me.MenuDisplayTextBox.TabIndex = 28
    Me.MenuDisplayTextBox.Text = "0"
    Me.MenuDisplayTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(378, 195)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(70, 13)
    Me.Quick_Label3.TabIndex = 33
    Me.Quick_Label3.Text = "Display Order"
    '
    'MenuDescriptionTextBox
    '
    Me.MenuDescriptionTextBox.DefaultValue = ""
    Me.MenuDescriptionTextBox.IntegerNumber = 0
    Me.MenuDescriptionTextBox.IsMandatory = False
    Me.MenuDescriptionTextBox.IsReadonlyForExistingRecord = False
    Me.MenuDescriptionTextBox.IsReadonlyForNewRecord = False
    Me.MenuDescriptionTextBox.Location = New System.Drawing.Point(481, 163)
    Me.MenuDescriptionTextBox.Name = "MenuDescriptionTextBox"
    Me.MenuDescriptionTextBox.PercentNumber = 0
    Me.MenuDescriptionTextBox.Size = New System.Drawing.Size(344, 20)
    Me.MenuDescriptionTextBox.TabIndex = 0
    Me.MenuDescriptionTextBox.Text = "0"
    Me.MenuDescriptionTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(378, 163)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(90, 13)
    Me.Quick_Label2.TabIndex = 32
    Me.Quick_Label2.Text = "Menu Description"
    '
    'MenuIDTextBox
    '
    Me.MenuIDTextBox.DefaultValue = ""
    Me.MenuIDTextBox.IntegerNumber = 0
    Me.MenuIDTextBox.IsMandatory = False
    Me.MenuIDTextBox.IsReadonlyForExistingRecord = False
    Me.MenuIDTextBox.IsReadonlyForNewRecord = False
    Me.MenuIDTextBox.Location = New System.Drawing.Point(481, 124)
    Me.MenuIDTextBox.Name = "MenuIDTextBox"
    Me.MenuIDTextBox.PercentNumber = 0
    Me.MenuIDTextBox.ReadOnly = True
    Me.MenuIDTextBox.Size = New System.Drawing.Size(121, 20)
    Me.MenuIDTextBox.TabIndex = 30
    Me.MenuIDTextBox.Text = "0"
    Me.MenuIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(378, 124)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(48, 13)
    Me.Quick_Label4.TabIndex = 29
    Me.Quick_Label4.Text = "Menu ID"
    '
    'ParentMenuIDTextBox
    '
    Me.ParentMenuIDTextBox.DefaultValue = ""
    Me.ParentMenuIDTextBox.IntegerNumber = 0
    Me.ParentMenuIDTextBox.IsMandatory = False
    Me.ParentMenuIDTextBox.IsReadonlyForExistingRecord = False
    Me.ParentMenuIDTextBox.IsReadonlyForNewRecord = False
    Me.ParentMenuIDTextBox.Location = New System.Drawing.Point(481, 278)
    Me.ParentMenuIDTextBox.Name = "ParentMenuIDTextBox"
    Me.ParentMenuIDTextBox.PercentNumber = 0
    Me.ParentMenuIDTextBox.ReadOnly = True
    Me.ParentMenuIDTextBox.Size = New System.Drawing.Size(179, 20)
    Me.ParentMenuIDTextBox.TabIndex = 34
    Me.ParentMenuIDTextBox.Text = "0"
    Me.ParentMenuIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(378, 278)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(82, 13)
    Me.Quick_Label1.TabIndex = 35
    Me.Quick_Label1.Text = "Parent Menu ID"
    '
    'FormCodeComboBox
    '
    Me.FormCodeComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.FormCodeComboBox.CompanyID = CType(0, Short)
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.FormCodeComboBox.DisplayLayout.Appearance = Appearance1
    Me.FormCodeComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.FormCodeComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.FormCodeComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.FormCodeComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.FormCodeComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.FormCodeComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.FormCodeComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.FormCodeComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.FormCodeComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.FormCodeComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.FormCodeComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.FormCodeComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.FormCodeComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.FormCodeComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.FormCodeComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.FormCodeComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.FormCodeComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlignAsString = "Left"
    Me.FormCodeComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.FormCodeComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.FormCodeComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.FormCodeComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.FormCodeComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.FormCodeComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.FormCodeComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.FormCodeComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.FormCodeComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.FormCodeComboBox.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
    Me.FormCodeComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.FormCodeComboBox.DropDownWidth = 179
    Me.FormCodeComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.FormCodeComboBox.IsMandatory = False
    Me.FormCodeComboBox.IsReadonlyForExistingRecord = False
    Me.FormCodeComboBox.IsReadonlyForNewRecord = False
    Me.FormCodeComboBox.Location = New System.Drawing.Point(481, 235)
    Me.FormCodeComboBox.Name = "FormCodeComboBox"
    Me.FormCodeComboBox.Size = New System.Drawing.Size(179, 22)
    Me.FormCodeComboBox.TabIndex = 1
    '
    'Quick_Label5
    '
    Me.Quick_Label5.AllowClearValue = False
    Me.Quick_Label5.AutoSize = True
    Me.Quick_Label5.DefaultValue = ""
    Me.Quick_Label5.Location = New System.Drawing.Point(376, 240)
    Me.Quick_Label5.Name = "Quick_Label5"
    Me.Quick_Label5.Size = New System.Drawing.Size(58, 13)
    Me.Quick_Label5.TabIndex = 37
    Me.Quick_Label5.Text = "Form Code"
    '
    'UpArrow
    '
    Me.UpArrow.Location = New System.Drawing.Point(481, 351)
    Me.UpArrow.Name = "UpArrow"
    Me.UpArrow.Size = New System.Drawing.Size(75, 39)
    Me.UpArrow.TabIndex = 38
    Me.UpArrow.Text = "UP Arrow"
    Me.UpArrow.UseVisualStyleBackColor = True
    '
    'DownArrow
    '
    Me.DownArrow.Location = New System.Drawing.Point(501, 445)
    Me.DownArrow.Name = "DownArrow"
    Me.DownArrow.Size = New System.Drawing.Size(75, 39)
    Me.DownArrow.TabIndex = 39
    Me.DownArrow.Text = "Down Arrow"
    Me.DownArrow.UseVisualStyleBackColor = True
    '
    'RightArrowButton
    '
    Me.RightArrowButton.Location = New System.Drawing.Point(582, 396)
    Me.RightArrowButton.Name = "RightArrowButton"
    Me.RightArrowButton.Size = New System.Drawing.Size(75, 39)
    Me.RightArrowButton.TabIndex = 40
    Me.RightArrowButton.Text = ">>"
    Me.RightArrowButton.UseVisualStyleBackColor = True
    '
    'LeftArrowButton
    '
    Me.LeftArrowButton.Location = New System.Drawing.Point(412, 396)
    Me.LeftArrowButton.Name = "LeftArrowButton"
    Me.LeftArrowButton.Size = New System.Drawing.Size(75, 39)
    Me.LeftArrowButton.TabIndex = 41
    Me.LeftArrowButton.Text = "<<"
    Me.LeftArrowButton.UseVisualStyleBackColor = True
    '
    'MenuSetting
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1028, 746)
    Me.Controls.Add(Me.LeftArrowButton)
    Me.Controls.Add(Me.RightArrowButton)
    Me.Controls.Add(Me.DownArrow)
    Me.Controls.Add(Me.UpArrow)
    Me.Controls.Add(Me.FormCodeComboBox)
    Me.Controls.Add(Me.Quick_Label5)
    Me.Controls.Add(Me.ParentMenuIDTextBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Controls.Add(Me.MenuDisplayTextBox)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.MenuDescriptionTextBox)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.MenuIDTextBox)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.MenuTreeView)
    Me.Name = "MenuSetting"
    Me.Text = "MenuForm"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.Controls.SetChildIndex(Me.MenuTreeView, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    Me.Controls.SetChildIndex(Me.MenuIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.MenuDescriptionTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.MenuDisplayTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.ParentMenuIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label5, 0)
    Me.Controls.SetChildIndex(Me.FormCodeComboBox, 0)
    Me.Controls.SetChildIndex(Me.UpArrow, 0)
    Me.Controls.SetChildIndex(Me.DownArrow, 0)
    Me.Controls.SetChildIndex(Me.RightArrowButton, 0)
    Me.Controls.SetChildIndex(Me.LeftArrowButton, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.FormCodeComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents MenuTreeView As System.Windows.Forms.TreeView
  Friend WithEvents MenuDisplayTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
  Friend WithEvents MenuDescriptionTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents MenuIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
  Friend WithEvents ParentMenuIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
  Friend WithEvents FormCodeComboBox As QuickBusinessControls.CompanyComboBox
  Friend WithEvents Quick_Label5 As QuickControls.Quick_Label
  Friend WithEvents UpArrow As System.Windows.Forms.Button
  Friend WithEvents DownArrow As System.Windows.Forms.Button
  Friend WithEvents RightArrowButton As System.Windows.Forms.Button
  Friend WithEvents LeftArrowButton As System.Windows.Forms.Button
End Class
