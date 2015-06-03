<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class test
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
        Me.grid1 = New SourceGrid.Grid()
        Me.Grid2 = New FlexCell.Grid()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grid1
        '
        Me.grid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grid1.AutoStretchColumnsToFitWidth = True
        Me.grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grid1.ClipboardMode = SourceGrid.ClipboardMode.Copy
        Me.grid1.EnableSort = True
        Me.grid1.Location = New System.Drawing.Point(4, 3)
        Me.grid1.Name = "grid1"
        Me.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows
        Me.grid1.SelectionMode = SourceGrid.GridSelectionMode.Row
        Me.grid1.Size = New System.Drawing.Size(521, 378)
        Me.grid1.TabIndex = 1
        Me.grid1.TabStop = True
        Me.grid1.ToolTipText = ""
        '
        'Grid2
        '
        Me.Grid2.CheckedImage = Nothing
        Me.Grid2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grid2.GridColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Grid2.Location = New System.Drawing.Point(524, 3)
        Me.Grid2.Name = "Grid2"
        Me.Grid2.Size = New System.Drawing.Size(562, 378)
        Me.Grid2.TabIndex = 5
        Me.Grid2.UncheckedImage = Nothing
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1100, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 40)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "SourceGrid"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1100, 67)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(66, 40)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "FlexCell"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.C1FlexGrid1.Location = New System.Drawing.Point(4, 387)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.DefaultSize = 19
        Me.C1FlexGrid1.Size = New System.Drawing.Size(521, 420)
        Me.C1FlexGrid1.TabIndex = 7
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(540, 387)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(66, 40)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "C1 Grid"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1178, 819)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.C1FlexGrid1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Grid2)
        Me.Controls.Add(Me.grid1)
        Me.Name = "test"
        Me.Text = "test"
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grid1 As SourceGrid.Grid
    Friend WithEvents Grid2 As FlexCell.Grid
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
