Imports System.Data.SQLite
Imports System.IO

''' <summary>
''' Singleton de connexion base SQLite
''' </summary>
''' <remarks></remarks>
Public Class Singleton
    'Paramètres de connexion
    Private Const DATASOURCE = "Data Source="
    Public Const DBFOLDER = "dbFolder"
    Private Const VERSION = "Version=3"
    Private Const PASSWORD = "Password="
    Private Const SEPARATOR = ";"
    Private pwd As String = ""

    'Objet de type connexion SQLite
    Private Shared Instance As Singleton = Nothing
    Private Shared _Connexion As SQLiteConnection = Nothing

    ''' <summary>
    ''' chaine de connexion à la base de données
    ''' </summary>
    ''' <value></value>
    ''' <returns>Chaine de connexion SQLite</returns>
    ''' <remarks></remarks>
    Private ReadOnly Property connString() As String
        Get
            Dim DBFILE = Configuration.getInstance.GetValueFromKey(DBFOLDER)
            If pwd.Equals(String.Empty) Then
                Return DATASOURCE & DBFILE & SEPARATOR & VERSION
            Else
                Return DATASOURCE & DBFILE & SEPARATOR & VERSION & SEPARATOR & PASSWORD & pwd
            End If
        End Get
    End Property

    ''' <summary>
    ''' Constructeur en mode singleton
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
        Try
            'création de la connexion
            _Connexion = New SQLiteConnection(connString())

            'ouverture de la connexion
            _Connexion.Open()

            '_Connexion.ChangePassword("newPwd")
        Catch ex As Exception
            Throw New DAOException("Problème lors de la connexion à la base de données", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Récupération de l'instance de connexion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance() As SQLiteConnection
        Try
            If _Connexion Is Nothing Then
                Instance = New Singleton()
                'Console.WriteLine("Connexion bdd ok : " & vbNewLine & _Connexion.FileName)
            End If
            Return _Connexion
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' fermeture de la connexion
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub close()

        If Not _Connexion Is Nothing Then
            _Connexion.Close()
            _Connexion = Nothing
        End If

    End Sub


End Class
