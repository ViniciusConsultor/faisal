<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompanyCheckedListBox
  Inherits QuickControls.ExpandableCheckedListBox

    'UserControl overrides dispose to clean up the component list.
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
    CType(Me.UltraExpandableGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'UltraExpandableGroupBox1
    '
    Me.UltraExpandableGroupBox1.Expanded = False
    Me.UltraExpandableGroupBox1.ExpandedSize = New System.Drawing.Size(236, 200)
    Me.UltraExpandableGroupBox1.Size = New System.Drawing.Size(236, 21)
    '
    'CompanyCheckedListBox
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Name = "CompanyCheckedListBox"
    Me.Size = New System.Drawing.Size(236, 21)
    CType(Me.UltraExpandableGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  'Friend WithEvents UltraExpandableGroupBoxPanel1 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
  'Friend WithEvents UltraListView1 As Infragistics.Win.UltraWinListView.UltraListView
End Class
