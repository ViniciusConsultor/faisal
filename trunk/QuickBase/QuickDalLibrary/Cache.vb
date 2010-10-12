Public Class Cache
  Private _LoginInfoObject As New LoginInfo

  Public Property LoginInfoObject() As LoginInfo
    Get

      Return _LoginInfoObject

    End Get
    Set(ByVal value As LoginInfo)

      _LoginInfoObject = value

    End Set
  End Property
End Class
