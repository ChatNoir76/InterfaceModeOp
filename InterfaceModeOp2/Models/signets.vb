Public Class Signets
    Private _idSignet As Integer
    Private _clef As String
    Private _valeur As String
    Private _code As String
    Private _idImpression As Integer

#Region "Property"
    Public Property idSignet As Integer
        Get
            Return _idSignet
        End Get
        Set(ByVal value As Integer)
            _idSignet = value
        End Set
    End Property
    Public ReadOnly Property getClefSignet As String
        Get
            Return _clef
        End Get
    End Property
    Public ReadOnly Property getValeurSignet As String
        Get
            Return _valeur
        End Get
    End Property
    Public ReadOnly Property getCodeSignet As String
        Get
            Return _code
        End Get
    End Property
    Public Property idImpression As Integer
        Get
            Return _idImpression
        End Get
        Set(ByVal value As Integer)
            _idImpression = value
        End Set
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _idSignet = -1
        _clef = Nothing
        _valeur = Nothing
        _code = Nothing
        _idImpression = -1
    End Sub
    Sub New(ByVal description As String, ByVal valeur As String, ByVal code As String, Optional ByVal idImpression As Integer = -1)
        _idSignet = -1
        _clef = description
        _valeur = valeur
        _code = code
        _idImpression = idImpression
    End Sub
    Sub New(ByVal idSignet As Integer, ByVal description As String, ByVal valeur As String, ByVal code As String, ByVal idImpression As Integer)
        _idSignet = idSignet
        _clef = description
        _valeur = valeur
        _code = code
        _idImpression = idImpression
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim description As New System.Text.StringBuilder("Signet N°")
        With description
            .Append(_idSignet).AppendLine()
            .Append("Impression n°").Append(_idImpression).AppendLine()
            .Append("code : ").Append(_code)
        End With
        Return description.ToString()
    End Function

End Class
