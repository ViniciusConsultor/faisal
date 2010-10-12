<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchCoa
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
    Dim DefaultFocusIndicatorRenderer1 As FarPoint.Win.Spread.DefaultFocusIndicatorRenderer = New FarPoint.Win.Spread.DefaultFocusIndicatorRenderer
    Dim DefaultScrollBarRenderer1 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Dim DefaultScrollBarRenderer2 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Me.CoaSearchTextBox = New System.Windows.Forms.TextBox
    Me.CoaSearch = New System.Windows.Forms.Label
    Me.CoaSpreadSheet = New FarPoint.Win.Spread.FpSpread
    Me.CoaSpreadSheet_Sheet1 = New FarPoint.Win.Spread.SheetView
    Me.GoCoaSearch = New System.Windows.Forms.Button
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    CType(Me.CoaSpreadSheet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.CoaSpreadSheet_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'CoaSearchTextBox
    '
    Me.CoaSearchTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CoaSearchTextBox.Location = New System.Drawing.Point(104, 20)
    Me.CoaSearchTextBox.Name = "CoaSearchTextBox"
    Me.CoaSearchTextBox.Size = New System.Drawing.Size(276, 20)
    Me.CoaSearchTextBox.TabIndex = 0
    '
    'CoaSearch
    '
    Me.CoaSearch.AutoSize = True
    Me.CoaSearch.Location = New System.Drawing.Point(8, 24)
    Me.CoaSearch.Name = "CoaSearch"
    Me.CoaSearch.Size = New System.Drawing.Size(90, 13)
    Me.CoaSearch.TabIndex = 1
    Me.CoaSearch.Text = "Chart of Account:"
    '
    'CoaSpreadSheet
    '
    Me.CoaSpreadSheet.AccessibleDescription = ""
    Me.CoaSpreadSheet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CoaSpreadSheet.FocusRenderer = DefaultFocusIndicatorRenderer1
    Me.CoaSpreadSheet.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.CoaSpreadSheet.HorizontalScrollBar.Name = ""
    Me.CoaSpreadSheet.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer1
    Me.CoaSpreadSheet.HorizontalScrollBar.TabIndex = 2
    Me.CoaSpreadSheet.Location = New System.Drawing.Point(8, 48)
    Me.CoaSpreadSheet.Name = "CoaSpreadSheet"
    Me.CoaSpreadSheet.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.CoaSpreadSheet_Sheet1})
    Me.CoaSpreadSheet.Size = New System.Drawing.Size(424, 185)
    Me.CoaSpreadSheet.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.CoaSpreadSheet.TabIndex = 2
    Me.CoaSpreadSheet.TabStop = False
    Me.CoaSpreadSheet.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.CoaSpreadSheet.VerticalScrollBar.Name = ""
    Me.CoaSpreadSheet.VerticalScrollBar.Renderer = DefaultScrollBarRenderer2
    Me.CoaSpreadSheet.VerticalScrollBar.TabIndex = 3
    Me.CoaSpreadSheet.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'CoaSpreadSheet_Sheet1
    '
    Me.CoaSpreadSheet_Sheet1.Reset()
    Me.CoaSpreadSheet_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.CoaSpreadSheet_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.CoaSpreadSheet_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.CoaSpreadSheet_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
    Me.CoaSpreadSheet_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.CoaSpreadSheet_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
    Me.CoaSpreadSheet_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.CoaSpreadSheet_Sheet1.SheetCornerStyle.Parent = "CornerDefault"
    Me.CoaSpreadSheet_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'GoCoaSearch
    '
    Me.GoCoaSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GoCoaSearch.Location = New System.Drawing.Point(384, 20)
    Me.GoCoaSearch.Name = "GoCoaSearch"
    Me.GoCoaSearch.Size = New System.Drawing.Size(48, 23)
    Me.GoCoaSearch.TabIndex = 3
    Me.GoCoaSearch.Text = "GO"
    Me.GoCoaSearch.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.CoaSpreadSheet)
    Me.GroupBox1.Controls.Add(Me.GoCoaSearch)
    Me.GroupBox1.Controls.Add(Me.CoaSearchTextBox)
    Me.GroupBox1.Controls.Add(Me.CoaSearch)
    Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(440, 241)
    Me.GroupBox1.TabIndex = 4
    Me.GroupBox1.TabStop = False
    '
    'SearchCoa
    '
    Me.AcceptButton = Me.GoCoaSearch
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(448, 272)
    Me.Controls.Add(Me.GroupBox1)
    Me.KeyPreview = True
    Me.Name = "SearchCoa"
    Me.Text = "SearchCoa"
    Me.Controls.SetChildIndex(Me.GroupBox1, 0)
    CType(Me.CoaSpreadSheet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.CoaSpreadSheet_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents CoaSearchTextBox As System.Windows.Forms.TextBox
  Friend WithEvents CoaSearch As System.Windows.Forms.Label
  Friend WithEvents FpSpread1 As FarPoint.Win.Spread.FpSpread
  Friend WithEvents CoaSpreadSheet_Sheet1 As FarPoint.Win.Spread.SheetView
  Friend WithEvents GoCoaSearch As System.Windows.Forms.Button
  Friend WithEvents CoaSpreadSheet As FarPoint.Win.Spread.FpSpread
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
