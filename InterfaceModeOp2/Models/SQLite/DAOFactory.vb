Imports System.Data.SQLite

Public Class DAOFactory

    Private Shared Function getConnexion() As SQLiteConnection
        Return Singleton.getInstance()
    End Function

    Public Shared Function getUtilisateur() As DAOUtilisateur
        getConnexion()
        Return New DAOUtilisateur()
    End Function

    Public Shared Function getAuditrails() As DAOAuditrails
        getConnexion()
        Return New DAOAuditrails
    End Function

    Public Shared Function getImpression() As DAOImpression
        getConnexion()
        Return New DAOImpression
    End Function

    Public Shared Function getSignet() As DAOSignet
        getConnexion()
        Return New DAOSignet
    End Function

    Public Shared Function getHistoDroit() As DAO_HDroit
        getConnexion()
        Return New DAO_HDroit
    End Function

    Public Shared Function getHistoVerification() As DAO_HVerif
        getConnexion()
        Return New DAO_HVerif
    End Function

    Public Shared Function getATPrinter() As DAOSpecificTransaction
        getConnexion()
        Return New DAOSpecificTransaction
    End Function

    Public Shared Function getViews() As DAOViews
        getConnexion()
        Return New DAOViews
    End Function

End Class
