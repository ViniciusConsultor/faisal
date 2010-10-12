Partial Public Class LogicalDataSet

  Partial Class BusinessRuleDataTable
    Public Sub UpdateRuleStatus(ByVal _ColumnName As String, ByVal _RuleName As String, ByVal _RuleDesc As String, ByVal _Valid As Boolean)
      Try
        If _Valid Then
          RemoveBusinessRule(_ColumnName, _RuleName, _RuleDesc)
        Else
          AddBusinessRule(_ColumnName, _RuleName, _RuleDesc)
        End If

      Catch ex As Exception
        Throw ex
      End Try
    End Sub

    Private Sub AddBusinessRule(ByVal _ColumnName As String, ByVal _RuleName As String, ByVal _RuleDesc As String)
      Try
        Dim _Found As Boolean

        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me(I).ColumnName = _ColumnName AndAlso Me(I).RuleName = _RuleName Then
            _Found = True
            Exit For
          End If
        Next

        If _Found Then RemoveBusinessRule(_ColumnName, _RuleName, _RuleDesc)
        AddBusinessRuleRow(_ColumnName, _RuleName, _RuleDesc)

      Catch ex As Exception
        Throw ex
      End Try
    End Sub

    Private Sub RemoveBusinessRule(ByVal _ColumnName As String, ByVal _RuleName As String, ByVal _RuleDesc As String)
      Try

        For I As Int32 = Me.Rows.Count - 1 To 0 Step -1
          If Me(I).ColumnName = _ColumnName AndAlso Me(I).RuleName = _RuleName Then
            Me.RemoveBusinessRuleRow(Me(I))
          End If
        Next

      Catch ex As Exception
        Throw ex
      End Try
    End Sub

  End Class

  Partial Class KeyValuePairDataTable

  End Class

End Class
