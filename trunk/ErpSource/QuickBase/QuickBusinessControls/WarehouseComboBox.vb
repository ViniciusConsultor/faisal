Imports QuickLibrary.Constants

Public Class WarehouseComboBox
  Private _WarehouseTA As New WarehouseTableAdapter
  Private _WarehouseDataTable As New QuickInventoryDataSet.WarehouseDataTable
  Private _EntityType As EntityTypes

  Public Sub LoadWarehouses(ByVal _CompanyIDpara As Int16)
    Try
      _WarehouseDataTable = _WarehouseTA.GetByCoID(_CompanyIDpara)
      General.SetColumnCaptions(DirectCast(_WarehouseDataTable, DataTable), Me.Name)
      'Reverse loop, to handle the index change due to deletion of row.
      For I As Int32 = _WarehouseDataTable.Columns.Count - 1 To 0 Step -1
        Select Case _WarehouseDataTable.Columns(I).ColumnName
          Case _WarehouseDataTable.Warehouse_NameColumn.ColumnName, _WarehouseDataTable.Warehouse_IDColumn.ColumnName _
            , _WarehouseDataTable.Co_IDColumn.ColumnName
          Case Else
            _WarehouseDataTable.Columns.RemoveAt(I)
        End Select
      Next

      Me.DataSource = _WarehouseDataTable
      Me.DisplayMember = _WarehouseDataTable.Warehouse_NameColumn.ColumnName
      Me.DropDownWidth = Me.Width
      Me.Rows.Band.Columns(_WarehouseDataTable.Co_IDColumn.ColumnName).Hidden = True
      Me.Rows.Band.Columns(_WarehouseDataTable.Warehouse_IDColumn.ColumnName).Hidden = True

      WarehouseComboBox_Resize(Nothing, Nothing)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception in LoadWarehouses(Int32)", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Public Property WarehouseID() As Int32
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.SelectedRow.Cells(_WarehouseDataTable.Warehouse_IDColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in WarehouseID get peroperty", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int32)
      Try
        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_WarehouseDataTable.Warehouse_IDColumn.ColumnName).Text = value.ToString Then
            Me.SelectedRow = Me.Rows(I)
            Exit For
          End If
        Next

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in WarehouseID set peroperty", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property EntityType() As EntityTypes
    Get
      Return _EntityType
    End Get
    Set(ByVal value As EntityTypes)
      _EntityType = value
    End Set
  End Property

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
  End Sub

  Private Sub WarehouseComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    Try
      Me.DropDownWidth = Me.Width
      'Me.Rows.Band.Columns(_PartyDataTable.Party_IDColumn.ColumnName).Width = Me.DropDownWidth * 30 / 100
      Me.Rows.Band.Columns(_WarehouseDataTable.Warehouse_NameColumn.ColumnName).Width = Me.DropDownWidth * 100 / 100 - SCROLLBAR_WIDTH
    Catch ex As Exception
      'Ignore all errors.
    End Try
  End Sub
End Class
