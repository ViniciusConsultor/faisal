'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2009
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'Faisal Saleem  27-Dec-09 Added DefaultValue property, implmented IClearControl
'                         interface.
''' <summary>
''' It is inherited from UltraCalendarCombo. DateTime.MinValue will clear the 
''' control.
''' </summary>
Public Class Quick_UltraCalendarCombo
  Implements IClearControl

#Region "Declarations"
  Private _DefaultValue As DateTime = Now
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
  Public Property DefaultValue() As DateTime
    Get
      Try

        Return _DefaultValue

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_UltraCalendarCombo.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As DateTime)
      Try

        _DefaultValue = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_UltraCalendarCombo.", ex)
        Throw _qex
      End Try
    End Set
  End Property

#End Region

#Region "Methods"
  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.Format = QuickLibrary.Constants.FORMAT_DATE_FOR_USER
  End Sub

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Clears value of the control
  ''' </summary>
  Public Function ClearControl() As QuickLibrary.Constants.MethodResult Implements IClearControl.ClearControl
    Try

      Me.Value = DefaultValue

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_Label.", ex)
      Throw _qex
    End Try
  End Function
#End Region

#Region "Event Methods"

#End Region

End Class
