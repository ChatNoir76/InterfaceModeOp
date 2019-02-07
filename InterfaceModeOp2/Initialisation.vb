<Assembly: Reflection.AssemblyVersion("2.0.0.0")> 

Public Class Initialisation

    Public __User As New Utilisateur

    Sub New()

        InitializeComponent()

        __User.setArchDossProd = New ArchDossProd(Configuration.getInstance.getWorkDir)

        remplissageLabel()

    End Sub

    Private Sub remplissageLabel()
        With Configuration.getInstance
            LBL_DossierBase.Text = .GetValueFromKey(service.INI_KEY_REPBASE)
            LBL_DossierA.Text = .GetValueFromKey(service.DossierProd.DossierA.ToString)
            LBL_DossierB.Text = .GetValueFromKey(service.DossierProd.DossierB.ToString)
            LBL_DossierC.Text = .GetValueFromKey(service.DossierProd.DossierC.ToString)
            LBL_DossierD.Text = .GetValueFromKey(service.DossierProd.DossierD.ToString)
            LBL_DossierE.Text = .GetValueFromKey(service.DossierProd.DossierE.ToString)
            LBL_DossierF.Text = .GetValueFromKey(service.DossierProd.DossierF.ToString)
            LBL_DirAbs_Work.Text = .getWorkDir
        End With

        LBL_Droit_A.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierA).ToString
        LBL_Droit_B.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierB).ToString
        LBL_Droit_C.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierC).ToString
        LBL_Droit_D.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierD).ToString
        LBL_Droit_E.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierE).ToString
        LBL_Droit_F.Text = __User.getArchDossProd.getAccess(service.DossierProd.DossierF).ToString
        LBL_Username.Text = __User.getNom
        LBL_Computer.Text = __User.getPC

    End Sub

#Region "Evènements sur boutons"
    ''' <summary>
    ''' Ouverture de la page principale de l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BT_Open_Click() Handles BT_Open.Click
        controleur.gotoView(service.View.Principale)
    End Sub
#End Region


End Class
