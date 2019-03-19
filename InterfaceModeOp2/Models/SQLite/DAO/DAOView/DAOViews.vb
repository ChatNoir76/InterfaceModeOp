Public Class DAOViews
    Inherits AbstractDAO

    Private Const _ATPRINTER_ALL = "select * from view_ATPrinter"
    Private Const _ATPRINTER_EC = "select * from view_ATPrinter where id_verification < 2"

    Private Const _ATPRINTER_SIGNET = "select * from view_ATPrinter_Signets where id_auditrails = ?"

    Private Const _ATAUTRE_ALL = "select * from view_AT"
    Private Const _ATAUTRE_EC = "select * from view_AT where id_verification < 2"

    Private Const _LISTEUSER = "select * from view_ListeUtilisateur"
    Private Const _HISTO_USER = "select * from view_ListeDroitUtilisateur where id_utilisateur = ?"
    Private Const _HISTO_VERIF = "select * from view_HistoVerif where id_auditrails = ?"

    ''' <summary>
    ''' Défini toutes les vues que le DataGridView est suceptible d'afficher
    ''' Elle devront être dans le table name du DGV
    ''' </summary>
    Public Enum views As Byte
        PasDeVue = 0
        AT_Printer_ALL = 1
        User_ALL = 2
        AT_IEA_ALL = 3
        AT_Printer_Signet = 4
        Histo_DroitUser = 5
        Histo_VerifAT = 6
        AT_Printer_Encours = 7
        AT_IEA_Encours = 8
    End Enum

    Public Enum reqById As Byte
        GetSignet = 0
        GetHistoDroit = 1
        GetHistoVerif = 2
    End Enum

    Private Sub createDataTable(ByRef maDataTable As DataTable, ByVal monReader As SQLite.SQLiteDataReader)
        If Not IsNothing(monReader) Then

            'Faire une vérif si colonnes identique car sinon bug....
            For i = 1 To monReader.FieldCount
                maDataTable.Columns.Add(monReader.GetName(i - 1))
            Next

            While (monReader.Read())
                Dim mesRow As New List(Of Object)
                For i = 1 To monReader.FieldCount
                    mesRow.Add(monReader(i - 1).ToString)
                Next
                maDataTable.Rows.Add(mesRow.ToArray)
            End While
        End If
    End Sub

    Public Function getById(ByVal id As Integer, ByVal requete As reqById, ByVal nomTable As String) As DataTable
        Using maDataTable As New DataTable(nomTable)
            Dim reader As SQLite.SQLiteDataReader = Nothing

            If requete = reqById.GetSignet Then
                reader = MyBase.DAOGetById(id, _ATPRINTER_SIGNET)
            ElseIf requete = reqById.GetHistoDroit Then
                reader = MyBase.DAOGetById(id, _HISTO_USER)
            ElseIf requete = reqById.GetHistoVerif Then
                reader = MyBase.DAOGetById(id, _HISTO_VERIF)
            End If

            createDataTable(maDataTable, reader)

            Return maDataTable
        End Using
    End Function

    Public Function getAll(ByVal maVue As views, ByVal nomTable As String) As DataTable
        Using maDataTable As New DataTable(nomTable)
            Dim reader As SQLite.SQLiteDataReader

            Select Case maVue
                Case views.AT_Printer_ALL
                    reader = MyBase.DAOGetAll(_ATPRINTER_ALL)
                Case views.User_ALL
                    reader = MyBase.DAOGetAll(_LISTEUSER)
                Case views.AT_IEA_ALL
                    reader = MyBase.DAOGetAll(_ATAUTRE_ALL)
                Case views.AT_Printer_Encours
                    reader = MyBase.DAOGetAll(_ATPRINTER_EC)
                Case views.AT_IEA_Encours
                    reader = MyBase.DAOGetAll(_ATAUTRE_EC)
                Case Else
                    reader = Nothing
            End Select

            createDataTable(maDataTable, reader)

            Return maDataTable
        End Using
    End Function
End Class
