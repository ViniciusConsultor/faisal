Public Class QuickFunctions

  Public Shared Function GetDateForReportCriteria(ByVal _Date As DateTime) As String
    Try
      Return Format(_Date, "MM-dd-yy")

    Catch ex As Exception
      Dim _QuickException As New QuickException("Exception in getting date format to pass to report.", ex)
      Throw _QuickException
    End Try
  End Function

  <ComponentModel.Description("My description for testing")> _
  Public Shared Function GetDateTimeForReportCriteria1(ByVal _Date As DateTime, ByVal _SetTimeToMidnight As Boolean) As String
    Try
      If Not _SetTimeToMidnight Then
        Return Format(_Date, "MM-dd-yyyy hh:mm tt")
      Else
        _Date = _Date.AddDays(1)
        _Date = _Date.Subtract(New TimeSpan(_Date.Hour, _Date.Minute, _Date.Second + 1))
        Return Format(_Date, "MM-dd-yyyy hh:mm tt")
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickException("Exception in getting date format to pass to report.", ex)
      Throw _QuickException
    End Try
  End Function

  Public Shared Function MaximumTimeOfDate(ByVal _Datepara As Date) As Date
    Try
      _Datepara = _Datepara.AddHours(23 - _Datepara.Hour)
      _Datepara = _Datepara.AddMinutes(59 - _Datepara.Minute)
      _Datepara = _Datepara.AddSeconds(59 - _Datepara.Second)

      Return _Datepara

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in MaximumTimeOfDate function.", ex)
      Throw _qex
    End Try
  End Function
End Class
