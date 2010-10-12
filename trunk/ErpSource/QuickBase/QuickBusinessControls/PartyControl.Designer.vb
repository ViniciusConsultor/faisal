<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PartyControl
  Inherits QuickControls.Quick_UltraComboBoxWithLabel

    'UserControl overrides dispose to clean up the component list.
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
    Me.components = New System.ComponentModel.Container
    Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.PartyAddressTextBox = New System.Windows.Forms.TextBox
    CType(Me.Quick_UltraComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Quick_UltraComboBox1
    '
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.Quick_UltraComboBox1.DisplayLayout.Appearance = Appearance1
    Me.Quick_UltraComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.Quick_UltraComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Me.Quick_UltraComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.Quick_UltraComboBox1.DisplayLayout.MaxColScrollRegions = 1
    Me.Quick_UltraComboBox1.DisplayLayout.MaxRowScrollRegions = 1
    Appearance2.BackColor = System.Drawing.SystemColors.Window
    Appearance2.ForeColor = System.Drawing.SystemColors.ControlText
    Me.Quick_UltraComboBox1.DisplayLayout.Override.ActiveCellAppearance = Appearance2
    Appearance3.BackColor = System.Drawing.SystemColors.Highlight
    Appearance3.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.Quick_UltraComboBox1.DisplayLayout.Override.ActiveRowAppearance = Appearance3
    Me.Quick_UltraComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.Quick_UltraComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance4.BackColor = System.Drawing.SystemColors.Window
    Me.Quick_UltraComboBox1.DisplayLayout.Override.CardAreaAppearance = Appearance4
    Appearance5.BorderColor = System.Drawing.Color.Silver
    Appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.Quick_UltraComboBox1.DisplayLayout.Override.CellAppearance = Appearance5
    Me.Quick_UltraComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.Quick_UltraComboBox1.DisplayLayout.Override.CellPadding = 0
    Appearance6.BackColor = System.Drawing.SystemColors.Control
    Appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance6.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance6.BorderColor = System.Drawing.SystemColors.Window
    Me.Quick_UltraComboBox1.DisplayLayout.Override.GroupByRowAppearance = Appearance6
    Appearance7.TextHAlign = Infragistics.Win.HAlign.Left
    Me.Quick_UltraComboBox1.DisplayLayout.Override.HeaderAppearance = Appearance7
    Me.Quick_UltraComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.Quick_UltraComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance8.BackColor = System.Drawing.SystemColors.Window
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Me.Quick_UltraComboBox1.DisplayLayout.Override.RowAppearance = Appearance8
    Me.Quick_UltraComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance9.BackColor = System.Drawing.SystemColors.ControlLight
    Me.Quick_UltraComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance9
    Me.Quick_UltraComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.Quick_UltraComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.Quick_UltraComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.Quick_UltraComboBox1.DropDownWidth = 343
    Me.Quick_UltraComboBox1.Size = New System.Drawing.Size(343, 22)
    '
    'Quick_Label1
    '
    Me.Quick_Label1.Location = New System.Drawing.Point(105, 4)
    Me.Quick_Label1.Size = New System.Drawing.Size(86, 12)
    '
    'ToolTip1
    '
    Me.ToolTip1.ShowAlways = True
    '
    'PartyAddressTextBox
    '
    Me.PartyAddressTextBox.Location = New System.Drawing.Point(0, 0)
    Me.PartyAddressTextBox.Name = "PartyAddressTextBox"
    Me.PartyAddressTextBox.ReadOnly = True
    Me.PartyAddressTextBox.Size = New System.Drawing.Size(100, 20)
    Me.PartyAddressTextBox.TabIndex = 2
    Me.PartyAddressTextBox.TabStop = False
    Me.PartyAddressTextBox.Text = "vsfsdf"
    '
    'PartyControl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.PartyAddressTextBox)
    Me.Name = "PartyControl"
    Me.Size = New System.Drawing.Size(343, 22)
    Me.Controls.SetChildIndex(Me.PartyAddressTextBox, 0)
    Me.Controls.SetChildIndex(Me.Quick_UltraComboBox1, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label1, 0)
    CType(Me.Quick_UltraComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents PartyAddressTextBox As System.Windows.Forms.TextBox


End Class
