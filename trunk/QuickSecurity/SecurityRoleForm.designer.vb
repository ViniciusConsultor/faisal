<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SecurityRoleForm
  Inherits QuickBaseForms.ParentToolbarForm
    'Inherits System.Windows.Forms.Form

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
    Dim DateButton2 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton3 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Me.CompanyComboBox = New QuickBusinessControls.CompanyComboBox
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.RoleIDTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.RoleDescTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.InactiveFromCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.InactiveToCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.Quick_Label5 = New QuickControls.Quick_Label
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.InactiveFromCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.InactiveToCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'CompanyComboBox
    '
    Me.CompanyComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.CompanyComboBox.CompanyID = CType(0, Short)
    Appearance25.BackColor = System.Drawing.SystemColors.Window
    Appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.CompanyComboBox.DisplayLayout.Appearance = Appearance25
    Me.CompanyComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.CompanyComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance26.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox.DisplayLayout.GroupByBox.Appearance = Appearance26
    Appearance27.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance27
    Me.CompanyComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance28.BackColor2 = System.Drawing.SystemColors.Control
    Appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance28.ForeColor = System.Drawing.SystemColors.GrayText
    Me.CompanyComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance28
    Me.CompanyComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.CompanyComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance29.BackColor = System.Drawing.SystemColors.Window
    Appearance29.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CompanyComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance29
    Appearance30.BackColor = System.Drawing.SystemColors.Highlight
    Appearance30.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.CompanyComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance30
    Me.CompanyComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.CompanyComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance31.BackColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance31
    Appearance32.BorderColor = System.Drawing.Color.Silver
    Appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.CompanyComboBox.DisplayLayout.Override.CellAppearance = Appearance32
    Me.CompanyComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.CompanyComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance33.BackColor = System.Drawing.SystemColors.Control
    Appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance33.BorderColor = System.Drawing.SystemColors.Window
    Me.CompanyComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance33
    Appearance34.TextHAlign = Infragistics.Win.HAlign.Left
    Me.CompanyComboBox.DisplayLayout.Override.HeaderAppearance = Appearance34
    Me.CompanyComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.CompanyComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance35.BackColor = System.Drawing.SystemColors.Window
    Appearance35.BorderColor = System.Drawing.Color.Silver
    Me.CompanyComboBox.DisplayLayout.Override.RowAppearance = Appearance35
    Me.CompanyComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance36.BackColor = System.Drawing.SystemColors.ControlLight
    Me.CompanyComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance36
    Me.CompanyComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.CompanyComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.CompanyComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.CompanyComboBox.DisplayMember = ""
    Me.CompanyComboBox.Location = New System.Drawing.Point(92, 52)
    Me.CompanyComboBox.Name = "CompanyComboBox"
    Me.CompanyComboBox.Size = New System.Drawing.Size(216, 22)
    Me.CompanyComboBox.TabIndex = 8
    Me.CompanyComboBox.ValueMember = ""
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.Location = New System.Drawing.Point(16, 56)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(54, 13)
    Me.Quick_Label1.TabIndex = 9
    Me.Quick_Label1.Text = "Company:"
    '
    'RoleIDTextBox
    '
    Me.RoleIDTextBox.IntegerNumber = 0
    Me.RoleIDTextBox.Location = New System.Drawing.Point(364, 52)
    Me.RoleIDTextBox.Name = "RoleIDTextBox"
    Me.RoleIDTextBox.PercentNumber = 0
    Me.RoleIDTextBox.ReadOnly = True
    Me.RoleIDTextBox.Size = New System.Drawing.Size(72, 20)
    Me.RoleIDTextBox.TabIndex = 10
    Me.RoleIDTextBox.TabStop = False
    Me.RoleIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.Location = New System.Drawing.Point(312, 56)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(46, 13)
    Me.Quick_Label2.TabIndex = 11
    Me.Quick_Label2.Text = "Role ID:"
    '
    'RoleDescTextBox
    '
    Me.RoleDescTextBox.IntegerNumber = 0
    Me.RoleDescTextBox.Location = New System.Drawing.Point(92, 80)
    Me.RoleDescTextBox.Name = "RoleDescTextBox"
    Me.RoleDescTextBox.PercentNumber = 0
    Me.RoleDescTextBox.Size = New System.Drawing.Size(345, 20)
    Me.RoleDescTextBox.TabIndex = 12
    Me.RoleDescTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.Location = New System.Drawing.Point(16, 84)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(63, 13)
    Me.Quick_Label3.TabIndex = 13
    Me.Quick_Label3.Text = "Description:"
    '
    'InactiveFromCalendarCombo
    '
    Me.InactiveFromCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.InactiveFromCalendarCombo.DateButtons.Add(DateButton2)
    Me.InactiveFromCalendarCombo.Format = "dd-MM-yy"
    Me.InactiveFromCalendarCombo.Location = New System.Drawing.Point(92, 108)
    Me.InactiveFromCalendarCombo.Name = "InactiveFromCalendarCombo"
    Me.InactiveFromCalendarCombo.NonAutoSizeHeight = 21
    Me.InactiveFromCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.InactiveFromCalendarCombo.TabIndex = 14
    Me.InactiveFromCalendarCombo.Value = New Date(2009, 5, 26, 0, 0, 0, 0)
    '
    'InactiveToCalendarCombo
    '
    Me.InactiveToCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.InactiveToCalendarCombo.DateButtons.Add(DateButton3)
    Me.InactiveToCalendarCombo.Format = "dd-MM-yy"
    Me.InactiveToCalendarCombo.Location = New System.Drawing.Point(316, 108)
    Me.InactiveToCalendarCombo.Name = "InactiveToCalendarCombo"
    Me.InactiveToCalendarCombo.NonAutoSizeHeight = 21
    Me.InactiveToCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.InactiveToCalendarCombo.TabIndex = 15
    Me.InactiveToCalendarCombo.Value = New Date(2009, 5, 26, 0, 0, 0, 0)
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.Location = New System.Drawing.Point(16, 112)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(74, 13)
    Me.Quick_Label4.TabIndex = 16
    Me.Quick_Label4.Text = "Inactive From:"
    '
    'Quick_Label5
    '
    Me.Quick_Label5.AutoSize = True
    Me.Quick_Label5.Location = New System.Drawing.Point(248, 112)
    Me.Quick_Label5.Name = "Quick_Label5"
    Me.Quick_Label5.Size = New System.Drawing.Size(64, 13)
    Me.Quick_Label5.TabIndex = 17
    Me.Quick_Label5.Text = "Inactive To:"
    '
    'SecurityRoleForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(469, 231)
    Me.Controls.Add(Me.Quick_Label5)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.InactiveToCalendarCombo)
    Me.Controls.Add(Me.InactiveFromCalendarCombo)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.RoleDescTextBox)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.RoleIDTextBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Controls.Add(Me.CompanyComboBox)
    Me.Name = "SecurityRoleForm"
    Me.Text = "SecurityRoleForm"
    Me.Controls.SetChildIndex(Me.CompanyComboBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.RoleIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.RoleDescTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.InactiveFromCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.InactiveToCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label5, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.InactiveFromCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.InactiveToCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents CompanyComboBox As QuickBusinessControls.CompanyComboBox
    Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
    Friend WithEvents RoleIDTextBox As QuickControls.Quick_TextBox
    Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
    Friend WithEvents RoleDescTextBox As QuickControls.Quick_TextBox
    Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
    Friend WithEvents InactiveFromCalendarCombo As QuickControls.Quick_UltraCalendarCombo
    Friend WithEvents InactiveToCalendarCombo As QuickControls.Quick_UltraCalendarCombo
    Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
    Friend WithEvents Quick_Label5 As QuickControls.Quick_Label
End Class
