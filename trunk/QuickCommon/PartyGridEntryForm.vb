Imports QuickLibrary
Imports QuickDalLibrary
Imports QuickDAL.QuickCommonDataSet

Public Class PartyGridEntryForm

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.TableName = "Party"
  End Sub

  Private Sub PartyGridEntryForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      Dim _PartyDataTable As New PartyDataTable

      For Each _Column As FarPoint.Win.Spread.Column In Me.Quick_Spread1.ActiveSheet.Columns
        Select Case _Column.DataField
          Case _PartyDataTable.AddressColumn.ColumnName
            _Column.Label = "Address"
          Case _PartyDataTable.CityColumn.ColumnName
            _Column.Label = "City"
          Case _PartyDataTable.Co_IDColumn.ColumnName
            _Column.Label = "Company ID"
            _Column.Visible = False
          Case _PartyDataTable.COA_IDColumn.ColumnName
            _Column.Label = "COA ID"
          Case _PartyDataTable.EmailColumn.ColumnName
            _Column.Label = "Email"
          Case _PartyDataTable.FaxColumn.ColumnName
            _Column.Label = "Fax"
          Case _PartyDataTable.Inactive_FromColumn.ColumnName
            _Column.Label = "Inactive From"
          Case _PartyDataTable.Inactive_ToColumn.ColumnName
            _Column.Label = "Inactive To"
          Case _PartyDataTable.Opening_CrColumn.ColumnName
            _Column.Label = "Opening Cr"
          Case _PartyDataTable.Opening_DrColumn.ColumnName
            _Column.Label = "Opening Dr"
          Case _PartyDataTable.Party_DescColumn.ColumnName
            _Column.Label = "Party Name"
          Case _PartyDataTable.Party_IDColumn.ColumnName
            _Column.Label = "Party ID"
          Case _PartyDataTable.PhoneColumn.ColumnName
            _Column.Label = "Phone"
          Case _PartyDataTable.ZipCodeColumn.ColumnName
            _Column.Label = "Postal Code"
        End Select
      Next
    Catch ex As Exception
      Dim ExceptionObject As New QuickExceptionAdvanced("Exception in setting column names", ex)
      ExceptionObject.Show(Me.LoginInfoObject)
    End Try
  End Sub
End Class