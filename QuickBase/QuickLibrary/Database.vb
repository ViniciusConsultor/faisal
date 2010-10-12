Public Class Database
  Public Shared Function GetDatabasesNames(ByVal ConnectionString As String) As DataTable
    Try
      Dim strQuery As String
      Dim DatabaseNamesDataSet As New DataSet
      Dim DatabaseNamesDataTable As DataTable

      strQuery = "Select Name From sysdatabases"

      Dim daDBObjects As New System.Data.SqlClient.SqlDataAdapter(strQuery, ConnectionString)
      daDBObjects.Fill(DatabaseNamesDataSet)

      DatabaseNamesDataTable = DatabaseNamesDataSet.Tables(0)
      DatabaseNamesDataSet.Tables.Remove(DatabaseNamesDataTable)

      Return DatabaseNamesDataTable

    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Public Shared Function GetSQLServerInstancesAvailable() As DataTable
    Try
      Dim TABLE_NAME_SQL_DATA_SOURCE_ENUMERATOR As String = "SQLServerEnumeration"
      Dim COLUMN_NAME_ENUMERATOR_FULL_NAME As String = "FullName"
      Dim objDataSources As System.Data.Sql.SqlDataSourceEnumerator = System.Data.Sql.SqlDataSourceEnumerator.Instance
      Dim dtbDataSources As DataTable

      dtbDataSources = objDataSources.GetDataSources()
      dtbDataSources.TableName = TABLE_NAME_SQL_DATA_SOURCE_ENUMERATOR
      dtbDataSources.Columns.Add(COLUMN_NAME_ENUMERATOR_FULL_NAME, System.Type.GetType("System.String"), "IIF(InstanceName <> '',(ServerName + '\' + InstanceName),ServerName)")
      dtbDataSources.Columns(dtbDataSources.Columns.Count - 1).SetOrdinal(0)

      Return dtbDataSources

    Catch ex As Exception
      Throw ex
    End Try
  End Function
End Class
