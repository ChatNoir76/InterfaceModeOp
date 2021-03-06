﻿Imports Word = Microsoft.Office.Interop.Word
Imports System.Reflection
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class WReader
    Private Shared _Instance As WReader = Nothing
    Private Shared ReadOnly mylock As New Object()

    Private Shared _myWord As Word.Application
    Private Shared _myDoc As Word.Document
    Private Shared _FullName As String = ""

    Private _ListeSignet As New List(Of Signets)
    Private Const _ERR_NOINSTANCE As String = "Il n'y a aucun document word ouvert"
    Private Const _sKey As String = "f4ty52uG"

    ''' <summary>
    ''' Détermine la méthode d'ouverture du document Word
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum method
        ''' <summary>
        ''' utilise la méthode Documents.Open sans ouverture des signets
        ''' </summary>
        ''' <remarks></remarks>
        open = 0
        ''' <summary>
        ''' utilise la méthode Documents.Add avec ouverture des signets
        ''' </summary>
        ''' <remarks></remarks>
        add = 1
    End Enum

#Region "Property"
    Public Shared ReadOnly Property getDocName() As String
        Get
            Return Path.GetFileName(_FullName)
        End Get
    End Property

    Private Property visible As Boolean
        Get
            Return _myWord.Visible
        End Get
        Set(ByVal value As Boolean)
            _myWord.Visible = value
        End Set
    End Property

    Public ReadOnly Property getInfos() As String
        Get
            Dim infos As String = "Information : " & getDocName & vbNewLine
            infos += "Nb Section : " & _myDoc.Sections.Count & vbNewLine

            For Each sec As Word.Section In _myDoc.Sections
                infos += "Section (" & sec.Index & ") Nb Header : " & sec.Headers.Count & " Nb Fields : " & sec.Range.Fields.Count & vbNewLine
                infos += "Section (" & sec.Index & ") Nb Footer : " & sec.Footers.Count & vbNewLine
            Next

            Return infos
        End Get
    End Property

    ''' <summary>
    ''' Détermine si un document word est ouvert
    ''' </summary>
    ''' <value></value>
    ''' <returns>True si document ouvert</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isOpen As Boolean
        Get
            Return Not NoInstance()
        End Get
    End Property

    ''' <summary>
    ''' Retourne le nombre de page du document word
    ''' </summary>
    ''' <value></value>
    ''' <returns>Object renvoyé par la méthode Doc.Range.Information(wdNumberOfPagesInDocument)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getPages() As Object
        Get
            If NoInstance() Then
                Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
            End If
            Try
                'variable vba
                Dim wdNumberOfPagesInDocument As Integer = 4
                Dim numPages As Integer = -1
                'pour avoir le bon nombre de page avant impression
                'nécessite d'avoir appelé la méthode extractionFields() 
                'pour éviter d'avoir de nouveau les boites de dialogues
                _myDoc.PrintPreview()
                numPages = _myDoc.Range.Information(wdNumberOfPagesInDocument)
                _myDoc.ClosePrintPreview()

                Return numPages
            Catch ex As Exception
                Throw New WReaderException("Erreur lors de la détermination du nombre de page du document word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Liste des signets renseignés par l'utilisateur
    ''' </summary>
    Public ReadOnly Property getFields As List(Of Signets)
        Get
            If NoInstance() Then
                Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
            End If
            Try
                Return _ListeSignet
            Catch ex As Exception
                Throw New WReaderException("Erreur lors de la récupération de la liste des signets", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End Try
        End Get
    End Property

    Public ReadOnly Property getLot As String
        Get
            Return extractLotSignet()
        End Get
    End Property

#End Region

#Region "Constructeur / Ouverture Doc Word"
    ''' <summary>
    ''' Méthode d'ouverture des modes opératoires
    ''' </summary>
    ''' <param name="fichier">Chemin fichier Word (ou crp)</param>
    ''' <remarks></remarks>
    Public Sub OpenWord(ByVal fichier As String, ByVal methode As method)
        Try
            'ferme le word en cours
            If Not NoInstance() Then
                'Fermeture ancien word
                Close()
            End If
        Catch ex As Exception
            Throw New WReaderException("Erreur lors du processus de fermeture de l'ancien Word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

        'vérification des données reçues
        If IsNothing(fichier) Then
            Throw New WReaderException("il n'y a pas de nom de fichier à ouvrir", System.Reflection.MethodBase.GetCurrentMethod().Name)
        ElseIf Not File.Exists(fichier) Then
            Throw New WReaderException("Le fichier demandé n'existe pas : " & vbNewLine & fichier, System.Reflection.MethodBase.GetCurrentMethod().Name)
        Else
            _FullName = fichier
        End If

        'détermine si word crypté ou non puis ouverture
        If Path.GetExtension(fichier).ToLower = Outils.EXT_SIMPLE_CRP Then
            Try
                'décryptage
                Dim monCRP As String = Decrypter(fichier)
                'Ouverture du word décrypté
                ouverture(monCRP, methode)
            Catch ex As Exception
                Throw New WReaderException("Erreur lors du processus de décryptage", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End Try
        Else
            Try
                'Ouverture d'un doc word
                ouverture(fichier, methode)
            Catch ex As Exception
                Throw New WReaderException("Erreur lors de l'ouverture du document Word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End Try
        End If

        Try
            'déprotection du document
            If _myDoc.ProtectionType <> Word.WdProtectionType.wdNoProtection Then
                If Not Unlocked(Outils.LISTE_MDP_PRODUCTION.Split("|")) Then
                    Throw New WReaderException("Le mot de passe du document Word est inconnu", "Unlocked")
                End If
            End If
        Catch ex As Exception
            Throw New WReaderException("Erreur lors du traitement post ouverture du document Word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

        Try
            If _myWord.Visible = True Then
                'le doc est non visible
                _myWord.Visible = False
            End If
        Catch ex As Exception
            Throw New WReaderException("Erreur lors du masquage du Word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

        Try
            'récupération des infos des signets
            extractionFields()
        Catch ex As Exception
            Throw New WReaderException("Erreur lors du processus d'extraction des signets", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Sub

    Private Sub ouverture(ByVal fichier As String, ByVal methode As method)
        If methode = method.add Then
            _myDoc = _myWord.Documents.Add(fichier, True, True, False)
        Else
            _myDoc = _myWord.Documents.Open(fichier)
            _myWord.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Récupération du fichier Word ouvert grace à la méthode openWord()
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMyWord() As WReader
        SyncLock (mylock)
            Try
                If IsNothing(_Instance) Then
                    _Instance = New WReader()
                End If
                Return _Instance
            Catch ex As Exception
                Throw New WReaderException("Erreur lors de la récupération de l'instance de la classe WReader", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End Try
        End SyncLock
    End Function
    Private Sub New()
        'Ouverture Word
        Try
            _myWord = New Word.Application
        Catch ex As Exception
            Throw New WReaderException("Erreur lors de la création de l'instance de l'application Word", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub
#End Region

#Region "Destructeur"
    ''' <summary>
    ''' Ferme le document Word actif
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub Close()
        On Error Resume Next
        _myDoc.Close(False)
        Err.Clear()
    End Sub
    ''' <summary>
    ''' libère les ressources de la classe
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Dispose()
        On Error Resume Next
        Close()
        _myWord.Quit()
        Err.Clear()
        _Instance = Nothing
    End Sub
#End Region

#Region "Methode Externe"
    Public Sub CopyTo(ByVal destination As String, Optional ByVal crypter As Boolean = False)
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        If IsNothing(destination) Then Exit Sub

        Try
            'déverrouille les signets avant enregistrement
            extractionFields(False)
            If crypter Then
                Cryptage(destination)
            Else
                _myDoc.SaveAs2(destination)
            End If
        Catch ex As Exception
            Throw New WReaderException("Erreur lors de la création d'une copie (" & destination & ")", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Remplace un texte par un autre dans l'entete du document
    ''' </summary>
    ''' <param name="TexteARemplacer"></param>
    ''' <param name="RemplacerPar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RemplaceTexteEntete(ByVal TexteARemplacer As String, ByVal RemplacerPar As String) As Boolean
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        If IsNothing(TexteARemplacer) Or IsNothing(RemplacerPar) Then Return False

        Dim operation As Boolean = False
        On Error GoTo no
        _myWord.ActiveWindow.ActivePane.View.SeekView = 1
        operation = _myWord.Selection.Find.Execute(FindText:=TexteARemplacer, Forward:=True, ReplaceWith:=RemplacerPar)
        _myWord.ActiveWindow.ActivePane.View.SeekView = 0
no:
        Return operation
    End Function

    ''' <summary>
    ''' Recherche la présence d'un mot clef en entete du document
    ''' </summary>
    ''' <param name="motClef">mot à cherché</param>
    ''' <returns>True si mot présent</returns>
    ''' <remarks>Meme technique de recherche que la méthode de remplacement</remarks>
    Public Function RechercheEnTete(ByVal motClef As String) As Boolean
        Dim rep As Boolean = False
        _myWord.ActiveWindow.ActivePane.View.SeekView = 1
        rep = _myWord.Selection.Find.Execute(motClef)
        _myWord.ActiveWindow.ActivePane.View.SeekView = 0
        Return rep
    End Function

    ''' <summary>
    ''' Conversion du word en PDF
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportAsPDF()
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If
        Try
            Dim PDFFileName As String = Path.GetTempFileName & ".pdf"
            _myDoc.ExportAsFixedFormat(PDFFileName, Word.WdExportFormat.wdExportFormatPDF, True)
        Catch ex As Exception
            Throw New WReaderException("erreur lors de la création d'un PDF à partir du Word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Relance les boites de dialogue de renseignement des signets
    ''' </summary>
    ''' <returns>Vrai si pas d'erreur</returns>
    ''' <remarks></remarks>
    Public Function ActiveFields() As Boolean
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        Dim retour As Boolean = True

        Try
            _myDoc.Range.Fields.Locked = False
            'Update() retourne 0 si ok et 1 si erreur
            retour = retour And Not _myDoc.Range.Fields.Update()
            _myDoc.Range.Fields.Locked = True

            _myDoc.Sections(1).Headers(1).Range.Fields.Locked = False
            retour = retour And Not _myDoc.Sections(1).Headers(1).Range.Fields.Update()
            _myDoc.Sections(1).Headers(1).Range.Fields.Locked = True
        Catch ex As Exception
            Throw New WReaderException("erreur lors de la réactivation manuelle des signets via boites de dialogues word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        Finally
            visible = False
            extractionFields()
        End Try

        Return retour
    End Function

    ''' <summary>
    ''' Imprime un document Word selon un interval de pages
    ''' </summary>
    ''' <param name="FromPage">1ere page de l'interval à imprimer</param>
    ''' <param name="ToPage">dernière page de l'interval à imprimer</param>
    ''' <remarks></remarks>
    ''' 
    Public Overloads Sub PrintDoc(ByVal FromPage As Integer, ByVal ToPage As Integer)
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        Try
            If FromPage < 0 Or ToPage < 0 Then Exit Sub
            If FromPage > ToPage Then Exit Sub
        Catch ex As Exception
            Throw New WReaderException("erreur avec les numéros de page envoyés à l'impression", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

        Try
            For i = FromPage To ToPage

            Next
        Catch ex As Exception
            Throw New WReaderException("erreur lors de l'impression du document (int, int)", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Imprime une page unique
    ''' </summary>
    ''' <param name="Page">Numéro de la page à imprimer</param>
    ''' <remarks></remarks>
    Public Overloads Sub PrintDoc(ByVal Page As Integer)
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        Try
            'impression Recto seul
            _myDoc.PrintOut(Background:=False, Range:=4, Pages:=CStr(Page))
        Catch ex As Exception
            Throw New WReaderException("erreur lors de l'impression du document (int)", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Imprime à partir d'une liste de numéro de page
    ''' </summary>
    ''' <param name="Pages">Liste de numéro de pages</param>
    ''' <remarks></remarks>
    Public Overloads Sub PrintDoc(ByVal Pages As List(Of Integer))
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        Try
            For Each Page As Integer In Pages
                PrintDoc(Page)
            Next
        Catch ex As Exception
            Throw New WReaderException("erreur lors de l'impression du document (List of int)", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ajoute un filigrane dans le doc word
    ''' </summary>
    ''' <param name="TexteFiligrane">Texte du filigrane</param>
    ''' <param name="Couleur">Objet de type Transparence/Couleur </param>
    ''' <remarks></remarks>
    Public Sub Filigrane(ByVal TexteFiligrane As String, ByVal Couleur As Color)
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        If IsNothing(TexteFiligrane) Or IsNothing(Couleur) Then Exit Sub

        Try
            For Each section As Word.Section In _myDoc.Sections
                section.Range.Select()

                _myDoc.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader
                _myWord.Selection.HeaderFooter.Shapes.AddTextEffect(1, TexteFiligrane, "Times New Roman", 1, False, False, 0, 0).Select()

                With _myWord.Selection.ShapeRange
                    .Name = "WA_" & section.Range.Start & section.Range.ID & "." & section.Index
                    .TextEffect.NormalizedHeight = False
                    .Line.Visible = False
                    .Fill.Visible = True
                    .Fill.Solid()
                    .Fill.ForeColor.RGB = RGB(Couleur.getRouge, Couleur.getVert, Couleur.getBleu)
                    .Fill.Transparency = Couleur.getAlpha
                    .Rotation = 330
                    .LockAspectRatio = True
                    .Height = CentimetersToPoints(4.46)
                    .Width = CentimetersToPoints(22.32)
                    .WrapFormat.AllowOverlap = True
                    .WrapFormat.Side = 3
                    .WrapFormat.Type = 3
                    .RelativeHorizontalPosition = 0
                    .RelativeVerticalPosition = 0
                    .Top = -999995
                    .Left = -999995
                End With
                _myDoc.ActiveWindow.ActivePane.View.SeekView = 0
            Next
        Catch ex As Exception
            Throw New WReaderException("erreur lors de l'ajout d'un filigrane au document word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Nettoyage du bas de page du document word
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub NettoyageTexteBasPage()
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If

        Try
            With _myDoc.PageSetup
                .FirstPageTray = 0
                .OtherPagesTray = 0
                .OddAndEvenPagesHeaderFooter = False
                .DifferentFirstPageHeaderFooter = False
                .TwoPagesOnOne = False
            End With

            For Each sec As Word.Section In _myDoc.Sections
                For Each foot As Word.HeaderFooter In sec.Footers
                    With foot.Range
                        .Delete()
                    End With
                Next
            Next
        Catch ex As Exception
            Throw New WReaderException("erreur lors du traitement du bas de page du document word", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Ajout d'un texte en bas de page du document word
    ''' </summary>
    ''' <param name="Texte">texte à ajouter</param>
    ''' <param name="Separtexte">String à ajouter entre l'ancien et le nouveau texte</param>
    ''' <remarks>Utiliser la méthode de nettoyage du bas de page avant d'utiliser cette méthode</remarks>
    Public Sub AjoutTexteBasPage(ByVal Texte As String, Optional ByVal Separtexte As String = "")
        If NoInstance() Then
            Throw New WReaderException(_ERR_NOINSTANCE, System.Reflection.MethodBase.GetCurrentMethod().Name)
        End If
        If IsNothing(Texte) Then Exit Sub
        Try
            Dim SecS, SecE As Integer

            'on récupère la taille du 1er bas de page
            With _myDoc.Sections(1).Footers(1).Range
                SecS = .Start
                SecE = .End
            End With

            For Each sec As Word.Section In _myDoc.Sections
                If sec.Footers.Count >= 1 Then
                    With sec.Footers(1).Range
                        'Si ce bas de page est différent du 1er, c'est que le texte à déjà été ajouté
                        If SecS = .Start And SecE = .End Then
                            .Text = Replace(Replace(.Text & Separtexte & Texte, Chr(10), ""), Chr(13), "") 'remplace .Text & vbLf & Texte
                            .Font.Size = 8.0
                        End If
                    End With
                End If
            Next
        Catch ex As Exception
            Throw New WReaderException("erreur lors de l'ajout d'information en bas de page", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
#End Region

#Region "METHODE INTERNE"
    ''' <summary>
    ''' Méthode de conversion centimetre en point
    ''' </summary>
    ''' <param name="value">valeur en centimetre</param>
    ''' <returns>valeur en point</returns>
    ''' <remarks></remarks>
    Private Function CentimetersToPoints(ByRef value As Long) As Long
        Return value * 28.3464574
    End Function

    ''' <summary>
    ''' Méthode de déprotection en utilisant une liste de mot clef
    ''' généralement ce sont les password de production
    ''' </summary>
    ''' <param name="ListeMDP">Liste des mots de passes potentiellement utilisés</param>
    ''' <remarks></remarks>
    Private Function Unlocked(ByVal ParamArray ListeMDP() As String) As Boolean
        'traitement fichier inconnu
        On Error Resume Next
        For Each mdp As String In ListeMDP
            _myDoc.Unprotect(mdp)
            If Err.Number <> 0 Then
                Err.Clear()
            Else
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Récupère les signets renseignés par l'utilisateur
    ''' Généralement renseigné à l'ouverture du document grace à la méthode _myWord.document.add
    ''' </summary>
    ''' <param name="Locked">Définie si les signets sont locké ou non à l'issue de la récupération</param>
    ''' <remarks></remarks>
    Private Sub extractionFields(Optional ByRef Locked As Boolean = True)
        Try
            _ListeSignet.Clear()
            'récupération des signets des anciens mode opératoire
            For Each element As Word.Field In _myDoc.Sections(1).Headers(1).Range.Fields
                If element.Type = Word.WdFieldType.wdFieldFillIn Then
                    Dim sgt As New Signets(extractClefSignet(element.Code.Text), element.Result.Text, element.Code.Text)
                    If Not _ListeSignet.Contains(sgt) Then
                        _ListeSignet.Add(sgt)
                    End If
                    element.Locked = True
                End If
            Next

            'récupération dans le corps du document
            For Each element As Word.Field In _myDoc.Range.Fields
                If element.Type = Word.WdFieldType.wdFieldSet Then
                    Dim sgt As New Signets(extractClefSignet(element.Code.Text), element.Result.Text, element.Code.Text)
                    If Not _ListeSignet.Contains(sgt) Then
                        _ListeSignet.Add(sgt)
                    End If
                    element.Locked = Locked 'pour bloquer l'ouverture type popup intempestif
                End If
            Next

            For Each sec As Word.Section In _myDoc.Sections
                For Each element As Word.Field In sec.Range.Fields
                    If element.Type = Word.WdFieldType.wdFieldSet Or element.Type = Word.WdFieldType.wdFieldFillIn Then
                        Dim sgt As New Signets(extractClefSignet(element.Code.Text), element.Result.Text, element.Code.Text)
                        If Not _ListeSignet.Contains(sgt) Then
                            _ListeSignet.Add(sgt)
                        End If
                        element.Locked = Locked 'pour bloquer l'ouverture type popup intempestif
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New WReaderException("erreur lors de la récupération de la valeur des signets renseigné par l'utilisateur", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Function extractClefSignet(ByVal codeSignet As String) As String
        Dim tab() = codeSignet.Split(Chr(34))
        If tab.Count = 3 Then
            Return tab(1).Trim({Chr(34), " "c})
        Else
            Return "-"
        End If
    End Function

    Private Function extractLotSignet() As String
        For Each element As Signets In _ListeSignet
            If LCase(element.getCodeSignet) Like "*" & LCase("Lot") & "*" Then
                Return element.getValeurSignet
            End If
        Next
        Return "-"
    End Function

    ''' <summary>
    ''' Crypte un fichier word en CRP
    ''' </summary>
    ''' <param name="destinationCRP">nom du fichier de destination</param>
    ''' <remarks>l'instance du document est perdu à l'issue de cette méthode</remarks>
    Private Sub Cryptage(ByRef destinationCRP As String)
        Dim fichierInput As String = Path.GetTempFileName
        Try
            _myDoc.SaveAs2(fichierInput)
            'close pour permettre l'acces à fsInput
            _myDoc.Close()
        Catch ex As Exception
            Throw New WReaderException("erreur lors de la création du fichier temporaire lors de l'opération de cryptage", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

        Dim fsInput As FileStream = New FileStream(fichierInput, FileMode.Open, FileAccess.Read)
        Dim fsEncrypted As New FileStream(destinationCRP, FileMode.Create, FileAccess.Write)
        Dim DES As New DESCryptoServiceProvider()
        DES.Key = ASCIIEncoding.ASCII.GetBytes(_sKey)
        DES.IV = ASCIIEncoding.ASCII.GetBytes(_sKey)
        Dim desencrypt As ICryptoTransform = DES.CreateEncryptor()
        Dim cryptostream As CryptoStream = New CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write)
        Try
            'Lit le texte du fichier source dans le tableau d'octets
            Dim bytearrayinput(fsInput.Length - 1) As Byte
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)

            'écrit le fichier crypté à l'aide de DES
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length)
        Catch ex As Exception
            Throw New WReaderException("erreur lors du cryptage du document word temporaire", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        Finally
            CryptoStream.Close()
            fsInput.Dispose()
            'suppression du fichier temporaire
            File.Delete(fichierInput)
        End Try
    End Sub

    ''' <summary>
    ''' Décrypte un fichier word CRP dans un dossier temporaire (GetTempPath)
    ''' </summary>
    ''' <param name="WordCrypter">Le chemin du fichier crypté</param>
    ''' <returns>Le chemin du fichier décrypté</returns>
    ''' <remarks></remarks>
    Private Function Decrypter(ByVal WordCrypter As String) As String

        Dim Destination As String = Path.GetTempPath & Path.GetFileNameWithoutExtension(WordCrypter)
        Dim DES As New Security.Cryptography.DESCryptoServiceProvider()
        DES.Key() = ASCIIEncoding.ASCII.GetBytes(_sKey)
        DES.IV = ASCIIEncoding.ASCII.GetBytes(_sKey)
        Dim fsread As New FileStream(WordCrypter, FileMode.Open, FileAccess.Read)
        Dim desdecrypt As Security.Cryptography.ICryptoTransform = DES.CreateDecryptor()
        Dim cryptostreamDecr As New Security.Cryptography.CryptoStream(fsread, desdecrypt, Security.Cryptography.CryptoStreamMode.Read)
        Dim fsDecrypted As New FileStream(Destination, FileMode.Create, FileAccess.Write)
        Dim buffer As New List(Of Byte)
        Dim byt As Integer
        Try
            Do
                byt = cryptostreamDecr.ReadByte()
                If byt = -1 Then
                    Exit Do
                Else
                    buffer.Add(byt)
                End If
            Loop
        Catch ex As Exception
            Throw New WReaderException("Erreur lors du processus de décryptage", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        Finally
            fsDecrypted.Write(buffer.ToArray, 0, buffer.Count)
            fsDecrypted.Flush()
            fsDecrypted.Close()
            fsread.Close()

        End Try

        Return Destination

    End Function

    ''' <summary>
    ''' Vérifie qu'un fichier word est bien ouvert
    ''' </summary>
    ''' <returns>true si aucune instance sinon false</returns>
    ''' <remarks></remarks>
    Private Function NoInstance() As Boolean
        Try
            Return _myWord.Documents.Count <= 0
        Catch ex As Exception
            Throw New WReaderException("erreur lors du controle d'instance", System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function
#End Region




End Class
