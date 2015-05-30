Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Class clsDataGridView

    'Private members
    Private Const mintDefaultActionCol As Short = 0
    Private Const mstrSelectionColName As String = "SelCol"

    'Private class members
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

#Region "Properties"

    Public ReadOnly Property getSelectionColName As String
        Get
            Return mstrSelectionColName
        End Get
    End Property

#End Region

#Region "Functions / Subs"

    Public Function bln_Init(ByRef rgrdGrid As DataGridView, Optional ByRef rbtnAddLine As Button = Nothing, Optional ByRef rbtnRemoveLine As Button = Nothing) As Boolean
        Dim blnReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle

        Try
            SetDoubleBuffered(rgrdGrid, True)

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
        'Dim myDataSet = New DataSet
        Dim strGridCaption As String = vbNullString
        Dim lstColumns As String()
        Dim newDGVCol As DataGridViewColumn
        Dim newDGVCell As DataGridViewCell

        Try
            grdGrid.SuspendLayout()

            grdGrid.Rows.Clear()
            grdGrid.Columns.Clear()

            strGridCaption = gcApplication.str_GetCaption(CInt(grdGrid.Tag), gcApplication.cUser.GetLanguage)

            lstColumns = Split(strGridCaption, "|")

            sqlCmd = New MySqlCommand(vstrSQL, gcApplication.MySQLConnection)

            mySQLReader = sqlCmd.ExecuteReader

            'myDataSet.Tables.Add(myDataTable)
            'myDataSet.EnforceConstraints = False

            myDataTable.Load(mySQLReader)

            For intColIndex As Integer = 0 To myDataTable.Columns.Count - 1

                newDGVCol = New DataGridViewColumn()

                If myDataTable.Columns(intColIndex).DataType.Name = "Byte" Or myDataTable.Columns(intColIndex).ColumnName = getSelectionColName Then
                    newDGVCell = New DataGridViewCheckBoxCell()
                Else
                    newDGVCell = New DataGridViewTextBoxCell()
                End If

                newDGVCol.DataPropertyName = myDataTable.Columns(intColIndex).ColumnName
                newDGVCol.CellTemplate = newDGVCell
                newDGVCol.Name = myDataTable.Columns(intColIndex).ColumnName
                newDGVCol.SortMode = DataGridViewColumnSortMode.Automatic

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

            grdGrid.ResumeLayout()

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

    Private Sub SetDoubleBuffered(ByRef vgrdGrid As DataGridView, ByVal vblnIsDoubleBuffered As Boolean)
        Dim dgvType As Type = vgrdGrid.GetType()
        Dim propInfos As Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)

        propInfos.SetValue(vgrdGrid, vblnIsDoubleBuffered, Nothing)
    End Sub

#End Region


#Region "Private Events"

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

End Class
