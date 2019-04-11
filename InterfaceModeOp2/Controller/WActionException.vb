Public Class WActionException
    Inherits Exception

    Private Const _NOSOURCE = "pas d'erreur source"
    Private Const _SOURCEWREADER = "WReader est la source de l'erreur : {0}"
    Private _ErreurSource As String

    Public ReadOnly Property getErreurSource As String
        Get
            Return _ErreurSource
        End Get
    End Property

    Sub New(ByVal errmsg As String)
        MyBase.New(errmsg)
        _ErreurSource = _NOSOURCE
    End Sub

    ''' <summary>
    ''' Permet l'identification précise d'une erreur généré par WAction
    ''' </summary>
    ''' <param name="ex">Exception d'origine</param>
    ''' <param name="errmsg">Message d'erreur</param>
    ''' <remarks></remarks>
    Sub New(ByVal errmsg As String, ByVal ex As Exception)
        MyBase.New(errmsg)
        If IsNothing(ex) Then
            _ErreurSource = _NOSOURCE
        Else
            _ErreurSource = ex.Message
        End If
    End Sub

    Sub New(ByVal errmsg As String, ByVal ex As WReaderException)
        MyBase.New(errmsg)
        If IsNothing(ex) Then
            _ErreurSource = _NOSOURCE
        Else
            _ErreurSource = String.Format(_SOURCEWREADER, ex.Message)
        End If
    End Sub



End Class
