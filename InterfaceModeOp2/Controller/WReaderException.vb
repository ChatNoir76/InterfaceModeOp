Public Class WReaderException
    Inherits Exception

    Private _errModule As String
    Private _DocumentName As String = Nothing
    Private Const _ERR_NOWORDDOC = "nom du document word non déterminé"

    Public ReadOnly Property getNomDocumentWordErreur() As String
        Get
            Return If(IsNothing(_DocumentName), _ERR_NOWORDDOC, _DocumentName)
        End Get
    End Property
    Public ReadOnly Property getProcedureOrigineErreur() As String
        Get
            Return _errModule
        End Get
    End Property

    ''' <summary>
    ''' Permet l'identification précise d'une erreur généré par WReader
    ''' </summary>
    ''' <param name="ex">Exception d'origine</param>
    ''' <param name="errmsg">Message d'erreur</param>
    ''' <param name="errModule">System.Reflection.MethodBase.GetCurrentMethod().Name</param>
    ''' <remarks></remarks>
    Sub New(ByVal errmsg As String, ByVal errModule As String, Optional ByVal ex As Exception = Nothing)
        MyBase.New(If(IsNothing(ex), errmsg, ex.Message))
        _errModule = errModule
        _DocumentName = WReader.getDocName
        WReader.Dispose()
    End Sub

End Class
