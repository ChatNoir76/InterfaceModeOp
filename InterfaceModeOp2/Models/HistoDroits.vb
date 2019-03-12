Public Class HistoDroits

    Private _commentaire As String
    Private _date As DateTime
    Private _idDroit As Double
    Private _idUtilisateur As Double

#Region "Property"
    Public Property commentaire As String
        Get
            Return _commentaire
        End Get
        Set(ByVal value As String)
            If IsNothing(value) Then value = ""
            _commentaire = value
        End Set
    End Property
    Public ReadOnly Property getDate As String
        Get
            Return DateToString(_date)
        End Get
    End Property
    Public ReadOnly Property getIdDroit As Double
        Get
            Return _idDroit
        End Get
    End Property
    Public ReadOnly Property getIdUtilisateur As Double
        Get
            Return _idUtilisateur
        End Get
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _commentaire = Nothing
        _date = Nothing
        _idDroit = -1
        _idUtilisateur = -1
    End Sub
    Sub New(ByVal commentaire As String, ByVal maDate As DateTime, ByVal idUtilisateur As Double, ByVal idDroit As Double)
        _commentaire = commentaire
        _date = maDate
        _idDroit = idDroit
        _idUtilisateur = idUtilisateur
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim str As New System.Text.StringBuilder
        With str
            .Append("Droit ")
            .Append(_idDroit)
            .Append(" de l'utilisateur ")
            .Append(_idUtilisateur)
            .Append(" du ")
            .Append(_date.ToString)
        End With
        Return str.ToString()
    End Function

End Class
