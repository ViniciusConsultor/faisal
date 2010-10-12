<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressWindowForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.ProgressBar = New System.Windows.Forms.ProgressBar
    Me.ProcessNameLabel = New QuickControls.Quick_Label
    Me.CurrentStepLabel = New QuickControls.Quick_Label
    Me.SuspendLayout()
    '
    'ProgressBar
    '
    Me.ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.ProgressBar.Location = New System.Drawing.Point(0, 50)
    Me.ProgressBar.Name = "ProgressBar"
    Me.ProgressBar.Size = New System.Drawing.Size(620, 19)
    Me.ProgressBar.TabIndex = 0
    '
    'ProcessNameLabel
    '
    Me.ProcessNameLabel.AutoSize = True
    Me.ProcessNameLabel.AllowClearValue = False
    Me.ProcessNameLabel.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ProcessNameLabel.Location = New System.Drawing.Point(0, 4)
    Me.ProcessNameLabel.Name = "ProcessNameLabel"
    Me.ProcessNameLabel.Size = New System.Drawing.Size(127, 23)
    Me.ProcessNameLabel.TabIndex = 1
    Me.ProcessNameLabel.Text = "Process Name"
    '
    'CurrentStepLabel
    '
    Me.CurrentStepLabel.AutoSize = True
    Me.CurrentStepLabel.AllowClearValue = False
    Me.CurrentStepLabel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CurrentStepLabel.Location = New System.Drawing.Point(0, 32)
    Me.CurrentStepLabel.Name = "CurrentStepLabel"
    Me.CurrentStepLabel.Size = New System.Drawing.Size(78, 14)
    Me.CurrentStepLabel.TabIndex = 2
    Me.CurrentStepLabel.Text = "Current Step"
    '
    'ProgressWindowForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.LavenderBlush
    Me.ClientSize = New System.Drawing.Size(620, 69)
    Me.ControlBox = False
    Me.Controls.Add(Me.CurrentStepLabel)
    Me.Controls.Add(Me.ProcessNameLabel)
    Me.Controls.Add(Me.ProgressBar)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.Name = "ProgressWindowForm"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "ProgressWindowForm"
    Me.TopMost = True
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Private WithEvents ProgressBar As System.Windows.Forms.ProgressBar
  Private WithEvents ProcessNameLabel As QuickControls.Quick_Label
  Private WithEvents CurrentStepLabel As QuickControls.Quick_Label
End Class
