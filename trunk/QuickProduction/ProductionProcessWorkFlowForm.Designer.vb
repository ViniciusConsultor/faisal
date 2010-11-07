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
    Me.PoductionProcessWorkflowSheetView = New FarPoint.Win.Spread.SheetView
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ProcessWorkFlowQuickSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PoductionProcessWorkflowSheetView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ProcessWorkFlowQuickSpread
    '
    Me.ProcessWorkFlowQuickSpread.AccessibleDescription = "Quick_Spread"
    Me.ProcessWorkFlowQuickSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProcessWorkFlowQuickSpread.AutoNewRow = True
    Me.ProcessWorkFlowQuickSpread.EditModePermanent = True
    Me.ProcessWorkFlowQuickSpread.EditModeReplace = True
    Me.ProcessWorkFlowQuickSpread.Location = New System.Drawing.Point(4, 48)
    Me.ProcessWorkFlowQuickSpread.Name = "ProcessWorkFlowQuickSpread"
    Me.ProcessWorkFlowQuickSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.PoductionProcessWorkflowSheetView})
    Me.ProcessWorkFlowQuickSpread.Size = New System.Drawing.Size(572, 316)
    Me.ProcessWorkFlowQuickSpread.TabIndex = 8
    '
    'PoductionProcessWorkflowSheetView
    '
    Me.PoductionProcessWorkflowSheetView.Reset()
    Me.PoductionProcessWorkflowSheetView.SheetName = "Sheet1"
    '
    'ProcessWorkFlowForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(577, 390)
    Me.Controls.Add(Me.ProcessWorkFlowQuickSpread)
    Me.Name = "ProcessWorkFlowForm"
    Me.Text = "ProductionProcessWorkFlowForm"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.Controls.SetChildIndex(Me.ProcessWorkFlowQuickSpread, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ProcessWorkFlowQuickSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PoductionProcessWorkflowSheetView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ProcessWorkFlowQuickSpread As QuickControls.Quick_Spread
  Friend WithEvents PoductionProcessWorkflowSheetView As FarPoint.Win.Spread.SheetView
End Class
