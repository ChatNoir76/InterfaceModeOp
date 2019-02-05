<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Impression
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NUD_1 = New System.Windows.Forms.NumericUpDown()
        Me.NUD_2 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TXT_Signets = New System.Windows.Forms.TextBox()
        Me.CB_AT = New System.Windows.Forms.ComboBox()
        CType(Me.NUD_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUD_2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TXT_AT.Location = New System.Drawing.Point(27, 301)
        Me.TXT_AT.Multiline = True
        Me.TXT_AT.Name = "TXT_AT"
        Me.TXT_AT.Size = New System.Drawing.Size(316, 107)
        Me.TXT_AT.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(349, 385)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(248, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Nombre de pages"
        '
        'NUD_1
        '
        Me.NUD_1.Location = New System.Drawing.Point(251, 40)
        Me.NUD_1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_1.Name = "NUD_1"
        Me.NUD_1.Size = New System.Drawing.Size(38, 20)
        Me.NUD_1.TabIndex = 9
        Me.NUD_1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NUD_2
        '
        Me.NUD_2.Location = New System.Drawing.Point(342, 40)
        Me.NUD_2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_2.Name = "NUD_2"
        Me.NUD_2.Size = New System.Drawing.Size(38, 20)
        Me.NUD_2.TabIndex = 10
        Me.NUD_2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(308, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "A"
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
        Me.TXT_Signets.Size = New System.Drawing.Size(353, 134)
        Me.TXT_Signets.TabIndex = 13
        '
        'CB_AT
        '
        Me.CB_AT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_AT.FormattingEnabled = True
        Me.CB_AT.Location = New System.Drawing.Point(27, 274)
        Me.CB_AT.Name = "CB_AT"
        Me.CB_AT.Size = New System.Drawing.Size(353, 21)
        Me.CB_AT.TabIndex = 14
        '
        'Impression
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 421)
        Me.Controls.Add(Me.CB_AT)
        Me.Controls.Add(Me.TXT_Signets)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.NUD_2)
        Me.Controls.Add(Me.NUD_1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TXT_AT)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_ListeImp)
        Me.Name = "Impression"
        Me.Text = "Impression"
        CType(Me.NUD_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUD_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CB_ListeImp As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXT_AT As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NUD_1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUD_2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TXT_Signets As System.Windows.Forms.TextBox
    Friend WithEvents CB_AT As System.Windows.Forms.ComboBox
End Class
