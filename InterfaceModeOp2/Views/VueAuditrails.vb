Imports ValdepharmTool.GGridView

Public Class VueAuditrails

    Private _DGVMainSize As Size
    Private _canResize = False

#Region "Constructeur"
    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()


        With DGV_Main
            _DGVMainSize = New Size(Me.Width - .Width, Me.Height - .Height)
            _canResize = True

            changerVue(DAOViews.views.Rien)

            'l'utilisateur ne peux pas supprimer/ajouter de données
            .AllowUserToDeleteRows = False
            .AllowUserToAddRows = False

            For i = 1 To .ColumnCount
                .Columns(i - 1).ReadOnly = True
            Next

        End With

    End Sub
#End Region

    Private Sub VueAuditrails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If _canResize Then
            'Gestion du DatagridView1
            With DGV_Main
                .Size = New Size(Me.Width - _DGVMainSize.Width, Me.Height - _DGVMainSize.Height)
            End With
        End If
    End Sub

    Private Sub changerVue(ByVal maVue As DAOViews.views)
        DGV_Main.DataSource = DAOFactory.getViews.getAsDataTable(maVue)
        Me.lbl_Vue.Text = "Visualisation de la vue : " & DGV_Main.DataSource.ToString
    End Sub

    Private Sub ImpressionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImpressionToolStripMenuItem.Click
        changerVue(DAOViews.views.ATPrinter)
    End Sub

    Private Sub ImprimerLeTableauToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimerLeTableauToolStripMenuItem.Click
        Dim test As New PrintDataGridView(DGV_Main)
        test.Impression()
    End Sub

    Private Sub EffacerLeTableauToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EffacerLeTableauToolStripMenuItem.Click
        changerVue(DAOViews.views.Rien)
    End Sub

    Private Sub DGV_Main_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_Main.CellContentClick

    End Sub

    Private Sub DGV_Main_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_Main.CellDoubleClick
        MsgBox("double click")
    End Sub

    Private Sub DGV_Main_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGV_Main.CellMouseDown

        If e.Button = MouseButtons.Right Then
            Dim m As New ContextMenuStrip
            m.Items.Add("coucou")
            m.Show(New Point(sender.bounds.Width, sender.bounds.height))
        End If
    End Sub
End Class