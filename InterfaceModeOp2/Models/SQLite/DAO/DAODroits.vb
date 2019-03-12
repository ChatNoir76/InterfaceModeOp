Imports System.Data.SQLite
Public Class DAODroits
    Inherits AbstractDAO
    Implements IDAO(Of StatutDroit)

    Public Function dbDelete(ByRef value As StatutDroit) As Boolean Implements IDAO(Of StatutDroit).dbDelete
        Return MyBase.DAODelete(value.idDroit, DRT_DELETE)
    End Function

    Public Function dbGetAll() As List(Of StatutDroit) Implements IDAO(Of StatutDroit).dbGetAll
        'création de la liste
        Dim maListeObj As New List(Of StatutDroit)
        Try
            'execution de la requete
            Dim reader As SQLiteDataReader = MyBase.DAOGetAll(DRT_READ)
            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New StatutDroit(reader(0).ToString,
                                       reader(1).ToString))
            End While
            'libération des objets
            reader.Close()
        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, DRT_ERR_TYPEOF), ex)
        End Try
        Return maListeObj
    End Function

    Public Function dbGetById(ByRef id As Integer) As StatutDroit Implements IDAO(Of StatutDroit).dbGetById
        Dim obj As StatutDroit = Nothing

        Try
            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, DRT_READBYID)

            If reader.Read() Then
                'récupération du résultat
                obj = New StatutDroit(reader(0).ToString,
                                       reader(1).ToString)
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETID, DRT_ERR_TYPEOF, id), ex)
        End Try
        Return obj
    End Function

    Public Sub dbInsert(ByRef value As StatutDroit) Implements IDAO(Of StatutDroit).dbInsert

        Try
            'intégration des paramètres à la requete
            value.idDroit = MyBase.DAOInsert(DRT_CREATE, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, DRT_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Sub dbUpdate(ByRef value As StatutDroit) Implements IDAO(Of StatutDroit).dbUpdate

        Try

            MyBase.DAOUpdate(DRT_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, DRT_ERR_TYPEOF, value.ToString), ex)
        End Try

    End Sub

    Public Function getInsertParameters(ByVal value As StatutDroit) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of StatutDroit).getInsertParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.getNomDroit
        Return {p1}
    End Function

    Public Function getUpdateParameters(ByVal value As StatutDroit) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of StatutDroit).getUpdateParameters
        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.Double
        p2.Value = value.idDroit
        Return {getInsertParameters(value), p2}
    End Function
End Class
