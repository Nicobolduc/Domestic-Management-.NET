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
        Dim lol As New SyncfusionGridController()

        'lol.bln_Init(C1FlexGrid1)

        C1FlexGrid1.Rows.Add(10)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sqlCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myDataTable As DataTable = New DataTable
        Dim strSQL As String
        Dim strGridCaption As String
        Dim dataTableArray(,) As Object
        Dim gridController As New SyncfusionGridController

        Dim headerStyle As GridStyleInfo
        Dim individualColStyle As GridStyleInfo
        Dim lstColumns As String()

        gridController.bln_Init(grdSync)

        strSQL = "  SELECT " & DataGridViewController.GridRowActions.CONSULT_ACTION & " AS Action, " & vbCrLf
        strSQL = strSQL & "         ProductPrice.ProP_ID, " & vbCrLf
        strSQL = strSQL & "         Company.Cy_ID, " & vbCrLf
        strSQL = strSQL & "         Company.Cy_Name, " & vbCrLf
        strSQL = strSQL & "         ProductBrand.ProB_ID, " & vbCrLf
        strSQL = strSQL & "         ProductBrand.ProB_Name, " & vbCrLf
        strSQL = strSQL & "         ProductPrice.ProP_Price " & vbCrLf
        strSQL = strSQL & "  FROM ProductPrice " & vbCrLf
        strSQL = strSQL & "     INNER JOIN Company ON Company.Cy_ID = ProductPrice.Cy_ID_Seller " & vbCrLf
        strSQL = strSQL & "     INNER JOIN ProductBrand ON ProductBrand.ProB_ID = ProductPrice.ProB_ID " & vbCrLf
        strSQL = strSQL & "  WHERE ProductPrice.Pro_ID = " & 42 & vbCrLf
        strSQL = strSQL & "  ORDER BY Company.Cy_name " & vbCrLf
        'Get the grid data
        sqlCmd = New MySqlCommand(strSQL, gcAppControler.MySQLConnection)

        mySQLReader = sqlCmd.ExecuteReader

        myDataTable.Load(mySQLReader)

        dataTableArray = New Object(myDataTable.Rows.Count - 1, myDataTable.Columns.Count) {}

        strGridCaption = gcAppControler.str_GetCaption(9, gcAppControler.cUser.GetLanguage)

        lstColumns = Split(strGridCaption.Insert(0, "|"), "|")

        'Reset the grid
        grdSync.ResetVolatileData()
        grdSync.RowCount = myDataTable.Rows.Count
        grdSync.ColCount = lstColumns.Count - 1

        'Definition of columns
        For colHeaderCpt As Integer = 1 To lstColumns.Count - 1

            'grdSync.ColCount += 1

            individualColStyle = New GridStyleInfo
            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

            If lstColumns(colHeaderCpt) = String.Empty Then
                grdSync.SetColHidden(colHeaderCpt, colHeaderCpt, True)
            Else

                grdSync(0, colHeaderCpt).Text = Microsoft.VisualBasic.Right(lstColumns(colHeaderCpt), lstColumns(colHeaderCpt).Length - 1)

                Select Case lstColumns(colHeaderCpt).Chars(0)
                    Case CChar("<")
                        'grdSync(0, colHeaderCpt).HorizontalAlignment = GridHorizontalAlignment.Left
                        individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Left


                    Case CChar("^")
                        individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center
                        'grdSync(0, colHeaderCpt).HorizontalAlignment = GridHorizontalAlignment.Center

                    Case CChar(">")
                        individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Right
                        'grdSync(0, colHeaderCpt).HorizontalAlignment = GridHorizontalAlignment.Right

                End Select

                grdSync.ChangeCells(GridRangeInfo.Cells(0, colHeaderCpt, grdSync.RowCount, colHeaderCpt), individualColStyle)
            End If

        Next
        
        'Set the grid data
        grdSync.BeginUpdate()

        For intRowIndex As Integer = 0 To myDataTable.Rows.Count - 1

            For intColIndex As Integer = 0 To myDataTable.Columns.Count - 1

                dataTableArray(intRowIndex, intColIndex) = myDataTable.Rows(intRowIndex)(intColIndex)
            Next
        Next

        grdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, myDataTable.Rows.Count, myDataTable.Columns.Count), dataTableArray)

        'Definition of visual styles
        headerStyle = New GridStyleInfo

        headerStyle.BackColor = Color.WhiteSmoke

        grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, 0, grdSync.ColCount), headerStyle, Syncfusion.Styles.StyleModifyType.ApplyNew)
        grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, grdSync.RowCount, 0), headerStyle)

        'grdSync.Item(1, 0).CellType = Syncfusion.Windows.Forms.Grid.GridCellTypeName.CheckBox

        grdSync.EndUpdate()

        grdSync.Refresh()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim strSQL As String = String.Empty
        Dim gridControlelr As New SyncfusionGridController

        gridControlelr.bln_Init(grdSync)

        strSQL = "  SELECT " & DataGridViewController.GridRowActions.CONSULT_ACTION & " AS Action, " & vbCrLf
        strSQL = strSQL & "         ProductPrice.ProP_ID, " & vbCrLf
        strSQL = strSQL & "         Company.Cy_ID, " & vbCrLf
        strSQL = strSQL & "         Company.Cy_Name, " & vbCrLf
        strSQL = strSQL & "         ProductBrand.ProB_ID, " & vbCrLf
        strSQL = strSQL & "         ProductBrand.ProB_Name, " & vbCrLf
        strSQL = strSQL & "         ProductPrice.ProP_Price " & vbCrLf
        strSQL = strSQL & "  FROM ProductPrice " & vbCrLf
        strSQL = strSQL & "     INNER JOIN Company ON Company.Cy_ID = ProductPrice.Cy_ID_Seller " & vbCrLf
        strSQL = strSQL & "     INNER JOIN ProductBrand ON ProductBrand.ProB_ID = ProductPrice.ProB_ID " & vbCrLf
        strSQL = strSQL & "  WHERE ProductPrice.Pro_ID = " & 42 & vbCrLf
        strSQL = strSQL & "  ORDER BY Company.Cy_name " & vbCrLf

        gridControlelr.bln_FillData(strSQL)
    End Sub

    Private Function getArray() As Integer(,)
        Dim numArrayCols As Integer = 7
        Dim numArrayRows As Integer = 1
        Dim intArray(,) As Integer
        Dim r As Random = New Random
        Dim i As Integer = 0

        intArray = New Integer((numArrayRows) - 1, numArrayCols) {}

        Do While (i < numArrayRows)
            Dim j As Integer = 0
            Do While (j < numArrayCols)
                intArray(i, j) = r.Next(10000)
                j = (j + 1)
            Loop
            i = (i + 1)
        Loop

        Return intArray
    End Function
End Class