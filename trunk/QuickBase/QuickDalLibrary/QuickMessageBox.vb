Imports System.Windows.Forms
Imports QuickLibrary.Constants
Imports QuickDALLibrary

Public Class QuickMessageBox
  Private _MessageShown As String
  Private _MessageBoxType As MessageBoxTypes
  Private _LoginInfoForEmail As LoginInfo
  Private Const ButtonTextOK As String = "&OK"
  Private Const ButtonTextCancel As String = "&Cancel"
  Private Const ButtonTextYes As String = "&Yes"
  Private Const ButtonTextNo As String = "&No"
  Private Const ButtonTextAbort As String = "&Abort"
  Private Const ButtonTextRetry As String = "&Retry"
  Private Const ButtonTextIgnore As String = "&Ignore"
  Public Enum MessageBoxTypes
    LongMessage
    ShortMessage
  End Enum

  Public Enum PredefinedMessages
    SaveSuccessfulMessage
    SaveUnSuccessfulMessage
    DeleteSuccessfulMessage
    DeleteUnSuccessfulMessage
    LoadSuccessfulMessage
    LoadUnSuccessfulMessage
  End Enum


#Region "Event Methods"

  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Jan-10
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Description of the method goes here ...
  ''' </summary>
  Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles Button1.Click, Button2.Click, Button3.Click

    Try
      If CType(sender, Control).Text = ButtonTextAbort Then
        Me.DialogResult = Windows.Forms.DialogResult.Abort
      ElseIf CType(sender, Control).Text = ButtonTextCancel Then
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
      ElseIf CType(sender, Control).Text = ButtonTextIgnore Then
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
      ElseIf CType(sender, Control).Text = ButtonTextNo Then
        Me.DialogResult = Windows.Forms.DialogResult.No
      ElseIf CType(sender, Control).Text = ButtonTextOK Then
        Me.DialogResult = Windows.Forms.DialogResult.OK
      ElseIf CType(sender, Control).Text = ButtonTextRetry Then
        Me.DialogResult = Windows.Forms.DialogResult.Retry
      ElseIf CType(sender, Control).Text = ButtonTextYes Then
        Me.DialogResult = Windows.Forms.DialogResult.Yes
      End If

      'Me.Close()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Button_Click of QuickMessageBox.", ex)
      Throw _qex
    End Try

  End Sub

  Private Sub RecordOperationOkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub InformVerndorButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformVerndorButton.Click
    Try
      Me.Cursor = Cursors.WaitCursor

      QuickAlert.SendAlert(_LoginInfoForEmail, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp User Alert", Me.LongMessageTextBox.Text)

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Methods"

  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal _PredefinedMessage As PredefinedMessages) As DialogResult
    Try
      Dim _Message As String
      Dim _QuickMessageBoxIcon As MessageBoxIcon = MessageBoxIcon.Information

      Select Case _PredefinedMessage
        Case PredefinedMessages.SaveSuccessfulMessage
          _Message = "Record(s) is successfully saved"
          _QuickMessageBoxIcon = MessageBoxIcon.Information
        Case PredefinedMessages.SaveUnSuccessfulMessage
          _Message = "Record(s) is not saved"
          _QuickMessageBoxIcon = MessageBoxIcon.Exclamation
        Case PredefinedMessages.DeleteSuccessfulMessage
          _Message = "Record(s) is succesfully deleted"
          _QuickMessageBoxIcon = MessageBoxIcon.Information
        Case PredefinedMessages.DeleteSuccessfulMessage
          _Message = "Record(s) is not deleted"
          _QuickMessageBoxIcon = MessageBoxIcon.Error
        Case PredefinedMessages.LoadSuccessfulMessage
          _Message = "Record(s) is loaded"
          _QuickMessageBoxIcon = MessageBoxIcon.Information
        Case PredefinedMessages.LoadSuccessfulMessage
          _Message = "Record(s) could not be loaded"
          _QuickMessageBoxIcon = MessageBoxIcon.Exclamation
        Case Else
          _Message = "This predefined message is not yet handled"
      End Select

      Show(_LoginInfo, _Message, MessageBoxButtons.OK, MessageBoxTypes.ShortMessage, _QuickMessageBoxIcon)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in showing predefined message.", ex)
      Throw _QuickException
    End Try
  End Function

  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal _BrokenBusinessRules As QuickDAL.LogicalDataSet.BusinessRuleDataTable) As DialogResult
    Try
      Dim _Message As String = String.Empty

      For I As Int32 = 0 To _BrokenBusinessRules.Count - 1
        _Message &= _BrokenBusinessRules(I).RuleDescription & Environment.NewLine
      Next
      Show(_LoginInfo, _Message, MessageBoxButtons.OK, MessageBoxTypes.ShortMessage, MessageBoxIcon.Error)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in show(LoginInfo,BusinessRuleDataTable)", ex)
      Throw _QuickException
    End Try
  End Function

  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal Message As String) As DialogResult
    Try
      Show(_LoginInfo, Message, MessageBoxButtons.OK, MessageBoxTypes.ShortMessage, MessageBoxIcon.Exclamation)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in show(LoginInfo,String)", ex)
      Throw _QuickException
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Jan-10
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' You can give message text and chose buttons for message box.
  ''' </summary>
  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal Message As String, ByVal QuickMessageBoxButton As MessageBoxButtons) As DialogResult
    Try
      Show(_LoginInfo, Message, QuickMessageBoxButton, MessageBoxTypes.ShortMessage, MessageBoxIcon.None)

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in Show of QuickMessageBox.", ex)
      Throw _qex
    End Try
  End Function

  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal Message As String, ByVal QuickMessageBoxIcon As MessageBoxIcon) As DialogResult
    Try
      Show(_LoginInfo, Message, MessageBoxButtons.OK, MessageBoxTypes.LongMessage, QuickMessageBoxIcon)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in show(LoginInfo,String)", ex)
      Throw _QuickException
    End Try
  End Function

  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal Message As String, ByVal MessageBoxType As MessageBoxTypes) As DialogResult
    Try
      Show(_LoginInfo, Message, MessageBoxButtons.OK, MessageBoxType, MessageBoxIcon.Information)

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in show(LoginInfo,String,Messageboxtypes)", ex)
      Throw _QuickException
    End Try
  End Function


  'Author: Faisal Saleem
  'Date Created(DD-MMM-YY): 15-Jan-10
  '***** Modification History *****
  'Name   Date(DD-MMM-YY)   Description
  '--------------------------------------------------------------------------------
  '
  ''' <summary>
  ''' Maximum options show method.
  ''' </summary>
  Public Overloads Shared Function Show(ByVal _LoginInfo As LoginInfo, ByVal Message As String, ByVal QuickMessageBoxButtons As MessageBoxButtons, ByVal MessageBoxType As MessageBoxTypes, ByVal QuickMessageBoxIcon As System.Windows.Forms.MessageBoxIcon) As DialogResult

    Dim QuickMessageBoxObject As New QuickMessageBox

    Try
      Dim DisplayMessage As Boolean = True

      With QuickMessageBoxObject
        QuickMessageBoxObject.MessageBoxType = MessageBoxType
        QuickMessageBoxObject._LoginInfoForEmail = _LoginInfo
        Select Case MessageBoxType
          Case MessageBoxTypes.ShortMessage
            'We should also check it form the showing this form by adding formobject.name & SETTING_ID_SEPERATOR & 
            'but right now there is no reference of the form available.
            If DatabaseCache.GetSettingValue(SETTING_ID_DisplayRecordOperationMessage) = "0" Then
              DisplayMessage = False
            End If
            QuickMessageBoxObject.RecordOperationTextBox.Text = Message
          Case Else
            'In every other case display long message type.
            QuickMessageBoxObject.LongMessageTextBox.Text = Message
        End Select

        Select Case QuickMessageBoxIcon
          Case MessageBoxIcon.Error, MessageBoxIcon.Stop
            QuickMessageBoxObject.PictureBox1.Image = My.Resources.Critical
          Case MessageBoxIcon.Exclamation, MessageBoxIcon.Hand
            QuickMessageBoxObject.PictureBox1.Image = My.Resources.Warning
          Case MessageBoxIcon.Question
            QuickMessageBoxObject.PictureBox1.Image = My.Resources.Question
          Case MessageBoxIcon.Information, MessageBoxIcon.Asterisk
            QuickMessageBoxObject.PictureBox1.Image = My.Resources.Information
          Case MessageBoxIcon.None
            QuickMessageBoxObject.PictureBox1.Image = Nothing
          Case Else
            QuickMessageBoxObject.PictureBox1.Image = My.Resources.Information
        End Select

        Select Case QuickMessageBoxButtons
          Case MessageBoxButtons.OK
            .Button1.Visible = False
            .Button2.Visible = False
            .Button3.Visible = True

            .Button1.Text = ButtonTextOK
            .Button2.Text = ButtonTextOK
            .Button3.Text = ButtonTextOK

            QuickMessageBoxObject.AcceptButton = QuickMessageBoxObject.Button3
            QuickMessageBoxObject.CancelButton = QuickMessageBoxObject.Button3
          Case MessageBoxButtons.AbortRetryIgnore
            .Button1.Visible = True
            .Button2.Visible = True
            .Button3.Visible = True

            .Button1.Text = ButtonTextAbort
            .Button2.Text = ButtonTextRetry
            .Button3.Text = ButtonTextIgnore

            QuickMessageBoxObject.AcceptButton = QuickMessageBoxObject.Button2
            QuickMessageBoxObject.CancelButton = QuickMessageBoxObject.Button1
          Case MessageBoxButtons.OKCancel
            .Button1.Visible = False
            .Button2.Visible = True
            .Button3.Visible = True

            .Button1.Text = ButtonTextOK
            .Button2.Text = ButtonTextOK
            .Button3.Text = ButtonTextCancel

            QuickMessageBoxObject.AcceptButton = QuickMessageBoxObject.Button2
            QuickMessageBoxObject.CancelButton = QuickMessageBoxObject.Button3
          Case MessageBoxButtons.RetryCancel
            .Button1.Visible = False
            .Button2.Visible = True
            .Button3.Visible = True

            .Button1.Text = ButtonTextOK
            .Button2.Text = ButtonTextRetry
            .Button3.Text = ButtonTextCancel

            QuickMessageBoxObject.AcceptButton = QuickMessageBoxObject.Button1
            QuickMessageBoxObject.CancelButton = QuickMessageBoxObject.Button3
          Case MessageBoxButtons.YesNo
            .Button1.Visible = False
            .Button2.Visible = True
            .Button3.Visible = True

            .Button1.Text = ButtonTextOK
            .Button2.Text = ButtonTextYes
            .Button3.Text = ButtonTextNo

            QuickMessageBoxObject.AcceptButton = QuickMessageBoxObject.Button2
            QuickMessageBoxObject.CancelButton = QuickMessageBoxObject.Button3
          Case MessageBoxButtons.YesNoCancel
            .Button1.Visible = True
            .Button2.Visible = True
            .Button3.Visible = True

            .Button1.Text = ButtonTextYes
            .Button2.Text = ButtonTextNo
            .Button3.Text = ButtonTextCancel

            QuickMessageBoxObject.AcceptButton = QuickMessageBoxObject.Button1
            QuickMessageBoxObject.CancelButton = QuickMessageBoxObject.Button3
        End Select

        If _LoginInfo IsNot Nothing AndAlso _LoginInfo.FormOjbect IsNot Nothing Then
          QuickMessageBoxObject.Owner = _LoginInfo.FormOjbect
        End If

        QuickMessageBoxObject.TopMost = True
        If DisplayMessage Then
          QuickMessageBoxObject.ShowDialog()
        End If
      End With

      Return QuickMessageBoxObject.DialogResult

    Catch ex As Exception
      Dim _QuickException As New QuickExceptionAdvanced("Exception in showing message. (LoginInfo,String,messageboxtype,MessageBoxIcon)", ex)
      Throw _QuickException
    Finally
      QuickMessageBoxObject.Close()
      QuickMessageBoxObject = Nothing
    End Try
  End Function
#End Region

#Region "Properties"
  Public Property MessageBoxType() As MessageBoxTypes
    Get
      Return _MessageBoxType
    End Get
    Set(ByVal value As MessageBoxTypes)
      _MessageBoxType = value
      Select Case _MessageBoxType
        Case MessageBoxTypes.ShortMessage
          'had to multiply with 3 because multiplying with 2 wasn't giving enough space to display panel
          Me.Width = RecordMessageBoxPanel.Width + BORDER_GAP_WIDTH * 3
          'had to multiply with 5 because multiplying with 2 wasn't giving enough space to display panel
          Me.Height = RecordMessageBoxPanel.Height + BORDER_GAP_HEIGHT * 8
          RecordMessageBoxPanel.Left = BORDER_GAP_WIDTH
          RecordMessageBoxPanel.Top = BORDER_GAP_HEIGHT
          LongMessageBoxPanel.Hide()
          RecordMessageBoxPanel.Show()
        Case Else
          'In every other case display long message type.
          Me.Width = LongMessageBoxPanel.Width + BORDER_GAP_WIDTH * 3
          Me.Height = LongMessageBoxPanel.Height + BORDER_GAP_HEIGHT * 8
          LongMessageBoxPanel.Left = BORDER_GAP_WIDTH
          LongMessageBoxPanel.Top = BORDER_GAP_HEIGHT
          LongMessageBoxPanel.Show()
          RecordMessageBoxPanel.Hide()
          Me.AcceptButton = Me.Button1
      End Select

      Me.ButtonsPanel.Left = Me.Width - Me.ButtonsPanel.Width - BORDER_GAP_WIDTH * 2
      Me.ButtonsPanel.Top = Me.Height - Me.ButtonsPanel.Height - BORDER_GAP_HEIGHT * 7

    End Set
  End Property

#End Region

  'Private Sub QuickMessageBox_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
  '  Try
  '    Select Case Me.MessageBoxType
  '      Case MessageBoxTypes.LongMessage
  '        Me.Button1.Focus()
  '      Case MessageBoxTypes.ShortMessage
  '        Me.RecordOperationOkButton.Focus()
  '    End Select
  '  Catch ex As Exception
  '    Dim _QuickException As New QuickExceptionAdvanced("Exception in QuickMessageBox_Activated method.", ex)
  '    Throw _QuickException
  '  End Try
  'End Sub
End Class
