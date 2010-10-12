' This class will be used to throw the exception and hold inner exception for logging.
' There will be automatic way to logging the exceptions.

Imports System.Windows.Forms

Public Class QuickException
  Inherits Exception

#Region "Contants"
    Protected Const EX_MESSAGE_CAPTION As String = "Exception Manager"

#End Region

  Public Sub New(ByVal message As String, ByVal innerException As Exception)
    MyBase.New(message, innerException)
  End Sub

  Public Function GetFullMessage() As String
    Try
      Dim FinalMessage As String
      Dim exException As Exception
      FinalMessage = String.Empty

      exException = Me
      Do While exException IsNot Nothing
        FinalMessage = GetCustomMessage(exException) & vbCrLf & FinalMessage  'Inner Most Message on the top.
        exException = exException.InnerException
      Loop

      Return FinalMessage
    Catch ex As Exception
      Throw ex
    End Try
  End Function

    Public Function GetCustomMessage(ByVal exException As Exception) As String
        Try
            Dim SqlExceptionObject As SqlClient.SqlException

            If TypeOf exException Is System.Data.SqlClient.SqlException Then
                SqlExceptionObject = DirectCast(exException, SqlClient.SqlException)
                Select Case SqlExceptionObject.Number
                    Case Constants.SQL_EX_LOGIN_FAILED_FOR_USER   'Login failed for user
                        Return ("User Name/ Password provided for database is not valid. (" & exException.Message & ")")
                End Select
            End If

            Return exException.Message
        Catch ex As Exception
            MessageBox.Show(ex.Message, EX_MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return String.Empty
        End Try
    End Function
End Class
