Imports QuickDALLibrary
Imports QuickLibrary.Common
Imports QuickLibrary
Imports QuickDAL.QuickInventoryDataSet

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2010
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This controls shows item summary.
''' </summary>
Public Class ItemSummaryBar

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

#Region "Declarations"
  Dim _ItemSummaryTA As New ItemSummaryTableAdapter
  Dim _ItemSummaryDataTable As New ItemSummaryDataTable
  Public IncludeStockSummary As Boolean = True
  Public IncludePurchaseSummary As Boolean = False
  Public IncludeSalesSummary As Boolean = False

#End Region

#Region "Properties"

  Private _CompanyID As Int32
  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 9-July-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Company ID of current criteria being displayed.
  ''' </summary>
  Public ReadOnly Property CompanyID() As Int32
    Get
      Try

        Return _CompanyID

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in CompanyID of ClassName/FormName.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  Private _SourceFirstID As Int32
  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 9-July-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Source First ID of current criteria being displayed.
  ''' </summary>
  Public ReadOnly Property SourceFirstID() As Int32
    Get
      Try

        Return _SourceFirstID

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SourceFirstID of ItemSummaryBar.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  Private _SourceSecondID As Int32
  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 9-July-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Source Second ID of current criteria being displayed.
  ''' </summary>
  Public ReadOnly Property SourceSecondID() As Int32
    Get
      Try

        Return _SourceSecondID

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SourceSecondID of ItemSummaryBar.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  Private _WarehouseID As Int32
  'Author: Faisal Saleem 
  'Date Created(DD-MMM-YY): 9-July-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Warehouse ID of current criteria being displayed.
  ''' </summary>
  Public ReadOnly Property WarehouseID() As Int32
    Get
      Try

        Return _WarehouseID

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in WarehouseID of ItemSummaryBar.", ex)
        Throw _qex
      End Try
    End Get
  End Property

#End Region

#Region "Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It returns the form object which contains this control.
  ''' </summary>
  Private Function GetFormObject() As System.Windows.Forms.Form
    Try
      Dim _Control As Windows.Forms.Control = Me

      Do While _Control IsNot Nothing AndAlso Not TypeOf _Control Is Windows.Forms.Form
        _Control = _Control.GetContainerControl
      Loop

      Return _Control
    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in GetFormObject method.", ex)
      Throw _QuickException
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will fetch the records and display, if current criteria is already showing
  ''' then it will not fetch again.
  ''' </summary>
  Public Sub ShowSummary(ByVal _CoIDpara As Int32, ByVal _SourceFirstIDpara As Int32, ByVal _SourceSecondIDpara As Int32, ByVal _WarehouseIDpara As Int32, Optional ByVal Reloadpara As Boolean = False)
    Try
      Dim _ResultDataTable As ItemSummaryDataTable

      If Reloadpara OrElse _CoIDpara <> CompanyID OrElse _SourceFirstIDpara <> SourceFirstID OrElse _SourceSecondIDpara <> SourceSecondID OrElse _WarehouseIDpara <> WarehouseID Then

        _CompanyID = _CoIDpara
        _SourceFirstID = _SourceFirstIDpara
        _SourceSecondID = _SourceSecondIDpara
        _WarehouseID = _WarehouseIDpara

        'Remove any existing rows.
        _ItemSummaryDataTable.Clear()

        _ResultDataTable = _ItemSummaryTA.GetByPrimaryKey(_CoIDpara, _SourceFirstIDpara, _SourceSecondIDpara, Constants.enuDocumentType.Stock, _WarehouseIDpara)
        For Each _row As ItemSummaryRow In _ResultDataTable.Rows
          _ItemSummaryDataTable.ImportRow(_row)
        Next
        _ResultDataTable = _ItemSummaryTA.GetByPrimaryKey(_CoIDpara, _SourceFirstIDpara, _SourceSecondIDpara, Constants.enuDocumentType.Purchased, _WarehouseIDpara)
        For Each _row As ItemSummaryRow In _ResultDataTable.Rows
          _ItemSummaryDataTable.ImportRow(_row)
        Next
        _ResultDataTable = _ItemSummaryTA.GetByPrimaryKey(_CoIDpara, _SourceFirstIDpara, _SourceSecondIDpara, Constants.enuDocumentType.Sold, _WarehouseIDpara)
        For Each _row As ItemSummaryRow In _ResultDataTable.Rows
          _ItemSummaryDataTable.ImportRow(_row)
        Next
        SetLayout()
      End If

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in ShowStock method", ex)
      Throw _QuickException
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will set the display layout of the grid.
  ''' </summary>
  Private Sub SetLayout()
    Try
      Me.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
      Me.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
      'Me.Height = 44
      General.SetColumnCaptions(_ItemSummaryDataTable, GetFormObject.Name)
      Me.ActiveSheet.DataSource = _ItemSummaryDataTable
      For I As Int32 = 0 To Me.ActiveSheet.ColumnCount - 1
        Select Case I
          Case _ItemSummaryDataTable.ItemSummary_Size01Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size02Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size03Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size04Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size05Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size06Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size07Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size08Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size09Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size10Column.Ordinal _
                , _ItemSummaryDataTable.ItemSummary_Size11Column.Ordinal

            Me.ActiveSheet.Columns(I).Width = Constants.QTY_CELL_WIDTH
            Me.ActiveSheet.Columns(I).CellType = QtyCellType
          Case _ItemSummaryDataTable.Summary_TypeColumn.Ordinal

          Case Else
            Me.ActiveSheet.Columns(I).Visible = False
        End Select
      Next

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in ShowStock method", ex)
      Throw _QuickException
    End Try
  End Sub

#End Region

#Region "Event Methods"
  Private Sub StockBar_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout
    Try
      SetLayout()

    Catch ex As Exception
      'Dim _QuickException As New QuickDALLibrary.QuickExceptionAdvanced("Exception in StockBar_Layout event method.", ex)
      'Throw _QuickException
    End Try
  End Sub

#End Region

End Class
