<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VoucherTypeForm
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
    Me.components = New System.ComponentModel.Container
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
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton2 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.CompanyComboBox = New QuickBusinessControls.CompanyComboBox
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.InactiveToCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.InactiveFromCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.Quick_Label7 = New QuickControls.Quick_Label
    Me.Quick_Label6 = New QuickControls.Quick_Label
    Me.VoucherTypeIDTextBox = New QuickControls.Quick_TextBox
    Me.VoucherTypeDescTextBox = New QuickControls.Quick_TextBox
    Me.VoucherTypeCodeTextBox = New QuickControls.Quick_TextBox
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.InactiveToCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.InactiveFromCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(249, 77)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(102, 13)
    Me.Quick_Label4.TabIndex = 33
    Me.Quick_Label4.Text = "Voucher Type Code"
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(17, 107)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(60, 13)
    Me.Quick_Label3.TabIndex = 32
    Me.Quick_Label3.Text = "Description"
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(17, 77)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(88, 13)
    Me.Quick_Label2.TabIndex = 31
    Me.Quick_Label2.Text = "Voucher Type ID"
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
    Appearance10.TextHAlign = Infragistics.Win.HAlign.Left
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
    Me.CompanyComboBox.DisplayMember = ""
    Me.CompanyComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.CompanyComboBox.DropDownWidth = 121
    Me.CompanyComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.CompanyComboBox.IsMandatory = False
    Me.CompanyComboBox.IsReadonlyForExistingRecord = False
    Me.CompanyComboBox.IsReadonlyForNewRecord = False
    Me.CompanyComboBox.Location = New System.Drawing.Point(122, 42)
    Me.CompanyComboBox.Name = "CompanyComboBox"
    Me.CompanyComboBox.Size = New System.Drawing.Size(121, 22)
    Me.CompanyComboBox.TabIndex = 0
    Me.CompanyComboBox.ValueMember = ""
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(17, 47)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(51, 13)
    Me.Quick_Label1.TabIndex = 29
    Me.Quick_Label1.Text = "Company"
    '
    'InactiveToCalendarCombo
    '
    Me.InactiveToCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.InactiveToCalendarCombo.DateButtons.Add(DateButton1)
    Me.InactiveToCalendarCombo.DefaultValue = New Date(2010, 2, 2, 8, 36, 32, 453)
    Me.InactiveToCalendarCombo.Format = "dd-MM-yy"
    Me.InactiveToCalendarCombo.Location = New System.Drawing.Point(369, 133)
    Me.InactiveToCalendarCombo.Name = "InactiveToCalendarCombo"
    Me.InactiveToCalendarCombo.NonAutoSizeHeight = 21
    Me.InactiveToCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.InactiveToCalendarCombo.TabIndex = 4
    '
    'InactiveFromCalendarCombo
    '
    Me.InactiveFromCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.InactiveFromCalendarCombo.DateButtons.Add(DateButton2)
    Me.InactiveFromCalendarCombo.DefaultValue = New Date(2010, 2, 2, 8, 36, 32, 500)
    Me.InactiveFromCalendarCombo.Format = "dd-MM-yy"
    Me.InactiveFromCalendarCombo.Location = New System.Drawing.Point(122, 133)
    Me.InactiveFromCalendarCombo.Name = "InactiveFromCalendarCombo"
    Me.InactiveFromCalendarCombo.NonAutoSizeHeight = 21
    Me.InactiveFromCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.InactiveFromCalendarCombo.TabIndex = 3
    '
    'Quick_Label7
    '
    Me.Quick_Label7.AllowClearValue = False
    Me.Quick_Label7.AutoSize = True
    Me.Quick_Label7.DefaultValue = ""
    Me.Quick_Label7.Location = New System.Drawing.Point(249, 137)
    Me.Quick_Label7.Name = "Quick_Label7"
    Me.Quick_Label7.Size = New System.Drawing.Size(61, 13)
    Me.Quick_Label7.TabIndex = 25
    Me.Quick_Label7.Text = "Inactive To"
    '
    'Quick_Label6
    '
    Me.Quick_Label6.AllowClearValue = False
    Me.Quick_Label6.AutoSize = True
    Me.Quick_Label6.DefaultValue = ""
    Me.Quick_Label6.Location = New System.Drawing.Point(17, 137)
    Me.Quick_Label6.Name = "Quick_Label6"
    Me.Quick_Label6.Size = New System.Drawing.Size(71, 13)
    Me.Quick_Label6.TabIndex = 26
    Me.Quick_Label6.Text = "Inactive From"
    '
    'VoucherTypeIDTextBox
    '
    Me.VoucherTypeIDTextBox.DefaultValue = ""
    Me.VoucherTypeIDTextBox.IntegerNumber = 0
    Me.VoucherTypeIDTextBox.IsMandatory = False
    Me.VoucherTypeIDTextBox.IsReadonlyForExistingRecord = False
    Me.VoucherTypeIDTextBox.IsReadonlyForNewRecord = False
    Me.VoucherTypeIDTextBox.Location = New System.Drawing.Point(122, 73)
    Me.VoucherTypeIDTextBox.Name = "VoucherTypeIDTextBox"
    Me.VoucherTypeIDTextBox.PercentNumber = 0
    Me.VoucherTypeIDTextBox.ReadOnly = True
    Me.VoucherTypeIDTextBox.Size = New System.Drawing.Size(121, 20)
    Me.VoucherTypeIDTextBox.TabIndex = 24
    Me.VoucherTypeIDTextBox.Text = "0"
    Me.VoucherTypeIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'VoucherTypeDescTextBox
    '
    Me.VoucherTypeDescTextBox.DefaultValue = ""
    Me.VoucherTypeDescTextBox.IntegerNumber = 0
    Me.VoucherTypeDescTextBox.IsMandatory = False
    Me.VoucherTypeDescTextBox.IsReadonlyForExistingRecord = False
    Me.VoucherTypeDescTextBox.IsReadonlyForNewRecord = False
    Me.VoucherTypeDescTextBox.Location = New System.Drawing.Point(122, 103)
    Me.VoucherTypeDescTextBox.Name = "VoucherTypeDescTextBox"
    Me.VoucherTypeDescTextBox.PercentNumber = 0
    Me.VoucherTypeDescTextBox.Size = New System.Drawing.Size(368, 20)
    Me.VoucherTypeDescTextBox.TabIndex = 2
    Me.VoucherTypeDescTextBox.Text = "0"
    Me.VoucherTypeDescTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'VoucherTypeCodeTextBox
    '
    Me.VoucherTypeCodeTextBox.DefaultValue = ""
    Me.VoucherTypeCodeTextBox.IntegerNumber = 0
    Me.VoucherTypeCodeTextBox.IsMandatory = False
    Me.VoucherTypeCodeTextBox.IsReadonlyForExistingRecord = False
    Me.VoucherTypeCodeTextBox.IsReadonlyForNewRecord = False
    Me.VoucherTypeCodeTextBox.Location = New System.Drawing.Point(369, 73)
    Me.VoucherTypeCodeTextBox.Name = "VoucherTypeCodeTextBox"
    Me.VoucherTypeCodeTextBox.PercentNumber = 0
    Me.VoucherTypeCodeTextBox.Size = New System.Drawing.Size(121, 20)
    Me.VoucherTypeCodeTextBox.TabIndex = 1
    Me.VoucherTypeCodeTextBox.Text = "0"
    Me.VoucherTypeCodeTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'VoucherTypeForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(507, 197)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.CompanyComboBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Controls.Add(Me.InactiveToCalendarCombo)
    Me.Controls.Add(Me.InactiveFromCalendarCombo)
    Me.Controls.Add(Me.Quick_Label7)
    Me.Controls.Add(Me.Quick_Label6)
    Me.Controls.Add(Me.VoucherTypeIDTextBox)
    Me.Controls.Add(Me.VoucherTypeDescTextBox)
    Me.Controls.Add(Me.VoucherTypeCodeTextBox)
    Me.Name = "VoucherTypeForm"
    Me.Text = "VoucherType"
    Me.Controls.SetChildIndex(Me.VoucherTypeCodeTextBox, 0)
    Me.Controls.SetChildIndex(Me.VoucherTypeDescTextBox, 0)
    Me.Controls.SetChildIndex(Me.VoucherTypeIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label6, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label7, 0)
    Me.Controls.SetChildIndex(Me.InactiveFromCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.InactiveToCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.CompanyComboBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.InactiveToCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.InactiveFromCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
    Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
    Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
    Friend WithEvents CompanyComboBox As QuickBusinessControls.CompanyComboBox
    Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
    Friend WithEvents InactiveToCalendarCombo As QuickControls.Quick_UltraCalendarCombo
    Friend WithEvents InactiveFromCalendarCombo As QuickControls.Quick_UltraCalendarCombo
    Friend WithEvents Quick_Label7 As QuickControls.Quick_Label
    Friend WithEvents Quick_Label6 As QuickControls.Quick_Label
    Friend WithEvents VoucherTypeIDTextBox As QuickControls.Quick_TextBox
    Friend WithEvents VoucherTypeDescTextBox As QuickControls.Quick_TextBox
    Friend WithEvents VoucherTypeCodeTextBox As QuickControls.Quick_TextBox
End Class
