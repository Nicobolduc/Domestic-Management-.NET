<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneralList
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGeneralList))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdList = New System.Windows.Forms.DataGridView()
        Me.txtFiltre = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.myFormControler = New DMS_Application.ctlFormControler()
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Filtre: "
        '
        'grdList
        '
        Me.grdList.AllowUserToAddRows = False
        Me.grdList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdList.EnableHeadersVisualStyles = False
        Me.grdList.Location = New System.Drawing.Point(12, 39)
        Me.grdList.MultiSelect = False
        Me.grdList.Name = "grdList"
        Me.grdList.RowHeadersWidth = 10
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.grdList.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdList.Size = New System.Drawing.Size(507, 486)
        Me.grdList.TabIndex = 0
        Me.grdList.Tag = ""
        '
        'txtFiltre
        '
        Me.txtFiltre.Location = New System.Drawing.Point(53, 9)
        Me.txtFiltre.Name = "txtFiltre"
        Me.txtFiltre.Size = New System.Drawing.Size(142, 20)
        Me.txtFiltre.TabIndex = 1
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(525, 146)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 3
        Me.ToolTips.SetToolTip(Me.btnDelete, "Supprimer")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.Location = New System.Drawing.Point(525, 92)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 2
        Me.ToolTips.SetToolTip(Me.btnUpdate, "Modifier")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(525, 38)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(48, 48)
        Me.btnAdd.TabIndex = 1
        Me.ToolTips.SetToolTip(Me.btnAdd, "Ajouter")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'ToolTips
        '
        Me.ToolTips.IsBalloon = True
        '
        'myFormControler
        '
        Me.myFormControler.FormIsLoading = False
        Me.myFormControler.FormMode = DMS_Application.clsConstants.Form_Modes.CONSULT_MODE
        Me.myFormControler.Item_ID = 0
        Me.myFormControler.Location = New System.Drawing.Point(494, 527)
        Me.myFormControler.Name = "myFormControler"
        Me.myFormControler.ShowButtonQuitOnly = True
        Me.myFormControler.Size = New System.Drawing.Size(85, 33)
        Me.myFormControler.TabIndex = 4
        '
        'frmGeneralList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 559)
        Me.Controls.Add(Me.myFormControler)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.txtFiltre)
        Me.Controls.Add(Me.grdList)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmGeneralList"
        Me.Text = "Liste générique"
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdList As System.Windows.Forms.DataGridView
    Friend WithEvents txtFiltre As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Public WithEvents myFormControler As DMS_Application.ctlFormControler
End Class
