<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QuickMessageBox
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QuickMessageBox))
    Me.Button2 = New System.Windows.Forms.Button
    Me.Button1 = New System.Windows.Forms.Button
    Me.Button3 = New System.Windows.Forms.Button
    Me.InformVerndorButton = New System.Windows.Forms.Button
    Me.LongMessageBoxPanel = New System.Windows.Forms.Panel
    Me.LongMessageTextBox = New QuickControls.Quick_TextBox
    Me.RecordMessageBoxPanel = New System.Windows.Forms.Panel
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.DontDisplayCheckbox = New QuickControls.Quick_CheckBox
    Me.RecordOperationTextBox = New QuickControls.Quick_TextBox
    Me.MessageNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.ButtonsPanel = New System.Windows.Forms.Panel
    Me.LongMessageBoxPanel.SuspendLayout()
    Me.RecordMessageBoxPanel.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.ButtonsPanel.SuspendLayout()
    Me.SuspendLayout()
    '
    'Button2
    '
    Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Button2.Location = New System.Drawing.Point(84, 4)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(75, 23)
    Me.Button2.TabIndex = 1
    Me.Button2.Text = "Button2"
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(4, 4)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 0
    Me.Button1.Text = "Button1"
    '
    'Button3
    '
    Me.Button3.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Button3.Location = New System.Drawing.Point(164, 4)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(75, 23)
    Me.Button3.TabIndex = 2
    Me.Button3.Text = "Button3"
    '
    'InformVerndorButton
    '
    Me.InformVerndorButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.InformVerndorButton.Location = New System.Drawing.Point(4, 280)
    Me.InformVerndorButton.Name = "InformVerndorButton"
    Me.InformVerndorButton.Size = New System.Drawing.Size(84, 23)
    Me.InformVerndorButton.TabIndex = 0
    Me.InformVerndorButton.Text = "Report Error"
    '
    'LongMessageBoxPanel
    '
    Me.LongMessageBoxPanel.Controls.Add(Me.LongMessageTextBox)
    Me.LongMessageBoxPanel.Controls.Add(Me.InformVerndorButton)
    Me.LongMessageBoxPanel.Location = New System.Drawing.Point(4, 96)
    Me.LongMessageBoxPanel.Name = "LongMessageBoxPanel"
    Me.LongMessageBoxPanel.Size = New System.Drawing.Size(432, 308)
    Me.LongMessageBoxPanel.TabIndex = 1
    '
    'LongMessageTextBox
    '
    Me.LongMessageTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LongMessageTextBox.BackColor = System.Drawing.SystemColors.Control
    Me.LongMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.LongMessageTextBox.DefaultValue = ""
    Me.LongMessageTextBox.IntegerNumber = 0
    Me.LongMessageTextBox.Location = New System.Drawing.Point(8, 8)
    Me.LongMessageTextBox.Multiline = True
    Me.LongMessageTextBox.Name = "LongMessageTextBox"
    Me.LongMessageTextBox.PercentNumber = 0
    Me.LongMessageTextBox.ReadOnly = True
    Me.LongMessageTextBox.Size = New System.Drawing.Size(416, 264)
    Me.LongMessageTextBox.TabIndex = 1
    Me.LongMessageTextBox.Text = "0"
    Me.LongMessageTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'RecordMessageBoxPanel
    '
    Me.RecordMessageBoxPanel.Controls.Add(Me.PictureBox1)
    Me.RecordMessageBoxPanel.Controls.Add(Me.DontDisplayCheckbox)
    Me.RecordMessageBoxPanel.Controls.Add(Me.RecordOperationTextBox)
    Me.RecordMessageBoxPanel.Location = New System.Drawing.Point(4, 4)
    Me.RecordMessageBoxPanel.Name = "RecordMessageBoxPanel"
    Me.RecordMessageBoxPanel.Size = New System.Drawing.Size(432, 89)
    Me.RecordMessageBoxPanel.TabIndex = 2
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(368, 8)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 4
    Me.PictureBox1.TabStop = False
    '
    'DontDisplayCheckbox
    '
    Me.DontDisplayCheckbox.AutoSize = True
    Me.DontDisplayCheckbox.DefaultValue = False
    Me.DontDisplayCheckbox.Location = New System.Drawing.Point(8, 64)
    Me.DontDisplayCheckbox.Name = "DontDisplayCheckbox"
    Me.DontDisplayCheckbox.Size = New System.Drawing.Size(179, 17)
    Me.DontDisplayCheckbox.TabIndex = 1
    Me.DontDisplayCheckbox.Text = "&Don't display this message again"
    Me.DontDisplayCheckbox.UseVisualStyleBackColor = True
    Me.DontDisplayCheckbox.Visible = False
    '
    'RecordOperationTextBox
    '
    Me.RecordOperationTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.RecordOperationTextBox.BackColor = System.Drawing.SystemColors.Control
    Me.RecordOperationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.RecordOperationTextBox.DefaultValue = ""
    Me.RecordOperationTextBox.IntegerNumber = 0
    Me.RecordOperationTextBox.Location = New System.Drawing.Point(8, 8)
    Me.RecordOperationTextBox.Multiline = True
    Me.RecordOperationTextBox.Name = "RecordOperationTextBox"
    Me.RecordOperationTextBox.PercentNumber = 0
    Me.RecordOperationTextBox.ReadOnly = True
    Me.RecordOperationTextBox.Size = New System.Drawing.Size(349, 41)
    Me.RecordOperationTextBox.TabIndex = 0
    Me.RecordOperationTextBox.Text = "0"
    Me.RecordOperationTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'MessageNotifyIcon
    '
    Me.MessageNotifyIcon.Text = "NotifyIcon1"
    Me.MessageNotifyIcon.Visible = True
    '
    'ButtonsPanel
    '
    Me.ButtonsPanel.Controls.Add(Me.Button3)
    Me.ButtonsPanel.Controls.Add(Me.Button2)
    Me.ButtonsPanel.Controls.Add(Me.Button1)
    Me.ButtonsPanel.Location = New System.Drawing.Point(500, 476)
    Me.ButtonsPanel.Name = "ButtonsPanel"
    Me.ButtonsPanel.Size = New System.Drawing.Size(240, 28)
    Me.ButtonsPanel.TabIndex = 0
    '
    'QuickMessageBox
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(743, 512)
    Me.Controls.Add(Me.ButtonsPanel)
    Me.Controls.Add(Me.RecordMessageBoxPanel)
    Me.Controls.Add(Me.LongMessageBoxPanel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "QuickMessageBox"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Message Box"
    Me.LongMessageBoxPanel.ResumeLayout(False)
    Me.LongMessageBoxPanel.PerformLayout()
    Me.RecordMessageBoxPanel.ResumeLayout(False)
    Me.RecordMessageBoxPanel.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ButtonsPanel.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents LongMessageTextBox As QuickControls.Quick_TextBox
  Friend WithEvents InformVerndorButton As System.Windows.Forms.Button
  Friend WithEvents LongMessageBoxPanel As System.Windows.Forms.Panel
  Friend WithEvents RecordMessageBoxPanel As System.Windows.Forms.Panel
  Friend WithEvents RecordOperationTextBox As QuickControls.Quick_TextBox
  Friend WithEvents MessageNotifyIcon As System.Windows.Forms.NotifyIcon
  Friend WithEvents DontDisplayCheckbox As QuickControls.Quick_CheckBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents ButtonsPanel As System.Windows.Forms.Panel

End Class
