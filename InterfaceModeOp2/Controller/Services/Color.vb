Public Class Color

    Private _Alpha As Double
    Private _Rouge As Byte
    Private _Vert As Byte
    Private _Bleu As Byte

    Public ReadOnly Property getAlpha() As Double
        Get
            Return _Alpha
        End Get
    End Property
    Public ReadOnly Property getRouge() As Byte
        Get
            Return _Rouge
        End Get
    End Property
    Public ReadOnly Property getVert() As Byte
        Get
            Return _Vert
        End Get
    End Property
    Public ReadOnly Property getBleu() As Byte
        Get
            Return _Bleu
        End Get
    End Property

    Sub New(Optional ByVal Alpha As Double = 1.0, Optional ByVal Rouge As Byte = 0, Optional ByVal Vert As Byte = 0, Optional ByVal Bleu As Byte = 0)
        If Alpha > 1.0 Then
            _Alpha = 1.0
        ElseIf Alpha < 0.0 Then
            _Alpha = 0.0
        Else
            _Alpha = Alpha
        End If
        _Rouge = Rouge
        _Vert = Vert
        _Bleu = Bleu
    End Sub

End Class
