<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlGridTextBox
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.txtTexte = New System.Windows.Forms.TextBox()
        Me.btnShowGrid = New System.Windows.Forms.Button()
        Me.myToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'txtTexte
        '
        Me.txtTexte.Location = New System.Drawing.Point(0, 0)
        Me.txtTexte.Name = "txtTexte"
        Me.txtTexte.Size = New System.Drawing.Size(116, 20)
        Me.txtTexte.TabIndex = 1
        '
        'btnShowGrid
        '
        Me.btnShowGrid.BackgroundImage = Global.DMS_Application.My.Resources.Resources.MagnifyGlass
        Me.btnShowGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnShowGrid.Location = New System.Drawing.Point(116, -1)
        Me.btnShowGrid.Name = "btnShowGrid"
        Me.btnShowGrid.Size = New System.Drawing.Size(25, 22)
        Me.btnShowGrid.TabIndex = 2
        Me.btnShowGrid.UseVisualStyleBackColor = True
        '
        'ctlGridTextBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnShowGrid)
        Me.Controls.Add(Me.txtTexte)
        Me.Name = "ctlGridTextBox"
        Me.Size = New System.Drawing.Size(144, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTexte As System.Windows.Forms.TextBox
    Friend WithEvents btnShowGrid As System.Windows.Forms.Button
    Friend WithEvents myToolTip As System.Windows.Forms.ToolTip

End Class
