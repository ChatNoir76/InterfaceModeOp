
Public Class vuePrincipale

    Private Shared _Instance As vuePrincipale = Nothing
    Private Shared ReadOnly mylock As New Object()

    Private Const _ESPACELIGNE As String = "___________________________________________________"
    Private Const _MAX_CHAR_AFFICHAGE As Integer = 255

#Region "Constructeur"
    Private Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Me.TXT_LoginUtilisateur.Text = Initialisation.__User.getNom
        Me.TXT_Droits.Text = Initialisation.__User.getDroitReel.ToString
        Me.TXT_Action.Text = "Initialisation OK"
    End Sub

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
    Public Sub AffichageTexteVuePrin(ByRef monTexte As String, Optional ByRef NouveauBloc As Boolean = False)
        Dim Affichage As New System.Text.StringBuilder(monTexte)
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