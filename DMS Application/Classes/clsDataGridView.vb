Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Class clsDataGridView

    'Private members
    Private Const mintDefaultActionCol As Short = 0

    'Private class members
    Private WithEvents PrintDoc As New PrintDocument()
    Private WithEvents PrintPrevDialog As New PrintPreviewDialog()
    Private WithEvents grdGrid As DataGridView

    'Public events
    Public Event SetDisplay()
    Public Event SaveGridData()

    'Public enum
    Public Enum GridRowActions
        CONSULT_ACTION = 0
        INSERT_ACTION = 1
        UPDATE_ACTION = 2
        DELETE_ACTION = 3
    End Enum


#Region "Functions / Subs"

    Public Function bln_Init(ByRef rgrdGrid As DataGridView, Optional ByRef rbtnAddLine As Button = Nothing, Optional ByRef rbtnRemoveLine As Button = Nothing) As Boolean
        Dim blnReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle

        Try
            grdGrid = rgrdGrid

            grdGrid.AutoSizeColumnsMode = CType(DataGridViewAutoSizeColumnMode.Fill, DataGridViewAutoSizeColumnsMode)

            grdGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            grdGrid.AutoGenerateColumns = False
            grdGrid.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control
            grdGrid.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Control
            grdGrid.RowHeadersWidth = 8
            grdGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            columnsHeaderStyle.Font = New Font(grdGrid.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)
            grdGrid.ColumnHeadersDefaultCellStyle = columnsHeaderStyle
            grdGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            grdGrid.AllowUserToResizeRows = False

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_FillData(ByVal vstrSQL As String) As Boolean
        Dim blnReturn As Boolean = True
        Dim sqlCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myDataTable As DataTable = New DataTable
        Dim strGridCaption As String = vbNullString
        Dim lstColumns As String()
        Dim newDGVCol As DataGridViewColumn
        Dim newDGVCell As DataGridViewCell

        Try
            grdGrid.Rows.Clear()
            grdGrid.Columns.Clear()

            strGridCaption = gcApplication.str_GetCaption(CInt(grdGrid.Tag), 1)

            lstColumns = Split(strGridCaption, "|")

            sqlCmd = New MySqlCommand(vstrSQL, gcApplication.MySQLConnection)

            mySQLReader = sqlCmd.ExecuteReader

            myDataTable.Load(mySQLReader)

            For intColIndex As Integer = 0 To myDataTable.Columns.Count - 1
                newDGVCol = New DataGridViewColumn()

                If myDataTable.Columns(intColIndex).DataType.Name = "Byte" Then
                    newDGVCell = New DataGridViewCheckBoxCell()
                Else
                    newDGVCell = New DataGridViewTextBoxCell()
                End If

                newDGVCol.DataPropertyName = myDataTable.Columns(intColIndex).ColumnName
                newDGVCol.CellTemplate = newDGVCell
                newDGVCol.Name = myDataTable.Columns(intColIndex).ColumnName

                grdGrid.Columns.Add(newDGVCol)

                If lstColumns(intColIndex) = vbNullString Then
                    grdGrid.Columns(intColIndex).Visible = False
                Else

                    grdGrid.Columns(intColIndex).HeaderText = Right(lstColumns(intColIndex), lstColumns(intColIndex).Length - 1)

                    Select Case lstColumns(intColIndex).Chars(0)
                        Case CChar("<")
                            grdGrid.Columns(intColIndex).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                            grdGrid.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Case CChar("^")
                            grdGrid.Columns(intColIndex).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            grdGrid.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        Case CChar(">")
                            grdGrid.Columns(intColIndex).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                            grdGrid.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    End Select
                End If
            Next

            For intRowIndex As Integer = 0 To myDataTable.Rows.Count - 1
                grdGrid.Rows.Add(myDataTable.Rows(intRowIndex).ItemArray)
            Next

            RaiseEvent SetDisplay()

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Public Sub AddLine()

        Try
            grdGrid.Rows.Add()

            grdGrid.Rows(grdGrid.Rows.Count - 1).Selected = True

            grdGrid.SelectedRows(0).HeaderCell.Style.BackColor = Color.LightGreen

            grdGrid.SelectedRows(0).HeaderCell.Style.SelectionBackColor = Color.LightGreen

            grdGrid.Rows(grdGrid.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightGreen

            grdGrid.Rows(grdGrid.Rows.Count - 1).Cells(mintDefaultActionCol).Value = GridRowActions.INSERT_ACTION

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub Printer_Init()

        Try
            PrintPrevDialog.PrintPreviewControl.Zoom = 1
            PrintPrevDialog.Document = PrintDoc
            PrintPrevDialog.Show()

            AddHandler PrintDoc.PrintPage, AddressOf PrintDoc_PrintPage

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub RemoveLine()
        Dim intSelectedRow As Integer

        Try
            If grdGrid.Rows.Count > 0 Then

                intSelectedRow = grdGrid.SelectedRows(0).Index

                If CInt(grdGrid.Rows(intSelectedRow).Cells(mintDefaultActionCol).Value) = GridRowActions.INSERT_ACTION Then
                    grdGrid.Rows.RemoveAt(intSelectedRow)

                    If grdGrid.Rows.Count > 0 Then
                        grdGrid.Rows(intSelectedRow - 1).Selected = True
                    End If
                Else
                    grdGrid.SelectedRows(0).HeaderCell.Style.BackColor = Color.Red

                    grdGrid.SelectedRows(0).HeaderCell.Style.SelectionBackColor = Color.Red

                    grdGrid.Rows(intSelectedRow).DefaultCellStyle.BackColor = Color.Red

                    grdGrid.Rows(intSelectedRow).Cells(mintDefaultActionCol).Value = GridRowActions.DELETE_ACTION
                End If
            End If

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Function CellIsEmpty(ByVal vintRow As Integer, ByVal vintCol As Integer) As Boolean
        Dim blnIsEmpty As Boolean = True

        Try
            Select Case False
                Case Not IsDBNull(grdGrid.Rows(vintRow).Cells(vintCol).Value)
                Case Not IsNothing(grdGrid.Rows(vintRow).Cells(vintCol).Value)
                Case Not String.IsNullOrEmpty(Trim(grdGrid.Rows(vintRow).Cells(vintCol).Value.ToString))
                Case Else
                    blnIsEmpty = False
            End Select

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnIsEmpty
    End Function

    Private Sub grdGrid_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGrid.CellValueChanged
        If grdGrid.Rows.Count > 0 And e.RowIndex >= 0 Then

            If CShort(grdGrid.Rows(e.RowIndex).Cells(mintDefaultActionCol).Value) = GridRowActions.CONSULT_ACTION Then
                grdGrid.Rows(e.RowIndex).HeaderCell.Style.SelectionBackColor = Color.Yellow
                grdGrid.Rows(e.RowIndex).HeaderCell.Style.BackColor = Color.Yellow
                grdGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow

                grdGrid.Rows(e.RowIndex).Cells(mintDefaultActionCol).Value = GridRowActions.UPDATE_ACTION
            End If
        End If
    End Sub

#End Region

    
    Private Sub PrintDoc_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDoc.PrintPage
        'Dim oStringFormat As StringFormat
        'Dim TotalWidth As Integer
        'Dim nRowPos As Integer = 0
        'Dim NewPage As Boolean = True

        'Dim PageNo As Integer = 1
        'Dim Header As String = "Consolidated Report"
        'Dim sUserName As String = "Nicolas"

        'Dim oColumnLefts As New ArrayList
        'Dim oColumnWidths As New ArrayList
        'Dim oColumnTypes As New ArrayList

        'Dim Height As Integer
        'Dim Width As Integer
        'Dim i As Integer
        'Dim RowsPerPage As Integer
        'Dim Top As Integer = e.MarginBounds.Top
        'Dim Left As Integer = e.MarginBounds.Left

        'TotalWidth = 0

        'For Each DColumn As DataGridViewColumn In grdGrid.Columns
        '    TotalWidth += DColumn.Width
        'Next

        'If PageNo = 1 Then
        '    For Each DColumn As DataGridViewColumn In grdGrid.Columns
        '        Width = CType(Math.Floor(DColumn.Width / TotalWidth * TotalWidth * (e.MarginBounds.Width / TotalWidth)), Int16)
        '        Height = CInt(e.Graphics.MeasureString(DColumn.HeaderText, DColumn.InheritedStyle.Font, Width).Height + 11)
        '        oColumnLefts.Add(Left)
        '        oColumnWidths.Add(Width)
        '        oColumnTypes.Add(DColumn.GetType)
        '        Left += Width
        '    Next
        'End If

        'Do While nRowPos < grdGrid.Rows.Count
        '    Dim oRow As DataGridViewRow = grdGrid.Rows(nRowPos)
        '    If Top + Height >= e.MarginBounds.Height + e.MarginBounds.Top Then

        '        'DrawFooter(e, RowsPerPage)
        '        NewPage = True
        '        PageNo += 1
        '        e.HasMorePages = True
        '        Exit Sub
        '    Else

        '        If NewPage Then
        '            Top = e.MarginBounds.Top
        '            i = 0
        '            For Each oColumn As DataGridViewColumn In grdGrid.Columns
        '                e.Graphics.FillRectangle(New SolidBrush(Drawing.Color.LightGray), New Rectangle(oColumn(i), Top, oColumnWidths(i), Height))
        '                e.Graphics.DrawRectangle(Pens.Black, New Rectangle(oColumn(i), Top, oColumn.Width(), Height))
        '                e.Graphics.DrawString(oColumn.HeaderText, oColumn.InheritedStyle.Font, New SolidBrush(oColumn.InheritedStyle.ForeColor), New RectangleF(oColumnLefts(i), Top, oColumnWidths(i), Height), oStringFormat)
        '                i += 1
        '            Next
        '            NewPage = False
        '        End If
        '        Top += Height
        '        i = 0
        '        For Each oCell As DataGridViewCell In oRow.Cells

        '            If oColumnTypes(i) Is GetType(DataGridViewTextBoxColumn) Then
        '                e.Graphics.DrawString(oCell.Value.ToString, oCell.InheritedStyle.Font, New SolidBrush(oCell.InheritedStyle.ForeColor), New RectangleF(oColumnLefts(i), Top, oColumnWidths(i), Height), oStringFormat)

        '            ElseIf oColumnTypes(i) Is GetType(DataGridViewImageColumn) Then
        '                Dim oCellSize As Rectangle = New Rectangle(oColumnLefts(i), Top, oColumnWidths(i), Height)
        '                Dim oImageSize As Size = CType(oCell.Value, Image).Size
        '                e.Graphics.DrawImage(oCell.Value, New Rectangle(oColumnLefts(i) + CType(((oCellSize.Width - oImageSize.Width) / 2), Int32), Top + CType(((oCellSize.Height - oImageSize.Height) / 2), Int32), CType(oCell.Value, Image).Width, CType(oCell.Value, Image).Height))
        '            End If

        '            e.Graphics.DrawRectangle(Pens.Black, New Rectangle(oColumnLefts(i), Top, oColumnWidths(i), Height))
        '            i += 1
        '        Next
        '    End If

        '    nRowPos += 1
        '    RowsPerPage += 1
        'Loop
        'DrawFooter(e, RowsPerPage)
        'e.HasMorePages = False
    End Sub
End Class
