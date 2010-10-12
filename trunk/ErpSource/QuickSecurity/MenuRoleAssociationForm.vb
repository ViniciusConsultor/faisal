Imports QuickLibrary

Public Class MenuRoleAssociationForm
  Dim _MenuRoleAssociationTable As New QuickDAL.QuickSecurityDataSet.MenuRoleAssociationDataTable
  Dim _MenuRoleAssociationTA As New QuickDAL.QuickSecurityDataSetTableAdapters.MenuRoleAssociationTableAdapter
  Dim _MenuWithSecurityTA As New QuickDAL.QuickSecurityDataSetTableAdapters.MenuWithSecurityTableAdapter
  Dim _MenuWithSecurityTable As QuickDAL.QuickSecurityDataSet.MenuWithSecurityDataTable

#Region "Event Methods"

  Private Sub MenuRoleAssociation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Me.CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)
      Me.Quick_Spread1_Sheet1.DataSource = _MenuWithSecurityTA.GetNoData

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Load event of MenuRoleAssociationForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub CompanyComboBox_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles CompanyComboBox.RowSelected
    Try
      Me.RolesComboBox1.LoadRoles(Me.CompanyComboBox.CompanyID)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in RowSelected event of CompanyComboBox of MenuRoleAssociationForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  Private Sub RolesComboBox1_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles RolesComboBox1.RowSelected
    Try
      If Me.RolesComboBox1.SelectedRow IsNot Nothing Then
        _MenuWithSecurityTable = _MenuWithSecurityTA.GetByCoIDRoleID(Me.LoginInfoObject.CompanyID, Me.RolesComboBox1.RoleID)
        Me.Quick_Spread1_Sheet1.DataSource = _MenuWithSecurityTable
      Else
        _MenuWithSecurityTable = _MenuWithSecurityTA.GetNoData
        Me.Quick_Spread1_Sheet1.DataSource = Nothing
      End If
      SetGridLayout()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in RowSelect event of RolesComboBox of MenuRoleAssociationForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try

  End Sub

#End Region

#Region "Methods"

  Private Sub SetGridLayout()
    Try
      Dim _checkboxColumn As New FarPoint.Win.Spread.CellType.CheckBoxCellType

      Me.Quick_Spread1.ShowDeleteRowButton(Me.Quick_Spread1_Sheet1) = False
      For I As Int32 = 0 To Me.Quick_Spread1_Sheet1.Columns.Count - 1
        With Me.Quick_Spread1_Sheet1.Columns(I)
          Select Case I
            Case _MenuWithSecurityTable.AllowedColumn.Ordinal
              .Visible = True
              .Locked = False
              .Width = 50
              .CellType = _checkboxColumn
            Case _MenuWithSecurityTable.Menu_DescColumn.Ordinal
              .Visible = True
              .Locked = True
              .Width = 300
            Case Else
              .Visible = False
          End Select
        End With
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetGridLayout of MenuRoleAssociationForm.", ex)
      Throw _qex
    End Try

  End Sub

#End Region

#Region "Toolbar methods"
  Protected Overrides Function SaveRecord() As Boolean
    Try
      'Below line will commit the pending changes to spread and datatable.
      Me.Quick_Spread1.EditMode = False

      For I As Int32 = 0 To _MenuWithSecurityTable.Rows.Count - 1
        If _MenuWithSecurityTable(I).RowState <> DataRowState.Unchanged Then
          With _MenuWithSecurityTable(I)
            If .Allowed Then
              'Try if record exists
              If _MenuRoleAssociationTA.UpdateStatus(Constants.RecordStatuses.Inserted, CompanyComboBox.CompanyID, .Menu_Id, Me.RolesComboBox1.RoleID) = 0 Then
                'If 0 record is updated then record does not exist, insert it.
                _MenuRoleAssociationTA.Insert(Me.CompanyComboBox.CompanyID, .Menu_Id, Me.RolesComboBox1.RoleID, Now, Me.LoginInfoObject.UserID, Nothing, Constants.RecordStatuses.Inserted)
              End If
            Else
              'Delete record
              _MenuRoleAssociationTA.UpdateStatus(Constants.RecordStatuses.Deleted, Me.CompanyComboBox.CompanyID, .Menu_Id, Me.RolesComboBox1.RoleID)
            End If
          End With
        End If
      Next

      Return True

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveButton click event method.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try

  End Function
#End Region

End Class