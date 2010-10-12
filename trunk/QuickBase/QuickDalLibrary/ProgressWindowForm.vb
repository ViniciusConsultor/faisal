Public Class ProgressWindowForm

  Public Property ProcessName() As String
    Get
      Return ProcessNameLabel.Text
    End Get
    Set(ByVal value As String)
      ProcessNameLabel.Text = value
      Me.ProcessNameLabel.Refresh()
    End Set
  End Property

  Public Sub ChangeProgress(ByVal CurrentValuepara As Int32, ByVal MaximumValuepara As Int32, ByVal CurrentSteppara As String)
    Try
      Me.ProgressBar.Value = CurrentValuepara
      Me.ProgressBar.Maximum = MaximumValuepara
      Me.CurrentStepLabel.Text = CurrentSteppara

      Me.ProgressBar.Refresh()
      Me.CurrentStepLabel.Refresh()
      Me.ProcessNameLabel.Refresh()
      Me.Refresh()

      Me.Opacity = 100 - (Me.ProgressBar.Value / Me.ProgressBar.Maximum) * 50
      If Me.ProgressBar.Value = Me.ProgressBar.Maximum Then Me.Close()

    Catch ex As Exception
      Dim _qex As New QuickExceptionAdvanced("Exception in ChangeProgress method of ProgressWindowForm.", ex)
      Throw _qex
    End Try
  End Sub
End Class