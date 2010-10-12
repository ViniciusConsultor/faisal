<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataEntryForm
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
    Me.Spread = New QuickControls.Quick_Spread
    Me.Spread_Sheet1 = New FarPoint.Win.Spread.SheetView
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Spread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.Spread_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Spread
    '
    Me.Spread.AccessibleDescription = ""
    Me.Spread.AutoNewRow = False
    Me.Spread.Location = New System.Drawing.Point(8, 40)
    Me.Spread.Name = "Spread"
    Me.Spread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.Spread_Sheet1})
    Me.Spread.Size = New System.Drawing.Size(488, 285)
    Me.Spread.TabIndex = 0
    TipAppearance1.BackColor = System.Drawing.SystemColors.Info
    TipAppearance1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    TipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText
    Me.Spread.TextTipAppearance = TipAppearance1
    '
    'Spread_Sheet1
    '
    Me.Spread_Sheet1.Reset()
    Me.Spread_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.Spread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.Spread_Sheet1.AutoUpdateNotes = True
    Me.Spread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'DataEntryForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(501, 355)
    Me.Controls.Add(Me.Spread)
    Me.Name = "DataEntryForm"
    Me.Text = "DataEntryForm"
    Me.Controls.SetChildIndex(Me.Spread, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Spread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.Spread_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Spread_Sheet1 As FarPoint.Win.Spread.SheetView
  Friend WithEvents Spread As QuickControls.Quick_Spread
End Class
