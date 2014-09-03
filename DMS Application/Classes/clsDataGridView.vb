Imports MySql.Data.MySqlClient

Public Class clsDataGridView

    'Private class members
    Private WithEvents grdGrid As DataGridView

    'Public events
    Public Event SetDisplay()
    Public Event SaveGridData()
    'Public Event AddLineButtonClick()
    'Public Event RemoveLineButtonClick()

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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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

            strGridCaption = gcAppControler.str_GetCaption(CInt(grdGrid.Tag), 1)

            lstColumns = Split(strGridCaption, "|")

            sqlCmd = New MySqlCommand(vstrSQL, gcAppControler.MySQLConnection)

            mySQLReader = sqlCmd.ExecuteReader

            myDataTable.Load(mySQLReader)

            For intColIndex As Integer = 0 To myDataTable.Columns.Count - 1
                newDGVCol = New DataGridViewColumn()
                newDGVCell = New DataGridViewTextBoxCell()
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
                            grdGrid.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Case CChar("^")
                            grdGrid.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        Case CChar(">")
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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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

            grdGrid.Rows(grdGrid.Rows.Count - 1).Cells(0).Value = GridRowActions.INSERT_ACTION

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub RemoveLine()
        Dim intSelectedRow As Integer

        Try
            If grdGrid.Rows.Count > 0 Then

                intSelectedRow = grdGrid.SelectedRows(0).Index

                If CInt(grdGrid.Rows(intSelectedRow).Cells(0).Value) = GridRowActions.INSERT_ACTION Then
                    grdGrid.Rows.RemoveAt(intSelectedRow)

                    If grdGrid.Rows.Count > 0 Then
                        grdGrid.Rows(intSelectedRow - 1).Selected = True
                    End If
                Else
                    grdGrid.SelectedRows(0).HeaderCell.Style.BackColor = Color.Red

                    grdGrid.SelectedRows(0).HeaderCell.Style.SelectionBackColor = Color.Red

                    grdGrid.Rows(intSelectedRow).DefaultCellStyle.BackColor = Color.Red

                    grdGrid.Rows(intSelectedRow).Cells(0).Value = GridRowActions.DELETE_ACTION
                End If
            End If

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

#End Region

End Class
