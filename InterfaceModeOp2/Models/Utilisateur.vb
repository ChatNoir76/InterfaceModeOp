Public Class Utilisateur

    'nom utilisateur
    Private _user As String
    'pc utilisateur
    Private _nomPC As String




    Sub New()
        _user = UCase(Environment.UserName)
        _nomPC = UCase(Environment.MachineName)




    End Sub

End Class
