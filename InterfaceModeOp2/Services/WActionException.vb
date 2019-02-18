Public Class WActionException
    Inherits Exception

    Private _ErreurSource As String

    Public ReadOnly Property getErreurSource As String
        Get
            Return _ErreurSource
        End Get
    End Property

    ''' <summary>
    ''' Permet l'identification précise d'une erreur généré par WAction
    ''' </summary>
    ''' <param name="ex">Exception d'origine</param>
    ''' <param name="errmsg">Message d'erreur</param>
    ''' <remarks></remarks>
    Sub New(ByVal errmsg As String, Optional ByVal ex As String = "Pas d'erreur source")
        MyBase.New(errmsg)
        _ErreurSource = ex
    End Sub



End Class
