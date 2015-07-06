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
        Dim GridBaseStyle1 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle2 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle3 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle4 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProduct))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbInfos = New System.Windows.Forms.GroupBox()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.grdPrices = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.btnRemoveRow = New System.Windows.Forms.Button()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.chkTaxable = New System.Windows.Forms.CheckBox()
        Me.formController = New DMS_Application.ctlFormController()
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
        Me.txtName.TabIndex = 0
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(72, 55)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(299, 21)
        Me.cboCategory.TabIndex = 2
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(72, 30)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(299, 21)
        Me.cboType.TabIndex = 1
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
        Me.gbInfos.Controls.Add(Me.grdPrices)
        Me.gbInfos.Controls.Add(Me.btnRemoveRow)
        Me.gbInfos.Controls.Add(Me.btnAddRow)
        Me.gbInfos.Location = New System.Drawing.Point(5, 104)
        Me.gbInfos.Name = "gbInfos"
        Me.gbInfos.Size = New System.Drawing.Size(406, 160)
        Me.gbInfos.TabIndex = 14
        Me.gbInfos.TabStop = False
        Me.gbInfos.Text = "Prix"
        '
        'cboCompany
        '
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(44, 59)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(76, 21)
        Me.cboCompany.TabIndex = 19
        Me.cboCompany.Visible = False
        '
        'grdPrices
        '
        GridBaseStyle1.Name = "Header"
        GridBaseStyle1.StyleInfo.Borders.Bottom = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle1.StyleInfo.Borders.Left = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle1.StyleInfo.Borders.Right = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle1.StyleInfo.Borders.Top = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle1.StyleInfo.CellType = "Header"
        GridBaseStyle1.StyleInfo.Font.Bold = True
        GridBaseStyle1.StyleInfo.Interior = New Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(184, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(216, Byte), Integer)))
        GridBaseStyle1.StyleInfo.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle
        GridBaseStyle2.Name = "Standard"
        GridBaseStyle2.StyleInfo.Font.Facename = "Tahoma"
        GridBaseStyle2.StyleInfo.Interior = New Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Window)
        GridBaseStyle3.Name = "Column Header"
        GridBaseStyle3.StyleInfo.BaseStyle = "Header"
        GridBaseStyle3.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center
        GridBaseStyle4.Name = "Row Header"
        GridBaseStyle4.StyleInfo.BaseStyle = "Header"
        GridBaseStyle4.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
        GridBaseStyle4.StyleInfo.Interior = New Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(184, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(216, Byte), Integer)))
        Me.grdPrices.BaseStylesMap.AddRange(New Syncfusion.Windows.Forms.Grid.GridBaseStyle() {GridBaseStyle1, GridBaseStyle2, GridBaseStyle3, GridBaseStyle4})
        Me.grdPrices.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdPrices.ColWidthEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridColWidth() {New Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)})
        Me.grdPrices.ControllerOptions = CType(((((Syncfusion.Windows.Forms.Grid.GridControllerOptions.ClickCells Or Syncfusion.Windows.Forms.Grid.GridControllerOptions.DragSelectRowOrColumn) _
            Or Syncfusion.Windows.Forms.Grid.GridControllerOptions.OleDataSource) _
            Or Syncfusion.Windows.Forms.Grid.GridControllerOptions.OleDropTarget) _
            Or Syncfusion.Windows.Forms.Grid.GridControllerOptions.ExcelLikeSelection), Syncfusion.Windows.Forms.Grid.GridControllerOptions)
        Me.grdPrices.Location = New System.Drawing.Point(6, 18)
        Me.grdPrices.Name = "grdPrices"
        Me.grdPrices.RowHeightEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridRowHeight() {New Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)})
        Me.grdPrices.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdPrices.Size = New System.Drawing.Size(352, 135)
        Me.grdPrices.SmartSizeBox = False
        Me.grdPrices.TabIndex = 3
        Me.grdPrices.Tag = "9"
        Me.grdPrices.Text = "GridControl1"
        Me.grdPrices.UseRightToLeftCompatibleTextBox = True
        '
        'btnRemoveRow
        '
        Me.btnRemoveRow.BackgroundImage = CType(resources.GetObject("btnRemoveRow.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveRow.Location = New System.Drawing.Point(364, 59)
        Me.btnRemoveRow.Name = "btnRemoveRow"
        Me.btnRemoveRow.Size = New System.Drawing.Size(35, 35)
        Me.btnRemoveRow.TabIndex = 2
        Me.btnRemoveRow.UseVisualStyleBackColor = True
        '
        'btnAddRow
        '
        Me.btnAddRow.BackgroundImage = CType(resources.GetObject("btnAddRow.BackgroundImage"), System.Drawing.Image)
        Me.btnAddRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddRow.Location = New System.Drawing.Point(364, 18)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(35, 35)
        Me.btnAddRow.TabIndex = 1
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'chkTaxable
        '
        Me.chkTaxable.Location = New System.Drawing.Point(11, 84)
        Me.chkTaxable.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.chkTaxable.Name = "chkTaxable"
        Me.chkTaxable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkTaxable.Size = New System.Drawing.Size(76, 17)
        Me.chkTaxable.TabIndex = 3
        Me.chkTaxable.Text = "Taxable"
        Me.chkTaxable.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkTaxable.UseVisualStyleBackColor = True
        '
        'formController
        '
        Me.formController.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(0, 268)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(416, 33)
        Me.formController.TabIndex = 4
        '
        'frmProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 299)
        Me.Controls.Add(Me.chkTaxable)
        Me.Controls.Add(Me.formController)
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
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents btnRemoveRow As System.Windows.Forms.Button
    Friend WithEvents btnAddRow As System.Windows.Forms.Button
    Friend WithEvents chkTaxable As System.Windows.Forms.CheckBox
    Friend WithEvents grdPrices As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
End Class
