''' <summary>
''' Contient toutes les constantes et les énumérations
''' </summary>
''' <remarks></remarks>
Public Class service
    ''' <summary>
    ''' Détermine la vue à afficher
    ''' </summary>
    ''' <remarks></remarks>
    Enum View As Integer
        Principale = 0
        autre = 1
    End Enum

    ''' <summary>
    ''' Niveau d'access aux dossiers
    ''' </summary>
    ''' <remarks></remarks>
    Enum AccessFolder As Integer
        None = 0
        Lecture = 1
        Ecriture = 2
    End Enum

    ''' <summary>
    ''' Clef de cryptage des fichiers word
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared CLEF_CRYPTAGE As String = "f4ty52uG"

    ''' <summary>
    ''' Liste des mots de passe dans les fichiers Word Chimie avant importation dans l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared LISTE_MDP_PRODUCTION As String = "production|productions|dupli|produit|duplis|Production|Productions|Dupli|Produit|Duplis|Produits|PRODUCTION|DUPLI|PRODUIT"

    ''' <summary>
    ''' extentions pour les fichiers cryptés
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared EXT_FICHIER_CRYPTER As String = "Fichier Crypte (*.crp)| *.crp"

    ''' <summary>
    ''' extentions pour les fichiers word
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared EXT_FICHIER_WORD As String = "Office Documents |*.doc;*.docx;*.dotx;*.dotm;*.dot"

    ''' <summary>
    ''' Nom du fichier de configuration attendu pour initialiser l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared NOM_FICHIER_INI As String = "InterfaceModeOp2"
End Class
