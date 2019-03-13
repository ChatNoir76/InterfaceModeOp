Imports System.Data.SQLite

Public Class DAO_HDroit
    Inherits AbstractDAO
    Implements IDAO_Histo(Of HistoDroits)

    Public Function dbGetAllStatutById(ByRef idObj As Integer) As System.Collections.Generic.List(Of HistoDroits) Implements IDAO_Histo(Of HistoDroits).dbGetAllStatutById
        'création de la liste
        Dim maListeObj As New List(Of HistoDroits)

        Try
            Dim reader As SQLiteDataReader = MyBase.DAOGetById(idObj, HDR_GETALL)

            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New HistoDroits(reader(0).ToString,
                                       reader(1).ToString,
                                       reader(2).ToString,
                                       reader(3).ToString))
            End While

            'libération des objets
            reader.Close()
        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, HDR_ERR_TYPEOF), ex)
        End Try
        Return maListeObj
    End Function

    Public Function dbGetLastStatutById(ByRef id As Integer) As HistoDroits Implements IDAO_Histo(Of HistoDroits).dbGetLastStatutById
        'création de la liste
        Dim monObj As HistoDroits = Nothing

        Try

            'définition des paramètres à mettre à jour
            Dim p1 = New SQLiteParameter()
            p1.DbType = DbType.Double
            p1.Value = id

            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, HDR_GETLAST)
            'récupération du jeu de résultats

            If reader.Read Then
                monObj = New HistoDroits(reader(0).ToString,
                       reader(1).ToString,
                       reader(2).ToString,
                       reader(3).ToString)
            End If

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, HDR_ERR_TYPEOF), ex)
        End Try
        Return monObj
    End Function

    Public Sub dbInsert(ByRef value As HistoDroits) Implements IDAO_Histo(Of HistoDroits).dbInsert
        Try
            MyBase.DAOInsert(HDR_INSERT, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, HDR_ERR_TYPEOF, value.ToString), ex)
        End Try

    End Sub

    Public Sub dbUpdate(ByRef value As HistoDroits) Implements IDAO_Histo(Of HistoDroits).dbUpdate
        Try
            MyBase.DAOUpdate(HDR_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, HDR_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Function getInsertParameters(ByVal value As HistoDroits) As System.Data.SQLite.SQLiteParameter() Implements IDAO_Histo(Of HistoDroits).getInsertParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.commentaire

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.String
        p2.Value = value.getDate

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.Double
        p3.Value = value.getIdUtilisateur

        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.Double
        p4.Value = value.getIdDroit
        Return {p1, p2, p3, p4}
    End Function

    Public Function getUpdateParameters(ByVal value As HistoDroits) As System.Data.SQLite.SQLiteParameter() Implements IDAO_Histo(Of HistoDroits).getUpdateParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.commentaire

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.Double
        p2.Value = value.getIdUtilisateur

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.Double
        p3.Value = value.getIdDroit

        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.String
        p4.Value = value.getDate
        Return {p1, p2, p3, p4}
    End Function
End Class
