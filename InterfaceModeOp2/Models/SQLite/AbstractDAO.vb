Imports System.Data.SQLite

Public MustInherit Class AbstractDAO

    Protected Const ERR_INSERT = "Erreur lors de l'insertion de l'objet {0} : {1}"
    Protected Const ERR_GETALL = "Erreur lors de la récupération de la liste des objets {0}"
    Protected Const ERR_GETID = "Erreur lors de la récupération de l'objet {0} ayant l'id {1}"
    Protected Const ERR_UPDATE = "Erreur lors de la mise à jour de l'objet {0} : {1}"
    Protected Const ERR_UPDATE_NOT1 = "l'objet {0} ayant l'id n°{1} n'a pas pû être mis à jour"
    Protected Const ERR_DELETE = "Erreur lors de la suppression de l'objet {0} : {1}"

    Protected Const DAO_LASTID = ";select last_insert_rowid()"

    Private Const DAO_INSERT = "DAO_INSERT"
    Private Const DAO_GETALL = "DAO_GETALL"
    Private Const DAO_GETID = "DAO_GETID {0}"
    Private Const DAO_UPDATE = "DAO_UPDATE"
    Private Const DAO_DELETE = "DAO_DELETE"


    Protected Function DAOInsert(ByVal RQ_INSERT As String, ByVal ParamArray ParamList() As SQLiteParameter) As Double
        Dim newID As Double = -1

        'définition de la requete
        Using tr As SQLiteTransaction = DAOFactory.getCon.BeginTransaction
            Using requete As SQLiteCommand = New SQLiteCommand()
                Try
                    'ajout dans la table
                    With requete
                        .Transaction = tr
                        .CommandText = RQ_INSERT & DAO_LASTID
                        .Parameters.AddRange(ParamList)
                        .Prepare()
                        newID = .ExecuteScalar()
                    End With

                Catch ex As Exception
                    tr.Rollback()
                    Throw New DAOException(getErrorMsg(RQ_INSERT, ex.Message, ParamList))
                End Try

                tr.Commit()
                Return newID
            End Using
        End Using
    End Function

    Protected Function DAOGetAll(ByVal RQ_SELECTALL As String) As SQLiteDataReader
        'définition de la requete
        Using requete As SQLiteCommand = New SQLiteCommand(RQ_SELECTALL, DAOFactory.getCon)

            Try
                'execution de la requete
                Dim reader As SQLiteDataReader = requete.ExecuteReader()

                'récupération du jeu de résultats
                Return reader

            Catch ex As Exception
                Throw New DAOException(getErrorMsg(RQ_SELECTALL, ex.Message))
            End Try
        End Using
    End Function

    Protected Function DAOGetById(ByRef id As Integer, ByVal RQ_GETID As String) As SQLiteDataReader
        'définition de la requete
        Using requete As SQLiteCommand = New SQLiteCommand(RQ_GETID, DAOFactory.getCon)
            Try

                'définition des paramètres à mettre à jour
                Dim p1 = New SQLiteParameter()
                p1.DbType = DbType.Double
                p1.Value = id

                'intégration des paramètres à la requete
                requete.Parameters.Add(p1)

                'execution de la requete
                requete.Prepare()
                Dim reader As SQLiteDataReader = requete.ExecuteReader()

                Return reader

            Catch ex As Exception
                Throw New DAOException(getErrorMsg(RQ_GETID, ex.Message, id))
            End Try
        End Using
    End Function

    Protected Sub DAOUpdate(ByVal RQ_UPDATE As String, ByVal ParamArray ParamList() As SQLiteParameter)
        'définition de la requete
        Using requete As SQLiteCommand = New SQLiteCommand(RQ_UPDATE, DAOFactory.getCon)

            Try

                'intégration des paramètres à la requete
                requete.Parameters.AddRange(ParamList)

                'exécution de la requete
                requete.Prepare()
                requete.ExecuteNonQuery()

            Catch ex As Exception
                Throw New DAOException(getErrorMsg(RQ_UPDATE, ex.Message, ParamList))
            End Try
        End Using
    End Sub

    Protected Function DAODelete(ByVal id As Double, ByVal RQ_DELETE As String) As Boolean
        Dim SuppObj As Boolean = False

        'définition de la requete
        Using requete As SQLiteCommand = New SQLiteCommand(RQ_DELETE, DAOFactory.getCon)

            Try

                'définition des paramètres à mettre à jour
                Dim p1 = New SQLiteParameter()
                p1.DbType = DbType.Double
                p1.Value = id

                'intégration des paramètres à la requete
                requete.Parameters.Add(p1)

                'exécution de la requete
                requete.Prepare()
                SuppObj = requete.ExecuteNonQuery()

            Catch ex As Exception
                Throw New DAOException(getErrorMsg(RQ_DELETE, ex.Message, id))
            End Try

            Return SuppObj
        End Using
    End Function

    Private Overloads Function getErrorMsg(ByVal requete As String, ByVal errmsg As String, Optional ByVal id As Integer = -1)
        Dim msg As New System.Text.StringBuilder(requete)
        With msg
            .AppendLine()

            'ajout liste des paramètres si présente
            If id <> -1 Then
                .Append("[").Append(id)
                .Append("]").AppendLine()
            End If

            .Append(errmsg)

        End With
        Return msg.ToString
    End Function

    Private Overloads Function getErrorMsg(ByVal requete As String, ByVal errmsg As String, ByVal ParamArray params() As SQLiteParameter) As String
        Dim msg As New System.Text.StringBuilder(requete)
        With msg
            .AppendLine()

            'ajout liste des paramètres si présente
            If Not IsNothing(params) Then
                .Append("[")
                For Each p As SQLiteParameter In params
                    .Append(p.Value).Append(", ")
                Next
                .Remove(.Length - 2, 2)
                .Append("]").AppendLine()
            End If

            .Append(errmsg)

        End With
        Return msg.ToString
    End Function

End Class
