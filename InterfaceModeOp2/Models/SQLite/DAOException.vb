Public Class DAOException
    Inherits Exception

    Private Const _ERR_NOSOURCE = "Pas d'erreur source"
    Private _errSource As String

    Public ReadOnly Property getErrSource As String
        Get
            Return _errSource
        End Get
    End Property
    Public Sub New(ByVal errmsg As String, Optional ByVal errExcep As Exception = Nothing)
        MyBase.New(errmsg)
        _errSource = If(IsNothing(errExcep), _ERR_NOSOURCE, errExcep.Message)
    End Sub

End Class
