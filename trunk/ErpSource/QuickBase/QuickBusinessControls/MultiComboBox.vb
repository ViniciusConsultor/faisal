'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 24-Jul-10
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This control can have multiple comboboxes on runtime.
''' </summary>
Public Class MultiComboBox

#Region "Declarations"
  Private Const COMBOBOX_NAME As String = "QuickUltraComboBox"
  Private ComboBoxCollection As New Generic.List(Of QuickControls.Quick_UltraComboBox)
  Public Event ValueChanged()
  Dim _DataTable As DataTable
  Dim _MaskForCode As String
  Dim _SeperatorInValue As String
  Dim _ColumnNameForComboBoxes As String
  Dim _ColumnNameForReturnValue As String
#End Region

#Region "Properties"

  Private _Text As String = String.Empty
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 03-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will return the concatinated value from the combo boxes.
  ''' </summary>
  Public Overrides Property Text() As String
    Get
      Try

        Return _Text

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in Text of MultiComboBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As String)
      Try

        MsgBox("You cannot set value for now")

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in Text of MultiComboBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property

#End Region

#Region "Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 24-Jul-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It changes the number of combo boxes in this control.
  ''' </summary>
  Public Sub SetComboBoxes(ByVal NumberOfCombBoxes As Int32)
    Try
      For I As Int32 = 1 To NumberOfCombBoxes
        Me.ComboBoxCollection.Add(New QuickControls.Quick_UltraComboBox)
        Me.ComboBoxCollection(I - 1).Rows.Band.ColHeadersVisible = False
        Me.ComboBoxCollection(I - 1).Name = "Combo" & I.ToString
        AddHandler Me.ComboBoxCollection(I - 1).ValueChanged, AddressOf Quick_UltraCombo_ValueChanged
        Me.Controls.Add(Me.ComboBoxCollection(I - 1))
      Next I

      AdjustCombBoxes()
    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetComboBoxes of MultiComboBox.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 01-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will re-arrange position and widths of the combo boxes according to the
  ''' current width of the control.
  ''' </summary>
  Private Sub AdjustCombBoxes()
    Try
      Dim _Width As Double

      _Width = Me.Width / Me.ComboBoxCollection.Count
      For I As Int32 = 0 To Me.ComboBoxCollection.Count - 1
        Me.ComboBoxCollection(I).Width = _Width
        Me.ComboBoxCollection(I).Left = _Width * I
      Next

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in AdjustCombBoxes of MultiComboBox.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 01-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will set number of columns and populate values in combo boxes.
  ''' </summary>
  Public Sub SetComboBoxesOnDataTable(ByVal _DataTablepara As DataTable, ByVal _MaskForCodepara As String, ByVal _SeperatorInValuepara As String, ByVal _ColumnNameForComboBoxespara As String, ByVal _ColumnNameForReturnValuepara As String)
    Try
      Dim _Collection As New Collection

      _DataTable = _DataTablepara
      _MaskForCode = _MaskForCodepara
      _SeperatorInValue = _SeperatorInValuepara
      _ColumnNameForComboBoxes = _ColumnNameForComboBoxespara
      _ColumnNameForReturnValue = _ColumnNameForReturnValuepara

      For I As Int32 = 0 To _DataTablepara.Rows.Count - 1
        With _DataTablepara.Rows(I).Item(_ColumnNameForComboBoxespara)
          If Not _Collection.Contains(.ToString.Substring(0, _MaskForCodepara.IndexOf(_SeperatorInValuepara))) Then
            _Collection.Add(.ToString.Substring(0, _MaskForCodepara.IndexOf(_SeperatorInValuepara)), .ToString.Substring(0, _MaskForCodepara.IndexOf(_SeperatorInValuepara)))
          Else
            'If it is already there then don't need to add.
          End If
        End With
      Next

      SetComboBoxes(QuickLibrary.Common.CountStringOccurences(_SeperatorInValuepara, _MaskForCodepara) + 1)
      Me.ComboBoxCollection(0).DataSource = _Collection

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SubName of ClassName/FormName.", ex)
      Throw _qex
    End Try
  End Sub
#End Region

#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 01-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event will be fired on all combo boxes of this control.
  ''' It will populate the next combo box values.
  ''' </summary>
  Private Sub Quick_UltraCombo_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
    Try
      'If this control is not based on data table then following code will not run.
      If _DataTable IsNot Nothing Then
        Dim _ComboBoxIndex As Int32 = 0
        Dim _Collection As New Collection
        Dim _StartIndex As Int32 = -1
        Dim _Length As Int32 = 0

        _ComboBoxIndex = DirectCast(sender, QuickControls.Quick_UltraComboBox).Name.Substring(5, DirectCast(sender, QuickControls.Quick_UltraComboBox).Name.Length - 5)

        _Text = String.Empty
        'Clear next combo boxes because user changed value.
        For I As Int32 = 0 To ComboBoxCollection.Count - 1
          If I > _ComboBoxIndex Then
            ComboBoxCollection(I).DataSource = Nothing
            ComboBoxCollection(I).Text = String.Empty
            ComboBoxCollection(I).SelectedRow = Nothing
          Else
            _Text &= ComboBoxCollection(I).Text & _SeperatorInValue
          End If
        Next I
        If _Text.Length > 0 Then _Text = _Text.Substring(0, _Text.Length - 1)

        If _ComboBoxIndex = Me.ComboBoxCollection.Count Then
          'When it is last combo then there is nothing to populate
        Else
          _StartIndex = QuickLibrary.Common.IndexOfStringAtOccurence(_SeperatorInValue, _MaskForCode, _ComboBoxIndex) + 1
          _Length = QuickLibrary.Common.IndexOfStringAtOccurence(_SeperatorInValue, _MaskForCode, _ComboBoxIndex + 1)
          If _Length < 0 Then _Length = _MaskForCode.Length
          _Length = _Length - _StartIndex

          'Now populate values in the very next combo box.
          For I As Int32 = 0 To _DataTable.Rows.Count - 1
            With _DataTable.Rows(I).Item(_ColumnNameForComboBoxes)
              If Not _Collection.Contains(.ToString.Substring(_StartIndex, _Length)) Then
                _Collection.Add(.ToString.Substring(_StartIndex, _Length), .ToString.Substring(_StartIndex, _Length))
              Else
                'If it is already there then don't need to add.
              End If
            End With
          Next

          Me.ComboBoxCollection(_ComboBoxIndex).DataSource = _Collection
        End If
      End If

      RaiseEvent ValueChanged()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Quick_UltraCombo_ValueChanged of MultiCombBox.", ex)
      _qex.Show(Nothing)
    End Try

  End Sub
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub
End Class

