<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageBudget
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageBudget))
        Me.grdBudget = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.myFormControler = New DMS_Application.ctlFormControler()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdBudget
        '
        Me.grdBudget.AllowUserToAddRows = False
        Me.grdBudget.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBudget.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.grdBudget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBudget.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column3, Me.Column2})
        Me.grdBudget.EnableHeadersVisualStyles = False
        Me.grdBudget.Location = New System.Drawing.Point(12, 131)
        Me.grdBudget.MultiSelect = False
        Me.grdBudget.Name = "grdBudget"
        Me.grdBudget.ReadOnly = True
        Me.grdBudget.RowHeadersWidth = 10
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.grdBudget.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdBudget.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdBudget.Size = New System.Drawing.Size(711, 481)
        Me.grdBudget.TabIndex = 2
        Me.grdBudget.Tag = "11"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
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
        Me.gbFilter.Location = New System.Drawing.Point(12, 12)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(711, 113)
        Me.gbFilter.TabIndex = 3
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "Filtres"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Location = New System.Drawing.Point(664, 20)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(40, 40)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(246, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Au:"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MMMM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(276, 65)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(143, 20)
        Me.dtpTo.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(245, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Du:"
        '
        'rbtnDeuxMois
        '
        Me.rbtnDeuxMois.AutoSize = True
        Me.rbtnDeuxMois.Location = New System.Drawing.Point(12, 88)
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
        Me.rbtnMensuelle.Location = New System.Drawing.Point(12, 65)
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
        Me.dtpFrom.Location = New System.Drawing.Point(275, 40)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(144, 20)
        Me.dtpFrom.TabIndex = 0
        '
        'myFormControler
        '
        Me.myFormControler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.myFormControler.FormIsLoading = False
        Me.myFormControler.FormMode = DMS_Application.clsConstants.Form_Modes.CONSULT_MODE
        Me.myFormControler.Item_ID = 0
        Me.myFormControler.Location = New System.Drawing.Point(638, 613)
        Me.myFormControler.Name = "myFormControler"
        Me.myFormControler.ShowButtonQuitOnly = True
        Me.myFormControler.Size = New System.Drawing.Size(85, 33)
        Me.myFormControler.TabIndex = 4
        '
        'frmManageBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 645)
        Me.Controls.Add(Me.gbFilter)
        Me.Controls.Add(Me.grdBudget)
        Me.Controls.Add(Me.myFormControler)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmManageBudget"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Budget"
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdBudget As System.Windows.Forms.DataGridView
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnDeuxMois As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnHebdo As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBiMensuel As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnMensuelle As System.Windows.Forms.RadioButton
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Public WithEvents myFormControler As DMS_Application.ctlFormControler

End Class
