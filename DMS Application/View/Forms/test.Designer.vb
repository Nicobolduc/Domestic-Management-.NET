﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.Grid2 = New FlexCell.Grid()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.grdSync = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.Populate = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.grdSync_2 = New Syncfusion.Windows.Forms.Grid.GridControl()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.grdSync, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSync_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Grid2
        '
        Me.Grid2.CheckedImage = Nothing
        Me.Grid2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grid2.GridColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Grid2.Location = New System.Drawing.Point(524, 429)
        Me.Grid2.Name = "Grid2"
        Me.Grid2.Size = New System.Drawing.Size(562, 378)
        Me.Grid2.TabIndex = 5
        Me.Grid2.UncheckedImage = Nothing
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1092, 429)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(66, 40)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "FlexCell"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(1092, 12)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(66, 40)
        Me.Button4.TabIndex = 10
        Me.Button4.Text = "TEst 1"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'grdSync
        '
        Me.grdSync.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdSync.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue
        Me.grdSync.Location = New System.Drawing.Point(12, 12)
        Me.grdSync.Name = "grdSync"
        Me.grdSync.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdSync.Size = New System.Drawing.Size(1074, 391)
        Me.grdSync.SmartSizeBox = False
        Me.grdSync.TabIndex = 11
        Me.grdSync.Tag = "9"
        Me.grdSync.Text = "GridControl1"
        Me.grdSync.UseRightToLeftCompatibleTextBox = True
        '
        'Populate
        '
        Me.Populate.Location = New System.Drawing.Point(1092, 58)
        Me.Populate.Name = "Populate"
        Me.Populate.Size = New System.Drawing.Size(66, 40)
        Me.Populate.TabIndex = 12
        Me.Populate.Text = "Feed Test"
        Me.Populate.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1092, 104)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 40)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Feed DB"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'grdSync_2
        '
        Me.grdSync_2.Location = New System.Drawing.Point(12, 429)
        Me.grdSync_2.Name = "grdSync_2"
        Me.grdSync_2.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode
        Me.grdSync_2.Size = New System.Drawing.Size(345, 168)
        Me.grdSync_2.SmartSizeBox = False
        Me.grdSync_2.TabIndex = 14
        Me.grdSync_2.Text = "GridControl1"
        Me.grdSync_2.UseRightToLeftCompatibleTextBox = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(363, 429)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(84, 40)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "Test"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1178, 819)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.grdSync_2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Populate)
        Me.Controls.Add(Me.grdSync)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Grid2)
        Me.Name = "test"
        Me.Text = "test"
        CType(Me.grdSync, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSync_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Grid2 As FlexCell.Grid
    Friend WithEvents Button2 As System.Windows.Forms.Button
    'Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents grdSync As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents Populate As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents grdSync_2 As Syncfusion.Windows.Forms.Grid.GridControl
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
