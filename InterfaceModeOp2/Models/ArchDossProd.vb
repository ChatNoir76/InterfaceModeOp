Imports System.IO
Imports InterfaceModeOp2.service

Public Class ArchDossProd
    Private Const _NBDossier As Integer = 6
    Private _mapDroit As New Hashtable

    ''' <summary>
    ''' Retourne l'accès d'un dossier qui a été déterminé au préalable
    ''' </summary>
    ''' <param name="monDossier"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getAccess(ByVal monDossier As service.DossierProd) As AccessFolder
        Get
            Return _mapDroit(monDossier.ToString)
        End Get
    End Property

    ''' <summary>
    ''' Détermination en fonction des droits directs sur les dossiers Prod
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        If Directory.Exists(Configuration.getInstance.getBaseDir) Then

            Dim ListeDossier = [Enum].GetValues(GetType(service.DossierProd))
            For Each dossier As service.DossierProd In ListeDossier
                _mapDroit.Add(dossier.ToString, getAccessFromFolder(Configuration.getInstance.getProdDir(dossier)))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Prends le format ?01?22 par exemple
    ''' </summary>
    ''' <param name="CodeIni">le code en lui meme (?00?11)</param>
    ''' <remarks></remarks>
    Sub New(ByVal CodeIni As String)
        If CodeIni.Length = _NBDossier Then
            For i As service.DossierProd = 1 To _NBDossier
                _mapDroit.Add(i.ToString, ToAccessFolder(CodeIni(i - 1)))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Réalise des test en accès / lecture / écriture sur un dossier passé en paramètre
    ''' </summary>
    ''' <param name="nomDossier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Converti les caractères provenant d'une string en élément accessFolder
    ''' </summary>
    ''' <param name="index">Elément d'une chaine de caractère du type 00?11?</param>
    ''' <returns>L'équivalent en type AccessFolder</returns>
    ''' <remarks></remarks>
    Private Function ToAccessFolder(ByVal index As Char) As AccessFolder
        Select Case index
            Case service.INI_IGNORE_CHAR
                Return AccessFolder.Ignore
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

    ''' <summary>
    ''' Vérifie si les droits en ecriture ou lecture sont suffisant pour avoir les droitUser passé en parametre
    ''' </summary>
    ''' <param name="Droit">le droit voulu qui est testé</param>
    ''' <returns>True si l'architecture permet ce droit</returns>
    ''' <remarks></remarks>
    Public Function isEnoughFor(ByVal Droit As service.DroitUser) As Boolean
        Dim Arch As New ArchDossProd(Configuration.getInstance.GetValueFromKey(Droit))
        Dim ListeDossier = [Enum].GetValues(GetType(service.DossierProd))

        For Each dossier As service.DossierProd In ListeDossier
            Dim DroitToUse As service.AccessFolder
            Dim monDroit As service.AccessFolder

            DroitToUse = Arch.getAccess(dossier)
            monDroit = Me.getAccess(dossier)

            If DroitToUse <> AccessFolder.Ignore Then
                If monDroit < DroitToUse Then
                    Return False
                End If
            End If
        Next

        Return True

    End Function

    Public Shared Function GetListeDroitUser() As List(Of ArchDossProd)
        Dim maListe As New List(Of ArchDossProd)

        For i As Byte = 0 To 5
            maListe.Add(New ArchDossProd(Configuration.getInstance.GetValueFromKey(i)))
        Next

        Return maListe
    End Function

End Class
