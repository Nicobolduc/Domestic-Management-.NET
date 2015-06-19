<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExpense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpense))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboInterval = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpBillDate = New System.Windows.Forms.DateTimePicker()
        Me.formController = New DMS_Application.ctlFormController()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
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
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(73, 6)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(228, 20)
        Me.txtCode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(206, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Récurrence:"
        '
        'cboInterval
        '
        Me.cboInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInterval.FormattingEnabled = True
        Me.cboInterval.Location = New System.Drawing.Point(274, 53)
        Me.cboInterval.Name = "cboInterval"
        Me.cboInterval.Size = New System.Drawing.Size(163, 21)
        Me.cboInterval.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Prochaine facturation le:"
        '
        'dtpBillDate
        '
        Me.dtpBillDate.CustomFormat = "dd/MM"
        Me.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBillDate.Location = New System.Drawing.Point(129, 53)
        Me.dtpBillDate.Name = "dtpBillDate"
        Me.dtpBillDate.ShowCheckBox = True
        Me.dtpBillDate.Size = New System.Drawing.Size(71, 20)
        Me.dtpBillDate.TabIndex = 2
        Me.dtpBillDate.Value = New Date(2014, 9, 4, 1, 12, 37, 0)
        '
        'formController
        '
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Modes.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(0, 86)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(445, 33)
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
        'frmExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 117)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboInterval)
        Me.Controls.Add(Me.dtpBillDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.formController)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExpense"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dépense"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboInterval As System.Windows.Forms.ComboBox
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpBillDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
