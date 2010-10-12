<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VoucherSingleEntry
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
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
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
    Me.VoucherNoLabel = New QuickControls.Quick_Label
    Me.VoucherNoTextBox = New QuickControls.Quick_TextBox
    Me.VoucherDateLabel = New QuickControls.Quick_Label
    Me.VoucherIDLabel = New QuickControls.Quick_Label
    Me.VoucherDateCalendar = New QuickControls.Quick_UltraCalendarCombo
    Me.PartyLabel = New QuickControls.Quick_Label
    Me.PartyComboBox = New QuickControls.Quick_UltraComboBox
    Me.AmountLabel = New QuickControls.Quick_Label
    Me.RemarksTextBox = New QuickControls.Quick_TextBox
    Me.RemarksLabel = New QuickControls.Quick_Label
    Me.AmountTextBox = New QuickControls.Quick_UltraNumericEditor
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.VoucherDateCalendar, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PartyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.AmountTextBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'VoucherNoLabel
    '
    Me.VoucherNoLabel.AutoSize = True
    Me.VoucherNoLabel.Location = New System.Drawing.Point(64, 96)
    Me.VoucherNoLabel.Name = "VoucherNoLabel"
    Me.VoucherNoLabel.Size = New System.Drawing.Size(67, 13)
    Me.VoucherNoLabel.TabIndex = 3
    Me.VoucherNoLabel.Text = "Voucher No:"
    '
    'VoucherNoTextBox
    '
    Me.VoucherNoTextBox.Location = New System.Drawing.Point(136, 92)
    Me.VoucherNoTextBox.Name = "VoucherNoTextBox"
    Me.VoucherNoTextBox.ReadOnly = True
    Me.VoucherNoTextBox.Size = New System.Drawing.Size(100, 20)
    Me.VoucherNoTextBox.TabIndex = 4
    '
    'VoucherDateLabel
    '
    Me.VoucherDateLabel.AutoSize = True
    Me.VoucherDateLabel.Location = New System.Drawing.Point(244, 96)
    Me.VoucherDateLabel.Name = "VoucherDateLabel"
    Me.VoucherDateLabel.Size = New System.Drawing.Size(33, 13)
    Me.VoucherDateLabel.TabIndex = 5
    Me.VoucherDateLabel.Text = "Date:"
    '
    'VoucherIDLabel
    '
    Me.VoucherIDLabel.AutoSize = True
    Me.VoucherIDLabel.Location = New System.Drawing.Point(64, 72)
    Me.VoucherIDLabel.Name = "VoucherIDLabel"
    Me.VoucherIDLabel.Size = New System.Drawing.Size(64, 13)
    Me.VoucherIDLabel.TabIndex = 2
    Me.VoucherIDLabel.Text = "Voucher ID:"
    '
    'VoucherDateCalendar
    '
    Me.VoucherDateCalendar.BackColor = System.Drawing.SystemColors.Window
    Me.VoucherDateCalendar.DateButtons.Add(DateButton1)
    Me.VoucherDateCalendar.Format = "dd-MM-yy"
    Me.VoucherDateCalendar.Location = New System.Drawing.Point(284, 92)
    Me.VoucherDateCalendar.Name = "VoucherDateCalendar"
    Me.VoucherDateCalendar.NonAutoSizeHeight = 21
    Me.VoucherDateCalendar.Size = New System.Drawing.Size(108, 21)
    Me.VoucherDateCalendar.TabIndex = 6
    Me.VoucherDateCalendar.Value = New Date(2008, 2, 4, 0, 0, 0, 0)
    '
    'PartyLabel
    '
    Me.PartyLabel.AutoSize = True
    Me.PartyLabel.Location = New System.Drawing.Point(64, 120)
    Me.PartyLabel.Name = "PartyLabel"
    Me.PartyLabel.Size = New System.Drawing.Size(34, 13)
    Me.PartyLabel.TabIndex = 7
    Me.PartyLabel.Text = "Party:"
    '
    'PartyComboBox
    '
    Me.PartyComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.PartyComboBox.DisplayLayout.Appearance = Appearance1
    Me.PartyComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.PartyComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.PartyComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.PartyComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.PartyComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.PartyComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.PartyComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.PartyComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.PartyComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.PartyComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.PartyComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.PartyComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.PartyComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.PartyComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.PartyComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.PartyComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.PartyComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlign = Infragistics.Win.HAlign.Left
    Me.PartyComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.PartyComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.PartyComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.PartyComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.PartyComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.PartyComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.PartyComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.PartyComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.PartyComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.PartyComboBox.DisplayMember = ""
    Me.PartyComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.PartyComboBox.Location = New System.Drawing.Point(136, 116)
    Me.PartyComboBox.Name = "PartyComboBox"
    Me.PartyComboBox.Size = New System.Drawing.Size(256, 22)
    Me.PartyComboBox.TabIndex = 8
    Me.PartyComboBox.ValueMember = ""
    '
    'AmountLabel
    '
    Me.AmountLabel.AutoSize = True
    Me.AmountLabel.Location = New System.Drawing.Point(64, 148)
    Me.AmountLabel.Name = "AmountLabel"
    Me.AmountLabel.Size = New System.Drawing.Size(46, 13)
    Me.AmountLabel.TabIndex = 9
    Me.AmountLabel.Text = "Amount:"
    '
    'RemarksTextBox
    '
    Me.RemarksTextBox.Location = New System.Drawing.Point(136, 168)
    Me.RemarksTextBox.Name = "RemarksTextBox"
    Me.RemarksTextBox.Size = New System.Drawing.Size(256, 20)
    Me.RemarksTextBox.TabIndex = 12
    '
    'RemarksLabel
    '
    Me.RemarksLabel.AutoSize = True
    Me.RemarksLabel.Location = New System.Drawing.Point(64, 172)
    Me.RemarksLabel.Name = "RemarksLabel"
    Me.RemarksLabel.Size = New System.Drawing.Size(52, 13)
    Me.RemarksLabel.TabIndex = 11
    Me.RemarksLabel.Text = "Remarks:"
    '
    'AmountTextBox
    '
    Me.AmountTextBox.Location = New System.Drawing.Point(136, 144)
    Me.AmountTextBox.MinValue = 0
    Me.AmountTextBox.Name = "AmountTextBox"
    Me.AmountTextBox.NullText = "0"
    Me.AmountTextBox.NumericType = Infragistics.Win.UltraWinEditors.NumericType.[Double]
    Me.AmountTextBox.Size = New System.Drawing.Size(100, 21)
    Me.AmountTextBox.TabIndex = 10
    '
    'VoucherSingleEntry
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(469, 224)
    Me.Controls.Add(Me.AmountTextBox)
    Me.Controls.Add(Me.RemarksTextBox)
    Me.Controls.Add(Me.RemarksLabel)
    Me.Controls.Add(Me.AmountLabel)
    Me.Controls.Add(Me.PartyComboBox)
    Me.Controls.Add(Me.PartyLabel)
    Me.Controls.Add(Me.VoucherDateCalendar)
    Me.Controls.Add(Me.VoucherIDLabel)
    Me.Controls.Add(Me.VoucherDateLabel)
    Me.Controls.Add(Me.VoucherNoTextBox)
    Me.Controls.Add(Me.VoucherNoLabel)
    Me.Name = "VoucherSingleEntry"
    Me.Text = "Single Entry Voucher"
    Me.Controls.SetChildIndex(Me.VoucherNoLabel, 0)
    Me.Controls.SetChildIndex(Me.VoucherNoTextBox, 0)
    Me.Controls.SetChildIndex(Me.VoucherDateLabel, 0)
    Me.Controls.SetChildIndex(Me.VoucherIDLabel, 0)
    Me.Controls.SetChildIndex(Me.VoucherDateCalendar, 0)
    Me.Controls.SetChildIndex(Me.PartyLabel, 0)
    Me.Controls.SetChildIndex(Me.PartyComboBox, 0)
    Me.Controls.SetChildIndex(Me.AmountLabel, 0)
    Me.Controls.SetChildIndex(Me.RemarksLabel, 0)
    Me.Controls.SetChildIndex(Me.RemarksTextBox, 0)
    Me.Controls.SetChildIndex(Me.AmountTextBox, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.VoucherDateCalendar, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PartyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.AmountTextBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents VoucherNoLabel As QuickControls.Quick_Label
  Friend WithEvents VoucherNoTextBox As QuickControls.Quick_TextBox
  Friend WithEvents VoucherDateLabel As QuickControls.Quick_Label
  Friend WithEvents VoucherIDLabel As QuickControls.Quick_Label
  Friend WithEvents VoucherDateCalendar As QuickControls.Quick_UltraCalendarCombo
  Friend WithEvents PartyLabel As QuickControls.Quick_Label
  Friend WithEvents PartyComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents AmountLabel As QuickControls.Quick_Label
  Friend WithEvents RemarksTextBox As QuickControls.Quick_TextBox
  Friend WithEvents RemarksLabel As QuickControls.Quick_Label
  Friend WithEvents AmountTextBox As QuickControls.Quick_UltraNumericEditor

End Class
