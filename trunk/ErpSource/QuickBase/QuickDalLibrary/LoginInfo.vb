Imports QuickLibrary

Public Class LoginInfo
  Implements ICloneable

  Private _UserID As Int32 = 0
  Private _UserName As String = String.Empty
  Private _CompanyID As Int16 = 0
  Private _CompanyDesc As String = String.Empty
  Private _DatabaseVersion As String = String.Empty
  Private _DatabaseServer As String = String.Empty
  Private _DatabaseName As String = String.Empty
  Private _ParentCompanyID As Int16 = 0
  Private _IsAdmin As Boolean = False
  Private _FormObject As System.Windows.Forms.Form = Nothing

  Public Property UserID() As Int32
    Get
      Try

        Return _UserID

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to get user id", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As Int32)
      Try

        _UserID = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to set user id", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property UserName() As String
    Get
      Try

        Return _UserName

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to get user name", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _UserName = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to set user name", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property CompanyID() As Int16
    Get
      Try

        Return _CompanyID

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to get company id", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As Int16)
      Try

        _CompanyID = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to set company id", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property CompanyDesc() As String
    Get
      Try

        Return _CompanyDesc

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to get company name", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _CompanyDesc = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to set company name", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property DatabaseServerName() As String
    Get
      Try

        Return _DatabaseServer

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to get database server name", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _DatabaseServer = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to set database server name", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property DatabaseName() As String
    Get
      Try

        Return _DatabaseName

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to get database name", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As String)
      Try

        _DatabaseName = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception to set database name", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property FormOjbect() As System.Windows.Forms.Form
    Get
      Return _FormObject
    End Get
    Set(ByVal value As System.Windows.Forms.Form)
      Try
        _FormObject = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickExceptionAdvanced("Exception in FormOjbect property set method.", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property ParentCompanyID() As Int16
    Get
      Try

        Return _ParentCompanyID

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in get method of ParentCompanyID property.", ex)
        Throw QuickExceptionObject
      End Try
    End Get
    Set(ByVal value As Int16)
      Try

        _ParentCompanyID = value

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in get method of ParentCompanyID property.", ex)
        Throw QuickExceptionObject
      End Try
    End Set
  End Property

  Public Property IsAdmin() As Boolean
    Get
      Return _IsAdmin
    End Get
    Set(ByVal value As Boolean)
      _IsAdmin = value
    End Set
  End Property

  Public Function Clone() As Object Implements System.ICloneable.Clone
    Return MemberwiseClone()
  End Function
End Class
