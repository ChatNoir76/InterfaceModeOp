Public Class UtilisateurOLD

    'nom utilisateur
    Private _nomUser As String
    'pc utilisateur
    Private _nomPC As String
    'droit architecture de production
    Private _archiDossProd As ArchDossProd
    'droit acces interface
    Private _DroitReelInterface As service.DroitUser = service.DroitUser.Guest
    'droit défini dans la base de données
    Private _DroitPrevu As service.DroitUser = service.DroitUser.Guest

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
    Public ReadOnly Property getDroitPrevu() As service.DroitUser
        Get
            Return _DroitPrevu
        End Get
    End Property
    Public ReadOnly Property getDroitReel() As service.DroitUser
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
    Public WriteOnly Property setDroitUser As service.DroitUser
        Set(ByVal value As service.DroitUser)
            _DroitPrevu = value
            _DroitReelInterface = DetermineDroitReel()
        End Set
    End Property
#End Region

    Sub New()
        _nomUser = UCase(Environment.UserName)
        _nomPC = UCase(Environment.MachineName)
    End Sub

    Private Function DetermineDroitReel() As service.DroitUser
        If Not IsNothing(_archiDossProd) Then

            If _archiDossProd.isEnoughFor(_DroitPrevu) Then
                Return _DroitPrevu
            Else
                Return If(_archiDossProd.isEnoughFor(service.DroitUser.Guest), service.DroitUser.Guest, service.DroitUser.NoUse)
            End If

        End If

        Return service.DroitUser.NoUse

    End Function

End Class
