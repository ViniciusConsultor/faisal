<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSetting
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
    Dim TipAppearance1 As FarPoint.Win.Spread.TipAppearance = New FarPoint.Win.Spread.TipAppearance
    Me.FormIDTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.FormCodeTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.FormNameTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.FormCaptionTextBox = New QuickControls.Quick_TextBox
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.FormControlSettingQuickSpread = New QuickControls.Quick_Spread
    Me.FormControlSettingQuickSpread_Sheet1 = New FarPoint.Win.Spread.SheetView
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.FormControlSettingQuickSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.FormControlSettingQuickSpread_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'FormIDTextBox
    '
    Me.FormIDTextBox.DefaultValue = ""
    Me.FormIDTextBox.IntegerNumber = 0
    Me.FormIDTextBox.IsMandatory = False
    Me.FormIDTextBox.IsReadonlyForExistingRecord = False
    Me.FormIDTextBox.IsReadonlyForNewRecord = False
    Me.FormIDTextBox.Location = New System.Drawing.Point(114, 60)
    Me.FormIDTextBox.Name = "FormIDTextBox"
    Me.FormIDTextBox.PercentNumber = 0
    Me.FormIDTextBox.ReadOnly = True
    Me.FormIDTextBox.Size = New System.Drawing.Size(121, 20)
    Me.FormIDTextBox.TabIndex = 19
    Me.FormIDTextBox.Text = "0"
    Me.FormIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(35, 64)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(44, 13)
    Me.Quick_Label4.TabIndex = 18
    Me.Quick_Label4.Text = "Form ID"
    '
    'FormCodeTextBox
    '
    Me.FormCodeTextBox.DefaultValue = ""
    Me.FormCodeTextBox.IntegerNumber = 0
    Me.FormCodeTextBox.IsMandatory = False
    Me.FormCodeTextBox.IsReadonlyForExistingRecord = False
    Me.FormCodeTextBox.IsReadonlyForNewRecord = False
    Me.FormCodeTextBox.Location = New System.Drawing.Point(330, 60)
    Me.FormCodeTextBox.Name = "FormCodeTextBox"
    Me.FormCodeTextBox.PercentNumber = 0
    Me.FormCodeTextBox.Size = New System.Drawing.Size(121, 20)
    Me.FormCodeTextBox.TabIndex = 0
    Me.FormCodeTextBox.Text = "0"
    Me.FormCodeTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(259, 64)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(58, 13)
    Me.Quick_Label1.TabIndex = 21
    Me.Quick_Label1.Text = "Form Code"
    '
    'FormNameTextBox
    '
    Me.FormNameTextBox.DefaultValue = ""
    Me.FormNameTextBox.IntegerNumber = 0
    Me.FormNameTextBox.IsMandatory = False
    Me.FormNameTextBox.IsReadonlyForExistingRecord = False
    Me.FormNameTextBox.IsReadonlyForNewRecord = False
    Me.FormNameTextBox.Location = New System.Drawing.Point(114, 99)
    Me.FormNameTextBox.Name = "FormNameTextBox"
    Me.FormNameTextBox.PercentNumber = 0
    Me.FormNameTextBox.Size = New System.Drawing.Size(344, 20)
    Me.FormNameTextBox.TabIndex = 1
    Me.FormNameTextBox.Text = "0"
    Me.FormNameTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(35, 103)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(61, 13)
    Me.Quick_Label2.TabIndex = 23
    Me.Quick_Label2.Text = "Form Name"
    '
    'FormCaptionTextBox
    '
    Me.FormCaptionTextBox.DefaultValue = ""
    Me.FormCaptionTextBox.IntegerNumber = 0
    Me.FormCaptionTextBox.IsMandatory = False
    Me.FormCaptionTextBox.IsReadonlyForExistingRecord = False
    Me.FormCaptionTextBox.IsReadonlyForNewRecord = False
    Me.FormCaptionTextBox.Location = New System.Drawing.Point(114, 131)
    Me.FormCaptionTextBox.Name = "FormCaptionTextBox"
    Me.FormCaptionTextBox.PercentNumber = 0
    Me.FormCaptionTextBox.Size = New System.Drawing.Size(344, 20)
    Me.FormCaptionTextBox.TabIndex = 2
    Me.FormCaptionTextBox.Text = "0"
    Me.FormCaptionTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(35, 135)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(69, 13)
    Me.Quick_Label3.TabIndex = 25
    Me.Quick_Label3.Text = "Form Caption"
    '
    'FormControlSettingQuickSpread
    '
    Me.FormControlSettingQuickSpread.AccessibleDescription = "Quick_Spread"
    Me.FormControlSettingQuickSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.FormControlSettingQuickSpread.AutoNewRow = True
    Me.FormControlSettingQuickSpread.BackColor = System.Drawing.SystemColors.Control
    Me.FormControlSettingQuickSpread.EditModePermanent = True
    Me.FormControlSettingQuickSpread.EditModeReplace = True
    Me.FormControlSettingQuickSpread.Location = New System.Drawing.Point(6, 163)
    Me.FormControlSettingQuickSpread.Name = "FormControlSettingQuickSpread"
    Me.FormControlSettingQuickSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.FormControlSettingQuickSpread_Sheet1})
    Me.FormControlSettingQuickSpread.Size = New System.Drawing.Size(1001, 297)
    Me.FormControlSettingQuickSpread.TabIndex = 3
    TipAppearance1.BackColor = System.Drawing.SystemColors.Info
    TipAppearance1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    TipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText
    Me.FormControlSettingQuickSpread.TextTipAppearance = TipAppearance1
    '
    'FormControlSettingQuickSpread_Sheet1
    '
    Me.FormControlSettingQuickSpread_Sheet1.Reset()
    Me.FormControlSettingQuickSpread_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.FormControlSettingQuickSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.FormControlSettingQuickSpread_Sheet1.AutoUpdateNotes = True
    Me.FormControlSettingQuickSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'FormSetting
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1028, 623)
    Me.Controls.Add(Me.FormControlSettingQuickSpread)
    Me.Controls.Add(Me.FormCaptionTextBox)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.FormNameTextBox)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.FormCodeTextBox)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Controls.Add(Me.FormIDTextBox)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Name = "FormSetting"
    Me.Text = "Form Setting"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    Me.Controls.SetChildIndex(Me.FormIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.FormCodeTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.FormNameTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.FormCaptionTextBox, 0)
    Me.Controls.SetChildIndex(Me.FormControlSettingQuickSpread, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.FormControlSettingQuickSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.FormControlSettingQuickSpread_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents FormIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
  Friend WithEvents FormCodeTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
  Friend WithEvents FormNameTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents FormCaptionTextBox As QuickControls.Quick_TextBox
  Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
  Friend WithEvents FormControlSettingQuickSpread As QuickControls.Quick_Spread
  Friend WithEvents FormControlSettingQuickSpread_Sheet1 As FarPoint.Win.Spread.SheetView
End Class
