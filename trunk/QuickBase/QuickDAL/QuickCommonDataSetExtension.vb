Namespace QuickCommonDataSetTableAdapters
  Partial Class AlertTableAdapter
    Public Sub New(ByVal ConnectionString As String)
      Me.Connection.ConnectionString = ConnectionString
    End Sub
  End Class
  Partial Class DatabaseTableAdapter
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get
        Return Me.Connection
      End Get
    End Property
  End Class
End Namespace
