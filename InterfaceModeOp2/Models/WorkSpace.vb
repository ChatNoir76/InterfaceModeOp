Imports System.IO

Public Class WorkSpace

    Private _RepertoireBase As String 'répertoire d'exécution
    Private _RepertoireTravail As String 'répertoire ou se trouve les dossier de A à F

    Private _NomDossierA As String
    Private _NomDossierB As String 'sauvegarde
    Private _NomDossierC As String 'modification
    Private _NomDossierD As String
    Private _NomDossierE As String 'officiel
    Private _NomDossierF As String 'archive

    Sub New(ByVal repertoireTravail As String)
        _RepertoireBase = Directory.GetCurrentDirectory
        _RepertoireTravail = repertoireTravail


    End Sub


End Class
