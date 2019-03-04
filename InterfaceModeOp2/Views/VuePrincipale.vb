
Public Class vuePrincipale

    Private Shared _Instance As vuePrincipale = Nothing
    Private Shared ReadOnly mylock As New Object()

    Private Const _ESPACELIGNE As String = "___________________________________________________"
    Private Const _MAX_CHAR_AFFICHAGE As Integer = 1024

#Region "Constructeur"
    Private Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        GestionMenuDroitUser()
        Me.TXT_LoginUtilisateur.Text = Initialisation.__User.getNom
        Me.TXT_Droits.Text = Initialisation.__User.getDroitReel.ToString
        Me.TXT_Action.Text = "Initialisation OK"
#If DEBUG Then
        Me.Info("--[MODE DEBUG]--", True)
#End If
    End Sub

    Private Sub GestionMenuDroitUser()
        Select Case Initialisation.__User.getDroitReel
            Case service.DroitUser.Guest 'consultation
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur)
                GestionMenu(False, TSMI_Utilisateur_Impression)
            Case service.DroitUser.User 'GUEST +impression
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur)
            Case service.DroitUser.KeyUser 'USER +Export
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur, TSMI_Administrateur)
                GestionMenu(False, TSMI_Administrateur_Importation)
            Case service.DroitUser.UserAQ 'USER +outils
                GestionMenu(False)
                GestionMenu(True, TSMI_Utilisateur, TSMI_Outils)
                GestionMenu(False, TSMI_Outils_Parametre_DroitUser)
            Case service.DroitUser.AdminAQ '+ gestion droit utilisateur
                GestionMenu(False, TSMI_Developpeur)
            Case service.DroitUser.AdminDvlp
                GestionMenu(True)
            Case Else
                GestionMenu(False)
                Info("Droits indéterminés")
        End Select
        GestionMenu(True, TSMI_Info)
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

    'accesseur de la vue principale
    Public Shared Function getVP() As vuePrincipale
        SyncLock (mylock)
            If IsNothing(_Instance) Then
                _Instance = New vuePrincipale()
            End If
            Return _Instance
        End SyncLock
    End Function
#End Region

#Region "Méthodes Externes"
    ''' <summary>
    ''' Affichage des informations à destination de l'utilisateur dans le TextBox 
    ''' </summary>
    ''' <param name="monTexte">Information à ajouter</param>
    ''' <param name="NouveauBloc">Défini un nouveau bloc d'information et permet d'effacer le TextBox si surchargé</param>
    ''' <remarks></remarks>
    Public Sub Info(ByRef monTexte As String, Optional ByRef NouveauBloc As Boolean = False)
        Dim Affichage As New System.Text.StringBuilder(monTexte)
#If DEBUG Then
        NouveauBloc = False
#End If
        With Affichage
            .AppendLine()
            If NouveauBloc Then
                .Append(_ESPACELIGNE).AppendLine()
                If Me.TXT_Action.Text.Length >= _MAX_CHAR_AFFICHAGE Then Me.TXT_Action.Clear()
            End If
            .Append(Me.TXT_Action.Text)
            Me.TXT_Action.Text = .ToString
        End With
    End Sub
#End Region

#Region "Evenement Menu/interface"
    Private Sub TSMI_Utilisateur_Consultation_Officiel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Officiel.Click
        WAction.doAction(service.Action.ConsultationOfficiel)
    End Sub
    Private Sub TSMI_Utilisateur_Consultation_Archive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Archive.Click
        WAction.doAction(service.Action.ConsultationArchive)
    End Sub
    Private Sub TSMI_Utilisateur_Impression_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Impression.Click
        WAction.doAction(service.Action.Impression)
    End Sub
    Private Sub TSMI_Administrateur_Importation_DepuisModif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Importation_DepuisModif.Click
        WAction.doAction(service.Action.Importation)
    End Sub
    Private Sub TSMI_Administrateur_Archivage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Archivage.Click
        WAction.doAction(service.Action.Archivage)
    End Sub
    Private Sub TSMI_Administrateur_Exportation_Officiel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Exportation_Officiel.Click
        WAction.doAction(service.Action.ExportationOfficiel)
    End Sub
    Private Sub TSMI_Administrateur_Exportation_Archive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Administrateur_Exportation_Archive.Click
        WAction.doAction(service.Action.ExportationArchive)
    End Sub
    Private Sub TSMI_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Info.Click
        With My.Application.Info
            Dim mesInfo As New System.Text.StringBuilder(.AssemblyName)
            mesInfo.AppendLine.AppendLine()
            mesInfo.Append(.Copyright).AppendLine()
            mesInfo.Append(String.Format("Version {0}.{1:00}", .Version.Major, .Version.Minor))
#If DEBUG Then
            mesInfo.AppendLine.Append("(Mode Debug)")
#End If
            MessageBox.Show(mesInfo.ToString, .Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End With
    End Sub
#End Region

#Region "FERMETURE INTERFACE"
    Private Sub FermetureInterface() Handles Me.FormClosed
        WReader.Dispose()
        Initialisation.Close()
    End Sub
#End Region

#Region "Méthode Interne"
    Private Sub vuePrincipale_Resize() Handles MyBase.Resize
        Me.GB_Main.Size = New Size(Me.Width - 30, Me.Height - 185)
        Me.TXT_Action.Size = New Size(Me.Width - 44, Me.Height - 210)
    End Sub
#End Region
End Class