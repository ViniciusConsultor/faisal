<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesInvoice1
  Inherits InventoryForm

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
    Me.SalesTaxLabel = New QuickControls.Quick_Label
    Me.SalesTaxTextBox = New QuickControls.Quick_TextBox
    Me.DiscountLabel = New QuickControls.Quick_Label
    Me.DiscountTextBox = New QuickControls.Quick_TextBox
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'SalesTaxLabel
    '
    Me.SalesTaxLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.SalesTaxLabel.AutoSize = True
    Me.SalesTaxLabel.Location = New System.Drawing.Point(148, 440)
    Me.SalesTaxLabel.Name = "SalesTaxLabel"
    Me.SalesTaxLabel.Size = New System.Drawing.Size(57, 13)
    Me.SalesTaxLabel.TabIndex = 16
    Me.SalesTaxLabel.Text = "Sales Tax:"
    '
    'SalesTaxTextBox
    '
    Me.SalesTaxTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.SalesTaxTextBox.Location = New System.Drawing.Point(208, 436)
    Me.SalesTaxTextBox.Name = "SalesTaxTextBox"
    Me.SalesTaxTextBox.Size = New System.Drawing.Size(80, 20)
    Me.SalesTaxTextBox.TabIndex = 15
    '
    'DiscountLabel
    '
    Me.DiscountLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.DiscountLabel.AutoSize = True
    Me.DiscountLabel.Location = New System.Drawing.Point(8, 440)
    Me.DiscountLabel.Name = "DiscountLabel"
    Me.DiscountLabel.Size = New System.Drawing.Size(52, 13)
    Me.DiscountLabel.TabIndex = 14
    Me.DiscountLabel.Text = "Discount:"
    '
    'DiscountTextBox
    '
    Me.DiscountTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.DiscountTextBox.Location = New System.Drawing.Point(64, 436)
    Me.DiscountTextBox.Name = "DiscountTextBox"
    Me.DiscountTextBox.Size = New System.Drawing.Size(80, 20)
    Me.DiscountTextBox.TabIndex = 13
    '
    'SalesInvoice
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(804, 486)
    Me.Controls.Add(Me.SalesTaxLabel)
    Me.Controls.Add(Me.SalesTaxTextBox)
    Me.Controls.Add(Me.DiscountLabel)
    Me.Controls.Add(Me.DiscountTextBox)
    Me.Name = "SalesInvoice"
    Me.Text = "SalesInvoice"
    Me.Controls.SetChildIndex(Me.DiscountTextBox, 0)
    Me.Controls.SetChildIndex(Me.DiscountLabel, 0)
    Me.Controls.SetChildIndex(Me.SalesTaxTextBox, 0)
    Me.Controls.SetChildIndex(Me.SalesTaxLabel, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents SalesTaxLabel As QuickControls.Quick_Label
  Friend WithEvents SalesTaxTextBox As QuickControls.Quick_TextBox
  Friend WithEvents DiscountLabel As QuickControls.Quick_Label
  Friend WithEvents DiscountTextBox As QuickControls.Quick_TextBox
End Class
