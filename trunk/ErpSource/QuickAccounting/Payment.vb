Imports QuickLibrary.Constants

Public Class Payment

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DocumentType = enuDocumentType.PaymentVoucher
    FormCode = "04-002"
    FormVersion = "1"
    Me.VoucherType = 2
  End Sub

#Region "Properties"
  
#End Region
End Class