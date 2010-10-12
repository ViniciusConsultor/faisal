<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExpandableCheckedListBox
  Inherits System.Windows.Forms.UserControl

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
    Me.UltraExpandableGroupBox1 = New Infragistics.Win.Misc.UltraExpandableGroupBox
    Me.UltraExpandableGroupBoxPanel1 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Me.UltraListView1 = New Infragistics.Win.UltraWinListView.UltraListView
    CType(Me.UltraExpandableGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.UltraExpandableGroupBox1.SuspendLayout()
    Me.UltraExpandableGroupBoxPanel1.SuspendLayout()
    CType(Me.UltraListView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'UltraExpandableGroupBox1
    '
    Me.UltraExpandableGroupBox1.BackColor = System.Drawing.Color.Transparent
    Me.UltraExpandableGroupBox1.Controls.Add(Me.UltraExpandableGroupBoxPanel1)
    Me.UltraExpandableGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.UltraExpandableGroupBox1.ExpandedSize = New System.Drawing.Size(236, 227)
    Me.UltraExpandableGroupBox1.Location = New System.Drawing.Point(0, 0)
    Me.UltraExpandableGroupBox1.Name = "UltraExpandableGroupBox1"
    Me.UltraExpandableGroupBox1.Size = New System.Drawing.Size(236, 227)
    Me.UltraExpandableGroupBox1.SupportThemes = False
    Me.UltraExpandableGroupBox1.TabIndex = 9
    Me.UltraExpandableGroupBox1.Text = "Company Check List"
    '
    'UltraExpandableGroupBoxPanel1
    '
    Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.UltraListView1)
    Me.UltraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.UltraExpandableGroupBoxPanel1.Location = New System.Drawing.Point(3, 19)
    Me.UltraExpandableGroupBoxPanel1.Name = "UltraExpandableGroupBoxPanel1"
    Me.UltraExpandableGroupBoxPanel1.Size = New System.Drawing.Size(230, 205)
    Me.UltraExpandableGroupBoxPanel1.TabIndex = 0
    '
    'UltraListView1
    '
    Me.UltraListView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.UltraListView1.Location = New System.Drawing.Point(0, 0)
    Me.UltraListView1.Name = "UltraListView1"
    Me.UltraListView1.ShowGroups = False
    Me.UltraListView1.Size = New System.Drawing.Size(230, 205)
    Me.UltraListView1.TabIndex = 0
    Me.UltraListView1.Text = "UltraListView1"
    Me.UltraListView1.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.Details
    Me.UltraListView1.ViewSettingsDetails.CheckBoxStyle = Infragistics.Win.UltraWinListView.CheckBoxStyle.CheckBox
    Me.UltraListView1.ViewSettingsList.CheckBoxStyle = Infragistics.Win.UltraWinListView.CheckBoxStyle.CheckBox
    '
    'ExpandableCheckedListBox
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.UltraExpandableGroupBox1)
    Me.Name = "ExpandableCheckedListBox"
    Me.Size = New System.Drawing.Size(236, 227)
    CType(Me.UltraExpandableGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.UltraExpandableGroupBox1.ResumeLayout(False)
    Me.UltraExpandableGroupBoxPanel1.ResumeLayout(False)
    CType(Me.UltraListView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents UltraExpandableGroupBoxPanel1 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
  Public WithEvents UltraExpandableGroupBox1 As Infragistics.Win.Misc.UltraExpandableGroupBox
  Public WithEvents UltraListView1 As Infragistics.Win.UltraWinListView.UltraListView

End Class
