<Assembly: Reflection.AssemblyVersion("2.0.0.0")> 

Public Class Initialisation
    'à l'initialisation récupère les données utilisateurs (nom windows)
    Public __User As New UtilisateurOLD
    Private _openBT As Boolean = True

    Sub New()

        InitializeComponent()

        Init()

        'si mode debug, change la config déterminé par la méthode init()
#If DEBUG Then
        ChangementConfiguration()
#End If
        'les infos sont ajoutées à la page
        remplissageLabel()

    End Sub

    Private Sub Init()
        Try
            'détermine les droits de l'utilisateur sur les dossiers de prod
            __User.setArchDossProd = New ArchDossProd()
            Me.BT_Open.Visible = False
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la détermination des droits utilisateurs sur les dossiers du répertoire de production" & vbNewLine & ex.Message, "Erreur à l'initialisation", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        Try
            'détermine les droits de l'utilisateur sur les dossiers de prod
            If Not System.IO.File.Exists(Configuration.getInstance.GetValueFromKey(Singleton.DBFOLDER)) Then
                Throw New Exception("La base de donnée n'a pas été trouvée")
            End If
            If IsNothing(DAOFactory.getConnexion()) Then
                Throw New Exception("Pas de connexion avec la base de donnée")
            End If
        Catch ex As Exception
            _openBT = False
            MessageBox.Show("Erreur lors de la connexion à la base de donnée" & vbNewLine & ex.Message, "Erreur à l'initialisation", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub ChangementConfiguration()

        'le profil utilisateur est défini sur
        __User.setDroitUser = service.DroitUser.AdminDvlp

        Exit Sub

        'son env. de prod est
        __User.setArchDossProd = New ArchDossProd("222222")

        'son droit est donc
        Debug.Print("Droit reel = " & __User.getDroitReel.ToString)

    End Sub

    Private Sub remplissageLabel()
        With Configuration.getInstance
            TXT_RepBase.Text = .GetValueFromKey(service.INI_KEY_REPBASE)
            LBL_DossierA.Text = .GetValueFromKey(service.DossierProd.DossierA.ToString)
            LBL_DossierB.Text = .GetValueFromKey(service.DossierProd.DossierB.ToString)
            LBL_DossierC.Text = .GetValueFromKey(service.DossierProd.DossierC.ToString)
            LBL_DossierD.Text = .GetValueFromKey(service.DossierProd.DossierD.ToString)
            LBL_DossierE.Text = .GetValueFromKey(service.DossierProd.DossierE.ToString)
            LBL_DossierF.Text = .GetValueFromKey(service.DossierProd.DossierF.ToString)
        End With

        LBL_Droit_A.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierA).ToString
        LBL_Droit_B.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierB).ToString
        LBL_Droit_C.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierC).ToString
        LBL_Droit_D.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierD).ToString
        LBL_Droit_E.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierE).ToString
        LBL_Droit_F.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierF).ToString
        TXT_Username.Text = __User.getNom
        TXT_PC.Text = __User.getPC
        TXT_Droit.Text = __User.getDroitReel.ToString

        If _openBT Then
            Me.BT_Open.Visible = __User.getDroitReel > service.DroitUser.NoUse
        End If

    End Sub

#Region "Evènements sur boutons"
    ''' <summary>
    ''' Ouverture de la page principale de l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BT_Open_Click() Handles BT_Open.Click
        Me.BT_Open.Visible = False
        Me.Visible = False
#If DEBUG Then
        Me.Visible = True
#End If
        vuePrincipale.getVP.Show()
    End Sub
#End Region
End Class
