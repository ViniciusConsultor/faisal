Namespace QuickERPTableAdapters

  Partial Public Class SalesInvoiceTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"

    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Dim QuickExceptionObject As New QuickLibrary.QuickException("Exception to get active transaction", ex)
          Throw QuickExceptionObject
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Dim QuickExceptionObject As New QuickLibrary.QuickException("Exception to set active transaction", ex)
          Throw QuickExceptionObject
        End Try
      End Set
    End Property

    Public Function BeginTransaction() As Boolean
      Try

        SqlTransactionObject = GetConnection.BeginTransaction

        Return True

      Catch ex As Exception
        Dim exException As New QuickLibrary.QuickException("Exception to begin transaction", ex)
        Throw exException
      End Try
    End Function

    Public Function CommitTransaction() As Boolean
      Try

        SqlTransaction.Commit()

        Return True

      Catch ex As Exception
        Dim exException As New QuickLibrary.QuickException("Exception to commit transaction", ex)
        Throw exException
      End Try
    End Function

    Public Function RollbackTransaction() As Boolean
      Try

        SqlTransaction.Rollback()

        Return True

      Catch ex As Exception
        Dim exException As New QuickLibrary.QuickException("Exception to rollback transaction", ex)
        Throw exException
      End Try
    End Function
#End Region

  End Class

End Namespace
