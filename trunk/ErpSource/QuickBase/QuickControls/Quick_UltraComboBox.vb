Imports QuickLibrary

Public Class Quick_UltraComboBox
  Implements IClearControl
  Implements IControlSetting

#Region "Declarations"

  Public Enum EntryModes
    SelectionFromList
    EntrybyUser
  End Enum

  Private _EntryMode As EntryModes

#End Region

#Region "Properties"
  Private _IsReadonlyForNewRecord As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Property IsReadonlyForNewRecord() As Boolean Implements IControlSetting.IsReadonlyForNewRecord
    Get
      Try

        Return _IsReadonlyForNewRecord

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsReadonlyForNewRecord of Quick_UltraComboBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsReadonlyForNewRecord = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsReadonlyForNewRecord of Quick_UltraComboBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property


  Private _IsReadonlyForExistingRecord As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Property IsReadonlyForExistingRecord() As Boolean Implements IControlSetting.IsReadonlyForExistingRecord
    Get
      Try

        Return _IsReadonlyForExistingRecord

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsReadonlyForExistingRecord of Quick_UltraComboBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsReadonlyForExistingRecord = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsReadonlyForExistingRecord of Quick_UltraComboBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Private _IsMandatory As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Feb-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Property IsMandatory() As Boolean Implements IControlSetting.IsMandatory
    Get
      Try

        Return _IsMandatory

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsMandatory of Quick_UltraComboBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsMandatory = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsMandatory of Quick_UltraComboBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2009
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Overridable Property EntryMode() As EntryModes
    Get
      Return _EntryMode
    End Get
    Set(ByVal value As EntryModes)
      Try
        _EntryMode = value

        If _EntryMode = EntryModes.SelectionFromList Then
          Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
        Else
          Me.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
        End If

      Catch ex As Exception
        Throw New QuickLibrary.QuickException("Exception is set method of property EntryMode of Quick_UltraComboBox.", ex)
      End Try
    End Set
  End Property

#End Region

#Region "Methods"

  'Author: Faisal
  'Date Created(DD-MMM-YY): 22-Nov-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This method will check text enetered by user is in the populated list of 
  ''' items or not. It requires the column name in which it will search 
  ''' for the entered text. Entry mode should be set to user entry to make this 
  ''' method work.
  ''' </summary>
  Public Function IsTextInList(ByVal ColumnName As String) As QuickLibrary.Constants.MethodResult
    Try
      If Me.Rows.Band.Columns.Exists(ColumnName) Then
        If ColumnName.Trim <> String.Empty AndAlso EntryMode = EntryModes.EntrybyUser Then
          For I As Int32 = 0 To Me.Rows.Count - 1
            If Me.Rows(I).Cells(ColumnName).Text = Me.Text Then
              Me.Rows(I).Selected = True
              Return Constants.MethodResult.Yes
            End If
          Next
        ElseIf EntryMode = EntryModes.SelectionFromList Then
          If Me.SelectedRow IsNot Nothing AndAlso Me.SelectedRow.Index >= 0 Then
            Return Constants.MethodResult.Yes
          Else
            Return Constants.MethodResult.No
          End If
        End If
      Else
        Throw New Exception("Column name provided does not exist in the combo box.")
      End If

      Return Constants.MethodResult.No
    Catch ex As Exception
      Dim _qex As New QuickException("Exception in IsTextInList of Quick_UltraComboBox.", ex)
      Throw _qex
    End Try
  End Function

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Clears value of the control.
  ''' </summary>
  Public Function ClearControl() As QuickLibrary.Constants.MethodResult Implements IClearControl.ClearControl
    Try

      Me.ActiveRow = Nothing
      Me.SelectedRow = Nothing

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_UltraComboBox.", ex)
      Throw _qex
    End Try
  End Function
#End Region


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 24-Jan-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Resize Event method. Can be overriden by child if resizing logic is different from default.
  ''' </summary>
  Protected Overridable Sub Quick_UltraComboBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    Try
      Me.DropDownWidth = Me.Width

    Catch ex As Exception
      'Ignore all errors.
    End Try
  End Sub
End Class
