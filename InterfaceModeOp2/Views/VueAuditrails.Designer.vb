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
        Me.TSMI = New System.Windows.Forms.MenuStrip()
        Me.VisualisationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_ATPrinter = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_ATAutre = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMI_LISTEUSER = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutilsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_TOOLS_PRINTAB = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_TOOLS_PRINTSEL = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMI_TOOLS_VUEOLD = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_TOOLS_VOIREC = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_Historique = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_Signets = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_Verification = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_Verification_AVerif = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_Verification_EnCours = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_Verification_VerifOK = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit_NoUse = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMI_Selection_ChangementDroit_Guest = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit_User = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit_KeyUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit_UserAQ = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit_AdminAQ = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Selection_ChangementDroit_AdminDvlp = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lbl_Vue = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.DGV_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TSMI.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV_Main
        '
        Me.DGV_Main.AllowUserToOrderColumns = True
        Me.DGV_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Main.Location = New System.Drawing.Point(12, 27)
        Me.DGV_Main.Name = "DGV_Main"
        Me.DGV_Main.Size = New System.Drawing.Size(745, 335)
        Me.DGV_Main.TabIndex = 0
        '
        'TSMI
        '
        Me.TSMI.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VisualisationToolStripMenuItem, Me.OutilsToolStripMenuItem, Me.TSMI_Selection})
        Me.TSMI.Location = New System.Drawing.Point(0, 0)
        Me.TSMI.Name = "TSMI"
        Me.TSMI.Size = New System.Drawing.Size(769, 24)
        Me.TSMI.TabIndex = 2
        Me.TSMI.Text = "MenuStrip1"
        '
        'VisualisationToolStripMenuItem
        '
        Me.VisualisationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_ATPrinter, Me.TSMI_ATAutre, Me.ToolStripSeparator2, Me.TSMI_LISTEUSER})
        Me.VisualisationToolStripMenuItem.Name = "VisualisationToolStripMenuItem"
        Me.VisualisationToolStripMenuItem.Size = New System.Drawing.Size(85, 20)
        Me.VisualisationToolStripMenuItem.Text = "Visualisation"
        '
        'TSMI_ATPrinter
        '
        Me.TSMI_ATPrinter.Name = "TSMI_ATPrinter"
        Me.TSMI_ATPrinter.Size = New System.Drawing.Size(195, 22)
        Me.TSMI_ATPrinter.Text = "Audit Trails Impression"
        '
        'TSMI_ATAutre
        '
        Me.TSMI_ATAutre.Name = "TSMI_ATAutre"
        Me.TSMI_ATAutre.Size = New System.Drawing.Size(195, 22)
        Me.TSMI_ATAutre.Text = "Audit Trails Autres"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(192, 6)
        '
        'TSMI_LISTEUSER
        '
        Me.TSMI_LISTEUSER.Name = "TSMI_LISTEUSER"
        Me.TSMI_LISTEUSER.Size = New System.Drawing.Size(195, 22)
        Me.TSMI_LISTEUSER.Text = "Liste Utilisateurs"
        '
        'OutilsToolStripMenuItem
        '
        Me.OutilsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_TOOLS_PRINTAB, Me.TSMI_TOOLS_PRINTSEL, Me.ToolStripSeparator3, Me.TSMI_TOOLS_VUEOLD, Me.TSMI_TOOLS_VOIREC})
        Me.OutilsToolStripMenuItem.Name = "OutilsToolStripMenuItem"
        Me.OutilsToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.OutilsToolStripMenuItem.Text = "Outils"
        '
        'TSMI_TOOLS_PRINTAB
        '
        Me.TSMI_TOOLS_PRINTAB.Name = "TSMI_TOOLS_PRINTAB"
        Me.TSMI_TOOLS_PRINTAB.Size = New System.Drawing.Size(185, 22)
        Me.TSMI_TOOLS_PRINTAB.Text = "Imprimer le tableau"
        '
        'TSMI_TOOLS_PRINTSEL
        '
        Me.TSMI_TOOLS_PRINTSEL.Name = "TSMI_TOOLS_PRINTSEL"
        Me.TSMI_TOOLS_PRINTSEL.Size = New System.Drawing.Size(185, 22)
        Me.TSMI_TOOLS_PRINTSEL.Text = "Imprimer la sélection"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(182, 6)
        '
        'TSMI_TOOLS_VUEOLD
        '
        Me.TSMI_TOOLS_VUEOLD.Name = "TSMI_TOOLS_VUEOLD"
        Me.TSMI_TOOLS_VUEOLD.Size = New System.Drawing.Size(185, 22)
        Me.TSMI_TOOLS_VUEOLD.Text = "Vue précédente"
        '
        'TSMI_TOOLS_VOIREC
        '
        Me.TSMI_TOOLS_VOIREC.Name = "TSMI_TOOLS_VOIREC"
        Me.TSMI_TOOLS_VOIREC.Size = New System.Drawing.Size(185, 22)
        Me.TSMI_TOOLS_VOIREC.Text = "Voir AT Vérifié"
        '
        'TSMI_Selection
        '
        Me.TSMI_Selection.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Selection_Historique, Me.TSMI_Selection_Signets, Me.TSMI_Selection_Verification, Me.TSMI_Selection_ChangementDroit})
        Me.TSMI_Selection.Name = "TSMI_Selection"
        Me.TSMI_Selection.Size = New System.Drawing.Size(67, 20)
        Me.TSMI_Selection.Text = "Sélection"
        '
        'TSMI_Selection_Historique
        '
        Me.TSMI_Selection_Historique.Name = "TSMI_Selection_Historique"
        Me.TSMI_Selection_Historique.Size = New System.Drawing.Size(240, 22)
        Me.TSMI_Selection_Historique.Text = "Voir Historique"
        '
        'TSMI_Selection_Signets
        '
        Me.TSMI_Selection_Signets.Name = "TSMI_Selection_Signets"
        Me.TSMI_Selection_Signets.Size = New System.Drawing.Size(240, 22)
        Me.TSMI_Selection_Signets.Text = "Voir Signets"
        '
        'TSMI_Selection_Verification
        '
        Me.TSMI_Selection_Verification.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Selection_Verification_AVerif, Me.TSMI_Selection_Verification_EnCours, Me.TSMI_Selection_Verification_VerifOK})
        Me.TSMI_Selection_Verification.Name = "TSMI_Selection_Verification"
        Me.TSMI_Selection_Verification.Size = New System.Drawing.Size(240, 22)
        Me.TSMI_Selection_Verification.Text = "Changement Statut Vérification"
        '
        'TSMI_Selection_Verification_AVerif
        '
        Me.TSMI_Selection_Verification_AVerif.Name = "TSMI_Selection_Verification_AVerif"
        Me.TSMI_Selection_Verification_AVerif.Size = New System.Drawing.Size(150, 22)
        Me.TSMI_Selection_Verification_AVerif.Text = "A vérifier"
        '
        'TSMI_Selection_Verification_EnCours
        '
        Me.TSMI_Selection_Verification_EnCours.Name = "TSMI_Selection_Verification_EnCours"
        Me.TSMI_Selection_Verification_EnCours.Size = New System.Drawing.Size(150, 22)
        Me.TSMI_Selection_Verification_EnCours.Text = "En cours"
        '
        'TSMI_Selection_Verification_VerifOK
        '
        Me.TSMI_Selection_Verification_VerifOK.Name = "TSMI_Selection_Verification_VerifOK"
        Me.TSMI_Selection_Verification_VerifOK.Size = New System.Drawing.Size(150, 22)
        Me.TSMI_Selection_Verification_VerifOK.Text = "Vérification ok"
        '
        'TSMI_Selection_ChangementDroit
        '
        Me.TSMI_Selection_ChangementDroit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Selection_ChangementDroit_NoUse, Me.ToolStripSeparator1, Me.TSMI_Selection_ChangementDroit_Guest, Me.TSMI_Selection_ChangementDroit_User, Me.TSMI_Selection_ChangementDroit_KeyUser, Me.TSMI_Selection_ChangementDroit_UserAQ, Me.TSMI_Selection_ChangementDroit_AdminAQ, Me.TSMI_Selection_ChangementDroit_AdminDvlp})
        Me.TSMI_Selection_ChangementDroit.Name = "TSMI_Selection_ChangementDroit"
        Me.TSMI_Selection_ChangementDroit.Size = New System.Drawing.Size(240, 22)
        Me.TSMI_Selection_ChangementDroit.Text = "Changement Droits Utilisateur"
        '
        'TSMI_Selection_ChangementDroit_NoUse
        '
        Me.TSMI_Selection_ChangementDroit_NoUse.Name = "TSMI_Selection_ChangementDroit_NoUse"
        Me.TSMI_Selection_ChangementDroit_NoUse.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_NoUse.Text = "Bloquer"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(131, 6)
        '
        'TSMI_Selection_ChangementDroit_Guest
        '
        Me.TSMI_Selection_ChangementDroit_Guest.Name = "TSMI_Selection_ChangementDroit_Guest"
        Me.TSMI_Selection_ChangementDroit_Guest.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_Guest.Text = "Guest"
        '
        'TSMI_Selection_ChangementDroit_User
        '
        Me.TSMI_Selection_ChangementDroit_User.Name = "TSMI_Selection_ChangementDroit_User"
        Me.TSMI_Selection_ChangementDroit_User.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_User.Text = "User"
        '
        'TSMI_Selection_ChangementDroit_KeyUser
        '
        Me.TSMI_Selection_ChangementDroit_KeyUser.Name = "TSMI_Selection_ChangementDroit_KeyUser"
        Me.TSMI_Selection_ChangementDroit_KeyUser.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_KeyUser.Text = "KeyUser"
        '
        'TSMI_Selection_ChangementDroit_UserAQ
        '
        Me.TSMI_Selection_ChangementDroit_UserAQ.Name = "TSMI_Selection_ChangementDroit_UserAQ"
        Me.TSMI_Selection_ChangementDroit_UserAQ.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_UserAQ.Text = "UserAQ"
        '
        'TSMI_Selection_ChangementDroit_AdminAQ
        '
        Me.TSMI_Selection_ChangementDroit_AdminAQ.Name = "TSMI_Selection_ChangementDroit_AdminAQ"
        Me.TSMI_Selection_ChangementDroit_AdminAQ.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_AdminAQ.Text = "AdminAQ"
        '
        'TSMI_Selection_ChangementDroit_AdminDvlp
        '
        Me.TSMI_Selection_ChangementDroit_AdminDvlp.Name = "TSMI_Selection_ChangementDroit_AdminDvlp"
        Me.TSMI_Selection_ChangementDroit_AdminDvlp.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Selection_ChangementDroit_AdminDvlp.Text = "AdminDvlp"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_Vue})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 365)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(769, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
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
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.DGV_Main)
        Me.Controls.Add(Me.TSMI)
        Me.MainMenuStrip = Me.TSMI
        Me.MinimumSize = New System.Drawing.Size(785, 425)
        Me.Name = "VueAuditrails"
        Me.Text = "VueAuditrails"
        CType(Me.DGV_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TSMI.ResumeLayout(False)
        Me.TSMI.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV_Main As System.Windows.Forms.DataGridView
    Friend WithEvents TSMI As System.Windows.Forms.MenuStrip
    Friend WithEvents VisualisationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_ATPrinter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents OutilsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_TOOLS_PRINTAB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_Vue As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSMI_Selection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_LISTEUSER As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_ATAutre As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSMI_Selection_Historique As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_Signets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_Verification As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_Verification_AVerif As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_Verification_EnCours As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_Verification_VerifOK As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit_NoUse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSMI_Selection_ChangementDroit_Guest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit_User As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit_KeyUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit_UserAQ As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit_AdminAQ As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Selection_ChangementDroit_AdminDvlp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_TOOLS_PRINTSEL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSMI_TOOLS_VUEOLD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_TOOLS_VOIREC As System.Windows.Forms.ToolStripMenuItem
End Class
