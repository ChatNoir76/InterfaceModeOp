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

    Public ReadOnly Property getBaseDir() As String
        Get
            Return Directory.GetCurrentDirectory
        End Get
    End Property
    Public ReadOnly Property getWorkDir() As String
        Get
            Dim WorkDir As New System.Text.StringBuilder(Directory.GetCurrentDirectory)
            With WorkDir
                .Append("\")
                .Append(Me.GetValueFromKey(service.INI_KEY_REPBASE))
            End With
            Return WorkDir.ToString
        End Get
    End Property
    Public ReadOnly Property getFullProdDir(ByVal dossierProd As service.DossierProd) As String
        Get
            Dim FullDir As New System.Text.StringBuilder(Directory.GetCurrentDirectory)
            With FullDir
                .Append("\")
                .Append(Me.GetValueFromKey(service.INI_KEY_REPBASE))
                .Append("\")
                .Append(Me.GetValueFromKey(dossierProd.ToString))
            End With
            Return FullDir.ToString
        End Get
    End Property
    Public ReadOnly Property getSimpleProdDir(ByVal dossierProd As service.DossierProd) As String
        Get
            Return Me.GetValueFromKey(dossierProd.ToString)
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
                '3 car c le minimum pour une combi C=V
                If Ligne.Length >= 3 Then
                    'si présence de = et que la ligne n'est pas un commentaire
                    If Ligne.Contains(_ATTRIBVAL) And Not Ligne(1).Equals(_COMMENTAIRE) Then
                        listeArgs.Add(Ligne.Split(_ATTRIBVAL)(0), Ligne.Split(_ATTRIBVAL)(1))
                    End If
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
