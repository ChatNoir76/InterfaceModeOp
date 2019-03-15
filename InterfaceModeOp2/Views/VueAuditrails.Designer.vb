<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VueAuditrails
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DGV_Main = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.VisualisationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EffacerLeTableauToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImpressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutilsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImprimerLeTableauToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lbl_Vue = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.DGV_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV_Main
        '
        Me.DGV_Main.AllowUserToOrderColumns = True
        Me.DGV_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Main.Location = New System.Drawing.Point(12, 63)
        Me.DGV_Main.Name = "DGV_Main"
        Me.DGV_Main.Size = New System.Drawing.Size(745, 169)
        Me.DGV_Main.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VisualisationToolStripMenuItem, Me.OutilsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(769, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'VisualisationToolStripMenuItem
        '
        Me.VisualisationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EffacerLeTableauToolStripMenuItem, Me.ToolStripSeparator1, Me.ImpressionToolStripMenuItem})
        Me.VisualisationToolStripMenuItem.Name = "VisualisationToolStripMenuItem"
        Me.VisualisationToolStripMenuItem.Size = New System.Drawing.Size(85, 20)
        Me.VisualisationToolStripMenuItem.Text = "Visualisation"
        '
        'EffacerLeTableauToolStripMenuItem
        '
        Me.EffacerLeTableauToolStripMenuItem.Name = "EffacerLeTableauToolStripMenuItem"
        Me.EffacerLeTableauToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.EffacerLeTableauToolStripMenuItem.Text = "Effacer le tableau"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(161, 6)
        '
        'ImpressionToolStripMenuItem
        '
        Me.ImpressionToolStripMenuItem.Name = "ImpressionToolStripMenuItem"
        Me.ImpressionToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ImpressionToolStripMenuItem.Text = "Impression"
        '
        'OutilsToolStripMenuItem
        '
        Me.OutilsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImprimerLeTableauToolStripMenuItem})
        Me.OutilsToolStripMenuItem.Name = "OutilsToolStripMenuItem"
        Me.OutilsToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.OutilsToolStripMenuItem.Text = "Outils"
        '
        'ImprimerLeTableauToolStripMenuItem
        '
        Me.ImprimerLeTableauToolStripMenuItem.Name = "ImprimerLeTableauToolStripMenuItem"
        Me.ImprimerLeTableauToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ImprimerLeTableauToolStripMenuItem.Text = "Imprimer le tableau"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_Vue})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 365)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(769, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_Vue
        '
        Me.lbl_Vue.Name = "lbl_Vue"
        Me.lbl_Vue.Size = New System.Drawing.Size(63, 17)
        Me.lbl_Vue.Text = "Pas de vue"
        '
        'VueAuditrails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 387)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.DGV_Main)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(785, 425)
        Me.Name = "VueAuditrails"
        Me.Text = "VueAuditrails"
        CType(Me.DGV_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV_Main As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents VisualisationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImpressionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents OutilsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImprimerLeTableauToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EffacerLeTableauToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_Vue As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
