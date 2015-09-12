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
        Dim GridBaseStyle1 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle2 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle3 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim GridBaseStyle4 As Syncfusion.Windows.Forms.Grid.GridBaseStyle = New Syncfusion.Windows.Forms.Grid.GridBaseStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBudgetManagement))
        Me.gbFilter = New System.Windows.Forms.GroupBox()
        Me.dtpToHr = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        Me.dtpFromHr = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnAll = New System.Windows.Forms.RadioButton()
        Me.rbtnNotPaid = New System.Windows.Forms.RadioButton()
        Me.rbtnPaid = New System.Windows.Forms.RadioButton()
        Me.dtpTo = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        Me.dtpFrom = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        Me.btnAfter = New System.Windows.Forms.Button()
        Me.btnBefore = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbtnDeuxMois = New System.Windows.Forms.RadioButton()
        Me.rbtnHebdo = New System.Windows.Forms.RadioButton()
        Me.rbtnBiMensuel = New System.Windows.Forms.RadioButton()
        Me.rbtnMensuelle = New System.Windows.Forms.RadioButton()
        Me.grdBudget = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.btnPay = New System.Windows.Forms.Button()
        Me.btnUnselectAll = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRefresh = New DMS_Application.ctlRefresh()
        Me.formController = New DMS_Application.ctlFormController()
        Me.gbInfos = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.gbFilter.SuspendLayout()
        CType(Me.dtpToHr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToHr.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromHr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromHr.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbInfos.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.dtpToHr)
        Me.gbFilter.Controls.Add(Me.dtpFromHr)
        Me.gbFilter.Controls.Add(Me.GroupBox1)
        Me.gbFilter.Controls.Add(Me.dtpTo)
        Me.gbFilter.Controls.Add(Me.dtpFrom)
        Me.gbFilter.Controls.Add(Me.btnAfter)
        Me.gbFilter.Controls.Add(Me.btnBefore)
        Me.gbFilter.Controls.Add(Me.Label2)
        Me.gbFilter.Controls.Add(Me.Label1)
        Me.gbFilter.Controls.Add(Me.rbtnDeuxMois)
        Me.gbFilter.Controls.Add(Me.rbtnHebdo)
        Me.gbFilter.Controls.Add(Me.rbtnBiMensuel)
        Me.gbFilter.Controls.Add(Me.rbtnMensuelle)
        Me.gbFilter.Location = New System.Drawing.Point(430, 2)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(528, 93)
        Me.gbFilter.TabIndex = 3
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "Filtres"
        '
        'dtpToHr
        '
        Me.dtpToHr.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat
        Me.dtpToHr.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpToHr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.dtpToHr.Calendar.AllowMultipleSelection = False
        Me.dtpToHr.Calendar.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpToHr.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpToHr.Calendar.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpToHr.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpToHr.Calendar.DayNamesFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.dtpToHr.Calendar.DaysFont = New System.Drawing.Font("Verdana", 8.0!)
        Me.dtpToHr.Calendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpToHr.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToHr.Calendar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpToHr.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None
        Me.dtpToHr.Calendar.HeadForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpToHr.Calendar.HeadGradient = True
        Me.dtpToHr.Calendar.HighlightColor = System.Drawing.Color.White
        Me.dtpToHr.Calendar.Iso8601CalenderFormat = False
        Me.dtpToHr.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpToHr.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpToHr.Calendar.Name = "monthCalendar"
        Me.dtpToHr.Calendar.ScrollButtonSize = New System.Drawing.Size(24, 24)
        Me.dtpToHr.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpToHr.Calendar.Size = New System.Drawing.Size(187, 174)
        Me.dtpToHr.Calendar.SizeToFit = True
        Me.dtpToHr.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpToHr.Calendar.TabIndex = 0
        Me.dtpToHr.Calendar.ThemedEnabledScrollButtons = False
        Me.dtpToHr.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpToHr.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpToHr.Calendar.NoneButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpToHr.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpToHr.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpToHr.Calendar.NoneButton.Location = New System.Drawing.Point(111, 0)
        Me.dtpToHr.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpToHr.Calendar.NoneButton.Text = "None"
        Me.dtpToHr.Calendar.NoneButton.UseVisualStyle = True
        Me.dtpToHr.Calendar.NoneButton.Visible = False
        '
        '
        '
        Me.dtpToHr.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpToHr.Calendar.TodayButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpToHr.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpToHr.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpToHr.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpToHr.Calendar.TodayButton.Size = New System.Drawing.Size(187, 20)
        Me.dtpToHr.Calendar.TodayButton.Text = "Today"
        Me.dtpToHr.Calendar.TodayButton.UseVisualStyle = True
        Me.dtpToHr.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpToHr.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpToHr.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpToHr.CustomFormat = "HH:mm"
        Me.dtpToHr.DropDownImage = Nothing
        Me.dtpToHr.DropDownNormalColor = System.Drawing.SystemColors.Control
        Me.dtpToHr.DropDownPressedColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpToHr.DropDownSelectedColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpToHr.EnableNullDate = False
        Me.dtpToHr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToHr.Location = New System.Drawing.Point(129, 47)
        Me.dtpToHr.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpToHr.MinValue = New Date(CType(0, Long))
        Me.dtpToHr.Name = "dtpToHr"
        Me.dtpToHr.ReadOnly = True
        Me.dtpToHr.ShowCheckBox = False
        Me.dtpToHr.ShowDropButton = False
        Me.dtpToHr.Size = New System.Drawing.Size(35, 20)
        Me.dtpToHr.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpToHr.TabIndex = 20
        Me.dtpToHr.TabStop = False
        Me.dtpToHr.Value = New Date(2015, 7, 15, 23, 59, 59, 0)
        '
        'dtpFromHr
        '
        Me.dtpFromHr.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat
        Me.dtpFromHr.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpFromHr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.dtpFromHr.Calendar.AllowMultipleSelection = False
        Me.dtpFromHr.Calendar.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpFromHr.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpFromHr.Calendar.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpFromHr.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFromHr.Calendar.DayNamesFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.dtpFromHr.Calendar.DaysFont = New System.Drawing.Font("Verdana", 8.0!)
        Me.dtpFromHr.Calendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFromHr.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromHr.Calendar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFromHr.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None
        Me.dtpFromHr.Calendar.HeadForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFromHr.Calendar.HeadGradient = True
        Me.dtpFromHr.Calendar.HighlightColor = System.Drawing.Color.White
        Me.dtpFromHr.Calendar.Iso8601CalenderFormat = False
        Me.dtpFromHr.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpFromHr.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFromHr.Calendar.Name = "monthCalendar"
        Me.dtpFromHr.Calendar.ScrollButtonSize = New System.Drawing.Size(24, 24)
        Me.dtpFromHr.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpFromHr.Calendar.Size = New System.Drawing.Size(187, 174)
        Me.dtpFromHr.Calendar.SizeToFit = True
        Me.dtpFromHr.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpFromHr.Calendar.TabIndex = 0
        Me.dtpFromHr.Calendar.ThemedEnabledScrollButtons = False
        Me.dtpFromHr.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpFromHr.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpFromHr.Calendar.NoneButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpFromHr.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpFromHr.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpFromHr.Calendar.NoneButton.Location = New System.Drawing.Point(111, 0)
        Me.dtpFromHr.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpFromHr.Calendar.NoneButton.Text = "None"
        Me.dtpFromHr.Calendar.NoneButton.UseVisualStyle = True
        Me.dtpFromHr.Calendar.NoneButton.Visible = False
        '
        '
        '
        Me.dtpFromHr.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpFromHr.Calendar.TodayButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpFromHr.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpFromHr.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpFromHr.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpFromHr.Calendar.TodayButton.Size = New System.Drawing.Size(187, 20)
        Me.dtpFromHr.Calendar.TodayButton.Text = "Today"
        Me.dtpFromHr.Calendar.TodayButton.UseVisualStyle = True
        Me.dtpFromHr.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpFromHr.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFromHr.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpFromHr.CustomFormat = "HH:mm"
        Me.dtpFromHr.DropDownImage = Nothing
        Me.dtpFromHr.DropDownNormalColor = System.Drawing.SystemColors.Control
        Me.dtpFromHr.DropDownPressedColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFromHr.DropDownSelectedColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpFromHr.EnableNullDate = False
        Me.dtpFromHr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromHr.Location = New System.Drawing.Point(129, 22)
        Me.dtpFromHr.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFromHr.MinValue = New Date(CType(0, Long))
        Me.dtpFromHr.Name = "dtpFromHr"
        Me.dtpFromHr.ReadOnly = True
        Me.dtpFromHr.ShowCheckBox = False
        Me.dtpFromHr.ShowDropButton = False
        Me.dtpFromHr.Size = New System.Drawing.Size(35, 20)
        Me.dtpFromHr.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpFromHr.TabIndex = 19
        Me.dtpFromHr.TabStop = False
        Me.dtpFromHr.Value = New Date(2015, 7, 15, 0, 0, 0, 0)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnAll)
        Me.GroupBox1.Controls.Add(Me.rbtnNotPaid)
        Me.GroupBox1.Controls.Add(Me.rbtnPaid)
        Me.GroupBox1.Location = New System.Drawing.Point(432, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(90, 74)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Statut"
        '
        'rbtnAll
        '
        Me.rbtnAll.AutoSize = True
        Me.rbtnAll.Checked = True
        Me.rbtnAll.Location = New System.Drawing.Point(6, 16)
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(49, 17)
        Me.rbtnAll.TabIndex = 18
        Me.rbtnAll.TabStop = True
        Me.rbtnAll.Text = "Tous"
        Me.rbtnAll.UseVisualStyleBackColor = True
        '
        'rbtnNotPaid
        '
        Me.rbtnNotPaid.AutoSize = True
        Me.rbtnNotPaid.Location = New System.Drawing.Point(6, 34)
        Me.rbtnNotPaid.Name = "rbtnNotPaid"
        Me.rbtnNotPaid.Size = New System.Drawing.Size(71, 17)
        Me.rbtnNotPaid.TabIndex = 17
        Me.rbtnNotPaid.Text = "Non payé"
        Me.rbtnNotPaid.UseVisualStyleBackColor = True
        '
        'rbtnPaid
        '
        Me.rbtnPaid.AutoSize = True
        Me.rbtnPaid.Location = New System.Drawing.Point(6, 52)
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
        Me.dtpTo.Calendar.HeadForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpTo.Calendar.HeadGradient = True
        Me.dtpTo.Calendar.HighlightColor = System.Drawing.Color.White
        Me.dtpTo.Calendar.Iso8601CalenderFormat = False
        Me.dtpTo.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpTo.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.Calendar.Name = "monthCalendar"
        Me.dtpTo.Calendar.ScrollButtonSize = New System.Drawing.Size(24, 24)
        Me.dtpTo.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpTo.Calendar.Size = New System.Drawing.Size(187, 174)
        Me.dtpTo.Calendar.SizeToFit = True
        Me.dtpTo.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpTo.Calendar.TabIndex = 0
        Me.dtpTo.Calendar.ThemedEnabledGrid = True
        Me.dtpTo.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpTo.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpTo.Calendar.NoneButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpTo.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpTo.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpTo.Calendar.NoneButton.Location = New System.Drawing.Point(111, 0)
        Me.dtpTo.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpTo.Calendar.NoneButton.Text = "None"
        Me.dtpTo.Calendar.NoneButton.UseVisualStyle = True
        Me.dtpTo.Calendar.NoneButton.Visible = False
        '
        '
        '
        Me.dtpTo.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpTo.Calendar.TodayButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpTo.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpTo.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpTo.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpTo.Calendar.TodayButton.Size = New System.Drawing.Size(187, 20)
        Me.dtpTo.Calendar.TodayButton.Text = "Today"
        Me.dtpTo.Calendar.TodayButton.UseVisualStyle = True
        Me.dtpTo.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpTo.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpTo.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpTo.DropDownImage = Nothing
        Me.dtpTo.DropDownNormalColor = System.Drawing.SystemColors.Control
        Me.dtpTo.DropDownPressedColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.DropDownSelectedColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpTo.EnableNullDate = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(39, 47)
        Me.dtpTo.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpTo.MinValue = New Date(CType(0, Long))
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.NoneButtonVisible = False
        Me.dtpTo.ShowCheckBox = False
        Me.dtpTo.Size = New System.Drawing.Size(90, 20)
        Me.dtpTo.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpTo.TabIndex = 15
        Me.dtpTo.ThemedChildControls = True
        Me.dtpTo.ThemesEnabled = True
        Me.dtpTo.UseCustomScrollerFrame = True
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
        Me.dtpFrom.Calendar.HeadForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFrom.Calendar.HeadGradient = True
        Me.dtpFrom.Calendar.HighlightColor = System.Drawing.Color.White
        Me.dtpFrom.Calendar.Iso8601CalenderFormat = False
        Me.dtpFrom.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpFrom.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.Calendar.Name = "monthCalendar"
        Me.dtpFrom.Calendar.ScrollButtonSize = New System.Drawing.Size(24, 24)
        Me.dtpFrom.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpFrom.Calendar.Size = New System.Drawing.Size(187, 174)
        Me.dtpFrom.Calendar.SizeToFit = True
        Me.dtpFrom.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpFrom.Calendar.TabIndex = 0
        Me.dtpFrom.Calendar.ThemedEnabledGrid = True
        Me.dtpFrom.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpFrom.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpFrom.Calendar.NoneButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpFrom.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpFrom.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpFrom.Calendar.NoneButton.Location = New System.Drawing.Point(111, 0)
        Me.dtpFrom.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpFrom.Calendar.NoneButton.Text = "None"
        Me.dtpFrom.Calendar.NoneButton.UseVisualStyle = True
        Me.dtpFrom.Calendar.NoneButton.Visible = False
        '
        '
        '
        Me.dtpFrom.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2010
        Me.dtpFrom.Calendar.TodayButton.BackColor = System.Drawing.SystemColors.Window
        Me.dtpFrom.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpFrom.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpFrom.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpFrom.Calendar.TodayButton.Size = New System.Drawing.Size(187, 20)
        Me.dtpFrom.Calendar.TodayButton.Text = "Today"
        Me.dtpFrom.Calendar.TodayButton.UseVisualStyle = True
        Me.dtpFrom.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFrom.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpFrom.DropDownImage = Nothing
        Me.dtpFrom.DropDownNormalColor = System.Drawing.SystemColors.Control
        Me.dtpFrom.DropDownPressedColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.DropDownSelectedColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpFrom.EnableNullDate = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(39, 22)
        Me.dtpFrom.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpFrom.MinValue = New Date(CType(0, Long))
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.NoneButtonVisible = False
        Me.dtpFrom.ShowCheckBox = False
        Me.dtpFrom.Size = New System.Drawing.Size(90, 20)
        Me.dtpFrom.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010
        Me.dtpFrom.TabIndex = 11
        Me.dtpFrom.ThemedChildControls = True
        Me.dtpFrom.ThemesEnabled = True
        Me.dtpFrom.Value = New Date(2015, 7, 15, 0, 0, 0, 0)
        '
        'btnAfter
        '
        Me.btnAfter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAfter.Location = New System.Drawing.Point(170, 47)
        Me.btnAfter.Name = "btnAfter"
        Me.btnAfter.Size = New System.Drawing.Size(23, 20)
        Me.btnAfter.TabIndex = 14
        Me.btnAfter.UseVisualStyleBackColor = True
        '
        'btnBefore
        '
        Me.btnBefore.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBefore.Location = New System.Drawing.Point(170, 22)
        Me.btnBefore.Name = "btnBefore"
        Me.btnBefore.Size = New System.Drawing.Size(23, 20)
        Me.btnBefore.TabIndex = 13
        Me.btnBefore.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBefore.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnBefore.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Au:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Du:"
        '
        'rbtnDeuxMois
        '
        Me.rbtnDeuxMois.AutoSize = True
        Me.rbtnDeuxMois.Location = New System.Drawing.Point(299, 47)
        Me.rbtnDeuxMois.Name = "rbtnDeuxMois"
        Me.rbtnDeuxMois.Size = New System.Drawing.Size(71, 17)
        Me.rbtnDeuxMois.TabIndex = 4
        Me.rbtnDeuxMois.Text = "Au 2 mois"
        Me.rbtnDeuxMois.UseVisualStyleBackColor = True
        '
        'rbtnHebdo
        '
        Me.rbtnHebdo.AutoSize = True
        Me.rbtnHebdo.Location = New System.Drawing.Point(199, 22)
        Me.rbtnHebdo.Name = "rbtnHebdo"
        Me.rbtnHebdo.Size = New System.Drawing.Size(94, 17)
        Me.rbtnHebdo.TabIndex = 3
        Me.rbtnHebdo.Text = "Hebdomadaire"
        Me.rbtnHebdo.UseVisualStyleBackColor = True
        '
        'rbtnBiMensuel
        '
        Me.rbtnBiMensuel.AutoSize = True
        Me.rbtnBiMensuel.Location = New System.Drawing.Point(199, 47)
        Me.rbtnBiMensuel.Name = "rbtnBiMensuel"
        Me.rbtnBiMensuel.Size = New System.Drawing.Size(81, 17)
        Me.rbtnBiMensuel.TabIndex = 2
        Me.rbtnBiMensuel.Text = "Bimensuelle"
        Me.rbtnBiMensuel.UseVisualStyleBackColor = True
        '
        'rbtnMensuelle
        '
        Me.rbtnMensuelle.AutoSize = True
        Me.rbtnMensuelle.Checked = True
        Me.rbtnMensuelle.Location = New System.Drawing.Point(299, 22)
        Me.rbtnMensuelle.Name = "rbtnMensuelle"
        Me.rbtnMensuelle.Size = New System.Drawing.Size(73, 17)
        Me.rbtnMensuelle.TabIndex = 1
        Me.rbtnMensuelle.TabStop = True
        Me.rbtnMensuelle.Text = "Mensuelle"
        Me.rbtnMensuelle.UseVisualStyleBackColor = True
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
        Me.grdBudget.Location = New System.Drawing.Point(4, 101)
        Me.grdBudget.Name = "grdBudget"
        Me.grdBudget.RowHeightEntries.AddRange(New Syncfusion.Windows.Forms.Grid.GridRowHeight() {New Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 25)})
        Me.grdBudget.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdBudget.Size = New System.Drawing.Size(954, 610)
        Me.grdBudget.SmartSizeBox = False
        Me.grdBudget.TabIndex = 5
        Me.grdBudget.Tag = "11"
        Me.grdBudget.Text = "GridControl1"
        Me.grdBudget.UseRightToLeftCompatibleTextBox = True
        '
        'btnPay
        '
        Me.btnPay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPay.BackgroundImage = CType(resources.GetObject("btnPay.BackgroundImage"), System.Drawing.Image)
        Me.btnPay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPay.Location = New System.Drawing.Point(963, 183)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.Size = New System.Drawing.Size(40, 40)
        Me.btnPay.TabIndex = 11
        Me.ToolTip.SetToolTip(Me.btnPay, "Payer les factures sélectionnées")
        Me.btnPay.UseVisualStyleBackColor = True
        '
        'btnUnselectAll
        '
        Me.btnUnselectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUnselectAll.BackColor = System.Drawing.SystemColors.Control
        Me.btnUnselectAll.Image = CType(resources.GetObject("btnUnselectAll.Image"), System.Drawing.Image)
        Me.btnUnselectAll.Location = New System.Drawing.Point(970, 132)
        Me.btnUnselectAll.Name = "btnUnselectAll"
        Me.btnUnselectAll.Size = New System.Drawing.Size(25, 25)
        Me.btnUnselectAll.TabIndex = 16
        Me.ToolTip.SetToolTip(Me.btnUnselectAll, "Désélectionner toutes les dépenses d'une période")
        Me.btnUnselectAll.UseVisualStyleBackColor = False
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectAll.Image = CType(resources.GetObject("btnSelectAll.Image"), System.Drawing.Image)
        Me.btnSelectAll.Location = New System.Drawing.Point(970, 101)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(25, 25)
        Me.btnSelectAll.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.btnSelectAll, "Sélectionner toutes les dépenses d'une période")
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(963, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(40, 40)
        Me.btnRefresh.TabIndex = 17
        '
        'formController
        '
        Me.formController.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formController.FormIsLoading = False
        Me.formController.FormMode = DMS_Application.mConstants.Form_Mode.CONSULT_MODE
        Me.formController.Item_ID = 0
        Me.formController.Location = New System.Drawing.Point(4, 714)
        Me.formController.Name = "formController"
        Me.formController.ShowButtonQuitOnly = False
        Me.formController.Size = New System.Drawing.Size(1003, 33)
        Me.formController.TabIndex = 4
        '
        'gbInfos
        '
        Me.gbInfos.Controls.Add(Me.txtName)
        Me.gbInfos.Controls.Add(Me.Label3)
        Me.gbInfos.Location = New System.Drawing.Point(4, 5)
        Me.gbInfos.Name = "gbInfos"
        Me.gbInfos.Size = New System.Drawing.Size(420, 90)
        Me.gbInfos.TabIndex = 18
        Me.gbInfos.TabStop = False
        Me.gbInfos.Text = "Informations"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(50, 20)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(247, 20)
        Me.txtName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 16)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Nom:"
        '
        'frmBudgetManagement
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1008, 747)
        Me.Controls.Add(Me.gbInfos)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnUnselectAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.btnPay)
        Me.Controls.Add(Me.grdBudget)
        Me.Controls.Add(Me.gbFilter)
        Me.Controls.Add(Me.formController)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmBudgetManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "28"
        Me.Text = "Budget"
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        CType(Me.dtpToHr.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToHr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromHr.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromHr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dtpTo.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBudget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbInfos.ResumeLayout(False)
        Me.gbInfos.PerformLayout()
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
    Friend WithEvents btnAfter As System.Windows.Forms.Button
    Friend WithEvents btnBefore As System.Windows.Forms.Button
    Friend WithEvents dtpFrom As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv
    Friend WithEvents dtpTo As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv
    Friend WithEvents btnPay As System.Windows.Forms.Button
    Friend WithEvents rbtnNotPaid As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPaid As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToHr As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv
    Friend WithEvents dtpFromHr As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv
    Friend WithEvents btnUnselectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents rbtnAll As System.Windows.Forms.RadioButton
    Friend WithEvents btnRefresh As DMS_Application.ctlRefresh
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents gbInfos As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
