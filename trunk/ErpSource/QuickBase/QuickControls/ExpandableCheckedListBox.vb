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
Public Class ExpandableCheckedListBox
  Private MAX_HEIGHT As Int16 = 200

#Region "Declarations"

#End Region

#Region "Properties"
  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-May-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' It will return command sperated string containing keys from slected rows.
  ''' </summary>
  Public Overridable ReadOnly Property CheckedKeys() As String
    Get
      Try
        Dim _CheckedCompanies As String = String.Empty
        For I As Int32 = 0 To Me.UltraListView1.CheckedItems.Count - 1
          _CheckedCompanies &= Me.UltraListView1.CheckedItems(I).Key & ","
        Next

        If _CheckedCompanies.Length > 0 Then
          Return _CheckedCompanies.Substring(0, _CheckedCompanies.Length - 1)
        Else
          Return _CheckedCompanies
        End If

      Catch ex As Exception
        Dim QuickExceptionObject As New QuickException("Exception in get method of CheckKeys property of CompanyCheckedListBox.", ex)
        Throw QuickExceptionObject
      End Try
    End Get
  End Property

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
  ''' This will select all rows in the list box.
  ''' </summary>
  Public Sub SelectAll()
    Try

      For I As Int32 = 0 To Me.UltraListView1.Items.Count - 1
        Me.UltraListView1.Items(I).CheckState = Windows.Forms.CheckState.Checked
      Next
    Catch ex As Exception
      Dim _qex As New QuickException("Exception in SelectAll of ExapndableCheckListBox.", ex)
      Throw _qex
    End Try
  End Sub

#End Region

#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 21-May-10
  '***** Modification History *****
  '                 Date      Description
  'Name          (DD-MMM-YY) 
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Nothing
  ''' </summary>
  Private Sub UltraListView1_ItemCheckStateChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinListView.ItemCheckStateChangedEventArgs) Handles UltraListView1.ItemCheckStateChanged

    Try
      Me.UltraExpandableGroupBox1.Text = String.Empty

      For I As Int32 = 0 To Me.UltraListView1.CheckedItems.Count - 1
        Me.UltraExpandableGroupBox1.Text &= Me.UltraListView1.CheckedItems(I).Value.ToString & ","
      Next

      If Me.UltraExpandableGroupBox1.Text.Length > 0 Then
        Me.UltraExpandableGroupBox1.Text = Me.UltraExpandableGroupBox1.Text.Substring(0, Me.UltraExpandableGroupBox1.Text.Length - 1)
      End If

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in UltraListView1_ItemCheckStateChanged event method of ExpandableCheckedListBox.", ex)
      Throw _qex
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
  ''' Nothing
  ''' </summary>
  Private Sub UltraExpandableGroupBox1_ExpandedStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UltraExpandableGroupBox1.ExpandedStateChanged

    Try
      If Me.UltraExpandableGroupBox1.Expanded Then
        Me.Height = MAX_HEIGHT
      Else
        Me.Height = Me.UltraExpandableGroupBox1.Height
      End If

    Catch ex As Exception
      Dim _qex As New QuickException("Exception in UltraExpandableGroupBox1_ExpandedStateChanged of ExpandableCheckLitBox.", ex)
      Throw _qex
    End Try

  End Sub

#End Region

End Class

