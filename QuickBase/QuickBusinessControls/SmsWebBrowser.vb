Imports QuickDAL.QuickERP
Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickLibrary.Constants

Public Class SmsWebBrowser
  Dim _AlertTA As New AlertTableAdapter
  Dim _AlertDataTable As New AlertDataTable

  Private Enum PredefinedWebPages
    WaridWebToSms
  End Enum

  Dim _CurrentPredfinedPage As PredefinedWebPages

  Public Sub OpenWaridWebToSmsPage()
    Try
      Me.Url = New System.Uri("http://members.waridtel.com/cgi-bin/warid/dev1/warid/homepage")
      _CurrentPredfinedPage = PredefinedWebPages.WaridWebToSms
      _AlertDataTable = _AlertTA.GetAllNotSentSms

    Catch ex As Exception
      Throw ex
    End Try
  End Sub


  Private Sub Quick_WebBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles Me.DocumentCompleted
    Try
      If Me.Url Is Nothing Then
        OpenWaridWebToSmsPage()
      ElseIf Me.Document.Url.AbsoluteUri = "http://members.waridtel.com/cgi-bin/warid/dev1/warid/homepage" Then
        Me.Document.All.GetElementsByName("userName2").Item(0).InnerText = "9455050"
        Me.Document.All.GetElementsByName("pass").Item(0).InnerText = "324725725336"
        Me.Document.InvokeScript("validate")
      ElseIf Me.Document.Url.AbsoluteUri = "https://members.waridtel.com/cgi-bin/warid/dev1/warid/login" Then
        clickelement(Me.Document.Links, "<IMG src=""/images/search_bullet.gif"" border=0>&nbsp;&nbsp;&nbsp;Web-to-SMS", ElementSearchOptions.InnerHTML)
      ElseIf Me.Document.Url.AbsoluteUri.IndexOf("programId=39767") > -1 Then
        If _AlertDataTable IsNot Nothing AndAlso _AlertDataTable.Count > 0 Then
          Dim _RecepientNumbers() As String = Split(_AlertDataTable(0).Alert_Destination, QuickLibrary.MultiValue.MultiValueSeperator)
          If _RecepientNumbers.Length > 0 Then
            If Me.Document.All.GetElementsByName("numbersadd").Item(0).InnerText <> String.Empty Then
              Me.Document.All.GetElementsByName("numbersadd").Item(0).InnerText = String.Empty
            End If
            For I As Int32 = 0 To _RecepientNumbers.Length - 1
              Me.Document.All.GetElementsByName("recipientnumber").Item(0).InnerText = _RecepientNumbers(I)
              Me.Document.InvokeScript("addtolist")
            Next I
            Me.Document.All.GetElementsByName("textarea").Item(0).InnerText = _AlertDataTable(0).Alert_Body

            clickelement(Me.Document.Forms(0).GetElementsByTagName("Input"), "Send Message", ElementSearchOptions.ValueAttribute)
          End If
          'Remove first item
          _AlertDataTable(0).DocumentStatus_ID = documentstatuses.Message_Send
        End If
      Else
        If My.Computer.Network.Ping("google.com") Then
          Me.OpenWaridWebToSmsPage()
        End If
      End If

      System.Windows.Forms.Application.DoEvents()

    Catch ex As Exception
      Throw ex
    Finally
      _AlertDataTable(0).NoOfTries += 1
      _AlertTA.Update(_AlertDataTable(0))
      _AlertDataTable.Rows.RemoveAt(0)
    End Try
  End Sub

  Public Sub SendPendingSms()
    _AlertDataTable = _AlertTA.GetAllNotSentSms
    Me.Quick_WebBrowser_DocumentCompleted(Me, New System.Windows.Forms.WebBrowserDocumentCompletedEventArgs(Me.Url))
  End Sub
  'Public Sub SendSms(ByVal _Receipts As String, ByVal _SmsBody As String)
  '  Try
  '    _ReceiptCollection.Add(_Receipts)
  '    _SmsTextCollection.Add(_SmsBody)

  '    Me.Quick_WebBrowser_DocumentCompleted(Me, New System.Windows.Forms.WebBrowserDocumentCompletedEventArgs(Me.Url))

  '  Catch ex As Exception
  '    Throw ex
  '  End Try
  'End Sub

End Class
