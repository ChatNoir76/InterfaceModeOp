Public Class Utilisateur

    Private _idUtilisateur As Integer
    Private _nomUtilisateur As String

#Region "Property"
    Public Property idUtilisateur As Integer
        Get
            Return _idUtilisateur
        End Get
        Set(ByVal value As Integer)
            _idUtilisateur = value
        End Set
    End Property
    Public ReadOnly Property getNomUtilisateur As String
        Get
            Return _nomUtilisateur
        End Get
    End Property
#End Region

#Region "Constructeur"
    Sub New()
        Me._idUtilisateur = -1
        Me._nomUtilisateur = Nothing
    End Sub

    Sub New(ByVal nomUtilisateur As String)
        Me._idUtilisateur = -1
        Me._nomUtilisateur = nomUtilisateur.ToUpper
    End Sub

    Sub New(ByVal idUtilisateur As Integer, ByVal nomUtilisateur As String)
        Me._idUtilisateur = idUtilisateur
        Me._nomUtilisateur = nomUtilisateur.ToUpper
    End Sub
#End Region

#Region "Méthodes Publiques"
    Public Overrides Function toString() As String
        Dim description As New System.Text.StringBuilder("Utilisateur n°")
        With description
            .Append(Me._idUtilisateur).AppendLine()
            .Append("nom : ").Append(Me._nomUtilisateur)
        End With
        Return description.ToString
    End Function
#End Region




End Class
