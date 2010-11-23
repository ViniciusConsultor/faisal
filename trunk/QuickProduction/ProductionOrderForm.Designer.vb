<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductionOrderForm
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
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton2 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton3 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton4 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton5 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton6 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton7 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Me.OrderNoLabel = New QuickControls.Quick_Label
    Me.OrderNoTextBox = New QuickControls.Quick_TextBox
    Me.OrderDateCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.OrderDateLabel = New QuickControls.Quick_Label
    Me.ItemMultiComboBox = New QuickBusinessControls.MultiComboBox
    Me.ItemLabel = New QuickControls.Quick_Label
    Me.FormulaDescriptionLabel = New QuickControls.Quick_Label
    Me.FormulaDescriptionTextBox = New QuickControls.Quick_TextBox
    Me.RemarksTextBox = New QuickControls.Quick_TextBox
    Me.RemarksLabel = New QuickControls.Quick_Label
    Me.SummarySpread = New QuickControls.Quick_Spread
    Me.SummarySheetView = New FarPoint.Win.Spread.SheetView
    Me.FormulaDetailSpread = New QuickControls.Quick_Spread
    Me.FormulaDetailSheetView = New FarPoint.Win.Spread.SheetView
    Me.ProductionOrderSpread = New QuickControls.Quick_Spread
    Me.ProductionOrderSheetView = New FarPoint.Win.Spread.SheetView
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.OrderDateCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.SummarySpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.SummarySheetView, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.FormulaDetailSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.FormulaDetailSheetView, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ProductionOrderSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ProductionOrderSheetView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OrderNoLabel
    '
    Me.OrderNoLabel.AllowClearValue = False
    Me.OrderNoLabel.AutoSize = True
    Me.OrderNoLabel.DefaultValue = ""
    Me.OrderNoLabel.Location = New System.Drawing.Point(8, 52)
    Me.OrderNoLabel.Name = "OrderNoLabel"
    Me.OrderNoLabel.Size = New System.Drawing.Size(56, 13)
    Me.OrderNoLabel.TabIndex = 0
    Me.OrderNoLabel.Text = "Order No.:"
    '
    'OrderNoTextBox
    '
    Me.OrderNoTextBox.DefaultValue = ""
    Me.OrderNoTextBox.IntegerNumber = 0
    Me.OrderNoTextBox.IsMandatory = False
    Me.OrderNoTextBox.IsReadonlyForExistingRecord = False
    Me.OrderNoTextBox.IsReadonlyForNewRecord = False
    Me.OrderNoTextBox.Location = New System.Drawing.Point(84, 48)
    Me.OrderNoTextBox.Name = "OrderNoTextBox"
    Me.OrderNoTextBox.PercentNumber = 0
    Me.OrderNoTextBox.ReadOnly = True
    Me.OrderNoTextBox.Size = New System.Drawing.Size(100, 20)
    Me.OrderNoTextBox.TabIndex = 1
    Me.OrderNoTextBox.Text = "0"
    Me.OrderNoTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'OrderDateCalendarCombo
    '
    Me.OrderDateCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton1)
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton2)
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton3)
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton4)
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton5)
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton6)
    Me.OrderDateCalendarCombo.DateButtons.Add(DateButton7)
    Me.OrderDateCalendarCombo.DefaultValue = New Date(2010, 11, 13, 17, 1, 2, 484)
    Me.OrderDateCalendarCombo.Format = "dd-MM-yy"
    Me.OrderDateCalendarCombo.Location = New System.Drawing.Point(248, 48)
    Me.OrderDateCalendarCombo.Name = "OrderDateCalendarCombo"
    Me.OrderDateCalendarCombo.NonAutoSizeHeight = 21
    Me.OrderDateCalendarCombo.Size = New System.Drawing.Size(121, 21)
    Me.OrderDateCalendarCombo.TabIndex = 2
    Me.OrderDateCalendarCombo.Value = New Date(2010, 11, 14, 0, 0, 0, 0)
    '
    'OrderDateLabel
    '
    Me.OrderDateLabel.AllowClearValue = False
    Me.OrderDateLabel.AutoSize = True
    Me.OrderDateLabel.DefaultValue = ""
    Me.OrderDateLabel.Location = New System.Drawing.Point(192, 52)
    Me.OrderDateLabel.Name = "OrderDateLabel"
    Me.OrderDateLabel.Size = New System.Drawing.Size(33, 13)
    Me.OrderDateLabel.TabIndex = 3
    Me.OrderDateLabel.Text = "Date:"
    '
    'ItemMultiComboBox
    '
    Me.ItemMultiComboBox.Location = New System.Drawing.Point(84, 72)
    Me.ItemMultiComboBox.Name = "ItemMultiComboBox"
    Me.ItemMultiComboBox.Size = New System.Drawing.Size(284, 20)
    Me.ItemMultiComboBox.TabIndex = 4
    '
    'ItemLabel
    '
    Me.ItemLabel.AllowClearValue = False
    Me.ItemLabel.AutoSize = True
    Me.ItemLabel.DefaultValue = ""
    Me.ItemLabel.Location = New System.Drawing.Point(8, 76)
    Me.ItemLabel.Name = "ItemLabel"
    Me.ItemLabel.Size = New System.Drawing.Size(30, 13)
    Me.ItemLabel.TabIndex = 5
    Me.ItemLabel.Text = "Item:"
    '
    'FormulaDescriptionLabel
    '
    Me.FormulaDescriptionLabel.AllowClearValue = False
    Me.FormulaDescriptionLabel.AutoSize = True
    Me.FormulaDescriptionLabel.DefaultValue = ""
    Me.FormulaDescriptionLabel.Location = New System.Drawing.Point(8, 100)
    Me.FormulaDescriptionLabel.Name = "FormulaDescriptionLabel"
    Me.FormulaDescriptionLabel.Size = New System.Drawing.Size(63, 13)
    Me.FormulaDescriptionLabel.TabIndex = 6
    Me.FormulaDescriptionLabel.Text = "Description:"
    '
    'FormulaDescriptionTextBox
    '
    Me.FormulaDescriptionTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.FormulaDescriptionTextBox.DefaultValue = ""
    Me.FormulaDescriptionTextBox.IntegerNumber = 0
    Me.FormulaDescriptionTextBox.IsMandatory = False
    Me.FormulaDescriptionTextBox.IsReadonlyForExistingRecord = False
    Me.FormulaDescriptionTextBox.IsReadonlyForNewRecord = False
    Me.FormulaDescriptionTextBox.Location = New System.Drawing.Point(84, 96)
    Me.FormulaDescriptionTextBox.Name = "FormulaDescriptionTextBox"
    Me.FormulaDescriptionTextBox.PercentNumber = 0
    Me.FormulaDescriptionTextBox.Size = New System.Drawing.Size(502, 20)
    Me.FormulaDescriptionTextBox.TabIndex = 7
    Me.FormulaDescriptionTextBox.Text = "0"
    Me.FormulaDescriptionTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'RemarksTextBox
    '
    Me.RemarksTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.RemarksTextBox.DefaultValue = ""
    Me.RemarksTextBox.IntegerNumber = 0
    Me.RemarksTextBox.IsMandatory = False
    Me.RemarksTextBox.IsReadonlyForExistingRecord = False
    Me.RemarksTextBox.IsReadonlyForNewRecord = False
    Me.RemarksTextBox.Location = New System.Drawing.Point(84, 120)
    Me.RemarksTextBox.Name = "RemarksTextBox"
    Me.RemarksTextBox.PercentNumber = 0
    Me.RemarksTextBox.Size = New System.Drawing.Size(502, 20)
    Me.RemarksTextBox.TabIndex = 9
    Me.RemarksTextBox.Text = "0"
    Me.RemarksTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'RemarksLabel
    '
    Me.RemarksLabel.AllowClearValue = False
    Me.RemarksLabel.AutoSize = True
    Me.RemarksLabel.DefaultValue = ""
    Me.RemarksLabel.Location = New System.Drawing.Point(8, 124)
    Me.RemarksLabel.Name = "RemarksLabel"
    Me.RemarksLabel.Size = New System.Drawing.Size(52, 13)
    Me.RemarksLabel.TabIndex = 8
    Me.RemarksLabel.Text = "Remarks:"
    '
    'SummarySpread
    '
    Me.SummarySpread.AccessibleDescription = "Quick_Spread"
    Me.SummarySpread.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SummarySpread.AutoNewRow = True
    Me.SummarySpread.EditModePermanent = True
    Me.SummarySpread.EditModeReplace = True
    Me.SummarySpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    Me.SummarySpread.Location = New System.Drawing.Point(4, 144)
    Me.SummarySpread.Name = "SummarySpread"
    Me.SummarySpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.SummarySheetView})
    Me.SummarySpread.Size = New System.Drawing.Size(584, 64)
    Me.SummarySpread.TabIndex = 10
    Me.SummarySpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    '
    'SummarySheetView
    '
    Me.SummarySheetView.Reset()
    Me.SummarySheetView.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.SummarySheetView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.SummarySheetView.ColumnHeader.Visible = False
    Me.SummarySheetView.RowHeader.Visible = False
    Me.SummarySheetView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'FormulaDetailSpread
    '
    Me.FormulaDetailSpread.AccessibleDescription = "Quick_Spread"
    Me.FormulaDetailSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.FormulaDetailSpread.AutoNewRow = True
    Me.FormulaDetailSpread.EditModePermanent = True
    Me.FormulaDetailSpread.EditModeReplace = True
    Me.FormulaDetailSpread.Location = New System.Drawing.Point(4, 260)
    Me.FormulaDetailSpread.Name = "FormulaDetailSpread"
    Me.FormulaDetailSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.FormulaDetailSheetView})
    Me.FormulaDetailSpread.Size = New System.Drawing.Size(584, 112)
    Me.FormulaDetailSpread.TabIndex = 11
    '
    'FormulaDetailSheetView
    '
    Me.FormulaDetailSheetView.Reset()
    Me.FormulaDetailSheetView.SheetName = "Sheet1"
    '
    'ProductionOrderSpread
    '
    Me.ProductionOrderSpread.AccessibleDescription = "Quick_Spread"
    Me.ProductionOrderSpread.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProductionOrderSpread.AutoNewRow = True
    Me.ProductionOrderSpread.EditModePermanent = True
    Me.ProductionOrderSpread.EditModeReplace = True
    Me.ProductionOrderSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    Me.ProductionOrderSpread.Location = New System.Drawing.Point(3, 212)
    Me.ProductionOrderSpread.Name = "ProductionOrderSpread"
    Me.ProductionOrderSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.ProductionOrderSheetView})
    Me.ProductionOrderSpread.Size = New System.Drawing.Size(584, 44)
    Me.ProductionOrderSpread.TabIndex = 12
    Me.ProductionOrderSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    '
    'ProductionOrderSheetView
    '
    Me.ProductionOrderSheetView.Reset()
    Me.ProductionOrderSheetView.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.ProductionOrderSheetView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.ProductionOrderSheetView.RowHeader.Visible = False
    Me.ProductionOrderSheetView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'ProductionOrderForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(590, 398)
    Me.Controls.Add(Me.ProductionOrderSpread)
    Me.Controls.Add(Me.FormulaDetailSpread)
    Me.Controls.Add(Me.SummarySpread)
    Me.Controls.Add(Me.RemarksTextBox)
    Me.Controls.Add(Me.RemarksLabel)
    Me.Controls.Add(Me.FormulaDescriptionTextBox)
    Me.Controls.Add(Me.FormulaDescriptionLabel)
    Me.Controls.Add(Me.ItemLabel)
    Me.Controls.Add(Me.ItemMultiComboBox)
    Me.Controls.Add(Me.OrderDateLabel)
    Me.Controls.Add(Me.OrderDateCalendarCombo)
    Me.Controls.Add(Me.OrderNoTextBox)
    Me.Controls.Add(Me.OrderNoLabel)
    Me.Name = "ProductionOrderForm"
    Me.Text = "Production Order"
    Me.Controls.SetChildIndex(Me.OrderNoLabel, 0)
    Me.Controls.SetChildIndex(Me.OrderNoTextBox, 0)
    Me.Controls.SetChildIndex(Me.OrderDateCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.OrderDateLabel, 0)
    Me.Controls.SetChildIndex(Me.ItemMultiComboBox, 0)
    Me.Controls.SetChildIndex(Me.ItemLabel, 0)
    Me.Controls.SetChildIndex(Me.FormulaDescriptionLabel, 0)
    Me.Controls.SetChildIndex(Me.FormulaDescriptionTextBox, 0)
    Me.Controls.SetChildIndex(Me.RemarksLabel, 0)
    Me.Controls.SetChildIndex(Me.RemarksTextBox, 0)
    Me.Controls.SetChildIndex(Me.SummarySpread, 0)
    Me.Controls.SetChildIndex(Me.FormulaDetailSpread, 0)
    Me.Controls.SetChildIndex(Me.ProductionOrderSpread, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.OrderDateCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.SummarySpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.SummarySheetView, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.FormulaDetailSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.FormulaDetailSheetView, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ProductionOrderSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ProductionOrderSheetView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OrderNoLabel As QuickControls.Quick_Label
  Friend WithEvents OrderNoTextBox As QuickControls.Quick_TextBox
  Friend WithEvents OrderDateCalendarCombo As QuickControls.Quick_UltraCalendarCombo
  Friend WithEvents OrderDateLabel As QuickControls.Quick_Label
  Friend WithEvents ItemMultiComboBox As QuickBusinessControls.MultiComboBox
  Friend WithEvents ItemLabel As QuickControls.Quick_Label
  Friend WithEvents FormulaDescriptionLabel As QuickControls.Quick_Label
  Friend WithEvents FormulaDescriptionTextBox As QuickControls.Quick_TextBox
  Friend WithEvents RemarksTextBox As QuickControls.Quick_TextBox
  Friend WithEvents RemarksLabel As QuickControls.Quick_Label
  Friend WithEvents SummarySpread As QuickControls.Quick_Spread
  Friend WithEvents SummarySheetView As FarPoint.Win.Spread.SheetView
  Friend WithEvents FormulaDetailSpread As QuickControls.Quick_Spread
  Friend WithEvents FormulaDetailSheetView As FarPoint.Win.Spread.SheetView
  Friend WithEvents ProductionOrderSpread As QuickControls.Quick_Spread
  Friend WithEvents ProductionOrderSheetView As FarPoint.Win.Spread.SheetView
End Class
