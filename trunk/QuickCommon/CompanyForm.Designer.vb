<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompanyForm
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
    Dim DateButton3 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
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
    Me.CommunicationTextBox = New QuickControls.Quick_TextBox
    Me.CommunicationLabel = New QuickControls.Quick_Label
    Me.AddressTextBox = New QuickControls.Quick_TextBox
    Me.AddressLabel = New QuickControls.Quick_Label
    Me.InactiveToLabel = New QuickControls.Quick_Label
    Me.InactiveFromLabel = New QuickControls.Quick_Label
    Me.CompanyInactiveToCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.CompanyInactiveFromCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.CompanyDescTextBox = New QuickControls.Quick_TextBox
    Me.CompanyDescLabel = New QuickControls.Quick_Label
    Me.ParentCompanyComboBox = New QuickControls.Quick_UltraComboBox
    Me.ParentCompanyLabel = New QuickControls.Quick_Label
    Me.CompanyCodeTextBox = New QuickControls.Quick_TextBox
    Me.CompanyCodeLabel = New QuickControls.Quick_Label
    Me.CompanyIDTextBox = New QuickControls.Quick_TextBox
    Me.CompanyIDLabel = New QuickControls.Quick_Label
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyInactiveToCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CompanyInactiveFromCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ParentCompanyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'CommunicationTextBox
    '
    Me.CommunicationTextBox.IntegerNumber = 0
    Me.CommunicationTextBox.Location = New System.Drawing.Point(104, 220)
    Me.CommunicationTextBox.Name = "CommunicationTextBox"
    Me.CommunicationTextBox.PercentNumber = 0
    Me.CommunicationTextBox.Size = New System.Drawing.Size(352, 20)
    Me.CommunicationTextBox.TabIndex = 17
    Me.CommunicationTextBox.Text = "0"
    Me.CommunicationTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'CommunicationLabel
    '
    Me.CommunicationLabel.AutoSize = True
    Me.CommunicationLabel.Location = New System.Drawing.Point(16, 224)
    Me.CommunicationLabel.Name = "CommunicationLabel"
    Me.CommunicationLabel.Size = New System.Drawing.Size(41, 13)
    Me.CommunicationLabel.TabIndex = 16
    Me.CommunicationLabel.Text = "Phone:"
    '
    'AddressTextBox
    '
    Me.AddressTextBox.IntegerNumber = 0
    Me.AddressTextBox.Location = New System.Drawing.Point(104, 192)
    Me.AddressTextBox.Name = "AddressTextBox"
    Me.AddressTextBox.PercentNumber = 0
    Me.AddressTextBox.Size = New System.Drawing.Size(352, 20)
    Me.AddressTextBox.TabIndex = 15
    Me.AddressTextBox.Text = "0"
    Me.AddressTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'AddressLabel
    '
    Me.AddressLabel.AutoSize = True
    Me.AddressLabel.Location = New System.Drawing.Point(16, 196)
    Me.AddressLabel.Name = "AddressLabel"
    Me.AddressLabel.Size = New System.Drawing.Size(48, 13)
    Me.AddressLabel.TabIndex = 14
    Me.AddressLabel.Text = "Address:"
    '
    'InactiveToLabel
    '
    Me.InactiveToLabel.AutoSize = True
    Me.InactiveToLabel.Location = New System.Drawing.Point(16, 168)
    Me.InactiveToLabel.Name = "InactiveToLabel"
    Me.InactiveToLabel.Size = New System.Drawing.Size(64, 13)
    Me.InactiveToLabel.TabIndex = 12
    Me.InactiveToLabel.Text = "Inactive To:"
    '
    'InactiveFromLabel
    '
    Me.InactiveFromLabel.AutoSize = True
    Me.InactiveFromLabel.Location = New System.Drawing.Point(16, 140)
    Me.InactiveFromLabel.Name = "InactiveFromLabel"
    Me.InactiveFromLabel.Size = New System.Drawing.Size(74, 13)
    Me.InactiveFromLabel.TabIndex = 10
    Me.InactiveFromLabel.Text = "Inactive From:"
    '
    'CompanyInactiveToCalendarCombo
    '
    Me.CompanyInactiveToCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.CompanyInactiveToCalendarCombo.DateButtons.Add(DateButton3)
    Me.CompanyInactiveToCalendarCombo.Format = "dd-MM-yy"
    Me.CompanyInactiveToCalendarCombo.Location = New System.Drawing.Point(104, 164)
    Me.CompanyInactiveToCalendarCombo.Name = "CompanyInactiveToCalendarCombo"
    Me.CompanyInactiveToCalendarCombo.NonAutoSizeHeight = 21
    Me.CompanyInactiveToCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.CompanyInactiveToCalendarCombo.TabIndex = 13
    '
    'CompanyInactiveFromCalendarCombo
    '
    Me.CompanyInactiveFromCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.CompanyInactiveFromCalendarCombo.DateButtons.Add(DateButton1)
    Me.CompanyInactiveFromCalendarCombo.Format = "dd-MM-yy"
    Me.CompanyInactiveFromCalendarCombo.Location = New System.Drawing.Point(104, 136)
    Me.CompanyInactiveFromCalendarCombo.Name = "CompanyInactiveFromCalendarCombo"
    Me.CompanyInactiveFromCalendarCombo.NonAutoSizeHeight = 21
    Me.CompanyInactiveFromCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.CompanyInactiveFromCalendarCombo.TabIndex = 11
    '
    'CompanyDescTextBox
    '
    Me.CompanyDescTextBox.IntegerNumber = 0
    Me.CompanyDescTextBox.Location = New System.Drawing.Point(104, 108)
    Me.CompanyDescTextBox.Name = "CompanyDescTextBox"
    Me.CompanyDescTextBox.PercentNumber = 0
    Me.CompanyDescTextBox.Size = New System.Drawing.Size(352, 20)
    Me.CompanyDescTextBox.TabIndex = 9
    Me.CompanyDescTextBox.Text = "0"
    Me.CompanyDescTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'CompanyDescLabel
    '
    Me.CompanyDescLabel.AutoSize = True
    Me.CompanyDescLabel.Location = New System.Drawing.Point(16, 112)
    Me.CompanyDescLabel.Name = "CompanyDescLabel"
    Me.CompanyDescLabel.Size = New System.Drawing.Size(85, 13)
    Me.CompanyDescLabel.TabIndex = 8
    Me.CompanyDescLabel.Text = "Company Name:"
    '
    'ParentCompanyComboBox
    '
    Me.ParentCompanyComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance13.BackColor = System.Drawing.SystemColors.Window
    Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.ParentCompanyComboBox.DisplayLayout.Appearance = Appearance13
    Me.ParentCompanyComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.ParentCompanyComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance14.BorderColor = System.Drawing.SystemColors.Window
    Me.ParentCompanyComboBox.DisplayLayout.GroupByBox.Appearance = Appearance14
    Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
    Me.ParentCompanyComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
    Me.ParentCompanyComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance16.BackColor2 = System.Drawing.SystemColors.Control
    Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
    Me.ParentCompanyComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
    Me.ParentCompanyComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.ParentCompanyComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance17.BackColor = System.Drawing.SystemColors.Window
    Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
    Me.ParentCompanyComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance17
    Appearance18.BackColor = System.Drawing.SystemColors.Highlight
    Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.ParentCompanyComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance18
    Me.ParentCompanyComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.ParentCompanyComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance19.BackColor = System.Drawing.SystemColors.Window
    Me.ParentCompanyComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance19
    Appearance20.BorderColor = System.Drawing.Color.Silver
    Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.ParentCompanyComboBox.DisplayLayout.Override.CellAppearance = Appearance20
    Me.ParentCompanyComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.ParentCompanyComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance21.BackColor = System.Drawing.SystemColors.Control
    Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance21.BorderColor = System.Drawing.SystemColors.Window
    Me.ParentCompanyComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance21
    Appearance22.TextHAlign = Infragistics.Win.HAlign.Left
    Me.ParentCompanyComboBox.DisplayLayout.Override.HeaderAppearance = Appearance22
    Me.ParentCompanyComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.ParentCompanyComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance23.BackColor = System.Drawing.SystemColors.Window
    Appearance23.BorderColor = System.Drawing.Color.Silver
    Me.ParentCompanyComboBox.DisplayLayout.Override.RowAppearance = Appearance23
    Me.ParentCompanyComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
    Me.ParentCompanyComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
    Me.ParentCompanyComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.ParentCompanyComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.ParentCompanyComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.ParentCompanyComboBox.DisplayMember = ""
    Me.ParentCompanyComboBox.Location = New System.Drawing.Point(280, 52)
    Me.ParentCompanyComboBox.Name = "ParentCompanyComboBox"
    Me.ParentCompanyComboBox.Size = New System.Drawing.Size(176, 22)
    Me.ParentCompanyComboBox.TabIndex = 5
    Me.ParentCompanyComboBox.ValueMember = ""
    '
    'ParentCompanyLabel
    '
    Me.ParentCompanyLabel.AutoSize = True
    Me.ParentCompanyLabel.Location = New System.Drawing.Point(216, 56)
    Me.ParentCompanyLabel.Name = "ParentCompanyLabel"
    Me.ParentCompanyLabel.Size = New System.Drawing.Size(59, 13)
    Me.ParentCompanyLabel.TabIndex = 4
    Me.ParentCompanyLabel.Text = "Belong To:"
    '
    'CompanyCodeTextBox
    '
    Me.CompanyCodeTextBox.IntegerNumber = 0
    Me.CompanyCodeTextBox.Location = New System.Drawing.Point(104, 80)
    Me.CompanyCodeTextBox.Name = "CompanyCodeTextBox"
    Me.CompanyCodeTextBox.PercentNumber = 0
    Me.CompanyCodeTextBox.Size = New System.Drawing.Size(352, 20)
    Me.CompanyCodeTextBox.TabIndex = 7
    Me.CompanyCodeTextBox.Text = "0"
    Me.CompanyCodeTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'CompanyCodeLabel
    '
    Me.CompanyCodeLabel.AutoSize = True
    Me.CompanyCodeLabel.Location = New System.Drawing.Point(16, 84)
    Me.CompanyCodeLabel.Name = "CompanyCodeLabel"
    Me.CompanyCodeLabel.Size = New System.Drawing.Size(82, 13)
    Me.CompanyCodeLabel.TabIndex = 6
    Me.CompanyCodeLabel.Text = "Company Code:"
    '
    'CompanyIDTextBox
    '
    Me.CompanyIDTextBox.IntegerNumber = 0
    Me.CompanyIDTextBox.Location = New System.Drawing.Point(104, 52)
    Me.CompanyIDTextBox.Name = "CompanyIDTextBox"
    Me.CompanyIDTextBox.PercentNumber = 0
    Me.CompanyIDTextBox.ReadOnly = True
    Me.CompanyIDTextBox.Size = New System.Drawing.Size(100, 20)
    Me.CompanyIDTextBox.TabIndex = 3
    Me.CompanyIDTextBox.Text = "0"
    Me.CompanyIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'CompanyIDLabel
    '
    Me.CompanyIDLabel.AutoSize = True
    Me.CompanyIDLabel.Location = New System.Drawing.Point(16, 56)
    Me.CompanyIDLabel.Name = "CompanyIDLabel"
    Me.CompanyIDLabel.Size = New System.Drawing.Size(68, 13)
    Me.CompanyIDLabel.TabIndex = 2
    Me.CompanyIDLabel.Text = "Company ID:"
    '
    'CompanyForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(469, 273)
    Me.Controls.Add(Me.CommunicationTextBox)
    Me.Controls.Add(Me.CommunicationLabel)
    Me.Controls.Add(Me.AddressTextBox)
    Me.Controls.Add(Me.AddressLabel)
    Me.Controls.Add(Me.InactiveToLabel)
    Me.Controls.Add(Me.InactiveFromLabel)
    Me.Controls.Add(Me.CompanyInactiveToCalendarCombo)
    Me.Controls.Add(Me.CompanyInactiveFromCalendarCombo)
    Me.Controls.Add(Me.CompanyDescTextBox)
    Me.Controls.Add(Me.CompanyDescLabel)
    Me.Controls.Add(Me.ParentCompanyComboBox)
    Me.Controls.Add(Me.ParentCompanyLabel)
    Me.Controls.Add(Me.CompanyCodeTextBox)
    Me.Controls.Add(Me.CompanyCodeLabel)
    Me.Controls.Add(Me.CompanyIDTextBox)
    Me.Controls.Add(Me.CompanyIDLabel)
    Me.Name = "CompanyForm"
    Me.Text = "CompanyForm"
    Me.Controls.SetChildIndex(Me.CompanyIDLabel, 0)
    Me.Controls.SetChildIndex(Me.CompanyIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.CompanyCodeLabel, 0)
    Me.Controls.SetChildIndex(Me.CompanyCodeTextBox, 0)
    Me.Controls.SetChildIndex(Me.ParentCompanyLabel, 0)
    Me.Controls.SetChildIndex(Me.ParentCompanyComboBox, 0)
    Me.Controls.SetChildIndex(Me.CompanyDescLabel, 0)
    Me.Controls.SetChildIndex(Me.CompanyDescTextBox, 0)
    Me.Controls.SetChildIndex(Me.CompanyInactiveFromCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.CompanyInactiveToCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.InactiveFromLabel, 0)
    Me.Controls.SetChildIndex(Me.InactiveToLabel, 0)
    Me.Controls.SetChildIndex(Me.AddressLabel, 0)
    Me.Controls.SetChildIndex(Me.AddressTextBox, 0)
    Me.Controls.SetChildIndex(Me.CommunicationLabel, 0)
    Me.Controls.SetChildIndex(Me.CommunicationTextBox, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyInactiveToCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CompanyInactiveFromCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ParentCompanyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents CompanyIDLabel As QuickControls.Quick_Label
  Friend WithEvents CompanyIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents CompanyCodeTextBox As QuickControls.Quick_TextBox
  Friend WithEvents CompanyCodeLabel As QuickControls.Quick_Label
  Friend WithEvents ParentCompanyLabel As QuickControls.Quick_Label
  Friend WithEvents CompanyDescTextBox As QuickControls.Quick_TextBox
  Friend WithEvents CompanyDescLabel As QuickControls.Quick_Label
  Friend WithEvents CompanyInactiveFromCalendarCombo As QuickControls.Quick_UltraCalendarCombo
  Friend WithEvents CompanyInactiveToCalendarCombo As QuickControls.Quick_UltraCalendarCombo
  Friend WithEvents InactiveFromLabel As QuickControls.Quick_Label
  Friend WithEvents InactiveToLabel As QuickControls.Quick_Label
  Friend WithEvents ParentCompanyComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents AddressLabel As QuickControls.Quick_Label
  Friend WithEvents AddressTextBox As QuickControls.Quick_TextBox
  Friend WithEvents CommunicationTextBox As QuickControls.Quick_TextBox
  Friend WithEvents CommunicationLabel As QuickControls.Quick_Label
End Class
