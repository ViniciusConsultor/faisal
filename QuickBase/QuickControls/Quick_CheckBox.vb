
'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2009
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'
''' <summary>
''' Quick_CheckBox custom control, it is inherited from .net checkbox.
''' </summary>
Public Class Quick_CheckBox
  Implements IClearControl

#Region "Declarations"
  Private _DefaultValue As Boolean = False

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
  Public Property DefaultValue() As Boolean
    Get
      Try

        Return _DefaultValue

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_CheckBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _DefaultValue = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_CheckBox.", ex)
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
  End Sub

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

      Me.Checked = DefaultValue

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_Label.", ex)
      Throw _qex
    End Try
  End Function
#End Region

#Region "Event Methods"

#End Region
End Class

