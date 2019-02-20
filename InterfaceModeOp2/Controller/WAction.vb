Imports ConfigBox.GBox
Module WAction

    'CONSTANTE BAS DE PAGE
    Private Const _FOOTER_IMPRESSION As String = "Bon pour utilisation n°"

    'CONSTANTE FILIGRANE
    Private Const _FILIGRANE_NOTUSE As String = "PAS POUR UTILISATION"
    Private Const _FILIGRANE_PERIME As String = "PERIME"

    'CONSTANTE D'ACTION
    Private Const _CONSULTATION As String = "Sélectionner le mode op à consulter"
    Private Const _IMPRESSION As String = "Sélectionner le mode op à consulter"

    'CONSTANTE DE MESSAGE ERREUR / OPERATION
    Private Const _MSG_FIN_OPERATION As String = "------Opération terminée------"
    Private Const _MSG_ERR_WREADER As String = "WREADER ERROR"
    Private Const _MSG_ERR_WACTION As String = "WACTION ERROR"
    Private Const _MSG_ERR_GENERALE As String = "GENERAL ERROR"

    Private _Rouge As New Color(0.8, 255, 0, 0)

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
                Case service.Action.Impression
                    Impression()
                Case Else
                    Throw New WActionException("Action Indéterminée")
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

    ''' <summary>
    ''' Permet l'impression d'un mode op pour utilisation en production
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Impression()
        Info("IMPRESSION POUR PRODUCTION", True)
        Dim monDossierProd As service.DossierProd = service.DossierProd.DossierE
        Dim NomDossier As String = Configuration.getInstance.getProdDir(monDossierProd)
        Dim OpenFileDiag As New BoxOpenFile(NomDossier)
        Dim nomFichier As String

        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(service.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_IMPRESSION)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        'récupération du résultat
        nomFichier = OpenFileDiag.Resultat

        If IsNothing(nomFichier) Then
            Info("Impression : annulée")
        Else
            Info("Impression : " & OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul))
            With WReader.GetMyWord
                .OpenWord(nomFichier)

                'info à récup d'une bdd
                Dim liste2 As New List(Of String)
                liste2.Add("ceci est une phrase test d'audit trails")

                Dim WPrinter As New VueImpression(.getFields, liste2, .getPages)
                Info("Ouverture de la boite de dialogue d'impression")
                WPrinter.ShowDialog()

                If WPrinter.isValidForPrinting Then
                    Info("Enregistrement de l'impression dans l'audit trails")
                    Debug.Print("AT : " & WPrinter.getAuditTrails)
                    Debug.Print("Imprimante : " & WPrinter.getNomPrinter)
                    'ID = NumBDD

                    'ID à remplacer par le numéro renvoyé par la BDD
                    .AjoutTexteBasPage(_FOOTER_IMPRESSION & "ID", " #")

                    Info("Impression en cours sur imprimante " & WPrinter.getNomPrinter)
                    .PrintDoc(WPrinter.getPageAImprimer)
                Else
                    Info("Impression : annulée")
                End If

            End With
        End If
        OpenFileDiag = Nothing
    End Sub

    ''' <summary>
    ''' Permet la consultation d'un mode op
    ''' </summary>
    ''' <param name="TConsultation">type de consultation</param>
    ''' <remarks></remarks>
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
