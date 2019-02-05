Imports System.IO
Imports ConfigBDD.GBDD
Class CheckFichier

    Private _ListeModeOp() As String
    Private _RepAll, _RepBase As String
    Private _Extention As String
    Private _MDP As List(Of String)
    Private _sKey As String
    Private _StrConn As String
    Private _Crypter As Boolean
    Private _RowNR As Integer = 0

    Const OK As String = "Word"
    Const CRP As String = "Crypté"
    Const PB As String = "Problème lors de la conversion"
    Const NR As String = "Non Reconnu"
    Const ABS As String = "Absent"

#Region "Property"
    Public WriteOnly Property Extention As String
        Set(ByVal value As String)
            _Extention = value
        End Set
    End Property
    Public WriteOnly Property ListMotPasse As List(Of String)
        Set(ByVal value As List(Of String))
            _MDP = value
        End Set
    End Property
    Public WriteOnly Property sKey As String
        Set(ByVal value As String)
            _sKey = value
        End Set
    End Property
    Public WriteOnly Property Strconn As String
        Set(ByVal value As String)
            _StrConn = value
        End Set
    End Property
#End Region

#Region "Constructeur"
    Sub New(ByVal Repertoire As String, ByVal ParamArray ListeFichier() As String)
        InitializeComponents()

        _ListeModeOp = ListeFichier
        _RepAll = Repertoire
        _RepBase = Last(_RepAll, "\")

        remplissageDGV()

    End Sub
#End Region

#Region "Méthode Externe"
    Public Sub Show()
        _main.Show()
    End Sub
#End Region

#Region "Gestion Interne"
    Private Sub remplissageDGV()
        Dim cpt As Integer = 1
        For Each nom As String In _ListeModeOp
            _DGV.Rows.Add(cpt, nom, "-")
            cpt += 1
        Next
    End Sub
    Private Function Last(ByRef Mot As String, ByVal Car As String) As String
        Return Mot.Split(Car)(Mot.Split(Car).Count - 1)
    End Function
    Private Sub ResteRow()
        Dim rapport As String
        Dim a As Integer = 0
        Dim b As Integer = 0
        Dim c As Integer = 0
        Dim d As Integer = 0
        Dim e As Integer = 0

        _RowNR = _DGV.Rows.Count - 1 - _PB.Value
        For Each ModeOp As DataGridViewRow In _DGV.Rows
            If ModeOp.Cells(0).Value = Nothing Then Continue For
            If ModeOp.Cells(2).Value = "" Or ModeOp.Cells(2).Value = "-" Then ModeOp.Cells(2).Value = ABS
            Select Case ModeOp.Cells(2).Value
                Case OK
                    a += 1
                Case NR
                    b += 1
                Case ABS
                    c += 1
                Case CRP
                    d += 1
                Case PB
                    e += 1
            End Select
        Next

        rapport = OK & " : " & a & vbNewLine & _
            NR & " : " & b & vbNewLine & _
            ABS & " : " & c & vbNewLine & _
            CRP & " : " & d & vbNewLine & _
            PB & " : " & e
        _PB.Value = _PB.Maximum
        MsgBox(rapport, MsgBoxStyle.Information, "Opération Terminée")

    End Sub
    Private Sub Find(ByVal Crypter As Boolean)
        _PB.Value = 0
        _PB.Maximum = _DGV.Rows.Count - 1
        _Crypter = Crypter

        For Each Rep As String In Directory.GetDirectories(_RepAll)
            NextScan(Rep)
        Next
        For Each Fichier As String In Directory.GetFiles(_RepAll)
            GetStatut(Fichier)
        Next
        ResteRow()
    End Sub
    Private Sub NextScan(ByVal Repertoire As String)
        For Each rep As String In Directory.GetDirectories(Repertoire)
            NextScan(rep)
        Next
        For Each Fichier As String In Directory.GetFiles(Repertoire)
            GetStatut(Fichier)
        Next
    End Sub
    Private Sub GetStatut(ByVal nom As String)
        For Each ModeOp As DataGridViewRow In _DGV.Rows
            If LCase(F(ModeOp.Cells(1).Value)) = Path.GetFileNameWithoutExtension(LCase(F(nom))) Then
                _PB.PerformStep()
                Select Case VerExt(nom)
                    Case 0 'non reconnu
                        ModeOp.Cells(2).Value = NR
                    Case 1 'fichier Word
                        If _Crypter Then
                            ModeOp.Cells(2).Value = CrypterFichier(nom)
                        Else
                            ModeOp.Cells(2).Value = OK
                        End If
                    Case 2 'fichier crypté
                        ModeOp.Cells(2).Value = CRP
                End Select
                Exit For
            End If
        Next
    End Sub
    Private Function CrypterFichier(ByVal fullname As String) As String
        Try
            Dim AT As New ACCDBModeOp(_StrConn)
            Dim GW As New GWord(fullname, main.TempFile, _MDP.ToArray)
            GW.NettoyageTexteBasPage()
            GW.AjoutTexteBasPage(main.Version)
            GW.CopyTo(fullname, _sKey)
            GW.Dispose(GWord.Delete.FichierSource)
            AT.AT_Importation(GW.Name, "Importation Automatique")
            GWord.KillWordProcess()
            Return CRP
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.ToString)
            Return PB
        End Try
    End Function
    Private Function F(ByVal Texte As String) As String
        If Texte = Nothing Then Return Nothing
        Texte = Texte.Replace(".crp", "")
        Texte = Texte.Replace(Chr(63), "e")
        Texte = Texte.Replace(Chr(13), "")
        Return Texte
    End Function
    Private Function VerExt(ByRef texte As String) As Integer

        If LCase(Path.GetExtension(texte)) = LCase(".crp") Then
            Return 2
        ElseIf _Extention Like "*" & LCase(Path.GetExtension(texte)) & "*" Then
            Return 1
        Else
            Return 0
        End If
    End Function
#End Region

End Class
Partial Class CheckFichier
    'gestion des évènements
    Private Sub BTStatut() Handles _BTCheckStatut.Click
        Find(False)
    End Sub
    Private Sub BTCryptage() Handles _BTCrypte.Click
        Find(True)
    End Sub
    Private Sub BTInfo() Handles _BTInfo.Click
        Dim Print As New PrintDataGridView(_DGV)
        Print.Impression(PrintDataGridView.Affichage.Defaut, PrintDataGridView.Style.Grille)
    End Sub
    Private Sub MainSizeChanged() Handles _main.SizeChanged
        Call NewSize()
    End Sub
End Class
Partial Class CheckFichier
    'construction graphique
    Private WithEvents _main As New Form
    Private WithEvents _DGV As New DataGridView
    Private WithEvents _BTCheckStatut As New Button
    Private WithEvents _BTCrypte As New Button
    Private WithEvents _BTInfo As New Button
    Private WithEvents _PB As New ProgressBar
    Private BTSize As New Size(150, 35)
    Private Space As New Size(8, 8)
    Private Bordure As New Size(16, 40)

    Private Sub InitializeComponents()
        With _main
            .MinimumSize = New Size(800, 600)
            .Text = "Gestion Statut Mode Opératoire"
        End With
        With _BTCheckStatut
            .Location = New Point(0, 0)
            .Size = BTSize
            .Text = "MAJ Statut"
        End With
        With _BTCrypte
            .Location = New Point(BTSize.Width + Space.Width, 0)
            .Size = BTSize
            .Text = "Cryptage"
        End With
        With _BTInfo
            .Location = New Point(BTSize.Width * 2 + Space.Width * 2, 0)
            .Size = BTSize
            .Text = "Print"
        End With
        With _PB
            .Minimum = 0
            .Step = 1
            .Location = New Point(BTSize.Width * 3 + Space.Width * 3, 0)
            'size variable selon la fenetre windows
        End With
        With _DGV
            .Location = New Point(0, BTSize.Height + Space.Height)
            'size variable selon la fenetre windows
            .Columns.Add("ID", "N°")
            .Columns.Add("Name", "Nom Fichier")
            .Columns.Add("Statut", "Statut Fichier")
            .Columns(0).Width = 75
            .Columns(1).Width = 250
            .Columns(2).Width = 250
        End With
        With _main.Controls
            .AddRange({_BTCheckStatut, _BTCrypte, _BTInfo, _PB, _DGV})
        End With
    End Sub
    Private Sub NewSize()
        _PB.Size = New Size(_main.Width - Bordure.Width - BTSize.Width * 3 - Space.Width * 3, BTSize.Height)
        _DGV.Size = New Size(_main.Width - Bordure.Width, _main.Height - Bordure.Height - BTSize.Height - Space.Height)
    End Sub
End Class