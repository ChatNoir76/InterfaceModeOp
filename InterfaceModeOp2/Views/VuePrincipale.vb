
Public Class vuePrincipale

    Private Shared _Instance As vuePrincipale = Nothing
    Private Shared ReadOnly mylock As New Object()

    Private Const _ESPACELIGNE As String = "___________________________________________________"
    

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
    Public Sub AffichageTexteVuePrin(ByVal monTexte As String, Optional ByVal InsertionLigne As Boolean = False)
        Me.TXT_Action.Text = monTexte & If(InsertionLigne, vbNewLine & _ESPACELIGNE & vbNewLine, vbNewLine) & Me.TXT_Action.Text
    End Sub
#End Region

#Region "Evenement Menu/interface"
    Private Sub TSMI_Utilisateur_Consultation_Officiel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Officiel.Click
        WAction.doAction(service.Action.ConsultationOfficiel)
    End Sub

    Private Sub TSMI_Utilisateur_Consultation_Archive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSMI_Utilisateur_Consultation_Archive.Click
        WAction.doAction(service.Action.ConsultationArchive)
    End Sub

#End Region

#Region "FERMETURE INTERFACE"
    Private Sub FermetureInterface() Handles Me.FormClosed
        WReader.Dispose()
        Initialisation.Close()
    End Sub
#End Region



End Class