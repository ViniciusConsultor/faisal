Imports QuickLibrary

' This class will hold the broken rules which will be used to validate the data.
Public Class BrokenRules
  Inherits CollectionBase

  Private EXC_PRE As String = "Exception in "
  Private EXC_ASSERT As String = EXC_PRE & "Assert"

  Private collRules As New Collection

  ' This method adds then rulename and description to the collection if IsBroken
  ' parameter is set to true and removes if it is set to false. If it already exists
  ' it replaces it.
  Private Sub Assert(ByVal RuleName As String, ByVal RuleDescription As String, ByVal IsBroken As Boolean)
    Try

      If IsBroken = False AndAlso collRules.Contains(RuleName) = True Then
        collRules.Remove(RuleName)
      Else
        If collRules.Contains(RuleName) Then
          collRules.Remove(RuleName)
        End If
        collRules.Add(RuleDescription, RuleName)
      End If
    Catch ex As Exception
      Dim objEx As New QuickException(EXC_ASSERT, ex)
      Throw objEx
    End Try
  End Sub
End Class
