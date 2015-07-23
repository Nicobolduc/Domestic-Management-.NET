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
        Me.components = New System.ComponentModel.Container()
        Dim GridBaseStyle5 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle6 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle7 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle8 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBudgetManagement))
        Me.gbFilter = New System.Windows.Forms.GroupBox()
        Me.rbtnNotPaid = New System.Windows.Forms.RadioButton()
        Me.rbtnPaid = New System.Windows.Forms.RadioButton()
        Me.dtpTo = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        Me.dtpFrom = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        Me.btnAfter = New System.Windows.Forms.Button()
        Me.btnBefore = New System.Windows.Forms.Button()
        Me.lblMainIncomeAmount = New System.Windows.Forms.Label()
        Me.lblMainIncomeDate = New System.Windows.Forms.Label()
        Me.lblMainIncomeAmount_text = New System.Windows.Forms.Label()
        Me.lblMainIncomeDate_text = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbtnDeuxMois = New System.Windows.Forms.RadioButton()
        Me.rbtnHebdo = New System.Windows.Forms.RadioButton()
        Me.rbtnBiMensuel = New System.Windows.Forms.RadioButton()
        Me.rbtnMensuelle = New System.Windows.Forms.RadioButton()
        Me.grdBudget = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnPay = New System.Windows.Forms.Button()
        Me.formController = New DMS_Application.ctlFormController()
        Me.gbFilter.SuspendLayout()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbFilter
        '
        Me.gbFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFilter.Controls.Add(Me.rbtnNotPaid)
        Me.gbFilter.Controls.Add(Me.rbtnPaid)
        Me.gbFilter.Controls.Add(Me.dtpTo)
        Me.gbFilter.Controls.Add(Me.dtpFrom)
        Me.gbFilter.Controls.Add(Me.btnAfter)
        Me.gbFilter.Controls.Add(Me.btnBefore)
        Me.gbFilter.Controls.Add(Me.lblMainIncomeAmount)
        Me.gbFilter.Controls.Add(Me.lblMainIncomeDate)
        Me.gbFilter.Controls.Add(Me.lblMainIncomeAmount_text)
        Me.gbFilter.Controls.Add(Me.lblMainIncomeDate_text)
        Me.gbFilter.Controls.Add(Me.Label2)
        Me.gbFilter.Controls.Add(Me.Label1)
        Me.gbFilter.Controls.Add(Me.rbtnDeuxMois)
        Me.gbFilter.Controls.Add(Me.rbtnHebdo)
        Me.gbFilter.Controls.Add(Me.rbtnBiMensuel)
        Me.gbFilter.Controls.Add(Me.rbtnMensuelle)
        Me.gbFilter.Location = New System.Drawing.Point(4, 2)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(755, 64)
        Me.gbFilter.TabIndex = 3
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "Filtres"
        '
        'rbtnNotPaid
        '
        Me.rbtnNotPaid.AutoSize = True
        Me.rbtnNotPaid.Checked = True
        Me.rbtnNotPaid.Location = New System.Drawing.Point(372, 17)
        Me.rbtnNotPaid.Name = "rbtnNotPaid"
        Me.rbtnNotPaid.Size = New System.Drawing.Size(71, 17)
        Me.rbtnNotPaid.TabIndex = 17
        Me.rbtnNotPaid.TabStop = True
        Me.rbtnNotPaid.Text = "Non payé"
        Me.rbtnNotPaid.UseVisualStyleBackColor = True
        '
        'rbtnPaid
        '
        Me.rbtnPaid.AutoSize = True
        Me.rbtnPaid.Location = New System.Drawing.Point(372, 40)
        Me.rbtnPaid.Name = "rbtnPaid"
        Me.rbtnPaid.Size = New System.Drawing.Size(49, 17)
        Me.rbtnPaid.TabIndex = 16
        Me.rbtnPaid.Text = "Payé"
        Me.rbtnPaid.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat
        Me.dtpTo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.dtpTo.Calendar.AllowMultipleSelection = False
        Me.dtpTo.Calendar.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpTo.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpTo.Calendar.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpTo.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.Calendar.DayNamesFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.dtpTo.Calendar.DaysFont = New System.Drawing.Font("Verdana", 8.0!)
        Me.dtpTo.Calendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpTo.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Calendar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpTo.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None
        Me.dtpTo.Calendar.HeaderEndColor = System.Drawing.Color.White
        Me.dtpTo.Calendar.HeaderStartColor = System.Drawing.Color.White
        Me.dtpTo.Calendar.HeadForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpTo.Calendar.HighlightColor = System.Drawing.Color.White
        Me.dtpTo.Calendar.Iso8601CalenderFormat = False
        Me.dtpTo.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpTo.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.Calendar.Name = "monthCalendar"
        Me.dtpTo.Calendar.ScrollButtonSize = New System.Drawing.Size(24, 24)
        Me.dtpTo.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpTo.Calendar.Size = New System.Drawing.Size(71, 174)
        Me.dtpTo.Calendar.SizeToFit = True
        Me.dtpTo.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Metro
        Me.dtpTo.Calendar.TabIndex = 0
        Me.dtpTo.Calendar.ThemedEnabledScrollButtons = False
        Me.dtpTo.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpTo.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro
        Me.dtpTo.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpTo.Calendar.NoneButton.ForeColor = System.Drawing.Color.White
        Me.dtpTo.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpTo.Calendar.NoneButton.Location = New System.Drawing.Point(-1, 0)
        Me.dtpTo.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpTo.Calendar.NoneButton.Text = "None"
        Me.dtpTo.Calendar.NoneButton.UseVisualStyle = True
        '
        '
        '
        Me.dtpTo.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro
        Me.dtpTo.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpTo.Calendar.TodayButton.ForeColor = System.Drawing.Color.White
        Me.dtpTo.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpTo.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpTo.Calendar.TodayButton.Size = New System.Drawing.Size(0, 20)
        Me.dtpTo.Calendar.TodayButton.Text = "Today"
        Me.dtpTo.Calendar.TodayButton.UseVisualStyle = True
        Me.dtpTo.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpTo.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpTo.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpTo.DropDownImage = Nothing
        Me.dtpTo.DropDownNormalColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.DropDownPressedColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.DropDownSelectedColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(236, 38)
        Me.dtpTo.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.MinValue = New Date(CType(0, Long))
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ReadOnly = True
        Me.dtpTo.ShowCheckBox = False
        Me.dtpTo.ShowDropButton = False
        Me.dtpTo.Size = New System.Drawing.Size(73, 20)
        Me.dtpTo.Style = Syncfusion.Windows.Forms.VisualStyle.Metro
        Me.dtpTo.TabIndex = 15
        Me.dtpTo.Value = New Date(2015, 7, 15, 0, 0, 0, 0)
        '
        'dtpFrom
        '
        Me.dtpFrom.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat
        Me.dtpFrom.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.dtpFrom.Calendar.AllowMultipleSelection = False
        Me.dtpFrom.Calendar.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpFrom.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpFrom.Calendar.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpFrom.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.Calendar.DayNamesFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.dtpFrom.Calendar.DaysFont = New System.Drawing.Font("Verdana", 8.0!)
        Me.dtpFrom.Calendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFrom.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Calendar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFrom.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None
        Me.dtpFrom.Calendar.HeaderEndColor = System.Drawing.Color.White
        Me.dtpFrom.Calendar.HeaderStartColor = System.Drawing.Color.White
        Me.dtpFrom.Calendar.HeadForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFrom.Calendar.HighlightColor = System.Drawing.Color.White
        Me.dtpFrom.Calendar.Iso8601CalenderFormat = False
        Me.dtpFrom.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpFrom.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.Calendar.Name = "monthCalendar"
        Me.dtpFrom.Calendar.ScrollButtonSize = New System.Drawing.Size(24, 24)
        Me.dtpFrom.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpFrom.Calendar.Size = New System.Drawing.Size(71, 174)
        Me.dtpFrom.Calendar.SizeToFit = True
        Me.dtpFrom.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Metro
        Me.dtpFrom.Calendar.TabIndex = 0
        Me.dtpFrom.Calendar.ThemedEnabledScrollButtons = False
        Me.dtpFrom.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpFrom.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro
        Me.dtpFrom.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpFrom.Calendar.NoneButton.ForeColor = System.Drawing.Color.White
        Me.dtpFrom.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpFrom.Calendar.NoneButton.Location = New System.Drawing.Point(-1, 0)
        Me.dtpFrom.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpFrom.Calendar.NoneButton.Text = "None"
        Me.dtpFrom.Calendar.NoneButton.UseVisualStyle = True
        '
        '
        '
        Me.dtpFrom.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro
        Me.dtpFrom.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpFrom.Calendar.TodayButton.ForeColor = System.Drawing.Color.White
        Me.dtpFrom.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpFrom.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpFrom.Calendar.TodayButton.Size = New System.Drawing.Size(0, 20)
        Me.dtpFrom.Calendar.TodayButton.Text = "Today"
        Me.dtpFrom.Calendar.TodayButton.UseVisualStyle = True
        Me.dtpFrom.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFrom.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpFrom.DropDownImage = Nothing
        Me.dtpFrom.DropDownNormalColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.DropDownPressedColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.DropDownSelectedColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(236, 13)
        Me.dtpFrom.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.MinValue = New Date(CType(0, Long))
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ReadOnly = True
        Me.dtpFrom.ShowCheckBox = False
        Me.dtpFrom.ShowDropButton = False
        Me.dtpFrom.Size = New System.Drawing.Size(73, 20)
        Me.dtpFrom.Style = Syncfusion.Windows.Forms.VisualStyle.Metro
        Me.dtpFrom.TabIndex = 11
        Me.dtpFrom.Value = New Date(2015, 7, 15, 0, 0, 0, 0)
        '
        'btnAfter
        '
        Me.btnAfter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAfter.Location = New System.Drawing.Point(315, 38)
        Me.btnAfter.Name = "btnAfter"
        Me.btnAfter.Size = New System.Drawing.Size(23, 20)
        Me.btnAfter.TabIndex = 14
        Me.btnAfter.UseVisualStyleBackColor = True
        '
        'btnBefore
        '
        Me.btnBefore.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBefore.Location = New System.Drawing.Point(315, 13)
        Me.btnBefore.Name = "btnBefore"
        Me.btnBefore.Size = New System.Drawing.Size(23, 20)
        Me.btnBefore.TabIndex = 13
        Me.btnBefore.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBefore.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnBefore.UseVisualStyleBackColor = True
        '
        'lblMainIncomeAmount
        '
        Me.lblMainIncomeAmount.Location = New System.Drawing.Point(638, 42)
        Me.lblMainIncomeAmount.Name = "lblMainIncomeAmount"
        Me.lblMainIncomeAmount.Size = New System.Drawing.Size(107, 16)
        Me.lblMainIncomeAmount.TabIndex = 12
        Me.lblMainIncomeAmount.Text = "nothing"
        '
        'lblMainIncomeDate
        '
        Me.lblMainIncomeDate.Location = New System.Drawing.Point(638, 18)
        Me.lblMainIncomeDate.Name = "lblMainIncomeDate"
        Me.lblMainIncomeDate.Size = New System.Drawing.Size(111, 16)
        Me.lblMainIncomeDate.TabIndex = 11
        Me.lblMainIncomeDate.Text = "nothing"
        '
        'lblMainIncomeAmount_text
        '
        Me.lblMainIncomeAmount_text.Location = New System.Drawing.Point(484, 42)
        Me.lblMainIncomeAmount_text.Name = "lblMainIncomeAmount_text"
        Me.lblMainIncomeAmount_text.Size = New System.Drawing.Size(148, 16)
        Me.lblMainIncomeAmount_text.TabIndex = 9
        Me.lblMainIncomeAmount_text.Text = "Montant du revenu principal:"
        '
        'lblMainIncomeDate_text
        '
        Me.lblMainIncomeDate_text.Location = New System.Drawing.Point(484, 17)
        Me.lblMainIncomeDate_text.Name = "lblMainIncomeDate_text"
        Me.lblMainIncomeDate_text.Size = New System.Drawing.Size(148, 16)
        Me.lblMainIncomeDate_text.TabIndex = 8
        Me.lblMainIncomeDate_text.Text = "Date du revenu principal:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(206, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Au:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(205, 18)
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
        Me.rbtnHebdo.Location = New System.Drawing.Point(12, 17)
        Me.rbtnHebdo.Name = "rbtnHebdo"
        Me.rbtnHebdo.Size = New System.Drawing.Size(94, 17)
        Me.rbtnHebdo.TabIndex = 3
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
        'grdBudget
        '
        Me.grdBudget.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridBaseStyle5.Name = "Header"
        GridBaseStyle5.StyleInfo.Borders.Bottom = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle5.StyleInfo.Borders.Left = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle5.StyleInfo.Borders.Right = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle5.StyleInfo.Borders.Top = New Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None)
        GridBaseStyle5.StyleInfo.CellType = "Header"
        GridBaseStyle5.StyleInfo.Font.Bold = True
        GridBaseStyle5.StyleInfo.Interior = New Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(184, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(216, Byte), Integer)))
        GridBaseStyle5.StyleInfo.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle
        GridBaseStyle6.Name = "Standard"
        GridBaseStyle6.StyleInfo.Font.Facename = "Tahoma"
        GridBaseStyle6.StyleInfo.Interior = New Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Window)
        GridBaseStyle7.Name = "Column Header"
        GridBaseStyle7.StyleInfo.BaseStyle = "Header"
        GridBaseStyle7.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center
        GridBaseStyle8.Name = "Row Header"
        GridBaseStyle8.StyleInfo.BaseStyle = "Header"
        GridBaseStyle8.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left
        GridBaseStyle8.StyleInfo.Interior = New Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(184, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(216, Byte), Integer)))
        Me.grdBudget.BaseStylesMap.AddRange(New Syncfusion.Windows.Forms.Grid.GridBaseStyle() {GridBaseStyle5, GridBaseStyle6, GridBaseStyle7, GridBaseStyle8})
        Me.grdBudget.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdBudget.ColWidthEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridColWidth() {New Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)})
        Me.grdBudget.Location = New System.Drawing.Point(4, 72)
        Me.grdBudget.Name = "grdBudget"
        Me.grdBudget.RowHeightEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridRowHeight() {New Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)})
        Me.grdBudget.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdBudget.Size = New System.Drawing.Size(801, 540)
        Me.grdBudget.SmartSizeBox = False
        Me.grdBudget.TabIndex = 5
        Me.grdBudget.Tag = "11"
        Me.grdBudget.Text = "GridControl1"
        Me.grdBudget.UseRightToLeftCompatibleTextBox = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Location = New System.Drawing.Point(765, 8)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(40, 40)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnPay
        '
        Me.btnPay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPay.Location = New System.Drawing.Point(649, 619)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.Size = New System.Drawing.Size(75, 23)
        Me.btnPay.TabIndex = 11
        Me.btnPay.Text = "Payer"
        Me.btnPay.UseVisualStyleBackColor = True
        '
        'formController
        '
        Me.formController.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(725, 614)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = True
        Me.formController.Size = New System.Drawing.Size(85, 33)
        Me.formController.TabIndex = 4
        '
        'frmBudgetManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 645)
        Me.Controls.Add(Me.btnPay)
        Me.Controls.Add(Me.btnRefresh)
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
        CType(Me.dtpTo.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnDeuxMois As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnHebdo As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBiMensuel As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnMensuelle As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents formController As DMS_Application.ctlFormController
    Friend WithEvents grdBudget As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents lblMainIncomeDate_text As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblMainIncomeAmount_text As System.Windows.Forms.Label
    Friend WithEvents lblMainIncomeDate As System.Windows.Forms.Label
    Friend WithEvents lblMainIncomeAmount As System.Windows.Forms.Label
    Friend WithEvents btnAfter As System.Windows.Forms.Button
    Friend WithEvents btnBefore As System.Windows.Forms.Button
    Friend WithEvents dtpFrom As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv
    Friend WithEvents dtpTo As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv
    Friend WithEvents btnPay As System.Windows.Forms.Button
    Friend WithEvents rbtnNotPaid As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPaid As System.Windows.Forms.RadioButton

End Class
