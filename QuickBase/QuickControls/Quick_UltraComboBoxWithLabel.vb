'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 26-Dec-09
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'
''' <summary>
''' This control will show a UltraComboBox and a optional label. User can choose
''' if the label should be shown. This label can display a different value than
''' the default shown in the ComboBox textbox.
''' </summary>
Public Class Quick_UltraComboBoxWithLabel
  Implements IClearControl

#Region "Declarations"
  Private _ColumnNameForLabelDisplay As String = String.Empty
  Private _ComboBoxWidthPercentage As Int32 = 30

#End Region

#Region "Properties"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This is the column name of combo box who's value will be displayed in label.
  ''' </summary>
  Public Property ColumnNameForLabelDisplay() As String
    Get
      Try

        Return _ColumnNameForLabelDisplay

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ColumnNameForLabelDisplay of Quick_UltraComboBoxWithLabel.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _ColumnNameForLabelDisplay = value
        Me.Quick_UltraComboBoxWithLabel_Resize(Nothing, Nothing)

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ColumnNameForLabelDisplay of Quick_UltraComboBoxWithLabel.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Description of the property goes here ...
  ''' </summary>
  Public Property ComboBoxWidthPercentage() As Int32
    Get
      Try

        Return _ComboBoxWidthPercentage

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ComboBoxWidthPercentage of Quick_UltraComboBoxWithLabel.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Int32)
      Try

        If value < 2 OrElse value > 100 Then
          Throw New Exception("ComboBoxWidthPercentage valid range is 2 to 100.")
        Else
          _ComboBoxWidthPercentage = value
        End If

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ComboBoxWidthPercentage of Quick_UltraComboBoxWithLabel.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Private _ControlRows As Short = 1
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 6-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It reflects height of control. Value one means that control has height of 
  ''' one combobox.
  ''' </summary>
  Protected Property ControlRows() As Short
    Get
      Try

        Return _ControlRows

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ControlRows of Quick_UltraComboBoxWithLabel.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Short)
      Try

        If value >= 1 AndAlso value <= 5 Then
          _ControlRows = value
          Me.Quick_UltraComboBoxWithLabel_Resize(Me, Nothing)
        Else
          Throw New QuickException("Value """ & value.ToString & """ is not valid ControlRows.", Nothing)
        End If

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ControlRows of Quick_UltraComboBoxWithLabel.", ex)
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
    Me.Quick_Label1.AllowClearValue = True
  End Sub


  'Author: Faisal 
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Description of the method goes here ...
  ''' </summary>
  Private Function UpdateLabelValue() As String
    Try

      If Me.Quick_UltraComboBox1.SelectedRow IsNot Nothing And Me.Quick_UltraComboBox1.Rows.Band.Columns.Exists(Me.ColumnNameForLabelDisplay) Then
        Me.Quick_Label1.Text = Me.Quick_UltraComboBox1.SelectedRow.Cells(Me.ColumnNameForLabelDisplay).Text
      End If
      Return Me.Quick_Label1.Text

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in UpdateLabelValue of Quick_UltraComboBoxWithLabel.", ex)
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

      Me.Quick_Label1.ClearControl()
      Me.Quick_UltraComboBox1.clearcontrol()

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_UltraComboBoxWithLabel.", ex)
      Throw _qex
    End Try
  End Function
#End Region

#Region "Event Methods"

  'Author: Faisal
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  Private Sub Quick_UltraComboBoxWithLabel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    Try

      Me.Height = Me.Quick_UltraComboBox1.Height * Me.ControlRows
      If Me.ColumnNameForLabelDisplay = String.Empty Then
        Me.Quick_UltraComboBox1.Width = Me.Width
        Me.Quick_Label1.Visible = False
      Else
        Me.Quick_UltraComboBox1.Width = Convert.ToInt32(Me.Width * Me.ComboBoxWidthPercentage / 100)
        Me.Quick_Label1.Width = Convert.ToInt32(Me.Width * (100 - Me.ComboBoxWidthPercentage) / 100)
        Me.Quick_Label1.Visible = True
        Me.Quick_Label1.Left = Me.Quick_UltraComboBox1.Width
        Me.Quick_Label1.Top = 0 ': Me.Quick_UltraComboBox1.Height = Me.Height
      End If

    Catch ex As Exception
      'Dim _qex As New QuickException("Exception in Quick_UltraComboBoxWithLabel_Resize of Quick_UltraComboBoxWithLabel.", ex)
      'Throw _qex

      'Ignore the errors because they will be due to negative size due to resize of form.
    End Try
  End Sub

  'Author: Faisal
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  Private Sub Quick_UltraComboBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Quick_UltraComboBox1.Leave
    Try

      UpdateLabelValue()

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in Quick_UltraComboBox1_Leave of Quick_UltraComboBoxWithLabel.", ex)
      Throw _qex
    End Try
  End Sub

  'Author: Faisal
  'Date Created(DD-MMM-YY): 26-Dec-09
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  Private Sub Quick_UltraComboBox1_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles Quick_UltraComboBox1.RowSelected
    Try

      UpdateLabelValue()

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in Quick_UltraComboBox1_RowSelected event method of Quick_UltraComboBoxWithLabel.", ex)
      Throw _qex
    End Try

  End Sub
#End Region

End Class

