Imports System.IO

''' <summary>
''' Lecture et enregistrement des variables du fichier ini
''' Variables d'environnements de l'interface
''' </summary>
''' <remarks></remarks>
Public Class Configuration

    'instance du singleton
    Private Shared _Instance As Configuration = Nothing

    'nom du fichier ini à la racine de l'interface
    Private _MonFichier As String

    'liste des combinaisons clef / valeur récupéré du fichier ini
    Private _KeyValueList As Hashtable

    'Caractère spéciaux du fichier ini
    Private Const _COMMENTAIRE As String = ";"
    Private Const _REGION As String = "["
    Private Const _ATTRIBVAL As String = "="

    Public ReadOnly Property getCurrentDir() As String
        Get
            Return Directory.GetCurrentDirectory
        End Get
    End Property

    Private Sub New()
        'récupère le nom du fichier décrit dans les constantes
        _MonFichier = service.NOM_FICHIER_INI
        _KeyValueList = getData()
    End Sub

    Public Shared Function getInstance() As Configuration

        If _Instance Is Nothing Then
            _Instance = New Configuration()
        End If

        Return _Instance

    End Function

    ''' <summary>
    ''' Récupère les combinaisons clef / valeur du fichier ini
    ''' </summary>
    ''' <returns>une liste de combinaison clef / valeur</returns>
    ''' <remarks></remarks>
    Private Function getData() As Hashtable
        Dim listeArgs As New Hashtable

        If File.Exists(_MonFichier) Then
            For Each Ligne As String In File.ReadAllLines(_MonFichier)
                If Ligne.Contains(_ATTRIBVAL) Then
                    listeArgs.Add(Ligne.Split(_ATTRIBVAL)(0), Ligne.Split(_ATTRIBVAL)(1))
                End If
            Next
        End If

        Return listeArgs
    End Function

    ''' <summary>
    ''' Retourne la valeur d'une clef passé en paramètre
    ''' </summary>
    ''' <param name="Clef"></param>
    ''' <returns>soit la valeur, soit nothing</returns>
    ''' <remarks></remarks>
    Public Function GetValueFromKey(ByVal Clef As String) As String
        Return _KeyValueList(Clef)
    End Function


End Class
