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
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboInterval = New System.Windows.Forms.ComboBox()
        Me.formController = New DMS_Application.ctlFormController()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.chkFixed = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grdAmount = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAmount, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Label2.Location = New System.Drawing.Point(4, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Récurrence:"
        '
        'cboInterval
        '
        Me.cboInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInterval.FormattingEnabled = True
        Me.cboInterval.Location = New System.Drawing.Point(73, 58)
        Me.cboInterval.Name = "cboInterval"
        Me.cboInterval.Size = New System.Drawing.Size(163, 21)
        Me.cboInterval.TabIndex = 3
        '
        'formController
        '
        Me.formController.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(-86, 241)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(473, 33)
        Me.formController.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Type:"
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(73, 31)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(163, 21)
        Me.cboType.TabIndex = 11
        '
        'chkFixed
        '
        Me.chkFixed.AutoSize = True
        Me.chkFixed.Location = New System.Drawing.Point(253, 33)
        Me.chkFixed.Name = "chkFixed"
        Me.chkFixed.Size = New System.Drawing.Size(45, 17)
        Me.chkFixed.TabIndex = 12
        Me.chkFixed.Text = "Fixe"
        Me.chkFixed.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAddRow)
        Me.GroupBox1.Controls.Add(Me.grdAmount)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 93)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(373, 143)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Périodes et montants"
        '
        'grdAmount
        '
        Me.grdAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdAmount.Location = New System.Drawing.Point(6, 19)
        Me.grdAmount.Name = "grdAmount"
        Me.grdAmount.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdAmount.Size = New System.Drawing.Size(318, 116)
        Me.grdAmount.SmartSizeBox = False
        Me.grdAmount.TabIndex = 14
        Me.grdAmount.Tag = "21"
        Me.grdAmount.Text = "GridControl1"
        Me.grdAmount.UseRightToLeftCompatibleTextBox = True
        '
        'btnAddRow
        '
        Me.btnAddRow.BackgroundImage = CType(resources.GetObject("btnAddRow.BackgroundImage"), System.Drawing.Image)
        Me.btnAddRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddRow.Location = New System.Drawing.Point(330, 19)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(35, 35)
        Me.btnAddRow.TabIndex = 15
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'frmExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 272)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkFixed)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboInterval)
        Me.Controls.Add(Me.formController)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExpense"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dépense"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboInterval As System.Windows.Forms.ComboBox
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents chkFixed As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdAmount As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents btnAddRow As System.Windows.Forms.Button
End Class
