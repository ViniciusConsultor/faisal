<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DefineProcessForm
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
    Me.Quick_Label1 = New QuickControls.Quick_Label
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.Quick_Label3 = New QuickControls.Quick_Label
    Me.ProcessIDTextBox = New QuickControls.Quick_TextBox
    Me.ProcessDescTextBox = New QuickControls.Quick_TextBox
    Me.ProcessCodeTextBox = New QuickControls.Quick_TextBox
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Label1
    '
    Me.Quick_Label1.AllowClearValue = False
    Me.Quick_Label1.AutoSize = True
    Me.Quick_Label1.DefaultValue = ""
    Me.Quick_Label1.Location = New System.Drawing.Point(29, 80)
    Me.Quick_Label1.Name = "Quick_Label1"
    Me.Quick_Label1.Size = New System.Drawing.Size(59, 13)
    Me.Quick_Label1.TabIndex = 8
    Me.Quick_Label1.Text = "Process ID"
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(29, 116)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(73, 13)
    Me.Quick_Label2.TabIndex = 9
    Me.Quick_Label2.Text = "Process Code"
    '
    'Quick_Label3
    '
    Me.Quick_Label3.AllowClearValue = False
    Me.Quick_Label3.AutoSize = True
    Me.Quick_Label3.DefaultValue = ""
    Me.Quick_Label3.Location = New System.Drawing.Point(29, 152)
    Me.Quick_Label3.Name = "Quick_Label3"
    Me.Quick_Label3.Size = New System.Drawing.Size(101, 13)
    Me.Quick_Label3.TabIndex = 10
    Me.Quick_Label3.Text = "Process Description"
    '
    'ProcessIDTextBox
    '
    Me.ProcessIDTextBox.DefaultValue = ""
    Me.ProcessIDTextBox.IntegerNumber = 0
    Me.ProcessIDTextBox.IsMandatory = False
    Me.ProcessIDTextBox.IsReadonlyForExistingRecord = False
    Me.ProcessIDTextBox.IsReadonlyForNewRecord = False
    Me.ProcessIDTextBox.Location = New System.Drawing.Point(140, 76)
    Me.ProcessIDTextBox.Name = "ProcessIDTextBox"
    Me.ProcessIDTextBox.PercentNumber = 0
    Me.ProcessIDTextBox.ReadOnly = True
    Me.ProcessIDTextBox.Size = New System.Drawing.Size(227, 20)
    Me.ProcessIDTextBox.TabIndex = 11
    Me.ProcessIDTextBox.Text = "0"
    Me.ProcessIDTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'ProcessDescTextBox
    '
    Me.ProcessDescTextBox.DefaultValue = ""
    Me.ProcessDescTextBox.IntegerNumber = 0
    Me.ProcessDescTextBox.IsMandatory = False
    Me.ProcessDescTextBox.IsReadonlyForExistingRecord = False
    Me.ProcessDescTextBox.IsReadonlyForNewRecord = False
    Me.ProcessDescTextBox.Location = New System.Drawing.Point(140, 148)
    Me.ProcessDescTextBox.Name = "ProcessDescTextBox"
    Me.ProcessDescTextBox.PercentNumber = 0
    Me.ProcessDescTextBox.Size = New System.Drawing.Size(227, 20)
    Me.ProcessDescTextBox.TabIndex = 1
    Me.ProcessDescTextBox.Text = "0"
    Me.ProcessDescTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'ProcessCodeTextBox
    '
    Me.ProcessCodeTextBox.DefaultValue = ""
    Me.ProcessCodeTextBox.IntegerNumber = 0
    Me.ProcessCodeTextBox.IsMandatory = False
    Me.ProcessCodeTextBox.IsReadonlyForExistingRecord = False
    Me.ProcessCodeTextBox.IsReadonlyForNewRecord = False
    Me.ProcessCodeTextBox.Location = New System.Drawing.Point(140, 112)
    Me.ProcessCodeTextBox.Name = "ProcessCodeTextBox"
    Me.ProcessCodeTextBox.PercentNumber = 0
    Me.ProcessCodeTextBox.Size = New System.Drawing.Size(227, 20)
    Me.ProcessCodeTextBox.TabIndex = 0
    Me.ProcessCodeTextBox.Text = "0"
    Me.ProcessCodeTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.Text
    '
    'DefineProcessForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(777, 412)
    Me.Controls.Add(Me.ProcessCodeTextBox)
    Me.Controls.Add(Me.ProcessDescTextBox)
    Me.Controls.Add(Me.ProcessIDTextBox)
    Me.Controls.Add(Me.Quick_Label3)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.Quick_Label1)
    Me.Name = "DefineProcessForm"
    Me.Text = "Define Production Process Form"
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label3, 0)
    Me.Controls.SetChildIndex(Me.ProcessIDTextBox, 0)
    Me.Controls.SetChildIndex(Me.ProcessDescTextBox, 0)
    Me.Controls.SetChildIndex(Me.ProcessCodeTextBox, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Quick_Label1 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label3 As QuickControls.Quick_Label
  Friend WithEvents ProcessIDTextBox As QuickControls.Quick_TextBox
  Friend WithEvents ProcessDescTextBox As QuickControls.Quick_TextBox
  Friend WithEvents ProcessCodeTextBox As QuickControls.Quick_TextBox
End Class
