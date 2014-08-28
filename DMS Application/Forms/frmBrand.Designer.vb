<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBrand
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
        Me.CtlFormControler1 = New DMS_Application.ctlFormControler()
        Me.SuspendLayout()
        '
        'CtlFormControler1
        '
        Me.CtlFormControler1.Location = New System.Drawing.Point(56, 312)
        Me.CtlFormControler1.Name = "CtlFormControler1"
        Me.CtlFormControler1.ShowButtonQuitOnly = False
        Me.CtlFormControler1.Size = New System.Drawing.Size(324, 33)
        Me.CtlFormControler1.TabIndex = 0
        '
        'frmBrand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 357)
        Me.Controls.Add(Me.CtlFormControler1)
        Me.Name = "frmBrand"
        Me.Text = "frmBrand"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtlFormControler1 As DMS_Application.ctlFormControler
End Class
