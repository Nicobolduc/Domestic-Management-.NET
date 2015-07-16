<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExpenseType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpenseType))
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.formController = New DMS_Application.ctlFormController()
        Me.ColorDialog = New System.Windows.Forms.ColorDialog()
        Me.btnColor = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(51, 12)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(273, 20)
        Me.txtName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Nom:"
        '
        'formController
        '
        Me.formController.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(0, 40)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(408, 33)
        Me.formController.TabIndex = 19
        '
        'btnColor
        '
        Me.btnColor.Location = New System.Drawing.Point(330, 10)
        Me.btnColor.Name = "btnColor"
        Me.btnColor.Size = New System.Drawing.Size(75, 23)
        Me.btnColor.TabIndex = 20
        Me.btnColor.Text = "Couleur"
        Me.btnColor.UseVisualStyleBackColor = True
        '
        'frmExpenseType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 71)
        Me.Controls.Add(Me.btnColor)
        Me.Controls.Add(Me.formController)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExpenseType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Type de dépense"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents btnColor As System.Windows.Forms.Button
End Class
