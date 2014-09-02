<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProduct
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProduct))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbInfos = New System.Windows.Forms.GroupBox()
        Me.btnRemoveLine = New System.Windows.Forms.Button()
        Me.btnAddLine = New System.Windows.Forms.Button()
        Me.grdPrices = New System.Windows.Forms.DataGridView()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.myFormControler = New DMS_Application.ctlFormControler()
        Me.gbInfos.SuspendLayout()
        CType(Me.grdPrices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Nom:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Catégorie:"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(72, 6)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(299, 20)
        Me.txtName.TabIndex = 1
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(72, 55)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(299, 21)
        Me.cboCategory.TabIndex = 3
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(72, 30)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(299, 21)
        Me.cboType.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Type:"
        '
        'gbInfos
        '
        Me.gbInfos.Controls.Add(Me.cboCompany)
        Me.gbInfos.Controls.Add(Me.btnRemoveLine)
        Me.gbInfos.Controls.Add(Me.btnAddLine)
        Me.gbInfos.Controls.Add(Me.grdPrices)
        Me.gbInfos.Location = New System.Drawing.Point(5, 109)
        Me.gbInfos.Name = "gbInfos"
        Me.gbInfos.Size = New System.Drawing.Size(366, 156)
        Me.gbInfos.TabIndex = 14
        Me.gbInfos.TabStop = False
        Me.gbInfos.Text = "Prix"
        '
        'btnRemoveLine
        '
        Me.btnRemoveLine.BackgroundImage = CType(resources.GetObject("btnRemoveLine.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveLine.Location = New System.Drawing.Point(326, 59)
        Me.btnRemoveLine.Name = "btnRemoveLine"
        Me.btnRemoveLine.Size = New System.Drawing.Size(35, 35)
        Me.btnRemoveLine.TabIndex = 3
        Me.btnRemoveLine.UseVisualStyleBackColor = True
        '
        'btnAddLine
        '
        Me.btnAddLine.BackgroundImage = CType(resources.GetObject("btnAddLine.BackgroundImage"), System.Drawing.Image)
        Me.btnAddLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddLine.Location = New System.Drawing.Point(326, 18)
        Me.btnAddLine.Name = "btnAddLine"
        Me.btnAddLine.Size = New System.Drawing.Size(35, 35)
        Me.btnAddLine.TabIndex = 2
        Me.btnAddLine.UseVisualStyleBackColor = True
        '
        'grdPrices
        '
        Me.grdPrices.AllowUserToAddRows = False
        Me.grdPrices.AllowUserToOrderColumns = True
        Me.grdPrices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPrices.EnableHeadersVisualStyles = False
        Me.grdPrices.Location = New System.Drawing.Point(6, 18)
        Me.grdPrices.MultiSelect = False
        Me.grdPrices.Name = "grdPrices"
        Me.grdPrices.RowHeadersWidth = 10
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.grdPrices.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdPrices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPrices.Size = New System.Drawing.Size(316, 131)
        Me.grdPrices.TabIndex = 1
        Me.grdPrices.Tag = "9"
        '
        'cboCompany
        '
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(22, 113)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(76, 21)
        Me.cboCompany.TabIndex = 18
        Me.cboCompany.Visible = False
        '
        'myFormControler
        '
        Me.myFormControler.FormIsLoading = False
        Me.myFormControler.FormMode = DMS_Application.clsConstants.Form_Modes.CONSULT_MODE
        Me.myFormControler.Item_ID = 0
        Me.myFormControler.Location = New System.Drawing.Point(0, 267)
        Me.myFormControler.Name = "myFormControler"
        Me.myFormControler.ShowButtonQuitOnly = False
        Me.myFormControler.Size = New System.Drawing.Size(376, 33)
        Me.myFormControler.TabIndex = 15
        '
        'frmProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(376, 299)
        Me.Controls.Add(Me.myFormControler)
        Me.Controls.Add(Me.gbInfos)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCategory)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProduct"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Produit"
        Me.gbInfos.ResumeLayout(False)
        CType(Me.grdPrices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbInfos As System.Windows.Forms.GroupBox
    Public WithEvents myFormControler As DMS_Application.ctlFormControler
    Friend WithEvents grdPrices As System.Windows.Forms.DataGridView
    Friend WithEvents btnRemoveLine As System.Windows.Forms.Button
    Friend WithEvents btnAddLine As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
End Class
