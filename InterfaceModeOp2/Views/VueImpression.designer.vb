<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VueImpression
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
        Me.CB_ListeImp = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXT_AT = New System.Windows.Forms.TextBox()
        Me.BT_Validation = New System.Windows.Forms.Button()
        Me.NUD_1 = New System.Windows.Forms.NumericUpDown()
        Me.NUD_2 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TXT_Signets = New System.Windows.Forms.TextBox()
        Me.CB_AT = New System.Windows.Forms.ComboBox()
        Me.GB_PrintPages = New System.Windows.Forms.GroupBox()
        Me.RB_OPT2 = New System.Windows.Forms.RadioButton()
        Me.RB_OPT1 = New System.Windows.Forms.RadioButton()
        Me.GB_OPT1 = New System.Windows.Forms.GroupBox()
        Me.GB_OPT2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TXT_PrintZone = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.NUD_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUD_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_PrintPages.SuspendLayout()
        Me.GB_OPT1.SuspendLayout()
        Me.GB_OPT2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CB_ListeImp
        '
        Me.CB_ListeImp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_ListeImp.FormattingEnabled = True
        Me.CB_ListeImp.Location = New System.Drawing.Point(34, 39)
        Me.CB_ListeImp.Name = "CB_ListeImp"
        Me.CB_ListeImp.Size = New System.Drawing.Size(194, 21)
        Me.CB_ListeImp.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Choix imprimante"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 258)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Justificatif pour audit trails"
        '
        'TXT_AT
        '
        Me.TXT_AT.Location = New System.Drawing.Point(27, 302)
        Me.TXT_AT.Multiline = True
        Me.TXT_AT.Name = "TXT_AT"
        Me.TXT_AT.Size = New System.Drawing.Size(493, 107)
        Me.TXT_AT.TabIndex = 6
        '
        'BT_Validation
        '
        Me.BT_Validation.Location = New System.Drawing.Point(543, 386)
        Me.BT_Validation.Name = "BT_Validation"
        Me.BT_Validation.Size = New System.Drawing.Size(31, 23)
        Me.BT_Validation.TabIndex = 7
        Me.BT_Validation.Text = "OK"
        Me.BT_Validation.UseVisualStyleBackColor = True
        '
        'NUD_1
        '
        Me.NUD_1.Location = New System.Drawing.Point(81, 15)
        Me.NUD_1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_1.Name = "NUD_1"
        Me.NUD_1.Size = New System.Drawing.Size(38, 20)
        Me.NUD_1.TabIndex = 9
        Me.NUD_1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NUD_2
        '
        Me.NUD_2.Location = New System.Drawing.Point(81, 48)
        Me.NUD_2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_2.Name = "NUD_2"
        Me.NUD_2.Size = New System.Drawing.Size(38, 20)
        Me.NUD_2.TabIndex = 10
        Me.NUD_2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "A la page"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(184, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Liste des signets rempli par l'utilisateur"
        '
        'TXT_Signets
        '
        Me.TXT_Signets.Location = New System.Drawing.Point(27, 103)
        Me.TXT_Signets.Multiline = True
        Me.TXT_Signets.Name = "TXT_Signets"
        Me.TXT_Signets.ReadOnly = True
        Me.TXT_Signets.Size = New System.Drawing.Size(547, 134)
        Me.TXT_Signets.TabIndex = 13
        '
        'CB_AT
        '
        Me.CB_AT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_AT.FormattingEnabled = True
        Me.CB_AT.Location = New System.Drawing.Point(27, 274)
        Me.CB_AT.Name = "CB_AT"
        Me.CB_AT.Size = New System.Drawing.Size(547, 21)
        Me.CB_AT.TabIndex = 14
        '
        'GB_PrintPages
        '
        Me.GB_PrintPages.Controls.Add(Me.RB_OPT2)
        Me.GB_PrintPages.Controls.Add(Me.RB_OPT1)
        Me.GB_PrintPages.Location = New System.Drawing.Point(245, 12)
        Me.GB_PrintPages.Name = "GB_PrintPages"
        Me.GB_PrintPages.Size = New System.Drawing.Size(122, 63)
        Me.GB_PrintPages.TabIndex = 15
        Me.GB_PrintPages.TabStop = False
        Me.GB_PrintPages.Text = "Nombre de pages"
        '
        'RB_OPT2
        '
        Me.RB_OPT2.AutoSize = True
        Me.RB_OPT2.Location = New System.Drawing.Point(8, 42)
        Me.RB_OPT2.Name = "RB_OPT2"
        Me.RB_OPT2.Size = New System.Drawing.Size(85, 17)
        Me.RB_OPT2.TabIndex = 1
        Me.RB_OPT2.TabStop = True
        Me.RB_OPT2.Text = "Personnalisé"
        Me.RB_OPT2.UseVisualStyleBackColor = True
        '
        'RB_OPT1
        '
        Me.RB_OPT1.AutoSize = True
        Me.RB_OPT1.Checked = True
        Me.RB_OPT1.Location = New System.Drawing.Point(8, 19)
        Me.RB_OPT1.Name = "RB_OPT1"
        Me.RB_OPT1.Size = New System.Drawing.Size(60, 17)
        Me.RB_OPT1.TabIndex = 0
        Me.RB_OPT1.TabStop = True
        Me.RB_OPT1.Text = "Interval"
        Me.RB_OPT1.UseVisualStyleBackColor = True
        '
        'GB_OPT1
        '
        Me.GB_OPT1.Controls.Add(Me.GB_OPT2)
        Me.GB_OPT1.Controls.Add(Me.NUD_1)
        Me.GB_OPT1.Controls.Add(Me.Label3)
        Me.GB_OPT1.Controls.Add(Me.NUD_2)
        Me.GB_OPT1.Controls.Add(Me.Label4)
        Me.GB_OPT1.Location = New System.Drawing.Point(386, 12)
        Me.GB_OPT1.Name = "GB_OPT1"
        Me.GB_OPT1.Size = New System.Drawing.Size(185, 85)
        Me.GB_OPT1.TabIndex = 16
        Me.GB_OPT1.TabStop = False
        Me.GB_OPT1.Text = "Impression"
        '
        'GB_OPT2
        '
        Me.GB_OPT2.Controls.Add(Me.Label6)
        Me.GB_OPT2.Controls.Add(Me.TXT_PrintZone)
        Me.GB_OPT2.Location = New System.Drawing.Point(0, 0)
        Me.GB_OPT2.Name = "GB_OPT2"
        Me.GB_OPT2.Size = New System.Drawing.Size(185, 85)
        Me.GB_OPT2.TabIndex = 17
        Me.GB_OPT2.TabStop = False
        Me.GB_OPT2.Text = "Impression"
        Me.GB_OPT2.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "ex : 1-6;10"
        '
        'TXT_PrintZone
        '
        Me.TXT_PrintZone.Location = New System.Drawing.Point(7, 49)
        Me.TXT_PrintZone.Name = "TXT_PrintZone"
        Me.TXT_PrintZone.Size = New System.Drawing.Size(172, 20)
        Me.TXT_PrintZone.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "De la page"
        '
        'VueImpression
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 421)
        Me.Controls.Add(Me.GB_OPT1)
        Me.Controls.Add(Me.GB_PrintPages)
        Me.Controls.Add(Me.CB_AT)
        Me.Controls.Add(Me.TXT_Signets)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.BT_Validation)
        Me.Controls.Add(Me.TXT_AT)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_ListeImp)
        Me.Name = "VueImpression"
        Me.Text = "Impression"
        CType(Me.NUD_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUD_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_PrintPages.ResumeLayout(False)
        Me.GB_PrintPages.PerformLayout()
        Me.GB_OPT1.ResumeLayout(False)
        Me.GB_OPT1.PerformLayout()
        Me.GB_OPT2.ResumeLayout(False)
        Me.GB_OPT2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CB_ListeImp As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXT_AT As System.Windows.Forms.TextBox
    Friend WithEvents BT_Validation As System.Windows.Forms.Button
    Friend WithEvents NUD_1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUD_2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TXT_Signets As System.Windows.Forms.TextBox
    Friend WithEvents CB_AT As System.Windows.Forms.ComboBox
    Friend WithEvents GB_PrintPages As System.Windows.Forms.GroupBox
    Friend WithEvents RB_OPT2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_OPT1 As System.Windows.Forms.RadioButton
    Friend WithEvents GB_OPT1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GB_OPT2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TXT_PrintZone As System.Windows.Forms.TextBox
End Class
