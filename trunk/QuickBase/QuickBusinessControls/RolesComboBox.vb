Imports QuickDAL.QuickSecurityDataSet
Imports QuickDAL.QuickSecurityDataSetTableAdapters

Public Class RolesComboBox
  Private _SecurityRoleTA As New SecurityRoleTableAdapter
  Private _SecurityRoleDataTable As New SecurityRoleDataTable

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
  End Sub


  Public Sub LoadRoles(ByVal _CompanyIDpara As Int16)
    Try
      _SecurityRoleDataTable = _SecurityRoleTA.GetByCoID(_CompanyIDpara)

      General.SetColumnCaptions(DirectCast(_SecurityRoleDataTable, DataTable), Me.Name)
      'Reverse loop, to handle the index change due to deletion of row.
      For I As Int32 = _SecurityRoleDataTable.Columns.Count - 1 To 0 Step -1
        Select Case _SecurityRoleDataTable.Columns(I).ColumnName
          Case _SecurityRoleDataTable.Role_DescColumn.ColumnName, _SecurityRoleDataTable.Role_IDColumn.ColumnName _
            , _SecurityRoleDataTable.Co_IDColumn.ColumnName
            'Do nothing, these columns will not be removed.
          Case Else
            _SecurityRoleDataTable.Columns.RemoveAt(I)
        End Select
      Next

      Me.DataSource = _SecurityRoleDataTable
      Me.DisplayMember = _SecurityRoleDataTable.Role_DescColumn.ColumnName
      Me.ValueMember = _SecurityRoleDataTable.Role_IDColumn.ColumnName
      Me.DropDownWidth = Me.Width
      Me.Rows.Band.Columns(_SecurityRoleDataTable.Role_DescColumn.ColumnName).Width = Me.DropDownWidth - QuickLibrary.Constants.SCROLLBAR_WIDTH
      Me.Rows.Band.Columns(_SecurityRoleDataTable.Co_IDColumn.ColumnName).Hidden = True
      Me.Rows.Band.Columns(_SecurityRoleDataTable.Role_IDColumn.ColumnName).Hidden = True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception in LoadRoles(int16) method of RolesComboBox", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Public Property RoleID() As Int32
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.SelectedRow.Cells(_SecurityRoleDataTable.Role_IDColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in Get method of RoleID property of RolesComboBox", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int32)
      Try
        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_SecurityRoleDataTable.Role_IDColumn.ColumnName).Text = value.ToString Then
            Me.SelectedRow = Me.Rows(I)
            Exit For
          End If
        Next

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in Set method of RoleID property of RolesComboBox", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

End Class
