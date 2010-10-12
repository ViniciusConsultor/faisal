Public Class Quick_WebBrowser
  Protected Enum ElementSearchOptions
    InnerHTML
    InnerText
    ValueAttribute
  End Enum

  Protected Sub clickelement(ByVal obj As System.Windows.Forms.HtmlElementCollection, ByVal _SearchText As String, ByVal _SearchOption As ElementSearchOptions)
    For Each objitem As System.Windows.Forms.HtmlElement In obj
      If (_SearchOption = ElementSearchOptions.InnerHTML AndAlso objitem.InnerHtml = _SearchText) _
        OrElse (_SearchOption = ElementSearchOptions.InnerText AndAlso objitem.InnerText = _SearchText) _
        OrElse (_SearchOption = ElementSearchOptions.ValueAttribute AndAlso objitem.GetAttribute("value") = _SearchText) Then
        objitem.InvokeMember("click")
      End If
      'Debug.WriteLine("Inner HTML=" & objitem.InnerHtml)
      'Debug.WriteLine("Inner Text=" & objitem.InnerText)
      'Debug.WriteLine("Tag Name=" & objitem.TagName)
      'Debug.WriteLine("ID=" & objitem.Id)
      'Debug.WriteLine("Name=" & objitem.Name)
      'Debug.WriteLine("Value Attribute=" & objitem.GetAttribute("value"))

      clickelement(objitem.Children, _SearchText, _SearchOption)
    Next
  End Sub


End Class
