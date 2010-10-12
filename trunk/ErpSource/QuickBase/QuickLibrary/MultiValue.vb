Public Class MultiValue
  Private _MultiValueString As String = ""
  Public Const MultiValueSeperator As String = ","
  Private _CurrentValueNumber As Int32 = 0

  Public Property MultiValueString() As String
    Get
      Return _MultiValueString
    End Get
    Set(ByVal value As String)
      _MultiValueString = value
    End Set
  End Property

  Public Sub MoveToStart()
    _CurrentValueNumber = 0
  End Sub

  Public Function NextValue() As String
    Try
      If _MultiValueString = String.Empty Then
        Return Nothing
      Else
        _CurrentValueNumber += 1
        Dim _ValueNumber As Int32 = 1
        Dim _SeperatorIndex As Int32 = -1
        Dim _EndofValueIndex As Int32 = -1

        If _CurrentValueNumber > 1 Then
          Do
            _SeperatorIndex += 1
            _ValueNumber += 1
            _SeperatorIndex = _MultiValueString.IndexOf(MultiValueSeperator, _SeperatorIndex)
          Loop While _SeperatorIndex <> -1
        Else
          _SeperatorIndex = 0
        End If

        'Return the value if found.
        If _SeperatorIndex <> -1 AndAlso _ValueNumber = _CurrentValueNumber Then
          _EndofValueIndex = _MultiValueString.IndexOf(MultiValueSeperator, _SeperatorIndex + 1)
          If _EndofValueIndex = -1 Then
            Return _MultiValueString.Substring(_SeperatorIndex + 1, _MultiValueString.Length - (_SeperatorIndex + 2))
          Else
            Return _MultiValueString.Substring(_SeperatorIndex + 1, _EndofValueIndex - _SeperatorIndex)
          End If
        End If
      End If

      Return ""
    Catch ex As Exception
      Dim _QuickExceptionObject As New QuickLibrary.QuickException("Exception in getting next value", ex)
      Throw _QuickExceptionObject
    End Try
  End Function

  Public Function Count() As Int32
    Try

    Catch ex As Exception
      Dim _QuickExceptionObject As New QuickLibrary.QuickException("Exception in getting count of values", ex)
      Throw _QuickExceptionObject
    End Try
  End Function
End Class
