Public Class WReaderException
    Inherits Exception

    Private _errSubName As String
    Private _ex As String
    Private _DocumentName As String = ""

    Public ReadOnly Property getDocName() As String
        Get
            Return _DocumentName
        End Get
    End Property
    Public ReadOnly Property getSubNameError() As String
        Get
            Return _errSubName
        End Get
    End Property
    Public ReadOnly Property getMessageSource() As String
        Get
            Return _ex
        End Get
    End Property

    ''' <summary>
    ''' Permet l'identification précise d'une erreur généré par WReader
    ''' </summary>
    ''' <param name="ex">Exception d'origine</param>
    ''' <param name="errmsg">Message d'erreur</param>
    ''' <param name="subName">System.Reflection.MethodBase.GetCurrentMethod().Name</param>
    ''' <remarks></remarks>
    Sub New(ByVal errmsg As String, ByVal subName As String, Optional ByVal ex As String = "Pas d'erreur source")
        MyBase.New(errmsg)
        _errSubName = subName
        _ex = ex
        _DocumentName = WReader.getDocName
        WReader.Dispose()
    End Sub



End Class
