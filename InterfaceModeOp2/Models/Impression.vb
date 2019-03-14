Public Class Impression
    Private _idImpression As Integer
    Private _lot As String
    Private _nomImprimante As String
    Private _pagesImpression As String
    Private _idAuditrails As Integer

#Region "Property"
    Public Property idImpression As Integer
        Get
            Return _idImpression
        End Get
        Set(ByVal value As Integer)
            _idImpression = value
        End Set
    End Property
    Public ReadOnly Property getLot As String
        Get
            Return _lot
        End Get
    End Property
    Public ReadOnly Property getNomImprimante As String
        Get
            Return _nomImprimante
        End Get
    End Property
    Public ReadOnly Property getPagesImpression As String
        Get
            Return _pagesImpression
        End Get
    End Property
    Public Property idAuditrails As Integer
        Get
            Return _idAuditrails
        End Get
        Set(ByVal value As Integer)
            _idAuditrails = value
        End Set
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _idImpression = -1
        _lot = Nothing
        _nomImprimante = Nothing
        _pagesImpression = Nothing
        _idAuditrails = -1
    End Sub
    Sub New(ByVal lot As String, ByVal nomImprimante As String, ByVal pagesImpression As String, Optional ByVal idAuditrails As Integer = -1)
        _idImpression = -1
        _lot = lot
        _nomImprimante = nomImprimante
        _pagesImpression = pagesImpression
        _idAuditrails = idAuditrails
    End Sub
    Sub New(ByVal idImpression As Integer, ByVal lot As String, ByVal nomImprimante As String, ByVal pagesImpression As String, ByVal idAuditrails As Integer)
        _idImpression = idImpression
        _lot = lot
        _nomImprimante = nomImprimante
        _pagesImpression = pagesImpression
        _idAuditrails = idAuditrails
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim description As New System.Text.StringBuilder("Impression n°")
        With description
            .Append(_idImpression).AppendLine()
            .Append("Audit trails n°").Append(_idAuditrails)
        End With
        Return description.ToString()
    End Function

End Class
