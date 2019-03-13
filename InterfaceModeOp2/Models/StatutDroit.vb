Public Class StatutDroit

    Private _idDroit As Integer
    Private _nomDroit As String

#Region "Property"
    Public Property idDroit As Integer
        Get
            Return _idDroit
        End Get
        Set(ByVal value As Integer)
            _idDroit = value
        End Set
    End Property
    Public ReadOnly Property getNomDroit As String
        Get
            Return _nomDroit
        End Get
    End Property
#End Region

#Region "Constructeurs"
    Sub New()
        _idDroit = -1
        _nomDroit = Nothing
    End Sub

    Sub New(ByVal nomDroit As String)
        _idDroit = -1
        _nomDroit = nomDroit
    End Sub

    Sub New(ByVal idDroit As Integer, ByVal nomDroit As String)
        _idDroit = idDroit
        _nomDroit = nomDroit
    End Sub
#End Region

    Public Overrides Function ToString() As String
        Dim monDroit As New System.Text.StringBuilder("Droit n°")
        With monDroit
            .Append(_idDroit)
            If IsNothing(_nomDroit) Then
                .Append(" (inconnu)")
            Else
                .Append(" (").Append(_nomDroit).Append(")")
            End If
        End With
        Return monDroit.ToString
    End Function

End Class
