Imports System.Data.SQLite

Public Class DAOAuditrails
    Inherits AbstractDAO
    Implements IDAO(Of auditrails)

    Public Function dbDelete(ByRef value As auditrails) As Boolean Implements IDAO(Of auditrails).dbDelete
        Return MyBase.DAODelete(value.idAuditrails, AT_DELETE)
    End Function

    Public Function dbGetAll() As System.Collections.Generic.List(Of auditrails) Implements IDAO(Of auditrails).dbGetAll
        'création de la liste
        Dim maListeObj As New List(Of auditrails)

        Try
            'execution de la requete
            Dim reader As SQLiteDataReader = MyBase.DAOGetAll(AT_READ)
            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New auditrails(reader(0).ToString,
                                       reader(1).ToString,
                                       reader(2).ToString,
                                       Convert.ToDateTime(reader(3).ToString),
                                       reader(4).ToString))
            End While
            'libération des objets
            reader.Close()
            Return maListeObj
        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, AT_ERR_TYPEOF), ex)
        End Try
    End Function

    Public Function dbGetById(ByRef id As Integer) As auditrails Implements IDAO(Of auditrails).dbGetById
        Dim obj As auditrails = Nothing

        Try

            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, AT_READBYID)
            If reader.Read() Then
                'récupération du résultat
                obj = New auditrails(reader(0).ToString,
                                            reader(1).ToString,
                                            reader(2).ToString,
                                            Convert.ToDateTime(reader(3).ToString),
                                            reader(4).ToString)
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETID, AT_ERR_TYPEOF, id), ex)
        End Try
        Return obj
    End Function

    Public Sub dbInsert(ByRef value As auditrails) Implements IDAO(Of auditrails).dbInsert
        Try
            value.idAuditrails = MyBase.DAOInsert(AT_CREATE, getInsertParameters(value))
        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, AT_ERR_TYPEOF, value.ToString), ex)
        End Try

    End Sub

    Public Sub dbUpdate(ByRef value As auditrails) Implements IDAO(Of auditrails).dbUpdate

        Try
            'définition des paramètres à mettre à jour
            Dim p1 = New SQLiteParameter()
            p1.DbType = DbType.String
            p1.Value = value.getNomFichierAuditrails

            Dim p2 = New SQLiteParameter()
            p2.DbType = DbType.String
            p2.Value = value.getCommentaireAuditrails

            Dim p3 = New SQLiteParameter()
            p3.DbType = DbType.String
            p3.Value = value.getDateAuditrails

            Dim p4 = New SQLiteParameter()
            p4.DbType = DbType.Double
            p4.Value = value.getIdUtilisateur

            Dim p5 = New SQLiteParameter()
            p5.DbType = DbType.Double
            p5.Value = value.idAuditrails

            MyBase.DAOUpdate(AT_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, AT_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Function getInsertParameters(ByVal value As auditrails) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of auditrails).getInsertParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.getNomFichierAuditrails

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.String
        p2.Value = value.getCommentaireAuditrails

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.String
        p3.Value = value.getDateAuditrails

        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.Double
        p4.Value = value.getIdUtilisateur

        Return {p1, p2, p3, p4}
    End Function

    Public Function getUpdateParameters(ByVal value As auditrails) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of auditrails).getUpdateParameters
        Dim p5 = New SQLiteParameter()
        p5.DbType = DbType.Double
        p5.Value = value.idAuditrails
        Return {getInsertParameters(value), p5}
    End Function
End Class
