Imports QuickLibrary
Imports System.Windows.Forms

Public Class Quick_Spread
  Implements IClearControl

  Private _AutoNewRow As Boolean = True
  Private _ShowDeleteRowButton As Boolean = True

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    FarPoint.Win.Spread.DefaultSkins.Classic.Apply(Me)
  End Sub

#Region "Properties"
  Public Property AutoNewRow() As Boolean
    Get
      Return _AutoNewRow
    End Get
    Set(ByVal value As Boolean)
      _AutoNewRow = value
    End Set
  End Property

  Public Property ShowDeleteRowButton(ByVal _SheetView As FarPoint.Win.Spread.SheetView) As Boolean
    Get
      Return _ShowDeleteRowButton
    End Get
    Set(ByVal value As Boolean)
      Try
        _ShowDeleteRowButton = value
        If _ShowDeleteRowButton Then
          AddDeleteButton(_SheetView)
        End If

      Catch ex As Exception
        Throw New QuickLibrary.QuickException("Exception in Set method of ShowDeleteRowButton property of Quick_Spread.", ex)
      End Try
    End Set
  End Property
#End Region

#Region "Methods"
  Public Function ActualColumnLabel(ByVal ColumnIndexpara As Int32) As String
    Try
      Dim _Labels() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" _
        , "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

      If ColumnIndexpara > _Labels.Length - 1 Then
        Return Nothing
      Else
        Return _Labels(ColumnIndexpara)
      End If

    Catch ex As Exception
      Throw New QuickException("Exception in ActualColumnLabel method of Quick_Spread.", ex)
    End Try
  End Function

  Private Sub AddNewRow()
    Try
      Me.ActiveSheet.Rows.Count += 1

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Public Sub Clear()
    Try
      For I As Int32 = 0 To Me.Sheets.Count - 1
        If Me.DataSource IsNot Nothing AndAlso TypeOf Me.DataSource Is DataTable Then
          DirectCast(Me.DataSource, DataTable).Rows.Clear()
        Else
          Me.Sheets(I).Rows.Clear()
        End If
      Next I

      'AddNewRow()

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Private Sub AddDeleteButton(ByVal _SheetView As FarPoint.Win.Spread.SheetView)
    Try
      Dim _CellType As New FarPoint.Win.Spread.CellType.ButtonCellType

      If _SheetView.DataSource Is Nothing Then
        _SheetView.Columns.Add(0, 1)
      ElseIf TypeOf _SheetView.DataSource Is DataTable Then
        With DirectCast(_SheetView.DataSource, DataTable)
          If Not .Columns.Contains("DeleteRowButton") Then
            .Columns.Add("DeleteRowButton")
            .Columns("DeleteRowButton").Caption = "Del"
            .Columns("DeleteRowButton").SetOrdinal(0)
          End If
        End With
      End If

      _SheetView.Columns(0).CellType = _CellType
      _SheetView.Columns(0).TabStop = False
      _SheetView.Columns(0).Width = 25

    Catch ex As Exception
      Throw New QuickException("Exception in AddDeleteButton method of Quick_Spread.", ex)
    End Try
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
  Public Overridable Function ClearControl() As QuickLibrary.Constants.MethodResult Implements IClearControl.ClearControl
    Try

      If DataSource IsNot Nothing AndAlso TypeOf DataSource Is DataTable Then
        DirectCast(DataSource, DataTable).Rows.Clear()
      Else
        Me.ActiveSheet.Rows.Clear()
      End If

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in ClearControl of Quick_ClearControl.", ex)
      Throw _qex
    End Try
  End Function
#End Region

#Region "Event Methods"





  Private Sub Quick_Spread_ButtonClicked(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles Me.ButtonClicked
    Try

      If Me._ShowDeleteRowButton AndAlso (Me.ActiveSheet.ActiveColumnIndex < Me.ActiveSheet.RowCount - 1 OrElse Not Me.AutoNewRow) Then
        If MessageBox.Show("Are you sure, you want to delete a row", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
          'Me.ActiveSheet.RemoveRows(e.Row, 1)
          Me.ActiveSheet.Rows(e.Row).Visible = False
          Me.ActiveSheet.Rows(e.Row).Tag = Constants.DELETE_ROW_TAG_TEXT
        End If
      End If

    Catch ex As Exception
      Dim _QuickException As QuickException = New QuickException("Exception in Quick_Spread_ButtonClicked event method of Quick Spread", ex)
      MessageBox.Show(_QuickException.GetFullMessage)
    End Try
  End Sub

  Private Sub Quick_Spread_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.EditModeOff
    Try
      Dim SheetViewObject As FarPoint.Win.Spread.SheetView

      SheetViewObject = Me.ActiveSheet
      If AutoNewRow AndAlso Not TypeOf SheetViewObject.DataSource Is DataTable AndAlso SheetViewObject.ActiveRowIndex = SheetViewObject.RowCount - 1 AndAlso SheetViewObject.ActiveCell.Text <> "" Then
        AddNewRow()
      End If

    Catch ex As Exception
      Dim _QuickException As QuickException = New QuickException("Exception in Quick_Spread_EditModeOff event method of Quick Spread", ex)
      MessageBox.Show(_QuickException.GetFullMessage)
    End Try
  End Sub

#End Region

End Class

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 19-Mar-2010
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' Nothing
''' </summary>
Public Class Quick_SpreadView
  Inherits FarPoint.Win.Spread.SheetView

#Region "Declarations"

#End Region

#Region "Properties"

#End Region

#Region "Methods"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 19-Mar-2010
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Use thid method to check if row is deleted. Currently row is set to visible 
  ''' false and tag text is set when row is deleted.
  ''' </summary>
  Public Function IsRowDeleted(ByVal _RowNo As Int32) As Boolean
    Try
      If Me.Rows(_RowNo).Visible = False AndAlso Me.Rows(_RowNo).Tag IsNot Nothing AndAlso Me.Rows(_RowNo).Tag.ToString = Constants.DELETE_ROW_TAG_TEXT Then
        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in IsRowDeleted of QuickSpreadView.", ex)
      Throw _qex
    End Try
  End Function
#End Region

#Region "Event Methods"

#End Region

End Class
