Public Class Utilisateur

    'nom utilisateur
    Private _nomUser As String
    'pc utilisateur
    Private _nomPC As String
    'droit architecture de production
    Private _archiDossProd As ArchDossProd

#Region "Property"
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

    Public WriteOnly Property setArchDossProd As ArchDossProd
        Set(ByVal value As ArchDossProd)
            _archiDossProd = value
        End Set
    End Property

#End Region

    Sub New()
        _nomUser = UCase(Environment.UserName)
        _nomPC = UCase(Environment.MachineName)
    End Sub

End Class
