<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GridDataEntryForm
  Inherits ParentToolbarForm

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
    Me.Quick_Spread1 = New QuickControls.Quick_Spread
    Me.Quick_Spread1_Sheet1 = New FarPoint.Win.Spread.SheetView
    Me.Quick_UltraProgressBar1 = New QuickControls.Quick_UltraProgressBar
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Quick_Spread1_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_Spread1
    '
    Me.Quick_Spread1.AccessibleDescription = "Quick_Spread"
    Me.Quick_Spread1.AutoNewRow = True
    Me.Quick_Spread1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Spread1.Location = New System.Drawing.Point(0, 40)
    Me.Quick_Spread1.Name = "Quick_Spread1"
    Me.Quick_Spread1.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.Quick_Spread1_Sheet1})
    Me.Quick_Spread1.Size = New System.Drawing.Size(498, 188)
    Me.Quick_Spread1.TabIndex = 0
    TipAppearance1.BackColor = System.Drawing.SystemColors.Info
    TipAppearance1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    TipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText
    Me.Quick_Spread1.TextTipAppearance = TipAppearance1
    '
    'Quick_Spread1_Sheet1
    '
    Me.Quick_Spread1_Sheet1.Reset()
    Me.Quick_Spread1_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.Quick_Spread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.Quick_Spread1_Sheet1.AutoUpdateNotes = True
    Me.Quick_Spread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'Quick_UltraProgressBar1
    '
    Me.Quick_UltraProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_UltraProgressBar1.Location = New System.Drawing.Point(0, 228)
    Me.Quick_UltraProgressBar1.Name = "Quick_UltraProgressBar1"
    Me.Quick_UltraProgressBar1.Size = New System.Drawing.Size(500, 16)
    Me.Quick_UltraProgressBar1.TabIndex = 1
    Me.Quick_UltraProgressBar1.Text = "[Formatted]"
    '
    'GridDataEntryForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(498, 266)
    Me.Controls.Add(Me.Quick_UltraProgressBar1)
    Me.Controls.Add(Me.Quick_Spread1)
    Me.Name = "GridDataEntryForm"
    Me.Text = "Data Entry Grid"
    Me.Controls.SetChildIndex(Me.Quick_Spread1, 0)
    Me.Controls.SetChildIndex(Me.Quick_UltraProgressBar1, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Quick_Spread1_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Quick_Spread1_Sheet1 As FarPoint.Win.Spread.SheetView
  Friend WithEvents Quick_UltraProgressBar1 As QuickControls.Quick_UltraProgressBar
  Protected WithEvents Quick_Spread1 As QuickControls.Quick_Spread
End Class
