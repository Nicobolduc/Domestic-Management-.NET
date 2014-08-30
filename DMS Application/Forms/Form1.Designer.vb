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
        Me.CtlFormControler1 = New DMS_Application.ctlFormControler()
        Me.SuspendLayout()
        '
        'CtlFormControler1
        '
        Me.CtlFormControler1.FormIsLoading = False
        Me.CtlFormControler1.FormMode = DMS_Application.clsConstants.Form_Modes.LOADED
        Me.CtlFormControler1.Item_ID = 0
        Me.CtlFormControler1.Location = New System.Drawing.Point(82, 303)
        Me.CtlFormControler1.Name = "CtlFormControler1"
        Me.CtlFormControler1.ShowButtonQuitOnly = True
        Me.CtlFormControler1.Size = New System.Drawing.Size(85, 33)
        Me.CtlFormControler1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 375)
        Me.Controls.Add(Me.CtlFormControler1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtlFormControler1 As DMS_Application.ctlFormControler
End Class
