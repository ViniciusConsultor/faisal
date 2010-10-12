Public Class QuickErpForm
  Private _FormVersion As String
  Private _FormID As String

#Region "Properties"
  Public Property FormVersion() As String
    Get
      Return _FormVersion
    End Get
    Set(ByVal value As String)
      _FormVersion = value
      FormVersionStatusBarLabel.Text = value
    End Set
  End Property

  Public Property FormID() As String
    Get
      Return _FormID
    End Get
    Set(ByVal value As String)
      _FormID = value
      FormIDStatusBarLabel.Text = value
    End Set
  End Property
#End Region
End Class