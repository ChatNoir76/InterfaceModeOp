''' <summary>
''' Contient toutes les constantes et les énumérations
''' Contient également toutes les méthodes statiques
''' </summary>
''' <remarks></remarks>
Public Class service

#Region "Enumération"

    ''' <summary>
    ''' Représente les droits utilisateurs
    ''' </summary>
    ''' <remarks></remarks>
    Enum DroitUser As Integer
        NoUse = -1
        Guest = 0
        User = 1
        KeyUser = 2
        UserAQ = 3
        AdminAQ = 4
        AdminDvlp = 5
    End Enum

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
        Probleme = -2
        Ignore = -1
        None = 0
        Lecture = 1
        Ecriture = 2
    End Enum

    ''' <summary>
    ''' Label des 6 dossiers Prod du fichier INI
    ''' </summary>
    ''' <remarks></remarks>
    Enum DossierProd As Integer
        DossierA = 1
        DossierB = 2
        DossierC = 3
        DossierD = 4
        DossierE = 5
        DossierF = 6
    End Enum

    Enum Action As Byte
        ConsultationOfficiel = 0
        ConsultationArchive = 1
        Impression = 2
        Importation = 3
        ExportationOfficiel = 4
        ExportationArchive = 5
        Archivage = 6
    End Enum
#End Region

#Region "Constantes"

    'Constante des entetes des mode op de production
    Public Const ET_DUPLICATA As String = "DUPLICATA"
    Public Const ET_ORIGINAL As String = "ORIGINAL"
    Public Const ET_PERIME As String = "PÉRIMÉ"

    'Constantes du fichier ini [configuration]
    Public Const INI_KEY_REPBASE As String = "BaseRep"
    Public Const INI_KEY_REPTMP As String = "Tmp"
    Public Const INI_IGNORE_CHAR As Char = "?"

    ''' <summary>
    ''' Clef de cryptage des fichiers word
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CLEF_CRYPTAGE As String = "f4ty52uG"

    ''' <summary>
    ''' Liste des mots de passe dans les fichiers Word Chimie avant importation dans l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LISTE_MDP_PRODUCTION As String = "production|productions|dupli|produit|duplis|Production|Productions|Dupli|Produit|Duplis|Produits|PRODUCTION|DUPLI|PRODUIT"

    ''' <summary>
    ''' extentions pour les fichiers cryptés
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXT_FICHIER_CRYPTER As String = "Fichier Crypte (*.crp)| *.crp"

    ''' <summary>
    ''' extentions pour les fichiers word
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXT_FICHIER_WORD As String = "Office Documents |*.doc;*.docx;*.dotx;*.dotm;*.dot"

    ''' <summary>
    ''' Nom du fichier de configuration attendu pour initialiser l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NOM_FICHIER_INI As String = "InterfaceModeOp2.ini"

    Public Const EXT_SIMPLE_CRP As String = ".crp"
#End Region

#Region "Méthodes statiques"

#End Region

End Class
