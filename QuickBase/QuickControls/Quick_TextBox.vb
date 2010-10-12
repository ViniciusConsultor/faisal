Imports QuickLibrary


'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 2009
'***** Modification History *****
'Name   Date(DD-MMM-YY)   Description
'--------------------------------------------------------------------------------
'Faisal Saleem  27-Dec-09 DefaultValue property added and implemented interface
'                         IClearControl.
''' <summary>
''' This is custom control which is inherited from .net TextBox
''' </summary>

Public Class Quick_TextBox
  Implements IClearControl
  Implements IControlSetting

#Region "Declarations"
  Private _TextBoxType As TextBoxTypes = TextBoxTypes.Text
  Private _DefaultValue As String = String.Empty

  Public Enum TextBoxTypes
    Text
    IntegerNumberOrPercentageNumber
  End Enum
#End Region

#Region "Properties"
  Private _IsReadonlyForNewRecord As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 14-Feb-2010
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
        Dim _qex As New QuickException("Exception in IsReadonlyForNewRecord of Quick_TextBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsReadonlyForNewRecord = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsReadonlyForNewRecord of Quick_TextBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property


  Private _IsReadonlyForExistingRecord As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 14-Feb-2010
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
        Dim _qex As New QuickException("Exception in IsReadonlyForExistingRecord of Quick_TextBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsReadonlyForExistingRecord = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsReadonlyForExistingRecord of Quick_TextBox.", ex)
        Throw _qex
      End Try
    End Set
  End Property

  Private _IsMandatory As Boolean
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 14-Feb-2010
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
        Dim _qex As New QuickException("Exception in IsMandatory of Quick_TextBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Boolean)
      Try

        _IsMandatory = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in IsMandatory of Quick_TextBox.", ex)
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
  ''' 
  ''' </summary>
  Public Property TextBoxType() As TextBoxTypes
    Get
      Return _TextBoxType
    End Get
    Set(ByVal value As TextBoxTypes)
      _TextBoxType = value
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
  Public Property IntegerNumber() As Int32
    Get
      Try
        Dim _IntegerNumber As Int32 = 0
        Int32.TryParse(Me.Text, _IntegerNumber)

        Return _IntegerNumber
      Catch ex As Exception
        Dim _qex As New QuickException("Exception in Set method of IntegerNumber property.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Int32)
      Try
        Me.Text = value.ToString
      Catch ex As Exception
        Dim _qex As New QuickException("Exception in Set method of IntegerNumber property.", ex)
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
  Public Property PercentNumber() As Int32
    Get
      Try
        Dim _PercentNumber As Int32 = 0

        'Percent number will have % and the integer number, so length must be greater than 1 for valid percent number.
        If Me.Text.Length > 1 Then
          Int32.TryParse(Me.Text.Substring(0, Me.Text.Length - 1), _PercentNumber)
        End If

        Return _PercentNumber
      Catch ex As Exception
        Dim _qex As New QuickException("Exception in Set method of PercentNumber property.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As Int32)
      Try
        Me.Text = value.ToString
      Catch ex As Exception
        Dim _qex As New QuickException("Exception in Set method of PercentNumber property.", ex)
        Throw _qex
      End Try
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
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_TextBox.", ex)
        Throw _qex
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _DefaultValue = value

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in DefaultValue of Quick_TextBox.", ex)
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
    Me.Text = Me.DefaultValue
  End Sub

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

      Text = DefaultValue

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_Label.", ex)
      Throw _qex
    End Try
  End Function
#End Region

#Region "Event Methods"
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
  Private Sub Quick_TextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
    Try
      Me.SelectAll()

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in Quick_TextBox_Enter of Quick_TextBox.", ex)
      Throw _qex
    End Try

  End Sub
#End Region
End Class