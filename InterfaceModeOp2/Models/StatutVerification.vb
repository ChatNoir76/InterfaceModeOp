Public Class StatutVerification
    Private _idVerification As Integer
    Private _commentaire As String

#Region "Property"
    Public Property idVerification As Integer
        Set(ByVal value As Integer)
            _idVerification = value
        End Set
        Get
            Return _idVerification
        End Get
    End Property
    Public ReadOnly Property getCommentaire As String
        Get
            Return _commentaire
        End Get
    End Property
#End Region

#Region "Construsteurs"
    Sub New()
        _idVerification = -1
        _commentaire = Nothing
    End Sub
    Sub New(ByVal commentaire As String)
        _idVerification = -1
        _commentaire = commentaire
    End Sub
    Sub New(ByVal idVerification As Integer, ByVal commentaire As String)
        _idVerification = idVerification
        _commentaire = commentaire
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim description As New System.Text.StringBuilder("id Vérification : ")
        With description
            .Append(_idVerification).AppendLine()
            If IsNothing(_commentaire) Or _commentaire = "" Then
                .Append("--pas de commentaire--")
            Else
                .Append("Commentaire : ").AppendLine()
                .Append(_commentaire)
            End If
        End With
        Return description.ToString
    End Function
End Class
