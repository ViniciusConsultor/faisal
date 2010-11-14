<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductionOrder
    Inherits System.Windows.Forms.Form

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
    Me.OrderNoLabel = New QuickControls.Quick_Label
    Me.OrderNoTextBox = New QuickControls.Quick_TextBox
    Me.Quick_UltraCalendarCombo1 = New QuickControls.Quick_UltraCalendarCombo
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.ItemMultiComboBox = New QuickBusinessControls.MultiComboBox
    Me.ItemLabel = New QuickControls.Quick_Label
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.Quick_TextBox2 = New QuickControls.Quick_TextBox
    Me.Quick_TextBox3 = New QuickControls.Quick_TextBox
    Me.Quick_Label5 = New QuickControls.Quick_Label
    Me.Quick_Spread1 = New QuickControls.Quick_Spread
    Me.Quick_Spread1_Sheet1 = New FarPoint.Win.Spread.SheetView
    Me.Quick_Spread2 = New QuickControls.Quick_Spread
    Me.Quick_Spread2_Sheet1 = New FarPoint.Win.Spread.SheetView
    CType(Me.Quick_UltraCalendarCombo1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread1_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread2_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OrderNoLabel
    '
    Me.OrderNoLabel.AllowClearValue = False
    Me.OrderNoLabel.AutoSize = True
    Me.OrderNoLabel.DefaultValue = ""
    Me.OrderNoLabel.Location = New System.Drawing.Point(8, 8)
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
    Me.OrderNoTextBox.Location = New System.Drawing.Point(84, 4)
    Me.OrderNoTextBox.Name = "OrderNoTextBox"
    Me.OrderNoTextBox.PercentNumber = 0
    Me.OrderNoTextBox.Size = New System.Drawing.Size(100, 20)
    Me.OrderNoTextBox.TabIndex = 1
    Me.OrderNoTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'Quick_UltraCalendarCombo1
    '
    Me.Quick_UltraCalendarCombo1.BackColor = System.Drawing.SystemColors.Window
    Me.Quick_UltraCalendarCombo1.DateButtons.Add(DateButton1)
    Me.Quick_UltraCalendarCombo1.DefaultValue = New Date(2010, 11, 13, 17, 1, 2, 484)
    Me.Quick_UltraCalendarCombo1.Format = "dd-MM-yy"
    Me.Quick_UltraCalendarCombo1.Location = New System.Drawing.Point(272, 4)
    Me.Quick_UltraCalendarCombo1.Name = "Quick_UltraCalendarCombo1"
    Me.Quick_UltraCalendarCombo1.NonAutoSizeHeight = 0
    Me.Quick_UltraCalendarCombo1.Size = New System.Drawing.Size(121, 21)
    Me.Quick_UltraCalendarCombo1.TabIndex = 2
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(192, 8)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(73, 13)
    Me.Quick_Label2.TabIndex = 3
    Me.Quick_Label2.Text = "Quick_Label2"
    '
    'ItemMultiComboBox
    '
    Me.ItemMultiComboBox.Location = New System.Drawing.Point(84, 28)
    Me.ItemMultiComboBox.Name = "ItemMultiComboBox"
    Me.ItemMultiComboBox.Size = New System.Drawing.Size(161, 20)
    Me.ItemMultiComboBox.TabIndex = 4
    '
    'ItemLabel
    '
    Me.ItemLabel.AllowClearValue = False
    Me.ItemLabel.AutoSize = True
    Me.ItemLabel.DefaultValue = ""
    Me.ItemLabel.Location = New System.Drawing.Point(8, 32)
    Me.ItemLabel.Name = "ItemLabel"
    Me.ItemLabel.Size = New System.Drawing.Size(30, 13)
    Me.ItemLabel.TabIndex = 5
    Me.ItemLabel.Text = "Item:"
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(8, 56)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(73, 13)
    Me.Quick_Label4.TabIndex = 6
    Me.Quick_Label4.Text = "Quick_Label4"
    '
    'Quick_TextBox2
    '
    Me.Quick_TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_TextBox2.DefaultValue = ""
    Me.Quick_TextBox2.IntegerNumber = 0
    Me.Quick_TextBox2.IsMandatory = False
    Me.Quick_TextBox2.IsReadonlyForExistingRecord = False
    Me.Quick_TextBox2.IsReadonlyForNewRecord = False
    Me.Quick_TextBox2.Location = New System.Drawing.Point(84, 52)
    Me.Quick_TextBox2.Name = "Quick_TextBox2"
    Me.Quick_TextBox2.PercentNumber = 0
    Me.Quick_TextBox2.Size = New System.Drawing.Size(502, 20)
    Me.Quick_TextBox2.TabIndex = 7
    Me.Quick_TextBox2.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'Quick_TextBox3
    '
    Me.Quick_TextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_TextBox3.DefaultValue = ""
    Me.Quick_TextBox3.IntegerNumber = 0
    Me.Quick_TextBox3.IsMandatory = False
    Me.Quick_TextBox3.IsReadonlyForExistingRecord = False
    Me.Quick_TextBox3.IsReadonlyForNewRecord = False
    Me.Quick_TextBox3.Location = New System.Drawing.Point(84, 76)
    Me.Quick_TextBox3.Name = "Quick_TextBox3"
    Me.Quick_TextBox3.PercentNumber = 0
    Me.Quick_TextBox3.Size = New System.Drawing.Size(502, 20)
    Me.Quick_TextBox3.TabIndex = 9
    Me.Quick_TextBox3.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'Quick_Label5
    '
    Me.Quick_Label5.AllowClearValue = False
    Me.Quick_Label5.AutoSize = True
    Me.Quick_Label5.DefaultValue = ""
    Me.Quick_Label5.Location = New System.Drawing.Point(8, 80)
    Me.Quick_Label5.Name = "Quick_Label5"
    Me.Quick_Label5.Size = New System.Drawing.Size(73, 13)
    Me.Quick_Label5.TabIndex = 8
    Me.Quick_Label5.Text = "Quick_Label5"
    '
    'Quick_Spread1
    '
    Me.Quick_Spread1.AccessibleDescription = "Quick_Spread"
    Me.Quick_Spread1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Spread1.AutoNewRow = True
    Me.Quick_Spread1.EditModePermanent = True
    Me.Quick_Spread1.EditModeReplace = True
    Me.Quick_Spread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    Me.Quick_Spread1.Location = New System.Drawing.Point(4, 100)
    Me.Quick_Spread1.Name = "Quick_Spread1"
    Me.Quick_Spread1.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.Quick_Spread1_Sheet1})
    Me.Quick_Spread1.Size = New System.Drawing.Size(584, 64)
    Me.Quick_Spread1.TabIndex = 10
    Me.Quick_Spread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    '
    'Quick_Spread1_Sheet1
    '
    Me.Quick_Spread1_Sheet1.Reset()
    Me.Quick_Spread1_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.Quick_Spread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.Quick_Spread1_Sheet1.ColumnHeader.Visible = False
    Me.Quick_Spread1_Sheet1.RowHeader.Visible = False
    Me.Quick_Spread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'Quick_Spread2
    '
    Me.Quick_Spread2.AccessibleDescription = "Quick_Spread"
    Me.Quick_Spread2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Spread2.AutoNewRow = True
    Me.Quick_Spread2.EditModePermanent = True
    Me.Quick_Spread2.EditModeReplace = True
    Me.Quick_Spread2.Location = New System.Drawing.Point(4, 168)
    Me.Quick_Spread2.Name = "Quick_Spread2"
    Me.Quick_Spread2.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.Quick_Spread2_Sheet1})
    Me.Quick_Spread2.Size = New System.Drawing.Size(584, 176)
    Me.Quick_Spread2.TabIndex = 11
    '
    'Quick_Spread2_Sheet1
    '
    Me.Quick_Spread2_Sheet1.Reset()
    Me.Quick_Spread2_Sheet1.SheetName = "Sheet1"
    '
    'ProductionOrder
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(590, 348)
    Me.Controls.Add(Me.Quick_Spread2)
    Me.Controls.Add(Me.Quick_Spread1)
    Me.Controls.Add(Me.Quick_TextBox3)
    Me.Controls.Add(Me.Quick_Label5)
    Me.Controls.Add(Me.Quick_TextBox2)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.ItemLabel)
    Me.Controls.Add(Me.ItemMultiComboBox)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.Quick_UltraCalendarCombo1)
    Me.Controls.Add(Me.OrderNoTextBox)
    Me.Controls.Add(Me.OrderNoLabel)
    Me.Name = "ProductionOrder"
    Me.Text = "Production Order"
    CType(Me.Quick_UltraCalendarCombo1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread1_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread2_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OrderNoLabel As QuickControls.Quick_Label
  Friend WithEvents OrderNoTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_UltraCalendarCombo1 As QuickControls.Quick_UltraCalendarCombo
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents ItemMultiComboBox As QuickBusinessControls.MultiComboBox
  Friend WithEvents ItemLabel As QuickControls.Quick_Label
  Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
  Friend WithEvents Quick_TextBox2 As QuickControls.Quick_TextBox
  Friend WithEvents Quick_TextBox3 As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label5 As QuickControls.Quick_Label
  Friend WithEvents Quick_Spread1 As QuickControls.Quick_Spread
  Friend WithEvents Quick_Spread1_Sheet1 As FarPoint.Win.Spread.SheetView
  Friend WithEvents Quick_Spread2 As QuickControls.Quick_Spread
  Friend WithEvents Quick_Spread2_Sheet1 As FarPoint.Win.Spread.SheetView
End Class
