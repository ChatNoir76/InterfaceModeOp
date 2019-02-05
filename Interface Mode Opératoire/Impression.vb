Imports PS = System.Drawing.Printing

Public Class Impression

    Private _PrinterProperty As PS.PrinterSettings
    Private _NomPrinter As String
    Private _AuditTrails As String
    Private _nbpage As String
    Private _Isok As Boolean = False
    Private _PS As Integer
    Private _PE As Integer
    Private WithEvents _Key As New KeyList(False, False, True)

#Region "Property"
    Public ReadOnly Property NomPrinter As String
        Get
            Return _NomPrinter
        End Get
    End Property
    Public ReadOnly Property AuditTrails As String
        Get
            Return _AuditTrails
        End Get
    End Property
    Public ReadOnly Property PStart As Integer
        Get
            Return _PS
        End Get
    End Property
    Public ReadOnly Property PEnd As Integer
        Get
            Return _PE
        End Get
    End Property
    Public ReadOnly Property IsOk As Boolean
        Get
            Return _Isok
        End Get
    End Property
    Public ReadOnly Property Description As String
        Get
            Return TXT_Signets.Text
        End Get
    End Property
#End Region

#Region "Constructeur"
    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Waiting()
        ChercheImprimantes()
        TXT_AT.Visible = False
    End Sub
#End Region

#Region "Initialisation"
    Public Sub DescriptionSignet(ByVal AddTexte As String)
        TXT_Signets.Text = TXT_Signets.Text & AddTexte & vbNewLine
    End Sub
    Public Sub DescriptionSignetClear()
        TXT_Signets.Clear()
    End Sub
    Public Sub DefinirPageMax(ByVal PageMax As Integer)
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
            If _PrinterProperty.IsValid Then
                If _PrinterProperty.CanDuplex Then Me.CB_ListeImp.Items.Add(printers(x))
            End If
        Next
    End Sub
    Public Sub ChargeCB(ByVal ParamArray Phrase() As String)
        CB_AT.Items.Clear()
        For Each element As String In Phrase
            CB_AT.Items.Add(element)
        Next
        CB_AT.Items.Add("Autre : ")
    End Sub
#End Region

#Region "Evènement"
    Private Sub CB_ListeImp_SelectedIndexChanged() Handles CB_ListeImp.SelectedIndexChanged
        If Not IsNothing(Me.CB_ListeImp.Text) Then _Key.List(0) = True Else _Key.List(0) = False
        TextBox1_TextChanged()
    End Sub
    Private Sub Button1_Click() Handles Button1.Click

        _NomPrinter = Me.CB_ListeImp.Text
        _AuditTrails = Me.TXT_AT.Text
        _PS = NUD_1.Value
        _PE = NUD_2.Value
        _Isok = True
        Me.Close()
    End Sub
    Private Sub TextBox1_TextChanged() Handles TXT_AT.TextChanged
        If TXT_AT.Text.Length > 10 Then _Key.List(1) = True Else _Key.List(1) = False
    End Sub
    Private Sub ok() Handles _Key.AllTrue
        Me.Button1.Enabled = True
    End Sub
    Private Sub Waiting() Handles _Key.Change
        Me.Button1.Enabled = False
    End Sub
    Private Sub NumericUpDown1_ValueChanged() Handles NUD_1.ValueChanged
        NUD_2.Minimum = NUD_1.Value
    End Sub
    Private Sub CB_AT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_AT.SelectedIndexChanged
        If CB_AT.SelectedIndex = CB_AT.Items.Count - 1 Then
            TXT_AT.Text = Nothing
            TXT_AT.Visible = True
        Else
            TXT_AT.Visible = False
            TXT_AT.Text = CB_AT.SelectedItem
        End If
        TextBox1_TextChanged()
    End Sub
#End Region


End Class

Public Delegate Sub KeyListEventHandler()
Public Class KeyList
    Public Event AllFalse As KeyListEventHandler
    Public Event AllTrue As KeyListEventHandler
    Public Event Change As KeyListEventHandler

    Private _List As List(Of Boolean)
    Public ReadOnly Property List As List(Of Boolean)
        Get
            Try
                Return _List
            Catch ex As Exception
                Return _List
            Finally
                RaiseEvent Change()
                If Iswhat(False) Then RaiseEvent AllFalse()
                If Iswhat(True) Then RaiseEvent AllTrue()
            End Try
        End Get
    End Property
    Sub New(ByVal ParamArray Bool() As Boolean)
        _List = New List(Of Boolean)
        For Each B As Boolean In Bool
            _List.Add(B)
        Next
    End Sub
    Private Function Iswhat(ByVal key As Boolean) As Boolean
        Dim nb As Integer = 0

        For Each bool As Boolean In _List
            If bool = key Then nb += 1
        Next

        If nb = _List.Count Then Return True Else Return False
    End Function
End Class