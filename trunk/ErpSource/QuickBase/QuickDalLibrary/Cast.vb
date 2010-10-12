Imports QuickLibrary

Public Class Cast
    Public Shared Function ToDecimal(ByVal value As String) As Decimal
        Try
            Dim _Result As Decimal

            If Decimal.TryParse(value, _Result) Then
                Return _Result
            End If

            Return 0
        Catch ex As Exception
            ToDecimal = 0
            Dim QuickExceptionObject As New QuickException("Exception in casting value from string to decimal", ex)
            Throw QuickExceptionObject
        End Try
    End Function

    Public Shared Function ToInt32(ByVal value As String) As Int32
        Try
            Dim _Result As Int32

            If Int32.TryParse(value, _Result) Then
                Return _Result
            End If

            Return 0
        Catch ex As Exception
            ToInt32 = 0
            Dim QuickExceptionObject As New QuickException("Exception in casting value from string to int32", ex)
            Throw QuickExceptionObject
        End Try
    End Function

    Public Shared Function ToDateTime(ByVal value As Object) As DateTime
        Try
            Dim _Result As DateTime

      If value IsNot Nothing AndAlso DateTime.TryParse(value.ToString, _Result) Then
        Return _Result
      End If

            Return DateTime.MinValue
        Catch ex As Exception
            ToDateTime = DateTime.MinValue
            Dim QuickExceptionObject As New QuickException("Exception in casting value from object to datetime", ex)
            Throw QuickExceptionObject
        End Try
    End Function
End Class
