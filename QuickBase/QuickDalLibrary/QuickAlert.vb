Imports QuickDAL.QuickERPTableAdapters
Imports QuickDAL.QuickERP
Imports QuickDAL.QuickCommonDataSet
Imports QuickDAL.QuickCommonDataSetTableAdapters
Imports QuickLibrary.Constants
Imports QuickLibrary

Public Class QuickAlert

  Private Const VENDER_EMAIL_ADDRESS As String = "quicktijarat@gmail.com"
  Private Const VENDER_MOBILE_NUMBER As String = "9455050"
  Public Shared Event AlertSent(ByVal AlertNo As Int32, ByVal TotalAlerts As Int32)

  Public Enum AlertReceipients
    VenderInfo
    ITManager
    EveryOne
  End Enum

  Shared Function SaveAlert(ByVal _LoginInfo As LoginInfo, ByVal _EmailReceipient As AlertReceipients, ByVal _Subject As String, ByVal _Body As String, ByVal _AlertType As AlertTypes) As Boolean
    Try
      Dim _AlertTA As New AlertTableAdapter
      Dim _AlertDataTable As New AlertDataTable
      Dim _AlertRow As AlertRow
      Dim _LoginInfoString As String = String.Empty
      Dim _AreaManagerEmail As String = String.Empty

      _AreaManagerEmail = DatabaseCache.GetSettingValue(SETTING_ID_EMAIL_AREA_MANAGER)

      'Prepare login information to be sent in email.
      If _LoginInfo IsNot Nothing Then
        _LoginInfoString = "Above is body of the email generated, following is the user information" & vbCrLf
        _LoginInfoString &= vbCrLf & "Email created on " & Common.systemDateTime.DayOfWeek.ToString & " " & Common.systemDateTime.ToLongDateString & " " & Common.systemDateTime.ToLongTimeString
        _LoginInfoString &= vbCrLf & "User Name is " & _LoginInfo.UserName
        _LoginInfoString &= vbCrLf & "Company user logged in is " & _LoginInfo.CompanyDesc & vbCrLf
        _LoginInfoString &= vbCrLf & "Following is the computer information" & vbCrLf
        _LoginInfoString &= vbCrLf & "Computer Name is " & My.Computer.Name
        _LoginInfoString &= vbCrLf & "Application Version " & My.Application.Info.Version.ToString
        _LoginInfoString &= vbCrLf & "Window user " & My.User.Name
        _LoginInfoString &= vbCrLf & "Available phycial memory is " _
          & My.Computer.Info.AvailablePhysicalMemory.ToString & " out of " & My.Computer.Info.TotalPhysicalMemory.ToString
        _LoginInfoString &= vbCrLf & "Available virtual memory is " _
          & My.Computer.Info.AvailableVirtualMemory.ToString & " out of " & My.Computer.Info.TotalVirtualMemory
        _LoginInfoString &= vbCrLf & "Operating system is " & My.Computer.Info.OSFullName _
          & " version is " & My.Computer.Info.OSVersion
        _LoginInfoString &= vbCrLf & "Platform is " & My.Computer.Info.OSPlatform
      End If

      _AlertRow = _AlertDataTable.NewAlertRow
      With _AlertRow
        .Co_ID = _LoginInfo.CompanyID
        .Alert_ID = Convert.ToInt32(_AlertTA.GetNewAlertIDByCoID(_LoginInfo.CompanyID).ToString)
        If _AlertType = AlertTypes.Email Then
          .Alert_Body = _Body & vbCrLf & _LoginInfoString
        Else
          .Alert_Body = _Body
        End If
        If .Alert_Body.Length > _AlertDataTable.Alert_BodyColumn.MaxLength Then
          .Alert_Body = .Alert_Body.Substring(0, _AlertDataTable.Alert_BodyColumn.MaxLength - 1)
        End If
        .Alert_DateTime = Common.systemDateTime
        Select Case _AlertType
          Case AlertTypes.Email
            Select Case _EmailReceipient
              Case AlertReceipients.VenderInfo
                .Alert_Destination = VENDER_EMAIL_ADDRESS
              Case AlertReceipients.ITManager
                If _AreaManagerEmail <> String.Empty Then
                  .Alert_Destination = _AreaManagerEmail
                Else
                  .Alert_Destination = VENDER_EMAIL_ADDRESS
                  .Alert_Subject &= " - Missing Area Manager Email Address"
                End If
              Case AlertReceipients.EveryOne
                .Alert_Destination = VENDER_EMAIL_ADDRESS
              Case Else
                .Alert_Destination = VENDER_EMAIL_ADDRESS
            End Select
          Case AlertTypes.SMS
            Select Case _EmailReceipient
              Case AlertReceipients.VenderInfo
                .Alert_Destination = VENDER_MOBILE_NUMBER
              Case Else
                .Alert_Destination = VENDER_MOBILE_NUMBER
            End Select
        End Select

        .Alert_Source = _LoginInfo.UserName & "(" & _LoginInfo.CompanyDesc & ")"
        .Alert_Subject = _Subject
        .Alert_Type = Convert.ToInt16(_AlertType)
        .Stamp_DateTime = Common.systemDateTime
        .Stamp_UserID = _LoginInfo.UserID
        .DocumentStatus_ID = DocumentStatuses.Message_Added
        .NoOfTries = 0
      End With

      _AlertDataTable.Rows.Add(_AlertRow)
      _AlertTA.Update(_AlertDataTable)

      System.Windows.Forms.Application.DoEvents()

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in SaveAlert method", ex)
      Throw _QuickException
    End Try
  End Function

  Shared Function SendEmailAlerts() As Boolean
    Try
      Dim _smtp As New System.Net.Mail.SmtpClient("smtp.gmail.com")
      Dim _Message As System.Net.Mail.MailMessage
      Dim _ob As New System.Net.CredentialCache
      Dim _AlertTA As New AlertTableAdapter
      Dim _AlertDataTable As New AlertDataTable

      _AlertDataTable = _AlertTA.Get100NotSentEmails

#If CONFIG = "Debug" Then
      Do While _AlertDataTable.Rows.Count > 5
        _AlertDataTable.Rows.RemoveAt(_AlertDataTable.Rows.Count - 1)
      Loop
#End If
        _smtp.Credentials = New Net.NetworkCredential(VENDER_EMAIL_ADDRESS, "qwer1234!@#$")

        For I As Int32 = 0 To _AlertDataTable.Rows.Count - 1
          If _AlertDataTable(I).Alert_Type = AlertTypes.Email Then
            Try
              _AlertDataTable(I).NoOfTries += 1S
            _AlertDataTable(I).Stamp_DateTime = Common.systemDateTime

              _Message = New System.Net.Mail.MailMessage
              _Message.From = New System.Net.Mail.MailAddress(VENDER_EMAIL_ADDRESS, _AlertDataTable(I).Alert_Source)
              _Message.To.Add(_AlertDataTable(I).Alert_Destination)
              _Message.Subject = _AlertDataTable(I).Alert_Subject
              _Message.Body = "Following email was generated on " _
                  & _AlertDataTable(I).Alert_DateTime.ToString & vbCrLf & vbCrLf & _AlertDataTable(I).Alert_Body
              _smtp.EnableSsl = True
            _smtp.Send(_Message)
            _AlertDataTable(I).DocumentStatus_ID = DocumentStatuses.Message_Send
            Catch exEmail As Exception
              'Try
              'Dim _QuickException As New QuickExceptionAdvanced("Exception is SendAlert method", exEmail)
              '_QuickException.Send(_LoginInfo)
              'Catch ex As Exception
              'We can not do anything here.
              'End Try
            End Try

            RaiseEvent AlertSent(I + 1, _AlertDataTable.Rows.Count)
            System.Windows.Forms.Application.DoEvents()
            _AlertTA.Update(_AlertDataTable)
          End If
        Next

        Return True
    Catch ex As Exception
      'Throw New QuickExceptionAdvanced("Exception in SaveAlerts method.", ex)
    End Try
  End Function

  Shared Function SendAlert(ByVal _LoginInfo As LoginInfo, ByVal _EmailReceipient As AlertReceipients, ByVal _Subject As String, ByVal _Body As String) As Boolean
    Try

      SaveAlert(_LoginInfo, _EmailReceipient, _Subject, _Body, AlertTypes.Email)

      Return SendEmailAlerts()

    Catch ex As Exception
      Throw ex
    End Try
  End Function

End Class
