Imports FlexCell
Imports System.Collections.Specialized

Public Class test


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
        sqlCmd = New MySqlCommand(strSQL, gcAppController.MySQLConnection)

        mySQLReader = sqlCmd.ExecuteReader

        myDataTable.Load(mySQLReader)

        dataTableArray = New Object(myDataTable.Rows.Count - 1, myDataTable.Columns.Count) {}

        strGridCaption = gcAppController.str_GetCaption(9, gcAppController.cUser.GetLanguage)

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

        grdSync.Cols.InsertRange(2, 5)

        Dim items As New StringCollection()
        items.Add("One")
        items.Add("Two")
        items.Add("Three")
        items.Add("Four")
        items.Add("Five")

        grdSync.ColStyles(4).CellType = "ComboBox"
        'grdSync(1, 4).DataSource = items
        grdSync.ColStyles(4).DataSource = items

        grdSync.EndUpdate()

        grdSync.Refresh()

    End Sub

    Private dataTableArray(,) As Object


    Private Sub setVisualStyle()
        Dim strGridCaption As String
        Dim lstColumns As String()
        Dim individualColStyle As GridStyleInfo

        strGridCaption = gcAppController.str_GetCaption(8, gcAppController.cUser.GetLanguage)

        lstColumns = Split(strGridCaption.Insert(0, "|"), "|")

        'Definition of columns
        For colHeaderCpt As Integer = 1 To lstColumns.Count - 1

            individualColStyle = New GridStyleInfo
            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

            If lstColumns(colHeaderCpt) = String.Empty Then
                grdSync.SetColHidden(colHeaderCpt, colHeaderCpt, True)
            Else

                grdSync(0, colHeaderCpt).Text = Microsoft.VisualBasic.Right(lstColumns(colHeaderCpt), lstColumns(colHeaderCpt).Length - 1)

                Select Case lstColumns(colHeaderCpt).Chars(0)
                    Case CChar("<")
                        individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Left


                    Case CChar("^")
                        individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

                    Case CChar(">")
                        individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Right

                End Select

                grdSync.ChangeCells(GridRangeInfo.Cells(0, colHeaderCpt, grdSync.RowCount, colHeaderCpt), individualColStyle)
            End If

        Next
    End Sub

    Private Sub getDataFromBD()
        Dim sqlCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myDataTable As DataTable = New DataTable
        Dim strSQL As String = String.Empty
        Dim gridController As New SyncfusionGridController

        gridController.bln_Init(grdSync)

        strSQL = strSQL & "  SELECT Company.Cy_ID, " & vbCrLf
        strSQL = strSQL & "         Company.Cy_Name " & vbCrLf
        strSQL = strSQL & "  FROM Company " & vbCrLf
        strSQL = strSQL & "  ORDER BY Company.Cy_Name " & vbCrLf

        sqlCmd = New MySqlCommand(strSQL, gcAppController.MySQLConnection)

        mySQLReader = sqlCmd.ExecuteReader

        myDataTable.Load(mySQLReader)

        dataTableArray = New Object(myDataTable.Rows.Count - 1, myDataTable.Columns.Count) {}

        For intRowIndex As Integer = 0 To myDataTable.Rows.Count - 1

            For intColIndex As Integer = 0 To myDataTable.Columns.Count - 1

                dataTableArray(intRowIndex, intColIndex) = myDataTable.Rows(intRowIndex)(intColIndex)
            Next
        Next

        numArrayCols = myDataTable.Columns.Count
        numArrayRows = myDataTable.Rows.Count
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Populate.Click

        Dim gridController As New SyncfusionGridController

        gridController.bln_Init(grdSync)

        SetUpArray()
        grdSync.ResetVolatileData()
        grdSync.Refresh()

        Me.Cursor = Cursors.WaitCursor

        grdSync.BeginUpdate()
        grdSync.RowCount = Me.numArrayRows
        grdSync.ColCount = Me.numArrayCols

        grdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, Me.numArrayRows, Me.numArrayCols), intArray)
        grdSync.EndUpdate()
        Refresh()

        Me.Cursor = Cursors.Arrow

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim gridCtrl As New SyncfusionGridController

        getDataFromBD()

        grdSync.ResetVolatileData()
        grdSync.Refresh()
        grdSync.BeginUpdate()

        grdSync.RowCount = Me.numArrayRows
        grdSync.ColCount = Me.numArrayCols

        grdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, Me.numArrayRows, Me.numArrayCols), dataTableArray)

        setVisualStyle()

        grdSync.EndUpdate()
        Refresh()
    End Sub

    Private intArray(,) As Integer

    Private numArrayCols As Integer = 5
    Private numArrayRows As Integer = 5

    Private Function SetUpArray() As Integer(,)

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