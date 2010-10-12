Imports QuickLibrary
Imports QuickDALLibrary

Public Class GridDataEntryForm
  Private _TableName As String
  Private _ItemTA As New ItemTableAdapter
  Private _DataAdapterObject As New SqlClient.SqlDataAdapter
  Private _CommandBuilderObject As New SqlClient.SqlCommandBuilder
  Private _CommandObject As New SqlClient.SqlCommand
  Private _DataSetObject As New DataSet
  Private _DataTableObject As New DataTable

  Protected Property TableName() As String
    Get
      Return _TableName
    End Get
    Set(ByVal value As String)
      _TableName = value
    End Set
  End Property

  Private Sub UpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      Dim _BlankRow As Boolean
      Dim _RowNo As Int32
      Dim _ColNo As Int32
      Dim _DataRows() As DataRow

      'Remove blank rows
      For _RowNo = 0 To Me.Quick_Spread1_Sheet1.Rows.Count - 1
        _BlankRow = True
        For _ColNo = 0 To Me.Quick_Spread1_Sheet1.ColumnCount - 1
          If Me.Quick_Spread1_Sheet1.Cells(_RowNo, _ColNo).Text <> String.Empty Then
            If Me.Quick_Spread1_Sheet1.RowCount > _DataTableObject.Rows.Count Then
              _DataTableObject.Rows.Add(_DataTableObject.NewRow)
            End If
            _DataTableObject.Rows(_RowNo).Item(_ColNo) = Me.Quick_Spread1_Sheet1.Cells(_RowNo, _ColNo).Value
            _BlankRow = False
          End If
        Next
        If _BlankRow Then Exit For
      Next
      Me.Quick_Spread1_Sheet1.Rows.Count = _RowNo

      'Update data in the database
      For _RowNo = 0 To Me.Quick_Spread1_Sheet1.RowCount - 1
        If _DataTableObject.Rows(_RowNo).RowState <> DataRowState.Unchanged Then
          Try
            ReDim _DataRows(1)
            _DataRows(0) = _DataTableObject.Rows(_RowNo)
            _DataTableObject.Rows(_RowNo).Item("Co_ID") = LoginInfoObject.CompanyID
            _DataAdapterObject.Update(_DataRows)
          Catch ex As SqlClient.SqlException
            MsgBox(ex.Number)
          End Try
        End If
      Next

    Catch ex As Exception
            Dim ExceptionObject As New QuickExceptionAdvanced("Exception in saving records", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Me.Quick_Spread1_Sheet1.Rows.Count = 1000
    End Try
  End Sub

  Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try


    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in clearing records", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub GridDataEntryForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      If Me.TableName <> String.Empty Then
        _CommandObject.Connection = _ItemTA.GetConnection
        _CommandObject.CommandText = "Select * From " & Me.TableName
        _DataAdapterObject.SelectCommand = _CommandObject

        _CommandBuilderObject.DataAdapter = _DataAdapterObject
        _DataAdapterObject.InsertCommand = _CommandBuilderObject.GetInsertCommand()
        _DataAdapterObject.UpdateCommand = _CommandBuilderObject.GetUpdateCommand
        _DataAdapterObject.DeleteCommand = _CommandBuilderObject.GetDeleteCommand

        _DataAdapterObject.Fill(_DataTableObject)
        Me.Quick_Spread1_Sheet1.DataSource = _DataTableObject
        Me.Quick_Spread1_Sheet1.Rows.Count = 1000

      End If

    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in showing columns", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub
End Class