Public Class auditrails

    Private _idAuditrails As Integer
    Private _nomFichierAuditrails As String
    Private _commentaireAuditrails As String
    Private _dateAuditrails As DateTime
    Private _idUtilisateur As Integer

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
            Return _nomFichierAuditrails
        End Get
    End Property
    Public ReadOnly Property getCommentaireAuditrails As String
        Get
            Return _commentaireAuditrails
        End Get
    End Property
    Public ReadOnly Property getDateAuditrails As String
        Get
            Return DateToString(_dateAuditrails)
        End Get
    End Property
    Public ReadOnly Property getIdUtilisateur As Integer
        Get
            Return _idUtilisateur
        End Get
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _idAuditrails = -1
        _nomFichierAuditrails = Nothing
        _commentaireAuditrails = Nothing
        _dateAuditrails = Nothing
        _idUtilisateur = -1
    End Sub
    Sub New(ByVal nomFichierAuditrails As String, ByVal commentaireAuditrails As String, ByVal dateAuditrails As DateTime, _
        ByVal idUtilisateur As Integer)
        _idAuditrails = -1
        _nomFichierAuditrails = nomFichierAuditrails
        _commentaireAuditrails = commentaireAuditrails
        _dateAuditrails = dateAuditrails
        _idUtilisateur = idUtilisateur
    End Sub
    Sub New(ByVal idAuditrails As Integer, ByVal nomFichierAuditrails As String, ByVal commentaireAuditrails As String, _
            ByVal dateAuditrails As DateTime, ByVal idUtilisateur As Integer)
        _idAuditrails = idAuditrails
        _nomFichierAuditrails = nomFichierAuditrails
        _commentaireAuditrails = commentaireAuditrails
        _dateAuditrails = dateAuditrails
        _idUtilisateur = idUtilisateur
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim description As New System.Text.StringBuilder("Audit Trails n°")
        With description
            .Append(_idAuditrails)
            .Append(" (ID utilisateur : ")
            .Append(_idUtilisateur)
            .Append(")").AppendLine()

            .Append(" (mode op : ")
            If IsNothing(_nomFichierAuditrails) Then
                .Append("pas de fichier lié")
            Else
                .Append(_nomFichierAuditrails)
            End If
            .Append(")")
        End With
        Return description.ToString()
    End Function

End Class
