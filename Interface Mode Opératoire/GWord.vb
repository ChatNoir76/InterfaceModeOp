Imports Word = Microsoft.Office.Interop.Word
Imports System
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text
Imports PS = System.Drawing.Printing

Public NotInheritable Class GWord
    'création de l'application Word et enregistrement des nom fichier source et destination
    Private WithEvents _myWord As New Word.Application
    Private _myDoc As Word.Document
    Private _FichierSource As String 'fichier de base
    Private _fichierTemp As String

    'Variable de détermination de la protection Word
    Private _GWK As String

    'Variable de gestion des signets
    Private _Fields As New List(Of Fields)

    'Variable de gestion ouverture fenetre windows
    Private _Size(1) As Integer

    'détermination du numéro de lot
    Private _NumLot As String

    'Private _WA As New Word.Application
    Private _WD As Word.Document

#Region "PROPERTY"
    Public ReadOnly Property ListeSignet As List(Of String)
        Get
            Return CreatListOfString()
        End Get
    End Property
    Private Function CreatListOfString() As List(Of String)
        Dim liste As New List(Of String)
        For Each element As Fields In _Fields
            liste.Add(element.Name & " = " & element.Value)
        Next
        Return liste
    End Function
    Public ReadOnly Property TempFullName As String
        Get
            Return _fichierTemp
        End Get
    End Property
    Public ReadOnly Property FullName As String
        Get
            Return _FichierSource
        End Get
    End Property
    Public ReadOnly Property Name As String
        Get
            Return _FichierSource.Split("\")(_FichierSource.Split("\").Count - 1)
        End Get
    End Property
    ''' <summary>
    ''' Détermine si Gword à besoin du mot de passe d'origine du word
    ''' </summary>
    ''' <value></value>
    ''' <returns>si true, les fonctions ayant besoin d'un mot de passe seront désactivées</returns>
    ''' <remarks></remarks>
    Private ReadOnly Property GWK As String
        Get
            Return _GWK
        End Get
    End Property
    Public ReadOnly Property Lot As String
        Get
            Return _NumLot
        End Get
    End Property
#End Region

#Region "Constructeur"
    'il copie le Word (décrypté) dans temporaire 
    ''' <summary>
    ''' Ouverture d'un fichier Word dans un temp (destination)
    ''' </summary>
    ''' <param name="Word">Nom du fichier Word déprotégé</param>
    ''' <param name="Destination">Fichier de destination</param>
    ''' <remarks></remarks>
    Sub New(ByVal Word As String, ByVal Destination As String)
        Try
            'ouverture d'un word Protégé par GWK OBSOLETE
            _FichierSource = Word
            _fichierTemp = Destination

            'copie du fichier source vers la destination
            File.Copy(_FichierSource, _fichierTemp)

            'chargement Word
            Creation()
        Catch ex As Exception
            Dispose(Delete.Rien)
            Throw ex
        End Try
    End Sub
    Sub New(ByVal Word As String, ByVal Destination As String, ByVal ParamArray listeMDP() As String)
        Try
            '1ere ouverture d'un word inconnu sans GWK
            _FichierSource = Word
            _fichierTemp = Destination
            _WD = _myWord.Documents.Open(Word)
            'si pas de protection
            If _WD.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection Then
                PrepareCreation()
                Exit Sub
            End If

            'si présence de GWK
            For Each var As Word.Variable In _WD.Variables
                If var.Name = _Key Then
                    PrepareCreation()
                    Exit Sub
                End If
            Next
            Unlocked(listeMDP)
        Catch ex As Exception
            Dispose(Delete.Rien)
            Throw ex
        End Try
    End Sub
    Private Sub Unlocked(ByVal ParamArray ListeMDP() As String)
        'traitement fichier inconnu 
        On Error Resume Next
        Err.Clear()
        For Each mdp As String In ListeMDP
            _WD.Unprotect(mdp)
            If Err.Number = 0 Then
                Err.Clear()
                PrepareCreation()
            Else
                Err.Clear()
            End If
        Next
    End Sub
    Private Sub PrepareCreation()
        _WD.Save()
        _WD.Close()
        '_WA.Quit()
        'copie du fichier source vers la destination
        File.Copy(_FichierSource, _fichierTemp)
        'chargement Word
        Creation()
    End Sub
    ''' <summary>
    ''' Ouverture d'un fichier Word Crypté dans un temp (destination)
    ''' </summary>
    ''' <param name="WordCRP">Nom du fichier complet (.crp) </param>
    ''' <param name="MotDePasseCryptage">Mot de passe du cryptage</param>
    ''' <param name="Destination">Fichier de destination</param>
    ''' <remarks></remarks>
    Sub New(ByVal WordCRP As String, ByVal MotDePasseCryptage As String, ByVal Destination As String)
        Try
            'ouverture fichier crypté avec GWK non active
            _FichierSource = WordCRP
            _fichierTemp = Destination

            'décrypte le fichier source vers la destination
            Decrypter(_FichierSource, MotDePasseCryptage, Destination)

            'copie et Décryptage du fichierCRP de base puis chargement Word
            Creation()
        Catch ex As Exception
            Dispose(Delete.Rien)
            Throw ex
        End Try


    End Sub
    Private Sub Creation()
        'chargement(Word)
        '_myWord = New Word.Application
        _myDoc = _myWord.Documents.Open(_fichierTemp)
        _myDoc.Save()

        'activation ou création d'une GWK
        LoadGWK()
    End Sub
#End Region

#Region "Option de protection et sauvegarde"
    Private Sub UnProtectWord()
        'On Error Resume Next
        _myDoc.Unprotect(GWK)
        If Err.Number <> 0 Then Throw New Exception("Problème déprotection de la GWK")
        _myDoc.Save()
    End Sub
    Private Sub ProtectWord()
        On Error Resume Next
        _myDoc.Protect(Password:=_GWK, Type:=Word.WdProtectionType.wdAllowOnlyReading)
        If Err.Number <> 0 Then Throw New Exception("Problème application de la GWK")
        _myDoc.Save()
    End Sub
#End Region

#Region "Gestion des modifications sur fichier temporaire Word"
    Public Function DonnePagesTotales() As Integer
        Return _myWord.ActiveWindow.Panes(1).Pages.Count
    End Function
    Public Sub RemplaceTexteEntete(ByVal TexteARemplacer As String, ByVal RemplacerPar As String)
        UnProtectWord()
        On Error Resume Next
        _myWord.ActiveWindow.ActivePane.View.SeekView = 1
        _myWord.Selection.Find.Execute(FindText:=TexteARemplacer, Forward:=True, ReplaceWith:=RemplacerPar)
        _myWord.ActiveWindow.ActivePane.View.SeekView = 0
        ProtectWord()
    End Sub
    Public Function Exist(ByVal TexteAChercher As String) As Boolean
        UnProtectWord()
        Dim rep As Boolean = False
        _myWord.ActiveWindow.ActivePane.View.SeekView = 1
        rep = _myWord.Selection.Find.Execute(TexteAChercher)
        _myWord.ActiveWindow.ActivePane.View.SeekView = 0
        ProtectWord()
        Return rep
    End Function
    Public Sub AjoutTexteBasPage(ByVal Texte As String, Optional ByVal Separtexte As String = " ")
        UnProtectWord()
        For Each sec As Word.Section In _myDoc.Sections
            With sec.Footers(1).Range
                .Text = Replace(Replace(.Text & Separtexte & Texte, Chr(10), ""), Chr(13), "") 'remplace .Text & vbLf & Texte
                .Font.Size = 8.0
            End With
        Next
        ProtectWord()
    End Sub
    Public Sub NettoyageTexteBasPage()
        UnProtectWord()

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
        ProtectWord()
    End Sub
    Public Sub ConvertirPDF(ByVal PDFFileName As String)

        'application des signets via boite de dialogue word
        'AjoutInfoSignet()
        ActiveFields()

        _myDoc.ExportAsFixedFormat(PDFFileName, Word.WdExportFormat.wdExportFormatPDF, True)

    End Sub
    Private Function CentimetersToPoints(ByRef value As Long) As Long
        Return value * 28.3464574
    End Function
    Public Sub Filigrane(ByVal TexteFiligrane As String, ByVal Color_RGB_Filigrane As List(Of Integer))
        Dim Rouge As Integer = Color_RGB_Filigrane(0)
        Dim Vert As Integer = Color_RGB_Filigrane(1)
        Dim Bleu As Integer = Color_RGB_Filigrane(2)

        UnProtectWord()

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
                .Fill.ForeColor.RGB = RGB(Rouge, Vert, Bleu)
                .Fill.Transparency = 0.8
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

        ProtectWord()

    End Sub
    Public Enum Duplex As Integer
        ParDéfaut = 0
        Verso = 1
    End Enum
    Public Sub SecurePrint(ByVal NomPrinter As String, ByVal AT As String, ByVal FromPage As Integer, ByVal ToPage As Integer, ByVal Style As Duplex)
        Select Case Style
            Case 0
                'impression par défaut (Recto/Verso)
                _myDoc.PrintOut(Background:=True, Range:=3, From:=CStr(FromPage), [To]:=CStr(ToPage), Copies:=1, ManualDuplexPrint:=False)
            Case 1
                'impression Recto seul
                For i = FromPage To ToPage
                    _myDoc.PrintOut(Background:=False, Range:=4, Pages:=CStr(i))
                    '_myDoc.PrintOut(Background:=True, Range:=3, From:=CStr(i), [To]:=CStr(i), Copies:=1, ManualDuplexPrint:=False)
                Next
            Case Else
                MsgBox("Impression Annulée : Mauvais paramètre 'Style'", MsgBoxStyle.Exclamation)
        End Select
    End Sub
#End Region

#Region "Gestion des signets"
    Private Function DonneDescriptionFillin(ByVal MarefCodeText As String) As String
        Dim texte() As String = Split(MarefCodeText, "\")
        texte(0) = texte(0).Replace("FILLIN  ", "")
        texte(0) = texte(0).Trim({Chr(34), " "c})
        Return texte(0)
    End Function
    Private Function DonneDescriptionSET(ByVal MarefCodeText As String) As String
        Dim texte() As String = Split(MarefCodeText, "\")
        Dim key As Boolean = False
        texte(0) = texte(0).Split(Chr(34))(1)
        texte(0) = texte(0).Trim({Chr(34), " "c})
        Return texte(0)
    End Function
    Private Function Description(ByVal s As Word.Field) As String
        If s.Type = Word.WdFieldType.wdFieldSet Then
            Return DonneDescriptionSET(s.Code.Text)
        End If
        If s.Type = Word.WdFieldType.wdFieldFillIn Then
            Return DonneDescriptionFillin(s.Code.Text)
        End If
        Return Nothing
    End Function
    Private Function AddFillin(ByVal s As Word.Field) As Fields
        Dim place As Long
        Dim rep As String
        Dim ID As Integer = s.Index
        Dim FillIn As Fields
        With _myDoc.Sections(1).Headers(1).Range.Fields
            rep = InputBox(DonneDescriptionFillin(.Item(ID).Code.Text), "InfoSignet", .Item(ID).Result.Text)
            UnProtectWord()
            place = .Item(ID).Result.Start
            .Item(ID).Result.Text = rep
            .Add(_myDoc.Range(place, place + Len(rep)))
            ProtectWord()
            FillIn = New Fields(ID, DonneDescriptionFillin(.Item(ID).Code.Text), rep)
            Return FillIn
        End With
    End Function
    Public Sub ActiveFields()
        For Each element As Word.Field In _myDoc.Range.Fields
            If element.Type = Word.WdFieldType.wdFieldSet Then
                element.Update()
                chercheLot(element)
                _Fields.Add(New Fields(Nothing, Description(element), element.Result.Text))
            End If
        Next
        For Each element As Word.Field In _myDoc.Sections(1).Headers(1).Range.Fields
            Select Case element.Type
                Case Word.WdFieldType.wdFieldFillIn
                    _Fields.Add(AddFillin(element))
                    chercheLot(element)
                    element.Locked = True
                Case Word.WdFieldType.wdFieldSet
                    element.Update()
                    chercheLot(element)
                    _Fields.Add(New Fields(Nothing, Description(element), element.Result.Text))
                Case Word.WdFieldType.wdFieldUserName
                    '_Fields.Add(New Fields(Nothing, "Username", element.Result.Text))
            End Select
        Next
    End Sub
    Private Sub chercheLot(ByVal element As Word.Field)
        If LCase(element.Code.Text) Like "*" & LCase("Lot") & "*" Then
            _NumLot = element.Result.Text
        End If
    End Sub
#End Region

#Region "Options d'exportations d'un fichier Word crypté"
    Public Sub AddListOfEditors(ByVal ParamArray Editeurs() As String)
        UnProtectWord()
        For Each Ed As String In Editeurs
            _myWord.ActiveWindow.ActivePane.View.SeekView = 0
            With _myWord.ActiveWindow.ActivePane.Selection
                .WholeStory()
                .Editors.Add(Ed)
            End With
        Next
        ProtectWord()
    End Sub
#End Region

#Region "Gestion de la fermeture"
    Public Enum Delete As Integer
        Rien = 0
        FichierTemp = 1
        FichierSource = 2
        Tous = 3
    End Enum
    ''' <summary>
    ''' Ferme le classeur
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close()
        _myDoc.Close()
    End Sub
    ''' <summary>
    ''' Ferme l'application et son classeur
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose(ByVal Deleting As Delete)
        On Error Resume Next
        Close()
        _myWord.Quit()

        Select Case Deleting
            Case 1
                File.Delete(_fichierTemp)
            Case 2
                File.Delete(_FichierSource)
            Case 3
                File.Delete(_fichierTemp)
                File.Delete(_FichierSource)
        End Select

    End Sub
#End Region

#Region "Modification Externe Word"
    ''' <summary>
    ''' Copie vers un Word soit protégé avec une GWordKey, soit non protégé
    ''' </summary>
    Public Overloads Sub CopyTo(ByVal Destination As String, ByVal GWordKey As Boolean)
        If Not GWordKey Then UnProtectWord()
        File.Copy(TempFullName, Destination, False)
    End Sub
    ''' <summary>
    ''' Copie vers un Word Crypté. Extention *.crp ajouté automatiquement si ajouteCRP = true
    ''' </summary>
    Public Overloads Sub CopyTo(ByVal Destination As String, ByVal Password As String, Optional ByVal AjouteCRP As Boolean = True)
        If AjouteCRP Then
            Crypter(TempFullName, Destination & ".crp", Password, False)
        Else
            Crypter(TempFullName, Destination, Password, False)
        End If

    End Sub
    Public Sub DeleteSourceFile()
        On Error Resume Next
        File.Delete(_FichierSource)
    End Sub
    Private Sub Crypter(ByVal FichierBase As String, ByVal Destination As String, ByVal MotDePasse As String, ByVal DeleteSource As Boolean)
        'pas de GWordKey/_MDP pour le cryptage
        UnProtectWord()

        Dim fichierInput As String = FichierBase & "0"
        File.Copy(FichierBase, fichierInput)
        Dim fsInput As New FileStream(fichierInput, FileMode.Open, FileAccess.Read)
        Dim fsEncrypted As New FileStream(Destination, FileMode.Create, FileAccess.Write)

        'parametre de cryptage
        Dim DES As New DESCryptoServiceProvider()
        DES.Key = ASCIIEncoding.ASCII.GetBytes(MotDePasse)
        DES.IV = ASCIIEncoding.ASCII.GetBytes(MotDePasse)
        Dim desencrypt As ICryptoTransform = DES.CreateEncryptor()
        Dim cryptostream As New CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write)

        'Lit le texte du fichier source dans le tableau d'octets
        Dim bytearrayinput(fsInput.Length - 1) As Byte
        fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)

        'écrit le fichier crypté à l'aide de DES
        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length)
        cryptostream.Close()

        fsInput.Dispose()
        File.Delete(fichierInput)
        If DeleteSource Then File.Delete(FichierBase)

        ProtectWord()
    End Sub
    Private Sub Decrypter(ByVal WordCrypter As String, ByVal sKey As String, ByVal Destination As String)

        Dim DES As New DESCryptoServiceProvider()
        DES.Key() = ASCIIEncoding.ASCII.GetBytes(sKey)
        DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
        Dim fsread As New FileStream(WordCrypter, FileMode.Open, FileAccess.Read)
        Dim desdecrypt As ICryptoTransform = DES.CreateDecryptor()
        Dim cryptostreamDecr As New CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read)
        Dim fsDecrypted As New FileStream(Destination, FileMode.Create, FileAccess.Write)
        Dim buffer As New List(Of Byte)
        Dim byt As Integer
        Do
            byt = cryptostreamDecr.ReadByte()
            If byt = -1 Then
                Exit Do
            Else
                buffer.Add(byt)
            End If
        Loop

        fsDecrypted.Write(buffer.ToArray, 0, buffer.Count)
        fsDecrypted.Flush()
        fsDecrypted.Close()
        fsread.Close()
    End Sub
#End Region

#Region "Evènement Word et fonctions partagées"
    Private Sub ChangeWindowsState(ByVal Statut As Word.WdWindowState)
        _myWord.ScreenUpdating = False
        _myWord.Visible = True
        _myWord.WindowState = Statut
        _myWord.Visible = False
        _myWord.ScreenUpdating = True
    End Sub
    Private Sub Minimize()
        On Error Resume Next
        ChangeWindowsState(Word.WdWindowState.wdWindowStateNormal)
        _myWord.Resize(0, 0)
    End Sub
    Private Sub NormalSize()
        With _myWord
            _myWord.WindowState = Word.WdWindowState.wdWindowStateNormal
            _myWord.Resize(_Size(0), _Size(1))
        End With
    End Sub
    Private Sub Word_Start() Handles _myWord.DocumentOpen
        _Size(0) = _myWord.Width
        _Size(1) = _myWord.Height
        Minimize()
    End Sub
    Private Sub BeforeClose() Handles _myWord.DocumentBeforeClose
        NormalSize()
    End Sub
    Private Sub Word_Activate() Handles _myWord.WindowActivate
        _myWord.Visible = False
    End Sub
    Shared Function RecupereSansExtention(ByRef MonFichier As String) As String
        Dim longueur As Integer = MonFichier.Length
        Return MonFichier.Remove(longueur - 4, 4)
    End Function
    Shared Sub KillWordProcess()

        Dim P() As Process
        P = Process.GetProcessesByName("WINWORD")

        For Each Proc As Process In P
            Proc.Dispose()
            'Proc.Kill()
        Next

    End Sub
#End Region

#Region "GWordKey"
    Private _Key As String = "GWordKey"
    Private _GWKIndex As Integer = 0
    Private Sub LoadGWK()
        'vérifie / récupére / crée la GWK
        For Each var As Word.Variable In _myDoc.Variables
            If var.Name = _Key Then
                _GWKIndex = var.Index
                _GWK = RecupGwordKey()
                ProtectWord()
                Exit Sub
            End If
        Next

        GWK_Creation()
        ProtectWord()
    End Sub
    Private Function RecupGwordKey() As String
        For Each var As Word.Variable In _myDoc.Variables
            If var.Name = _Key Then Return var.Value
        Next
        Return Nothing
    End Function
    Private Function GWK_VerifPresence() As Boolean
        For Each var As Word.Variable In _myDoc.Variables
            If var.Name = _Key Then Return True
        Next
        Return False
    End Function
    Private Function GWK_Creation() As Boolean
        Try
            Dim _MDP As New GenerateMDP()
            _GWK = _MDP.Key

            For Each var As Word.Variable In _myDoc.Variables
                If var.Name = _Key Then _GWKIndex = var.Index
            Next

            If _GWKIndex = 0 Then
                _myDoc.Variables.Add(_Key, GWK)
                _GWKIndex = _myDoc.Variables.Item(_Key).Index
                Return True
            Else
                _myDoc.Variables(_GWKIndex).Value = GWK
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

    Protected Class Fields
        Private _ID As Integer
        Private _Name As String
        Private _Value As String

        Public ReadOnly Property ID As Integer
            Get
                Return _ID
            End Get
        End Property
        Public ReadOnly Property Name As String
            Get
                Return _Name
            End Get
        End Property
        Public ReadOnly Property Value As String
            Get
                Return _Value
            End Get
        End Property
        Sub New(ByVal ID As Integer, ByVal Name As String, ByVal Value As String)
            _ID = ID
            _Name = Name
            _Value = Value
        End Sub

    End Class
    Protected Class GenerateMDP
        Private _Key As String = Nothing
        Public ReadOnly Property Key As String
            Get
                Return _Key
            End Get
        End Property
        Sub New(Optional ByVal NbCar As Integer = 10)
            For i = 1 To NbCar
                _Key = _Key & GetMe()
            Next
        End Sub
        Private Function GetMe() As String
            Randomize()
            Dim A As Integer = Int(Rnd() * 62) + 1
            Select Case A
                Case 1 To 10
                    Return Chr(A + 47)
                Case 11 To 36
                    Return Chr(A + 54)
                Case 37 To 62
                    Return Chr(A + 60)
                Case Else
                    Return Nothing
            End Select
        End Function
        Public Overrides Function ToString() As String
            Return Key
        End Function
    End Class
End Class
