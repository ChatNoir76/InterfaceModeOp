Module controleur

    Private vue

    Sub gotoView(ByVal direction As service.View)

        Select Case direction
            Case service.View.Principale
                vue = New vuePrincipale()
            Case Else
                vue = New vuePrincipale()
        End Select


        vue.Show()

    End Sub
End Module
