Imports QuickLibrary
Imports QuickDALLibrary

Public Class ParentBasicForm
  Private _FormVersion As String
  Private _FormCode As String
  Private _LoginInfo As New LoginInfo

#Region "Properties"

  Private _SettingForm As QuickDAL.QuickCommonDataSet.SettingFormCompanyAssociationDataTable
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
  Protected ReadOnly Property SettingForm() As QuickDAL.QuickCommonDataSet.SettingFormCompanyAssociationDataTable
    Get
      Try

        If _SettingForm Is Nothing Then
          Dim _SettingFormCompanyAssociationTA As New QuickDAL.QuickCommonDataSetTableAdapters.SettingFormCompanyAssociationTableAdapter
          _SettingForm = _SettingFormCompanyAssociationTA.GetByCoIDFormCode(Me.LoginInfoObject.CompanyID, Me.FormCode)
        End If

        Return _SettingForm

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SettingForm of ParentBasicForm.", ex)
        Throw _qex
      End Try
    End Get
    'Set(ByVal value As QuickDAL.QuickCommonDataSet.SettingFormCompanyAssociationDataTable)
    '  Try

    '    _SettingForm = value

    '  Catch ex As Exception
    '    Dim _qex As New QuickExceptionAdvanced("Exception in SettingForm of ParentBasicForm.", ex)
    '    Throw _qex
    '  End Try
    'End Set
  End Property

  Private _SettingFormControls As QuickDAL.QuickCommonDataSet.SettingFormControlsCompanyAssociationDataTable
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
  Protected ReadOnly Property SettingFormControls() As QuickDAL.QuickCommonDataSet.SettingFormControlsCompanyAssociationDataTable
    Get
      Try

        If _SettingFormControls Is Nothing Then
          Dim _SettingFormControlsCompanyAssociationTA As New QuickDAL.QuickCommonDataSetTableAdapters.SettingFormControlsCompanyAssociationTableAdapter
          _SettingFormControls = _SettingFormControlsCompanyAssociationTA.GetByCoIDFormCode(Me.LoginInfoObject.CompanyID, Me.FormCode)
        End If

        Return _SettingFormControls

      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SettingFormControls of ParentBasicForm.", ex)
        Throw _qex
      End Try
    End Get
    'Set(ByVal value As QuickDAL.QuickCommonDataSet.SettingFormControlsCompanyAssociationDataTable)
    '  Try

    '    _SettingFormControls = value

    '  Catch ex As Exception
    '    Dim _qex As New QuickExceptionAdvanced("Exception in SettingFormControls of ParentBasicForm.", ex)
    '    Throw _qex
    '  End Try
    'End Set
  End Property

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2008
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Property FormVersion() As String
    Get
      Return _FormVersion
    End Get
    Set(ByVal value As String)
      _FormVersion = value
      FormVersionStatusBarLabel.Text = value
    End Set
  End Property


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 2008
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Public Property FormCode() As String
    Get
      Return _FormCode
    End Get
    Set(ByVal value As String)
      _FormCode = value
      FormIDStatusBarLabel.Text = value
    End Set
  End Property

  Public Property LoginInfoObject() As LoginInfo
    Get
      Return _LoginInfo
    End Get
    Set(ByVal value As LoginInfo)
      Try
        _LoginInfo = value

        If _LoginInfo IsNot Nothing Then
          _LoginInfo.FormOjbect = Me
        End If
      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in LoginInfoOjbect property set method.", ex)
        Throw _qex
      End Try
    End Set
  End Property
#End Region

#Region "Methods"
  Private Sub SetCommonControlProperties(ByVal _Control As Windows.Forms.Control)
    Try
      Dim _FormControlPermissionTA As New QuickDAL.QuickSecurityDataSetTableAdapters.FormControlPermissionTableAdapter
      Dim _FormControlPermissionTable As QuickDAL.QuickSecurityDataSet.FormControlPermissionDataTable

      'Setting security
      _FormControlPermissionTable = _FormControlPermissionTA.GetByCoIDRoleIDFormNameControlName(Me.LoginInfoObject.CompanyID, Me.LoginInfoObject.RoleID, Me.Name, _Control.Name)
      If _FormControlPermissionTable.Rows.Count > 0 Then
        _Control.Enabled = _FormControlPermissionTable(0).IsEnabled
      End If

      'Setting formats
      If TypeOf _Control Is QuickControls.Quick_UltraCalendarCombo Then
        With DirectCast(_Control, QuickControls.Quick_UltraCalendarCombo)
          .Format = DatabaseCache.GetSettingValue(Constants.SETTING_ID_FormatDateToDisplay)
        End With
      End If

      For I As Int32 = 0 To _Control.Controls.Count - 1
        'Check the controls contained by this control.
        SetCommonControlProperties(_Control.Controls(I))
      Next

    Catch ex As Exception
      Dim _QuickException As New QuickDALLibrary.QuickExceptionAdvanced("Exception in SetCommonControlProperties", ex)
      Throw _QuickException
    End Try
  End Sub
#End Region

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 7-Mar-11
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This event method will do the setting necessary such as captions and security.
  ''' </summary>
  Private Sub ParentBasicForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      SetCommonControlProperties(Me)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ParentBasicForm_Load of ParentBasicForm.", ex)
      Throw _qex
    End Try
  End Sub
End Class