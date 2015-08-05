<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMealManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMealManagement))
        Me.grdMeals = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.formController = New DMS_Application.ctlFormController()
        CType(Me.grdMeals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdMeals
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
        Me.grdMeals.BaseStylesMap.AddRange(New Syncfusion.Windows.Forms.Grid.GridBaseStyle() {GridBaseStyle1, GridBaseStyle2, GridBaseStyle3, GridBaseStyle4})
        Me.grdMeals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdMeals.ColorStyles = Syncfusion.Windows.Forms.ColorStyles.Office2010Blue
        Me.grdMeals.ColWidthEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridColWidth() {New Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)})
        Me.grdMeals.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue
        Me.grdMeals.Location = New System.Drawing.Point(12, 52)
        Me.grdMeals.Name = "grdMeals"
        Me.grdMeals.Properties.ForceImmediateRepaint = False
        Me.grdMeals.Properties.MarkColHeader = True
        Me.grdMeals.Properties.MarkRowHeader = False
        Me.grdMeals.RowHeightEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridRowHeight() {New Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)})
        Me.grdMeals.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdMeals.Size = New System.Drawing.Size(787, 629)
        Me.grdMeals.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.grdMeals.SmartSizeBox = False
        Me.grdMeals.TabIndex = 4
        Me.grdMeals.Tag = "20"
        Me.grdMeals.Text = "GridControl1"
        Me.grdMeals.UseRightToLeftCompatibleTextBox = True
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Lucida Handwriting", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Navy
        Me.lblDate.Location = New System.Drawing.Point(280, 14)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(219, 32)
        Me.lblDate.TabIndex = 7
        Me.lblDate.Text = "septembre 2015"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnLeft
        '
        Me.btnLeft.BackColor = System.Drawing.Color.Transparent
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLeft.Image = CType(resources.GetObject("btnLeft.Image"), System.Drawing.Image)
        Me.btnLeft.Location = New System.Drawing.Point(242, 14)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(32, 32)
        Me.btnLeft.TabIndex = 8
        Me.btnLeft.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(505, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 32)
        Me.Button1.TabIndex = 9
        Me.Button1.UseVisualStyleBackColor = False
        '
        'formController
        '
        Me.formController.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(717, 687)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = True
        Me.formController.Size = New System.Drawing.Size(85, 33)
        Me.formController.TabIndex = 1
        '
        'frmMealManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 720)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnLeft)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.grdMeals)
        Me.Controls.Add(Me.formController)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMealManagement"
        Me.Text = "Repas"
        CType(Me.grdMeals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents grdMeals As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
