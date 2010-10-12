Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDALLibrary
Imports System.Windows.Forms


Public Class Purchase
  
  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DocumentType = enuDocumentType.Purchase
    Me.lblSaleNo.Text = "P. No.:"
  End Sub

End Class