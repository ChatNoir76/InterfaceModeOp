Imports System.Data.SQLite

Public Class DAO_HVerif
    Inherits AbstractDAO
    Implements IDAO_Histo(Of HistoVerification)

    Public Function dbGetAllStatutById(ByRef id As Integer) As System.Collections.Generic.List(Of HistoVerification) Implements IDAO_Histo(Of HistoVerification).dbGetAllStatutById
        'création de la liste
        Dim maListeObj As New List(Of HistoVerification)

        Try
            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, HVF_GETALL)

            'récupération du jeu de résultats
            While reader.Read()
                maListeObj.Add(New HistoVerification(reader(0).ToString,
                                       Convert.ToDateTime(reader(1).ToString),
                                       reader(2).ToString,
                                       reader(3).ToString))
            End While

            'libération des objets
            reader.Close()

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, HVF_ERR_TYPEOF), ex)
        End Try
        Return maListeObj
    End Function

    Public Function dbGetLastStatutById(ByRef id As Integer) As HistoVerification Implements IDAO_Histo(Of HistoVerification).dbGetLastStatutById
        'création de la liste
        Dim monObj As HistoVerification = Nothing

        Try
            Dim reader As SQLiteDataReader = MyBase.DAOGetById(id, HVF_GETLAST)
            'récupération du jeu de résultats

            If reader.Read Then
                monObj = New HistoVerification(reader(0).ToString,
                       Convert.ToDateTime(reader(1).ToString),
                       reader(2).ToString,
                       reader(3).ToString)
            End If

            'libération des objets
            reader.Close()
        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_GETALL, HVF_ERR_TYPEOF), ex)
        End Try
        Return monObj
    End Function

    Public Sub dbInsert(ByRef value As HistoVerification) Implements IDAO_Histo(Of HistoVerification).dbInsert
        Try
            MyBase.DAOInsert(HVF_INSERT, getInsertParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_INSERT, HVF_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Sub dbUpdate(ByRef value As HistoVerification) Implements IDAO_Histo(Of HistoVerification).dbUpdate
        Dim nbLigneUpdate As Integer = 0

        Try

            MyBase.DAOUpdate(HVF_UPDATE, getUpdateParameters(value))

        Catch ex As Exception
            Throw New DAOException(String.Format(ERR_UPDATE, HVF_ERR_TYPEOF, value.ToString), ex)
        End Try
    End Sub

    Public Function getInsertParameters(ByVal value As HistoVerification) As System.Data.SQLite.SQLiteParameter() Implements IDAO_Histo(Of HistoVerification).getInsertParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.commentaire

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.String
        p2.Value = value.getDate

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.Double
        p3.Value = value.getIdAuditrails

        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.Double
        p4.Value = value.getIdVerification
        Return {p1, p2, p3, p4}
    End Function

    Public Function getUpdateParameters(ByVal value As HistoVerification) As System.Data.SQLite.SQLiteParameter() Implements IDAO_Histo(Of HistoVerification).getUpdateParameters
        'définition des paramètres à mettre à jour
        Dim p1 = New SQLiteParameter()
        p1.DbType = DbType.String
        p1.Value = value.commentaire

        Dim p2 = New SQLiteParameter()
        p2.DbType = DbType.Double
        p2.Value = value.getIdAuditrails

        Dim p3 = New SQLiteParameter()
        p3.DbType = DbType.Double
        p3.Value = value.getIdVerification

        Dim p4 = New SQLiteParameter()
        p4.DbType = DbType.String
        p4.Value = value.getDate
        Return {p1, p2, p3, p4}
    End Function

End Class
