<Assembly: Reflection.AssemblyVersion("2.0.0.0")> 

Public Class Initialisation

    Sub New()

        InitializeComponent()

        LBL_DossierBase.Text = "\" & "test"


    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FindFolderAccess()

    End Sub

#Region "Evènements sur boutons"
    ''' <summary>
    ''' Ouverture de la page principale de l'interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BT_Open_Click() Handles BT_Open.Click
        controleur.gotoView(service.View.Principale)
    End Sub
#End Region


End Class
