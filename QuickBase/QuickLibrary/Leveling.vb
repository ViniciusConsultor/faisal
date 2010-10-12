Public Class Leveling
  Public Shared Function GetParentLevel(ByVal _Code As String) As String
    Try
      Dim _LastIndex As Int32 = 0

      If _Code <> String.Empty Then
        _LastIndex = _Code.LastIndexOf(Constants.ITEM_LEVELING_SEPERATOR)

        If _LastIndex > 0 Then
          Return _Code.Substring(0, _LastIndex)
        End If
      End If

      Return Nothing
    Catch ex As Exception
      Throw New QuickLibrary.QuickException("Exception in GetParentLevel(String) function", ex)
    End Try
  End Function

  Public Shared Function GetUptoSpecifiedLevel(ByVal _Code As String, ByVal _LevelNo As Int32) As String
    Try
      Dim _SeperatorIndex As Int32 = 0
      Dim _LevelCount As Int32

      Do
        _SeperatorIndex = _Code.IndexOf(Constants.ITEM_LEVELING_SEPERATOR, _SeperatorIndex)
        If _SeperatorIndex >= 0 Then _LevelCount += 1
      Loop While _SeperatorIndex >= 0 AndAlso _LevelCount < _LevelNo

      If _LevelCount = _LevelNo Then
        'If the current level is found. This part will executed when required level is lesser than total levels
        Return _Code.Substring(0, _SeperatorIndex)
      ElseIf _LevelCount + 1 = _LevelNo Then
        'Last level is not counted by above loop.
        'So if required level is one above the counted level then it means complete code is required.
        Return _Code
      End If

      Return String.Empty
    Catch ex As Exception
      Throw New QuickLibrary.QuickException("Exception in GetUptoSpecifiedLevel(String,Int32) function", ex)
    End Try
  End Function
End Class
