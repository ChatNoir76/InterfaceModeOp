Imports ConfigBox.GBox
Imports System.Text
Module WAction
    'CONSTANTE BAS DE PAGE
    Private Const _FOOTER_IMPRESSION As String = "Bon pour utilisation n°"
    Private _FOOTER_VERSION As String = "Date Importation " & Now()

    'CONSTANTE FILIGRANE
    Private Const _FILIGRANE_NOTUSE As String = "PAS POUR UTILISATION"
    Private Const _FILIGRANE_PERIME As String = "Document PÉRIMÉ"

    'CONSTANTE D'ACTION
    Private Const _DESC_CONSULTATION As String = "Sélectionner le mode op à consulter"
    Private Const _DESC_IMPRESSION As String = "Sélectionner le mode op à imprimer"
    Private Const _DESC_IMPORTATION As String = "Sélectionner le mode op à importer"
    Private Const _DESC_IMPORTATION_DOSS_B As String = "Sélectionner la destination de la sauvegarde du mode opératoire à importer (cette version ne sera pas utilisable par la production)"
    Private Const _DESC_IMPORTATION_DOSS_E As String = "Sélectionner la destination du Duplicata utilisable en production"


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
                Case service.Action.Importation
                    Importation()
                Case Else
                    Throw New WActionException("--Action Indéterminée--")
            End Select
        Catch ex As WReaderException
            Info("Source : " & ex.getErreurSource, True)
            Info(ex.Message)
            Info("Procédure : " & ex.getProcedureOrigineErreur)
            Info("document ayant généré l'erreur: " & ex.getNomDocumentWordErreur)
            Info(_MSG_ERR_WREADER)
        Catch ex As WActionException
            Info("Source : " & ex.getErreurSource, True)
            Info(ex.Message)
            Info(_MSG_ERR_WACTION)
        Catch ex As Exception
            Info(ex.Message, True)
            Info(_MSG_ERR_GENERALE)
        Finally
            WReader.Dispose()
            Info(_MSG_FIN_OPERATION)
        End Try
    End Sub

    ''' <summary>
    ''' Permet l'importation d'un mode op pour utilisation en production dossier E 
    ''' et copie dans un dossier de sauvegarde B en lecture seule
    ''' </summary>
    ''' <remarks>Le fichier source est détruit à l'issue de cette manipulation</remarks>
    Private Sub Importation()
        Info("IMPORTATION D'UN MODE OPÉRATOIRE ", True)
        Dim NeedArchivage As DialogResult
        Dim nomFichierC, nomFichierB, nomFichierE As String

        'DOSSIER IMPORTATION
        Dim OpenFileDiag As New BoxOpenFile(Configuration.getInstance.getProdDir(service.DossierProd.DossierC))
        'le word à chercher sera sous format Word
        OpenFileDiag.ChoixExtension(service.EXT_FICHIER_WORD)
        'description de la boite de dialogue
        OpenFileDiag.Description(_DESC_IMPORTATION)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()
        'récupération du résultat
        nomFichierC = OpenFileDiag.Resultat

        If IsNothing(nomFichierC) Then
            Info("Importation : annulée")
            Exit Sub
        End If

        'DOSSIER ENREGISTREMENT SAUVEGARDE
        Dim SaveFileDiagB As New BoxSaveFile(Configuration.getInstance.getProdDir(service.DossierProd.DossierB), nomFichierC)
        SaveFileDiagB.Description(_DESC_IMPORTATION_DOSS_B, 10, FontStyle.Bold)
        SaveFileDiagB.ShowDialog()
        nomFichierB = SaveFileDiagB.Resultat

        If IsNothing(nomFichierB) Then
            Info("Importation : annulée")
            Exit Sub
        End If

        'DOSSIER ENREGISTREMENT OFFICIEL
        Dim SaveFileDiagE As New BoxSaveFile(Configuration.getInstance.getProdDir(service.DossierProd.DossierE), nomFichierC, service.EXT_SIMPLE_CRP, BoxSaveFile.ext.Ajoute)
        SaveFileDiagE.Description(_DESC_IMPORTATION_DOSS_E, 10, FontStyle.Bold)
        SaveFileDiagE.ShowDialog()
        nomFichierE = SaveFileDiagE.Resultat

        If IsNothing(nomFichierE) Then
            Info("Importation : annulée")
            Exit Sub
        Else
            Dim txtMsg As New StringBuilder("Avez Vous une version antérieure à archiver?")
            NeedArchivage = MessageBox.Show(txtMsg.ToString, "Archivage de version antérieure", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If NeedArchivage = DialogResult.Cancel Then
                Info("Importation : annulée")
                Exit Sub
            End If

            Info("Importation : " & OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul))
            With WReader.GetMyWord
                .OpenWord(nomFichierC)

                If Not .RechercheEnTete(service.ET_ORIGINAL) Then
                    Throw New WActionException("Importation annulée car le mot clef " & service.ET_ORIGINAL & " n'est pas présent dans l'entête du document Word")
                End If

                Info("Nettoyage du bas de page")
                'le bas de page doit être vide
                .NettoyageTexteBasPage()

                Info("Ajout de la date d'importation")
                'Ajout de la date d'importation
                .AjoutTexteBasPage(_FOOTER_VERSION, "")

                Info("Copie dans le dossier de sauvegarde")
                .CopyTo(nomFichierB)

                Info("Remplacement ORIGINAL par Duplicata")
                If Not .RemplaceTexteEntete(service.ET_ORIGINAL, service.ET_DUPLICATA) Then
                    Throw New WActionException("erreur lors du remplacement du mot Original par duplicata en entête")
                End If

                Info("Copie dans le dossier de production")
                'copyTo + cryptage = doc word déchargé de la mémoire via myDoc.close()
                .CopyTo(nomFichierE, True)

                Info("Supression du fichier source")
                System.IO.File.Delete(nomFichierC)

            End With
        End If
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
        OpenFileDiag.Description(_DESC_IMPRESSION)
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
        OpenFileDiag.Description(_DESC_CONSULTATION)
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

    'appel de la zone d'affichage de la vue principale
    Private Sub Info(ByVal monTexte As String, Optional ByVal Serapation As Boolean = False)
        vuePrincipale.getVP.AffichageTexteVuePrin(monTexte, Serapation)
    End Sub


End Module
