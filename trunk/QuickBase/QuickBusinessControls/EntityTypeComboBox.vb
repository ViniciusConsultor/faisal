Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDALLibrary
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet

Public Class EntityTypeComboBox
  Dim _EntityTypeTableAdapter As New EntityTypeTableAdapter
  Dim _EntityTypeTable As EntityTypeDataTable

  Public Sub LoadEntityTypes()
    Try
      _EntityTypeTable = _EntityTypeTableAdapter.GetAll

      General.SetColumnCaptions(_EntityTypeTable, Me.FindForm.Name)
      'For Each _EntityTypeRow As QuickERP.EntityTypeRow In _EntityTypeTable.Rows
      '  Select Case _EntityTypeRow.EntityType_ID
      '    Case Constants.EntityTypes.Customer, Constants.EntityTypes.CustomerAndSupplier _
      '      , Constants.EntityTypes.SalesMan, Constants.EntityTypes.Supplier
      '    Case Else
      '      _EntityTypeTable.Rows.Remove(_EntityTypeRow)
      '  End Select
      'Next

      With _EntityTypeTable
        'Revrese loop, to handle the index change due to deletion of column
        For i As Int32 = .Columns.Count - 1 To 0 Step -1
          Select Case .Columns(i).ColumnName
            Case .EntityType_CodeColumn.ColumnName, .EntityType_DescColumn.ColumnName _
              , .EntityType_IDColumn.ColumnName
            Case Else
              .Columns.RemoveAt(i)
          End Select
        Next

        Me.DataSource = _EntityTypeTable
        Me.DisplayMember = .EntityType_DescColumn.ColumnName
        Me.ValueMember = .EntityType_IDColumn.ColumnName
        Me.DropDownWidth = Me.Width
        Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
        Me.Rows.Band.Columns(.EntityType_DescColumn.ColumnName).Width = Me.Width * 100 / 100 - Constants.SCROLLBAR_WIDTH   'Leave space for scroll bar
        Me.Rows.Band.Columns(.EntityType_CodeColumn.ColumnName).Hidden = True
        Me.Rows.Band.Columns(.EntityType_IDColumn.ColumnName).Hidden = True
      End With

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in LoadChildCompanies method.", ex)
    End Try
  End Sub

  Public Property EntityTypeID() As Int32
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.SelectedRow.Cells(_EntityTypeTable.EntityType_IDColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in get method of CompanyID property.", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int32)
      Try
        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_EntityTypeTable.EntityType_IDColumn.ColumnName).Text = value.ToString Then
            Me.SelectedRow = Me.Rows(I)
            Exit For
          End If
        Next
      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in set method of CompanyID property.", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

End Class
