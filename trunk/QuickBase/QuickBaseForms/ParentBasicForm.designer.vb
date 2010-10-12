<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParentBasicForm
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
    Me.sspMaster = New System.Windows.Forms.StatusStrip
    Me.MessageStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.ModeStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.FormIDStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.FormVersionStatusBarLabel = New System.Windows.Forms.ToolStripStatusLabel
    Me.sspMaster.SuspendLayout()
    Me.SuspendLayout()
    '
    'sspMaster
    '
    Me.sspMaster.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MessageStatusBarLabel, Me.ModeStatusBarLabel, Me.FormIDStatusBarLabel, Me.FormVersionStatusBarLabel})
    Me.sspMaster.Location = New System.Drawing.Point(0, 244)
    Me.sspMaster.Name = "sspMaster"
    Me.sspMaster.Size = New System.Drawing.Size(292, 22)
    Me.sspMaster.TabIndex = 3
    Me.sspMaster.Text = "StatusStrip1"
    '
    'MessageStatusBarLabel
    '
    Me.MessageStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.MessageStatusBarLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
    Me.MessageStatusBarLabel.Name = "MessageStatusBarLabel"
    Me.MessageStatusBarLabel.Size = New System.Drawing.Size(95, 17)
    Me.MessageStatusBarLabel.Spring = True
    '
    'ModeStatusBarLabel
    '
    Me.ModeStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.ModeStatusBarLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
    Me.ModeStatusBarLabel.Name = "ModeStatusBarLabel"
    Me.ModeStatusBarLabel.Size = New System.Drawing.Size(37, 17)
    Me.ModeStatusBarLabel.Text = "Mode"
    '
    'FormIDStatusBarLabel
    '
    Me.FormIDStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.FormIDStatusBarLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
    Me.FormIDStatusBarLabel.Name = "FormIDStatusBarLabel"
    Me.FormIDStatusBarLabel.Size = New System.Drawing.Size(49, 17)
    Me.FormIDStatusBarLabel.Text = "Form ID"
    '
    'FormVersionStatusBarLabel
    '
    Me.FormVersionStatusBarLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.FormVersionStatusBarLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
    Me.FormVersionStatusBarLabel.Name = "FormVersionStatusBarLabel"
    Me.FormVersionStatusBarLabel.Size = New System.Drawing.Size(73, 17)
    Me.FormVersionStatusBarLabel.Text = "Form Version"
    '
    'QuickErpForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(292, 266)
    Me.Controls.Add(Me.sspMaster)
    Me.Name = "QuickErpForm"
    Me.Text = "Quick ERP"
    Me.sspMaster.ResumeLayout(False)
    Me.sspMaster.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents sspMaster As System.Windows.Forms.StatusStrip
  Friend WithEvents MessageStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents ModeStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents FormIDStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents FormVersionStatusBarLabel As System.Windows.Forms.ToolStripStatusLabel
End Class
