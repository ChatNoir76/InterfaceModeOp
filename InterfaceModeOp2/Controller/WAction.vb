Imports ValdepharmTool.GBox
Imports System.Text

Module WAction
#Region "CONSTANTES"

    'CONSTANTE BAS DE PAGE
    Private Const _FOOTER_IMPRESSION = "Bon pour utilisation n°{0}"
    Private Const _FOOTER_VERSION = "Date {0} {1}"

    'CONSTANTE FILIGRANE A AJOUTER AUX DOCS WORD
    Private Const _FILIGRANE_NOTUSE = "PAS POUR UTILISATION"
    Private Const _FILIGRANE_PERIME = "Document PÉRIMÉ"

    'CONSTANTE D'ACTION DESCRIPTION BOITE DE DIALOGUE
    Private Const _DESC_CONSULTATION = "Sélectionner le mode op à consulter"
    Private Const _DESC_IMPRESSION = "Sélectionner le mode op à imprimer"
    Private Const _DESC_IMPORTATION = "Sélectionner le mode op à importer"
    Private Const _DESC_IMPORTATION_DOSS_C = "Sélectionner la destination du mode op à exporter"
    Private Const _DESC_EXPORTATION = "Sélectionner le mode op à exporter"
    Private Const _DESC_IMPORTATION_DOSS_B = "Sélectionner la destination de la sauvegarde du mode opératoire à importer (cette version ne sera pas utilisable par la production)"
    Private Const _DESC_IMPORTATION_DOSS_E = "Sélectionner la destination du Duplicata utilisable en production"
    Private Const _DESC_ARCHIVAGE_DOSS_E = "Sélectionner le Duplicata crypté à archiver"
    Private Const _DESC_ARCHIVAGE_DOSS_F = "Sélectionner la destination du duplicata à archiver"

    'CONSTANTE DE MESSAGE ERREUR / OPERATION
    Private Const _MSG_FIN_OPERATION = "------Opération terminée------"
    Private Const _MSG_ERR_DAO = "DAO ERROR"
    Private Const _MSG_ERR_WREADER = "WREADER ERROR"
    Private Const _MSG_ERR_WACTION = "WACTION ERROR"
    Private Const _MSG_ERR_GENERALE = "GENERAL ERROR"
    Private Const _MSG_ERR_EX_1 = "erreur lors du remplacement du mot {0} par {1} en entête"
    Private Const _MSG_ERR_EX_2 = "Importation annulée car le mot clef {0} n'est pas présent dans l'entête du document Word"
    Private Const _MSG_ERR_EX_3 = "Une importation nécessitant un archivage d'une version antérieure doit obligatoirement avoir un mode op archivé, l'importation est donc annulée"
    Private Const _MSG_ERR_EX_4 = "Mode de consultation indéterminé"
    Private Const _MSG_ERR_EX_5 = "Mode d'exportation indéterminé"

    'CONSTANTE GENERALE DE TEXTE
    Private Const _GEN_BDD_1 = "Enregistrement dans la base de donnée"
    Private Const _GEN_BDD_2 = "Enregistrement AT n°{0}"
    Private Const _GEN_Copy = "Fichier {0} vers {1}"
    Private Const _GEN_CopyBackup = "Fichier {0} vers {1} et copie {2}"

    Private Const _GEN_INFO_FILIGRANE = "Création du filigrane"
    Private Const _GEN_INFO_NETTBASPAGE = "Nettoyage du bas de page"
    Private Const _GEN_INFO_COPYTO = "Copie dans le dossier {0}"
    Private Const _GEN_INFO_REMPLACEMENT_ET = "Remplacement {0} par {1}"
    Private Const _GEN_INFO_DEL_SOURCE = "Supression du fichier source"
    Private Const _GEN_INFO_PDF = "Création du PDF"
    Private Const _GEN_INFO_AJOUT_BASPAGE = "Ajout information en bas de page"

    Private Const _GEN_ARCHIVAGE_1 = "ARCHIVAGE D'UN MODE OPERATOIRE"
    Private Const _GEN_ARCHIVAGE_2 = "Archivage d'une version antérieure"
    Private Const _GEN_ARCHIVAGE_3 = "Archivage : annulé"
    Private Const _GEN_ARCHIVAGE_4 = "Archivage : {0}"

    Private Const _GEN_IMPORTATION_1 = "Importation : annulée"
    Private Const _GEN_IMPORTATION_2 = "Ajout de la date d'importation"
    Private Const _GEN_IMPORTATION_3 = "Importation : {0}"
    Private Const _GEN_IMPORTATION_4 = "Archivage de version antérieure"
    Private Const _GEN_IMPORTATION_5 = "Avez Vous une version antérieure à archiver?"
    Private Const _GEN_IMPORTATION_6 = "IMPORTATION D'UN MODE OPÉRATOIRE"

    Private Const _GEN_IMPRESSION_1 = "IMPRESSION POUR PRODUCTION"
    Private Const _GEN_IMPRESSION_2 = "Impression : annulée"
    Private Const _GEN_IMPRESSION_3 = "Impression : {0}"
    Private Const _GEN_IMPRESSION_4 = "Ouverture de la boite de dialogue d'impression"
    Private Const _GEN_IMPRESSION_5 = "Enregistrement de l'impression dans l'audit trails"
    Private Const _GEN_IMPRESSION_6 = "Imprimante : {0}"
    Private Const _GEN_IMPRESSION_7 = "Impression en cours sur imprimante {0}"

    Private Const _GEN_CONSULTATION_1 = "CONSULTATION -> {0}"
    Private Const _GEN_CONSULTATION_2 = "Consultation : annulée"
    Private Const _GEN_CONSULTATION_3 = "Consultation : {0}"

    Private Const _GEN_EXPORTATION_1 = "EXPORTATION -> {0}"
    Private Const _GEN_EXPORTATION_2 = "Exportation : annulée"
    Private Const _GEN_EXPORTATION_3 = "Exportation : {0}"

    Private _Rouge As New Color(0.8, 255, 0, 0)
    Private Const _Police As Single = 10
    Private Const _Style As Integer = FontStyle.Bold
#End Region

    Private Enum TypeModeOp As Byte
        Archive = 0
        Officiel = 1
    End Enum

    Public Sub doAction(ByVal monAction As Outils.Action)
        Try
            Select Case monAction
                Case Outils.Action.ConsultationOfficiel
                    Consultation(TypeModeOp.Officiel)
                Case Outils.Action.ConsultationArchive
                    Consultation(TypeModeOp.Archive)
                Case Outils.Action.Impression
                    Impression()
                Case Outils.Action.Importation
                    Importation()
                Case Outils.Action.Archivage
                    Archivage()
                Case Outils.Action.ExportationOfficiel
                    Exportation(TypeModeOp.Officiel)
                Case Outils.Action.ExportationArchive
                    Exportation(TypeModeOp.Archive)
                Case Else
                    Throw New WActionException("--Action Indéterminée--")
            End Select
        Catch ex As DAOException
            Info("Source : " & ex.getErrSource, True)
            Info(ex.Message)
            Info(_MSG_ERR_DAO)
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
    Private Sub Exportation(ByVal TypeExport As TypeModeOp)

        Info(String.Format(_GEN_EXPORTATION_1, TypeExport.ToString), True)
        Dim monDossierProd As Outils.DossierProd

        If TypeExport = TypeModeOp.Officiel Then
            monDossierProd = Outils.DossierProd.DossierE
        ElseIf TypeExport = TypeModeOp.Archive Then
            monDossierProd = Outils.DossierProd.DossierF
        Else
            Throw New WActionException(_MSG_ERR_EX_5)
        End If

        'FICHIER A EXPORTER
        Dim FileExp As New BoxOpenFile(Configuration.getInstance.getFullProdDir(monDossierProd))
        'le word à chercher sera crypté
        FileExp.ChoixExtension(Outils.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        FileExp.Description(_DESC_EXPORTATION, _Police, _Style)
        'affichage de la boite de dialogue
        FileExp.ShowDialog()
        If IsNothing(FileExp.Resultat) Then
            Info(_GEN_EXPORTATION_2)
            Exit Sub
        End If

        'EMPLACEMENT D'EXPORTATION
        Dim FileC As New BoxSaveFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierC), System.IO.Path.GetFileNameWithoutExtension(FileExp.Resultat))
        FileC.Description(_DESC_IMPORTATION_DOSS_C, _Police, _Style)
        FileC.ShowDialog()
        If IsNothing(FileC.Resultat) Then
            Info(_GEN_EXPORTATION_2)
            Exit Sub
        End If

        Info(String.Format(_GEN_EXPORTATION_3, FileExp.Result(BoxOpenFile.Donne.FichierSeul)))
        With WReader.GetMyWord
            .OpenWord(FileExp.Resultat, WReader.method.open)

            'remplacement en entete
            If TypeExport = TypeModeOp.Archive Then
                Info(String.Format(_GEN_INFO_REMPLACEMENT_ET, Outils.ET_PERIME, Outils.ET_ORIGINAL))
                If Not .RemplaceTexteEntete(Outils.ET_PERIME, Outils.ET_ORIGINAL) Then
                    Throw New WActionException(String.Format(_MSG_ERR_EX_1, Outils.ET_PERIME, Outils.ET_ORIGINAL))
                End If
            Else
                Info(String.Format(_GEN_INFO_REMPLACEMENT_ET, Outils.ET_DUPLICATA, Outils.ET_ORIGINAL))
                If Not .RemplaceTexteEntete(Outils.ET_DUPLICATA, Outils.ET_ORIGINAL) Then
                    Throw New WActionException(String.Format(_MSG_ERR_EX_1, Outils.ET_DUPLICATA, Outils.ET_ORIGINAL))
                End If
            End If

            Info(_GEN_INFO_AJOUT_BASPAGE)
            .AjoutTexteBasPage(String.Format(_FOOTER_VERSION, "Exportation", Now))

            Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(Outils.DossierProd.DossierC)))
            .CopyTo(FileC.Resultat)

            Info(_GEN_BDD_1)
            Dim at As New auditrails(FileExp.Resultat,
                                     String.Format(_GEN_EXPORTATION_3, TypeExport.ToString),
                                     String.Format(_GEN_Copy, getPath(FileExp.Resultat), getPath(FileC.Resultat)),
                                     Now,
                                     Initialisation.__User.getUserId,
                                     Operations.Exportation)
            DAOFactory.getAuditrails.dbInsert(at)
            Info(String.Format(_GEN_BDD_2, at.idAuditrails))

            Info(_GEN_INFO_DEL_SOURCE)
            System.IO.File.Delete(FileExp.Resultat)

        End With
    End Sub

    Private Sub Archivage(Optional ByVal viaImportation As Boolean = False)
        If viaImportation Then
            Info(_GEN_ARCHIVAGE_2)
        Else
            Info(_GEN_ARCHIVAGE_1, True)
        End If

        'MODE OP A ARCHIVER
        Dim FileE As New BoxOpenFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierE))
        'le word à chercher sera crypté
        FileE.ChoixExtension(Outils.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        FileE.Description(_DESC_ARCHIVAGE_DOSS_E, _Police, _Style)
        'affichage de la boite de dialogue
        FileE.ShowDialog()

        If IsNothing(FileE.Resultat) Then
            If viaImportation Then
                Throw New WActionException(_MSG_ERR_EX_3)
            Else
                Info(_GEN_ARCHIVAGE_3)
            End If
        End If

        'DOSSIER ENREGISTREMENT ARCHIVE
        Dim FileF As New BoxSaveFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierF), FileE.Resultat)
        FileF.Description(_DESC_ARCHIVAGE_DOSS_F, _Police, _Style)
        FileF.ShowDialog()

        If IsNothing(FileF.Resultat) Then
            If viaImportation Then
                Throw New WActionException(_MSG_ERR_EX_3)
            Else
                Info(_GEN_ARCHIVAGE_3)
            End If
        Else
            Info(String.Format(_GEN_ARCHIVAGE_4, FileE.Result(BoxOpenFile.Donne.FichierSeul)))
            With WReader.GetMyWord
                .OpenWord(FileE.Resultat, WReader.method.open)

                'remplacement mot en entête
                Info(String.Format(_GEN_INFO_REMPLACEMENT_ET, Outils.ET_DUPLICATA, Outils.ET_PERIME))
                If Not .RemplaceTexteEntete(Outils.ET_DUPLICATA, Outils.ET_PERIME) Then
                    Throw New WActionException(String.Format(_MSG_ERR_EX_1, Outils.ET_DUPLICATA, Outils.ET_PERIME))
                End If

                'ajout texte en bas de page
                Info(_GEN_INFO_AJOUT_BASPAGE)
                .AjoutTexteBasPage(String.Format(_FOOTER_VERSION, Outils.Action.Archivage.ToString, Now()), " #")

                'copy vers dossier archive
                Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(Outils.DossierProd.DossierF)))
                .CopyTo(FileF.Resultat, True)

                Info(_GEN_BDD_1)
                Dim at As New auditrails(FileE.Resultat,
                                         _GEN_ARCHIVAGE_1,
                                         String.Format(_GEN_Copy, getPath(FileE.Resultat), getPath(FileF.Resultat)),
                                         Now,
                                         Initialisation.__User.getUserId,
                                         Operations.Archivage)
                DAOFactory.getAuditrails.dbInsert(at)
                Info(String.Format(_GEN_BDD_2, at.idAuditrails))

                'Suppression de la source
                Info(_GEN_INFO_DEL_SOURCE)
                System.IO.File.Delete(FileE.Resultat)

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
        Dim FileC As New BoxOpenFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierC))
        'le word à chercher sera sous format Word
        FileC.ChoixExtension(Outils.EXT_FICHIER_WORD)
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
        Dim FileB As New BoxSaveFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierB), FileC.Resultat)
        FileB.Description(_DESC_IMPORTATION_DOSS_B, _Police, _Style)
        FileB.ShowDialog()
        If IsNothing(FileB.Resultat) Then
            Info(_GEN_IMPORTATION_1)
            Exit Sub
        End If

        'DOSSIER ENREGISTREMENT OFFICIEL
        Dim FileE As New BoxSaveFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierE), FileC.Resultat, Outils.EXT_SIMPLE_CRP, BoxSaveFile.ext.Ajoute)
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
            .OpenWord(FileC.Resultat, WReader.method.open)

            If Not .RechercheEnTete(Outils.ET_ORIGINAL) Then
                Throw New WActionException(String.Format(_MSG_ERR_EX_2, Outils.ET_ORIGINAL))
            End If

            Info(_GEN_INFO_NETTBASPAGE)
            'le bas de page doit être vide
            .NettoyageTexteBasPage()

            Info(_GEN_IMPORTATION_2)
            'Ajout de la date d'importation
            .AjoutTexteBasPage(String.Format(_FOOTER_VERSION, Outils.Action.Importation.ToString(), Now()))

            Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(Outils.DossierProd.DossierB)))
            .CopyTo(FileB.Resultat)

            Info(String.Format(_GEN_INFO_REMPLACEMENT_ET, Outils.ET_ORIGINAL, Outils.ET_DUPLICATA))
            If Not .RemplaceTexteEntete(Outils.ET_ORIGINAL, Outils.ET_DUPLICATA) Then
                Throw New WActionException(String.Format(_MSG_ERR_EX_1, Outils.ET_ORIGINAL, Outils.ET_DUPLICATA))
            End If

            Info(String.Format(_GEN_INFO_COPYTO, Configuration.getInstance.getSimpleProdDir(Outils.DossierProd.DossierE)))
            'copyTo + cryptage = doc word déchargé de la mémoire via myDoc.close()
            .CopyTo(FileE.Resultat, True)

            Info(_GEN_BDD_1)
            Dim at As New auditrails(FileC.Resultat, _GEN_IMPORTATION_6, "", Now, Initialisation.__User.getUserId, 2)
            DAOFactory.getAuditrails.dbInsert(at)
            Info(String.Format(_GEN_BDD_2, at.idAuditrails))

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
        Dim OpenFileDiag As New BoxOpenFile(Configuration.getInstance.getFullProdDir(Outils.DossierProd.DossierE))

        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(Outils.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_DESC_IMPRESSION, _Police, _Style)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        If IsNothing(OpenFileDiag.Resultat) Then
            Info(_GEN_IMPRESSION_2)
        Else
            Info(String.Format(_GEN_IMPRESSION_3, OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul)))
            With WReader.GetMyWord
                .OpenWord(OpenFileDiag.Resultat, WReader.method.add)

                'info à récup d'une bdd
                Dim liste2 As New List(Of String)
                liste2.Add("ceci est une phrase test d'audit trails")

                Dim WPrinter As New VueImpression(.getFields, liste2, .getPages)
                Info(_GEN_IMPRESSION_4)
                WPrinter.ShowDialog()

                If WPrinter.isValidForPrinting Then
                    Info(_GEN_IMPRESSION_5)

                    Dim PT_at As New auditrails(getPath(OpenFileDiag.Resultat),
                                                WPrinter.getAuditTrails,
                                                "",
                                                Now(),
                                                Initialisation.__User.getUserId,
                                                Operations.Impression)
                    Dim PT_imp As New Impression(WReader.GetMyWord.getLot,
                                                 WPrinter.getNomPrinter,
                                                 getPagesAsString(WPrinter.getPageAImprimer))
                    Dim PT_signet As New List(Of Signets)
                    For Each element As Signets In .getFields
                        PT_signet.Add(New Signets(element.getClefSignet, element.getValeurSignet, element.getCodeSignet))
                    Next
                    DAOFactory.getATPrinter.dbInsertATPrinter(PT_at, PT_imp, PT_signet)

                    .AjoutTexteBasPage(String.Format(_FOOTER_IMPRESSION, PT_at.idAuditrails), " #")

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
    Private Sub Consultation(ByVal TConsultation As TypeModeOp)

        Info(String.Format(_GEN_CONSULTATION_1, TConsultation.ToString), True)
        Dim monDossierProd As Outils.DossierProd
        Dim monFiligrane As String
        Select Case TConsultation
            Case TypeModeOp.Archive
                monDossierProd = Outils.DossierProd.DossierF
                monFiligrane = _FILIGRANE_PERIME
            Case TypeModeOp.Officiel
                monDossierProd = Outils.DossierProd.DossierE
                monFiligrane = _FILIGRANE_NOTUSE
            Case Else
                Throw New WActionException(_MSG_ERR_EX_4)
        End Select

        Dim OpenFileDiag As New BoxOpenFile(Configuration.getInstance.getFullProdDir(monDossierProd))

        'le word à chercher sera crypté
        OpenFileDiag.ChoixExtension(Outils.EXT_FICHIER_CRYPTER)
        'description de la boite de dialogue
        OpenFileDiag.Description(_DESC_CONSULTATION, _Police, _Style)
        'affichage de la boite de dialogue
        OpenFileDiag.ShowDialog()

        If IsNothing(OpenFileDiag.Resultat) Then
            Info(_GEN_CONSULTATION_2)
        Else
            Info(String.Format(_GEN_CONSULTATION_3, OpenFileDiag.Result(BoxOpenFile.Donne.FichierSeul)))
            With WReader.GetMyWord
                .OpenWord(OpenFileDiag.Resultat, WReader.method.add)
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
        vuePrincipale.getVP.Info(monTexte, Serapation)
    End Sub

    Private Function getPath(ByVal fichier As String) As String
        Return Replace(fichier, Configuration.getInstance.getWorkDir, "..")
    End Function

    Private Function getPagesAsString(ByVal liste As List(Of Integer)) As String
        If liste.Count = 0 Then
            Return "-"
        Else
            Dim txt As New System.Text.StringBuilder
            With txt
                For Each _Int As Integer In liste
                    .Append(_Int).Append(",")
                Next
            End With
            Return txt.ToString.Remove(txt.ToString.Length - 1)
        End If
    End Function

End Module
