Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDALLibrary
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickERP

Public Class CompanyComboBox
  Private _CompanyTA As New CompanyTableAdapter
  Private _CompanyDataTable As New CompanyDataTable

  Public Sub LoadAllCompanies(ByVal _CompanyIDpara As Int16)
    Try
      _CompanyDataTable = _CompanyTA.GetAll
      SetLayout()

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in LoadThisAndChildCompanies method.", ex)
    End Try
  End Sub

  Public Sub LoadThisAndChildCompanies(ByVal _CompanyIDpara As Int16)
    Try
      _CompanyDataTable = _CompanyTA.GetParentAndChildsByCoID(_CompanyIDpara)
      SetLayout()

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in LoadThisAndChildCompanies method.", ex)
    End Try
  End Sub

  Public Sub SetLayout()
    Try
      QuickDALLibrary.General.SetColumnCaptions(_CompanyDataTable, Me.FindForm.Name)
      With _CompanyDataTable
        'Revrese loop, to handle the index change due to deletion of column
        For i As Int32 = .Columns.Count - 1 To 0 Step -1
          Select Case .Columns(i).ColumnName
            Case .Co_CodeColumn.ColumnName, _
            .Co_DescColumn.ColumnName, .Co_IdColumn.ColumnName
            Case Else
              .Columns.RemoveAt(i)
          End Select
        Next

        Me.DataSource = _CompanyDataTable
        Me.DisplayMember = .Co_DescColumn.ColumnName
        Me.ValueMember = .Co_IdColumn.ColumnName
        Me.DropDownWidth = Me.Width
        Me.Rows.Band.Columns(.Co_CodeColumn.ColumnName).Width = Me.Width * 30 / 100
        Me.Rows.Band.Columns(.Co_DescColumn.ColumnName).Width = Me.Width * 70 / 100 - 10  'Leave space for scroll bar
        Me.Rows.Band.Columns(.Co_IdColumn.ColumnName).Hidden = True
      End With

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetLayout method of CompanyComboBox", ex)
      Throw _qex
    End Try
  End Sub

  Public Property CompanyID() As Int16
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.SelectedRow.Cells(_CompanyDataTable.Co_IdColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in get method of CompanyID property.", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int16)
      Try
        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_CompanyDataTable.Co_IdColumn.ColumnName).Text = value.ToString Then
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
