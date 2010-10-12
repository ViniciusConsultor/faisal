<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferData
  Inherits QuickBaseForms.ParentBasicForm

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
    Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance(1326844)
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton2 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton3 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Me.TransferStatusTextBox1 = New QuickControls.Quick_TextBox
    Me.OverAllUltraProgressBar1 = New QuickControls.Quick_UltraProgressBar
    Me.ProcessUltraProgressBar = New QuickControls.Quick_UltraProgressBar
    Me.DownloadCheckBox = New QuickControls.Quick_CheckBox
    Me.StartDateCalendarCombo = New QuickControls.Quick_UltraCalendarCombo
    Me.ShutdownCheckBox = New QuickControls.Quick_CheckBox
    Me.StartTransferXmlButton = New QuickControls.Quick_Button
    Me.ImportFromXML = New QuickControls.Quick_Button
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
    Me.LastTransferDateCheckBox = New QuickControls.Quick_CheckBox
    CType(Me.StartDateCalendarCombo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TransferStatusTextBox1
    '
    Me.TransferStatusTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TransferStatusTextBox1.BackColor = System.Drawing.SystemColors.Control
    Me.TransferStatusTextBox1.DefaultValue = ""
    Me.TransferStatusTextBox1.ForeColor = System.Drawing.SystemColors.WindowText
    Me.TransferStatusTextBox1.IntegerNumber = 0
    Me.TransferStatusTextBox1.IsMandatory = False
    Me.TransferStatusTextBox1.IsReadonlyForExistingRecord = False
    Me.TransferStatusTextBox1.IsReadonlyForNewRecord = False
    Me.TransferStatusTextBox1.Location = New System.Drawing.Point(6, 69)
    Me.TransferStatusTextBox1.Multiline = True
    Me.TransferStatusTextBox1.Name = "TransferStatusTextBox1"
    Me.TransferStatusTextBox1.PercentNumber = 0
    Me.TransferStatusTextBox1.ReadOnly = True
    Me.TransferStatusTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.TransferStatusTextBox1.Size = New System.Drawing.Size(593, 303)
    Me.TransferStatusTextBox1.TabIndex = 4
    Me.TransferStatusTextBox1.Text = "0"
    Me.TransferStatusTextBox1.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'OverAllUltraProgressBar1
    '
    Me.OverAllUltraProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Appearance1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
    Appearance1.BackColor2 = System.Drawing.Color.Yellow
    Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal
    Appearance1.BackHatchStyle = Infragistics.Win.BackHatchStyle.HorizontalBrick
    Me.OverAllUltraProgressBar1.Appearances.Add(Appearance1)
    Me.OverAllUltraProgressBar1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.OverAllUltraProgressBar1.Location = New System.Drawing.Point(101, 47)
    Me.OverAllUltraProgressBar1.Name = "OverAllUltraProgressBar1"
    Me.OverAllUltraProgressBar1.Size = New System.Drawing.Size(497, 18)
    Me.OverAllUltraProgressBar1.Step = 1
    Me.OverAllUltraProgressBar1.TabIndex = 3
    Me.OverAllUltraProgressBar1.Text = "[Formatted]"
    '
    'ProcessUltraProgressBar
    '
    Me.ProcessUltraProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProcessUltraProgressBar.BorderStyle = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.ProcessUltraProgressBar.Location = New System.Drawing.Point(101, 27)
    Me.ProcessUltraProgressBar.Name = "ProcessUltraProgressBar"
    Me.ProcessUltraProgressBar.Size = New System.Drawing.Size(497, 17)
    Me.ProcessUltraProgressBar.Step = 1
    Me.ProcessUltraProgressBar.TabIndex = 2
    Me.ProcessUltraProgressBar.Text = "[Formatted]"
    '
    'DownloadCheckBox
    '
    Me.DownloadCheckBox.AutoSize = True
    Me.DownloadCheckBox.Checked = True
    Me.DownloadCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
    Me.DownloadCheckBox.DefaultValue = False
    Me.DownloadCheckBox.Location = New System.Drawing.Point(100, 7)
    Me.DownloadCheckBox.Name = "DownloadCheckBox"
    Me.DownloadCheckBox.Size = New System.Drawing.Size(74, 17)
    Me.DownloadCheckBox.TabIndex = 1
    Me.DownloadCheckBox.Text = "Download"
    Me.DownloadCheckBox.UseVisualStyleBackColor = True
    '
    'StartDateCalendarCombo
    '
    Me.StartDateCalendarCombo.BackColor = System.Drawing.SystemColors.Window
    Me.StartDateCalendarCombo.DateButtons.Add(DateButton1)
    Me.StartDateCalendarCombo.DateButtons.Add(DateButton2)
    Me.StartDateCalendarCombo.DateButtons.Add(DateButton3)
    Me.StartDateCalendarCombo.DefaultValue = New Date(2010, 3, 23, 12, 26, 9, 265)
    Me.StartDateCalendarCombo.Format = "dd-MM-yy"
    Me.StartDateCalendarCombo.Location = New System.Drawing.Point(376, 5)
    Me.StartDateCalendarCombo.Name = "StartDateCalendarCombo"
    Me.StartDateCalendarCombo.NonAutoSizeHeight = 21
    Me.StartDateCalendarCombo.Size = New System.Drawing.Size(97, 21)
    Me.StartDateCalendarCombo.TabIndex = 6
    Me.StartDateCalendarCombo.Value = New Date(2009, 5, 12, 0, 0, 0, 0)
    '
    'ShutdownCheckBox
    '
    Me.ShutdownCheckBox.AutoSize = True
    Me.ShutdownCheckBox.DefaultValue = False
    Me.ShutdownCheckBox.Location = New System.Drawing.Point(180, 7)
    Me.ShutdownCheckBox.Name = "ShutdownCheckBox"
    Me.ShutdownCheckBox.Size = New System.Drawing.Size(74, 17)
    Me.ShutdownCheckBox.TabIndex = 8
    Me.ShutdownCheckBox.Text = "Shutdown"
    Me.ShutdownCheckBox.UseVisualStyleBackColor = True
    '
    'StartTransferXmlButton
    '
    Me.StartTransferXmlButton.Location = New System.Drawing.Point(4, 4)
    Me.StartTransferXmlButton.Name = "StartTransferXmlButton"
    Me.StartTransferXmlButton.Size = New System.Drawing.Size(88, 23)
    Me.StartTransferXmlButton.TabIndex = 10
    Me.StartTransferXmlButton.Text = "Start Transfer"
    Me.StartTransferXmlButton.UseVisualStyleBackColor = True
    '
    'ImportFromXML
    '
    Me.ImportFromXML.Location = New System.Drawing.Point(4, 32)
    Me.ImportFromXML.Name = "ImportFromXML"
    Me.ImportFromXML.Size = New System.Drawing.Size(88, 23)
    Me.ImportFromXML.TabIndex = 11
    Me.ImportFromXML.Text = "Import XML"
    Me.ImportFromXML.UseVisualStyleBackColor = True
    '
    'OpenFileDialog1
    '
    Me.OpenFileDialog1.FileName = "OpenFileDialog1"
    '
    'LastTransferDateCheckBox
    '
    Me.LastTransferDateCheckBox.AutoSize = True
    Me.LastTransferDateCheckBox.DefaultValue = False
    Me.LastTransferDateCheckBox.Location = New System.Drawing.Point(260, 7)
    Me.LastTransferDateCheckBox.Name = "LastTransferDateCheckBox"
    Me.LastTransferDateCheckBox.Size = New System.Drawing.Size(114, 17)
    Me.LastTransferDateCheckBox.TabIndex = 12
    Me.LastTransferDateCheckBox.Text = "Last Transfer Date"
    Me.LastTransferDateCheckBox.UseVisualStyleBackColor = True
    '
    'frmTransferData
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(606, 402)
    Me.Controls.Add(Me.LastTransferDateCheckBox)
    Me.Controls.Add(Me.ImportFromXML)
    Me.Controls.Add(Me.StartTransferXmlButton)
    Me.Controls.Add(Me.ShutdownCheckBox)
    Me.Controls.Add(Me.StartDateCalendarCombo)
    Me.Controls.Add(Me.DownloadCheckBox)
    Me.Controls.Add(Me.TransferStatusTextBox1)
    Me.Controls.Add(Me.ProcessUltraProgressBar)
    Me.Controls.Add(Me.OverAllUltraProgressBar1)
    Me.ForeColor = System.Drawing.SystemColors.WindowText
    Me.Name = "frmTransferData"
    Me.Text = "Transfer Data"
    Me.Controls.SetChildIndex(Me.OverAllUltraProgressBar1, 0)
    Me.Controls.SetChildIndex(Me.ProcessUltraProgressBar, 0)
    Me.Controls.SetChildIndex(Me.TransferStatusTextBox1, 0)
    Me.Controls.SetChildIndex(Me.DownloadCheckBox, 0)
    Me.Controls.SetChildIndex(Me.StartDateCalendarCombo, 0)
    Me.Controls.SetChildIndex(Me.ShutdownCheckBox, 0)
    Me.Controls.SetChildIndex(Me.StartTransferXmlButton, 0)
    Me.Controls.SetChildIndex(Me.ImportFromXML, 0)
    Me.Controls.SetChildIndex(Me.LastTransferDateCheckBox, 0)
    CType(Me.StartDateCalendarCombo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TransferStatusTextBox1 As QuickControls.Quick_TextBox
  Friend WithEvents OverAllUltraProgressBar1 As QuickControls.Quick_UltraProgressBar
  Friend WithEvents ProcessUltraProgressBar As QuickControls.Quick_UltraProgressBar
  Friend WithEvents DownloadCheckBox As QuickControls.Quick_CheckBox
  Friend WithEvents StartDateCalendarCombo As QuickControls.Quick_UltraCalendarCombo
  Friend WithEvents ShutdownCheckBox As QuickControls.Quick_CheckBox
  Friend WithEvents StartTransferXmlButton As QuickControls.Quick_Button
  Friend WithEvents ImportFromXML As QuickControls.Quick_Button
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents LastTransferDateCheckBox As QuickControls.Quick_CheckBox
End Class
