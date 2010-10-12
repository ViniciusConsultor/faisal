Imports QuickDALLibrary
Imports QuickDAL.LogicalDataSet
Imports QuickDAL.QuickCommonDataSet
Imports QuickLibrary

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 11-Jan-09
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'
''' <summary>
''' This form is used to search values
''' </summary>
Public Class FilterForm

#Region "Declarations"
  Private _SearchOptionTA As New SearchOptionTableAdapter
  Private _SearchOptionDetailTA As New SearchOptionDetailTableAdapter

  Private _SearchOptionDataTable As SearchOptionDataTable
  Private _SearchOptionDetailDataTable As SearchOptionDetailDataTable
  Private _SearchComparisonNumeric As New SearchComparisonTypeDataTable
  Private _SearchComparisonString As New SearchComparisonTypeDataTable

  Private _SearchTableName As String
  Private _SearchResultUnTypedDataTable As DataTable
  Private _IsString As Boolean = False
  Private _SearchOption As SearchOptionIDs

  Public Enum SearchOptionIDs
    SalesInvoice = 1
    PosSalesInvoice = 2
    Purchase = 3
    PurchaseReturn = 4
    SelesReturnInvoice = 5
    PurchaseWarehouse = 6
    '    Receipt
    '    Payment
    '    Item
    '    Party
    '    COA
    '    VoucherType
    '    VoucherEntry
  End Enum

#End Region

#Region "Properties"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 05-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will give the datatable with search result(s).
  ''' </summary>
  Public ReadOnly Property SearchResultUnTypedDataTable() As DataTable
    Get
      Try

        Return _SearchResultUnTypedDataTable

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SearchResultUnTypedDataTable of SearchForm.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 6-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Current Search Option
  ''' </summary>
  Public Property SearchOption() As SearchOptionIDs
    Get
      Try

        Return _SearchOption

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SearchOption of SearchForm.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As SearchOptionIDs)
      Try

        _SearchOption = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SearchOption of SearchForm.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Private _DocumentType As Constants.enuDocumentType
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 22-Feb-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Document Type can be needed withing query to fetch the records.
  ''' </summary>
  Public Property DocumentType() As Constants.enuDocumentType
    Get
      Try

        Return _DocumentType

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in DocumentType of SearchForm.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Constants.enuDocumentType)
      Try

        _DocumentType = value

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in DocumentType of SearchForm.", ex)
        Throw _qex
      End Try
    End Set
  End Property


  ''Author: Faisal Saleem
  ''Date Created(DD-MMM-YY): 05-Feb-2010
  ''***** Modification History *****
  ''                 Date      Description
  ''Name          (DD-MMM-YY) 
  ''--------------------------------------------------------------------------------
  ''
  '''' <summary>
  '''' This is name of the table which will be searched.
  '''' </summary>
  'Public Property SearchTableName() As String
  '  Get
  '    Try

  '      Return _SearchTableName

  '    Catch ex As Exception
  '      Dim _qex As New QuickExceptionAdvanced("Exception in SearchTableName of SearchForm.", ex)
  '      Throw _qex
  '    End Try
  '  End Get
  '  Set(ByVal value As String)
  '    Try

  '      _SearchTableName = value

  '    Catch ex As Exception
  '      Dim _qex As New QuickExceptionAdvanced("Exception in SearchTableName of SearchForm.", ex)
  '      Throw _qex
  '    End Try
  '  End Set
  'End Property

  ''Author: Faisal Saleem
  ''Date Created(DD-MMM-YY): 11-Jan-09
  ''***** Modification History *****
  ''Name   Date(DD-MMM-YY)   Description
  ''--------------------------------------------------------------------------------
  ''
  '''' <summary>
  '''' SearchOptions DataTable will used to fill search combo and respective search
  '''' types. This will be further used to create query to fetch records.
  '''' </summary>
  'Private Property SearchOptionsDataTable() As QuickDAL.LogicalDataSet.SearchOptionDataTable
  '  Get
  '    Try

  '      If _SearchOptionsDataTable Is Nothing Then
  '        _SearchOptionsDataTable = New SearchOptionDataTable
  '      End If
  '      Return _SearchOptionsDataTable

  '    Catch ex As Exception
  '      Dim _qex As New QuickExceptionAdvanced("Exception in SearchOptionsDataTable of SearchForm.", ex)
  '      Throw _qex
  '    End Try
  '  End Get
  '  Set(ByVal value As QuickDAL.LogicalDataSet.SearchOptionDataTable)
  '    Try

  '      _SearchOptionsDataTable = value

  '    Catch ex As Exception
  '      Dim _qex As New QuickExceptionAdvanced("Exception in SearchOptionsDataTable of SearchForm.", ex)
  '      Throw _qex
  '    End Try
  '  End Set
  'End Property

#End Region

#Region "Methods"

#End Region

#Region "Event Methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 13-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Load Event
  ''' </summary>
  Private Sub SearchForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try

      With Me._SearchComparisonNumeric
        .AddSearchComparisonTypeRow(">", ">")
        .AddSearchComparisonTypeRow("<", "<")
        .AddSearchComparisonTypeRow(">=", ">=")
        .AddSearchComparisonTypeRow("<=", "<=")
        .AddSearchComparisonTypeRow("=", "=")
        .AddSearchComparisonTypeRow("<>", "<>")
      End With

      With Me._SearchComparisonString
        .AddSearchComparisonTypeRow("Equal", "Equal")
        .AddSearchComparisonTypeRow("StartsWith", "Starts With")
        .AddSearchComparisonTypeRow("EndsWith", "Ends With")
        .AddSearchComparisonTypeRow("Contains", "Contains")
      End With

      _SearchOptionDataTable = _SearchOptionTA.GetBySearchOptionID(SearchOption)
      _SearchOptionDetailDataTable = _SearchOptionDetailTA.GetBySearchOptionID(SearchOption)

      With Me.SearchItemComboBox
        .DataSource = _SearchOptionDetailDataTable
        .DisplayMember = _SearchOptionDetailDataTable.ColumnCaptionColumn.ColumnName
        .ValueMember = _SearchOptionDetailDataTable.SearchOptionDetail_IDColumn.ColumnName
        For I As Int32 = 0 To _SearchOptionDetailDataTable.Columns.Count - 1
          If _SearchOptionDetailDataTable.Columns(I).ColumnName <> _SearchOptionDetailDataTable.ColumnCaptionColumn.ColumnName Then
            .Rows.Band.Columns(_SearchOptionDetailDataTable.Columns(I).ColumnName).Hidden = True
          Else
            'Nothing to do for column to display
          End If
        Next I
        .Rows.Band.ColHeadersVisible = False
        Me.SearchItemComboBox_Resize(Me.SearchItemComboBox, Nothing)

        For I As Int32 = 0 To .Rows.Count - 1
          If _SearchOptionDetailDataTable(I).IsHidden Then
            .Rows(I).Hidden = True
          ElseIf _SearchOptionDetailDataTable(I).IsDefault Then
            .Rows(I).Selected = True
          End If
        Next

      End With

      Me.SearchComparisonTypeComboBox.Rows.Band.ColHeadersVisible = False

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SearchForm_Load event method of SearchForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Jan-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' No Description.
  ''' </summary>
  Private Sub SearchItemComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchItemComboBox.Resize
    Try
      Me.SearchItemComboBox.Rows.Band.Columns(Me._SearchOptionDetailDataTable.ColumnCaptionColumn.ColumnName).Width = Me.SearchItemComboBox.Width - Constants.SCROLLBAR_WIDTH

    Catch ex As Exception
      'Ignore all errors
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Jan-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' No Description.
  ''' </summary>
  Private Sub SearchComparisonTypeComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchComparisonTypeComboBox.Resize
    Try

      Me.SearchComparisonTypeComboBox.Rows.Band.Columns(_SearchComparisonNumeric.ComparisonCaptionColumn.ColumnName).Width = Me.SearchItemComboBox.Width - Constants.SCROLLBAR_WIDTH

    Catch ex As Exception
      'Ignore all errors.
    End Try
  End Sub

  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 13-Jan-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Fired on row selection
  ''' </summary>
  Private Sub SearchItemComboBox_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles SearchItemComboBox.RowSelected
    Try
      If Me.SearchItemComboBox.SelectedRow IsNot Nothing Then

        Select Case CType(Me.SearchItemComboBox.SelectedRow.Cells(_SearchOptionDetailDataTable.SqlDbTypeColumn.ColumnName).Text, SqlDbType)
          Case SqlDbType.BigInt, SqlDbType.Decimal, SqlDbType.Float, SqlDbType.Int, SqlDbType.Money, SqlDbType.SmallInt, SqlDbType.SmallMoney _
          , SqlDbType.TinyInt

            With Me.SearchComparisonTypeComboBox
              .DataSource = _SearchComparisonNumeric
              .DisplayMember = _SearchComparisonNumeric.ComparisonCaptionColumn.ColumnName
              .ValueMember = _SearchComparisonNumeric.ComparisonValueColumn.ColumnName
              .Rows.Band.Columns(_SearchComparisonNumeric.ComparisonValueColumn.ColumnName).Hidden = True
            End With
            _IsString = False

          Case SqlDbType.Char, SqlDbType.NChar, SqlDbType.NText, SqlDbType.NVarChar, SqlDbType.Text, SqlDbType.VarChar

            With Me.SearchComparisonTypeComboBox
              .DataSource = _SearchComparisonString
              .DisplayMember = _SearchComparisonString.ComparisonCaptionColumn.ColumnName
              .ValueMember = _SearchComparisonString.ComparisonValueColumn.ColumnName
              .Rows.Band.Columns(_SearchComparisonString.ComparisonValueColumn.ColumnName).Hidden = True
            End With
            _IsString = True

        End Select

        Me.SearchComparisonTypeComboBox_Resize(Me.SearchComparisonTypeComboBox, Nothing)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SearchItemComboBox_RowSelected event method of SearchForm.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 5-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' OkButton click event method.
  ''' </summary>
  Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
    Try
      Dim _Query As String
      Dim _CompanyDataTable As New QuickDAL.QuickCommonDataSet.CompanyDataTable
      Dim _DocumentTypeDataTable As New QuickDAL.QuickCommonDataSet.DocumentTypeDataTable

      _Query = _SearchOptionDataTable(0).SearchOption_Query & " WHERE "

      For I As Int32 = 0 To _SearchOptionDetailDataTable.Rows.Count - 1
        If _SearchOptionDetailDataTable(I).IsHidden Then
          If _SearchOptionDetailDataTable(I).ColumnName.IndexOf(_CompanyDataTable.Co_IdColumn.ColumnName) >= 0 Then
            _Query &= _SearchOptionDetailDataTable(I).ColumnName & "=" & Me.LoginInfoObject.CompanyID.ToString & " AND "

          ElseIf _SearchOptionDetailDataTable(I).ColumnName.IndexOf(_DocumentTypeDataTable.DocumentType_IDColumn.ColumnName) >= 0 Then
            _Query &= _SearchOptionDetailDataTable(I).ColumnName & "=" & Convert.ToString(Me.DocumentType) & " AND "

          End If
        End If
      Next

      If _IsString Then
        _Query &= Me.SearchItemComboBox.ActiveRow.Cells(_SearchOptionDetailDataTable.ColumnNameColumn.ColumnName).Text & "='" & SearchValueTextBox.Text & "'"
      Else
        _Query &= Me.SearchItemComboBox.ActiveRow.Cells(_SearchOptionDetailDataTable.ColumnNameColumn.ColumnName).Text & "=" & SearchValueTextBox.Text
      End If

      _SearchResultUnTypedDataTable = QuickDAL.QuickCommonDataSet.CompanyDataTable.GetUnTypedDataTableByQuery(_Query)
      Me.Hide()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in OkButton_Click of SearchForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub
#End Region

End Class