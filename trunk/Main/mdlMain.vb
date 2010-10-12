Module mdlMain

  '*************************************************************************************
  'This method is called on the load of application and this method performs the startup
  'tasks.
    Public Sub LaunchApplication()
        Try
      MDIParent1.Show()
        Catch ex As Exception
            Throw ex
        End Try
  End Sub

End Module
