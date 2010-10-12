
Namespace QuickInventoryDataSetTableAdapters

  Partial Public Class InventoryTableAdapter
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

    Public Function BeginTransaction() As Boolean
      Try

        SqlTransactionObject = GetConnection.BeginTransaction

        Return True

      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function CommitTransaction() As Boolean
      Try

        SqlTransaction.Commit()

        Return True

      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function RollbackTransaction() As Boolean
      Try

        SqlTransaction.Rollback()

        Return True

      Catch ex As Exception
        Throw ex
      End Try
    End Function
#End Region

  End Class

  Partial Public Class InventoryDetailTableAdapter
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

  Partial Public Class InventorySalesInvoiceTableAdapter
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

  Public Class ItemTableAdapter
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

  Public Class ItemSummaryTableAdapter
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

Partial Public Class QuickInventoryDataSet

  Partial Public Class InventoryDataTable
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

  Partial Public Class InventoryRow
    Public BrokenBusinessRule As New LogicalDataSet.BusinessRuleDataTable

    Public Function CheckBusniessRules() As Boolean
      Try
        'Just used to get the names of columns
        Dim _InventoryDataTable As New InventoryDataTable

        BrokenBusinessRule.UpdateRuleStatus(_InventoryDataTable.Co_IDColumn.ColumnName, My.Resources.RuleNameCoIDGreaterThanZero, My.Resources.RuleDescCoIDGreaterThanZero, Me.Co_ID < 1)

        _InventoryDataTable = Nothing

      Catch ex As Exception
        Throw ex
      End Try
    End Function
  End Class
End Class

Public Class Voucher

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 20-Jun-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  '''' <summary>
  '''' This will insert or update the voucher with given values.
  '''' </summary>
  'Private Function AssertVoucherForDocument(ByVal CompanyIDpara As Int32, ByVal DocumentTypeIDpara As Int32, ByVal DocumentIDpara As Int32, ByVal DocumentDatepara As DateTime, ByVal VoucherTypeIDpara As Int32, ByVal DocumentCoaIDpara As Int32, ByVal PartyCoaIDpara As Int32) As Boolean
  '  Try
  '    'Save the voucher
  '    Dim _VoucherTA As New QuickAccountingDataSetTableAdapters.VoucherTableAdapter
  '    Dim _VoucherTable As New QuickAccountingDataSet.VoucherDataTable
  '    Dim _VoucherDetailTable As New QuickAccountingDataSet.VoucherDetailDataTable
  '    Dim _VoucherRow As QuickAccountingDataSet.VoucherRow
  '    Dim _VoucherDetailRow As QuickAccountingDataSet.VoucherDetailRow
  '    Dim _VoucherID As Int32
  '    Dim _VoucherDetailID As Int16

  '    'Get vouchers posted for this invoice.
  '    _VoucherTable = _VoucherTA.GetByCoIDSourceIDSourceDocumentTypeID(CompanyIDpara, DocumentIDpara, DocumentTypeIDpara)
  '    'If voucher is already posted for this invoice.
  '    If _VoucherTable.Rows.Count > 0 Then
  '      For Each _VoucherRow In _VoucherTable.Rows
  '        If _VoucherRow.VoucherType_ID = VoucherTypeIDpara Then
  '          _VoucherRow.Voucher_Date = _InventoryDataRow.Inventory_Date
  '          _VoucherRow.Stamp_DateTime = Now
  '          _VoucherRow.Stamp_UserID = LoginInfoObject.UserID
  '          _VoucherRow.Remarks = "S. No. " & _InventoryDataRow.Inventory_No
  '          'Get voucher detail.
  '          _VoucherDetailTable = _VoucherDetailTA.GetByCoIDVoucherID(_VoucherRow.Co_ID, _VoucherRow.Voucher_ID)
  '          For Each _VoucherDetailRow In _VoucherDetailTable.Rows
  '            If _VoucherDetailRow.DebitAmount = 0 Then
  '              _VoucherDetailRow.CreditAmount = _SalesInvoiceAmountTotal
  '            Else
  '              _VoucherDetailRow.DebitAmount = _SalesInvoiceAmountTotal
  '            End If
  '            _VoucherDetailRow.Narration = "S. No. " & _InventoryDataRow.Inventory_No
  '            _VoucherDetailRow.Stamp_DateTime = Now
  '            _VoucherDetailRow.Stamp_User_Id = LoginInfoObject.UserID
  '          Next
  '        End If
  '      Next
  '      _VoucherTA.Update(_VoucherTable)
  '      _VoucherDetailTA.Update(_VoucherDetailTable)
  '    Else
  '      _VoucherID = Convert.ToInt32(_VoucherTA.GetNewVoucherIDByCoID(LoginInfoObject.CompanyID))
  '      _VoucherTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherTypeID, _VoucherID.ToString, _InventoryDataRow.Inventory_Date, DocumentStatuses.General_Posted, "S. No. " & _InventoryDataRow.Inventory_No, LoginInfoObject.UserID, Now, SalesInvoiceID, Convert.ToInt16(enuDocumentType.SalesInvoice), Nothing, Constants.DocumentStatuses.General_Posted)
  '      'Cash entry
  '      _VoucherDetailID = Convert.ToInt16(_VoucherDetailTA.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherID))
  '      _VoucherDetailTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherDetailID, _CashCoaId, "S. No. " & _InventoryDataRow.Inventory_No, _SalesInvoiceAmountTotal, 0, LoginInfoObject.UserID, Now, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, RecordStatuses.Inserted)
  '      'Sales entry
  '      _VoucherDetailID = Convert.ToInt16(_VoucherDetailTA.GetNewVoucherDetailIDByCoIDVoucherID(LoginInfoObject.CompanyID, _VoucherID))
  '      _VoucherDetailTA.Insert(LoginInfoObject.CompanyID, _VoucherID, _VoucherDetailID, _SalesCoaId, "S. No. " & _InventoryDataRow.Inventory_No, 0, _SalesInvoiceAmountTotal, LoginInfoObject.UserID, Now, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, RecordStatuses.Inserted)
  '    End If

  '  Catch ex As Exception
  '    Dim _qex As New Exception("Exception in AssertVoucherForDocument of Voucher class.", ex)
  '    Throw _qex
  '  End Try
  'End Function
End Class
