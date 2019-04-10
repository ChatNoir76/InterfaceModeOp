Imports System.Data.SQLite

Public Class DAOSpecificTransaction
    Inherits AbstractDAO

    Public Sub dbInsertATPrinter(ByRef arg_auditrails As auditrails, ByRef arg_impression As Impression, ByRef arg_signets As List(Of Signets))

        'pour une transaction SQLite
        Using tr As SQLiteTransaction = Singleton.getInstance.BeginTransaction
            Dim DAO_At As New DAOAuditrails
            Dim DAO_Imp As New DAOImpression
            Dim DAO_sig As New DAOSignet

            Dim cmd_AT As SQLiteCommand = Singleton.getInstance.CreateCommand
            With cmd_AT
                Try
                    'insertion dans la table audit trails
                    .Transaction = tr
                    .CommandText = AT_CREATE & DAO_LASTID
                    .Parameters.AddRange(DAO_At.getInsertParameters(arg_auditrails))
                    .Prepare()
                    arg_auditrails.idAuditrails = .ExecuteScalar()
                    'ajout du nouvel id à l'obj impression
                    arg_impression.idAuditrails = arg_auditrails.idAuditrails
                Catch ex As Exception
                    tr.Rollback()
                    Throw New DAOException("erreur lors de l'insertion de l'audit trails", ex)
                End Try
                If arg_auditrails.idAuditrails <= 0 Then
                    tr.Rollback()
                    Throw New DAOException("le programme n'a pas pu récupérer l'ID de l'audit trails")
                End If
            End With

            Dim cmd_IMP As SQLiteCommand = Singleton.getInstance.CreateCommand
            With cmd_IMP
                Try
                    'insertion dans la table audit trails
                    .Transaction = tr
                    .CommandText = IMP_CREATE & DAO_LASTID
                    .Parameters.AddRange(DAO_Imp.getInsertParameters(arg_impression))
                    .Prepare()
                    arg_impression.idImpression = .ExecuteScalar()
                Catch ex As Exception
                    tr.Rollback()
                    Throw New DAOException("erreur lors de l'insertion de l'audit trails", ex)
                End Try
                If arg_impression.idImpression <= 0 Then
                    tr.Rollback()
                    Throw New DAOException("le programme n'a pas pu récupérer l'ID de l'enregistrement des infos d'impression")
                End If
            End With

            'insertion des signets
            For Each signet As Signets In arg_signets
                Dim cmd_signet As SQLiteCommand = Singleton.getInstance.CreateCommand
                With cmd_signet
                    Try
                        signet.idImpression = arg_impression.idImpression
                        .CommandText = SGT_CREATE & DAO_LASTID
                        .Parameters.AddRange(DAO_sig.getInsertParameters(signet))
                        .Prepare()
                        signet.idSignet = .ExecuteScalar

                        If signet.idSignet <= 0 Then
                            tr.Rollback()
                            Throw New DAOException("le programme n'a pas pu récupérer l'ID de l'enregistrement des signets d'impression")
                        End If
                    Catch ex As Exception
                        tr.Rollback()
                        Throw New DAOException("erreur lors de l'insertion des signets dans la table signet", ex)
                    End Try
                End With
            Next
            tr.Commit()
        End Using
    End Sub

End Class
