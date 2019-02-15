Module controleur



#Region "Gestion des vues"
    Private vue

    ''' <summary>
    ''' Changement de vue de l'interface
    ''' </summary>
    ''' <param name="direction"></param>
    ''' <remarks></remarks>
    Sub gotoView(ByVal direction As service.View)

        Select Case direction
            Case service.View.Principale
                vue = New vuePrincipale()
            Case Else
                vue = New vuePrincipale()
        End Select


        vue.Show()

    End Sub
#End Region

#Region "Action sur fichiers"
    Sub doAction(ByVal monAction As service.Action)
        Select Case monAction
            Case service.Action.ConsultationOfficiel
                MsgBox(service.Action.ConsultationOfficiel.ToString)
            Case service.Action.ConsultationArchive
                MsgBox(service.Action.ConsultationArchive.ToString)
        End Select

    End Sub



#End Region

End Module
