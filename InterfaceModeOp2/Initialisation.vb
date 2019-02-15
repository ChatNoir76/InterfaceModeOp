<Assembly: Reflection.AssemblyVersion("2.0.0.0")> 

Public Class Initialisation

    Public __User As New Utilisateur

    Sub New()

        InitializeComponent()

        Init()

#If DEBUG Then
        ChangementConfiguration()
#End If

        remplissageLabel()

    End Sub

    Private Sub Init()

        __User.setArchDossProd = New ArchDossProd()
        Me.BT_Open.Visible = False

    End Sub

    Private Sub ChangementConfiguration()

        'le profil utilisateur est défini sur
        __User.setDroitUser = service.DroitUser.AdminDvlp

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

        Me.BT_Open.Visible = __User.getDroitReel > service.DroitUser.NoUse

    End Sub

#Region "Evènements sur boutons"
    ''' <summary>
    ''' Ouverture de la page principale de l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BT_Open_Click() Handles BT_Open.Click
        Me.BT_Open.Visible = False
        controleur.gotoView(service.View.Principale)
    End Sub
#End Region

End Class
