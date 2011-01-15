

Partial Public Class QuickInventoryDataSet
  Partial Class StockInquiryDataTable

    Private Sub StockInquiryDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
      If (e.Column.ColumnName = Me.Co_IDColumn.ColumnName) Then
        'Add user code here
      End If

    End Sub

  End Class

End Class
