Public Class signets
    Private _idSignet As Integer
    Private _description As String
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
    Public ReadOnly Property getDescriptionSignet As String
        Get
            Return _description
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
        _description = Nothing
        _valeur = Nothing
        _code = Nothing
        _idImpression = -1
    End Sub
    Sub New(ByVal description As String, ByVal valeur As String, ByVal code As String, Optional ByVal idImpression As Integer = -1)
        _idSignet = -1
        _description = description
        _valeur = valeur
        _code = code
        _idImpression = idImpression
    End Sub
    Sub New(ByVal idSignet As Integer, ByVal description As String, ByVal valeur As String, ByVal code As String, ByVal idImpression As Integer)
        _idSignet = idSignet
        _description = description
        _valeur = valeur
        _code = code
        _idImpression = idImpression
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim description As New System.Text.StringBuilder("Signet N°")
        With description
            .Append(_idSignet)
            If _idImpression = -1 Then
                .Append("(?)")
            Else
                .Append(" -- Impression n°").Append(_idImpression)
            End If
            .AppendLine()
            If IsNothing(_code) Then
                .Append("--pas de code--")
            Else
                .Append(_code)
            End If
        End With
        Return description.ToString()
    End Function

End Class
