Public Class DAOViews
    Inherits AbstractDAO

    Private Const _ATPRINTER = "select * from view_ATPrinter"

    Public Enum views As Byte
        Rien = 0
        ATPrinter = 1
    End Enum

    Public Function getAsDataTable(ByVal maVue As views) As DataTable
        Using maDataTable As New DataTable(maVue.ToString)
            Dim maRequete As String

            Select Case maVue
                Case views.ATPrinter
                    maRequete = _ATPRINTER
                Case Else
                    maRequete = Nothing
            End Select

            If Not IsNothing(maRequete) Then
                Dim reader As SQLite.SQLiteDataReader = MyBase.DAOGetAll(maRequete)
                For i = 1 To reader.FieldCount
                    maDataTable.Columns.Add(reader.GetName(i - 1))
                Next

                While (reader.Read())
                    Dim mesRow As New List(Of Object)
                    For i = 1 To reader.FieldCount
                        mesRow.Add(reader(i - 1).ToString)
                    Next
                    maDataTable.Rows.Add(mesRow.ToArray)
                End While
            End If

            Return maDataTable
        End Using
    End Function
End Class
