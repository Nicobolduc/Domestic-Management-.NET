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
        Me.formController = New DMS_Application.ctlFormController()
        Me.chkMainIncome = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnRemoveRow = New System.Windows.Forms.Button()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.grdPeriod = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.cboFrequency = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'formController
        '
        Me.formController.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(3, 205)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(413, 33)
        Me.formController.TabIndex = 4
        '
        'chkMainIncome
        '
        Me.chkMainIncome.AutoSize = True
        Me.chkMainIncome.Location = New System.Drawing.Point(307, 8)
        Me.chkMainIncome.Name = "chkMainIncome"
        Me.chkMainIncome.Size = New System.Drawing.Size(106, 17)
        Me.chkMainIncome.TabIndex = 10
        Me.chkMainIncome.Text = "Revenu principal"
        Me.chkMainIncome.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAddRow)
        Me.GroupBox1.Controls.Add(Me.btnRemoveRow)
        Me.GroupBox1.Controls.Add(Me.grdPeriod)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 59)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(406, 143)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Périodes et montants"
        '
        'btnRemoveRow
        '
        Me.btnRemoveRow.BackgroundImage = CType(resources.GetObject("btnRemoveRow.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveRow.Location = New System.Drawing.Point(362, 60)
        Me.btnRemoveRow.Name = "btnRemoveRow"
        Me.btnRemoveRow.Size = New System.Drawing.Size(35, 35)
        Me.btnRemoveRow.TabIndex = 16
        Me.btnRemoveRow.UseVisualStyleBackColor = True
        '
        'btnAddRow
        '
        Me.btnAddRow.BackgroundImage = CType(resources.GetObject("btnAddRow.BackgroundImage"), System.Drawing.Image)
        Me.btnAddRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddRow.Location = New System.Drawing.Point(362, 19)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(35, 35)
        Me.btnAddRow.TabIndex = 15
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'grdPeriod
        '
        Me.grdPeriod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdPeriod.Location = New System.Drawing.Point(6, 20)
        Me.grdPeriod.Name = "grdPeriod"
        Me.grdPeriod.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdPeriod.Size = New System.Drawing.Size(350, 116)
        Me.grdPeriod.SmartSizeBox = False
        Me.grdPeriod.TabIndex = 14
        Me.grdPeriod.Tag = "27"
        Me.grdPeriod.Text = "GridControl1"
        Me.grdPeriod.UseRightToLeftCompatibleTextBox = True
        '
        'cboFrequency
        '
        Me.cboFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFrequency.FormattingEnabled = True
        Me.cboFrequency.Location = New System.Drawing.Point(73, 32)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(228, 21)
        Me.cboFrequency.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Fréquence"
        '
        'frmIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 236)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkMainIncome)
        Me.Controls.Add(Me.cboFrequency)
        Me.Controls.Add(Me.formController)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIncome"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Revenu"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents chkMainIncome As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddRow As System.Windows.Forms.Button
    Friend WithEvents grdPeriod As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveRow As System.Windows.Forms.Button
End Class
