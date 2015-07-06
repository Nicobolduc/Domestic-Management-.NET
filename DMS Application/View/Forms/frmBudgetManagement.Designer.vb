<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBudgetManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBudgetManagement))
        Dim GridBaseStyle1 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle2 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle3 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle4 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Me.gbFilter = New System.Windows.Forms.GroupBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbtnDeuxMois = New System.Windows.Forms.RadioButton()
        Me.rbtnHebdo = New System.Windows.Forms.RadioButton()
        Me.rbtnBiMensuel = New System.Windows.Forms.RadioButton()
        Me.rbtnMensuelle = New System.Windows.Forms.RadioButton()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.grdBudget = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.formController = New DMS_Application.ctlFormController()
        Me.gbFilter.SuspendLayout()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbFilter
        '
        Me.gbFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFilter.Controls.Add(Me.btnRefresh)
        Me.gbFilter.Controls.Add(Me.Label2)
        Me.gbFilter.Controls.Add(Me.dtpTo)
        Me.gbFilter.Controls.Add(Me.Label1)
        Me.gbFilter.Controls.Add(Me.rbtnDeuxMois)
        Me.gbFilter.Controls.Add(Me.rbtnHebdo)
        Me.gbFilter.Controls.Add(Me.rbtnBiMensuel)
        Me.gbFilter.Controls.Add(Me.rbtnMensuelle)
        Me.gbFilter.Controls.Add(Me.dtpFrom)
        Me.gbFilter.Location = New System.Drawing.Point(4, 2)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(734, 64)
        Me.gbFilter.TabIndex = 3
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "Filtres"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Location = New System.Drawing.Point(687, 13)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(40, 40)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(328, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Au:"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MMMM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(358, 38)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(143, 20)
        Me.dtpTo.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(327, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Du:"
        '
        'rbtnDeuxMois
        '
        Me.rbtnDeuxMois.AutoSize = True
        Me.rbtnDeuxMois.Location = New System.Drawing.Point(112, 40)
        Me.rbtnDeuxMois.Name = "rbtnDeuxMois"
        Me.rbtnDeuxMois.Size = New System.Drawing.Size(71, 17)
        Me.rbtnDeuxMois.TabIndex = 4
        Me.rbtnDeuxMois.Text = "Au 2 mois"
        Me.rbtnDeuxMois.UseVisualStyleBackColor = True
        '
        'rbtnHebdo
        '
        Me.rbtnHebdo.AutoSize = True
        Me.rbtnHebdo.Checked = True
        Me.rbtnHebdo.Location = New System.Drawing.Point(12, 17)
        Me.rbtnHebdo.Name = "rbtnHebdo"
        Me.rbtnHebdo.Size = New System.Drawing.Size(94, 17)
        Me.rbtnHebdo.TabIndex = 3
        Me.rbtnHebdo.TabStop = True
        Me.rbtnHebdo.Text = "Hebdomadaire"
        Me.rbtnHebdo.UseVisualStyleBackColor = True
        '
        'rbtnBiMensuel
        '
        Me.rbtnBiMensuel.AutoSize = True
        Me.rbtnBiMensuel.Location = New System.Drawing.Point(12, 40)
        Me.rbtnBiMensuel.Name = "rbtnBiMensuel"
        Me.rbtnBiMensuel.Size = New System.Drawing.Size(81, 17)
        Me.rbtnBiMensuel.TabIndex = 2
        Me.rbtnBiMensuel.Text = "Bimensuelle"
        Me.rbtnBiMensuel.UseVisualStyleBackColor = True
        '
        'rbtnMensuelle
        '
        Me.rbtnMensuelle.AutoSize = True
        Me.rbtnMensuelle.Location = New System.Drawing.Point(112, 17)
        Me.rbtnMensuelle.Name = "rbtnMensuelle"
        Me.rbtnMensuelle.Size = New System.Drawing.Size(73, 17)
        Me.rbtnMensuelle.TabIndex = 1
        Me.rbtnMensuelle.Text = "Mensuelle"
        Me.rbtnMensuelle.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MMMM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(357, 13)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(144, 20)
        Me.dtpFrom.TabIndex = 0
        '
        'grdBudget
        '
        Me.grdBudget.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.grdBudget.BaseStylesMap.AddRange(New Syncfusion.Windows.Forms.Grid.GridBaseStyle() {GridBaseStyle1, GridBaseStyle2, GridBaseStyle3, GridBaseStyle4})
        Me.grdBudget.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdBudget.ColWidthEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridColWidth() {New Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)})
        Me.grdBudget.Location = New System.Drawing.Point(4, 72)
        Me.grdBudget.Name = "grdBudget"
        Me.grdBudget.RowHeightEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridRowHeight() {New Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)})
        Me.grdBudget.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdBudget.Size = New System.Drawing.Size(734, 540)
        Me.grdBudget.SmartSizeBox = False
        Me.grdBudget.TabIndex = 5
        Me.grdBudget.Tag = "11"
        Me.grdBudget.Text = "GridControl1"
        Me.grdBudget.UseRightToLeftCompatibleTextBox = True
        '
        'formController
        '
        Me.formController.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(658, 614)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = True
        Me.formController.Size = New System.Drawing.Size(85, 33)
        Me.formController.TabIndex = 4
        '
        'frmBudgetManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 645)
        Me.Controls.Add(Me.grdBudget)
        Me.Controls.Add(Me.gbFilter)
        Me.Controls.Add(Me.formController)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBudgetManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Budget"
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnDeuxMois As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnHebdo As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBiMensuel As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnMensuelle As System.Windows.Forms.RadioButton
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents grdBudget As Syncfusion.Windows.Forms.Grid.GridControl

End Class
