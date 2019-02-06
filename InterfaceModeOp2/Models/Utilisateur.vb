Public Class Utilisateur

    Private _user As String
    Private _nomPC As String


    Sub New()
        _user = UCase(Environment.UserName)
        _nomPC = UCase(Environment.MachineName)




    End Sub

End Class
