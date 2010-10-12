Public Class SharedSetting
  Public Shared QuickErpConnectionString As String = String.Empty

End Class

Namespace QuickCommonDataSetTableAdapters

  Public Class PartyTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try

      End Set
    End Property

#End Region

  End Class

  Partial Class CompanyTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try

      End Set
    End Property
#End Region

  End Class

  Partial Class SettingTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

          Me.Adapter.UpdateCommand.Transaction = value
          Me.Adapter.InsertCommand.Transaction = value
          Me.Adapter.DeleteCommand.Transaction = value
          'For Each _SqlCommand As SqlClient.SqlCommand In Me.CommandCollection
          '_SqlCommand.Transaction = value
          'Next

        Catch ex As Exception
          Throw ex
        End Try
      End Set
    End Property
#End Region

  End Class

  Public Class TransferTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try
      End Set
    End Property
#End Region

  End Class

  '  Public Class TransferPattern1TableAdapter
  '    Private SqlTransactionObject As SqlClient.SqlTransaction

  '#Region "ADO.NET Methods"
  '    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
  '      Get

  '        Return Me.Connection

  '      End Get
  '    End Property

  '    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
  '      Set(ByVal value As SqlClient.SqlConnection)

  '        Me.Connection = value

  '      End Set
  '    End Property

  '    Public Property SqlTransaction() As SqlClient.SqlTransaction
  '      Get
  '        Try

  '          Return SqlTransactionObject

  '        Catch ex As Exception
  '          Dim QuickExceptionObject As New QuickLibrary.QuickException("Exception to get active transaction", ex)
  '          Throw QuickExceptionObject
  '        End Try
  '      End Get
  '      Set(ByVal value As SqlClient.SqlTransaction)
  '        Try

  '          SqlTransactionObject = value

  '        Catch ex As Exception
  '          Dim QuickExceptionObject As New QuickLibrary.QuickException("Exception to set active transaction", ex)
  '          Throw QuickExceptionObject
  '        End Try

  '      End Set
  '    End Property
  '#End Region

  '  End Class

  '  Public Class TransferPattern2TableAdapter
  '    Private SqlTransactionObject As SqlClient.SqlTransaction

  '#Region "ADO.NET Methods"
  '    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
  '      Get

  '        Return Me.Connection

  '      End Get
  '    End Property

  '    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
  '      Set(ByVal value As SqlClient.SqlConnection)

  '        Me.Connection = value

  '      End Set
  '    End Property

  '    Public Property SqlTransaction() As SqlClient.SqlTransaction
  '      Get
  '        Try

  '          Return SqlTransactionObject

  '        Catch ex As Exception
  '          Dim QuickExceptionObject As New QuickLibrary.QuickException("Exception to get active transaction", ex)
  '          Throw QuickExceptionObject
  '        End Try
  '      End Get
  '      Set(ByVal value As SqlClient.SqlTransaction)
  '        Try

  '          SqlTransactionObject = value

  '        Catch ex As Exception
  '          Dim QuickExceptionObject As New QuickLibrary.QuickException("Exception to set active transaction", ex)
  '          Throw QuickExceptionObject
  '        End Try

  '      End Set
  '    End Property
  '#End Region

  '  End Class
End Namespace

Namespace QuickAccountingDataSetTableAdapters

  Public Class COATableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try
      End Set
    End Property
#End Region

  End Class

  Public Class VoucherTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try
      End Set
    End Property
#End Region

  End Class

  Public Class VoucherDetailTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try

      End Set
    End Property
#End Region

  End Class

  Public Class VoucherTypeTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try

      End Set
    End Property
#End Region

  End Class

End Namespace

Namespace QuickSecurityDataSetTableAdapters
  Public Class UserTableAdapter
    Private SqlTransactionObject As SqlClient.SqlTransaction

#Region "ADO.NET Methods"
    Public ReadOnly Property GetConnection() As SqlClient.SqlConnection
      Get

        Return Me.Connection

      End Get
    End Property

    Public WriteOnly Property SetConnection() As SqlClient.SqlConnection
      Set(ByVal value As SqlClient.SqlConnection)

        Me.Connection = value

      End Set
    End Property

    Public Property SqlTransaction() As SqlClient.SqlTransaction
      Get
        Try

          Return SqlTransactionObject

        Catch ex As Exception
          Throw ex
        End Try
      End Get
      Set(ByVal value As SqlClient.SqlTransaction)
        Try

          SqlTransactionObject = value

        Catch ex As Exception
          Throw ex
        End Try
      End Set
    End Property
#End Region

  End Class
End Namespace

Namespace QuickInventoryDataSetTableAdapters

End Namespace

Partial Public Class QuickCommonDataSet

  Partial Public Class PartyDataTable
    Public Function CheckBusinessRules() As Boolean
      Try
        Dim _Status As Boolean = True

        For I As Int32 = 0 To Me.Rows.Count - 1
          If Not Me(0).CheckBusniessRules() Then _Status = False
        Next

        Return _Status
      Catch ex As Exception
        Throw ex
      End Try
    End Function
  End Class

  Partial Public Class PartyRow
    Public BrokenBusinessRule As New LogicalDataSet.BusinessRuleDataTable

    Public Function CheckBusniessRules() As Boolean
      Try
        'Just used to get the names of columns
        Dim _PartyDataTable As New PartyDataTable

        BrokenBusinessRule.UpdateRuleStatus(_PartyDataTable.Co_IDColumn.ColumnName, _
          My.Resources.RuleNameCoIDGreaterThanZero, _
          My.Resources.RuleDescCoIDGreaterThanZero, _
          Me.Co_ID > 0)
        BrokenBusinessRule.UpdateRuleStatus(_PartyDataTable.Party_IDColumn.ColumnName, _
          My.Resources.RuleNamePartyIDGreaterThanZero, _
          My.Resources.RuleDescPartyIDGreaterThanZero, _
          Not Me.IsParty_CodeNull AndAlso Me.Party_ID > 0)
        BrokenBusinessRule.UpdateRuleStatus(_PartyDataTable.Party_CodeColumn.ColumnName, _
          My.Resources.RuleNamePartyCodeRequired, _
          My.Resources.RuleDescPartyCodeRequired, _
          Not Me.IsParty_CodeNull AndAlso Me.Party_Code.Length > 0)
        BrokenBusinessRule.UpdateRuleStatus(_PartyDataTable.Party_DescColumn.ColumnName, _
          My.Resources.RuleNamePartyNameRequired, _
          My.Resources.RuleDescPartyNameRequired, _
         Not Me.IsParty_DescNull AndAlso Me.Party_Desc.Trim.Length > 0)
        BrokenBusinessRule.UpdateRuleStatus(_PartyDataTable.Opening_DrColumn.ColumnName, _
          My.Resources.RuleNameOpeningDrAndCrMutuallyExlusive, _
          My.Resources.RuleDescOpeningDrAndCrMutuallyExlusive, _
          Me.IsOpening_CrNull OrElse Me.IsOpening_DrNull OrElse (Not (Me.Opening_Cr > 0 AndAlso Me.Opening_Dr > 0)))
        BrokenBusinessRule.UpdateRuleStatus(_PartyDataTable.EntityType_IDColumn.ColumnName, _
          My.Resources.RuleNameEntityTypeRequired, _
          My.Resources.RuleDescEntityTypeRequired, _
          Me.EntityType_ID > 0)

        _PartyDataTable = Nothing
        Return (BrokenBusinessRule.Count <= 0)

      Catch ex As Exception
        Throw ex
      End Try
    End Function
  End Class
End Class

Namespace My

  Partial Friend NotInheritable Class MySettings

    <Global.System.Configuration.ApplicationScopedSettingAttribute(), _
      Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
      Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString), _
      Global.System.Configuration.DefaultSettingValueAttribute("Data Source=.\SQLEXPRESS;Initial Catalog=Quick_ERP;Integrated Security=True")> _
    Public ReadOnly Property Quick_ERPConnectionString() As String
      Get
        If QuickDAL.SharedSetting.QuickErpConnectionString Is Nothing OrElse QuickDAL.SharedSetting.QuickErpConnectionString = String.Empty Then
          Return Configuration.ConfigurationManager.ConnectionStrings("Quick_Erp").ConnectionString
        Else
          Return QuickDAL.SharedSetting.QuickErpConnectionString
        End If
      End Get
    End Property
  End Class

End Namespace