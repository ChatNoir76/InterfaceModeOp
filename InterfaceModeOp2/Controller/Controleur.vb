Module controleur


    Sub gotoView(ByVal direction As service.View)

        Select Case direction
            Case service.View.Principale

            Case Else

        End Select


        Dim vue As New vuePrincipale()

        vue.Show()


    End Sub
End Module
