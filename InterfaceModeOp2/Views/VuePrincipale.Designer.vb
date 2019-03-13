<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class vuePrincipale
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
        Me.TSMI_Developpeur = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Developpeur_GestionDroitUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Developpeur_ConversionModeOp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TSMI_Administrateur = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Administrateur_Importation = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Administrateur_Importation_DepuisModif = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Administrateur_Exportation = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Administrateur_Exportation_Officiel = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Administrateur_Exportation_Archive = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Administrateur_Archivage = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Utilisateur = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Utilisateur_Consultation = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Utilisateur_Consultation_Officiel = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Utilisateur_Consultation_Archive = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Utilisateur_Impression = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_AuditTrails = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_AuditTrails_Impression = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_AuditTrails_Exportation = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_AuditTrails_Importation = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_AuditTrails_Paramètres = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_Parametre = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Outils_Parametre_DroitUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMI_Info = New System.Windows.Forms.ToolStripMenuItem()
        Me.GB_Main = New System.Windows.Forms.GroupBox()
        Me.TXT_Action = New System.Windows.Forms.TextBox()
        Me.TXT_Droits = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXT_LoginUtilisateur = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.GB_Main.SuspendLayout()
        Me.SuspendLayout()
        '
        'TSMI_Developpeur
        '
        Me.TSMI_Developpeur.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Developpeur_GestionDroitUser, Me.TSMI_Developpeur_ConversionModeOp})
        Me.TSMI_Developpeur.Name = "TSMI_Developpeur"
        Me.TSMI_Developpeur.Size = New System.Drawing.Size(86, 20)
        Me.TSMI_Developpeur.Text = "Développeur"
        '
        'TSMI_Developpeur_GestionDroitUser
        '
        Me.TSMI_Developpeur_GestionDroitUser.Name = "TSMI_Developpeur_GestionDroitUser"
        Me.TSMI_Developpeur_GestionDroitUser.Size = New System.Drawing.Size(209, 22)
        Me.TSMI_Developpeur_GestionDroitUser.Text = "Gestion Droits Utilisateurs"
        '
        'TSMI_Developpeur_ConversionModeOp
        '
        Me.TSMI_Developpeur_ConversionModeOp.Name = "TSMI_Developpeur_ConversionModeOp"
        Me.TSMI_Developpeur_ConversionModeOp.Size = New System.Drawing.Size(209, 22)
        Me.TSMI_Developpeur_ConversionModeOp.Text = "Conversion Mode Op"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Administrateur, Me.TSMI_Utilisateur, Me.TSMI_Outils, Me.TSMI_Developpeur, Me.TSMI_Info})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(390, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TSMI_Administrateur
        '
        Me.TSMI_Administrateur.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Administrateur_Importation, Me.TSMI_Administrateur_Exportation, Me.TSMI_Administrateur_Archivage})
        Me.TSMI_Administrateur.Name = "TSMI_Administrateur"
        Me.TSMI_Administrateur.Size = New System.Drawing.Size(98, 20)
        Me.TSMI_Administrateur.Text = "Administrateur"
        '
        'TSMI_Administrateur_Importation
        '
        Me.TSMI_Administrateur_Importation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Administrateur_Importation_DepuisModif})
        Me.TSMI_Administrateur_Importation.Name = "TSMI_Administrateur_Importation"
        Me.TSMI_Administrateur_Importation.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Administrateur_Importation.Text = "Importation"
        '
        'TSMI_Administrateur_Importation_DepuisModif
        '
        Me.TSMI_Administrateur_Importation_DepuisModif.Name = "TSMI_Administrateur_Importation_DepuisModif"
        Me.TSMI_Administrateur_Importation_DepuisModif.Size = New System.Drawing.Size(181, 22)
        Me.TSMI_Administrateur_Importation_DepuisModif.Text = "Depuis Modification"
        '
        'TSMI_Administrateur_Exportation
        '
        Me.TSMI_Administrateur_Exportation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Administrateur_Exportation_Officiel, Me.TSMI_Administrateur_Exportation_Archive})
        Me.TSMI_Administrateur_Exportation.Name = "TSMI_Administrateur_Exportation"
        Me.TSMI_Administrateur_Exportation.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Administrateur_Exportation.Text = "Exportation"
        '
        'TSMI_Administrateur_Exportation_Officiel
        '
        Me.TSMI_Administrateur_Exportation_Officiel.Name = "TSMI_Administrateur_Exportation_Officiel"
        Me.TSMI_Administrateur_Exportation_Officiel.Size = New System.Drawing.Size(166, 22)
        Me.TSMI_Administrateur_Exportation_Officiel.Text = "Depuis Officiel"
        '
        'TSMI_Administrateur_Exportation_Archive
        '
        Me.TSMI_Administrateur_Exportation_Archive.Name = "TSMI_Administrateur_Exportation_Archive"
        Me.TSMI_Administrateur_Exportation_Archive.Size = New System.Drawing.Size(166, 22)
        Me.TSMI_Administrateur_Exportation_Archive.Text = "Depuis Archivage"
        '
        'TSMI_Administrateur_Archivage
        '
        Me.TSMI_Administrateur_Archivage.Name = "TSMI_Administrateur_Archivage"
        Me.TSMI_Administrateur_Archivage.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Administrateur_Archivage.Text = "Archivage"
        '
        'TSMI_Utilisateur
        '
        Me.TSMI_Utilisateur.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Utilisateur_Consultation, Me.TSMI_Utilisateur_Impression})
        Me.TSMI_Utilisateur.Name = "TSMI_Utilisateur"
        Me.TSMI_Utilisateur.Size = New System.Drawing.Size(72, 20)
        Me.TSMI_Utilisateur.Text = "Utilisateur"
        '
        'TSMI_Utilisateur_Consultation
        '
        Me.TSMI_Utilisateur_Consultation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Utilisateur_Consultation_Officiel, Me.TSMI_Utilisateur_Consultation_Archive})
        Me.TSMI_Utilisateur_Consultation.Name = "TSMI_Utilisateur_Consultation"
        Me.TSMI_Utilisateur_Consultation.Size = New System.Drawing.Size(347, 22)
        Me.TSMI_Utilisateur_Consultation.Text = "Consultation Mode Op au format PDF"
        '
        'TSMI_Utilisateur_Consultation_Officiel
        '
        Me.TSMI_Utilisateur_Consultation_Officiel.Name = "TSMI_Utilisateur_Consultation_Officiel"
        Me.TSMI_Utilisateur_Consultation_Officiel.Size = New System.Drawing.Size(114, 22)
        Me.TSMI_Utilisateur_Consultation_Officiel.Text = "Officiel"
        '
        'TSMI_Utilisateur_Consultation_Archive
        '
        Me.TSMI_Utilisateur_Consultation_Archive.Name = "TSMI_Utilisateur_Consultation_Archive"
        Me.TSMI_Utilisateur_Consultation_Archive.Size = New System.Drawing.Size(114, 22)
        Me.TSMI_Utilisateur_Consultation_Archive.Text = "Archive"
        '
        'TSMI_Utilisateur_Impression
        '
        Me.TSMI_Utilisateur_Impression.Name = "TSMI_Utilisateur_Impression"
        Me.TSMI_Utilisateur_Impression.Size = New System.Drawing.Size(347, 22)
        Me.TSMI_Utilisateur_Impression.Text = "Impression Mode Op pour Utilisation En Production"
        '
        'TSMI_Outils
        '
        Me.TSMI_Outils.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Outils_AuditTrails, Me.TSMI_Outils_Parametre})
        Me.TSMI_Outils.Name = "TSMI_Outils"
        Me.TSMI_Outils.Size = New System.Drawing.Size(50, 20)
        Me.TSMI_Outils.Text = "Outils"
        '
        'TSMI_Outils_AuditTrails
        '
        Me.TSMI_Outils_AuditTrails.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Outils_AuditTrails_Impression, Me.TSMI_Outils_AuditTrails_Exportation, Me.TSMI_Outils_AuditTrails_Importation, Me.TSMI_Outils_AuditTrails_Paramètres})
        Me.TSMI_Outils_AuditTrails.Name = "TSMI_Outils_AuditTrails"
        Me.TSMI_Outils_AuditTrails.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Outils_AuditTrails.Text = "Audit Trails"
        '
        'TSMI_Outils_AuditTrails_Impression
        '
        Me.TSMI_Outils_AuditTrails_Impression.Name = "TSMI_Outils_AuditTrails_Impression"
        Me.TSMI_Outils_AuditTrails_Impression.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Outils_AuditTrails_Impression.Text = "Impression"
        '
        'TSMI_Outils_AuditTrails_Exportation
        '
        Me.TSMI_Outils_AuditTrails_Exportation.Name = "TSMI_Outils_AuditTrails_Exportation"
        Me.TSMI_Outils_AuditTrails_Exportation.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Outils_AuditTrails_Exportation.Text = "Exportation"
        '
        'TSMI_Outils_AuditTrails_Importation
        '
        Me.TSMI_Outils_AuditTrails_Importation.Name = "TSMI_Outils_AuditTrails_Importation"
        Me.TSMI_Outils_AuditTrails_Importation.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Outils_AuditTrails_Importation.Text = "Importation"
        '
        'TSMI_Outils_AuditTrails_Paramètres
        '
        Me.TSMI_Outils_AuditTrails_Paramètres.Name = "TSMI_Outils_AuditTrails_Paramètres"
        Me.TSMI_Outils_AuditTrails_Paramètres.Size = New System.Drawing.Size(137, 22)
        Me.TSMI_Outils_AuditTrails_Paramètres.Text = "Paramètres"
        '
        'TSMI_Outils_Parametre
        '
        Me.TSMI_Outils_Parametre.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI_Outils_Parametre_DroitUser})
        Me.TSMI_Outils_Parametre.Name = "TSMI_Outils_Parametre"
        Me.TSMI_Outils_Parametre.Size = New System.Drawing.Size(134, 22)
        Me.TSMI_Outils_Parametre.Text = "Paramètres"
        '
        'TSMI_Outils_Parametre_DroitUser
        '
        Me.TSMI_Outils_Parametre_DroitUser.Name = "TSMI_Outils_Parametre_DroitUser"
        Me.TSMI_Outils_Parametre_DroitUser.Size = New System.Drawing.Size(209, 22)
        Me.TSMI_Outils_Parametre_DroitUser.Text = "Gestion Droits Utilisateurs"
        '
        'TSMI_Info
        '
        Me.TSMI_Info.Name = "TSMI_Info"
        Me.TSMI_Info.Size = New System.Drawing.Size(24, 20)
        Me.TSMI_Info.Text = "?"
        '
        'GB_Main
        '
        Me.GB_Main.Controls.Add(Me.TXT_Action)
        Me.GB_Main.Location = New System.Drawing.Point(4, 143)
        Me.GB_Main.Name = "GB_Main"
        Me.GB_Main.Size = New System.Drawing.Size(376, 219)
        Me.GB_Main.TabIndex = 12
        Me.GB_Main.TabStop = False
        Me.GB_Main.Text = "Description Actions Utilisateur"
        '
        'TXT_Action
        '
        Me.TXT_Action.Location = New System.Drawing.Point(6, 19)
        Me.TXT_Action.Multiline = True
        Me.TXT_Action.Name = "TXT_Action"
        Me.TXT_Action.ReadOnly = True
        Me.TXT_Action.Size = New System.Drawing.Size(362, 194)
        Me.TXT_Action.TabIndex = 0
        Me.TXT_Action.TabStop = False
        '
        'TXT_Droits
        '
        Me.TXT_Droits.Enabled = False
        Me.TXT_Droits.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_Droits.Location = New System.Drawing.Point(174, 114)
        Me.TXT_Droits.Name = "TXT_Droits"
        Me.TXT_Droits.Size = New System.Drawing.Size(206, 23)
        Me.TXT_Droits.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(52, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Droits Interface : "
        '
        'TXT_LoginUtilisateur
        '
        Me.TXT_LoginUtilisateur.Enabled = False
        Me.TXT_LoginUtilisateur.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_LoginUtilisateur.Location = New System.Drawing.Point(174, 66)
        Me.TXT_LoginUtilisateur.Name = "TXT_LoginUtilisateur"
        Me.TXT_LoginUtilisateur.Size = New System.Drawing.Size(206, 23)
        Me.TXT_LoginUtilisateur.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Login Utilisateur : "
        '
        'vuePrincipale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(390, 366)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GB_Main)
        Me.Controls.Add(Me.TXT_Droits)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TXT_LoginUtilisateur)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(406, 404)
        Me.Name = "vuePrincipale"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Interface Mode Op v2"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GB_Main.ResumeLayout(False)
        Me.GB_Main.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TSMI_Developpeur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Developpeur_GestionDroitUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Developpeur_ConversionModeOp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TSMI_Administrateur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Administrateur_Importation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Administrateur_Importation_DepuisModif As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Administrateur_Exportation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Administrateur_Exportation_Officiel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Administrateur_Exportation_Archive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Administrateur_Archivage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Utilisateur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Utilisateur_Consultation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Utilisateur_Consultation_Officiel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Utilisateur_Consultation_Archive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Utilisateur_Impression As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils_AuditTrails As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils_AuditTrails_Impression As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils_AuditTrails_Exportation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils_AuditTrails_Importation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils_Parametre As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Outils_Parametre_DroitUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMI_Info As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GB_Main As System.Windows.Forms.GroupBox
    Friend WithEvents TXT_Action As System.Windows.Forms.TextBox
    Friend WithEvents TXT_Droits As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXT_LoginUtilisateur As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TSMI_Outils_AuditTrails_Paramètres As System.Windows.Forms.ToolStripMenuItem
End Class
