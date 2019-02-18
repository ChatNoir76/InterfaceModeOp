Imports ConfigBox.GBox
Module WAction
    Private _Rouge As New Color(0.8, 255, 0, 0)
    Private Const _FILIGRANE_NOTUSE As String = "PAS POUR UTILISATION"
    Private Const _FILIGRANE_PERIME As String = "PERIME"

    Private Const _CONSULTATION As String = "Sélectionner le mode op à consulter"

    Private Const _MSG_FIN_OPERATION As String = "------Opération terminée------"
    Private Const _MSG_ERR_WREADER As String = "WREADER ERROR"
    Private Const _MSG_ERR_WACTION As String = "WACTION ERROR"
    Private Const _MSG_ERR_GENERALE As String = "GENERAL ERROR"

    Private Enum TypeConsultation As Byte
        Archive = 0
        Officiel = 1
    End Enum

#Region "Traitement WAction"
    Public Sub doAction(ByVal monAction As service.Action)
        Try
            Select Case monAction
                Case service.Action.ConsultationOfficiel
                    Consultation(TypeConsultation.Officiel)
                Case service.Action.ConsultationArchive
                    Consultation(TypeConsultation.Archive)
            End Select
        Catch ex As WReaderException
            Info(_MSG_ERR_WREADER, True)
            Info("document ayant généré l'erreur: " & ex.getNomDocumentWordErreur)
            Info("Procédure : " & ex.getProcedureOrigineErreur)
            Info(ex.Message)
            Info("Source : " & ex.getErreurSource)
        Catch ex As WActionException
            Info(_MSG_ERR_WACTION, True)
            Info(ex.Message)
            Info("Source : " & ex.getErreurSource)
        Catch ex As Exception
            Info(_MSG_ERR_GENERALE, True)
            Info(ex.Message)
        Finally
            WReader.Dispose()
            Info(_MSG_FIN_OPERATION)
        End Try
    End Sub

    Private Sub Consultation(ByVal TConsultation As TypeConsultation)

        Info("CONSULTATION -> " & TConsultation.ToString, True)
        Dim monDossierProd As service.DossierProd
        Dim monFiligrane As String
        Select Case TConsultation
            Case TypeConsultation.Archive
                monDossierProd = service.DossierProd.DossierF
                monFiligrane = _FILIGRANE_PERIME
            Case TypeConsultation.Officiel
                monDossierProd = service.DossierProd.DossierE
                monFiligrane = _FILIGRANE_NOTUSE
            Case Else
                Throw New WActionException("Mode de consultation indéterminé")
        End Select

        Dim NomDossier As String = Configuration.getInstance.getProdDir(monDossierProd)
        Dim OpenFileDiag As New BoxOpenFile(NomDossier)
        Dim nomFichier As String

        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(service.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_CONSULTATION)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        'récupération du résultat
        nomFichier = OpenFileDiag.Resultat

        If IsNothing(nomFichier) Then
            Info("Consultation : annulée")
        Else
            Info("Consultation : " & OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul))
            With WReader.GetMyWord
                .OpenWord(nomFichier)
                Info("Création du filigrane")
                .Filigrane(monFiligrane, _Rouge)
                Info("Création du PDF")
                .ExportAsPDF()
            End With
        End If
        OpenFileDiag = Nothing
    End Sub
#End Region

    Private Sub Info(ByVal monTexte As String, Optional ByVal Serapation As Boolean = False)
        vuePrincipale.getVP.AffichageTexteVuePrin(monTexte, Serapation)
    End Sub


End Module
