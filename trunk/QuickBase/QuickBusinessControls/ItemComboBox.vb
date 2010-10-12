Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDALLibrary
Imports QuickDAL.QuickInventoryDataSet

Public Class ItemComboBox
  Private _ItemTA As New ItemTableAdapter
  Private _ItemDataTable As New ItemDataTable
  'Private _EntityType As Constants.EntityTypes

  Public Sub LoadItems(ByVal _CompanyIDpara As Int16)
    Try
      _ItemDataTable = _ItemTA.GetAll

      QuickDALLibrary.General.SetColumnCaptions(DirectCast(_ItemDataTable, DataTable), Me.FindForm.Name)
      'Reverse loop, to handle the index change due to deletion of row.
      For I As Int32 = _ItemDataTable.Columns.Count - 1 To 0 Step -1
        Select Case _ItemDataTable.Columns(I).ColumnName
          Case _ItemDataTable.Item_DescColumn.ColumnName, _ItemDataTable.Item_IDColumn.ColumnName _
            , _ItemDataTable.Co_IDColumn.ColumnName, _ItemDataTable.Item_CodeColumn.ColumnName
          Case Else
            _ItemDataTable.Columns.RemoveAt(I)
        End Select
      Next

      Me.DataSource = _ItemDataTable
      Me.DisplayMember = _ItemDataTable.Item_CodeColumn.ColumnName
      Me.DropDownWidth = Me.Width
      Me.Rows.Band.Columns(_ItemDataTable.Item_CodeColumn.ColumnName).Width = Me.Width * 30 / 100
      Me.Rows.Band.Columns(_ItemDataTable.Item_DescColumn.ColumnName).Width = Me.Width * 70 / 100 - 10  'Leave space for scroll bar.
      Me.Rows.Band.Columns(_ItemDataTable.Co_IDColumn.ColumnName).Hidden = True
      Me.Rows.Band.Columns(_ItemDataTable.Item_IDColumn.ColumnName).Hidden = True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception in populating sales man", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Public Property ItemID() As Int32
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.SelectedRow.Cells(_ItemDataTable.Item_IDColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in item id from selected row of item combo box", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int32)
      Try
        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_ItemDataTable.Item_IDColumn.ColumnName).Text = value.ToString Then
            Me.SelectedRow = Me.Rows(I)
            Exit For
          End If
        Next
      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in setting item id - ItemComboBox", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property ItemCode() As String
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return String.Empty
        Else
          Return Me.SelectedRow.Cells(_ItemDataTable.Item_CodeColumn.ColumnName).Text
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in get method of ItemCode property of ItemComboBox.", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As String)
      Try
        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_ItemDataTable.Item_CodeColumn.ColumnName).Text = value Then
            Me.SelectedRow = Me.Rows(I)
            Exit For
          End If
        Next

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in set method of ItemCode property of ItemComboBox.", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    'Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
  End Sub
End Class
