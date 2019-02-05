Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Permissions
Imports Access = Microsoft.Office.Interop.Access
Imports ConfigBDD.GBDD
Imports ConfigBox.GBox
Imports ConfigINI.GINI
Imports ConfigSize.GFormSize

<Assembly: Reflection.AssemblyVersion("1.24.0.1")> 

Public Class main
    Private _MyWord As GWord
    Const FichierINIfind As String = "InterfaceModeOp.ini"
    Const ListeModeOp As String = "ModeOp.txt"
    Private FichierINI As String = Nothing
    Private BaseRep As String
    Private Tmp As String = Path.GetTempPath
    Private _sKey As String
    Private ShutDwn As Boolean = False
    Private _Utilisateur As String = UCase(Environment.UserName)
    Private _DroitUtilisateur As DroitUser
    Private _DimForm As DimensionFenetre
    Private _FichierOFD As New List(Of String) 'dernier fichier ouvert avec OpenFileDialog
    Private _FichierSFD As New List(Of String) 'dernier fichier enregistré avec SaveFileDialog
    Private _listWordProtectKey As New List(Of String)
    Private _DefaultPolice As Single = 12
    Private _DefaultFStyle As FontStyle = FontStyle.Bold
    Private _ForGetMe() As Integer = {53, 87, 104, 97, 69, 70, 49, 51}
    Private _StrConn As String 'String de connexion BDD
    ''' <summary>
    ''' MasEC=0 ; MasApp=1 ; Modif=2 ; Aut=3 ; Off=4 ; Arch=5
    ''' </summary>
    ''' <remarks>MasEC=0 ; MasApp=1 ; Modif=2 ; Aut=3 ; Off=4 ; Arch=5</remarks>
    Private _ArchitectureDossier As New List(Of String)
    Private _TypeFichier As New List(Of String)
    Private _listeDroitUserDossier As New List(Of String)
    Private _SixFunt As String

#Region "Enumération"
    ''' <summary>
    ''' lié à la collection _TypeFichier(index)
    ''' </summary>
    Private Enum TypeFichier As Integer
        Crypter = 0
        Word = 1
    End Enum
    ''' <summary>
    ''' lié à la collection _ArchitectureDossier(index)
    ''' </summary>
    Private Enum Dossier As Integer
        MasterEnCours = 0
        MasterApprouvé = 1
        Modification = 2
        Autres = 3
        Officiel = 4
        Archive = 5
    End Enum
    Private Enum Format As Integer
        Original = 0
        Duplicata = 1
        Périmé = 2
    End Enum
    Private Enum DroitUser As Integer
        Guest = 0
        User = 1
        KeyUser = 2
        UserAQ = 3
        AdminAQ = 4
        AdminDvlp = 5
    End Enum
    Private Enum AccessFolder As Integer
        None = 0
        Lecture = 1
        Ecriture = 2
    End Enum
#End Region

#Region "PROPERTY"
    Private ReadOnly Property FichierCrypter As String
        Get
            Return _TypeFichier(0)
        End Get
    End Property
    Private ReadOnly Property FichierSourceOFD As List(Of String)
        Get
            Return _FichierOFD
        End Get
    End Property
    Private ReadOnly Property FichierEnregSFD As List(Of String)
        Get
            Return _FichierSFD
        End Get
    End Property
    Private ReadOnly Property FichierWord As String
        Get
            Return _TypeFichier(1)
        End Get
    End Property
    Private ReadOnly Property IniMDP As String
        Get
            Return GetMe()
        End Get
    End Property
    Private Function GetMe() As String
        Dim str As String = Nothing
        For i = 0 To _ForGetMe.Count - 1
            str = str & Chr(_ForGetMe(i))
        Next
        Return str
    End Function
#End Region

#Region "Chargement INI"
    Private Function verifFichier(ByVal ParamArray nomFichier() As String) As Boolean
        Dim clef As Boolean = True
        For Each fichier As String In nomFichier
            If Not File.Exists(fichier) Then
                MessageBox.Show(fichier & " est manquant !" & vbNewLine & "l'application va s'arrêter", "Fichier Manquant", MessageBoxButtons.OK, MessageBoxIcon.Error)
                clef = False
                Exit For
            End If
        Next
        Return clef
    End Function
    Sub New()
        InitializeComponent()

        'Vérification de la présence des fichiers de configuration
        If Not verifFichier("ConfigINI.dll", "ConfigBDD.dll", "ConfigBox.dll", "ConfigSize.dll", "InterfaceModeOp.inic") Then
            Me.Close()
        End If

        AffichageAction("Connexion : " & _Utilisateur)
        AffichageAction("Langue : " & System.Globalization.RegionInfo.CurrentRegion.Name)
        AffichageAction("mode accès dossier : " & cv("T:\API\PRIVATE\PRODUCTION\Interface Mode Operatoire").ToString)
        Try
            If File.Exists(FichierINIfind) Then
                FichierINI = FichierINIfind
                initialize()
            ElseIf File.Exists(FichierINIfind & "c") Then
                FichierINI = FichierINIfind & "c"
                initialize()
            Else
                MsgBox("Le programme ne trouve pas le fichier de configuration : " & vbNewLine & FichierINIfind & vbNewLine & FichierINI & vbNewLine & vbNewLine & "L'application va s'arrêter...", MsgBoxStyle.Critical, "Fichier .ini Manquant")
                ShutDwn = True
                Exit Sub
            End If
        Catch ex As Exception
            AffichageAction(ex.Message)
        End Try

#If DEBUG Then
        AffichageAction("MODE DEBUG : droit utilisateur : " & _DroitUtilisateur.ToString)
#End If

    End Sub
    Private Sub initialize()
        Try
            Dim Cini As New ConfigINIc(FichierINI, IniMDP) 'Erreur KBOUCOU ICI

            'Déclaration des variables depuis fichier INI
            BaseRep = Cini.GetKey("BaseRep")
            Tmp += Cini.GetKey("Tmp")
            _sKey = Cini.GetKey("sKey")

            'Attention à l'ordre car l'index est relié à l'énumération typefichier
            _TypeFichier.Add(Cini.GetKey("FichierCrypter"))
            _TypeFichier.Add(Cini.GetKey("OfficeDocuments"))

            'Attention à l'ordre car l'index est relié à l'énumération dossier
            _ArchitectureDossier.Add(BaseRep & Cini.GetKey("MasterEnCours"))
            _ArchitectureDossier.Add(BaseRep & Cini.GetKey("MasterApprouve"))
            _ArchitectureDossier.Add(BaseRep & Cini.GetKey("Modification"))
            _ArchitectureDossier.Add(BaseRep & Cini.GetKey("Autres"))
            _ArchitectureDossier.Add(BaseRep & Cini.GetKey("Officiel"))
            _ArchitectureDossier.Add(BaseRep & Cini.GetKey("Archive"))

            _listWordProtectKey = RecupListeINI(Cini.GetKey("UnderProtected"))
            _StrConn = Cini.OpenProviderViaFichierINI("STRConn")

            With _listeDroitUserDossier
                For i = 0 To 5
                    .Add(Cini.GetKey(i))
                Next
            End With

        Catch ex As Exception
            'si problème avec clef INI, ouverture du fichier INI
            GestionMenu(False)
            GestionMenu(True, TSMI_Developpeur, TSMI_Developpeur_ConfigINI)
            GestionMenu(False, TSMI_Developpeur_EtatModeOp, TSMI_Developpeur_GestionDroitUser, TSMI_Developpeur_OpenAccess)
            AffichageAction(ex.Message)
        End Try

        Try
            'création du dossier temp
            If System.IO.Directory.Exists(Tmp) = False Then System.IO.Directory.CreateDirectory(Tmp)
            _MyWord = Nothing

            'system de gestion du redimensionnement de la fenetre principale
            _DimForm = New DimensionFenetre(Me)

            enregLogin()
            CleanTemp()

        Catch ex As Exception
            'Si problème, mise en retrait du programme
            GestionMenu(False)
            AffichageAction(ex.Message)
        End Try
    End Sub
    Private Sub CleanTemp()
        'nettoyage dossier temp
        On Error Resume Next
        For Each fichier As String In Directory.GetFiles(Tmp)
            File.Delete(fichier)
        Next
    End Sub
    Private Function RecupListeINI(ByVal clef As String) As List(Of String)
        Dim Liste As New List(Of String)
        On Error GoTo out
        For Each MDP As String In clef.Split(Chr(124))
            Liste.Add(MDP)
        Next
        Return Liste
out:
        Return Nothing
    End Function
#End Region

#Region "Gestion Paramètres Utilisateurs"
    Private Sub enregLogin()
        Dim T As New DataBDD(_StrConn)
        Dim D7 As New DataSet
        Dim DIT As Integer
        Dim DITF As DroitUser = 0

        'Détermination des droits en fonction de l'interface
        Me.TXT_LoginUtilisateur.Text = _Utilisateur
        D7 = T.RecupListe_DataSet("DroitUser")

        For Each User As DataRow In D7.Tables(0).Rows
            If UCase(User(1)) = _Utilisateur Then
                For i = 0 To 4
                    If User(i + 2) Then _DroitUtilisateur = i + 1
                Next
            End If
        Next
        DITF = _DroitUtilisateur

        'Récupération des droits en fonction de l'IT (droit sur dossier)
        DIT = DroitIT()

        If DIT < _DroitUtilisateur Then
            If _DroitUtilisateur <> DroitUser.AdminDvlp Then
                _DroitUtilisateur = DIT
                AffichageAction("Les droits interface et IT ne sont pas en adéquation pour le profil " & DITF.ToString & vbNewLine & "Profil compatible : " & _DroitUtilisateur.ToString & " [code:" & _SixFunt & "]")
            Else
                AffichageAction("Les droits interface et IT ne sont pas en adéquation pour le profil " & DITF.ToString & vbNewLine & "Merci de vérifier les paramètres [code:" & _SixFunt & "]")
            End If

        End If
        Me.TXT_Droits.Text = _DroitUtilisateur.ToString
        GestionMenuDroitUser()

    End Sub
    Private Function DroitIT() As DroitUser
        Dim DUserIT As Integer = 0
        Dim Macoll() As Integer = CType([Enum].GetValues(GetType(Dossier)), Integer())

        For Each D As Dossier In Macoll
            _SixFunt = _SixFunt & cv(_ArchitectureDossier(D))
        Next


        For i = 0 To _listeDroitUserDossier.Count - 1
            If _SixFunt Like _listeDroitUserDossier(i) Then DUserIT = i
        Next

        Return DUserIT

    End Function
    Private Sub GestionMenuDroitUser()
#If DEBUG Then
        _DroitUtilisateur = InputBox("Droit Utilisateur... 0 à 5")
#End If
        Select Case _DroitUtilisateur
            Case DroitUser.Guest 'consultation
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur)
                GestionMenu(False, TSMI_Utilisateur_Impression)
            Case DroitUser.User 'GUEST +impression
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur)
            Case DroitUser.KeyUser 'USER +Export
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur, TSMI_Administrateur)
                GestionMenu(False, TSMI_Administrateur_Importation)
            Case DroitUser.UserAQ 'USER +outils
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur, TSMI_Outils)
                GestionMenu(False, TSMI_Outils_Parametre_DroitUser)
            Case DroitUser.AdminAQ '+ gestion droit utilisateur
                GestionMenu(False, TSMI_Developpeur)
            Case DroitUser.AdminDvlp
                GestionMenu(True)
            Case Else
                GestionMenu(False)
                MsgBox("Droits indéterminés", MsgBoxStyle.Exclamation)
        End Select
    End Sub
    ''' <summary>
    ''' Gestion des menus d'en-tete
    ''' </summary>
    Private Overloads Sub GestionMenu(ByVal key As Boolean)
        For Each item As ToolStripMenuItem In MenuStrip1.Items
            item.Visible = key
        Next
    End Sub
    ''' <summary>
    ''' Gestion des menus en fonction de la liste
    ''' </summary>
    ''' <param name="ListeTSMI">liste des menus affectés</param>
    ''' <remarks></remarks>
    Private Overloads Sub GestionMenu(ByVal key As Boolean, ByVal ParamArray ListeTSMI() As ToolStripMenuItem)
        For Each Menu As ToolStripMenuItem In ListeTSMI
            Menu.Visible = key
        Next
    End Sub
    ''' <summary>
    ''' Gestion des menus (mère/enfants)
    ''' </summary>
    ''' <param name="MenuMere">les menus mère / enfants seront affectés</param>
    ''' <remarks></remarks>
    Private Overloads Sub GestionMenu(ByVal key As Boolean, ByVal MenuMere As ToolStripMenuItem)
        For Each item As ToolStripMenuItem In MenuMere.DropDownItems
            item.Visible = key
        Next
        MenuMere.Visible = key
    End Sub
#End Region

#Region "Menu Interface : CONSULTATION"
    ''' <summary>
    ''' consultation d'un fichier crypté en PDF
    ''' </summary>
    ''' <param name="DossierBase">répertoire d'origine du fichier à consulter</param>
    ''' <param name="BaseTxt">description de base pour affichageAction</param>
    ''' <param name="Filigrane">Filigrane à ajouter</param>
    ''' <param name="CouleurFiligrane">couleur du type {0,0,0}.tolist</param>
    ''' <param name="Description">description affiché lors du chargement du fichier</param>
    ''' <remarks></remarks>
    Private Sub Consultation(ByVal DossierBase As Dossier, ByVal BaseTxt As String, ByVal Filigrane As String, ByVal CouleurFiligrane As List(Of Integer), ByVal Description As String, ByVal Format As Format)
        'consultation d'un fichier crypté en PDF
        Dim MyType As String = FichierCrypter
        Dim MyDossier As String = _ArchitectureDossier(DossierBase)

        Try
            If ChargementMyWord(MyType, MyDossier, Description, _DefaultPolice, _DefaultFStyle, TempFile) Then
                If Not _MyWord.Exist(Format.ToString) Then
                    Throw (New Exception("Le fichier n'est pas un " & Format.ToString))
                End If
                _MyWord.Filigrane(Filigrane, CouleurFiligrane)
                _MyWord.ConvertirPDF(TempFile)
                AffichageAction(BaseTxt & _MyWord.FullName)
            End If
        Catch ex As Exception
            AffichageAction(BaseTxt & ex.Message)
        Finally
            clear(GWord.Delete.FichierTemp) 'libère les ressources
            AffichageAction(BaseTxt & "Opération Terminée")
        End Try
    End Sub
    Private Sub TSMI_Utilisateur_Consultation_Officiel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Officiel.Click
        Consultation(Dossier.Officiel, "Consultation PDF : ", "Pas Pour Utilisation", {255, 0, 0}.ToList, "Choix du mode opératoire à consulter : ", Format.Duplicata)
    End Sub
    Private Sub TSMI_Utilisateur_Consultation_Archive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Archive.Click
        Consultation(Dossier.Archive, "Consultation PDF : ", "---Périmé---", {255, 0, 0}.ToList, "Choix du mode opératoire périmé à consulter : ", Format.Périmé)
    End Sub
#End Region

#Region "Menu  Interface : IMPRESSION"
    ''' <summary>
    ''' impression d'un fichier crypté
    ''' </summary>
    ''' <param name="DossierBase">répertoire d'origine du fichier à imprimer</param>
    ''' <param name="BaseTxt">description de base pour affichageAction</param>
    ''' <param name="Description">description affiché lors du chargement du fichier</param>
    Private Sub Impression(ByVal DossierBase As Dossier, ByVal BaseTxt As String, ByVal Description As String)
        'impression d'un fichier crypté
        Dim MyType As String = FichierCrypter
        Dim MyDossier As String = _ArchitectureDossier(DossierBase)
        Dim P As New Impression()
        Dim Format As Format = Format.Duplicata
        'Audit trails
        Dim AT As New ACCDBModeOp(_StrConn)

        Try
            If ChargementMyWord(MyType, MyDossier, Description, _DefaultPolice, _DefaultFStyle, TempFile) Then
                If Not _MyWord.Exist(Format.ToString) Then
                    Throw (New Exception("Le fichier n'est pas un " & Format.ToString))
                End If
                _MyWord.ActiveFields()
                P.DefinirPageMax(_MyWord.DonnePagesTotales)

                For Each element As String In _MyWord.ListeSignet
                    P.DescriptionSignet(element)
                Next

                P.ChargeCB(AT.RecupListe_STR("ATe", "Texte").ToArray)

                P.ShowDialog()
                If Not P.IsOk Then Exit Sub

                Dim ID As Integer = AT.AT_Impression(_MyWord.Name, P.NomPrinter, P.PStart, P.PEnd, P.AuditTrails, _MyWord.Lot, P.Description)
                _MyWord.AjoutTexteBasPage("Bon pour utilisation ID=" & ID, "#")

                _MyWord.SecurePrint(P.NomPrinter, P.AuditTrails, P.PStart, P.PEnd, GWord.Duplex.Verso)
                AffichageAction(BaseTxt & _MyWord.FullName)
            End If
        Catch ex As Exception
            AffichageAction(BaseTxt & ex.Message)
        Finally
            clear(GWord.Delete.FichierTemp) 'libère les ressources
            AffichageAction(BaseTxt & "Opération Terminée")
        End Try
    End Sub
    Private Sub TSMI_Utilisateur_Impression_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Impression.Click
        Impression(Dossier.Officiel, "Impression Mode op : ", "Choix du mode opératoire à imprimer : ")
    End Sub
#End Region

#Region "Menu  Interface : EXPORTATION"
    ''' <summary>
    ''' exportation depuis un fichier crypté vers le dossier d'exportation
    ''' </summary>
    ''' <param name="DossierBase">répertoire d'origine du fichier à imprimer</param>
    ''' <param name="destination">répertoire de destination du fichier exporté</param>
    ''' <param name="BaseTxt">description de base pour affichageAction</param>
    ''' <param name="DescriptionImportation">description affiché lors du chargement du fichier</param>
    ''' <param name="DescriptionEnregistrement">description affiché lors de l'exportation du fichier</param>
    ''' <remarks></remarks>
    Private Sub Exportation(ByVal DossierBase As Dossier, ByVal destination As Dossier, ByVal BaseTxt As String, ByVal DescriptionImportation As String, _
                            ByVal DescriptionEnregistrement As String, Optional ByVal TaillePoliceDescription As Single = 10, _
                            Optional ByVal FStyle As FontStyle = FontStyle.Regular, Optional ByVal Entete As String = "Original", _
                            Optional ByVal Crypter As Boolean = False, Optional ByVal DeleteSource As Boolean = False, _
                            Optional ByVal DeleteTexteBasPage As Boolean = False, Optional ByVal TexteBasPage As String = Nothing, _
                            Optional ByVal TexteAuditTrail As String = Nothing)
        'exportation depuis un fichier crypté
        Dim MyType As String = FichierCrypter
        Dim DossierOrigine As String = _ArchitectureDossier(DossierBase)
        Dim DossierDestination As String = _ArchitectureDossier(destination)
        Dim AT As New ACCDBModeOp(_StrConn)

        Try
            If ChargementMyWord(MyType, DossierOrigine, DescriptionImportation, TaillePoliceDescription, FStyle, TempFile) Then
                If BSF(DossierDestination, GWord.RecupereSansExtention(_MyWord.Name()), DescriptionEnregistrement) Then
                    _MyWord.RemplaceTexteEntete("Duplicata", Entete)
                    If DeleteTexteBasPage Then _MyWord.NettoyageTexteBasPage()
                    If Not TexteBasPage = Nothing Then _MyWord.AjoutTexteBasPage(TexteBasPage)

                    If Crypter Then
                        _MyWord.CopyTo(_FichierSFD(0), _sKey)
                    Else
                        _MyWord.CopyTo(_FichierSFD(0), False)
                    End If
                    If DeleteSource Then _MyWord.DeleteSourceFile()


                    AT.AT_Exportation(_MyWord.Name(), TexteAuditTrail)

                    AffichageAction(BaseTxt & FichierEnregSFD(0))
                End If
            End If
        Catch ex As Exception
            AffichageAction(BaseTxt & ex.Message)
        Finally
            clear(GWord.Delete.FichierTemp)
            AffichageAction(BaseTxt & "Opération Terminée")
        End Try
    End Sub
    Private Sub TSMI_Administrateur_Archivage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Archivage.Click
        Exportation(Dossier.Officiel, Dossier.Archive, "Exportation Mode Op pour Archive : ", "Mode Opératoire à exporter pour Archive", "Emplacement Enregistrement du fichier à Archiver", , , "PÉRIMÉ", True, True, , "Exportation vers Archive le : " & Now, "Pour Archivage")
    End Sub
    Private Sub TSMI_Administrateur_Exportation_Officiel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Exportation_Officiel.Click
        Exportation(Dossier.Officiel, Dossier.Modification, "Exportation Mode Op pour modification : ", "Mode Opératoire à exporter pour modification", "Emplacement Enregistrement du fichier à modifier", DeleteTexteBasPage:=True, TexteAuditTrail:="Modification depuis Officiel")
    End Sub
    Private Sub TSMI_Administrateur_Exportation_Archive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Exportation_Archive.Click
        Exportation(Dossier.Archive, Dossier.Modification, "Exportation Mode Op pour modification : ", "Mode Opératoire à exporter pour modification", "Emplacement Enregistrement du fichier à modifier", DeleteTexteBasPage:=True, TexteAuditTrail:="Modification depuis Archive")
    End Sub
#End Region

#Region "Menu Interface : IMPORTATION"
    Public Function Version() As String
        Return "VERSION DU " & Now.Day & "/" & Now.Month & "/" & Now.Year
    End Function
    ''' <summary>
    ''' importation depuis un fichier Word
    ''' </summary>
    ''' <param name="DossierBase">répertoire d'origine du fichier à imprimer</param>
    ''' <param name="Destination">répertoire de destination du fichier importé</param>
    ''' <param name="BaseTxt">description de base pour affichageAction</param>
    ''' <param name="DescriptionChargement">description affiché lors du chargement du fichier</param>
    ''' <param name="DescriptionEnregistrement">description affiché lors de l'importation du fichier</param>
    ''' <remarks></remarks>
    <Obsolete("Cette méthode n'est plus à jours car seule la méthode ImportationMajeur est utilisée")> Private Sub ImportationMineure(ByVal DossierBase As Dossier, ByVal Destination As Dossier, ByVal BaseTxt As String, ByVal DescriptionChargement As String, ByVal DescriptionEnregistrement As String, ByVal typefichier As TypeFichier, ByVal commentaireAT As String)
        'importation depuis un fichier Word
        Dim MyType As String = _TypeFichier(typefichier)
        Dim DossierOrigine As String = _ArchitectureDossier(DossierBase)
        Dim DossierDestination As String = _ArchitectureDossier(Destination)
        Dim AT As New ACCDBModeOp(_StrConn)
        Try
            If ChargementMyWord(MyType, DossierOrigine, DescriptionChargement, _DefaultPolice, _DefaultFStyle, TempFile) Then
                If BSF(DossierDestination, _MyWord.Name, DescriptionEnregistrement, ".crp", BoxSaveFile.ext.Ajoute) Then
                    _MyWord.RemplaceTexteEntete("Original", "Duplicata")
                    _MyWord.NettoyageTexteBasPage()
                    _MyWord.AjoutTexteBasPage(Version)
                    _MyWord.CopyTo(FichierEnregSFD(0), _sKey, False)
                    AT.AT_Importation(_MyWord.Name, commentaireAT)
                    AffichageAction(BaseTxt & FichierEnregSFD(0))
                End If
            End If
        Catch ex As Exception
            AffichageAction(BaseTxt & ex.Message)
        Finally
            clear(GWord.Delete.Tous)
            AffichageAction(BaseTxt & "Opération Terminée")
        End Try
    End Sub
    Private Sub ImportationMajeure(ByVal DossierBase As Dossier, ByVal BaseTxt As String, ByVal DescriptionChargement As String, ByVal OpenByChoiceBox As Boolean, Optional ByVal commentaireAT As String = "importation majeure", Optional ByVal typeFichier As TypeFichier = main.TypeFichier.Word)
        'importation d'un fichier Word
        Dim MyType As String = _TypeFichier(typeFichier)
        Dim DossierOrigine As String = _ArchitectureDossier(DossierBase)
        Dim ChargOk As Boolean = False
        Dim AT As New ACCDBModeOp(_StrConn)

        Try

            If OpenByChoiceBox Then
                ChargOk = ChargementMyWord(MyType, DossierOrigine, DescriptionChargement, _DefaultPolice, _DefaultFStyle, TempFile)
            Else
                'OBSOLETE
                ChargOk = ChargementMyWord(MyType, DescriptionChargement, DossierOrigine, TempFile)
            End If

            'on récupère le fichier à importer puis on le charge dans GWord
            If ChargOk Then
                'on défini l'emplacement de l'enregistrement de l'original approuvé
                If BSF(_ArchitectureDossier(1), _MyWord.Name, "Emplacement Enregistrement de l'original approuvé") Then
                    'on copie le fichier en le cryptant avec une clef
                    _MyWord.RemplaceTexteEntete("Duplicata", "Original") 'au cas ou 
                    _MyWord.NettoyageTexteBasPage()
                    _MyWord.CopyTo(FichierEnregSFD(0), True)
                    AffichageAction("Importation Master Original dans " & FichierEnregSFD(0))
                Else
                    Exit Sub
                End If


                'on défini l'emplacement de l'enregistrement du duplicata
                If BSF(_ArchitectureDossier(4), _MyWord.Name, "Emplacement Enregistrement du duplicata") Then
                    With _MyWord
                        .RemplaceTexteEntete("Original", "Duplicata")
                        _MyWord.NettoyageTexteBasPage()
                        _MyWord.AjoutTexteBasPage(Version)
                        .CopyTo(FichierEnregSFD(0), _sKey)
                        AT.AT_Importation(_MyWord.Name, commentaireAT)
                        AffichageAction("Création Duplicata dans " & FichierEnregSFD(0))
                        .DeleteSourceFile()
                    End With
                Else
                    Exit Sub
                End If
            Else
                AffichageAction(BaseTxt & " Opération Annulée")
            End If
        Catch ex As Exception
            AffichageAction(BaseTxt & ex.Message)
        Finally
            clear(GWord.Delete.Tous)
            AffichageAction("Importation Master : Opération Terminée")
        End Try
    End Sub
    Private Sub TSMI_Administrateur_Importation_DepuisMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Importation_DepuisMaster.Click
        ImportationMajeure(Dossier.Modification, "Importation d'un Master", "Fichier Master Original à Importer", True, "Importation Master vers Officiel")
    End Sub
    Private Sub TSMI_Administrateur_Importation_DepuisModif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Importation_DepuisModif.Click
        'ImportationMineure(Dossier.Modification, Dossier.Officiel, "Importation Mode Op post Modification Mineure : ", "Version Nouveau Mode Op à importer", "Emplacement Enregistrement Mode Op après modification")
        ImportationMajeure(Dossier.Modification, "Importation post modification mineure", "Fichier Mode opératoire post modif mineure à Importer", True, "Importation Modif vers Officiel")
    End Sub

#End Region

#Region "Procédure Interne Interface"
    Private Sub AffichageAction(ByVal Texte As String)
        With Me.GB_Main.Controls("TXT_Action")
            .Text = Texte & vbNewLine & .Text
        End With
    End Sub
    Friend Function TempFile() As String
        Dim ok As Boolean = False
        Dim Z As Integer = 0
        Dim cpt As Integer = 0

        Do
            Try
                cpt += 1
                File.Delete(Tmp & Z)
                File.Delete(Tmp & Z & ".pdf")
                ok = True
            Catch ex As Exception
                Z += 1
                If cpt = 1000 Then Return Nothing
            End Try
        Loop While ok = False

        Return Tmp & Z
    End Function
    Private Sub clear(ByVal del As GWord.Delete)
        'supprime le word temporaire
        On Error Resume Next
        _MyWord.Dispose(del)
        _MyWord = Nothing
    End Sub
    Private Function cv(ByVal Dossier As String) As AccessFolder
        Try
            Directory.GetFiles(Dossier)
        Catch ex As Exception
            Return 0 'pas d'accès
        End Try
        Try
            Dim fsread As New FileStream(Dossier & "\test.txt", FileMode.Create)
            fsread.Dispose()
            File.Delete(Dossier & "\test.txt")
        Catch ex As Exception
            Return 1 'lecture
        End Try
        Return 2 'lecture/ecriture
    End Function
#End Region

#Region "Ouverture Fermeture Interface"
    Private Sub main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        On Error Resume Next
        _MyWord.Close()
        _MyWord = Nothing
    End Sub
    Private Sub main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If ShutDwn Then Me.Close()

    End Sub
#End Region

#Region "ChargementMyWord"
    Private Function Estcrypter(ByVal MyType As String) As Boolean
        If MyType = _TypeFichier(0) Then Return True Else Return False
    End Function
    Private Overloads Function ChargementMyWord(ByVal MyType As String, ByVal Titre As String, ByVal Dossier As String, ByVal Destination As String) As Boolean
        If OFD(MyType, Titre, Dossier) Then
            Return LoadWord(Estcrypter(MyType), Destination)
        End If
        Return False
    End Function
    ''' <summary>
    ''' Chargement Word via BOF
    ''' </summary>
    Private Overloads Function ChargementMyWord(ByVal MyType As String, ByVal RepertoireBase As String, ByVal Description As String, ByVal TaillePolice As Single, ByVal FStyle As FontStyle, ByVal Destination As String) As Boolean
        If BOF(RepertoireBase, Description, TaillePolice, FStyle, MyType) Then
            Return LoadWord(Estcrypter(MyType), Destination)
        End If
        Return False
    End Function
    Private Function LoadWord(ByVal EstCrypter As Boolean, ByVal destination As String) As Boolean
        'on charge le fichier dans GWord
        If EstCrypter Then
            _MyWord = New GWord(FichierSourceOFD(0), _sKey, destination)
        Else
            _MyWord = New GWord(FichierSourceOFD(0), TempFile, _listWordProtectKey.ToArray)
        End If

        Return True
    End Function
#End Region

#Region "Boite de dialogue OFD / SFD"
    Private Sub ClearFD(ByVal cOFD As Boolean, ByVal cSFD As Boolean)
        On Error Resume Next
        If cOFD Then _FichierOFD.Clear()
        If cSFD Then _FichierSFD.Clear()
    End Sub
    Private Function BOF(ByVal RepertoireBase As String, ByVal Description As String, ByVal TaillePolice As Single, ByVal FStyle As FontStyle, ByVal ParamArray TypeFichier() As String) As Boolean
        Dim OD As New BoxOpenFile(RepertoireBase.ToString)
        ClearFD(True, False)
        With OD
            .Description(Description, TaillePolice, FStyle)
            .ChoixExtension(TypeFichier)
            .ShowDialog()

            If IsNothing(.Resultat) Then
                Return False
            Else
                _FichierOFD.Add(.Result(BoxOpenFile.Donne.NomFichierComplet))
                _FichierOFD.Add(.Result(BoxOpenFile.Donne.Fichier))
                Return True
            End If
        End With
    End Function
    Private Overloads Function BSF(ByVal RepertoireBase As String, ByVal NomFichier As String, ByVal Description As String) As Boolean
        ClearFD(False, True)
        Dim OD As New BoxSaveFile(RepertoireBase, NomFichier)
        With OD
            .Description(Description, 10, FontStyle.Italic)
            .ShowDialog()
            If IsNothing(.Resultat) Then
                Return False
            Else
                _FichierSFD.Add(.Resultat)
                Return True
            End If
        End With
    End Function
    Private Overloads Function BSF(ByVal RepertoireBase As String, ByVal NomFichier As String, ByVal Description As String, ByVal Extention As String, ByVal Action As BoxSaveFile.ext) As Boolean
        ClearFD(False, True)
        Dim OD As New BoxSaveFile(RepertoireBase, NomFichier, Extention, Action)
        With OD
            .Description(Description, 10, FontStyle.Italic)
            .ShowDialog()

            If IsNothing(.Resultat) Then
                Return False
            Else
                _FichierSFD.Add(.Resultat)
                Return True
            End If
        End With
    End Function
    Private Function OFD(ByVal Type As String, Optional ByVal Titre As String = Nothing, Optional ByVal Dossier As String = Nothing) As Boolean
        ClearFD(True, False)
        Try
            Dim _OFD As New OpenFileDialog
            With _OFD
                .Multiselect = False
                .Filter = Type
                If Not IsNothing(Titre) Then .Title = Titre
                If Not IsNothing(Dossier) Then .InitialDirectory = Dossier
                .ShowDialog()
                _FichierOFD.Add(.FileName)
                _FichierOFD.Add(.SafeFileName)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SFD(ByVal Type As String, Optional ByVal Titre As String = Nothing, Optional ByVal Dossier As String = Nothing, Optional ByVal NomFichier As String = Nothing) As Boolean
        ClearFD(False, True)
        Try
            Dim _SFD As New SaveFileDialog
            With _SFD
                .Filter = Type.ToString
                If Not IsNothing(Titre) Then .Title = Titre
                If Not IsNothing(Dossier) Then .InitialDirectory = Dossier.ToString
                .FileName = NomFichier
                .ShowDialog()
                _FichierSFD.Add(.FileName)
                Return True
            End With
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Evènement Autres"
    Private Sub TSMI_Outils_AuditTrails_Impression_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Outils_AuditTrails_Impression.Click
        Dim AT As New AuditTrails(_StrConn, AuditTrails.ModeVisu.Impression) With {.CanAdd = False, _
                                                                                    .CanDelete = False, _
                                                                                    .ModeAffichage = DataGridViewAutoSizeColumnsMode.Fill, _
                                                                                   .Titre = "Audit Trails Impression", _
                                                                                   .Text = "Les informations de l'audit trails sont en lecture seule, cependant, vous pouvez ajouter un commentaire dans la colonne RevuAQ et checker la ligne" & vbNewLine & "N'oublié pas d'enregistrer sinon vos 'checks' et vos commentaires ne seront pas pris en compte"}
        AT.ReadOnlyColumns(0, 1, 2, 3, 4, 5, 6, 7, 8, 9) = True
        AT.VisibleColumns(8) = False
        AT.Show()

    End Sub
    Private Sub TSMI_Outils_AuditTrails_Exportation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Outils_AuditTrails_Exportation.Click
        Dim AT As New AuditTrails(_StrConn, AuditTrails.ModeVisu.Exportation) With {.CanAdd = False, _
                                                                                    .CanDelete = False, _
                                                                                    .ModeAffichage = DataGridViewAutoSizeColumnsMode.Fill, _
                                                                                    .Titre = "Audit Trails Exportation", _
                                                                                    .Text = "Les informations de l'audit trails sont en lecture seule, cependant, vous pouvez ajouter un commentaire dans la colonne RevuAQ et checker la ligne" & vbNewLine & "N'oublié pas d'enregistrer sinon vos 'checks' et vos commentaires ne seront pas pris en compte"}
        AT.ReadOnlyColumns(0, 1, 2, 3, 4) = True
        AT.Show()
    End Sub
    Private Sub TSMI_Outils_AuditTrails_Importation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Outils_AuditTrails_Importation.Click
        Dim AT As New AuditTrails(_StrConn, AuditTrails.ModeVisu.Importation) With {.CanAdd = False, _
                                                                                    .CanDelete = False, _
                                                                                    .ModeAffichage = DataGridViewAutoSizeColumnsMode.Fill, _
                                                                                    .Titre = "Audit Trails Importation", _
                                                                                    .Text = "Les informations de l'audit trails sont en lecture seule, cependant, vous pouvez ajouter un commentaire dans la colonne RevuAQ et checker la ligne" & vbNewLine & "N'oubliez pas d'enregistrer sinon vos 'checks' et vos commentaires ne seront pas pris en compte"}
        AT.ReadOnlyColumns(0, 1, 2, 3, 4) = True
        AT.Show()
    End Sub
    Private Sub TSMI_Outils_Parametre_PhrasePredef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Outils_Parametre_PhrasePredef.Click
        Dim T As New DataBDD(_StrConn) With {.CanDelete = True, _
                                             .CanAdd = True, _
                                             .Text = "Définition de la liste de choix des phrases lors de l'audit trails d'impression"}
        T.LoadTable("ATe")
        T.VisibleColumns(0, 1) = False
        T.Show()
    End Sub
    Private Sub TSMI_Outils_Parametre_DroitUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Outils_Parametre_DroitUser.Click
        Dim T As New DataBDD(_StrConn) With {.CanDelete = False, _
                                             .CanAdd = True, _
                                             .Text = "La case à cocher à droite correspond aux droits utilisateurs les plus importants" & _
                                             vbNewLine & "Les logins en bleu sont les administrateurs", _
                                             .Titre = "Droits Utilisateurs"}
        T.LoadTable("DroitUser")
        T.ParamRows(6, "true", True, Color.LightBlue, True)
        T.VisibleColumns(0, 6) = False
        T.Show()

    End Sub
    Private Sub TSMI_Developpeur_ConfigINI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Developpeur_ConfigINI.Click
        Dim T As New GConfigINI(FichierINI, IniMDP)
        T.Show()
    End Sub
    Private Sub TSMI_Developpeur_GestionDroitUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Developpeur_GestionDroitUser.Click
        Dim T As New DataBDD(_StrConn) With {.CanDelete = True, _
                                     .CanAdd = True, _
                                     .Text = "La case à cocher à droite correspond aux droits utilisateurs les plus importants" _
                                            & vbNewLine & "Les Admins sont en bleu", _
                                             .Titre = "Droits Utilisateurs"}
        T.LoadTable("DroitUser")
        T.ParamRows(6, "true", ColorRow:=Color.LightBlue)
        T.Show()
    End Sub
    Private Sub TSMI_Developpeur_OpenAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Developpeur_OpenAccess.Click
        Dim BDDname As String = FindInProvider("Data source")
        Dim passW As String = FindInProvider("password")

        If File.Exists(BDDname) Then
            Dim Db As New Access.Application
            Db.OpenCurrentDatabase(BDDname, MsgBox("Ouvrir la base de donnée en mode exclusif?", MsgBoxStyle.YesNo, "Mode Exclusif"), passW)
            Db.Visible = True
        End If
    End Sub
    Private Function FindInProvider(ByVal Key As String) As String
        Dim str() As String = _StrConn.Split("=")
        Dim index As Integer = 0

        For i = 0 To str.Count - 2
            If str(i).Contains(Key) Then
                index = i + 1
                Exit For
            End If
        Next
        If index = 0 Then Return Nothing
        Return str(index).Split(";")(0)
    End Function
    Private Sub TSMI_Developpeur_EtatModeOp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Developpeur_EtatModeOp.Click
        If File.Exists(ListeModeOp) Then
            Dim Flux As String = File.ReadAllText(ListeModeOp)
            Dim liste() As String = Split(Flux, Chr(10))
            Dim CF As New CheckFichier(_ArchitectureDossier(Dossier.Officiel), liste.ToArray) With {.Extention = FichierWord, .ListMotPasse = _listWordProtectKey, _
                                                                                                      .sKey = _sKey, .Strconn = _StrConn}
            CF.Show()
        Else
            MsgBox("Fichier Absent : " & ListeModeOp)
        End If


    End Sub
    Private Sub TSMI_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Info.Click
        With My.Application.Info
            Dim txt As String
            txt = .AssemblyName & vbNewLine & vbNewLine
            txt += .Description & vbNewLine & vbNewLine
            txt += .Copyright & vbNewLine
            txt += "Version : " & .Version.ToString

            MessageBox.Show(txt, .Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End With
    End Sub
#End Region

#Region "Evenement redimentionnement fenetre"
    Private Sub main_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        On Error Resume Next
        If WindowState = 2 Then _DimForm.GetAbsValue(Me, Me.GB_Main, Me.TXT_Action)
    End Sub
    Private Sub main_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        On Error Resume Next
        _DimForm.GetAbsValue(Me, Me.GB_Main, Me.TXT_Action)
    End Sub
#End Region
End Class
