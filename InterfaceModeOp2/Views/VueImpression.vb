﻿Imports PS = System.Drawing.Printing

Public Class VueImpression
    Private Const _AT_NBCAR_MIN As Byte = 20
    Private _PrinterProperty As PS.PrinterSettings
    Private _NomPrinter As String = Nothing
    Private _AuditTrails As String = "noAT"
    Private _ZoneImpression As String = Nothing
    Private _PageToPrint As New List(Of Integer)
    Private _FormulaireValid As Boolean = False
    Private _pageMax As Integer

    Private Const _PAGES = "1-{0}"

#Region "Property"
    Public ReadOnly Property getNomPrinter As String
        Get
            Return _NomPrinter
        End Get
    End Property
    Public ReadOnly Property getAuditTrails As String
        Get
            Return _AuditTrails
        End Get
    End Property
    Public ReadOnly Property getPageAImprimer As List(Of Integer)
        Get
            Return _PageToPrint
        End Get
    End Property
    Public ReadOnly Property isValidForPrinting As Boolean
        Get
            Return _FormulaireValid
        End Get
    End Property
#End Region

#Region "Constructeur"
    Sub New(ByVal ListeSignet As List(Of Signets), ByVal ListePhraseAT As List(Of String), ByVal PageMax As Integer)

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        _pageMax = PageMax

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        DefinirPageMax(PageMax)
        ChercheImprimantes()
        ChargeListePhraseAT(ListePhraseAT)
        DescriptionSignet(ListeSignet)

        _ZoneImpression = String.Format(_PAGES, _pageMax)

        TXT_PrintZone.Text = _ZoneImpression
    End Sub
    Private Sub DefinirPageMax(ByVal PageMax As Integer)
        NUD_2.Maximum = PageMax
        NUD_2.Minimum = 1
        NUD_1.Maximum = PageMax
        NUD_1.Minimum = 1
        NUD_2.Value = PageMax
        NUD_1.Value = 1
    End Sub
    Private Sub ChercheImprimantes()
        Dim printers As System.Drawing.Printing.PrinterSettings.StringCollection = System.Drawing.Printing.PrinterSettings.InstalledPrinters()
        _PrinterProperty = New Printing.PrinterSettings

        Me.CB_ListeImp.Items.Clear()
        For x As Integer = 0 To printers.Count - 1
            _PrinterProperty.PrinterName = printers(x).ToString
            'récupère uniquement l'imprimante par défault
            If _PrinterProperty.IsValid Then
                If _PrinterProperty.IsDefaultPrinter Then Me.CB_ListeImp.Items.Add(printers(x))
            End If
        Next
    End Sub
    Private Sub ChargeListePhraseAT(ByVal value As List(Of String))
        CB_AT.Items.Clear()
        For Each element As String In value
            CB_AT.Items.Add(element)
        Next
        CB_AT.Items.Add("Autre : ")
        TXT_AT.Visible = False
    End Sub
#End Region

#Region "Méthodes publiques"
    Public Sub DescriptionSignet(ByVal ListeSignet As List(Of Signets))
        TXT_Signets.Clear()
        For Each Str As Signets In ListeSignet
            TXT_Signets.Text += Str.getClefSignet & "=" & Str.getValeurSignet & vbNewLine
        Next
    End Sub
#End Region

#Region "Méthodes internes"
    Private Function isOKToPrint() As Boolean
        'Une imprimante est choisi
        If IsNothing(_NomPrinter) Then
            MessageBox.Show("Veuillez choisir une imprimante", "impression", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        'Audit trails renseigné
        If _AuditTrails.Length < _AT_NBCAR_MIN Then
            MessageBox.Show(String.Format("Veuillez renseigner l'audit trails avec au moins {0} caractères", _AT_NBCAR_MIN), "impression", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        'Zone d'impression
        Return canPrint()
    End Function
    Private Function canPrint() As Boolean

        Try
            'suppression des espaces blanc
            _ZoneImpression = _ZoneImpression.Replace(" ", "")
            Dim zone As New List(Of String)

            'sépare en fonction des sections ;
            If _ZoneImpression.Contains(";") Then
                zone = _ZoneImpression.Split(";").ToList
            Else
                zone.Add(_ZoneImpression)
            End If

            _PageToPrint.Clear()
            'pour toutes les sections
            For Each pz As String In zone
                If pz.Contains("-") Then
                    Dim debut As Integer = CInt(pz.Split("-")(0))
                    Dim fin As Integer = CInt(pz.Split("-")(1))
                    For i = debut To fin
                        _PageToPrint.Add(i)
                    Next
                Else
                    Dim page As Integer = CInt(pz)
                    If _PageToPrint.Contains(page) Then
                        MessageBox.Show(String.Format("Il n'est pas autorisé d'imprimer plusieurs fois la page n°{0}", page), "impression", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                    _PageToPrint.Add(page)
                End If
            Next
            For Each page As Integer In _PageToPrint
                If page > _pageMax Then
                    MessageBox.Show(String.Format("L'impression de la page {0} n'est pas possible car il n'y a que {1} page(s) max", page, _pageMax), "impression", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
                If page < 1 Then
                    MessageBox.Show("Impossible d'imprimer une page avec un numéro négatif ou égal à zéro", "impression", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region

#Region "Validation OK Pour Imprimer"
    Private Sub PrintZone() Handles TXT_PrintZone.Leave
        _ZoneImpression = TXT_PrintZone.Text
    End Sub
    Private Sub CB_ListeImp_SelectedIndexChanged() Handles CB_ListeImp.SelectedIndexChanged
        _NomPrinter = Me.CB_ListeImp.SelectedItem
    End Sub
    Private Sub TXT_AT_TextChanged() Handles TXT_AT.TextChanged
        If TXT_AT.Text.Length >= _AT_NBCAR_MIN Then
            TXT_AT.BackColor = Drawing.Color.White
        Else
            TXT_AT.BackColor = Drawing.Color.Pink
        End If
        _AuditTrails = TXT_AT.Text
    End Sub
    Private Sub NumericUpDowns_ValueChanged() Handles NUD_1.ValueChanged, NUD_2.ValueChanged
        NUD_2.Minimum = NUD_1.Value
        _ZoneImpression = NUD_1.Value & " - " & NUD_2.Value
    End Sub
#End Region

#Region "Evènement"
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_OPT1.CheckedChanged
        'Pour les supperpositions de GroupBox
        'Le num1 doit toujours etre visible sinon le GB2 ne le sera pas
        'et si GB 2 visible alors GB1 ne l'est plus
        If RB_OPT1.Checked Then
            GB_OPT2.Visible = False
            Call NumericUpDowns_ValueChanged()
        Else
            GB_OPT2.Visible = True
        End If
    End Sub
    Private Sub CB_AT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_AT.SelectedIndexChanged
        If CB_AT.SelectedIndex = CB_AT.Items.Count - 1 Then
            TXT_AT.Text = Nothing
            TXT_AT.Visible = True
        Else
            TXT_AT.Visible = False
            _AuditTrails = CB_AT.SelectedItem
        End If
    End Sub
    ''' <summary>
    ''' vérification du format de page en mode interval
    ''' </summary>
    Private Sub VerifFormat() Handles TXT_PrintZone.TextChanged
        Dim newTxt As String = Nothing

        For Each c As String In TXT_PrintZone.Text.ToCharArray
            Dim Num = Asc(c)
            If Num >= Asc("0") And Num <= Asc("9") Then
                newTxt += Chr(Num)
            ElseIf Num = Asc(";") Or Num <= Asc("-") Then
                newTxt += Chr(Num)
            End If
        Next

        TXT_PrintZone.Text = newTxt

    End Sub
#End Region

    Private Sub BT_Validation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Validation.Click
        If isOKToPrint() Then
            _FormulaireValid = True
            Me.Close()
        End If
    End Sub

End Class