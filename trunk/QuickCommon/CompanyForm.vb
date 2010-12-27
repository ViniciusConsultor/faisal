Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary

Public Class CompanyForm

#Region "Declaration"
  Private _CompanyTableAdapterObject As New CompanyTableAdapter
  Private _CommunicationAdapter As New CommunicationTableAdapter
  Private _AddressAdapter As New AddressTableAdapter

  Private WithEvents _CompanyDataTable As New CompanyDataTable
  Private WithEvents _CommunicationTable As New CommunicationDataTable
  Private WithEvents _AddressTable As New AddressDataTable

  Private _CurrentCompanyDataRow As CompanyRow
  Private _CommunicationDataRow As CommunicationRow
  Private _AddressDataRow As AddressRow

  Const CompanyCode As String = "Co_Id"
  Const CompanyDescription As String = "Co_Desc"
#End Region

#Region "Events"
  Private Sub CompanyForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      PopulateParentCompanyCombo()
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception on the Load form", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try

  End Sub
#End Region

#Region "Methods"

  Protected Overrides Function SaveRecord() As Boolean
    Try
      'Dim CompanyID As Int32
      Dim CompanyDataRow As CompanyRow = Nothing

      If _CurrentCompanyDataRow Is Nothing Then
        CompanyDataRow = _CompanyDataTable.NewCompanyRow
        CompanyDataRow.Co_Id = CShort(_CompanyTableAdapterObject.GetNewCoID())

        _CurrentCompanyDataRow = CompanyDataRow
      End If

      'Set common properties for insert and update.
      _CurrentCompanyDataRow.Co_Code = CompanyCodeTextBox.Text
      _CurrentCompanyDataRow.Co_Desc = CompanyDescTextBox.Text
      _CurrentCompanyDataRow.Parent_Co_ID = CShort(ParentCompanyComboBox.Value)
      If IsDBNull(CompanyInactiveFromCalendarCombo.Value) OrElse CompanyInactiveFromCalendarCombo.Value Is Nothing Then
        _CurrentCompanyDataRow.SetInactive_FromNull()
      Else
        _CurrentCompanyDataRow.Inactive_From = Convert.ToDateTime(CompanyInactiveFromCalendarCombo.Value)
      End If
      If IsDBNull(CompanyInactiveToCalendarCombo.Value) OrElse CompanyInactiveToCalendarCombo.Value Is Nothing Then
        _CurrentCompanyDataRow.SetInactive_ToNull()
      Else
        _CurrentCompanyDataRow.Inactive_To = Convert.ToDateTime(CompanyInactiveToCalendarCombo.Value)
      End If
      _CurrentCompanyDataRow.Stamp_DateTime = Common.SystemDateTime
      _CurrentCompanyDataRow.Stamp_UserID = Convert.ToInt16(LoginInfoObject.UserID)

      If _CurrentCompanyDataRow.RowState = DataRowState.Detached Then
        CurrentRecordDataRow = _CurrentCompanyDataRow
        _CompanyDataTable.Rows.Add(_CurrentCompanyDataRow)
      End If

      _CompanyTableAdapterObject.Update(_CompanyDataTable)

      '****** Update Communication 
      If _CommunicationDataRow Is Nothing Then
        _CommunicationDataRow = _CommunicationTable.NewCommunicationRow
        _CommunicationDataRow.Co_ID = _CurrentCompanyDataRow.Co_Id
        _CommunicationDataRow.Communication_ID = _CommunicationAdapter.GetNewCommunicationID(_CurrentCompanyDataRow.Co_Id).Value
        _CommunicationDataRow.Source_Document_Co_ID = _CurrentCompanyDataRow.Co_Id
        _CommunicationDataRow.Source_Document_ID = _CurrentCompanyDataRow.Co_Id
        _CommunicationDataRow.Source_DocumentType_ID = Constants.enuDocumentType.Company

      End If
      'Set common properites for insert and update
      _CommunicationDataRow.Communication_Type = Constants.enuCommuncationTypes.PhoneNumber
      _CommunicationDataRow.Communication_Value = Me.CommunicationTextBox.Text
      _CommunicationDataRow.Stamp_UserID = Me.LoginInfoObject.UserID
      _CommunicationDataRow.Stamp_DateTime = Common.SystemDateTime

      If _CommunicationDataRow.RowState = DataRowState.Detached Then
        _CommunicationTable.Rows.Add(_CommunicationDataRow)
      End If

      _CommunicationAdapter.Update(_CommunicationDataRow)

      '****** Update Address
      If _AddressDataRow Is Nothing Then
        _AddressDataRow = _AddressTable.NewAddressRow
        _AddressDataRow.Co_ID = _CurrentCompanyDataRow.Co_Id
        _AddressDataRow.Address_ID = _AddressAdapter.GetNewAddressID(_CurrentCompanyDataRow.Co_Id).Value
        _AddressDataRow.Parent_Address_ID = 0
        _AddressDataRow.Source_Document_Co_ID = _CurrentCompanyDataRow.Co_Id
        _AddressDataRow.Source_Document_ID = _CurrentCompanyDataRow.Co_Id
        _AddressDataRow.Source_DocumentType_ID = Constants.enuDocumentType.Company
      End If
      'Set common properites for insert and update
      _AddressDataRow.AddressType_ID = Constants.enuAddressTypes.PrimaryAddress
      _AddressDataRow.Address_Desc = Me.AddressTextBox.Text
      _AddressDataRow.Stamp_DateTime = Common.SystemDateTime
      _AddressDataRow.Stamp_UserID = Me.LoginInfoObject.UserID

      If _AddressDataRow.RowState = DataRowState.Detached Then
        _AddressTable.Rows.Add(_AddressDataRow)
      End If

      _AddressAdapter.Update(_AddressDataRow)

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in saverecord method of CompanyForm", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try
      If Me._CompanyDataTable.Rows.Count > 0 Then
        Me._CurrentCompanyDataRow = _CompanyDataTable(0)   ' CType(Me._CompanyDataTable.Rows(Me.CurrentRecordIndex), CompanyRow)
        Me.ClearControls(Me)
        Me.CompanyIDTextBox.Text = _CurrentCompanyDataRow.Co_Id.ToString()
        ParentCompanyComboBox.Value = _CurrentCompanyDataRow.Parent_Co_ID
        Me.CompanyCodeTextBox.Text = _CurrentCompanyDataRow.Co_Code.ToString()
        CompanyDescTextBox.Text = _CurrentCompanyDataRow.Co_Desc.ToString()
        If Not _CurrentCompanyDataRow.IsInactive_FromNull Then
          CompanyInactiveFromCalendarCombo.Value = _CurrentCompanyDataRow.Inactive_From
        Else
          CompanyInactiveFromCalendarCombo.Value = Nothing
        End If
        If Not _CurrentCompanyDataRow.IsInactive_ToNull Then
          CompanyInactiveToCalendarCombo.Value = _CurrentCompanyDataRow.Inactive_To
        Else
          CompanyInactiveToCalendarCombo.Value = Nothing
        End If

        '***** Show Communication Details
        If Me._CommunicationDataRow IsNot Nothing Then
          Me.CommunicationTextBox.Text = Me._CommunicationDataRow.Communication_Value
        Else
          Me.CommunicationTextBox.Text = ""
        End If

        '***** Show Address Details
        If Me._AddressDataRow IsNot Nothing Then
          Me.AddressTextBox.Text = Me._AddressDataRow.Address_Desc
        Else
          Me.AddressTextBox.Text = ""
        End If
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Private Function PopulateParentCompanyCombo() As Boolean

    ParentCompanyComboBox.DataSource = _CompanyTableAdapterObject.GetAll()
    ParentCompanyComboBox.ValueMember = CompanyCode
    ParentCompanyComboBox.DisplayMember = CompanyDescription
    With ParentCompanyComboBox.DisplayLayout.Bands(0)
      For i As Int32 = 0 To .Columns.Count - 1
        If .Columns(CompanyCode).Index <> .Columns(i).Index And .Columns(CompanyDescription).Index <> .Columns(i).Index Then
          ParentCompanyComboBox.DisplayLayout.Bands(0).Columns(i).Hidden = True
        End If
      Next
    End With


  End Function

  Private Sub LoadCommuncationDetails()
    Try
      _CommunicationTable = _CommunicationAdapter.GetBySource(Constants.enuDocumentType.Company, _CurrentCompanyDataRow.Co_Id, _CurrentCompanyDataRow.Co_Id)
      If _CommunicationTable.Rows.Count > 0 Then
        _CommunicationDataRow = _CommunicationTable(0)
      Else
        _CommunicationDataRow = Nothing
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in LoadCommuncationDetails on CompanyForm", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  Private Sub LoadAddressDetails()
    Try
      _AddressTable = _AddressAdapter.GetBySource(Constants.enuDocumentType.Company, _CurrentCompanyDataRow.Co_Id, _CurrentCompanyDataRow.Co_Id)
      If _AddressTable.Rows.Count > 0 Then
        _AddressDataRow = _AddressTable(0)
      Else
        _AddressDataRow = Nothing
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in LoadAddressDetails on CompanyForm", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

#End Region

#Region "Properties"

#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _TempCompanyDataTable As CompanyDataTable = Nothing

      Cursor = Cursors.WaitCursor
      _TempCompanyDataTable = Me._CompanyTableAdapterObject.GetFirst()
      If _TempCompanyDataTable.Rows.Count > 0 Then
        _CompanyDataTable = _TempCompanyDataTable
        _CurrentCompanyDataRow = _CompanyDataTable(0)

        '***** Load Communcation Details
        LoadCommuncationDetails()
        '***** Load Communcation Details
        LoadAddressDetails()

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
      Dim _TempCompanyDataTable As CompanyDataTable = Nothing

      Cursor = Cursors.WaitCursor

      If (_CurrentCompanyDataRow Is Nothing OrElse _CurrentCompanyDataRow.Table Is Nothing) Then
        _TempCompanyDataTable = Me._CompanyTableAdapterObject.GetNextByCoID(LoginInfoObject.CompanyID)
      Else
        _TempCompanyDataTable = Me._CompanyTableAdapterObject.GetNextByCoID(_CurrentCompanyDataRow.Co_Id)
      End If

      If _TempCompanyDataTable.Rows.Count > 0 Then
        _CompanyDataTable = _TempCompanyDataTable
        _CurrentCompanyDataRow = _CompanyDataTable(0)

        '***** Load Communcation Details
        LoadCommuncationDetails()
        '***** Load Communcation Details
        LoadAddressDetails()

        MyBase.MoveNextButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move next", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _TempCompanyDataTable As CompanyDataTable = Nothing

      Cursor = Cursors.WaitCursor

      If (_CurrentCompanyDataRow Is Nothing) Then
        _TempCompanyDataTable = Me._CompanyTableAdapterObject.GetPreviousByCoID(LoginInfoObject.CompanyID)
      Else
        _TempCompanyDataTable = Me._CompanyTableAdapterObject.GetPreviousByCoID(_CurrentCompanyDataRow.Co_Id)
      End If

      If _TempCompanyDataTable.Rows.Count > 0 Then
        _CompanyDataTable = _TempCompanyDataTable
        _CurrentCompanyDataRow = _CompanyDataTable(0)

        '***** Load Communcation Details
        LoadCommuncationDetails()
        '***** Load Communcation Details
        LoadAddressDetails()

        MyBase.MovePreviousButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move previous", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Dim _TempCompanyDataTable As CompanyDataTable = Nothing

      Cursor = Cursors.WaitCursor

      _TempCompanyDataTable = Me._CompanyTableAdapterObject.GetLast()
      If _TempCompanyDataTable.Rows.Count > 0 Then
        _CompanyDataTable = _TempCompanyDataTable
        _CurrentCompanyDataRow = _CompanyDataTable(0)

        '***** Load Communcation Details
        LoadCommuncationDetails()
        '***** Load Communcation Details
        LoadAddressDetails()

        MyBase.MoveLastButtonClick(sender, e)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move last", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub SaveButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If SaveRecord() Then
        QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully saved", QuickMessageBox.MessageBoxTypes.ShortMessage)
      Else
        QuickMessageBox.Show(Me.LoginInfoObject, "Record is not successfully saved", QuickMessageBox.MessageBoxTypes.ShortMessage)
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub CancelButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Me._CurrentCompanyDataRow = Nothing
      Me._CompanyDataTable.Rows.Clear()
      MyBase.CancelButtonClick(sender, e)
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to cancel button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub DeleteButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      If MessageBox.Show("Are you sure, you want to delete the record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

        '***** Delete data from Communication
        _CommunicationAdapter.DeleteAllBySource(Constants.enuDocumentType.Company, Me._CurrentCompanyDataRow.Co_Id, Me._CurrentCompanyDataRow.Co_Id)
        _CommunicationDataRow = Nothing

        '***** Delete data from Address
        _AddressAdapter.DeleteAllBySource(Constants.enuDocumentType.Company, Me._CurrentCompanyDataRow.Co_Id, Me._CurrentCompanyDataRow.Co_Id)
        _AddressDataRow = Nothing

        Me._CurrentCompanyDataRow.Delete()
        _CompanyTableAdapterObject.Update(_CompanyDataTable)

        Me._CurrentCompanyDataRow = Nothing
        MyBase.DeleteButtonClick(sender, e)

        QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)
      Else
        'Do nothing user don't want to delete.
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to delete button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

#End Region

End Class