Imports SourceGrid
Imports FlexCell

Public Class test

    Dim lol As New SourceGrid.Grid



    Private Sub test_Activated(sender As Object, e As EventArgs) Handles Button1.Click
        grid1.BorderStyle = BorderStyle.Fixed3D
        grid1.ColumnsCount = 10
        grid1.FixedRows = 1
        grid1.Rows.Insert(0)

        grid1.Width = 521

        Dim cbEditor As SourceGrid.Cells.Editors.ComboBox = New SourceGrid.Cells.Editors.ComboBox(GetType(System.String))

        cbEditor.StandardValues = New String() {"Value 1", "Value 2", "Value 3"}
        cbEditor.EditableMode = (SourceGrid.EditableMode.Focus _
                    Or (SourceGrid.EditableMode.SingleClick Or SourceGrid.EditableMode.AnyKey))
        grid1(0, 0) = New SourceGrid.Cells.ColumnHeader("String")
        grid1(0, 1) = New SourceGrid.Cells.ColumnHeader("DateTime")
        grid1(0, 2) = New SourceGrid.Cells.ColumnHeader("CheckBox")
        grid1(0, 3) = New SourceGrid.Cells.ColumnHeader("ComboBox")

        Dim r As Integer = 1

        Do While (r < 10)
            grid1.Rows.Insert(r)
            grid1(r, 0) = New SourceGrid.Cells.Cell(("Hello " + r.ToString), GetType(System.String))
            grid1(r, 1) = New SourceGrid.Cells.Cell(DateTime.Today, GetType(DateTime))
            grid1(r, 2) = New SourceGrid.Cells.CheckBox(Nothing, True)
            grid1(r, 3) = New SourceGrid.Cells.Cell("Value 1", cbEditor)
            grid1(r, 3).View = SourceGrid.Cells.Views.ComboBox.Default
            r = (r + 1)
        Loop

        ' grid1.AutoSizeCells()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With Grid2
            .AutoRedraw = False

            .Cols = 7
            .DefaultFont = New Font("Tahoma", 8)
            .DisplayFocusRect = False
            .DisplayDateTimeMask = True
            .ExtendLastCol = True
            .DrawMode = FlexCell.DrawModeEnum.OwnerDraw
            .BorderStyle = FlexCell.BorderStyleEnum.FixedSingle
            .FixedRowColStyle = FlexCell.FixedRowColStyleEnum.Flat
            .AllowUserPaste = FlexCell.ClipboardDataEnum.Text

            '.CheckedImage = New Bitmap(MyBase.GetType().Assembly.GetManifestResourceStream("VBDemo.Checked.bmp"))
            '.UncheckedImage = Nothing

            .BackColorFixed = Color.FromArgb(90, 158, 214)
            .BackColorFixedSel = Color.FromArgb(110, 180, 230)
            .BackColorBkg = Color.FromArgb(90, 158, 214)
            .BackColor1 = Color.FromArgb(231, 235, 247)
            .BackColor2 = Color.FromArgb(239, 243, 255)
            .CellBorderColorFixed = Color.Black
            .GridColor = Color.FromArgb(148, 190, 231)

            .Cell(0, 1).Text = "TextBox"
            .Cell(0, 2).Text = "ComboBox"
            .Cell(0, 3).Text = "CheckBox"
            .Cell(0, 4).Text = "Calendar"
            .Cell(0, 5).Text = "Button"
            .Cell(0, 6).Text = "HyperLink"

            .Column(1).CellType = FlexCell.CellTypeEnum.TextBox
            .Column(2).CellType = FlexCell.CellTypeEnum.ComboBox
            .Column(3).CellType = FlexCell.CellTypeEnum.CheckBox
            .Column(4).CellType = FlexCell.CellTypeEnum.Calendar
            .Column(5).CellType = FlexCell.CellTypeEnum.Button
            .Column(6).CellType = FlexCell.CellTypeEnum.HyperLink

            .Column(0).Visible = False
            .Column(1).Width = 100
            .Column(2).Width = 100
            .Column(3).Width = 70
            .Column(4).Width = 90
            .Column(5).Width = 100
            .Column(6).Width = 140

            .AutoRedraw = True
            .Refresh()
        End With

        Grid2.Column(2).MaxLength = 4
        With Grid2.ComboBox(2)
            '.Locked = True
            .DropDownFont = New Font("Courier New", 9)
            .Items.Add("AAAA - 001")
            .Items.Add("AAAB - 002")
            .Items.Add("ABCC - 003")
            .Items.Add("ABCD - 004")
            .Items.Add("BAAA - 005")
            .Items.Add("BBCC - 006")
            .Items.Add("CABC - 007")
            .Items.Add("CABB - 008")
            .Items.Add("CBAA - 009")
            .Items.Add("DABC - 010")
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lol As New C1FlexGridController()

        lol.bln_Init(C1FlexGrid1)



        C1FlexGrid1.Rows.Add(10)
    End Sub

End Class