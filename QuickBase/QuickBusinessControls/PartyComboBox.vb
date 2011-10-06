Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL
Imports QuickDALLibrary
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters

Public Class PartyComboBox

#Region "Declaration"

  Private _PartyTA As New PartyTableAdapter
  Private _PartyDataTable As New PartyDataTable
  Private _EntityType As Constants.EntityTypes
  Private _PartyCode As String

#End Region

#Region "Methods"

  Public Sub LoadParties(ByVal _CompanyIDpara As Int16)
    Try
      If _EntityType = 0 Then
        _PartyDataTable = _PartyTA.GetByInactiveStatusCoID(False, _CompanyIDpara)
      ElseIf EntityType = Constants.EntityTypes.Customer Then
        _PartyDataTable = _PartyTA.GetByCoIDInactiveStatus2EntityTypes(False, _CompanyIDpara, EntityTypes.Customer, EntityTypes.CustomerAndSupplier)
      ElseIf EntityType = Constants.EntityTypes.Supplier Then
        _PartyDataTable = _PartyTA.GetByCoIDInactiveStatus2EntityTypes(False, _CompanyIDpara, EntityTypes.Supplier, EntityTypes.CustomerAndSupplier)
      Else
        _PartyDataTable = _PartyTA.GetByCoIDInactiveStatusEntityTypeID(False, _CompanyIDpara, EntityType)
      End If
      _PartyDataTable.Party_CodeColumn.SetOrdinal(1)  'It should be the first column.
      General.SetColumnCaptions(DirectCast(_PartyDataTable, DataTable), Me.Name)
      'Reverse loop, to handle the index change due to deletion of row.
      For I As Int32 = _PartyDataTable.Columns.Count - 1 To 0 Step -1
        Select Case _PartyDataTable.Columns(I).ColumnName
          Case _PartyDataTable.Party_DescColumn.ColumnName, _PartyDataTable.Party_IDColumn.ColumnName _
            , _PartyDataTable.Co_IDColumn.ColumnName, _PartyDataTable.COA_IDColumn.ColumnName _
            , _PartyDataTable.Party_CodeColumn.ColumnName
          Case Else
            _PartyDataTable.Columns.RemoveAt(I)
        End Select
      Next

      Me.DataSource = _PartyDataTable
      EntryMode = EntryMode
      Me.DropDownWidth = Me.Width
      Me.Rows.Band.Columns(_PartyDataTable.Co_IDColumn.ColumnName).Hidden = True
      Me.Rows.Band.Columns(_PartyDataTable.COA_IDColumn.ColumnName).Hidden = True
      Me.Rows.Band.Columns(_PartyDataTable.Party_IDColumn.ColumnName).Hidden = True

    Catch ex As Exception
      Dim QuickExceptionObject As New QuickException("Exception in populating sales man", ex)
      Throw QuickExceptionObject
    End Try
  End Sub

  'Author: Faisal
  'Date Created(DD-MMM-YY): 22-Nov-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This function will return "Yes" if the party selected or entered is valid
  ''' otherwise "No".
  ''' </summary>
  Public Function IsValid() As Constants.MethodResult
    Try
      Return Me.IsTextInList(_PartyDataTable.Party_CodeColumn.ColumnName)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in IsValid of PartCombBox.", ex)
      Throw _qex
    End Try
  End Function


#End Region

#Region "Properties"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 30-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Returns the party code from the selected/entered party.
  ''' </summary>
  Public Property PartyCode() As String
    Get
      Try

        If Me.SelectedRow Is Nothing Then
          Return String.Empty
        Else
          Return Me.SelectedRow.Cells(_PartyDataTable.Party_CodeColumn.ColumnName).Text
        End If

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in PartyCode of PartyComboBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As String)
      Try

        For I As Int32 = 0 To Me.Rows.Count - 1
          If Me.Rows(I).Cells(_PartyDataTable.Party_CodeColumn.ColumnName).Text.ToLower = value.ToLower Then
            Me.SelectedRow = Me.Rows(I)
            Exit For
          End If
        Next

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in PartyCode of PartyComboBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Public Property PartyID() As Int32
    Get
      Try
        If Me.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.SelectedRow.Cells(_PartyDataTable.Party_IDColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in getting sales man id", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int32)
      For I As Int32 = 0 To Me.Rows.Count - 1
        If Me.Rows(I).Cells(_PartyDataTable.Party_IDColumn.ColumnName).Text = value.ToString Then
          Me.SelectedRow = Me.Rows(I)
          Exit For
        End If
      Next
    End Set
  End Property

  Public Property EntityType() As Constants.EntityTypes
    Get
      Return _EntityType
    End Get
    Set(ByVal value As Constants.EntityTypes)
      _EntityType = value
    End Set
  End Property

  'Author: Faisal
  'Date Created(DD-MMM-YY): 22-Nov-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This is the overriden method in PartyComboBox which will set entry mode and
  ''' also set  display and value member.
  ''' </summary>
  Public Overrides Property EntryMode() As QuickControls.Quick_UltraComboBox.EntryModes
    Get
      Return MyBase.EntryMode
    End Get
    Set(ByVal value As QuickControls.Quick_UltraComboBox.EntryModes)
      MyBase.EntryMode = value
      If EntryMode = EntryModes.SelectionFromList Then
        Me.DisplayMember = _PartyDataTable.Party_DescColumn.ColumnName
      Else
        Me.DisplayMember = _PartyDataTable.Party_CodeColumn.ColumnName
      End If
    End Set
  End Property
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
  End Sub

#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2009
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Overriden event method of parent class.
  ''' </summary>
  Protected Overrides Sub Quick_UltraComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs)
    Try
      'Prent method has the logic to resize drop down panel width. Drop down panel should be resized first and then columns because
      'columns are withing panel.
      MyBase.Quick_UltraComboBox_Resize(sender, e)

      Me.Rows.Band.Columns(_PartyDataTable.Party_CodeColumn.ColumnName).Width = (Me.Width - Constants.SCROLLBAR_WIDTH) * 30 / 100
      Me.Rows.Band.Columns(_PartyDataTable.Party_DescColumn.ColumnName).Width = (Me.Width - Constants.SCROLLBAR_WIDTH) * 70 / 100
    Catch ex As Exception
      'Ignore all errors.
    End Try
  End Sub

#End Region

End Class
