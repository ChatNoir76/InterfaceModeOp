Imports System.IO
Imports InterfaceModeOp2.service

Public Class ArchDossProd
    Private Const _NBDossier As Integer = 6
    Private _mapDroit As New Hashtable

    Public ReadOnly Property getAccess(ByVal monDossier As service.DossierProd) As AccessFolder
        Get
            Return _mapDroit(monDossier.ToString)
        End Get
    End Property

    ''' <summary>
    ''' Détermination en fonction des droits directs sur les dossiers Prod
    ''' </summary>
    ''' <param name="RepertoireTravail"></param>
    ''' <remarks></remarks>
    Sub New(ByVal RepertoireTravail As String)
        If Directory.Exists(RepertoireTravail) Then

            Dim ListeDossier = [Enum].GetNames(GetType(service.DossierProd))
            For Each dossier As String In ListeDossier
                _mapDroit.Add(dossier, getAccessFromFolder(RepertoireTravail & "\" & Configuration.getInstance.GetValueFromKey(dossier)))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Prends le format ?01?22 par exemple
    ''' </summary>
    ''' <param name="CodeIni">le code en lui meme (?00?11)</param>
    ''' <param name="IgnoreDossier">le char qui correspond à ignorer (?)</param>
    ''' <remarks></remarks>
    Sub New(ByVal CodeIni As String, ByVal IgnoreDossier As Char)
        If CodeIni.Length = _NBDossier Then
            For i As service.DossierProd = 1 To _NBDossier
                _mapDroit.Add(i.ToString, ToAccessFolder(CodeIni(i)))
            Next
        End If
    End Sub

    Private Function getAccessFromFolder(ByVal nomDossier As String) As AccessFolder
        Const NOM_TEST As String = "\test.txt"
        Const NO_REP As String = "\"

        Dim access As AccessFolder = AccessFolder.Probleme

        On Error GoTo endOfTest

        If Not Directory.Exists(nomDossier) Then Throw New Exception

        access = AccessFolder.Ignore
        If nomDossier.EndsWith(NO_REP) Then Throw New Exception

        'test de l'acces au dossier
        access = AccessFolder.None
        Directory.GetFiles(nomDossier)

        'test si création d'un fichier est possible
        access = AccessFolder.Lecture
        Dim fsread As New FileStream(nomDossier & NOM_TEST, FileMode.Create)
        fsread.Dispose()
        File.Delete(nomDossier & NOM_TEST)
        access = AccessFolder.Ecriture

endOfTest:
        Return access

    End Function

    Private Function ToAccessFolder(ByVal index As Char) As AccessFolder
        Select Case index
            Case "-1"
                Return AccessFolder.Ignore
            Case "0"
                Return AccessFolder.None
            Case "1"
                Return AccessFolder.Lecture
            Case "2"
                Return AccessFolder.Ecriture
            Case Else
                Return AccessFolder.Probleme
        End Select
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function

End Class
