<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlDateTimePicker
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.dtpAdv = New Syncfusion.Windows.Forms.Tools.DateTimePickerAdv()
        CType(Me.dtpAdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAdv.Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpAdv
        '
        Me.dtpAdv.BorderColor = System.Drawing.Color.Empty
        '
        '
        '
        Me.dtpAdv.Calendar.AllowMultipleSelection = False
        Me.dtpAdv.Calendar.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.dtpAdv.Calendar.Culture = New System.Globalization.CultureInfo("en-US")
        Me.dtpAdv.Calendar.DaysFont = New System.Drawing.Font("Verdana", 8.0!)
        Me.dtpAdv.Calendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpAdv.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAdv.Calendar.HeaderFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAdv.Calendar.Iso8601CalenderFormat = False
        Me.dtpAdv.Calendar.Location = New System.Drawing.Point(0, 0)
        Me.dtpAdv.Calendar.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpAdv.Calendar.Name = "monthCalendar"
        Me.dtpAdv.Calendar.SelectedDates = New Date(-1) {}
        Me.dtpAdv.Calendar.Size = New System.Drawing.Size(206, 174)
        Me.dtpAdv.Calendar.SizeToFit = True
        Me.dtpAdv.Calendar.TabIndex = 0
        Me.dtpAdv.Calendar.WeekFont = New System.Drawing.Font("Verdana", 8.0!)
        '
        '
        '
        Me.dtpAdv.Calendar.NoneButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpAdv.Calendar.NoneButton.IsBackStageButton = False
        Me.dtpAdv.Calendar.NoneButton.Location = New System.Drawing.Point(134, 0)
        Me.dtpAdv.Calendar.NoneButton.Size = New System.Drawing.Size(72, 20)
        Me.dtpAdv.Calendar.NoneButton.Text = "None"
        '
        '
        '
        Me.dtpAdv.Calendar.TodayButton.BeforeTouchSize = New System.Drawing.Size(75, 23)
        Me.dtpAdv.Calendar.TodayButton.IsBackStageButton = False
        Me.dtpAdv.Calendar.TodayButton.Location = New System.Drawing.Point(0, 0)
        Me.dtpAdv.Calendar.TodayButton.Size = New System.Drawing.Size(134, 20)
        Me.dtpAdv.Calendar.TodayButton.Text = "Today"
        Me.dtpAdv.CalendarSize = New System.Drawing.Size(189, 176)
        Me.dtpAdv.DropDownImage = Nothing
        Me.dtpAdv.Location = New System.Drawing.Point(0, 0)
        Me.dtpAdv.MetroColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.dtpAdv.MinValue = New Date(CType(0, Long))
        Me.dtpAdv.Name = "dtpAdv"
        Me.dtpAdv.Size = New System.Drawing.Size(189, 20)
        Me.dtpAdv.TabIndex = 0
        Me.dtpAdv.Value = New Date(2015, 7, 15, 22, 32, 10, 936)
        '
        'ctlDateTimePicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dtpAdv)
        Me.Name = "ctlDateTimePicker"
        Me.Size = New System.Drawing.Size(189, 20)
        CType(Me.dtpAdv.Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAdv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpAdv As Syncfusion.Windows.Forms.Tools.DateTimePickerAdv

End Class
