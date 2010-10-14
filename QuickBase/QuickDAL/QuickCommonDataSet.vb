﻿Partial Class QuickCommonDataSet
  Partial Class CompanyDataTable

    'Author: Faisal Saleem
    'Date Created(DD-MMM-YY): 05-Feb-2010
    '***** Modification History *****
    '                 Date      Description
    'Name          (DD-MMM-YY) 
    '--------------------------------------------------------------------------------
    '
    ''' <summary>
    ''' It executes the query provided and returns the result in untyped datatable.
    ''' </summary>
    Public Shared Function GetUnTypedDataTableByQuery(ByVal _QueryPara As String) As DataTable
      Dim _CompanyTA As New QuickCommonDataSetTableAdapters.CompanyTableAdapter
      Dim _Connection As SqlClient.SqlConnection = _CompanyTA.Connection
      Dim _DataTable As New DataTable
      Dim _DataAdapter As New SqlClient.SqlDataAdapter(_QueryPara, _Connection)

      Try
        _DataAdapter.Fill(_DataTable)

        Return _DataTable

      Catch ex As Exception
        Throw ex
      Finally
        If _Connection IsNot Nothing And _Connection.State = ConnectionState.Open Then _Connection.Close()
        _CompanyTA = Nothing
        _Connection = Nothing
        _DataAdapter = Nothing
      End Try
    End Function

  End Class
End Class