Imports System.Data.SQLite

Public Class DAOVerification
    Inherits AbstractDAO
    Implements IDAO(Of StatutVerification)

    Public Function dbDelete(ByRef value As StatutVerification) As Boolean Implements IDAO(Of StatutVerification).dbDelete
        Return MyBase.DAODelete(value.idVerification, VRF_DELETE)
    End Function

    Public Function dbGetAll() As System.Collections.Generic.List(Of StatutVerification) Implements IDAO(Of StatutVerification).dbGetAll
        'création de la liste
        Dim maListeObj As New List(Of StatutVerification)

        Try
            'execution de la requete
            Dim reader As SQLiteDataReader = MyBase.DAOGetAll(VRF_READ)
            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New StatutVerification(reader(0).ToString,
                                       reader(1).ToString))
            End While
            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, VRF_ERR_TYPEOF), ex)
        End Try
        Return maListeObj
    End Function

    Public Function dbGetById(ByRef id As Integer) As StatutVerification Implements IDAO(Of StatutVerification).dbGetById
        Dim obj As StatutVerification = Nothing

        Try

            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, VRF_READBYID)

            If reader.Read() Then
                'récupération du résultat
                obj = New StatutVerification(reader(0).ToString,
                                       reader(1).ToString)
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETID, VRF_ERR_TYPEOF, id), ex)
        End Try

        Return obj

    End Function

    Public Sub dbInsert(ByRef value As StatutVerification) Implements IDAO(Of StatutVerification).dbInsert

        Try

            value.idVerification = MyBase.DAOInsert(VRF_CREATE, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, VRF_ERR_TYPEOF, value.ToString), ex)
        End Try

    End Sub

    Public Sub dbUpdate(ByRef value As StatutVerification) Implements IDAO(Of StatutVerification).dbUpdate
        Try

            MyBase.DAOUpdate(VRF_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, VRF_ERR_TYPEOF, value.ToString), ex)
        End Try

    End Sub

    Public Function getInsertParameters(ByVal value As StatutVerification) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of StatutVerification).getInsertParameters

        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.getCommentaire
        Return {p1}
    End Function

    Public Function getUpdateParameters(ByVal value As StatutVerification) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of StatutVerification).getUpdateParameters
        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.Double
        p2.Value = value.idVerification
        Return {getInsertParameters(value), p2}
    End Function

End Class
