'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 17-Apr-11
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This custom color lets user select color.
''' </summary>
Public Class Quick_ColorControl

#Region "Declarations"

#End Region

#Region "Properties"

  Private _SelectedColor As Drawing.Color
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 17-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This property gets/sets the color of the control.
  ''' </summary>
  Private Property SelectedColor() As Drawing.Color
    Get
      Try

        Return _SelectedColor

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in SelectedColor of Quick_ColorControl.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Drawing.Color)
      Try

        _SelectedColor = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in SelectedColor of Quick_ColorControl.", ex)
        Throw _qex
      End Try
    End Set
  End Property

#End Region

#Region "Methods"

#End Region

#Region "Event Methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 17-Apr-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event method will show color dialog to select color.
  ''' </summary>
  Private Sub Quick_ColorControl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
    Try

      Me.ColorDialog1.FullOpen = True
      Me.ColorDialog1.Color = Me.BackColor
      Me.ColorDialog1.ShowDialog()
      Me.BackColor = Me.ColorDialog1.Color

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in SubName of ClassName/FormName.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

End Class
