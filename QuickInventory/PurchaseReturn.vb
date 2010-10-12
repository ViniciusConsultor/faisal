Imports QuickLibrary.Constants

Public Class PurchaseReturn
  Private Const GRID_BOTTOM_MARGIN As Int32 = 300

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DocumentType = enuDocumentType.PurchaseReturn
    Me.lblSaleNo.Text = "PR. No.:"
  End Sub

End Class