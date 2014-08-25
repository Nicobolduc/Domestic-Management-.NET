Imports MySql.Data.MySqlClient

Public Class clsDataGridView

    Private grdGrid As DataGridView


    Public Event SetDisplay()

    Public Function bln_Init(ByRef rgrdGrid As DataGridView) As Boolean
        Dim blnReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle
        Dim rowsHeaderStyle As New DataGridViewCellStyle

        Try
            grdGrid = rgrdGrid

            rowsHeaderStyle.BackColor = Color.Lime
            grdGrid.RowHeadersDefaultCellStyle = rowsHeaderStyle
            grdGrid.RowHeadersWidth = 8
            grdGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            columnsHeaderStyle.Font = New Font(grdGrid.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)
            grdGrid.ColumnHeadersDefaultCellStyle = columnsHeaderStyle
            grdGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            grdGrid.AllowUserToResizeRows = False
            grdGrid.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_FillData(ByVal vstrSQL As String) As Boolean
        Dim blnReturn As Boolean = True
        Dim sqlCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim dtTable As DataTable = New DataTable
        Dim strGridCaption As String = vbNullString
        Dim lstColumns As String()

        Try
            strGridCaption = gcApp.str_GetCaption(CInt(grdGrid.Tag), 1)

            lstColumns = Split(strGridCaption, "|")

            sqlCmd = New MySqlCommand(vstrSQL, gcApp.cMySQLConnection)

            mySQLReader = sqlCmd.ExecuteReader

            dtTable.Load(mySQLReader)

            grdGrid.DataSource = dtTable

            For intIndex As Short = 0 To CShort(lstColumns.Length - 1)

                If lstColumns(intIndex) = vbNullString Then
                    grdGrid.Columns(intIndex).Visible = False
                Else

                    grdGrid.Columns(intIndex).HeaderText = Right(lstColumns(intIndex), lstColumns(intIndex).Length - 1)

                    Select Case lstColumns(intIndex).Chars(0)
                        Case CChar("<")
                            grdGrid.Columns(intIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Case CChar("^")
                            grdGrid.Columns(intIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        Case CChar(">")
                            grdGrid.Columns(intIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    End Select
                End If
            Next

            RaiseEvent SetDisplay()

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function


End Class
