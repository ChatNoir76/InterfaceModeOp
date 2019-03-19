Imports System.Data.SQLite

Public Class DAOImpression
    Inherits AbstractDAO
    Implements IDAO(Of Impression)

    Public Function dbDelete(ByRef value As Impression) As Boolean Implements IDAO(Of Impression).dbDelete
        Return MyBase.DAODelete(value.idImpression, IMP_DELETE)
    End Function

    Public Function dbGetAll() As System.Collections.Generic.List(Of Impression) Implements IDAO(Of Impression).dbGetAll
        'création de la liste
        Dim maListeObj As New List(Of Impression)

        Try

            'execution de la requete
            Dim reader As SQLiteDataReader = MyBase.DAOGetAll(IMP_READ)
            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New Impression(reader(0).ToString,
                                       reader(1).ToString,
                                       reader(2).ToString,
                                       reader(3).ToString,
                                       reader(4).ToString))
            End While
            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, IMP_ERR_TYPEOF), ex)
        End Try
        Return maListeObj
    End Function

    Public Function dbGetById(ByRef id As Integer) As Impression Implements IDAO(Of Impression).dbGetById
        Dim obj As Impression = Nothing

        Try
            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, IMP_READBYID)

            If reader.Read() Then
                'récupération du résultat
                obj = New Impression(reader(0).ToString,
                                           reader(1).ToString,
                                           reader(2).ToString,
                                           reader(3).ToString,
                                           reader(4).ToString)
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETID, IMP_ERR_TYPEOF, id), ex)
        End Try
        Return obj

    End Function

    Public Sub dbInsert(ByRef value As Impression) Implements IDAO(Of Impression).dbInsert
        Try

            value.idImpression = MyBase.DAOInsert(IMP_CREATE, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, IMP_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Sub dbUpdate(ByRef value As Impression) Implements IDAO(Of Impression).dbUpdate
        Try
            MyBase.DAOUpdate(IMP_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, IMP_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Function getInsertParameters(ByVal value As Impression) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of Impression).getInsertParameters
        'définition des paramètres à mettre à jour
        Dim p0 = New SQLiteParameter()
        p0.DbType = DbType.String
        p0.Value = value.getLot

        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.getNomImprimante

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.String
        p2.Value = value.getPagesImpression

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.String
        p3.Value = value.idAuditrails
        Return {p0, p1, p2, p3}
    End Function

    Public Function getUpdateParameters(ByVal value As Impression) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of Impression).getUpdateParameters
        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.Double
        p4.Value = value.idImpression
        Return {getInsertParameters(value), p4}
    End Function
End Class
