Imports QuickLibrary.Constants

Public Class Receipt

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DocumentType = enuDocumentType.ReceiptVoucher
    Me.VoucherType = 1
  End Sub
End Class