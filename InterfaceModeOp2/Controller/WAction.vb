Imports ConfigBox.GBox
Imports System.Text
Module WAction
#Region "CONSTANTES"

    'CONSTANTE BAS DE PAGE
    Private Const _FOOTER_IMPRESSION As String = "Bon pour utilisation n°"
    Private Const _FOOTER_VERSION As String = "Date {0} {1}"

    'CONSTANTE FILIGRANE A AJOUTER AUX DOCS WORD
    Private Const _FILIGRANE_NOTUSE As String = "PAS POUR UTILISATION"
    Private Const _FILIGRANE_PERIME As String = "Document PÉRIMÉ"

    'CONSTANTE D'ACTION DESCRIPTION BOITE DE DIALOGUE
    Private Const _DESC_CONSULTATION As String = "Sélectionner le mode op à consulter"
    Private Const _DESC_IMPRESSION As String = "Sélectionner le mode op à imprimer"
    Private Const _DESC_IMPORTATION As String = "Sélectionner le mode op à importer"
    Private Const _DESC_IMPORTATION_DOSS_B As String = "Sélectionner la destination de la sauvegarde du mode opératoire à importer (cette version ne sera pas utilisable par la production)"
    Private Const _DESC_IMPORTATION_DOSS_E As String = "Sélectionner la destination du Duplicata utilisable en production"
    Private Const _DESC_ARCHIVAGE_DOSS_E As String = "Sélectionner le Duplicata crypté à archiver"

    'CONSTANTE DE MESSAGE ERREUR / OPERATION
    Private Const _MSG_FIN_OPERATION As String = "------Opération terminée------"
    Private Const _MSG_ERR_WREADER As String = "WREADER ERROR"
    Private Const _MSG_ERR_WACTION As String = "WACTION ERROR"
    Private Const _MSG_ERR_GENERALE As String = "GENERAL ERROR"
    Private Const _MSG_ERR_EX_1 As String = "erreur lors du remplacement du mot {0} par {1} en entête"
    Private Const _MSG_ERR_EX_2 As String = "Importation annulée car le mot clef {0} n'est pas présent dans l'entête du document Word"
    Private Const _MSG_ERR_EX_3 As String = "Une importation nécessitant un archivage d'une version antérieure doit obligatoirement avoir un mode op archivé, l'importation est donc annulée"
    Private Const _MSG_ERR_EX_4 As String = "Mode de consultation indéterminé"

    'CONSTANTE GENERALE DE TEXTE
    Private Const _GEN_INFO_FILIGRANE As String = "Création du filigrane"
    Private Const _GEN_INFO_NETTBASPAGE As String = "Nettoyage du bas de page"
    Private Const _GEN_INFO_COPYTO As String = "Copie dans le dossier {0}"
    Private Const _GEN_INFO_REMPLACEMENT_ET As String = "Remplacement {0} par {1}"
    Private Const _GEN_INFO_DEL_SOURCE As String = "Supression du fichier source"
    Private Const _GEN_INFO_PDF As String = "Création du PDF"
    Private Const _GEN_INFO_AJOUT_BASPAGE As String = "Ajout information en bas de page"

    Private Const _GEN_ARCHIVAGE_1 As String = "ARCHIVAGE D'UN MODE OPERATOIRE"
    Private Const _GEN_ARCHIVAGE_2 As String = "Archivage d'une version antérieure"
    Private Const _GEN_ARCHIVAGE_3 As String = "Archivage : annulé"
    Private Const _GEN_ARCHIVAGE_4 As String = "Archivage : {0}"

    Private Const _GEN_IMPORTATION_1 As String = "Importation : annulée"
    Private Const _GEN_IMPORTATION_2 As String = "Ajout de la date d'importation"
    Private Const _GEN_IMPORTATION_3 As String = "Importation : {0}"
    Private Const _GEN_IMPORTATION_4 As String = "Archivage de version antérieure"
    Private Const _GEN_IMPORTATION_5 As String = "Avez Vous une version antérieure à archiver?"
    Private Const _GEN_IMPORTATION_6 As String = "IMPORTATION D'UN MODE OPÉRATOIRE"

    Private Const _GEN_IMPRESSION_1 As String = "IMPRESSION POUR PRODUCTION"
    Private Const _GEN_IMPRESSION_2 As String = "Impression : annulée"
    Private Const _GEN_IMPRESSION_3 As String = "Impression : {0}"
    Private Const _GEN_IMPRESSION_4 As String = "Ouverture de la boite de dialogue d'impression"
    Private Const _GEN_IMPRESSION_5 As String = "Enregistrement de l'impression dans l'audit trails"
    Private Const _GEN_IMPRESSION_6 As String = "Imprimante : {0}"
    Private Const _GEN_IMPRESSION_7 As String = "Impression en cours sur imprimante {0}"

    Private Const _GEN_CONSULTATION_1 As String = "CONSULTATION -> {0}"
    Private Const _GEN_CONSULTATION_2 As String = "Consultation : annulée"
    Private Const _GEN_CONSULTATION_3 As String = "Consultation : {0}"

    Private _Rouge As New Color(0.8, 255, 0, 0)
    Private Const _Police As Single = 10
    Private Const _Style As Integer = FontStyle.Bold
#End Region

    Private Enum TypeConsultation As Byte
        Archive = 0
        Officiel = 1
    End Enum

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
                Case service.Action.Archivage
                    Archivage()
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

#Region "Traitement WAction"
    Private Sub Archivage(Optional ByVal viaImportation As Boolean = False)
        If viaImportation Then
            Info(_GEN_ARCHIVAGE_2)
        Else
            Info(_GEN_ARCHIVAGE_1, True)
        End If

        'MODE OP A ARCHIVER
        Dim OpenFileDiag As New BoxOpenFile(Configuration.getInstance.getFullProdDir(service.DossierProd.DossierE))
        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(service.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_DESC_ARCHIVAGE_DOSS_E, _Police, _Style)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        If IsNothing(OpenFileDiag.Resultat) Then
            If viaImportation Then
                Throw New WActionException(_MSG_ERR_EX_3)
            Else
                Info(_GEN_ARCHIVAGE_3)
            End If
        End If

        'DOSSIER ENREGISTREMENT ARCHIVE
        Dim FileF As New BoxSaveFile(Configuration.getInstance.getFullProdDir(service.DossierProd.DossierF), OpenFileDiag.Resultat)
        FileF.Description(_DESC_IMPORTATION_DOSS_B, _Police, _Style)
        FileF.ShowDialog()

        If IsNothing(FileF.Resultat) Then
            If viaImportation Then
                Throw New WActionException(_MSG_ERR_EX_3)
            Else
                Info(_GEN_ARCHIVAGE_3)
            End If
        Else
            Info(String.Format(_GEN_ARCHIVAGE_4, OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul)))
            With WReader.GetMyWord
                .OpenWord(OpenFileDiag.Resultat)

                'remplacement mot en entête
                Info(String.Format(_GEN_INFO_REMPLACEMENT_ET, service.ET_DUPLICATA, service.ET_PERIME))
                If Not .RemplaceTexteEntete(service.ET_DUPLICATA, service.ET_PERIME) Then
                    Throw New WActionException(String.Format(_MSG_ERR_EX_1, service.ET_DUPLICATA, service.ET_PERIME))
                End If

                'ajout texte en bas de page
                Info(_GEN_INFO_AJOUT_BASPAGE)
                .AjoutTexteBasPage(String.Format(_FOOTER_VERSION, service.Action.Archivage.ToString, Now()), " #")

                'copy vers dossier archive
                Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(service.DossierProd.DossierF)))
                .CopyTo(FileF.Resultat, True)

                'Suppression de la source
                Info(_GEN_INFO_DEL_SOURCE)
                System.IO.File.Delete(OpenFileDiag.Resultat)

            End With
        End If
    End Sub

    ''' <summary>
    ''' Permet l'importation d'un mode op pour utilisation en production dossier E 
    ''' et copie dans un dossier de sauvegarde B en lecture seule
    ''' </summary>
    ''' <remarks>Le fichier source est détruit à l'issue de cette manipulation</remarks>
    Private Sub Importation()
        Info(_GEN_IMPORTATION_6, True)
        Dim NeedArchivage As DialogResult

        'DOSSIER IMPORTATION
        Dim FileC As New BoxOpenFile(Configuration.getInstance.getFullProdDir(service.DossierProd.DossierC))
        'le word à chercher sera sous format Word
        FileC.ChoixExtension(service.EXT_FICHIER_WORD)
        'description de la boite de dialogue
        FileC.Description(_DESC_IMPORTATION, _Police, _Style)
        'affichage de la boite de dialogue
        FileC.ShowDialog()
        'récupération du résultat
        If IsNothing(FileC.Resultat) Then
            Info(_GEN_IMPORTATION_1)
            Exit Sub
        End If

        'DOSSIER ENREGISTREMENT SAUVEGARDE
        Dim FileB As New BoxSaveFile(Configuration.getInstance.getFullProdDir(service.DossierProd.DossierB), FileC.Resultat)
        FileB.Description(_DESC_IMPORTATION_DOSS_B, 10, FontStyle.Bold)
        FileB.ShowDialog()
        If IsNothing(FileB.Resultat) Then
            Info(_GEN_IMPORTATION_1)
            Exit Sub
        End If

        'DOSSIER ENREGISTREMENT OFFICIEL
        Dim FileE As New BoxSaveFile(Configuration.getInstance.getFullProdDir(service.DossierProd.DossierE), FileC.Resultat, service.EXT_SIMPLE_CRP, BoxSaveFile.ext.Ajoute)
        FileE.Description(_DESC_IMPORTATION_DOSS_E, _Police, _Style)
        FileE.ShowDialog()
        If IsNothing(FileE.Resultat) Then
            Info(_GEN_IMPORTATION_1)
            Exit Sub
        End If

        Dim txtMsg As New StringBuilder(_GEN_IMPORTATION_5)
        NeedArchivage = MessageBox.Show(txtMsg.ToString, _GEN_IMPORTATION_4, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        If NeedArchivage = DialogResult.Cancel Then
            Info(_GEN_IMPORTATION_1)
            Exit Sub
        ElseIf NeedArchivage = DialogResult.Yes Then
            'Archivage d'une version ultérieure
            'si erreur procédure annulée
            Archivage(True)
        End If

        Info(String.Format(_GEN_IMPORTATION_3, FileC.Result(BoxOpenFile.Donne.FichierSeul)))
        With WReader.GetMyWord
            .OpenWord(FileC.Resultat)

            If Not .RechercheEnTete(service.ET_ORIGINAL) Then
                Throw New WActionException(String.Format(_MSG_ERR_EX_2, service.ET_ORIGINAL))
            End If

            Info(_GEN_INFO_NETTBASPAGE)
            'le bas de page doit être vide
            .NettoyageTexteBasPage()

            Info(_GEN_IMPORTATION_2)
            'Ajout de la date d'importation
            .AjoutTexteBasPage(String.Format(_FOOTER_VERSION, service.Action.Importation.ToString(), Now()))

            Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(service.DossierProd.DossierB)))
            .CopyTo(FileB.Resultat)

            Info(String.Format(_GEN_INFO_REMPLACEMENT_ET, service.ET_ORIGINAL, service.ET_DUPLICATA))
            If Not .RemplaceTexteEntete(service.ET_ORIGINAL, service.ET_DUPLICATA) Then
                Throw New WActionException(String.Format(_MSG_ERR_EX_1, service.ET_ORIGINAL, service.ET_DUPLICATA))
            End If

            Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(service.DossierProd.DossierE)))
            'copyTo + cryptage = doc word déchargé de la mémoire via myDoc.close()
            .CopyTo(FileE.Resultat, True)

            Info(_GEN_INFO_DEL_SOURCE)
            System.IO.File.Delete(FileC.Resultat)

        End With

    End Sub

    ''' <summary>
    ''' Permet l'impression d'un mode op pour utilisation en production
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Impression()
        Info(_GEN_IMPRESSION_1, True)
        Dim OpenFileDiag As New BoxOpenFile(Configuration.getInstance.getFullProdDir(service.DossierProd.DossierE))

        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(service.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_DESC_IMPRESSION, _Police, _Style)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        If IsNothing(OpenFileDiag.Resultat) Then
            Info(_GEN_IMPRESSION_2)
        Else
            Info(String.Format(_GEN_IMPRESSION_3, OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul)))
            With WReader.GetMyWord
                .OpenWord(OpenFileDiag.Resultat)

                'info à récup d'une bdd
                Dim liste2 As New List(Of String)
                liste2.Add("ceci est une phrase test d'audit trails")

                Dim WPrinter As New VueImpression(.getFields, liste2, .getPages)
                Info(_GEN_IMPRESSION_4)
                WPrinter.ShowDialog()

                If WPrinter.isValidForPrinting Then
                    Info(_GEN_IMPRESSION_5)
                    Debug.Print("AT : " & WPrinter.getAuditTrails)
                    Debug.Print(String.Format(_GEN_IMPRESSION_6, WPrinter.getNomPrinter))
                    'ID = NumBDD

                    'ID à remplacer par le numéro renvoyé par la BDD
                    .AjoutTexteBasPage(_FOOTER_IMPRESSION & "ID", " #")

                    Info(String.Format(_GEN_IMPRESSION_7, WPrinter.getNomPrinter))
                    .PrintDoc(WPrinter.getPageAImprimer)
                Else
                    Info(_GEN_IMPRESSION_2)
                End If

            End With
        End If
    End Sub

    ''' <summary>
    ''' Permet la consultation d'un mode op
    ''' </summary>
    ''' <param name="TConsultation">type de consultation</param>
    ''' <remarks></remarks>
    Private Sub Consultation(ByVal TConsultation As TypeConsultation)

        Info(String.Format(_GEN_CONSULTATION_1, TConsultation.ToString), True)
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
                Throw New WActionException(_MSG_ERR_EX_4)
        End Select

        Dim OpenFileDiag As New BoxOpenFile(Configuration.getInstance.getFullProdDir(monDossierProd))

        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(service.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_DESC_CONSULTATION, _Police, _Style)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        If IsNothing(OpenFileDiag.Resultat) Then
            Info(_GEN_CONSULTATION_2)
        Else
            Info(String.Format(_GEN_CONSULTATION_3, OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul)))
            With WReader.GetMyWord
                .OpenWord(OpenFileDiag.Resultat)
                Info(_GEN_INFO_FILIGRANE)
                .Filigrane(monFiligrane, _Rouge)
                Info(_GEN_INFO_PDF)
                .ExportAsPDF()
            End With
        End If
    End Sub
#End Region

    'appel de la zone d'affichage de la vue principale
    Private Sub Info(ByVal monTexte As String, Optional ByVal Serapation As Boolean = False)
        vuePrincipale.getVP.AffichageTexteVuePrin(monTexte, Serapation)
    End Sub


End Module
