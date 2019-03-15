Imports ValdepharmTool.GGridView

Public Class VueAuditrails

#Region "Constructeur"
    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        DGV_Main.DataSource = DAOFactory.getUtilisateur.dbGetAll

    End Sub
#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim test As New PrintDataGridView(DGV_Main)
        test.Impression()
    End Sub
End Class