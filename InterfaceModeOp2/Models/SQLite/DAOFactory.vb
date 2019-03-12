Imports System.Data.SQLite

Public Class DAOFactory

    Private Shared connection As SQLiteConnection = Nothing

    Public Shared ReadOnly Property getCon() As SQLiteConnection
        Get
            Return connection
        End Get
    End Property

    Sub New()
        connection = Singleton.getInstance()
    End Sub

    Public Function getUtilisateur() As DAOUtilisateur
        Return New DAOUtilisateur()
    End Function

    Public Function getStatutDroits() As DAODroits
        Return New DAODroits
    End Function

    Public Function getAuditrails() As DAOAuditrails
        Return New DAOAuditrails
    End Function

    Public Function getStatutVerification() As DAOVerification
        Return New DAOVerification
    End Function

    Public Function getImpression() As DAOImpression
        Return New DAOImpression
    End Function

    Public Function getSignet() As DAOSignet
        Return New DAOSignet
    End Function

    Public Function getHistoDroit() As DAO_HDroit
        Return New DAO_HDroit
    End Function

    Public Function getHistoVerification() As DAO_HVerif
        Return New DAO_HVerif
    End Function

    Public Function getATPrinter() As DAOSpecificTransaction
        Return New DAOSpecificTransaction
    End Function

End Class
