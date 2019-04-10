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
    Private Const PWD = "tXpD56gN"
    Private Const SEPARATOR = ";"
    Private Const RQ_TEST = "select * from enum_droits"

    'Objet de type connexion SQLite
    Private Shared _Instance As Singleton = Nothing
    Private Shared _Connexion As SQLiteConnection = Nothing
    Private Shared _ModeProtection As SByte = -1

    ''' <summary>
    ''' -1:Probleme; 0:NonProtege, 1:protege
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property getModeProtection() As SByte
        Get
            Return _ModeProtection
        End Get
    End Property

    ''' <summary>
    ''' chaine de connexion à la base de données
    ''' </summary>
    ''' <value></value>
    ''' <returns>Chaine de connexion SQLite</returns>
    ''' <remarks></remarks>
    Private ReadOnly Property connString(Optional ByVal bddProtege As Boolean = True) As String
        Get
            Dim DBFILE = Configuration.getInstance.GetValueFromKey(DBFOLDER)
            If Not bddProtege Then
                Return DATASOURCE & DBFILE & SEPARATOR & VERSION
            Else
                Return DATASOURCE & DBFILE & SEPARATOR & VERSION & SEPARATOR & PASSWORD & PWD
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

            If Not ConnexionOK() Then
                _Connexion = New SQLiteConnection(connString(False))
                _Connexion.Open()
                _ModeProtection = 0
            Else
                _ModeProtection = 1
            End If

        Catch ex As Exception
            Throw New DAOException("Problème lors de la connexion à la base de données", ex)
        End Try
    End Sub

    'Test la connexion en vérifiant si résultat avec enum_droit
    'Permet de tester si base protégé par mot de passe
    'car si mdp vide, la connexion est ok mais aucune table visible
    Private Function ConnexionOK() As Boolean
        Using requete As SQLiteCommand = New SQLiteCommand(RQ_TEST, _Connexion)

            Try
                Dim reader As SQLiteDataReader = requete.ExecuteReader()

                If reader.Read() Then
                    Return True
                Else : Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    Public Shared Sub protectionBDD(Optional ByVal protect As Boolean = True)
        If protect Then
            getInstance.ChangePassword(PWD)
            _ModeProtection = 1
        Else
            getInstance.ChangePassword("")
            _ModeProtection = 0
        End If
    End Sub

    ''' <summary>
    ''' Récupération de l'instance de connexion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance() As SQLiteConnection
        Try
            If _Connexion Is Nothing Then
                _Instance = New Singleton()
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
