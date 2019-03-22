<Assembly: Reflection.AssemblyVersion("2.0.0.0")> 
<Assembly: Reflection.AssemblyFileVersion("2.0.0.0")> 
<Assembly: Reflection.AssemblyInformationalVersion("VERSION 2 TEST ")> 

Public Class Initialisation
    'à l'initialisation récupère les données utilisateurs (nom windows)
    Public __User As UserConfig
    Private _BTVisible As Boolean = True

    Private Const _BDD_NOUSER = "l'utilisateur {0} n'est pas présent dans la base de donnée"
    Private Const _BDD_NOFILE = "La base de donnée n'a pas été trouvée"
    Private Const _BDD_NOCONN = "Pas de connexion avec la base de donnée"
    Private Const _BDD_ERRCONN = "Erreur lors de la connexion à la base de donnée"
    Private Const _BDD_GETDROIT = "La BDD renvoie les droits {0} pour l'utilisateur {1}"
    Private Const _INI_CHECKFOLDER = "Erreur lors de la configuration utilisateur"
    Private Const _INI_TEST = "Test lecture / écriture des dossiers de production"
    Private Const _INI_NOFOLDERNAME = "N/A"
    Private Const _INI_OUV = "Ouverture de l'interface"
    Private Const _INI_FERM = "L'interface à été vérouillée"

#Region "Constructeur"
    Sub New()

        InitializeComponent()

        ConfigurationInterface()

#If DEBUG Then
        'si mode debug, change la config déterminé par la méthode init()
        DebugConfiguration(0, Droits.AdminDvlp, "222222")
#End If

        'les infos sont ajoutées à la page
        ResumeInfo()

    End Sub
#End Region

#Region "Configuration"
    Private Sub ConfigurationInterface()
        Try
            'détermine les droits de l'utilisateur sur les dossiers de prod
            'en réalisant des tests de lectures écritures sur chaques dossiers
            __User = New UserConfig
        Catch ex As Exception
            _BTVisible = False
            Info(_INI_CHECKFOLDER & vbNewLine & ex.Message, True)
            Exit Sub
        End Try

        Try
            'vérification de la présence de la base de données
            'le chemin est défini dans fichier ini
            If Not System.IO.File.Exists(Configuration.getInstance.GetValueFromKey(Singleton.DBFOLDER)) Then
                Throw New Exception(_BDD_NOFILE)
            End If
            If IsNothing(DAOFactory.getConnexion()) Then
                Throw New Exception(_BDD_NOCONN)
            End If
        Catch ex As Exception
            _BTVisible = False
            Info(_BDD_ERRCONN & vbNewLine & ex.Message, True)
            Exit Sub
        End Try

        Try
            'détermination des droits via bdd
            Dim monUser As Utilisateur = DAOFactory.getUtilisateur.dbGetByName(__User.getUserName)
            If IsNothing(monUser) Then
                'l'utilisateur n'est pas présent dans la bdd
                __User.setDroitUser = Droits.Guest
                Info(String.Format(_BDD_NOUSER, __User.getUserName))
            Else
                'l'utilisateur est présent et à des droits
                __User.setUserId = monUser.idUtilisateur
                __User.setDroitUser = DAOFactory.getHistoDroit.dbGetLastStatutById(monUser.idUtilisateur).getIdDroit
            End If
            Info(String.Format(_BDD_GETDROIT, __User.getDroitBDD, __User.getUserName))
        Catch ex As Exception
            _BTVisible = False
            Info("" & vbNewLine & ex.Message, True)
            Exit Sub
        End Try
    End Sub
    ''' <summary>
    ''' 1 : DroitBDD
    ''' 2 : envTravail
    ''' les deux 1+2 = 3
    ''' </summary>
    ''' <param name="level"></param>
    ''' <remarks></remarks>
    Private Sub DebugConfiguration(ByVal level As Byte, ByVal bdd As Droits, ByVal envT As String)
        Info()
        If level >= 2 Then
            level -= 2
            'son env. de prod est
            __User.setArchDossProd = envT
            Dim env As New System.Text.StringBuilder()
            For i = 1 To 6
                With env
                    .Append("[")
                    .Append(__User.getArchDossProd.getAccess(i))
                    .Append("]")
                End With
            Next
            Info("Mode Debug force l'environnement de travail à " & env.ToString)
        End If

        If level >= 1 Then
            level -= 1
            'le profil utilisateur est défini sur
            __User.setDroitUser = bdd
            Info("Mode Debug force les droits utilisateur à " & __User.getDroitBDD.ToString)
        End If

    End Sub
    Private Sub ResumeInfo()
        TXT_Username.Text = __User.getUserName
        TXT_PC.Text = __User.getPC
        TXT_RepBase.Text = Configuration.getInstance.GetValueFromKey(Outils.INI_KEY_REPBASE)
        TXT_Droit.Text = __User.getDroitDetermine.ToString

        Dim ListeDossier = [Enum].GetValues(GetType(Outils.DossierProd))
        Info()
        For Each dossier As Outils.DossierProd In ListeDossier
            Dim infostr As New System.Text.StringBuilder
            Dim nom As String
            With infostr
                .Append(dossier.ToString)
                .Append(" : ")
                nom = Configuration.getInstance.GetValueFromKey(dossier.ToString)
                .Append(If(nom = "", _INI_NOFOLDERNAME, nom))
                .Append(" [")
                .Append(__User.getArchDossProd.getAccess(dossier).ToString)
                .Append("]")

                Info(.ToString)

            End With
        Next
        Info(_INI_TEST)

        If __User.getDroitDetermine <= Droits.NoUse Then
            _BTVisible = False
        End If

        Me.BT_Open.Visible = _BTVisible
        Info()
        Info(If(_BTVisible, _INI_OUV, _INI_FERM))

    End Sub
    Private Sub Info(Optional ByVal txt As String = Nothing, Optional ByVal sautLigne As Boolean = False)
        Dim mesInfos As New System.Text.StringBuilder()
        With mesInfos
            If sautLigne Then .AppendLine()
            If Not IsNothing(txt) Then
                .Append(txt)
            End If
            If sautLigne Then .AppendLine()
            .AppendLine()
            .Append(TXT_InfoINI.Text)
        End With
        TXT_InfoINI.Text = mesInfos.ToString
    End Sub
#End Region

#Region "Evènements sur boutons"
    ''' <summary>
    ''' Ouverture de la page principale de l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BT_Open_Click() Handles BT_Open.Click
        Me.BT_Open.Visible = False
        Me.Visible = False
        vuePrincipale.getVP.Show()
    End Sub
#End Region
End Class
