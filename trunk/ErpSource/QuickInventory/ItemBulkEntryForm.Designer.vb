<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemBulkEntryForm
  Inherits QuickBaseForms.BulkTransferForm

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
    CType(Me.spread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'spread
    '
    Me.spread.FocusRenderer = DefaultFocusIndicatorRenderer1
    Me.spread.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.spread.HorizontalScrollBar.Name = ""
    Me.spread.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer1
    Me.spread.HorizontalScrollBar.TabIndex = 2
    Me.spread.Location = New System.Drawing.Point(0, 128)
    Me.spread.Size = New System.Drawing.Size(705, 236)
    Me.spread.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.spread.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.spread.VerticalScrollBar.Name = ""
    Me.spread.VerticalScrollBar.Renderer = DefaultScrollBarRenderer2
    Me.spread.VerticalScrollBar.TabIndex = 3
    Me.spread.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'ItemBulkEntryForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(708, 402)
    Me.Name = "ItemBulkEntryForm"
    Me.Text = "ItemBulkEntryForm"
    CType(Me.spread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
End Class
