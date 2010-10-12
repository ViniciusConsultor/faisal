'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 27-Dec-09
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'Faisal Saleem  27-Dec-09 Added DefaultValue property, implmented IClearControl
'                         interface.
''' <summary>
''' Description of the class goes here ...
''' </summary>
Public Class Quick_Label
  Implements IClearControl

#Region "Declarations"
  Private _AllowClearValue As Boolean = False
  Private _DefaultValue As String = String.Empty
#End Region

#Region "Properties"
  Public Property AllowClearValue() As Boolean
    Get
      Return _AllowClearValue
    End Get
    Set(ByVal value As Boolean)
      _AllowClearValue = value
    End Set
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 27-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Set default value, it will be set whenever this control is cleared.
  ''' </summary>
  Public Property DefaultValue() As String
    Get
      Try

        Return _DefaultValue

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_Label.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _DefaultValue = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_Label.", ex)
        Throw _qex
      End Try
    End Set
  End Property
#End Region

#Region "Methods"

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

      If AllowClearValue Then
        Text = DefaultValue
      End If

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_Label.", ex)
      Throw _qex
    End Try
  End Function

#End Region

#Region "Event Methods"

#End Region

End Class

