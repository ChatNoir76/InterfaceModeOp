﻿Imports ValdepharmTool.GGridView

Public Class VueAuditrails

    Private Const AFFICHAGE_VUE = "Visualisation de la vue : {0}"
    Private Const SELECTION = "Sélection de {0} n°{1}"
    Private Const CHG_VERIFICATION = "Vérification de {0} : {1}"
    Private Const CHG_DROITS = "{0} change les droits : {1}"

    Private Const VUE_ATPRINTER = "Audit trails d'impression pour mode opératoire"
    Private Const VUE_ATPRINTER_SIGNETS = "Signet d'impression pour mode opératoire"
    Private Const VUE_ATIEA = "Audit trails d'Importation/Exportation/Archivage"
    Private Const VUE_USERS = "Liste des utilisateurs"
    Private Const VUE_NOVIEW = "Pas de Vue"

    Private _DGVMainSize As Size
    Private _canResize = False
    Private _Vue_Encours As DAOViews.views
    Private _Vue_Precedente As DAOViews.views = DAOViews.views.PasDeVue

#Region "Constructeur"
    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        With DGV_Main
            'l'utilisateur ne peux pas supprimer/ajouter de données
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False
            .MultiSelect = True
            .ReadOnly = True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            _DGVMainSize = New Size(Me.Width - .Width, Me.Height - .Height)
            _canResize = True
            TSMI_TOOLS_PRINTSEL.Visible = False
            changerVue(DAOViews.views.PasDeVue)
        End With

    End Sub
#End Region

#Region "Evènement Menu"
    Private Sub Vue_ATImpression(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_ATPrinter.Click
        changerVue(DAOViews.views.ATPrinter)
    End Sub
    Private Sub Vue_Rien(ByVal sender As System.Object, ByVal e As System.EventArgs)
        changerVue(DAOViews.views.PasDeVue)
    End Sub
    Private Sub TSMI_LISTEUSER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_LISTEUSER.Click
        changerVue(DAOViews.views.ListUser)
    End Sub
    Private Sub TSMI_ATAutre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_ATAutre.Click
        changerVue(DAOViews.views.ATImportExportArchivage)
    End Sub
    Private Sub ConfigMenuSelection(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_Main.CellClick
        'DGV_Main.Rows.Item(e.RowIndex).Cells(e.ColumnIndex).Value
        'sélection d'une ligne complète
        If e.ColumnIndex = -1 And e.RowIndex > -1 Then
            TSMI_TOOLS_PRINTSEL.Visible = True
            TSMI_Selection.Visible = True
            Dim maVue As DAOViews.views = _Vue_Encours

            Select Case maVue
                Case DAOViews.views.ATPrinter
                    gestionMenuSelection(True, False, True, False, True)
                    TSMI_Selection.Text = String.Format(SELECTION, "l'audit trails d'impression", DGV_Main.Rows.Item(e.RowIndex).Cells(0).Value)
                Case DAOViews.views.ATImportExportArchivage
                    gestionMenuSelection(True, True, False, False, True)
                    TSMI_Selection.Text = String.Format(SELECTION, "l'audit trails", DGV_Main.Rows.Item(e.RowIndex).Cells(0).Value)
                Case DAOViews.views.ListUser
                    gestionMenuSelection(True, True, False, True, False)
                    TSMI_Selection.Text = String.Format(SELECTION, "l'utilisateur", DGV_Main.Rows.Item(e.RowIndex).Cells(0).Value)
                Case Else
                    TSMI_Selection.Visible = False
            End Select
        Else
            TSMI_Selection.Visible = False
            TSMI_TOOLS_PRINTSEL.Visible = False
        End If
    End Sub
    Private Sub Imprimer_DGV(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_TOOLS_PRINTAB.Click
        Dim DGV As New PrintDataGridView(DGV_Main)
        DGV.Impression(PrintDataGridView.Affichage.Defaut)
    End Sub
    Private Sub TSMI_TOOLS_PRINTSEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_TOOLS_PRINTSEL.Click
        Dim DGV As New PrintDataGridView(DGV_Main)
        DGV.Impression(PrintDataGridView.Affichage.Selection)
    End Sub
    Private Sub VuePrécédenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_TOOLS_VUEOLD.Click
        changerVue(_Vue_Precedente, True)
    End Sub
#End Region

#Region "Redimensionnement de la fenetre"
    Private Sub VueAuditrails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If _canResize Then
            'Gestion du DatagridView1
            With DGV_Main
                .Size = New Size(Me.Width - _DGVMainSize.Width, Me.Height - _DGVMainSize.Height)
            End With
        End If
    End Sub
#End Region

#Region "Méthode Interne"
    Private Sub changerVue(ByVal maVue As DAOViews.views, Optional ByVal reset As Boolean = False)
        If reset Then
            _Vue_Precedente = DAOViews.views.PasDeVue
            _Vue_Encours = maVue
        Else
            _Vue_Precedente = _Vue_Encours
            _Vue_Encours = maVue
        End If


        TSMI_Selection.Visible = False

        Select Case maVue
            Case DAOViews.views.ATPrinter
                DGV_Main.DataSource = DAOFactory.getViews.getAll(maVue, VUE_ATPRINTER)
            Case DAOViews.views.ATImportExportArchivage
                DGV_Main.DataSource = DAOFactory.getViews.getAll(maVue, VUE_ATIEA)
            Case DAOViews.views.ListUser
                DGV_Main.DataSource = DAOFactory.getViews.getAll(maVue, VUE_USERS)
            Case DAOViews.views.ATPrinter_Signet
                DGV_Main.DataSource = DAOFactory.getViews.getById(DGV_Main.SelectedCells(0).Value, DAOViews.reqById.GetSignet, VUE_ATPRINTER_SIGNETS)
                'on cache la 2e colonnes id_auditrails
                DGV_Main.Columns(1).Visible = False
            Case Else
                DGV_Main.DataSource = Nothing
        End Select

        If maVue <> DAOViews.views.PasDeVue Then
            'cache la 1ere colonnes contenant l'id avec le vrai nom de colonne
            DGV_Main.Columns(0).Visible = False
        End If

        Me.lbl_Vue.Text = String.Format(AFFICHAGE_VUE,
                                        If(IsNothing(DGV_Main.DataSource),
                                            VUE_NOVIEW,
                                            DGV_Main.DataSource.ToString))

        If _Vue_Precedente = DAOViews.views.PasDeVue Then
            Me.TSMI_TOOLS_VUEOLD.Visible = False
        Else
            Me.TSMI_TOOLS_VUEOLD.Visible = True
        End If
    End Sub
    Private Sub gestionMenuSelection(ByVal commentaire As Boolean, ByVal historique As Boolean, ByVal signets As Boolean, ByVal chgDroit As Boolean, ByVal chgVerif As Boolean)
        TSMI_Selection_Commentaire.Visible = commentaire
        TSMI_Selection_Historique.Visible = historique
        TSMI_Selection_Signets.Visible = signets
        TSMI_Selection_ChangementDroit.Visible = chgDroit
        TSMI_Selection_Verification.Visible = chgVerif
    End Sub
#End Region

#Region "Gestion du tri"
    Private Sub DGV_Main_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_Main.CellDoubleClick
        If e.ColumnIndex > -1 And e.RowIndex > -1 Then
            For Each element As DataGridViewRow In DGV_Main.Rows
                If element.Cells(e.ColumnIndex).Value() <> DGV_Main.Rows.Item(e.RowIndex).Cells(e.ColumnIndex).Value Then
                    element.Visible = False
                End If
            Next
        End If
    End Sub
#End Region

#Region "Changement Statut de vérification"
    Private Sub SetStatut(ByVal newStatut As Verification)
        Dim message As String = String.Empty

        message = InputBox("Indiquer ici les raisons du changement", "Changement du statut de vérification")

        If Not String.Empty = message Then
            DAOFactory.getHistoVerification.dbInsert(New HistoVerification(String.Format(CHG_VERIFICATION, Environment.UserName.ToUpper, message), Now, DGV_Main.SelectedCells(0).Value, newStatut))
        Else
            MessageBox.Show("Annulé")
        End If
        changerVue(_Vue_Encours)
    End Sub
    Private Sub TSMI_Selection_Verification_AVerif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_Verification_AVerif.Click
        SetStatut(VERIFICATION.NoVerif)
    End Sub
    Private Sub TSMI_Selection_Verification_EnCours_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_Verification_EnCours.Click
        SetStatut(Verification.EnCours)
    End Sub
    Private Sub TSMI_Selection_Verification_VerifOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_Verification_VerifOK.Click
        SetStatut(Verification.VerifOK)
    End Sub
#End Region

#Region "Changement de statut Droit utilisateurs"
    Private Sub setDroitUser(ByVal drt As Droits)
        Dim message As String = String.Empty

        message = InputBox("Indiquer ici les raisons du changement", "Changement des droits utilisateurs")

        If Not String.Empty = message Then
            DAOFactory.getHistoDroit.dbInsert(New HistoDroits(String.Format(CHG_DROITS, Environment.UserName.ToUpper, message), Now, DGV_Main.SelectedCells(0).Value, drt))
        Else
            MessageBox.Show("Annulé")
        End If
        changerVue(_Vue_Encours)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_NoUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_NoUse.Click
        setDroitUser(Droits.NoUse)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_Guest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_Guest.Click
        setDroitUser(Droits.Guest)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_User_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_User.Click
        setDroitUser(Droits.User)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_KeyUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_KeyUser.Click
        setDroitUser(Droits.KeyUser)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_UserAQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_UserAQ.Click
        setDroitUser(Droits.UserAQ)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_AdminAQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_AdminAQ.Click
        setDroitUser(Droits.AdminAQ)
    End Sub
    Private Sub TSMI_Selection_ChangementDroit_AdminDvlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_ChangementDroit_AdminDvlp.Click
        setDroitUser(Droits.AdminDvlp)
    End Sub
#End Region

#Region "Affichage des signets"
    Private Sub TSMI_Selection_Signets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Selection_Signets.Click
        changerVue(DAOViews.views.ATPrinter_Signet)
    End Sub
#End Region

End Class