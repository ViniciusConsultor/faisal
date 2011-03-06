Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDAL.QuickCommonDataSet

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 02-Mar-11
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This form is used to assign control level permissions for user role
''' </summary>
Public Class FormControlPermissionForm

#Region "Declarations"
  Private _SettingFormCompanyAssociationTA As New QuickDAL.QuickCommonDataSetTableAdapters.SettingFormCompanyAssociationTableAdapter
  Private _FormControlPermissionTA As New QuickDAL.QuickSecurityDataSetTableAdapters.FormControlPermissionTableAdapter

  Private _SettingFormCompanyAssociationTable As New QuickDAL.QuickCommonDataSet.SettingFormCompanyAssociationDataTable
  Private _FormControlPermissionTable As New QuickDAL.QuickSecurityDataSet.FormControlPermissionDataTable
#End Region

#Region "Properties"

#End Region

#Region "Methods"

#End Region

#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 07-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will save records in database.
  ''' </summary>
  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim _FormControlPermissionToSaveTable As QuickDAL.QuickSecurityDataSet.FormControlPermissionDataTable
      Dim _FormControlPermissionToSaveRow As QuickDAL.QuickSecurityDataSet.FormControlPermissionRow
      Dim _RecordExists As Boolean = False
      Dim _RecordRow As Int32

      Me.FormControlPermissionSpreadSheet.SetActiveCell(0, 0)
      _FormControlPermissionToSaveTable = _FormControlPermissionTA.GetByCoIDRoleIDFormID(Me.CompanyComboBox.CompanyID, Me.RolesComboBox.RoleID, Convert.ToInt32(Me.FormNameUltraComboBox.SelectedRow.Cells(Me._SettingFormCompanyAssociationTable.Form_IDColumn.ColumnName).Text))

      For I As Int32 = 0 To _FormControlPermissionTable.Rows.Count - 1
        If _FormControlPermissionTable(I).RowState <> DataRowState.Unchanged Then
          _RecordExists = False
          For _RecordRow = 0 To _FormControlPermissionToSaveTable.Rows.Count - 1
            If Me._FormControlPermissionTable(I).ControlID = _FormControlPermissionToSaveTable(_RecordRow).ControlID Then
              _RecordExists = True
              Exit For
            End If
          Next _RecordRow

          If _RecordExists Then
            With _FormControlPermissionToSaveTable(_RecordRow)
              .IsEnabled = _FormControlPermissionTable(I).IsEnabled
              .SetUploadedNull()
            End With
          Else
            _FormControlPermissionToSaveRow = _FormControlPermissionToSaveTable.NewFormControlPermissionRow
            With _FormControlPermissionToSaveRow
              .Co_ID = _FormControlPermissionTable(I).Co_ID
              .ControlID = _FormControlPermissionTable(I).ControlID
              .FormID = _FormControlPermissionTable(I).FormID
              .IsEnabled = _FormControlPermissionTable(I).IsEnabled
              .IsVisible = _FormControlPermissionTable(I).IsVisible
              .RecordStatus_ID = QuickLibrary.Constants.RecordStatuses.Inserted
              .RoleID = _FormControlPermissionTable(I).RoleID
              .Stamp_DateTime = Date.UtcNow
              .Stamp_UserID = Me.LoginInfoObject.UserID
              .SetUploadedNull()
            End With
            _FormControlPermissionToSaveTable.Rows.Add(_FormControlPermissionToSaveRow)
          End If
        End If
      Next

      _FormControlPermissionTA.Update(_FormControlPermissionToSaveTable)

      Return True

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SaveButtonClick of FormConrolPermissionForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Function
#End Region

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 02-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event will initialize form controls
  ''' </summary>
  Private Sub FormControlPermissionForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Me.CompanyComboBox.LoadThisAndChildCompanies(Me.LoginInfoObject.CompanyID)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in FormControlPermissionForm_Load event method of FormControlPermissionForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 02-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will load the controls form the selected form.
  ''' </summary>
  Private Sub FormNameUltraComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormNameUltraComboBox.ValueChanged
    Try
      If Me.FormNameUltraComboBox.SelectedRow IsNot Nothing Then
        _FormControlPermissionTable = _FormControlPermissionTA.GetAllControlsPermissionsByCoIDFormID(Me.CompanyComboBox.CompanyID, Me.RolesComboBox.RoleID, Convert.ToInt32(Me.FormNameUltraComboBox.SelectedRow.Cells(Me._SettingFormCompanyAssociationTable.Form_IDColumn.ColumnName).Text))

        Me.FormControlPermissionSpreadSheet.DataSource = _FormControlPermissionTable

        For I As Int32 = 0 To Me.FormControlPermissionSpreadSheet.Columns.Count - 1
          With _FormControlPermissionTable
            Select Case I
              Case .Control_NameColumn.Ordinal
                Me.FormControlPermissionSpreadSheet.Columns(I).Locked = True
                Me.FormControlPermissionSpreadSheet.Columns(I).Width = 200
              Case .Control_CaptionColumn.Ordinal
                Me.FormControlPermissionSpreadSheet.Columns(I).Locked = True
                Me.FormControlPermissionSpreadSheet.Columns(I).Width = 200
              Case .IsEnabledColumn.Ordinal
                Me.FormControlPermissionSpreadSheet.Columns(I).Locked = False
                Me.FormControlPermissionSpreadSheet.Columns(I).Width = 60
              Case Else
                Me.FormControlPermissionSpreadSheet.Columns(I).Visible = False
            End Select
          End With
        Next
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in FormNameUltraComboBox_ValueChanged of FormControlPermissionForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 06-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event method will load roles for selected company.
  ''' </summary>
  Private Sub CompanyComboBox_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CompanyComboBox.ValueChanged

    Try
      Me.RolesComboBox.LoadRoles(Me.CompanyComboBox.CompanyID)
      _SettingFormCompanyAssociationTable = _SettingFormCompanyAssociationTA.GetAllByCoID(Me.LoginInfoObject.CompanyID)

      _SettingFormCompanyAssociationTable.PrimaryKey = Nothing
      With _SettingFormCompanyAssociationTable
        .Columns.Remove(.Co_IDColumn)
        .Columns.Remove(.Upload_DateTimeColumn)
        .Columns.Remove(.Stamp_DateTimeColumn)
        .Columns.Remove(.Stamp_UserIDColumn)
        .Columns.Remove(.RecordStatus_IDColumn)
      End With
      With Me.FormNameUltraComboBox
        .DataSource = _SettingFormCompanyAssociationTable
        .DisplayMember = _SettingFormCompanyAssociationTable.Form_NameColumn.ColumnName
        .ValueMember = _SettingFormCompanyAssociationTable.Form_IDColumn.ColumnName
        .Rows.Band.Columns(_SettingFormCompanyAssociationTable.Form_IDColumn.ColumnName).Hidden = True
        .DropDownWidth = .Width - QuickLibrary.Constants.SCROLLBAR_WIDTH
        .Rows.Band.Columns(_SettingFormCompanyAssociationTable.Form_CodeColumn.ColumnName).Width = Convert.ToInt32(.DropDownWidth * 0.15)
        .Rows.Band.Columns(_SettingFormCompanyAssociationTable.Form_NameColumn.ColumnName).Width = Convert.ToInt32(.DropDownWidth * 0.4)
        .Rows.Band.Columns(_SettingFormCompanyAssociationTable.Form_CaptionColumn.ColumnName).Width = Convert.ToInt32(.DropDownWidth * 0.4)
      End With


    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in CompanyComboBox_ValueChanged of FormControlPermissionForm.", ex)
      _qex.Show(Me.LoginInfoObject)
    End Try
  End Sub
End Class