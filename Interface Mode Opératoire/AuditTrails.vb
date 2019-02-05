Imports ConfigBDD.GBDD
Imports System.Linq

Public NotInheritable Class AuditTrails
    Inherits DataBDD

    Const _HStart As Integer = 150
    Const PRINT As String = "Impression"
    Const IMP As String = "Importation"
    Const EXP As String = "Exportation"
    Private _Mode As Integer

    Private _GBox As New GroupBox
    Private _CBNomFichier As ComboBox
    Private _CBLot As ComboBox
    Private _TXTSignet As TextBox
    Private WithEvents _BTTri As Button
    Private WithEvents _BTReset As Button
    Private _DT1 As DateTimePicker
    Private _DT2 As DateTimePicker
    Private _TBox As TextBox
    Private _CHK1 As CheckBox

    Public Enum ModeVisu
        Importation = 0
        Exportation = 1
        Impression = 2
    End Enum

#Region "Constructeur"
    Sub New(ByVal STRconn As String, ByVal Mode As ModeVisu)
        MyBase.New(STRconn, _HStart)
        _Mode = Mode

        Select Case _Mode
            Case 0
                MyBase.LoadTable("ATimport")
            Case 1
                MyBase.LoadTable("ATexport")
            Case 2
                MyBase.LoadTable("ATPrint")
        End Select

        InitializeCtrl()
        remplissageCB(_CBNomFichier, 1)
        If _Mode = 2 Then remplissageCB(_CBLot, 9)

    End Sub
    Private Sub InitializeCtrl()
        Const H1 As Integer = 25
        Const Es As Integer = 15
        Dim BTsize As New Size(75, H1)
        Dim CBsize As New Size(300, H1)
        Dim LBL1 As New Label
        Dim LBL2 As New Label
        Dim LBL3 As New Label

        _CBNomFichier = New ComboBox
        _BTTri = New Button
        _BTReset = New Button
        _DT1 = New DateTimePicker
        _DT2 = New DateTimePicker
        _CHK1 = New CheckBox

        With _GBox
            .Location = New Point(0, 0)
            .Size = New Size(MyBase.FormMain.MinimumSize.Width - 16, _HStart)
        End With



        'LIGNE1
        Dim locY As Integer = Es
        With _BTTri
            .Location = New Point(Es, Es)
            .Size = BTsize
            .Text = "Trier"
        End With
        With _BTReset
            .Location = New Point(BTsize.Width + Es, Es)
            .Size = BTsize
            .Text = "Reset"
        End With
        With _CHK1
            .Location = New Point(_BTReset.Location.X + BTsize.Width + Es * 2, Es)
            .Text = "Exclure les lignes vérifiées"
            .Size = New Size(150, H1)
            .Checked = False
        End With

        'LIGNE2
        locY = _BTReset.Location.Y + BTsize.Height + Es
        With LBL1
            .Location = New Point(Es, locY)
            .Text = "Nom de Fichier"
        End With
        With _CBNomFichier
            .Location = New Point(Es, locY + Es)
            .Size = CBsize
        End With

        'LIGNE3
        locY = _CBNomFichier.Location.Y + _CBNomFichier.Height + Es
        With LBL2
            .Location = New Point(Es, locY)
            .Text = "Date Départ"
        End With
        With _DT1
            .Location = New Point(Es, locY + Es)
            .Size = New Size(225, H1)
            .ShowUpDown = True
            .Text = CDate("01/01/2015")
        End With
        With LBL3
            .Location = New Point(2 * Es + _DT1.Width, locY)
            .Text = "Date Fin"
        End With
        With _DT2
            .Location = New Point(2 * Es + _DT1.Width, locY + Es)
            .Size = New Size(225, H1)
            .ShowUpDown = True
        End With

        _GBox.Controls.AddRange({_BTTri, _BTReset, _CBNomFichier, _DT1, _DT2, LBL1, LBL2, LBL3, _CHK1})

        If _Mode = 2 Then
            _TBox = New TextBox
            _CBLot = New ComboBox
            _TXTSignet = New TextBox

            Dim LBL4 As New Label
            Dim LBL5 As New Label
            With LBL4
                .Location = New Point(380, Es)
                .Text = "Numéro Lot"
            End With
            With _CBLot
                .Location = New Point(380, Es * 2)
                .Size = New Size(100, H1)
            End With
            With LBL5
                .Location = New Point(380, Es * 4)
                .Text = "Mots Clefs Signets"
            End With
            With _TXTSignet
                .Location = New Point(380, Es * 5)
                .Size = New Size(100, H1)
            End With

            With _TBox
                .Location = New Point(500, CInt(Es / 2))
                .Size = New Size(250, 150)
                .Multiline = True
                .WordWrap = True
                .ReadOnly = True
                .BackColor = Color.White
            End With
            _GBox.Controls.AddRange({_TBox, _CBLot, _TXTSignet, LBL4, LBL5})
        End If
        MyBase.AddCtrlToMyBaseForm(_GBox)
    End Sub
    Private Sub remplissageCB(ByRef CBox As ComboBox, ByVal Index As Integer)
        Dim liste As New List(Of String)
        Dim key As Boolean = False

        For Each DR As DataGridViewRow In DataGV.Rows
            For Each Str As String In liste
                If LCase(Str) = LCase(DR.Cells(Index).Value) Or LCase(DR.Cells(Index).Value) = Nothing Then
                    key = True
                    Exit For
                End If
            Next
            If key = False Then liste.Add(DR.Cells(Index).Value)
        Next
        CBox.Items.AddRange(liste.ToArray)
        CBox.Sorted = True
    End Sub
#End Region

#Region "Evènement"
    Private Sub ChangedSize() Handles Me.SizeFormMainChanged
        If _Mode = 2 Then
            Me._GBox.Size = New Size(MyBase.FormMain.Width - 16, _GBox.Height)
            Me._TBox.Size = New Size(MyBase.FormMain.Width - 16 - _TBox.Location.X, _TBox.Height)
        End If
    End Sub
    Private Sub SelectedCell() Handles Me.DataGVSelectCell
        If _Mode = 2 Then
            On Error Resume Next
            _TBox.Text = MyBase.DataGV.CurrentRow.Cells(8).Value
        End If
    End Sub
#End Region

#Region "Tri"
    Private Sub GoTri() Handles _BTTri.Click
        Dim recherche = From DR As DataGridViewRow In DataGV.Rows
                        Select DR

        'toutes les lignes invisibles
        DataGV.CurrentCell = Nothing
        For Each DGVR As DataGridViewRow In DataGV.Rows
            DGVR.Visible = False
        Next

        'Requete de recherche commune
        recherche = From DR As DataGridViewRow In recherche
                    Where DR.Cells(1).Value Like "*" & _CBNomFichier.Text & "*"
                    Select DR
        If _CHK1.Checked Then
            recherche = From DR As DataGridViewRow In recherche
                        Where DR.Cells(DR.Cells.Count - 1).Value = False
                        Select DR
        End If


        'Requete de recherche spécifique
        Select Case _Mode
            Case 0, 1 'importation / exportation
                recherche = From DR As DataGridViewRow In recherche
                            Where DR.Cells(3).Value >= _DT1.Text And DR.Cells(3).Value <= CDate(_DT2.Text).AddDays(1)
                            Select DR

            Case 2 'impression
                _TBox.Text = Nothing
                recherche = From DR As DataGridViewRow In recherche
                            Where DR.Cells(6).Value >= _DT1.Text And DR.Cells(6).Value <= CDate(_DT2.Text).AddDays(1)
                            Select DR
                recherche = From DR As DataGridViewRow In recherche
                            Where DR.Cells(8).Value Like "*" & _TXTSignet.Text & "*"
                            Select DR
                recherche = From DR As DataGridViewRow In recherche
                            Where DR.Cells(9).Value Like "*" & _CBLot.Text & "*"
                            Select DR
        End Select

        'on affiche les lignes dans recherche
        If recherche.Count > 0 Then
            For i = 0 To recherche.Count - 1
                For Each DGVR As DataGridViewRow In DataGV.Rows
                    If DGVR.Cells(0).Value = recherche(i).Cells(0).Value Then
                        DGVR.Visible = True
                        Exit For
                    End If
                Next
            Next
        End If


    End Sub
    Private Sub ResetTri() Handles _BTReset.Click
        On Error Resume Next
        _CBLot.Text = Nothing
        _CBNomFichier.Text = Nothing
        _DT1.Text = CDate("01/01/2015")
        _DT2.Text = Now
        _CHK1.Checked = False
        _TXTSignet.Text = Nothing
        GoTri()

    End Sub
#End Region

End Class
