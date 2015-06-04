Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Class DataGridViewController

    'Private members
    Private Const mintDefaultActionCol As Short = 0
    Private Const mstrSelectionColName As String = "SelCol"

    'Private class members
    Private WithEvents grdDGV As DataGridView

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

            grdDGV = rgrdGrid

            grdDGV.AutoSizeColumnsMode = CType(DataGridViewAutoSizeColumnMode.Fill, DataGridViewAutoSizeColumnsMode)

            grdDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            grdDGV.AutoGenerateColumns = False
            grdDGV.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control
            grdDGV.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Control
            grdDGV.RowHeadersWidth = 8
            grdDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            columnsHeaderStyle.Font = New Font(grdDGV.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)
            grdDGV.ColumnHeadersDefaultCellStyle = columnsHeaderStyle
            grdDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            grdDGV.AllowUserToResizeRows = False

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
        'Dim myDataSet = New DataSet
        Dim strGridCaption As String = vbNullString
        Dim lstColumns As String()
        Dim newDGVCol As DataGridViewColumn
        Dim newDGVCell As DataGridViewCell

        Try
            grdDGV.SuspendLayout()

            grdDGV.Rows.Clear()
            grdDGV.Columns.Clear()

            strGridCaption = gcAppControler.str_GetCaption(CInt(grdDGV.Tag), gcAppControler.cUser.GetLanguage)

            lstColumns = Split(strGridCaption, "|")

            sqlCmd = New MySqlCommand(vstrSQL, gcAppControler.MySQLConnection)

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

                grdDGV.Columns.Add(newDGVCol)

                If lstColumns(intColIndex) = vbNullString Then
                    grdDGV.Columns(intColIndex).Visible = False
                Else

                    grdDGV.Columns(intColIndex).HeaderText = Right(lstColumns(intColIndex), lstColumns(intColIndex).Length - 1)

                    Select Case lstColumns(intColIndex).Chars(0)
                        Case CChar("<")
                            grdDGV.Columns(intColIndex).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                            grdDGV.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Case CChar("^")
                            grdDGV.Columns(intColIndex).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            grdDGV.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        Case CChar(">")
                            grdDGV.Columns(intColIndex).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                            grdDGV.Columns(intColIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    End Select
                End If
            Next

            For intRowIndex As Integer = 0 To myDataTable.Rows.Count - 1
                grdDGV.Rows.Add(myDataTable.Rows(intRowIndex).ItemArray)
            Next

            RaiseEvent SetDisplay()

            grdDGV.ResumeLayout()

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
            grdDGV.Rows.Add()

            grdDGV.Rows(grdDGV.Rows.Count - 1).Selected = True

            grdDGV.SelectedRows(0).HeaderCell.Style.BackColor = Color.LightGreen

            grdDGV.SelectedRows(0).HeaderCell.Style.SelectionBackColor = Color.LightGreen

            grdDGV.Rows(grdDGV.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightGreen

            grdDGV.Rows(grdDGV.Rows.Count - 1).Cells(mintDefaultActionCol).Value = GridRowActions.INSERT_ACTION

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub RemoveLine()
        Dim intSelectedRow As Integer

        Try
            If grdDGV.Rows.Count > 0 Then

                intSelectedRow = grdDGV.SelectedRows(0).Index

                If CInt(grdDGV.Rows(intSelectedRow).Cells(mintDefaultActionCol).Value) = GridRowActions.INSERT_ACTION Then
                    grdDGV.Rows.RemoveAt(intSelectedRow)

                    If grdDGV.Rows.Count > 0 Then
                        grdDGV.Rows(intSelectedRow - 1).Selected = True
                    End If
                Else
                    grdDGV.SelectedRows(0).HeaderCell.Style.BackColor = Color.Red

                    grdDGV.SelectedRows(0).HeaderCell.Style.SelectionBackColor = Color.Red

                    grdDGV.Rows(intSelectedRow).DefaultCellStyle.BackColor = Color.Red

                    grdDGV.Rows(intSelectedRow).Cells(mintDefaultActionCol).Value = GridRowActions.DELETE_ACTION
                End If
            End If

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Function CellIsEmpty(ByVal vintRow As Integer, ByVal vintCol As Integer) As Boolean
        Dim blnIsEmpty As Boolean = True

        Try
            Select Case False
                Case Not IsDBNull(grdDGV.Rows(vintRow).Cells(vintCol).Value)
                Case Not IsNothing(grdDGV.Rows(vintRow).Cells(vintCol).Value)
                Case Not String.IsNullOrEmpty(Trim(grdDGV.Rows(vintRow).Cells(vintCol).Value.ToString))
                Case Else
                    blnIsEmpty = False
            End Select

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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

    Private Sub grdGrid_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDGV.CellValueChanged
        If grdDGV.Rows.Count > 0 And e.RowIndex >= 0 Then

            If CShort(grdDGV.Rows(e.RowIndex).Cells(mintDefaultActionCol).Value) = GridRowActions.CONSULT_ACTION Then
                grdDGV.Rows(e.RowIndex).HeaderCell.Style.SelectionBackColor = Color.Yellow
                grdDGV.Rows(e.RowIndex).HeaderCell.Style.BackColor = Color.Yellow
                grdDGV.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow

                grdDGV.Rows(e.RowIndex).Cells(mintDefaultActionCol).Value = GridRowActions.UPDATE_ACTION
            End If
        End If
    End Sub

#End Region

End Class
