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

    'Liste des phrases d'audit trails
    Private _maListePAT As New List(Of String)

    'mot clef list
    Private Const _PAT = "PAT"

    'Caractère spéciaux du fichier ini
    Private Const _COMMENTAIRE As String = ";"
    Private Const _REGION As String = "["
    Private Const _ATTRIBVAL As String = "="

    ''' <summary>
    ''' Répertoire de l'application en cours
    ''' </summary>
    Public ReadOnly Property getBaseDir() As String
        Get
            Return Directory.GetCurrentDirectory
        End Get
    End Property
    ''' <summary>
    ''' Répertoire de l'application en cours avec répertoire de base
    ''' </summary>
    Public ReadOnly Property getWorkDir() As String
        Get
            Dim WorkDir As New System.Text.StringBuilder(Directory.GetCurrentDirectory)
            With WorkDir
                .Append("\")
                .Append(Me.GetValueFromKey(Outils.INI_KEY_REPBASE))
            End With
            Return WorkDir.ToString
        End Get
    End Property
    Public ReadOnly Property getFullProdDir(ByVal dossierProd As Outils.DossierProd) As String
        Get
            Dim FullDir As New System.Text.StringBuilder(Directory.GetCurrentDirectory)
            With FullDir
                .Append("\")
                .Append(Me.GetValueFromKey(Outils.INI_KEY_REPBASE))
                .Append("\")
                .Append(Me.GetValueFromKey(dossierProd.ToString))
            End With
            Return FullDir.ToString
        End Get
    End Property
    Public ReadOnly Property getSimpleProdDir(ByVal dossierProd As Outils.DossierProd) As String
        Get
            Return Me.GetValueFromKey(dossierProd.ToString)
        End Get
    End Property
    Public ReadOnly Property getListePhraseAT() As List(Of String)
        Get
            Return _maListePAT
        End Get
    End Property

    Private Sub New()
        'récupère le nom du fichier décrit dans les constantes
        _MonFichier = Outils.NOM_FICHIER_INI
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
                        Dim maClef As String = Ligne.Split(_ATTRIBVAL)(0)
                        Dim maValeur As String = Ligne.Split(_ATTRIBVAL)(1)
                        Try
                            'si la clef est égale à PAT
                            If maClef = _PAT Then
                                If maValeur <> String.Empty Then
                                    _maListePAT.Add(maValeur)
                                End If
                            Else
                                'si la clef n'existe pas déjà dans le hashtable
                                If Not listeArgs.ContainsKey(maClef) Then
                                    listeArgs.Add(maClef, maValeur)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show("Une donnée dans le fichier de configuration (*.ini) est corrompue", "Erreur combinaison mot clef - valeur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End Try
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
