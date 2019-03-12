Imports System.Data.SQLite
Public Class DAOUtilisateur
    Inherits AbstractDAO
    Implements IDAO(Of Utilisateur)

    Public Sub dbInsert(ByRef value As Utilisateur) Implements IDAO(Of Utilisateur).dbInsert
        Try

            value.idUtilisateur = MyBase.DAOInsert(USR_CREATE, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, USR_ERR_TYPEOF, value.toString), ex)
        End Try
    End Sub

    Public Function dbGetAll() As List(Of Utilisateur) Implements IDAO(Of Utilisateur).dbGetAll
        'création de la liste d'utilisateurs
        Dim users As New List(Of Utilisateur)

        Try

            'execution de la requete
            Dim reader As SQLiteDataReader = MyBase.DAOGetAll(USR_READ)
            'récupération du jeu de résultats
            While reader.Read()
                users.Add(New Utilisateur(reader(0).ToString,
                                       reader(1).ToString))
            End While
            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, USR_ERR_TYPEOF), ex)
        End Try

        Return users
    End Function

    Public Function dbGetById(ByRef id As Integer) As Utilisateur Implements IDAO(Of Utilisateur).dbGetById
        Dim monUtilisateur As Utilisateur = Nothing

        Try

            'définition des paramètres à mettre à jour
            Dim p1 = New SQLiteParameter()
            p1.DbType = DbType.Double
            p1.Value = id

            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, USR_READBYID)

            'récupération du résultat
            If reader.Read() Then
                monUtilisateur = (New Utilisateur(reader(0).ToString,
                                                   reader(1).ToString))
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETID, USR_ERR_TYPEOF, id), ex)
        End Try

        Return monUtilisateur

    End Function

    Public Function dbDelete(ByRef value As Utilisateur) As Boolean Implements IDAO(Of Utilisateur).dbDelete
        Return MyBase.DAODelete(value.idUtilisateur, USR_DELETE)
    End Function

    Public Sub dbUpdate(ByRef value As Utilisateur) Implements IDAO(Of Utilisateur).dbUpdate
        Dim nbLigneUpdate As Integer = 0

        Try

            MyBase.DAOUpdate(USR_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, USR_ERR_TYPEOF, value.toString), ex)
        End Try

    End Sub

    Public Function getInsertParameters(ByVal value As Utilisateur) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of Utilisateur).getInsertParameters

        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.getNomUtilisateur

        Return {p1}
    End Function

    Public Function getUpdateParameters(ByVal value As Utilisateur) As System.Data.SQLite.SQLiteParameter() Implements IDAO(Of Utilisateur).getUpdateParameters
        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.Double
        p3.Value = value.idUtilisateur

        Return {getInsertParameters(value), p3}
    End Function
End Class
