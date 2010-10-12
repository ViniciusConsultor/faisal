Imports System.Windows.Forms
Imports QuickDAL
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
'Imports QuickDAL.QuickAccountingDataSet
'Imports QuickDAL.QuickAccountingDataSetTableAdapters
Imports QuickDalLibrary
Imports QuickLibrary
Imports QuickLibrary.Constants
Imports QuickLibrary.Common
Imports System.Drawing

Public Class SearchCoa

#Region "Declarations"
  Private _CoaAccountingTableAdapter As New QuickAccountingDataSetTableAdapters.COATableAdapter
  Private _CoaDataTable As New QuickAccountingDataSet.COADataTable
  Private _SearchResultUnTypedDataTable As QuickAccountingDataSet.COADataTable
  Private _SearchResultUnTypedDataRow() As DataRow
  Private _ActiveRowNo As Integer = 0
  Private _LastSearcingText As String = String.Empty

  Private Enum COAEnum
    Co_ID
    COA_ID
    COA_Code
    COA_Desc
  End Enum


#End Region

#Region "Properties"
  Public ReadOnly Property SearchResultUnTypedDataTable() As DataTable
    Get
      Try
        Return _SearchResultUnTypedDataTable
      Catch ex As Exception
        Dim _qex As New QuickExceptionAdvanced("Exception in SearchResultUnTypedDataTable of SearchForm.", ex)
        Throw _qex
      End Try
    End Get
  End Property

#End Region

#Region "Methods"

#End Region

#Region "Event Methods"

#End Region


  Private Sub SearchCoa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Try
      Me._CoaDataTable = Me._CoaAccountingTableAdapter.GetByCoID(1)
      Me.CoaSpreadSheet_Sheet1.DataSource = Me._CoaDataTable

      For I As Int32 = 0 To Me.CoaSpreadSheet_Sheet1.ColumnCount - 1
        'If Me.CoaSpreadSheet_Sheet1.Columns(I).Index = 2 OrElse Me.CoaSpreadSheet_Sheet1.Columns(I).Index = 3 Then
        If Me.CoaSpreadSheet_Sheet1.Columns(I).Index = COAEnum.COA_Code OrElse Me.CoaSpreadSheet_Sheet1.Columns(I).Index = COAEnum.COA_Desc Then

          Me.CoaSpreadSheet_Sheet1.Columns(COAEnum.COA_Code).Visible = True
          Me.CoaSpreadSheet_Sheet1.Columns(COAEnum.COA_Desc).Visible = True
          Me.CoaSpreadSheet_Sheet1.Columns(COAEnum.COA_Desc).Width = QTY_CELL_WIDTH + 170
          Me.CoaSpreadSheet_Sheet1.Columns(COAEnum.COA_Code).Width = QTY_CELL_WIDTH + 50
        Else
          Me.CoaSpreadSheet_Sheet1.Columns(I).Visible = False
        End If
      Next
      If Me.CoaSpreadSheet.ActiveSheet.Rows.Count > 0 Then
        Me.CoaSpreadSheet.ActiveSheet.ActiveRow.BackColor = Color.Beige
      End If

      'Me.VoucherDetailQuickSpread.ActiveSheet.Columns(5).ForeColor = Color.Red


    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Load form of Look up Form.", ex)
      Throw _qex
    End Try


 
  End Sub


  Private Sub GoCoaSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoCoaSearch.Click
    Try


      Me._CoaDataTable = Me._CoaAccountingTableAdapter.GetAllLikeCoaCodeCoaDesc("%" & Me.CoaSearchTextBox.Text & "%", "%" & Me.CoaSearchTextBox.Text & "%", 1)
      Me.CoaSpreadSheet_Sheet1.DataSource = Me._CoaDataTable

      For I As Int32 = 0 To Me.CoaSpreadSheet_Sheet1.ColumnCount - 1
        If Me.CoaSpreadSheet_Sheet1.Columns(I).Index = COAEnum.COA_Code OrElse Me.CoaSpreadSheet_Sheet1.Columns(I).Index = COAEnum.COA_Desc Then

          Me.CoaSpreadSheet_Sheet1.Columns(2).Visible = True
          Me.CoaSpreadSheet_Sheet1.Columns(3).Visible = True
          Me.CoaSpreadSheet_Sheet1.Columns(3).Width = 100
        Else
          Me.CoaSpreadSheet_Sheet1.Columns(I).Visible = False
        End If
      Next
      '  Me._ActiveRowNo = 0

      If Me._LastSearcingText = Me.CoaSearchTextBox.Text Then
        Me._SearchResultUnTypedDataTable = Me._CoaAccountingTableAdapter.GetMatchingCOACodeRow(Me.CoaSpreadSheet.ActiveSheet.GetText(Me._ActiveRowNo, 2), 1)
        Me.Hide()
      Else
      End If

      If Me.CoaSpreadSheet.ActiveSheet.Rows.Count > 0 Then
        Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo).BackColor = Color.Beige
      End If
      Me._LastSearcingText = Me.CoaSearchTextBox.Text
      Me.CoaSearchTextBox.Focus()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Search Coa of Look up Form.", ex)
      Throw _qex
    End Try
  End Sub

  

  Private Sub CoaSpreadSheet_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles CoaSpreadSheet.CellDoubleClick
    'Dim _temp As String
    '_temp = Me.CoaSpreadSheet.ActiveSheet.GetText(Me.CoaSpreadSheet.ActiveSheet.ActiveRowIndex, 2)
    '_SearchResultUnTypedDataRow = Me._CoaDataTable.Select(CStr(CStr(Me._CoaDataTable.COA_CodeColumn.ColumnName) = Me.CoaSpreadSheet.ActiveSheet.GetText(Me.CoaSpreadSheet.ActiveSheet.ActiveRowIndex, 2)))
    'Dim i As Integer
    'For i = 0 To Me._CoaDataTable.Rows.Count - 1
    '  MsgBox(CStr(Me._CoaDataTable.Rows(i).Item(me._CoaDataTable.COA_CodeColumn.ColumnName)))
    'Next
    ' _SearchResultUnTypedDataRow = Me._CoaDataTable.Select(CStr(CStr(Me._CoaDataTable.COA_CodeColumn.ColumnName) = _temp))
    '  _SearchResultUnTypedDataRow = Me._CoaDataTable.Select(CStr(CStr("COA_Code") = _temp))

    Me._SearchResultUnTypedDataTable = Me._CoaAccountingTableAdapter.GetMatchingCOACodeRow(Me.CoaSpreadSheet.ActiveSheet.GetText(Me.CoaSpreadSheet.ActiveSheet.ActiveRowIndex, 2), 1)
    Me.Hide()

  End Sub

  Private Sub CoaSearchTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CoaSearchTextBox.KeyPress
   


    'If Asc(e.KeyChar) = 27 Then
    '  If Me._ActiveRowNo <> Me.CoaSpreadSheet.ActiveSheet.Rows.Count - 1 Then
    '    '  Me.CoaSpreadSheet.ActiveSheet.ActiveRow.BackColor = Color.White
    '    Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo).BackColor = Color.White
    '    Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo + 1).BackColor = Color.Beige
    '    Me._ActiveRowNo += 1
    '    '    Me.CoaSpreadSheet.ActiveSheet.Rows(Me.CoaSpreadSheet.ActiveSheet.ActiveRowIndex + 1).BackColor = Color.Beige
    '  End If
    'End If


  End Sub

  Private Sub CoaSearchTextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CoaSearchTextBox.KeyUp
    If e.KeyCode = Keys.Down Then
      If Me._ActiveRowNo <> Me.CoaSpreadSheet.ActiveSheet.Rows.Count - 1 Then
        '  Me.CoaSpreadSheet.ActiveSheet.ActiveRow.BackColor = Color.White
        Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo).BackColor = Color.White
        Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo + 1).BackColor = Color.Beige
        Me._ActiveRowNo += 1
        '    Me.CoaSpreadSheet.ActiveSheet.Rows(Me.CoaSpreadSheet.ActiveSheet.ActiveRowIndex + 1).BackColor = Color.Beige
      End If
    End If

    If e.KeyCode = Keys.Up Then
      If Me._ActiveRowNo = 0 Then
        Exit Sub
      End If
      Me._ActiveRowNo -= 1
      If Me._ActiveRowNo <> Me.CoaSpreadSheet.ActiveSheet.Rows.Count - 1 Then
        Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo).BackColor = Color.Beige
        Me.CoaSpreadSheet.ActiveSheet.Rows(Me._ActiveRowNo + 1).BackColor = Color.White

      End If
    End If

  End Sub


  'Private Sub GoCoaSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GoCoaSearch.KeyPress
  '  MsgBox(Asc(e.KeyChar))
  'End Sub

  Private Sub CoaSearchTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CoaSearchTextBox.TextChanged

  End Sub
End Class