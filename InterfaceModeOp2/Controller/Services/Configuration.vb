Imports System.IO

''' <summary>
''' Lecture et enregistrement des variables du fichier ini
''' </summary>
''' <remarks></remarks>
Public Class Configuration

    'instance du singleton
    Private Shared instance As Configuration = Nothing

    'Mots clef du fichier ini
    Private _CONFIGURATION As String = "[Configuration]"
    Private _DROITIT As String = "[DroitIT]"

    Private _MonFichier As String

    Private Sub New(ByVal CheminFichierINI As String)
        _MonFichier = CheminFichierINI
    End Sub

    Public Shared Function getInstance(ByVal CheminFichierINI As String) As Configuration

        If instance Is Nothing Then
            instance = New Configuration(CheminFichierINI)
        End If

        Return instance

    End Function

    Public Function GetKey(ByVal Clef As String) As String
        If File.Exists(_MonFichier) Then
            For Each Ligne As String In File.ReadAllLines(_MonFichier)
                If Ligne.Split("=")(0) = Clef Then
                    If Ligne.Split("=")(1) = Nothing Then
                        Throw New Exception("Clef Fichier INI corrompue" & vbNewLine & Clef)
                    Else
                        Return Ligne.Split("=")(1)
                    End If
                End If
            Next
        End If

        Return Nothing
    End Function


End Class
