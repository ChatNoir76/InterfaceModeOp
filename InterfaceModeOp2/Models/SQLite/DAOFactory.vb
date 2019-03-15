Imports System.Data.SQLite

Public Class DAOFactory

    Private Shared connection As SQLiteConnection = Singleton.getInstance()

    Public Shared ReadOnly Property getConnexion() As SQLiteConnection
        Get
            Return connection
        End Get
    End Property

    Public Shared Function getUtilisateur() As DAOUtilisateur
        Return New DAOUtilisateur()
    End Function

    Public Shared Function getAuditrails() As DAOAuditrails
        Return New DAOAuditrails
    End Function

    Public Shared Function getImpression() As DAOImpression
        Return New DAOImpression
    End Function

    Public Shared Function getSignet() As DAOSignet
        Return New DAOSignet
    End Function

    Public Shared Function getHistoDroit() As DAO_HDroit
        Return New DAO_HDroit
    End Function

    Public Shared Function getHistoVerification() As DAO_HVerif
        Return New DAO_HVerif
    End Function

    Public Shared Function getATPrinter() As DAOSpecificTransaction
        Return New DAOSpecificTransaction
    End Function

    Public Shared Function getViews() As DAOViews
        Return New DAOViews
    End Function

End Class
