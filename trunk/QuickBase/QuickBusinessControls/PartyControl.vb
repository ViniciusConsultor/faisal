Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters

'Author: Faisal
'Date Created(DD-MMM-YY): 26-Dec-09
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'
''' <summary>
''' It is inherited from Quick_UltraComboBoxWithLabel, further has some logic for
''' populating and validating parties.
''' </summary>
Public Class PartyControl

#Region "Declarations"
  Private _PartyTA As New PartyTableAdapter
  Private _PartyDataTable As New PartyDataTable
  Private _EntityType As Constants.EntityTypes
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
            , _PartyDataTable.Party_CodeColumn.ColumnName, _PartyDataTable.AddressColumn.ColumnName
          Case Else
            _PartyDataTable.Columns.RemoveAt(I)
        End Select
      Next

      Me.Quick_UltraComboBox1.DataSource = _PartyDataTable
      EntryMode = EntryMode
      Me.Quick_UltraComboBox1.DropDownWidth = Me.Width
      Me.Quick_UltraComboBox1.Rows.Band.Columns(_PartyDataTable.Co_IDColumn.ColumnName).Hidden = True
      Me.Quick_UltraComboBox1.Rows.Band.Columns(_PartyDataTable.COA_IDColumn.ColumnName).Hidden = True
      Me.Quick_UltraComboBox1.Rows.Band.Columns(_PartyDataTable.Party_IDColumn.ColumnName).Hidden = True
      Me.Quick_UltraComboBox1.Rows.Band.Columns(_PartyDataTable.AddressColumn.ColumnName).Hidden = True

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
      Return Me.Quick_UltraComboBox1.IsTextInList(_PartyDataTable.Party_CodeColumn.ColumnName)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in IsValid of PartCombBox.", ex)
      Throw _qex
    End Try
  End Function

#End Region

#Region "Properties"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' EntryMode of the PartyComboBox
  ''' </summary>
  Public Property EntryMode() As PartyComboBox.EntryModes
    Get
      Try

        Return Me.Quick_UltraComboBox1.EntryMode

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in EntryMode of ClassName/FormName.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As PartyComboBox.EntryModes)
      Try

        Me.Quick_UltraComboBox1.EntryMode = value
        If EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList Then
          Me.Quick_UltraComboBox1.DisplayMember = _PartyDataTable.Party_DescColumn.ColumnName
        Else
          Me.Quick_UltraComboBox1.DisplayMember = _PartyDataTable.Party_CodeColumn.ColumnName
          Me.ColumnNameForLabelDisplay = _PartyDataTable.Party_DescColumn.ColumnName
        End If

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in EntryMode of ClassName/FormName.", ex)
        Throw _qex
      End Try
    End Set
  End Property


  Private _CoaID As Int32
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Jun-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  'Faisal Saleem  29-Aug-10   Added condition for empty and nothing string for COA.
  ''' <summary>
  ''' This will return Chart of account id associated with the selected party.
  ''' </summary>
  Public ReadOnly Property CoaID() As Int32
    Get
      Try

        If Me.Quick_UltraComboBox1.SelectedRow Is Nothing Then
          Return 0
        Else
          If Me.Quick_UltraComboBox1.SelectedRow.Cells(_PartyDataTable.COA_IDColumn.ColumnName).Text Is Nothing Then
            Return 0
          ElseIf Me.Quick_UltraComboBox1.SelectedRow.Cells(_PartyDataTable.COA_IDColumn.ColumnName).Text.Trim = String.Empty Then
            Return 0
          Else
            Return Convert.ToInt32(Me.Quick_UltraComboBox1.SelectedRow.Cells(_PartyDataTable.COA_IDColumn.ColumnName).Text)
          End If
        End If

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in CoaID of PartyControl.", ex)
        Throw _qex
      End Try
    End Get
  End Property

  Public Property PartyID() As Int32
    Get
      Try
        If Me.Quick_UltraComboBox1.SelectedRow Is Nothing Then
          Return 0
        Else
          Return Convert.ToInt32(Me.Quick_UltraComboBox1.SelectedRow.Cells(_PartyDataTable.Party_IDColumn.ColumnName).Text)
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in getting sales man id", ex)
        Throw QuickExceptionObject
      End Try
    End Get

    Set(ByVal value As Int32)
      For I As Int32 = 0 To Me.Quick_UltraComboBox1.Rows.Count - 1
        If Me.Quick_UltraComboBox1.Rows(I).Cells(_PartyDataTable.Party_IDColumn.ColumnName).Text = value.ToString Then
          Me.Quick_UltraComboBox1.SelectedRow = Me.Quick_UltraComboBox1.Rows(I)
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

  Private _ShowAddress As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 6-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' If true then second row will be displayed for address of the party.
  ''' </summary>
  Public Property ShowAddress() As Boolean
    Get
      Try

        Return _ShowAddress

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ShowAddress of PartyControl.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _ShowAddress = value
        If _ShowAddress Then
          Me.ControlRows = 2
        Else
          Me.ControlRows = 1
        End If

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in ShowAddress of PartyControl.", ex)
        Throw _qex
      End Try
    End Set
  End Property


#End Region

#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub Quick_UltraComboBox1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Quick_UltraComboBox1.Resize
    Try

      Me.Quick_UltraComboBox1.DropDownWidth = Me.Width
      Me.Quick_UltraComboBox1.Rows.Band.Columns(_PartyDataTable.Party_CodeColumn.ColumnName).Width = (Me.Width - Constants.SCROLLBAR_WIDTH) * 30 / 100
      Me.Quick_UltraComboBox1.Rows.Band.Columns(_PartyDataTable.Party_DescColumn.ColumnName).Width = (Me.Width - Constants.SCROLLBAR_WIDTH) * 70 / 100

    Catch ex As Exception
      'Ignore all errors.
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 6-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will display address in tooltip and label.
  ''' </summary>
  Private Sub Quick_UltraComboBox1_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles Quick_UltraComboBox1.RowSelected

    Try
      If Me.Quick_UltraComboBox1.SelectedRow Is Nothing Then
        Me.ToolTip1.SetToolTip(Me.Quick_UltraComboBox1, "")
        Me.ToolTip1.SetToolTip(Me.Quick_Label1, "")
      Else
        Me.ToolTip1.SetToolTip(Me.Quick_UltraComboBox1, Me.Quick_UltraComboBox1.SelectedRow.Cells(Me._PartyDataTable.AddressColumn.ColumnName).Text)
        Me.ToolTip1.SetToolTip(Me.Quick_Label1, Me.Quick_UltraComboBox1.SelectedRow.Cells(Me._PartyDataTable.AddressColumn.ColumnName).Text)
      End If

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Quick_UltraComboBox1_RowSelected of PartyControl.", ex)
      _qex.Show(Nothing)
    End Try

  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 6-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Resizing address textbox.
  ''' </summary>
  Private Sub PartyControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    Try

      PartyAddressTextBox.Top = Me.Quick_UltraComboBox1.Height + 1
      PartyAddressTextBox.Left = 1 : Me.PartyAddressTextBox.Width = Me.Quick_UltraComboBox1.Width

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SubName of ClassName/FormName.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.Quick_UltraComboBox1.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
  End Sub

End Class

