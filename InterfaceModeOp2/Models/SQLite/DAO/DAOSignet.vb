Imports System.Data.SQLite

Public Class DAOSignet
    Inherits AbstractDAO
    Implements IDAO(Of Signets)

    Public Sub dbInsertMulti(ByRef value As List(Of Signets))
        Dim tr As SQLiteTransaction = Singleton.getInstance.BeginTransaction
        Using tr
            Try
                For Each signet As Signets In value
                    Dim cmd As SQLiteCommand = Singleton.getInstance.CreateCommand
                    With cmd
                        .Transaction = tr
                        .CommandText = SGT_CREATE
                        .Parameters.AddRange(getInsertParameters(signet))
                        .Prepare()
                        .ExecuteNonQuery()
                    End With
                Next
            Catch ex As Exception
                tr.Rollback()
                Throw New DAOException("erreur lors de l'insertion des signets dans la table signet", ex)
            End Try
            tr.Commit()
        End Using
    End Sub

    Public Function dbDelete(ByRef value As Signets) As Boolean Implements IDAO(Of Signets).dbDelete
        Return MyBase.DAODelete(value.idSignet, SGT_DELETE)
    End Function

    Public Function dbGetAll() As System.Collections.Generic.List(Of Signets) Implements IDAO(Of Signets).dbGetAll
        'création de la liste
        Dim maListeObj As New List(Of Signets)

        Try

            'execution de la requete
            Dim reader As SQLiteDataReader = MyBase.DAOGetAll(SGT_READ)
            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New Signets(reader(0).ToString,
                                       reader(1).ToString,
                                       reader(2).ToString,
                                       reader(3).ToString,
                                       reader(4).ToString))
            End While
            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, SGT_ERR_TYPEOF), ex)
        End Try
        Return maListeObj
    End Function

    Public Function dbGetById(ByRef id As Integer) As Signets Implements IDAO(Of Signets).dbGetById
        Dim obj As Signets = Nothing

        Try

            'définition des paramètres à mettre à jour
            Dim p1 = New SQLiteParameter()
            p1.DbType = DbType.Double
            p1.Value = id

            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, SGT_READBYID)

            If reader.Read() Then
                'récupération du résultat
                obj = New Signets(reader(0).ToString,
                                           reader(1).ToString,
                                           reader(2).ToString,
                                           reader(3).ToString,
                                           reader(4).ToString)
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETID, SGT_ERR_TYPEOF, id), ex)
        End Try
        Return obj

    End Function

    Public Sub dbInsert(ByRef value As Signets) Implements IDAO(Of Signets).dbInsert
        Try
            value.idSignet = MyBase.DAOInsert(SGT_CREATE, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, SGT_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Sub dbUpdate(ByRef value As Signets) Implements IDAO(Of Signets).dbUpdate
        Try

            MyBase.DAOUpdate(SGT_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, SGT_ERR_TYPEOF, value.ToString), ex)
        End Try

    End Sub

    Public Function getInsertParameters(ByVal value As Signets) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of Signets).getInsertParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.getClefSignet

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.String
        p2.Value = value.getValeurSignet

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.String
        p3.Value = value.getCodeSignet

        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.Double
        p4.Value = value.idImpression

        Return {p1, p2, p3, p4}
    End Function

    Public Function getUpdateParameters(ByVal value As Signets) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of Signets).getUpdateParameters

        Dim p5 = New SQLiteParameter()
        p5.DbType = DbType.Double
        p5.Value = value.idSignet

        Return {getInsertParameters(value), p5}

    End Function
End Class
