Public Class auditrails

    Private _idAuditrails As Integer
    Private _nomFichier As String
    Private _commentaire As String
    Private _infosystem As String
    Private _date As DateTime
    Private _idUtilisateur As Integer
    Private _idOperation As Operations

#Region "Property"
    Public Property idAuditrails As Integer
        Set(ByVal value As Integer)
            _idAuditrails = value
        End Set
        Get
            Return _idAuditrails
        End Get
    End Property
    Public ReadOnly Property getNomFichierAuditrails As String
        Get
            Return _nomFichier
        End Get
    End Property
    Public ReadOnly Property getCommentaireAuditrails As String
        Get
            Return _commentaire
        End Get
    End Property
    Public ReadOnly Property getinfosystemAuditrails As String
        Get
            Return _infosystem
        End Get
    End Property
    Public ReadOnly Property getDateAuditrails As String
        Get
            Return DateToString(_date)
        End Get
    End Property
    Public ReadOnly Property getIdUtilisateur As Integer
        Get
            Return _idUtilisateur
        End Get
    End Property
    Public ReadOnly Property getIdOperation As Operations
        Get
            Return _idOperation
        End Get
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _idAuditrails = -1
        _nomFichier = Nothing
        _commentaire = Nothing
        _infosystem = Nothing
        _date = Nothing
        _idUtilisateur = -1
        _idOperation = -1
    End Sub
    Sub New(ByVal nomFichierAuditrails As String, ByVal commentaireAuditrails As String, ByVal infosystem As String, _
            ByVal dateAuditrails As DateTime, ByVal idUtilisateur As Integer, ByVal idOperation As Operations)
        _idAuditrails = -1
        _nomFichier = nomFichierAuditrails
        _commentaire = commentaireAuditrails
        _infosystem = infosystem
        _date = dateAuditrails
        _idUtilisateur = idUtilisateur
        _idOperation = idOperation
    End Sub
    Sub New(ByVal idAuditrails As Integer, ByVal nomFichierAuditrails As String, ByVal commentaireAuditrails As String, ByVal infosystem As String, _
            ByVal dateAuditrails As DateTime, ByVal idUtilisateur As Integer, ByVal idOperation As Operations)
        _idAuditrails = idAuditrails
        _nomFichier = nomFichierAuditrails
        _commentaire = commentaireAuditrails
        _infosystem = infosystem
        _date = dateAuditrails
        _idUtilisateur = idUtilisateur
        _idOperation = idOperation
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim description As New System.Text.StringBuilder("Audit Trails n°")
        With description
            .Append(_idAuditrails).AppendLine()
            .Append("ID utilisateur : ").Append(_idUtilisateur).AppendLine()
            .Append("operation : ").Append(_idOperation.ToString)
        End With
        Return description.ToString()
    End Function

End Class
