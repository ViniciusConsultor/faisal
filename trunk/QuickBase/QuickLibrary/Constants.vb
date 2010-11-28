Public Class Constants

  Public Const SQL_EX_LOGIN_FAILED_FOR_USER As Int32 = 18456
  Public Const DELETE_ROW_TAG_TEXT As String = "RowDeleted"

#Region "Configuration Related"
  Public Const CONFIG_FILE_NAME As String = "quick.cfg"
  Public Const CONFIG_KEY_APPLICATION_USER_NAME As String = "ApplicationUserName"
  Public Const CONFIG_KEY_APPLICATION_USER_PASSWORD As String = "ApplicationPassword"
  Public Const CONFIG_KEY_CONNECTION_STRING As String = "ConnectionString"
#End Region

#Region "Path Related"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 12-Oct-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' This will return the path to store and read configuration path, \ will be 
  ''' the last character of path string.
  ''' </summary>
  Public Shared ReadOnly Property ConfigurationFilePath() As String
    Get
      Try

        Return My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\"

      Catch ex As Exception
        Dim _qex As New QuickException("Exception in ConfigurationFilePath of Constants.", ex)
        Throw _qex
      End Try
    End Get
  End Property


#End Region

#Region "Item Related"
  'Public Const ITEM_SIZE_01 As String = "1"
  'Public Const ITEM_SIZE_02 As String = "2"
  'Public Const ITEM_SIZE_03 As String = "3"
  'Public Const ITEM_SIZE_04 As String = "4"
  'Public Const ITEM_SIZE_05 As String = "5"
  'Public Const ITEM_SIZE_06 As String = "6"
  'Public Const ITEM_SIZE_07 As String = "7"
  'Public Const ITEM_SIZE_08 As String = "8"
  'Public Const ITEM_SIZE_09 As String = "9"
  'Public Const ITEM_SIZE_10 As String = "10"
  'Public Const ITEM_SIZE_11 As String = "11"
  'Public Const ITEM_SIZE_12 As String = "12"
  'Public Const ITEM_SIZE_13 As String = "13"
  Public Const ITEM_SIZE_01_ALIAS As String = "100"
  Public Const ITEM_SIZE_02_ALIAS As String = "110"
  Public Const ITEM_SIZE_03_ALIAS As String = "120"
  Public Const ITEM_SIZE_04_ALIAS As String = "130"
  Public Const ITEM_SIZE_05_ALIAS As String = "140"
  Public Const ITEM_SIZE_06_ALIAS As String = "150"
  Public Const ITEM_SIZE_07_ALIAS As String = "160"
  Public Const ITEM_SIZE_08_ALIAS As String = "170"
  Public Const ITEM_SIZE_09_ALIAS As String = "180"
  Public Const ITEM_SIZE_10_ALIAS As String = "190"
  Public Const ITEM_SIZE_11_ALIAS As String = "CM"
  Public Const ITEM_SIZE_12_ALIAS As String = "200"
  Public Const ITEM_SIZE_13_ALIAS As String = "210"

  Public Const ITEM_LEVELING_SEPERATOR As String = "-"
  Public Const ITEM_MULTIPLE_COLUMNS As Boolean = True
#End Region

#Region "Enumerations"

  Public Enum SysContraintStatus
    PrimaryKey = 1
    UniqueKey = 2
    ForeignKey = 3
    Check = 4
    DefaultConstraint = 5
    ColumnLevel = 16
    TableLevel = 32
  End Enum

  Public Enum EntityTypes
    SalesMan = 1
    Customer
    Supplier
    CustomerAndSupplier
    CustomerCategory
    PurchaseSource
    Vender
  End Enum

  Public Enum MethodResult
    Success
    Failed
    PartialSucceded
    Yes
    No
  End Enum

  Public Enum RecordStatuses
    Inserted = 1
    Updated = 2
    Deleted = 4
  End Enum

  Public Enum DocumentStatuses
    Transfer_Completed = 1
    Transfer_InCompleteDueToException = 2
    Transfer_InComplete = 3
    General_Inserted = 5
    General_Unposted = 6
    General_Posted = 7
    Alert_NotSent = 8
    Alert_Send = 9
  End Enum

  Public Enum AlertTypes
    Email
    SMS
  End Enum

  'These values must match with the values of database.
  Public Enum enuDocumentType As Short
    SalesInvoice = 1
    SalesInvoiceReturn = 2
    Purchase = 3
    PurchaseReturn = 4
    Item = 5
    Party = 6
    Company = 7
    User = 8
    COA = 9
    VoucherType = 10
    ApplicationSetting = 11
    PaymentVoucher = 12
    ReceiptVoucher = 13
    PurchaseOrder = 14
    Stock = 15
    Purchased = 16
    Sold = 17
    PurchaseWarehouse = 18
    ProductionOrder = 19
  End Enum

  Public Enum enuCommuncationTypes As Short
    PhoneNumber = 1
  End Enum

  Public Enum enuAddressTypes As Short
    PrimaryAddress
  End Enum

#End Region

#Region "Setting Related"
  Public Const SETTING_ID_SEPERATOR As String = "."

  Public Const SETTING_ID_DisplayRecordOperationMessage As String = "DisplayRecordOperationMessage"
  Public Const SETTING_ID_DisplaySaveRecordMessage As String = "DisplaySaveRecordMessage.DisplayRecordOperationMessage"
  Public Const SETTING_ID_DisplayDeleteRecordMessage As String = "DisplayDeleteRecordMessage.DisplayRecordOperationMessage"
  Public Const SETTING_ID_ModificationAllowedPrefix As String = "ModificationAllowed"

  Public Const SETTING_ID_DefaultWindowState As String = "DefaultWindowState"
  Public Const SETTING_ID_DefaultWarehouseID As String = "DefaultWarehouseID"

  Public Const SETTING_ID_ReceiptVoucherType As String = "ReceiptVoucherType"
  Public Const SETTING_ID_PaymentVoucherType As String = "PaymentVoucherType"
  Public Const SETTING_ID_SalesInvoiceVoucherType As String = "SalesInvoiceVoucherType"
  Public Const SETTING_ID_DbVersion As String = "DBVersion"
  Public Const SETTING_ID_SalesInvoiceEntityTypes As String = "SalesInvoiceEntityTypes"

  Public Const SETTING_ID_TOOLBAR_BUTTON_DISPLAY_STYLE As String = "ToolbarButtonDisplayStyle"
  Public Const SETTING_ID_TOOLBAR_BUTTON_DISPLAY_STYLE_CANCEL As String = "ToolbarButtonDisplayStyle" & SETTING_ID_SEPERATOR & "Cancel"
  Public Const SETTING_ID_TOOLBAR_BUTTON_DISPLAY_STYLE_MOVE_FIRST As String = "ToolbarButtonDisplayStyle" & SETTING_ID_SEPERATOR & "MoveFirst"
  Public Const SETTING_ID_TOOLBAR_BUTTON_DISPLAY_STYLE_MOVE_PREVIOUS As String = "ToolbarButtonDisplayStyle" & SETTING_ID_SEPERATOR & "MovePrevious"

  Public Const SETTING_ID_ITEM_DESC_LEVEL As String = "ItemDescLevel"
  Public Const SETTING_ID_DBColumnCaption As String = "DBColumnCaption"
  'COA related
  Public Const SETTING_ID_CashCoaId As String = "CashCoaId"
  Public Const SETTING_ID_SalesCoaId As String = "SalesCoaId"

  Public Const SETTING_DESC_DbVersion As String = "Database Version"

  Public Const SETTING_VALUE_TRUE As String = "yes"
  Public Const SETTING_VALUE_FALSE As String = "no"
  Public Const SETTING_VALUE_SHOW_IMAGE_ONLY As String = "showimageonly"
  Public Const SETTING_VALUE_SHOW_TEXT_ONLY As String = "showtextonly"
  Public Const SETTING_VALUE_SHOW_IMAGE_AND_TEXT As String = "showimageandtext"

  Public Const SETTING_VALUE_DATATYPE_STRING As String = "String"
  Public Const SETTING_VALUE_DATATYPE_INTEGER As String = "Integer"
  Public Const SETTING_VALUE_DATATYPE_RANGE As String = "Range"

  Public Const SETTING_ID_FormatDateToDisplay As String = "FormatDateToDisplay"
  Public Const SETTING_ID_FormatDateForInput As String = "FormatDateForInput"
  Public Const SETTING_ID_DocumentNoFormat_SalesInvoice As String = "DocumentNoFormat" & SETTING_ID_SEPERATOR & "SalesInvoice"
  Public Const SETTING_ID_DocumentNoFormat_ProductionOrder As String = "DocumentNoFormat" & SETTING_ID_SEPERATOR & "ProductionOrder"

  Public Const SETTING_ID_EMAIL_AREA_MANAGER As String = "EmailAddressAreaManager"

  Public Const SETTING_ID_ITEMSUMMARY_STOCK_CAPTION As String = "ItemSummaryStockCaption"
  Public Const SETTING_ID_ITEMSUMMARY_PURCHASE_CAPTION As String = "ItemSummaryPurchaseCaption"
  Public Const SETTING_ID_ITEMSUMMARY_SALES_CAPTION As String = "ItemSummarySalesCaption"

  Public Const SETTING_ID_ReportFooterText As String = "ReportFooterText"

#Region "User input related"
  Public Const SETTING_ID_UserInput_ItemSize01 As String = "UserInput_ItemSize01"
  Public Const SETTING_ID_UserInput_ItemSize02 As String = "UserInput_ItemSize02"
  Public Const SETTING_ID_UserInput_ItemSize03 As String = "UserInput_ItemSize03"
  Public Const SETTING_ID_UserInput_ItemSize04 As String = "UserInput_ItemSize04"
  Public Const SETTING_ID_UserInput_ItemSize05 As String = "UserInput_ItemSize05"
  Public Const SETTING_ID_UserInput_ItemSize06 As String = "UserInput_ItemSize06"
  Public Const SETTING_ID_UserInput_ItemSize07 As String = "UserInput_ItemSize07"
  Public Const SETTING_ID_UserInput_ItemSize08 As String = "UserInput_ItemSize08"
  Public Const SETTING_ID_UserInput_ItemSize09 As String = "UserInput_ItemSize09"
  Public Const SETTING_ID_UserInput_ItemSize10 As String = "UserInput_ItemSize10"
  Public Const SETTING_ID_UserInput_ItemSize11 As String = "UserInput_ItemSize11"
  Public Const SETTING_ID_UserInput_ItemSize12 As String = "UserInput_ItemSize12"
  Public Const SETTING_ID_UserInput_ItemSize13 As String = "UserInput_ItemSize13"

  Public Const SETTING_ID_Mask_ItemCode As String = "Mask_ItemCode"
  Public Const SETTING_ID_Mask_COACode As String = "Mask_COACode"
#End Region

#End Region

#Region "DB Related"
  'Public Const TABLE_NAME_BASE_SETTING As String = "Base_Setting"
  'Public Const TABLE_NAME_SALES_INVOICE As String = "Inv_SalesInvoice"
  'Public Const TABLE_NAME_SALES_INVOICE_DETAIL As String = "Inv_SalesInvoice_Detail"
  Public Const OBJECTTYPE_USER_TABLE As String = "U"
#End Region

#Region "Date Time Related"
  Public Const FORMAT_DATE_FOR_USER As String = "dd-MM-yy"
  Public Const FORMAT_DATE_FOR_REPORT As String = "dd-MMM-yy"
  Public Const FORMAT_DATETIME_FOR_USER As String = "dd-MMM-yy hh:mm:ss p"
#End Region

#Region "COA"

#End Region

#Region "GUI Related"
  Public Const BORDER_GAP_WIDTH As Int32 = 5
  Public Const BORDER_GAP_HEIGHT As Int32 = 5
  Public Const SCROLLBAR_WIDTH As Int32 = 18
  Public Const QTY_CELL_WIDTH As Int32 = 35
  Public Const QTY_TOTAL_CELL_WIDTH As Int32 = 50
  Public Const AMOUNT_CELL_WIDTH As Int32 = 50
  Public Const AMOUNT_TOTAL_CELL_WIDTH As Int32 = 60
  Public Const ITEM_DESC_CELL_WIDTH As Int32 = 70
  Public Const ITEM_CODE_CELL_WIDTH As Int32 = 30
  Public Const ITEM_CODECOMPLETE_CELL_WIDTH As Int32 = 40
  Public Const CO_CODE_CELL_WIDTH As Int32 = 30
#End Region

#Region "Color Related"
  Shared ReadOnly Property ForeColorQtyNegative() As Drawing.Color
    Get
      Return Drawing.Color.Red
    End Get
  End Property

  Shared ReadOnly Property ForeColorQtyZero() As Drawing.Color
    Get
      Return Drawing.Color.LightGray
    End Get
  End Property

  Shared ReadOnly Property ForeColorQtyPositive() As Drawing.Color
    Get
      Return Drawing.Color.Black
    End Get
  End Property

  Shared ReadOnly Property BackColorQtyNegative() As Drawing.Color
    Get
      Return Drawing.Color.White
    End Get
  End Property

  Shared ReadOnly Property BackColorQtyZero() As Drawing.Color
    Get
      Return Drawing.Color.White
    End Get
  End Property

  Shared ReadOnly Property BackColorQtyPositive() As Drawing.Color
    Get
      Return Drawing.Color.White
    End Get
  End Property

  Shared ReadOnly Property BackColorTotalRow() As Drawing.Color
    Get
      Return Drawing.Color.Bisque
    End Get
  End Property

  Shared ReadOnly Property BackColorGrandTotalRow() As Drawing.Color
    Get
      Return Drawing.Color.BurlyWood
    End Get
  End Property

#End Region

End Class