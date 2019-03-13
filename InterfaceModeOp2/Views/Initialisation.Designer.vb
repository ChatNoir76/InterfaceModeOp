<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Initialisation
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
        Me.BT_Open = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TXT_Username = New System.Windows.Forms.TextBox()
        Me.TXT_PC = New System.Windows.Forms.TextBox()
        Me.TXT_RepBase = New System.Windows.Forms.TextBox()
        Me.TXT_Droit = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TXT_InfoINI = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BT_Open
        '
        Me.BT_Open.Location = New System.Drawing.Point(331, 6)
        Me.BT_Open.Name = "BT_Open"
        Me.BT_Open.Size = New System.Drawing.Size(109, 91)
        Me.BT_Open.TabIndex = 2
        Me.BT_Open.Text = "Ouvrir l'interface ModeOp"
        Me.BT_Open.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(128, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "User"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(101, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Ordinateur"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(60, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Répertoire de base"
        '
        'TXT_Username
        '
        Me.TXT_Username.Enabled = False
        Me.TXT_Username.Location = New System.Drawing.Point(163, 6)
        Me.TXT_Username.Name = "TXT_Username"
        Me.TXT_Username.Size = New System.Drawing.Size(159, 20)
        Me.TXT_Username.TabIndex = 28
        '
        'TXT_PC
        '
        Me.TXT_PC.Enabled = False
        Me.TXT_PC.Location = New System.Drawing.Point(163, 32)
        Me.TXT_PC.Name = "TXT_PC"
        Me.TXT_PC.Size = New System.Drawing.Size(159, 20)
        Me.TXT_PC.TabIndex = 29
        '
        'TXT_RepBase
        '
        Me.TXT_RepBase.Enabled = False
        Me.TXT_RepBase.Location = New System.Drawing.Point(163, 58)
        Me.TXT_RepBase.Name = "TXT_RepBase"
        Me.TXT_RepBase.Size = New System.Drawing.Size(159, 20)
        Me.TXT_RepBase.TabIndex = 30
        '
        'TXT_Droit
        '
        Me.TXT_Droit.Enabled = False
        Me.TXT_Droit.Location = New System.Drawing.Point(163, 84)
        Me.TXT_Droit.Name = "TXT_Droit"
        Me.TXT_Droit.Size = New System.Drawing.Size(159, 20)
        Me.TXT_Droit.TabIndex = 31
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(83, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 13)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "Droit Interface"
        '
        'TXT_InfoINI
        '
        Me.TXT_InfoINI.Enabled = False
        Me.TXT_InfoINI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_InfoINI.Location = New System.Drawing.Point(6, 19)
        Me.TXT_InfoINI.Multiline = True
        Me.TXT_InfoINI.Name = "TXT_InfoINI"
        Me.TXT_InfoINI.Size = New System.Drawing.Size(416, 253)
        Me.TXT_InfoINI.TabIndex = 33
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TXT_InfoINI)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 110)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(428, 278)
        Me.GroupBox1.TabIndex = 34
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Information d'initialisation"
        '
        'Initialisation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 400)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TXT_Droit)
        Me.Controls.Add(Me.TXT_RepBase)
        Me.Controls.Add(Me.TXT_PC)
        Me.Controls.Add(Me.TXT_Username)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BT_Open)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Initialisation"
        Me.Text = "Page d'initialisation avant ouverture"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BT_Open As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TXT_Username As System.Windows.Forms.TextBox
    Friend WithEvents TXT_PC As System.Windows.Forms.TextBox
    Friend WithEvents TXT_RepBase As System.Windows.Forms.TextBox
    Friend WithEvents TXT_Droit As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TXT_InfoINI As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
