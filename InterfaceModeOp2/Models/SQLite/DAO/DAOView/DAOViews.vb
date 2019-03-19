Public Class DAOViews
    Inherits AbstractDAO

    Private Const _ATPRINTER = "select * from view_ATPrinter"
    Private Const _ListeUser = "select * from view_ListeUtilisateur"
    Private Const _ATAutre = "select * from view_AT"

    Private Const _ATPRINTER_SIGNET = "select * from view_ATPrinter_Signets where id_auditrails = ?"

    ''' <summary>
    ''' Défini toutes les vues que le DataGridView est suceptible d'afficher
    ''' Elle devront être dans le table name du DGV
    ''' </summary>
    Public Enum views As Byte
        PasDeVue = 0
        ATPrinter = 1
        ListUser = 2
        ATImportExportArchivage = 3
        ATPrinter_Signet = 4
    End Enum

    Public Enum reqById As Byte
        GetSignet = 0
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
            End If

            createDataTable(maDataTable, reader)

            Return maDataTable
        End Using
    End Function

    Public Function getAll(ByVal maVue As views, ByVal nomTable As String) As DataTable
        Using maDataTable As New DataTable(nomTable)
            Dim reader As SQLite.SQLiteDataReader

            Select Case maVue
                Case views.ATPrinter
                    reader = MyBase.DAOGetAll(_ATPRINTER)
                Case views.ListUser
                    reader = MyBase.DAOGetAll(_ListeUser)
                Case views.ATImportExportArchivage
                    reader = MyBase.DAOGetAll(_ATAutre)
                Case Else
                    reader = Nothing
            End Select

            createDataTable(maDataTable, reader)

            Return maDataTable
        End Using
    End Function
End Class
