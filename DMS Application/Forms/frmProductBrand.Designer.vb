<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductBrand
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
        Me.myFormControler = New DMS_Application.ctlFormControler()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'myFormControler
        '
        Me.myFormControler.FormIsLoading = False
        Me.myFormControler.FormMode = DMS_Application.clsConstants.Form_Modes.CONSULT_MODE
        Me.myFormControler.Item_ID = 0
        Me.myFormControler.Location = New System.Drawing.Point(0, 31)
        Me.myFormControler.Name = "myFormControler"
        Me.myFormControler.ShowButtonQuitOnly = False
        Me.myFormControler.Size = New System.Drawing.Size(437, 33)
        Me.myFormControler.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(5, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 16)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Nom:"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(58, 5)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(374, 20)
        Me.txtName.TabIndex = 1
        '
        'frmProductBrand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 62)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.myFormControler)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProductBrand"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Marque"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents myFormControler As DMS_Application.ctlFormControler
End Class
