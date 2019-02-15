Public Class vuePrincipale

    Private Sub TSMI_Utilisateur_Consultation_Officiel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Officiel.Click
        controleur.doAction(service.Action.ConsultationOfficiel)
    End Sub

    Private Sub TSMI_Utilisateur_Consultation_Archive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Archive.Click
        controleur.doAction(service.Action.ConsultationArchive)
    End Sub
End Class