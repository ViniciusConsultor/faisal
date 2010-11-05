<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessWorkFlowForm
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
    Me.ProcessWorkFlowQuickSpread = New QuickControls.Quick_Spread
    Me.SheetView1 = New FarPoint.Win.Spread.SheetView
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ProcessWorkFlowQuickSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.SheetView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ProcessWorkFlowQuickSpread
    '
    Me.ProcessWorkFlowQuickSpread.AccessibleDescription = "Quick_Spread"
    Me.ProcessWorkFlowQuickSpread.AutoNewRow = True
    Me.ProcessWorkFlowQuickSpread.EditModePermanent = True
    Me.ProcessWorkFlowQuickSpread.EditModeReplace = True
    Me.ProcessWorkFlowQuickSpread.Location = New System.Drawing.Point(12, 70)
    Me.ProcessWorkFlowQuickSpread.Name = "ProcessWorkFlowQuickSpread"
    Me.ProcessWorkFlowQuickSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.SheetView1})
    Me.ProcessWorkFlowQuickSpread.Size = New System.Drawing.Size(553, 288)
    Me.ProcessWorkFlowQuickSpread.TabIndex = 8
    '
    'SheetView1
    '
    Me.SheetView1.Reset()
    Me.SheetView1.SheetName = "Sheet1"
    '
    'ProcessWorkFlowForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(976, 504)
    Me.Controls.Add(Me.ProcessWorkFlowQuickSpread)
    Me.Name = "ProcessWorkFlowForm"
    Me.Text = "ProductionProcessWorkFlowForm"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.Controls.SetChildIndex(Me.ProcessWorkFlowQuickSpread, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ProcessWorkFlowQuickSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.SheetView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ProcessWorkFlowQuickSpread As QuickControls.Quick_Spread
  Friend WithEvents SheetView1 As FarPoint.Win.Spread.SheetView
End Class
