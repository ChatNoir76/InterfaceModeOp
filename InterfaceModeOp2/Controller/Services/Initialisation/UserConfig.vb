Public Class UserConfig

    Private _utilisateur As Utilisateur
    'pc utilisateur
    Private _nomPC As String
    'droit architecture de production
    Private _archiDossProd As ArchDossProd
    'droit acces interface
    Private _DroitReelInterface As Outils.DroitUser = Outils.DroitUser.Guest
    'droit défini dans la base de données
    Private _DroitBDD As Outils.DroitUser = Outils.DroitUser.Guest

#Region "Property"
    'GETTER
    Public ReadOnly Property getUserName() As String
        Get
            Return _utilisateur.getNomUtilisateur
        End Get
    End Property
    Public ReadOnly Property getUserId() As Double
        Get
            Return _utilisateur.idUtilisateur
        End Get
    End Property
    Public ReadOnly Property getPC() As String
        Get
            Return _nomPC
        End Get
    End Property
    Public ReadOnly Property getArchDossProd() As ArchDossProd
        Get
            Return _archiDossProd
        End Get
    End Property
    Public ReadOnly Property getDroitBDD() As Outils.DroitUser
        Get
            Return _DroitBDD
        End Get
    End Property
    Public ReadOnly Property getDroitDetermine() As Outils.DroitUser
        Get
            Return _DroitReelInterface
        End Get
    End Property

    'SETTER
    Public WriteOnly Property setArchDossProd As String
        Set(ByVal value As String)
            _archiDossProd = New ArchDossProd(value)
            DetermineDroitReel()
        End Set
    End Property
    Public WriteOnly Property setDroitUser As Outils.DroitUser
        Set(ByVal value As Outils.DroitUser)
            _DroitBDD = value
            DetermineDroitReel()
        End Set
    End Property
    Public WriteOnly Property setUserId() As Double
        Set(ByVal value As Double)
            _utilisateur.idUtilisateur = value
        End Set
    End Property
#End Region

    Sub New()
        'détermination des droits utilisateurs sur dossiers de prod
        _archiDossProd = New ArchDossProd()

        'détermination de l'utilisateur
        _utilisateur = New Utilisateur(Environment.UserName)

        'détermine le nom du pc
        _nomPC = UCase(Environment.MachineName)

        'calcul des droits interface
        DetermineDroitReel()
    End Sub

    Private Sub DetermineDroitReel()
        If Not IsNothing(_archiDossProd) Then

            If _archiDossProd.isEnoughFor(_DroitBDD) Then
                _DroitReelInterface = _DroitBDD
                Exit Sub
            Else
                _DroitReelInterface = If(_archiDossProd.isEnoughFor(Outils.DroitUser.Guest), Outils.DroitUser.Guest, Outils.DroitUser.NoUse)
                Exit Sub
            End If

        End If

        _DroitReelInterface = Outils.DroitUser.NoUse

    End Sub

End Class
