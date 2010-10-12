Imports System.Windows.Forms
Imports QuickLibrary.Constants

Public Class QuickExceptionAdvanced
    Inherits QuickLibrary.QuickException

    Public Sub New(ByVal Message As String, ByVal InnerException As Exception)
        MyBase.New(Message, InnerException)
    End Sub

  'This routine should log the exception and show it to the user.
  Public Sub Show(ByVal _LoginInfo As LoginInfo)
    Try

      Save(_LoginInfo)
      QuickMessageBox.Show(_LoginInfo, GetFullMessage, MessageBoxButtons.OK, QuickMessageBox.MessageBoxTypes.LongMessage, MessageBoxIcon.Exclamation)

    Catch ex As Exception
      MessageBox.Show("" & vbCrLf & ex.Message, "Exception in showing MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Public Sub Save(ByVal _LoginInfo As LoginInfo)
    Try

      QuickAlert.SaveAlert(_LoginInfo, QuickAlert.AlertReceipients.VenderInfo, "Quick Erp Exception Alert", GetFullMessage, AlertTypes.Email)

    Catch ex As Exception
      'Throw ex
    End Try
  End Sub

End Class
