Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters

'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 21-May-10 
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' It is an expandable control and it contains checked list box so that user can
''' randomly select companies. It returns comma seperated company IDs so that it
''' can be passed to queries directly or use in code however needed.
''' </summary>
Public Class CompanyCheckedListBox
  Private _CompanyTA As New CompanyTableAdapter
  Private _CompanyDataTable As New CompanyDataTable
  Private MAX_HEIGHT As Int16 = 200

#Region "Declarations"

#End Region

#Region "Properties"

#End Region

#Region "Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-May-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will load current company and all its child companies.
  ''' </summary>
  Public Sub LoadThisAndChildCompanies(ByVal _CompanyIDpara As Int16)
    Try
      _CompanyDataTable = _CompanyTA.GetParentAndChildsByCoID(_CompanyIDpara)
      SetLayout()

    Catch ex As Exception
      Throw New QuickExceptionAdvanced("Exception in LoadThisAndChildCompanies method of CompanyCheckListBox.", ex)
    End Try
  End Sub


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-May-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It sets the display layout of the data and control.
  ''' </summary>
  Public Sub SetLayout()
    Try
      QuickDALLibrary.General.SetColumnCaptions(_CompanyDataTable, Me.FindForm.Name)
      With _CompanyDataTable
        'Revrese loop, to handle the index change due to deletion of column
        For i As Int32 = .Columns.Count - 1 To 0 Step -1
          Select Case .Columns(i).ColumnName
            Case .Co_CodeColumn.ColumnName, _
            .Co_DescColumn.ColumnName, .Co_IdColumn.ColumnName
            Case Else
              .Columns.RemoveAt(i)
          End Select
        Next

        For I As Int32 = 0 To _CompanyDataTable.Rows.Count - 1
          Me.UltraListView1.Items.Add(_CompanyDataTable(I).Co_Id, _CompanyDataTable(I).Co_Code)
        Next I

      End With

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in SetLayout method of CompanyCheckedListBox", ex)
      Throw _qex
    End Try
  End Sub

#End Region

#Region "Event Methods"

#End Region

End Class

