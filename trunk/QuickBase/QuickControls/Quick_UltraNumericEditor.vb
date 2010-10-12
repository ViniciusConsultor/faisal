
'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2009
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'Faisal Saleem  27-Dec-09 DefaultValue property added and implemented interface
'                         IClearControl.
''' <summary>
''' This is custom control which is inherited from UltraNumericEditor
''' </summary>
Public Class Quick_UltraNumericEditor
  Implements IClearControl

#Region "Declarations"
  Private _DefaultValue As Decimal = 0

#End Region

#Region "Properties"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Set default value, it will be set whenever this control is cleared.
  ''' </summary>
  Public Property DefaultValue() As Decimal
    Get
      Try

        Return _DefaultValue

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_UltraNumericEditor.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Decimal)
      Try

        _DefaultValue = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_UltraNumericEditor.", ex)
        Throw _qex
      End Try
    End Set
  End Property

#End Region

#Region "Methods"

#End Region

#Region "Event Methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Description of the method goes here ...
  ''' </summary>
  Public Function ClearControl() As QuickLibrary.Constants.MethodResult Implements IClearControl.ClearControl
    Try

      Me.Value = DefaultValue

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_UltraNumericEditor.", ex)
      Throw _qex
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Jun-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event method code will select the text when focus is on this control.
  ''' </summary>
  Private Sub Quick_UltraNumericEditor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
    Try
      Me.SelectAll()

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in Quick_UltraNumericEditor_Enter of Quick_UltraNumericEditor.", ex)
      Throw _qex
    End Try

  End Sub
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextControl
    Me.PromptChar = Nothing
  End Sub

End Class