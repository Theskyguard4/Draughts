<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.AiButt = New System.Windows.Forms.Button()
        Me.CountLabel = New System.Windows.Forms.Label()
        Me.AIVSAIBUtt = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'AiButt
        '
        Me.AiButt.Location = New System.Drawing.Point(839, 30)
        Me.AiButt.Name = "AiButt"
        Me.AiButt.Size = New System.Drawing.Size(100, 100)
        Me.AiButt.TabIndex = 0
        Me.AiButt.Text = "Vs Computer"
        Me.AiButt.UseVisualStyleBackColor = True
        '
        'CountLabel
        '
        Me.CountLabel.AutoSize = True
        Me.CountLabel.Location = New System.Drawing.Point(839, 137)
        Me.CountLabel.Name = "CountLabel"
        Me.CountLabel.Size = New System.Drawing.Size(0, 17)
        Me.CountLabel.TabIndex = 1
        '
        'AIVSAIBUtt
        '
        Me.AIVSAIBUtt.Location = New System.Drawing.Point(839, 691)
        Me.AIVSAIBUtt.Name = "AIVSAIBUtt"
        Me.AIVSAIBUtt.Size = New System.Drawing.Size(100, 100)
        Me.AIVSAIBUtt.TabIndex = 2
        Me.AIVSAIBUtt.Text = "Computer " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Vs" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Computer"
        Me.AIVSAIBUtt.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 803)
        Me.Controls.Add(Me.AIVSAIBUtt)
        Me.Controls.Add(Me.CountLabel)
        Me.Controls.Add(Me.AiButt)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AiButt As System.Windows.Forms.Button
    Friend WithEvents CountLabel As System.Windows.Forms.Label
    Friend WithEvents AIVSAIBUtt As System.Windows.Forms.Button

End Class
