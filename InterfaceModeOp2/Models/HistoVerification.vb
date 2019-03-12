Public Class HistoVerification
    Private _commentaire As String
    Private _date As DateTime
    Private _idVerification As Double
    Private _idAuditrails As Double

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
    Public ReadOnly Property getIdVerification As Double
        Get
            Return _idVerification
        End Get
    End Property
    Public ReadOnly Property getIdAuditrails As Double
        Get
            Return _idAuditrails
        End Get
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _commentaire = Nothing
        _date = Nothing
        _idVerification = -1
        _idAuditrails = -1
    End Sub
    Sub New(ByVal commentaire As String, ByVal maDate As DateTime, ByVal idAuditrails As Double, ByVal idVerification As Double)
        _commentaire = commentaire
        _date = maDate
        _idVerification = idVerification
        _idAuditrails = idAuditrails
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim str As New System.Text.StringBuilder
        With str
            .Append("Verification ")
            .Append(_idVerification)
            .Append(" de l'audit trails ")
            .Append(_idAuditrails)
            .Append(" du ")
            .Append(_date.ToString)
        End With
        Return str.ToString()
    End Function

End Class
