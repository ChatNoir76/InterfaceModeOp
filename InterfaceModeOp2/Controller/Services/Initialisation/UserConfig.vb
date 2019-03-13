Public Class UserConfig

    'nom utilisateur
    Private _nomUser As String
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
    Public ReadOnly Property getNom() As String
        Get
            Return _nomUser
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
    Public ReadOnly Property getDroitReel() As Outils.DroitUser
        Get
            Return _DroitReelInterface
        End Get
    End Property

    'SETTER
    Public WriteOnly Property setArchDossProd As ArchDossProd
        Set(ByVal value As ArchDossProd)
            _archiDossProd = value
            _DroitReelInterface = DetermineDroitReel()
        End Set
    End Property
    Public WriteOnly Property setDroitUser As Outils.DroitUser
        Set(ByVal value As Outils.DroitUser)
            _DroitBDD = value
            _DroitReelInterface = DetermineDroitReel()
        End Set
    End Property
#End Region

    Sub New()
        _nomUser = UCase(Environment.UserName)
        _nomPC = UCase(Environment.MachineName)
    End Sub

    Private Function DetermineDroitReel() As Outils.DroitUser
        If Not IsNothing(_archiDossProd) Then

            If _archiDossProd.isEnoughFor(_DroitBDD) Then
                Return _DroitBDD
            Else
                Return If(_archiDossProd.isEnoughFor(Outils.DroitUser.Guest), Outils.DroitUser.Guest, Outils.DroitUser.NoUse)
            End If

        End If

        Return Outils.DroitUser.NoUse

    End Function

End Class
