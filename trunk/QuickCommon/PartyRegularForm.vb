Imports QuickDAL
Imports QuickDALLibrary
Imports QuickLibrary
Imports System.Windows.Forms
'Imports QuickDAL.QuickCommonDataSet
'Imports QuickDAL.QuickCommonDataSetTableAdapters
'Imports QuickDAL.QuickAccountingDataSet
'Imports QuickDAL.QuickAccountingDataSetTableAdapters

Public Class PartyRegularForm
  'Private _PartyTableAdapter As New PartyTableAdapter
  'Private _PartyDataTable As New PartyDataTable


  Private _PartyTableAdapter As New QuickCommonDataSetTableAdapters.PartyTableAdapter
  Private _PartyDataTable As New QuickCommonDataSet.PartyDataTable
  Private partyRow As QuickDAL.QuickCommonDataSet.PartyRow
  Private _CurrentRecordDeleted As Boolean = False
  'Private _Flag As Boolean = False

#Region "Toolbar Methods"
  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor
      If Me.PartyIDTextBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
        Exit Sub
      End If

      If Me.CurrentRecordDataRow IsNot Nothing Then

        If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

          Me._CurrentRecordDeleted = True
         
          Me.SaveRecord()




          'With DirectCast(Me.CurrentRecordDataRow, QuickCommonDataSet.PartyRow)  ' Change
          '  .Delete()
          '  _PartyTableAdapter.Update(Me.CurrentRecordDataRow)
          'End With

          'SaveRecord()
          'Below line is necessary so that parent form don't ask for record change confirmation.
          Me.CurrentRecordDataRow = Nothing
          MyBase.DeleteButtonClick(sender, e)
          QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)
        Else
        End If
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "No record to delete", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Information)
      End If
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to delete button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Me._PartyDataTable = Me._PartyTableAdapter.GetFirstByCoID(LoginInfoObject.CompanyID)
      If Me._PartyDataTable.Rows.Count > 0 Then
        Me.CurrentRecordDataRow = Me._PartyDataTable.Rows(0)
        MyBase.MoveFirstButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveNextButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.CurrentRecordDataRow Is Nothing Then
        Me._PartyDataTable = Me._PartyTableAdapter.GetNextByCoIDPartyID(LoginInfoObject.CompanyID, 0)
      Else
        Me._PartyDataTable = Me._PartyTableAdapter.GetNextByCoIDPartyID(LoginInfoObject.CompanyID, DirectCast(Me.CurrentRecordDataRow, QuickCommonDataSet.PartyRow).Party_ID) ' change
      End If
      If Me._PartyDataTable.Rows.Count > 0 Then
        Me.CurrentRecordDataRow = Me._PartyDataTable.Rows(0)
        MyBase.MoveFirstButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Me._PartyDataTable = Me._PartyTableAdapter.GetLastByCoID(LoginInfoObject.CompanyID)
      If Me._PartyDataTable.Rows.Count > 0 Then
        Me.CurrentRecordDataRow = Me._PartyDataTable.Rows(0)
        MyBase.MoveFirstButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If Me.CurrentRecordDataRow Is Nothing Then
        Me._PartyDataTable = Me._PartyTableAdapter.GetPreviousByCoIDPartyID(LoginInfoObject.CompanyID, 0)
      Else
        Me._PartyDataTable = Me._PartyTableAdapter.GetPreviousByCoIDPartyID(LoginInfoObject.CompanyID, DirectCast(Me.CurrentRecordDataRow, QuickCommonDataSet.PartyRow).Party_ID) ' Change
      End If
      If Me._PartyDataTable.Rows.Count > 0 Then
        Me.CurrentRecordDataRow = Me._PartyDataTable.Rows(0)
        MyBase.MoveFirstButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Windows.Forms.Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveSuccessfulMessage)
        Me.PartyCodeTextBox.Focus()
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, QuickMessageBox.PredefinedMessages.SaveUnSuccessfulMessage)
      End If
      ' MyBase.SaveButtonClick(sender, e)
    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in Save Button Click event.", ex)
      _QuickException.Show(Me.LoginInfoObject)
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
  End Sub
  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Me.CurrentRecordDataRow = Nothing
    Me._PartyDataTable.Rows.Clear()
    MyBase.CancelButtonClick(sender, e)
    Me.InactiveFromCalendarCombo.Value = Nothing
    Me.InactiveToCalendarCombo.Value = Nothing
    Me.CommissionTextBox.Text = CStr(0)
    Me.OpeningCreditTextBox.Text = CStr(0)
    Me.OpeningDebitTextBox.Text = CStr(0)
    Me.PartyCodeTextBox.Focus()
  End Sub

#End Region

#Region "Methods"

  Protected Overrides Function SaveRecord() As Boolean
    Try
      Dim _PartyRow As QuickCommonDataSet.PartyRow = Nothing  'change

      Dim _PartyID As System.Nullable(Of Int32)
      Dim _COAID As Int32
      If Me.IsValid() Then
        If Me.CurrentRecordDataRow Is Nothing Then
          _PartyRow = _PartyDataTable.NewPartyRow

          With _PartyRow
            .Co_ID = Me.LoginInfoObject.CompanyID
            _PartyID = _PartyTableAdapter.GetNewPartyIDByCoID(Me.LoginInfoObject.CompanyID)
            If _PartyID.HasValue Then
              .Party_ID = _PartyID.Value
            Else
              .Party_ID = 0
            End If
          End With
          _PartyRow.RecordStatus_ID = Constants.RecordStatuses.Inserted
        Else
          _PartyRow = DirectCast(Me.CurrentRecordDataRow, QuickCommonDataSet.PartyRow)
          If Me._CurrentRecordDeleted = True Then
            _PartyRow.RecordStatus_ID = Constants.RecordStatuses.Deleted
          Else
            _PartyRow.RecordStatus_ID = Constants.RecordStatuses.Updated
            ' Change
          End If
        End If

        'Common properties, which needs to be updated in both insert and update.
        With _PartyRow
          .Party_Code = Me.PartyCodeTextBox.Text
          If CoaComboBox.SelectedRow Is Nothing OrElse Not Int32.TryParse(CoaComboBox.SelectedRow.Cells(_PartyDataTable.COA_IDColumn.ColumnName).Text, _COAID) Then
            '  .COA_ID = 0
          Else
            .COA_ID = _COAID
          End If
          .Party_Desc = PartyNameTextBox.Text
          .Opening_Dr = 0

          Decimal.TryParse(OpeningDebitTextBox.Text, .Opening_Dr)
          .Opening_Cr = 0

          Decimal.TryParse(OpeningCreditTextBox.Text, .Opening_Cr)
          .Commission = 0
          .Commission = CDec(Format(CDec(CommissionTextBox.Text), "##.00"))

          .EntityType_ID = 0
          If EntityTypeComboBox.SelectedRow Is Nothing OrElse Not Int32.TryParse(EntityTypeComboBox.SelectedRow.Cells(_PartyDataTable.EntityType_IDColumn.ColumnName).Text, .EntityType_ID) Then
            .EntityType_ID = 0
          End If
          .Address = AddressTextBox.Text
          .ZipCode = ZipCodeTextBox.Text
          .City = CityTextBox.Text
          .State = StateTextBox.Text
          .Country = CountryTextBox.Text
          .Email = EmailTextBox.Text
          .Phone = PhoneTextBox.Text
          .Fax = FaxTextBox.Text
          .URL = UrlTextBox.Text
          .Stamp_DateTime = Now
          .Stamp_UserID = Me.LoginInfoObject.UserID
          If Me.InactiveFromCalendarCombo.Value Is DBNull.Value OrElse Me.InactiveFromCalendarCombo.Value Is Nothing Then
            .SetInactive_FromNull()
          Else
            .Inactive_From = CDate(Me.InactiveFromCalendarCombo.Value)
          End If
          If Me.InactiveToCalendarCombo.Value Is DBNull.Value OrElse Me.InactiveToCalendarCombo.Value Is Nothing Then
            .SetInactive_ToNull()
          Else
            .Inactive_To = CDate(Me.InactiveToCalendarCombo.Value)
          End If
        End With

        'Dim _PartyRow1 As QuickDAL.QuickCommonDataSet.PartyRow

        'If Not _PartyRow1.CheckBusniessRules Then
        '  QuickMessageBox.Show(Me.LoginInfoObject, partyRow.BrokenBusinessRule)
        '  Return False
        'Else
        If _PartyRow.RowState = DataRowState.Detached Then _PartyDataTable.Rows.Add(_PartyRow)
        _PartyTableAdapter.Update(_PartyDataTable)
        Me.PartyIDTextBox.Text = _PartyRow.Party_ID.ToString
        Me.CurrentRecordDataRow = _PartyRow

        Return True
      End If
      ' End If



      '  If Not _PartyRow.CheckBusniessRules Then
      '    QuickMessageBox.Show(Me.LoginInfoObject, _PartyRow.BrokenBusinessRule)
      '    Return False
      '  Else
      '    If _PartyRow.RowState = DataRowState.Detached Then _PartyDataTable.Rows.Add(_PartyRow)
      '    _PartyTableAdapter.Update(_PartyDataTable)
      '    Me.PartyIDTextBox.Text = _PartyRow.Party_ID.ToString
      '    Me.CurrentRecordDataRow = _PartyRow
      '    Return True
      '  End If
      'End If
      ' 
      Me._CurrentRecordDeleted = False

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in SaveRecord method", ex)
    End Try
  End Function
  Private Function IsValid() As Boolean
    Try
      Dim _TempTable As QuickCommonDataSet.PartyDataTable
      _TempTable = Me._PartyTableAdapter.GetByCoIdAndPartyCode(Me.LoginInfoObject.CompanyID, Me.PartyCodeTextBox.Text)

      If Me.PartyIDTextBox.Text = "" Then
        If _TempTable.Rows.Count > 0 Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Party code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.PartyCodeTextBox.Focus()
          Return False
        End If
      ElseIf _TempTable.Rows.Count = 1 Then
        If _TempTable.Rows(0).Item(_TempTable.Party_IDColumn.ColumnName).ToString = CStr(Me.PartyIDTextBox.Text) And _TempTable.Rows(0).Item(_TempTable.Party_CodeColumn.ColumnName).ToString = Me.PartyCodeTextBox.Text Then
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "Duplicate Party code Entered.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
          Me.PartyCodeTextBox.Focus()
          Return False
        End If
      End If

      If Me.PartyCodeTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the party code to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.PartyCodeTextBox.Focus()
        Return False

      ElseIf Me.CoaComboBox.SelectedRow Is Nothing Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select chart of account to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.CoaComboBox.Focus()
        Return False
      ElseIf Me.EntityTypeComboBox.Text = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should select the type of entity to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.EntityTypeComboBox.Focus()
        Return False
      ElseIf Me.PartyNameTextBox.Text.Trim = String.Empty Then
        QuickMessageBox.Show(Me.LoginInfoObject, "You should enter the name of party to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Me.PartyNameTextBox.Focus()
        Return False
      ElseIf CInt(Me.CommissionTextBox.Text) >= 100 Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Commission is not more than 100% to save the record.", MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.ShortMessage, MessageBoxIcon.Warning)
        Return False

     
      End If
      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to IsValid function", ex)
      QuickExceptionObject.Show(LoginInfoObject)
    Finally
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      Cursor = Cursors.WaitCursor
      If Me.CurrentRecordDataRow IsNot Nothing Then
        Dim _PartyRow As QuickCommonDataSet.PartyRow = DirectCast(Me.CurrentRecordDataRow, QuickCommonDataSet.PartyRow) ' Change

        With _PartyRow
          PartyIDTextBox.Text = .Party_ID.ToString
          PartyCodeTextBox.Text = .Party_Code
          If Not .IsCOA_IDNull Then CoaComboBox.Value = .COA_ID
          PartyNameTextBox.Text = .Party_Desc
          Me.OpeningCreditTextBox.Text = .Opening_Cr.ToString
          OpeningDebitTextBox.Text = .Opening_Dr.ToString
          CommissionTextBox.Text = .Commission.ToString
          EntityTypeComboBox.Value = .EntityType_ID
          AddressTextBox.Text = .Address
          ZipCodeTextBox.Text = .ZipCode
          CityTextBox.Text = .City
          StateTextBox.Text = .State
          If Not .IsCountryNull Then CountryTextBox.Text = .Country
          EmailTextBox.Text = .Email
          PhoneTextBox.Text = .Phone
          FaxTextBox.Text = .Fax
          UrlTextBox.Text = .URL

          If .IsInactive_FromNull Then
            Me.InactiveFromCalendarCombo.Value = Nothing
          Else
            Me.InactiveFromCalendarCombo.Value = .Inactive_From
          End If
          If .IsInactive_ToNull Then
            Me.InactiveToCalendarCombo.Value = Nothing
          Else
            Me.InactiveToCalendarCombo.Value = .Inactive_To
          End If
        End With
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move first", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Function
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
  End Sub

#Region "Events"
  Private Sub PartyRegularForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Cursor = Cursors.WaitCursor
      Dim _CoaTA As New QuickAccountingDataSetTableAdapters.COATableAdapter 

      Dim _CoaTable As QuickAccountingDataSet.COADataTable

      Me.EntityTypeComboBox.LoadEntityTypes()
      _CoaTable = _CoaTA.GetByCoID(Me.LoginInfoObject.CompanyID)
      Me.CoaComboBox.DataSource = _CoaTable
      Me.CoaComboBox.DisplayMember = _CoaTable.COA_DescColumn.ColumnName
      Me.CoaComboBox.ValueMember = _CoaTable.COA_IDColumn.ColumnName
      For I As Int32 = 0 To Me.CoaComboBox.Rows.Band.Columns.Count - 1
        Select Case Me.CoaComboBox.Rows.Band.Columns(I).Key
          Case _CoaTable.COA_CodeColumn.ColumnName, _CoaTable.COA_DescColumn.ColumnName
          Case Else
            Me.CoaComboBox.Rows.Band.Columns(I).Hidden = True
        End Select
      Next

      Me.AddressTextBox.Text = Nothing
      Me.CityTextBox.Text = Nothing
      Me.CountryTextBox.Text = Nothing
      Me.EmailTextBox.Text = Nothing
      Me.FaxTextBox.Text = Nothing
      Me.PartyNameTextBox.Text = Nothing
      Me.PhoneTextBox.Text = Nothing
      Me.StateTextBox.Text = Nothing
      Me.UrlTextBox.Text = Nothing
      Me.ZipCodeTextBox.Text = Nothing
      Me.PartyIDTextBox.Text = Nothing
      Me.InactiveFromCalendarCombo.Value = Nothing
      Me.InactiveToCalendarCombo.Value = Nothing
      Me.PartyCodeTextBox.Text = Nothing

      Me.AddressTextBox.MaxLength = Me._PartyDataTable.AddressColumn.MaxLength
      Me.CityTextBox.MaxLength = Me._PartyDataTable.CityColumn.MaxLength
      Me.CountryTextBox.MaxLength = Me._PartyDataTable.CountryColumn.MaxLength
      Me.EmailTextBox.MaxLength = Me._PartyDataTable.EmailColumn.MaxLength
      Me.FaxTextBox.MaxLength = Me._PartyDataTable.FaxColumn.MaxLength
      Me.PartyCodeTextBox.MaxLength = Me._PartyDataTable.Party_CodeColumn.MaxLength
      Me.PartyNameTextBox.MaxLength = Me._PartyDataTable.Party_DescColumn.MaxLength
      Me.PhoneTextBox.MaxLength = Me._PartyDataTable.PhoneColumn.MaxLength
      Me.StateTextBox.MaxLength = Me._PartyDataTable.StateColumn.MaxLength
      Me.UrlTextBox.MaxLength = Me._PartyDataTable.URLColumn.MaxLength
      Me.ZipCodeTextBox.MaxLength = Me._PartyDataTable.ZipCodeColumn.MaxLength
      Me.PartyCodeTextBox.Focus()

     


    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in load event of " & Me.Name, ex)
      _QuickException.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Private Sub OpeningDebitTextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpeningDebitTextBox.Enter
    Me.OpeningDebitTextBox.Focus()

  End Sub

  Private Sub OpeningDebitTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles OpeningDebitTextBox.KeyPress
    If Not Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
      e.Handled = True
    End If
  End Sub

  Private Sub OpeningCreditTextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpeningCreditTextBox.Enter
    Me.OpeningCreditTextBox.Focus()
  End Sub

  Private Sub OpeningCreditTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles OpeningCreditTextBox.KeyPress
    If Not Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
      e.Handled = True
    End If
  End Sub

  Private Sub CommissionTextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CommissionTextBox.Enter
    Me.CommissionTextBox.Focus()
  End Sub

  Private Sub CommissionTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CommissionTextBox.KeyPress
    If Not Char.IsDigit(e.KeyChar) And Not Asc(e.KeyChar) = 8 And Not Asc(e.KeyChar) = 46 Then
      e.Handled = True
    End If
  End Sub
  Private Sub OpeningDebitTextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpeningDebitTextBox.Leave
    If CInt(Me.OpeningDebitTextBox.Text) > 0 Then
      Me.OpeningCreditTextBox.Text = CStr(0)
    End If
  End Sub
  Private Sub OpeningCreditTextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpeningCreditTextBox.Leave
    If CInt(Me.OpeningCreditTextBox.Text) > 0 Then
      Me.OpeningDebitTextBox.Text = CStr(0)
    End If
  End Sub
  
#End Region

  'Private Sub CoaComboBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CoaComboBox.Enter
  '  Me._Flag = True
  '  If Me._Flag = True Then
  '    SendKeys.Send("{f4}")
  '  End If
  'End Sub



  'Private Sub CoaComboBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CoaComboBox.Leave
  '  Me._Flag = False
  'End Sub
End Class