Imports System.Data.OleDb
Imports ConfigBDD.GBDD
Public Class ACCDBModeOp
    Inherits BDDACCDB

    Sub New(ByVal ClefConnexion As String)
        MyBase.New(ClefConnexion)
    End Sub

    Public Function AT_Impression(ByVal NomFichier As String, ByVal Imprimante As String, ByVal PageStart As Integer, ByVal PageEnd As Integer, ByVal Commentaire As String, ByVal Lot As String, ByVal Signets As String) As Integer
        'création de la requete
        Dim Maintenant As Date = Now
        Dim User As String = FSTR(Environment.UserName)
        Dim requete As String = "Insert Into ATPrint (NomFichier, Imprimante, PageStart, PageEnd, Commentaire, DateHeure, LoginWin, Lot, Signets) values ('"
        requete = requete & FSTR(NomFichier) & "', '"
        requete = requete & FSTR(Imprimante) & "', '"
        requete = requete & FSTR(PageStart) & "', '"
        requete = requete & FSTR(PageEnd) & "', '"
        requete = requete & FSTR(Commentaire) & "', '"
        requete = requete & Maintenant & "', '"
        requete = requete & User & "', '"
        requete = requete & FSTR(Lot) & "', '"
        requete = requete & FSTR(Signets) & "')"
        MyBase.EnvoiRequete(requete)

        Return RecupID(Maintenant, User)

    End Function
    Public Sub AT_Importation(ByVal NomFichier As String, ByVal Commentaire As String)
        'création de la requete
        Dim requete As String = "Insert Into ATImport (NomFichier, Commentaire, DateHeure, LoginWin) values ('"
        requete = requete & FSTR(NomFichier) & "', '"
        requete = requete & FSTR(Commentaire) & "', '"
        requete = requete & Now() & "', '"
        requete = requete & FSTR(Environment.UserName) & "')"
        MyBase.EnvoiRequete(requete)
    End Sub
    Public Sub AT_Exportation(ByVal NomFichier As String, ByVal Commentaire As String)
        'création de la requete
        Dim requete As String = "Insert Into ATExport (NomFichier, Commentaire, DateHeure, LoginWin) values ('"
        requete = requete & FSTR(NomFichier) & "', '"
        requete = requete & FSTR(Commentaire) & "', '"
        requete = requete & Now() & "', '"
        requete = requete & FSTR(Environment.UserName) & "')"
        MyBase.EnvoiRequete(requete)
    End Sub
    Private Function RecupID(ByVal Maintenant As Date, ByVal User As String) As Integer
        Dim requete As String = "Select * From ATPrint Where LoginWin='" & User & "'"
        On Error Resume Next
        Dim Commande As New OleDbCommand(requete, MyBase.Connexion)
        Dim MyReader As OleDbDataReader = Commande.ExecuteReader
        Dim ID As Integer

        While MyReader.Read()
            If CDate(MyReader("DateHeure").ToString) = CDate(Maintenant.ToString) Then
                ID = MyReader("ID").ToString
            End If
        End While
        Commande.Dispose()
        MyReader.Close()

        Return ID

    End Function

End Class
