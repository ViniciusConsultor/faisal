Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDAL.QuickInventoryDataSet
Imports QuickDAL.QuickSecurityDataSet
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickDALLibrary
Imports System.windows.forms

Public Class DataEntryForm

#Region "Declaration"
  Private _DocumentTypeDataTable As DataTable

  Dim _ItemTableAdapterObject As New ItemTableAdapter
  Dim _BranchTableAdapterObject As New CompanyTableAdapter
  Dim _PartyTableAdapterObject As New PartyTableAdapter
  Dim _UserTableAdapterObject As New UserTableAdapter

  Dim _ItemDataTable As New ItemDataTable
  Dim _BranchDataTable As New CompanyDataTable
  Dim _PartyDataTable As New PartyDataTable
  Dim _UserDataTable As New UserDataTable

  Private FieldLimit As Int32 = 100
#End Region

#Region "Toolbar methods"

  Protected Overrides Sub MoveFirstButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Select Case DocumentType
        Case enuDocumentType.Item
          Me._DocumentTypeDataTable = _ItemTableAdapterObject.GetFirstByCoID(LoginInfoObject.CompanyID)
        Case enuDocumentType.Company
          Me._DocumentTypeDataTable = _BranchTableAdapterObject.GetFirst
        Case enuDocumentType.Party
          Me._DocumentTypeDataTable = _PartyTableAdapterObject.GetFirstByCoID(LoginInfoObject.CompanyID)
        Case enuDocumentType.User
          Me._DocumentTypeDataTable = _UserTableAdapterObject.GetFirstByCoID(LoginInfoObject.CompanyID)
      End Select

      MyBase.MoveFirstButtonClick(sender, e)

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

      Select Case DocumentType
        Case enuDocumentType.Item
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _ItemTableAdapterObject.GetNextByCoIDItemCode(LoginInfoObject.CompanyID, "")
          Else
            Me._DocumentTypeDataTable = _ItemTableAdapterObject.GetNextByCoIDItemCode(LoginInfoObject.CompanyID, DirectCast(_CurrentRecordDataRow, ItemRow).Item_Code)
          End If
        Case enuDocumentType.Company
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _BranchTableAdapterObject.GetNextByCoID(0)
          Else
            Me._DocumentTypeDataTable = _BranchTableAdapterObject.GetNextByCoID(DirectCast(_CurrentRecordDataRow, CompanyRow).Co_Id)
          End If
        Case enuDocumentType.Party
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _PartyTableAdapterObject.GetNextByCoIDPartyID(LoginInfoObject.CompanyID, 0)
          Else
            Me._DocumentTypeDataTable = _PartyTableAdapterObject.GetNextByCoIDPartyID(LoginInfoObject.CompanyID, DirectCast(_CurrentRecordDataRow, PartyRow).Party_ID)
          End If
        Case enuDocumentType.User
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _UserTableAdapterObject.GetNextByCoIDUserID(LoginInfoObject.CompanyID, 0)
          Else
            Me._DocumentTypeDataTable = _UserTableAdapterObject.GetNextByCoIDUserID(LoginInfoObject.CompanyID, DirectCast(_CurrentRecordDataRow, UserRow).User_ID)
          End If
      End Select

      MyBase.MoveNextButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move next", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MovePreviousButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Select Case DocumentType
        Case enuDocumentType.Item
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _ItemTableAdapterObject.GetPreviousByCoIDItemCode(LoginInfoObject.CompanyID, "")
          Else
            Me._DocumentTypeDataTable = _ItemTableAdapterObject.GetPreviousByCoIDItemCode(LoginInfoObject.CompanyID, DirectCast(_CurrentRecordDataRow, ItemRow).Item_Code)
          End If
        Case enuDocumentType.Company
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _BranchTableAdapterObject.GetPreviousByCoID(0)
          Else
            Me._DocumentTypeDataTable = _BranchTableAdapterObject.GetPreviousByCoID(DirectCast(_CurrentRecordDataRow, CompanyRow).Co_Id)
          End If
        Case enuDocumentType.Party
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _PartyTableAdapterObject.GetPreviousByCoIDPartyID(LoginInfoObject.CompanyID, 0)
          Else
            Me._DocumentTypeDataTable = _PartyTableAdapterObject.GetPreviousByCoIDPartyID(LoginInfoObject.CompanyID, DirectCast(_CurrentRecordDataRow, PartyRow).Party_ID)
          End If
        Case enuDocumentType.User
          If Me._CurrentRecordDataRow Is Nothing Then
            Me._DocumentTypeDataTable = _UserTableAdapterObject.GetPreviousByCoIDUserID(LoginInfoObject.CompanyID, 0)
          Else
            Me._DocumentTypeDataTable = _UserTableAdapterObject.GetPreviousByCoIDUserID(LoginInfoObject.CompanyID, DirectCast(_CurrentRecordDataRow, UserRow).User_ID)
          End If
      End Select

      MyBase.MovePreviousButtonClick(sender, e)

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to move previous", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub

  Protected Overrides Sub MoveLastButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      Cursor = Cursors.WaitCursor

      Select Case DocumentType
        Case enuDocumentType.Item
          Me._DocumentTypeDataTable = _ItemTableAdapterObject.GetLastByCoID(LoginInfoObject.CompanyID)
        Case enuDocumentType.Company
          Me._DocumentTypeDataTable = _BranchTableAdapterObject.GetLast
        Case enuDocumentType.Party
          Me._DocumentTypeDataTable = _PartyTableAdapterObject.GetLastByCoID(LoginInfoObject.CompanyID)
        Case enuDocumentType.User
          Me._DocumentTypeDataTable = _UserTableAdapterObject.GetLastByCoID(LoginInfoObject.CompanyID)
      End Select

      MyBase.MoveLastButtonClick(sender, e)

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
        QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully saved.", QuickMessageBox.MessageBoxTypes.ShortMessage)
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
      Me._CurrentRecordDataRow = Nothing
      Me._DocumentTypeDataTable.Clear()
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
        Me._CurrentRecordDataRow.Delete()

        If SaveRecord() Then
          QuickMessageBox.Show(Me.LoginInfoObject, "Record is successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)
          MyBase.DeleteButtonClick(sender, e)
        Else
          QuickMessageBox.Show(Me.LoginInfoObject, "Record is not successfully deleted.", QuickMessageBox.MessageBoxTypes.ShortMessage)
        End If
      Else
        'User canceled the delete operation.
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to delete button click", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Events"
  Private Sub DataEntryForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Cursor = Windows.Forms.Cursors.WaitCursor

      Select Case DocumentType
        Case enuDocumentType.Company
          _DocumentTypeDataTable = New CompanyDataTable
        Case enuDocumentType.Party
          _DocumentTypeDataTable = New PartyDataTable
        Case enuDocumentType.Item
          _DocumentTypeDataTable = New ItemDataTable
        Case enuDocumentType.User
          _DocumentTypeDataTable = New UserDataTable
        Case Else
          MessageBox.Show("This document type is not incorporated yet", "Unknown Document Type", MessageBoxButtons.OK, MessageBoxIcon.Information)
          _DocumentTypeDataTable = New ItemDataTable
      End Select

      If _DocumentTypeDataTable Is Nothing Then
        'Me.Spread.Hide()
      Else
        General.SetColumnCaptions(_DocumentTypeDataTable, Me.Name)
        If _DocumentTypeDataTable.Columns.Count > FieldLimit Then
          Me.Spread_Sheet1.Columns.Count = 4
          Me.Spread_Sheet1.Rows.Count = Convert.ToInt32(System.Math.Round(_DocumentTypeDataTable.Columns.Count / 2, 0, MidpointRounding.AwayFromZero))
        Else
          Me.Spread_Sheet1.Columns.Count = 2
          Me.Spread_Sheet1.Rows.Count = _DocumentTypeDataTable.Columns.Count
        End If

        ShowFields()
        SetWidths()
      End If

      Me.Spread_Sheet1.RowHeaderVisible = False
      Me.Spread_Sheet1.ColumnHeaderVisible = False

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    Finally
      Cursor = Windows.Forms.Cursors.Default
    End Try
  End Sub

#End Region

#Region "Methods"
  Protected Overridable Function ShowFields() As Boolean
    Try
      Dim _SpreadRowNo As Int32
      Dim _SpreadFieldColumnNo As Int32
      Dim _SpreadFieldValueNo As Int32
      'Dim _FieldFont As Drawing.Font

      For I As Int32 = 0 To _DocumentTypeDataTable.Columns.Count - 1
        If _DocumentTypeDataTable.Columns.Count > FieldLimit Then
          If I > 0 Then
            If _SpreadFieldColumnNo = 2 Then
              _SpreadRowNo += 1
              _SpreadFieldColumnNo = 0
            Else
              _SpreadFieldColumnNo = 2
            End If
          End If
        Else
          _SpreadRowNo = I
          _SpreadFieldColumnNo = 0
        End If

        _SpreadFieldValueNo = _SpreadFieldColumnNo + 1

        Me.Spread_Sheet1.SetText(_SpreadRowNo, _SpreadFieldColumnNo, _DocumentTypeDataTable.Columns(I).Caption)
        Me.Spread_Sheet1.Columns(_SpreadFieldColumnNo).Locked = True
        Me.Spread_Sheet1.Columns(_SpreadFieldColumnNo).TabStop = False
        Me.Spread_Sheet1.Columns(_SpreadFieldColumnNo).BackColor = Drawing.Color.LightGray

        If _SpreadRowNo Mod 2 = 0 Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).BackColor = Drawing.Color.AntiqueWhite
        End If
        If LoginInfoObject.CompanyID <> 0 Then
          Select Case _DocumentTypeDataTable.Columns(I).ColumnName
            Case _BranchDataTable.Co_IdColumn.ColumnName
              If Me.DocumentType <> enuDocumentType.Company Then
                Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).Locked = True
              End If
            Case _BranchDataTable.Stamp_UserIDColumn.ColumnName _
            , _BranchDataTable.Stamp_DateTimeColumn.ColumnName
              Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).Locked = True
          End Select
        End If

        If _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Boolean") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.DateTime") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.String") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.TextCellType
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Int16") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.NumberCellType
          Me.Spread_Sheet1.SetValue(_SpreadRowNo, _SpreadFieldValueNo, 0)
          DirectCast(Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType, FarPoint.Win.Spread.CellType.NumberCellType).DecimalPlaces = 0
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Int32") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.NumberCellType
          Me.Spread_Sheet1.SetValue(_SpreadRowNo, _SpreadFieldValueNo, 0)
          DirectCast(Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType, FarPoint.Win.Spread.CellType.NumberCellType).DecimalPlaces = 0
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Int64") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.NumberCellType
          Me.Spread_Sheet1.SetValue(_SpreadRowNo, _SpreadFieldValueNo, 0)
          DirectCast(Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType, FarPoint.Win.Spread.CellType.NumberCellType).DecimalPlaces = 0
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Single") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.NumberCellType
          Me.Spread_Sheet1.SetValue(_SpreadRowNo, _SpreadFieldValueNo, 0)
          DirectCast(Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType, FarPoint.Win.Spread.CellType.NumberCellType).DecimalPlaces = 2
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Double") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.NumberCellType
          Me.Spread_Sheet1.SetValue(_SpreadRowNo, _SpreadFieldValueNo, 0)
          DirectCast(Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType, FarPoint.Win.Spread.CellType.NumberCellType).DecimalPlaces = 2
        ElseIf _DocumentTypeDataTable.Columns(I).DataType Is System.Type.GetType("System.Decimal") Then
          Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType = New FarPoint.Win.Spread.CellType.NumberCellType
          Me.Spread_Sheet1.SetValue(_SpreadRowNo, _SpreadFieldValueNo, 0)
          DirectCast(Me.Spread_Sheet1.Cells(_SpreadRowNo, _SpreadFieldValueNo).CellType, FarPoint.Win.Spread.CellType.NumberCellType).DecimalPlaces = 2
        End If
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception to show fields", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Protected Overridable Function SetWidths() As Boolean
    Try
      For I As Int32 = 0 To Me.Spread_Sheet1.Columns.Count - 1
        If (I Mod 2) = 0 Then
          Me.Spread_Sheet1.Columns(I).Width = Me.Spread_Sheet1.Columns(I).GetPreferredWidth
        Else
          Me.Spread_Sheet1.Columns(I).Width = 200
        End If
      Next

      Me.Spread.Width = Me.Width - Me.Spread.Left - 20
      Me.Spread.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Bottom
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception to set widths", ex)
      Throw QuickExceptionObject
    End Try
  End Function

  Private Function IsValid() As Boolean
    Try

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to check if record is valid", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Function SaveRecord() As Boolean
    Try
      'Dim NewID As Int32
      Dim _ItemDataRow As ItemRow = Nothing
      Dim _BranchDataRow As CompanyRow = Nothing
      Dim _PartyDataRow As PartyRow = Nothing
      Dim _UserDataRow As UserRow = Nothing
      Dim _ValueChanged As Boolean

      If Not IsValid() Then Exit Function

      Me.Spread.EditMode = False

      If _CurrentRecordDataRow Is Nothing Then
        Select Case DocumentType
          Case enuDocumentType.Company
            _BranchDataRow = _BranchDataTable.NewCompanyRow
            _CurrentRecordDataRow = Nothing
            _CurrentRecordDataRow = _BranchDataRow
          Case enuDocumentType.Item
            _ItemDataRow = _ItemDataTable.NewItemRow
            _CurrentRecordDataRow = Nothing
            _CurrentRecordDataRow = _ItemDataRow
          Case enuDocumentType.Party
            _PartyDataRow = _PartyDataTable.NewPartyRow
            _CurrentRecordDataRow = Nothing
            _CurrentRecordDataRow = _PartyDataRow
          Case enuDocumentType.User
            _UserDataRow = _UserDataTable.NewUserRow
            _CurrentRecordDataRow = Nothing
            _CurrentRecordDataRow = _UserDataRow
        End Select
      Else
        'In case of updated only common properties need to be set.
      End If

      'If row is deleted then there is no need to set user values.
      If _CurrentRecordDataRow.RowState <> DataRowState.Deleted Then
        'Single loop to update values is not used otherwise it will always change the values.
        For I As Int32 = 0 To _DocumentTypeDataTable.Columns.Count - 1
          _ValueChanged = False
          If Me.Spread_Sheet1.GetValue(I, 1) Is Nothing AndAlso _CurrentRecordDataRow.Item(I) IsNot Nothing Then
            _ValueChanged = True
          ElseIf Me.Spread_Sheet1.GetValue(I, 1) IsNot Nothing AndAlso _CurrentRecordDataRow.Item(I) Is Nothing Then
            _ValueChanged = True
          ElseIf Me.Spread_Sheet1.GetValue(I, 1) Is Nothing AndAlso _CurrentRecordDataRow.Item(I) Is Nothing Then
            _ValueChanged = False
          ElseIf Me.Spread_Sheet1.GetValue(I, 1).ToString <> _CurrentRecordDataRow.Item(I).ToString Then
            _ValueChanged = True
          End If

          If _ValueChanged Then
            If Me.Spread_Sheet1.GetValue(I, 1) Is Nothing Then
              _CurrentRecordDataRow.Item(I) = DBNull.Value
            Else
              _CurrentRecordDataRow.Item(I) = Me.Spread_Sheet1.GetValue(I, 1)
            End If
          End If
        Next
      End If

      'Common values to set in case of insert/ update.
      If _CurrentRecordDataRow.RowState <> DataRowState.Deleted AndAlso _CurrentRecordDataRow.RowState <> DataRowState.Unchanged Then
        For I As Int32 = 0 To _DocumentTypeDataTable.Columns.Count - 1
          If _DocumentTypeDataTable.Columns(I).ColumnName.ToLower = "stamp_datetime" Then
            _CurrentRecordDataRow.Item(I) = Now
          ElseIf _DocumentTypeDataTable.Columns(I).ColumnName.ToLower = "stamp_userid" Then
            _CurrentRecordDataRow.Item(I) = LoginInfoObject.UserID
          ElseIf (Me.DocumentType <> enuDocumentType.Company _
            AndAlso LoginInfoObject.CompanyID <> 0) _
            AndAlso _DocumentTypeDataTable.Columns(I).ColumnName.ToLower = "co_id" Then
            _CurrentRecordDataRow.Item(I) = LoginInfoObject.CompanyID
          ElseIf Me.Spread_Sheet1.GetValue(I, 1) Is Nothing Then
            _CurrentRecordDataRow.Item(I) = DBNull.Value
          Else
            _CurrentRecordDataRow.Item(I) = (Me.Spread_Sheet1.GetValue(I, 1))
          End If
        Next
      End If

      Select Case DocumentType
        Case enuDocumentType.Company
          If _CurrentRecordDataRow.RowState = DataRowState.Detached Then _BranchDataTable.Rows.Add(_CurrentRecordDataRow)
          _BranchTableAdapterObject.Update(_CurrentRecordDataRow)
          _BranchTableAdapterObject.Update(_BranchDataTable)
          _BranchTableAdapterObject.Update(DirectCast(_DocumentTypeDataTable, CompanyDataTable))
        Case enuDocumentType.Item
          If _CurrentRecordDataRow.RowState = DataRowState.Detached Then _ItemDataTable.Rows.Add(_CurrentRecordDataRow)
          _ItemTableAdapterObject.Update(_CurrentRecordDataRow)
          _ItemTableAdapterObject.Update(_ItemDataTable)
          _ItemTableAdapterObject.Update(DirectCast(_DocumentTypeDataTable, ItemDataTable))
        Case enuDocumentType.Party
          If _CurrentRecordDataRow.RowState = DataRowState.Detached Then _PartyDataTable.Rows.Add(_CurrentRecordDataRow)
          _PartyTableAdapterObject.Update(_CurrentRecordDataRow)
          _PartyTableAdapterObject.Update(_PartyDataTable)
          _PartyTableAdapterObject.Update(DirectCast(_DocumentTypeDataTable, PartyDataTable))
        Case enuDocumentType.User
          If _CurrentRecordDataRow.RowState = DataRowState.Detached Then _UserDataTable.Rows.Add(_CurrentRecordDataRow)
          _UserTableAdapterObject.Update(_CurrentRecordDataRow)
          _UserTableAdapterObject.Update(_UserDataTable)
          _UserTableAdapterObject.Update(DirectCast(_DocumentTypeDataTable, UserDataTable))
      End Select

      Return True
    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to save record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
      Return False
    End Try
  End Function

  Protected Overrides Function ShowRecord() As Boolean
    Try

      If Me._DocumentTypeDataTable.Rows.Count > 0 Then
        Me.CurrentRecordDataRow = Me._DocumentTypeDataTable.Rows(Me.CurrentRecordIndex)

        For I As Int32 = 0 To _DocumentTypeDataTable.Columns.Count - 1
          Me.Spread_Sheet1.SetValue(I, 1, Me.CurrentRecordDataRow.Item(I))
        Next
      Else
        Me.ClearControls(Me)
        Me.CurrentRecordDataRow = Nothing
      End If

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to show record", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Function

  Protected Overrides Sub ClearControls(ByRef pControlObject As System.Windows.Forms.Control)
    Try
      For I As Int32 = 0 To Me.Spread_Sheet1.Rows.Count - 1
        Me.Spread_Sheet1.SetValue(I, 1, Nothing)
      Next

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception to clear controls", ex)
      QuickExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub

#End Region
End Class