<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DBConnectivityForm
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
    Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
    Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
    Me.lblSQLServerName1 = New System.Windows.Forms.Label
    Me.lblDatabaseName1 = New System.Windows.Forms.Label
    Me.PasswordTextBox = New System.Windows.Forms.TextBox
    Me.lblPassword1 = New System.Windows.Forms.Label
    Me.UserNameTextBox = New System.Windows.Forms.TextBox
    Me.lblUserName1 = New System.Windows.Forms.Label
    Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
    Me.ConnectionTypeOptionSet = New QuickControls.Quick_UltraOptionSet
    Me.PrimaryFilePathButton = New QuickControls.Quick_Button
    Me.DatabaseNameComboBox = New QuickControls.Quick_UltraComboBox
    Me.SQLServerNameComboBox = New QuickControls.Quick_UltraComboBox
    Me.OkButton = New QuickControls.Quick_Button
    Me.CancelButton1 = New QuickControls.Quick_Button
    Me.PrimaryFilePathTextBox = New QuickControls.Quick_TextBox
    Me.DatabaseFileLabel = New QuickControls.Quick_Label
    Me.WindowsSecurityCheckBox = New QuickControls.Quick_CheckBox
    CType(Me.ConnectionTypeOptionSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DatabaseNameComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.SQLServerNameComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'lblSQLServerName1
    '
    Me.lblSQLServerName1.AutoSize = True
    Me.lblSQLServerName1.Location = New System.Drawing.Point(24, 80)
    Me.lblSQLServerName1.Name = "lblSQLServerName1"
    Me.lblSQLServerName1.Size = New System.Drawing.Size(96, 13)
    Me.lblSQLServerName1.TabIndex = 4
    Me.lblSQLServerName1.Text = "SQL Server Name:"
    '
    'lblDatabaseName1
    '
    Me.lblDatabaseName1.AutoSize = True
    Me.lblDatabaseName1.Location = New System.Drawing.Point(24, 184)
    Me.lblDatabaseName1.Name = "lblDatabaseName1"
    Me.lblDatabaseName1.Size = New System.Drawing.Size(87, 13)
    Me.lblDatabaseName1.TabIndex = 12
    Me.lblDatabaseName1.Text = "Database Name:"
    '
    'PasswordTextBox
    '
    Me.PasswordTextBox.Location = New System.Drawing.Point(132, 152)
    Me.PasswordTextBox.Name = "PasswordTextBox"
    Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(43)
    Me.PasswordTextBox.Size = New System.Drawing.Size(260, 20)
    Me.PasswordTextBox.TabIndex = 11
    '
    'lblPassword1
    '
    Me.lblPassword1.AutoSize = True
    Me.lblPassword1.Location = New System.Drawing.Point(24, 156)
    Me.lblPassword1.Name = "lblPassword1"
    Me.lblPassword1.Size = New System.Drawing.Size(56, 13)
    Me.lblPassword1.TabIndex = 10
    Me.lblPassword1.Text = "Password:"
    '
    'UserNameTextBox
    '
    Me.UserNameTextBox.Location = New System.Drawing.Point(132, 124)
    Me.UserNameTextBox.Name = "UserNameTextBox"
    Me.UserNameTextBox.Size = New System.Drawing.Size(260, 20)
    Me.UserNameTextBox.TabIndex = 9
    '
    'lblUserName1
    '
    Me.lblUserName1.AutoSize = True
    Me.lblUserName1.Location = New System.Drawing.Point(24, 128)
    Me.lblUserName1.Name = "lblUserName1"
    Me.lblUserName1.Size = New System.Drawing.Size(63, 13)
    Me.lblUserName1.TabIndex = 8
    Me.lblUserName1.Text = "User Name:"
    '
    'OpenFileDialog
    '
    Me.OpenFileDialog.Filter = "mdf|*.mdf"
    '
    'ConnectionTypeOptionSet
    '
    Me.ConnectionTypeOptionSet.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
    Me.ConnectionTypeOptionSet.ItemAppearance = Appearance1
    ValueListItem1.DataValue = "Default Item"
    ValueListItem1.DisplayText = "SQL Server Connection"
    ValueListItem2.DataValue = "ValueListItem1"
    ValueListItem2.DisplayText = "File Connection"
    Me.ConnectionTypeOptionSet.Items.Add(ValueListItem1)
    Me.ConnectionTypeOptionSet.Items.Add(ValueListItem2)
    Me.ConnectionTypeOptionSet.Location = New System.Drawing.Point(135, 8)
    Me.ConnectionTypeOptionSet.Name = "ConnectionTypeOptionSet"
    Me.ConnectionTypeOptionSet.Size = New System.Drawing.Size(140, 32)
    Me.ConnectionTypeOptionSet.TabIndex = 0
    '
    'PrimaryFilePathButton
    '
    Me.PrimaryFilePathButton.Location = New System.Drawing.Point(368, 48)
    Me.PrimaryFilePathButton.Name = "PrimaryFilePathButton"
    Me.PrimaryFilePathButton.Size = New System.Drawing.Size(24, 20)
    Me.PrimaryFilePathButton.TabIndex = 3
    Me.PrimaryFilePathButton.Text = "..."
    Me.PrimaryFilePathButton.UseVisualStyleBackColor = True
    '
    'DatabaseNameComboBox
    '
    Me.DatabaseNameComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.DatabaseNameComboBox.DisplayMember = ""
    Me.DatabaseNameComboBox.Location = New System.Drawing.Point(132, 180)
    Me.DatabaseNameComboBox.Name = "DatabaseNameComboBox"
    Me.DatabaseNameComboBox.Size = New System.Drawing.Size(260, 22)
    Me.DatabaseNameComboBox.TabIndex = 13
    Me.DatabaseNameComboBox.ValueMember = ""
    '
    'SQLServerNameComboBox
    '
    Me.SQLServerNameComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Me.SQLServerNameComboBox.DisplayMember = ""
    Me.SQLServerNameComboBox.Location = New System.Drawing.Point(132, 76)
    Me.SQLServerNameComboBox.Name = "SQLServerNameComboBox"
    Me.SQLServerNameComboBox.Size = New System.Drawing.Size(260, 22)
    Me.SQLServerNameComboBox.TabIndex = 5
    Me.SQLServerNameComboBox.ValueMember = ""
    '
    'OkButton
    '
    Me.OkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.OkButton.Location = New System.Drawing.Point(133, 245)
    Me.OkButton.Name = "OkButton"
    Me.OkButton.Size = New System.Drawing.Size(68, 22)
    Me.OkButton.TabIndex = 14
    Me.OkButton.Text = "OK"
    Me.OkButton.UseVisualStyleBackColor = True
    '
    'CancelButton1
    '
    Me.CancelButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
    Me.CancelButton1.Location = New System.Drawing.Point(209, 245)
    Me.CancelButton1.Name = "CancelButton1"
    Me.CancelButton1.Size = New System.Drawing.Size(69, 22)
    Me.CancelButton1.TabIndex = 15
    Me.CancelButton1.Text = "Cancel"
    Me.CancelButton1.UseVisualStyleBackColor = True
    '
    'PrimaryFilePathTextBox
    '
    Me.PrimaryFilePathTextBox.IntegerNumber = 0
    Me.PrimaryFilePathTextBox.Location = New System.Drawing.Point(132, 48)
    Me.PrimaryFilePathTextBox.Name = "PrimaryFilePathTextBox"
    Me.PrimaryFilePathTextBox.PercentNumber = 0
    Me.PrimaryFilePathTextBox.Size = New System.Drawing.Size(232, 20)
    Me.PrimaryFilePathTextBox.TabIndex = 2
    Me.PrimaryFilePathTextBox.Text = "0"
    Me.PrimaryFilePathTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'DatabaseFileLabel
    '
    Me.DatabaseFileLabel.AutoSize = True
    Me.DatabaseFileLabel.Location = New System.Drawing.Point(24, 52)
    Me.DatabaseFileLabel.Name = "DatabaseFileLabel"
    Me.DatabaseFileLabel.Size = New System.Drawing.Size(75, 13)
    Me.DatabaseFileLabel.TabIndex = 1
    Me.DatabaseFileLabel.Text = "Database File:"
    '
    'WindowsSecurityCheckBox
    '
    Me.WindowsSecurityCheckBox.AutoSize = True
    Me.WindowsSecurityCheckBox.Location = New System.Drawing.Point(136, 104)
    Me.WindowsSecurityCheckBox.Name = "WindowsSecurityCheckBox"
    Me.WindowsSecurityCheckBox.Size = New System.Drawing.Size(111, 17)
    Me.WindowsSecurityCheckBox.TabIndex = 7
    Me.WindowsSecurityCheckBox.Text = "Windows Security"
    Me.WindowsSecurityCheckBox.UseVisualStyleBackColor = True
    '
    'DBConnectivityForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(411, 296)
    Me.Controls.Add(Me.WindowsSecurityCheckBox)
    Me.Controls.Add(Me.ConnectionTypeOptionSet)
    Me.Controls.Add(Me.PrimaryFilePathButton)
    Me.Controls.Add(Me.DatabaseNameComboBox)
    Me.Controls.Add(Me.SQLServerNameComboBox)
    Me.Controls.Add(Me.OkButton)
    Me.Controls.Add(Me.lblSQLServerName1)
    Me.Controls.Add(Me.lblDatabaseName1)
    Me.Controls.Add(Me.CancelButton1)
    Me.Controls.Add(Me.PasswordTextBox)
    Me.Controls.Add(Me.PrimaryFilePathTextBox)
    Me.Controls.Add(Me.lblPassword1)
    Me.Controls.Add(Me.DatabaseFileLabel)
    Me.Controls.Add(Me.lblUserName1)
    Me.Controls.Add(Me.UserNameTextBox)
    Me.Name = "DBConnectivityForm"
    Me.Text = "Database Connectivity"
    Me.Controls.SetChildIndex(Me.UserNameTextBox, 0)
    Me.Controls.SetChildIndex(Me.lblUserName1, 0)
    Me.Controls.SetChildIndex(Me.DatabaseFileLabel, 0)
    Me.Controls.SetChildIndex(Me.lblPassword1, 0)
    Me.Controls.SetChildIndex(Me.PrimaryFilePathTextBox, 0)
    Me.Controls.SetChildIndex(Me.PasswordTextBox, 0)
    Me.Controls.SetChildIndex(Me.CancelButton1, 0)
    Me.Controls.SetChildIndex(Me.lblDatabaseName1, 0)
    Me.Controls.SetChildIndex(Me.lblSQLServerName1, 0)
    Me.Controls.SetChildIndex(Me.OkButton, 0)
    Me.Controls.SetChildIndex(Me.SQLServerNameComboBox, 0)
    Me.Controls.SetChildIndex(Me.DatabaseNameComboBox, 0)
    Me.Controls.SetChildIndex(Me.PrimaryFilePathButton, 0)
    Me.Controls.SetChildIndex(Me.ConnectionTypeOptionSet, 0)
    Me.Controls.SetChildIndex(Me.WindowsSecurityCheckBox, 0)
    CType(Me.ConnectionTypeOptionSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DatabaseNameComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.SQLServerNameComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents PrimaryFilePathTextBox As QuickControls.Quick_TextBox
  Friend WithEvents DatabaseFileLabel As QuickControls.Quick_Label
  Friend WithEvents OkButton As QuickControls.Quick_Button
  Friend WithEvents CancelButton1 As QuickControls.Quick_Button
  Friend WithEvents DatabaseNameComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents SQLServerNameComboBox As QuickControls.Quick_UltraComboBox
  Friend WithEvents lblSQLServerName1 As System.Windows.Forms.Label
  Friend WithEvents lblDatabaseName1 As System.Windows.Forms.Label
  Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
  Friend WithEvents lblPassword1 As System.Windows.Forms.Label
  Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
  Friend WithEvents lblUserName1 As System.Windows.Forms.Label
  Friend WithEvents PrimaryFilePathButton As QuickControls.Quick_Button
  Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
  Friend WithEvents ConnectionTypeOptionSet As QuickControls.Quick_UltraOptionSet
  Friend WithEvents WindowsSecurityCheckBox As QuickControls.Quick_CheckBox
End Class
