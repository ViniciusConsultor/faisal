<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VoucherForm
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
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DefaultFocusIndicatorRenderer1 As FarPoint.Win.Spread.DefaultFocusIndicatorRenderer = New FarPoint.Win.Spread.DefaultFocusIndicatorRenderer
    Dim DefaultScrollBarRenderer1 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Dim DefaultScrollBarRenderer2 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.VoucherIDTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.VoucherNoTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.RemarksTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label5 = New QuickControls.Quick_Label
    Me.VoucherTypeComboBox = New QuickControls.Quick_UltraComboBox
    Me.VoucherDateCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.VoucherDetailQuickSpread = New QuickControls.Quick_Spread
    Me.VoucherDetailQuickSpread_SheetView2 = New FarPoint.Win.Spread.SheetView
    Me.PostButton = New System.Windows.Forms.Button
    Me.UnpostedLabel = New System.Windows.Forms.Label
    Me.Quick_Label7 = New QuickControls.Quick_Label
    Me.Quick_Label8 = New QuickControls.Quick_Label
    Me.TotalDebitLabel = New QuickControls.Quick_Label
    Me.TotalCreditLabel = New QuickControls.Quick_Label
    Me.BalanceCreditLabel = New QuickControls.Quick_Label
    Me.Quick_Label9 = New QuickControls.Quick_Label
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.VoucherTypeComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.VoucherDateCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.VoucherDetailQuickSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.VoucherDetailQuickSpread_SheetView2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(12, 47)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(61, 13)
    Me.Quick_Label1.TabIndex = 8
    Me.Quick_Label1.Text = "Voucher ID"
    '
    'VoucherIDTextBox
    '
    Me.VoucherIDTextBox.DefaultValue = ""
    Me.VoucherIDTextBox.IntegerNumber = 0
    Me.VoucherIDTextBox.IsMandatory = False
    Me.VoucherIDTextBox.IsReadonlyForExistingRecord = False
    Me.VoucherIDTextBox.IsReadonlyForNewRecord = False
    Me.VoucherIDTextBox.Location = New System.Drawing.Point(88, 43)
    Me.VoucherIDTextBox.Name = "VoucherIDTextBox"
    Me.VoucherIDTextBox.PercentNumber = 0
    Me.VoucherIDTextBox.ReadOnly = True
    Me.VoucherIDTextBox.Size = New System.Drawing.Size(121, 20)
    Me.VoucherIDTextBox.TabIndex = 9
    Me.VoucherIDTextBox.TabStop = False
    Me.VoucherIDTextBox.Text = "0"
    Me.VoucherIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(12, 107)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(49, 13)
    Me.Quick_Label2.TabIndex = 10
    Me.Quick_Label2.Text = "Remarks"
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(229, 77)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(73, 13)
    Me.Quick_Label3.TabIndex = 12
    Me.Quick_Label3.Text = "Voucher Date"
    '
    'VoucherNoTextBox
    '
    Me.VoucherNoTextBox.DefaultValue = ""
    Me.VoucherNoTextBox.IntegerNumber = 0
    Me.VoucherNoTextBox.IsMandatory = False
    Me.VoucherNoTextBox.IsReadonlyForExistingRecord = False
    Me.VoucherNoTextBox.IsReadonlyForNewRecord = False
    Me.VoucherNoTextBox.Location = New System.Drawing.Point(308, 46)
    Me.VoucherNoTextBox.Name = "VoucherNoTextBox"
    Me.VoucherNoTextBox.PercentNumber = 0
    Me.VoucherNoTextBox.Size = New System.Drawing.Size(121, 20)
    Me.VoucherNoTextBox.TabIndex = 15
    Me.VoucherNoTextBox.Text = "0"
    Me.VoucherNoTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(229, 46)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(64, 13)
    Me.Quick_Label4.TabIndex = 14
    Me.Quick_Label4.Text = "Voucher No"
    '
    'RemarksTextBox
    '
    Me.RemarksTextBox.DefaultValue = ""
    Me.RemarksTextBox.IntegerNumber = 0
    Me.RemarksTextBox.IsMandatory = False
    Me.RemarksTextBox.IsReadonlyForExistingRecord = False
    Me.RemarksTextBox.IsReadonlyForNewRecord = False
    Me.RemarksTextBox.Location = New System.Drawing.Point(88, 103)
    Me.RemarksTextBox.Name = "RemarksTextBox"
    Me.RemarksTextBox.PercentNumber = 0
    Me.RemarksTextBox.Size = New System.Drawing.Size(544, 20)
    Me.RemarksTextBox.TabIndex = 4
    Me.RemarksTextBox.Text = "0"
    Me.RemarksTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label5
    '
    Me.Quick_Label5.AllowClearValue = False
    Me.Quick_Label5.AutoSize = True
    Me.Quick_Label5.DefaultValue = ""
    Me.Quick_Label5.Location = New System.Drawing.Point(12, 77)
    Me.Quick_Label5.Name = "Quick_Label5"
    Me.Quick_Label5.Size = New System.Drawing.Size(74, 13)
    Me.Quick_Label5.TabIndex = 16
    Me.Quick_Label5.Text = "Voucher Type"
    '
    'VoucherTypeComboBox
    '
    Me.VoucherTypeComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.VoucherTypeComboBox.DisplayLayout.Appearance = Appearance1
    Me.VoucherTypeComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.VoucherTypeComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.VoucherTypeComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.VoucherTypeComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.VoucherTypeComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.VoucherTypeComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.VoucherTypeComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.VoucherTypeComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.VoucherTypeComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.VoucherTypeComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.VoucherTypeComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.VoucherTypeComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.VoucherTypeComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.VoucherTypeComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.VoucherTypeComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.VoucherTypeComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.VoucherTypeComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlignAsString = "Left"
    Me.VoucherTypeComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.VoucherTypeComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.VoucherTypeComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.VoucherTypeComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.VoucherTypeComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.VoucherTypeComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.VoucherTypeComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.VoucherTypeComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.VoucherTypeComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.VoucherTypeComboBox.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
    Me.VoucherTypeComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.VoucherTypeComboBox.DropDownWidth = 121
    Me.VoucherTypeComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.VoucherTypeComboBox.IsMandatory = False
    Me.VoucherTypeComboBox.IsReadonlyForExistingRecord = False
    Me.VoucherTypeComboBox.IsReadonlyForNewRecord = False
    Me.VoucherTypeComboBox.Location = New System.Drawing.Point(88, 72)
    Me.VoucherTypeComboBox.Name = "VoucherTypeComboBox"
    Me.VoucherTypeComboBox.Size = New System.Drawing.Size(121, 22)
    Me.VoucherTypeComboBox.TabIndex = 0
    '
    'VoucherDateCalendarCombo
    '
    Me.VoucherDateCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.VoucherDateCalendarCombo.DateButtons.Add(DateButton1)
    Me.VoucherDateCalendarCombo.DefaultValue = New Date(CType(0, Long))
    Me.VoucherDateCalendarCombo.Format = "dd-MM-yy"
    Me.VoucherDateCalendarCombo.Location = New System.Drawing.Point(308, 73)
    Me.VoucherDateCalendarCombo.Name = "VoucherDateCalendarCombo"
    Me.VoucherDateCalendarCombo.NonAutoSizeHeight = 21
    Me.VoucherDateCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.VoucherDateCalendarCombo.TabIndex = 1
    Me.VoucherDateCalendarCombo.Value = New Date(2009, 7, 26, 0, 0, 0, 0)
    '
    'VoucherDetailQuickSpread
    '
    Me.VoucherDetailQuickSpread.AccessibleDescription = "Quick_Spread"
    Me.VoucherDetailQuickSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.VoucherDetailQuickSpread.AutoNewRow = True
    Me.VoucherDetailQuickSpread.BackColor = System.Drawing.SystemColors.Control
    Me.VoucherDetailQuickSpread.EditModePermanent = True
    Me.VoucherDetailQuickSpread.EditModeReplace = True
    Me.VoucherDetailQuickSpread.FocusRenderer = DefaultFocusIndicatorRenderer1
    Me.VoucherDetailQuickSpread.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.VoucherDetailQuickSpread.HorizontalScrollBar.Name = ""
    Me.VoucherDetailQuickSpread.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer1
    Me.VoucherDetailQuickSpread.HorizontalScrollBar.TabIndex = 2
    Me.VoucherDetailQuickSpread.Location = New System.Drawing.Point(12, 136)
    Me.VoucherDetailQuickSpread.Name = "VoucherDetailQuickSpread"
    Me.VoucherDetailQuickSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.VoucherDetailQuickSpread_SheetView2})
    Me.VoucherDetailQuickSpread.Size = New System.Drawing.Size(872, 348)
    Me.VoucherDetailQuickSpread.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.VoucherDetailQuickSpread.TabIndex = 5
    Me.VoucherDetailQuickSpread.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.VoucherDetailQuickSpread.VerticalScrollBar.Name = ""
    Me.VoucherDetailQuickSpread.VerticalScrollBar.Renderer = DefaultScrollBarRenderer2
    Me.VoucherDetailQuickSpread.VerticalScrollBar.TabIndex = 3
    Me.VoucherDetailQuickSpread.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'VoucherDetailQuickSpread_SheetView2
    '
    Me.VoucherDetailQuickSpread_SheetView2.Reset()
    Me.VoucherDetailQuickSpread_SheetView2.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.VoucherDetailQuickSpread_SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.VoucherDetailQuickSpread_SheetView2.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.VoucherDetailQuickSpread_SheetView2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
    Me.VoucherDetailQuickSpread_SheetView2.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.VoucherDetailQuickSpread_SheetView2.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
    Me.VoucherDetailQuickSpread_SheetView2.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.VoucherDetailQuickSpread_SheetView2.SheetCornerStyle.Parent = "CornerDefault"
    Me.VoucherDetailQuickSpread_SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'PostButton
    '
    Me.PostButton.Location = New System.Drawing.Point(449, 72)
    Me.PostButton.Name = "PostButton"
    Me.PostButton.Size = New System.Drawing.Size(75, 23)
    Me.PostButton.TabIndex = 17
    Me.PostButton.Text = "Post"
    Me.PostButton.UseVisualStyleBackColor = True
    '
    'UnpostedLabel
    '
    Me.UnpostedLabel.Location = New System.Drawing.Point(455, 52)
    Me.UnpostedLabel.Name = "UnpostedLabel"
    Me.UnpostedLabel.Size = New System.Drawing.Size(61, 18)
    Me.UnpostedLabel.TabIndex = 18
    Me.UnpostedLabel.Text = "Unposted"
    Me.UnpostedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.UnpostedLabel.Visible = False
    '
    'Quick_Label7
    '
    Me.Quick_Label7.AllowClearValue = False
    Me.Quick_Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Label7.AutoSize = True
    Me.Quick_Label7.DefaultValue = ""
    Me.Quick_Label7.Location = New System.Drawing.Point(279, 503)
    Me.Quick_Label7.Name = "Quick_Label7"
    Me.Quick_Label7.Size = New System.Drawing.Size(62, 13)
    Me.Quick_Label7.TabIndex = 21
    Me.Quick_Label7.Text = "Total Debit:"
    '
    'Quick_Label8
    '
    Me.Quick_Label8.AllowClearValue = False
    Me.Quick_Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Label8.AutoSize = True
    Me.Quick_Label8.DefaultValue = ""
    Me.Quick_Label8.Location = New System.Drawing.Point(495, 503)
    Me.Quick_Label8.Name = "Quick_Label8"
    Me.Quick_Label8.Size = New System.Drawing.Size(67, 13)
    Me.Quick_Label8.TabIndex = 22
    Me.Quick_Label8.Text = "Total Credit::"
    '
    'TotalDebitLabel
    '
    Me.TotalDebitLabel.AllowClearValue = True
    Me.TotalDebitLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TotalDebitLabel.DefaultValue = "0"
    Me.TotalDebitLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TotalDebitLabel.Location = New System.Drawing.Point(348, 494)
    Me.TotalDebitLabel.Name = "TotalDebitLabel"
    Me.TotalDebitLabel.Size = New System.Drawing.Size(141, 31)
    Me.TotalDebitLabel.TabIndex = 23
    Me.TotalDebitLabel.Text = "0"
    Me.TotalDebitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'TotalCreditLabel
    '
    Me.TotalCreditLabel.AllowClearValue = True
    Me.TotalCreditLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TotalCreditLabel.DefaultValue = "0"
    Me.TotalCreditLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TotalCreditLabel.Location = New System.Drawing.Point(556, 494)
    Me.TotalCreditLabel.Name = "TotalCreditLabel"
    Me.TotalCreditLabel.Size = New System.Drawing.Size(138, 31)
    Me.TotalCreditLabel.TabIndex = 24
    Me.TotalCreditLabel.Text = "0"
    Me.TotalCreditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'BalanceCreditLabel
    '
    Me.BalanceCreditLabel.AllowClearValue = True
    Me.BalanceCreditLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.BalanceCreditLabel.DefaultValue = "0"
    Me.BalanceCreditLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BalanceCreditLabel.Location = New System.Drawing.Point(745, 494)
    Me.BalanceCreditLabel.Name = "BalanceCreditLabel"
    Me.BalanceCreditLabel.Size = New System.Drawing.Size(141, 31)
    Me.BalanceCreditLabel.TabIndex = 26
    Me.BalanceCreditLabel.Text = "0"
    Me.BalanceCreditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Quick_Label9
    '
    Me.Quick_Label9.AllowClearValue = False
    Me.Quick_Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Label9.AutoSize = True
    Me.Quick_Label9.DefaultValue = ""
    Me.Quick_Label9.Location = New System.Drawing.Point(697, 503)
    Me.Quick_Label9.Name = "Quick_Label9"
    Me.Quick_Label9.Size = New System.Drawing.Size(49, 13)
    Me.Quick_Label9.TabIndex = 25
    Me.Quick_Label9.Text = "Balance:"
    '
    'VoucherForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(897, 567)
    Me.Controls.Add(Me.BalanceCreditLabel)
    Me.Controls.Add(Me.Quick_Label9)
    Me.Controls.Add(Me.TotalCreditLabel)
    Me.Controls.Add(Me.TotalDebitLabel)
    Me.Controls.Add(Me.Quick_Label8)
    Me.Controls.Add(Me.Quick_Label7)
    Me.Controls.Add(Me.UnpostedLabel)
    Me.Controls.Add(Me.PostButton)
    Me.Controls.Add(Me.VoucherDetailQuickSpread)
    Me.Controls.Add(Me.VoucherDateCalendarCombo)
    Me.Controls.Add(Me.VoucherTypeComboBox)
    Me.Controls.Add(Me.RemarksTextBox)
    Me.Controls.Add(Me.Quick_Label5)
    Me.Controls.Add(Me.VoucherNoTextBox)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.VoucherIDTextBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.KeyPreview = True
    Me.Name = "VoucherForm"
    Me.Text = "VoucherForm"
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.VoucherIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    Me.Controls.SetChildIndex(Me.VoucherNoTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label5, 0)
    Me.Controls.SetChildIndex(Me.RemarksTextBox, 0)
    Me.Controls.SetChildIndex(Me.VoucherTypeComboBox, 0)
    Me.Controls.SetChildIndex(Me.VoucherDateCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.VoucherDetailQuickSpread, 0)
    Me.Controls.SetChildIndex(Me.PostButton, 0)
    Me.Controls.SetChildIndex(Me.UnpostedLabel, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label7, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label8, 0)
    Me.Controls.SetChildIndex(Me.TotalDebitLabel, 0)
    Me.Controls.SetChildIndex(Me.TotalCreditLabel, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label9, 0)
    Me.Controls.SetChildIndex(Me.BalanceCreditLabel, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.VoucherTypeComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.VoucherDateCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.VoucherDetailQuickSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.VoucherDetailQuickSpread_SheetView2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
  Friend WithEvents VoucherIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
  Friend WithEvents VoucherNoTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
  Friend WithEvents RemarksTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label5 As QuickControls.Quick_Label
  Friend WithEvents VoucherTypeComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents VoucherDateCalendarCombo As QuickControls.Quick_UltraCalendarCombo
  'Friend WithEvents UltraDataSource2 As Infragistics.Win.UltraWinDataSource.UltraDataSource
  'Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
  Friend WithEvents grdSalesInvoice As QuickControls.Quick_Spread
  Friend WithEvents VoucherDetailQuickSpread As QuickControls.Quick_Spread
  Friend WithEvents VoucherDetailQuickSpread_SheetView2 As FarPoint.Win.Spread.SheetView
  Friend WithEvents PostButton As System.Windows.Forms.Button
  Friend WithEvents UnpostedLabel As System.Windows.Forms.Label
  Friend WithEvents Quick_Label7 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label8 As QuickControls.Quick_Label
  Friend WithEvents TotalDebitLabel As QuickControls.Quick_Label
  Friend WithEvents TotalCreditLabel As QuickControls.Quick_Label
  Friend WithEvents BalanceCreditLabel As QuickControls.Quick_Label
  Friend WithEvents Quick_Label9 As QuickControls.Quick_Label
End Class
