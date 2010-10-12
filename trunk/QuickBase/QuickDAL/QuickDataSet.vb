Partial Class QuickERP
  Partial Class TransferPattern1_delDataTable

    Private Sub TransferPattern1_delDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
      If (e.Column.ColumnName = Me.Record_IDColumn.ColumnName) Then
        'Add user code here
      End If

    End Sub

  End Class

  Partial Class del_ItemSummaryDataTable

  End Class

End Class