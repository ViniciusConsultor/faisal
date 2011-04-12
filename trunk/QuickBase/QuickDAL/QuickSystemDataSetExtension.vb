Namespace QuickSystemDataSetTableAdapters
  Partial Class ObjectTableAdapter
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get
        Return Me.Connection
      End Get
    End Property
  End Class
End Namespace

Public Class QuickSystemDataSetExtension

End Class
