<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIncome
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIncome))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboFrequency = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpPayDate = New System.Windows.Forms.DateTimePicker()
        Me.formController = New DMS_Application.ctlFormController()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkMainIncome = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nom:"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(73, 6)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(228, 20)
        Me.txtName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(237, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Fréquence"
        '
        'cboFrequency
        '
        Me.cboFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFrequency.FormattingEnabled = True
        Me.cboFrequency.Location = New System.Drawing.Point(305, 52)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(163, 21)
        Me.cboFrequency.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Prochaine réception le:"
        '
        'dtpPayDate
        '
        Me.dtpPayDate.CustomFormat = "dd/MM/yy"
        Me.dtpPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPayDate.Location = New System.Drawing.Point(129, 52)
        Me.dtpPayDate.Name = "dtpPayDate"
        Me.dtpPayDate.ShowCheckBox = True
        Me.dtpPayDate.Size = New System.Drawing.Size(102, 20)
        Me.dtpPayDate.TabIndex = 2
        Me.dtpPayDate.Value = New Date(2014, 9, 4, 1, 12, 37, 0)
        '
        'formController
        '
        Me.formController.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(0, 75)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(473, 33)
        Me.formController.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Montant:"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(73, 28)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAmount.Size = New System.Drawing.Size(75, 20)
        Me.txtAmount.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(148, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "$"
        '
        'chkMainIncome
        '
        Me.chkMainIncome.AutoSize = True
        Me.chkMainIncome.Location = New System.Drawing.Point(317, 8)
        Me.chkMainIncome.Name = "chkMainIncome"
        Me.chkMainIncome.Size = New System.Drawing.Size(106, 17)
        Me.chkMainIncome.TabIndex = 10
        Me.chkMainIncome.Text = "Revenu principal"
        Me.chkMainIncome.UseVisualStyleBackColor = True
        '
        'frmIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 106)
        Me.Controls.Add(Me.chkMainIncome)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboFrequency)
        Me.Controls.Add(Me.dtpPayDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.formController)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIncome"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Revenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpPayDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkMainIncome As System.Windows.Forms.CheckBox
End Class
