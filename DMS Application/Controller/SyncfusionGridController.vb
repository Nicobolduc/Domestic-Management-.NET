
Public Class SyncfusionGridController

    'Private members
    Private Const mintDefaultActionCol As Short = 0
    Private Const mstrSelectionColName As String = "SelCol"

    'Private class members
    Private WithEvents grdSync As GridControl
    Private colsSizeBehavior As ColsSizeBehaviorsController = Nothing

    'Public events
    Public Event SetDisplay()
    Public Event ValidateData()
    Public Event SaveGridData()

    'Public enums
    Public Enum GridRowActions
        CONSULT_ACTION = mConstants.Form_Modes.CONSULT_MODE
        INSERT_ACTION = mConstants.Form_Modes.INSERT_MODE
        UPDATE_ACTION = mConstants.Form_Modes.UPDATE_MODE
        DELETE_ACTION = mConstants.Form_Modes.DELETE_MODE
    End Enum


#Region "Properties"

    Public WriteOnly Property SetColsSizeBehavior As ColsSizeBehaviorsController.colsSizeBehaviors
        Set(value As ColsSizeBehaviorsController.colsSizeBehaviors)
            colsSizeBehavior = New ColsSizeBehaviorsController
            colsSizeBehavior.AttachGrid(grdSync)
            colsSizeBehavior.ColsSizeBehavior = value
        End Set
    End Property

    Public ReadOnly Property GetSelectedRowsCount As Integer
        Get
            Return grdSync.Selections.GetSelectedRows(True, True).Count
        End Get
    End Property

#End Region


#Region "Functions / Subs"

    Public Function bln_Init(ByRef rgrdGrid As GridControl, Optional ByRef rbtnAddLine As Button = Nothing, Optional ByRef rbtnRemoveLine As Button = Nothing) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle

        Try
            grdSync = rgrdGrid

            grdSync.BeginInit()
            'AddHandler grdSync.QueryCellInfo, New GridQueryCellInfoEventHandler(AddressOf GridQueryCellInfo)

            grdSync.ThemesEnabled = True
            grdSync.UnHideColsOnDblClick = False
            grdSync.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue

            grdSync.NumberedRowHeaders = False
            grdSync.NumberedColHeaders = False
            grdSync.DefaultGridBorderStyle = GridBorderStyle.Solid
            grdSync.BorderStyle = BorderStyle.Fixed3D

            grdSync.DefaultRowHeight = 18
            grdSync.DefaultColWidth = 70
            grdSync.SetColWidth(0, 0, 9)

            grdSync.ListBoxSelectionMode = SelectionMode.One

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            grdSync.EndInit()
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_FillData(ByVal vstrSQL As String) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim sqlCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myDataTable As DataTable = New DataTable
        Dim strGridCaption As String = String.Empty
        Dim lstColumns As String()
        Dim dataTableArray(,) As Object
        Dim headerStyle As GridStyleInfo
        Dim individualColStyle As GridStyleInfo

        Try
            'Retrieve grid data from database
            sqlCmd = New MySqlCommand(vstrSQL, gcAppControler.MySQLConnection)

            mySQLReader = sqlCmd.ExecuteReader

            myDataTable.Load(mySQLReader)

            dataTableArray = New Object(myDataTable.Rows.Count - 1, myDataTable.Columns.Count) {}

            strGridCaption = gcAppControler.str_GetCaption(CInt(grdSync.Tag), gcAppControler.cUser.GetLanguage)

            lstColumns = Split(strGridCaption.Insert(0, "|"), "|")

            'Reset the grid
            'grdSync.Model.Data.Clear()
            'grdSync.RowCount = 0
            'grdSync.ColCount = 0
            grdSync.Model.ResetVolatileData()
            grdSync.Refresh()
            'grdSync.Invalidate()
            grdSync.BeginUpdate()
            'grdSync.ResetVolatileData()
            'grdSync.ClearCells(GridRangeInfo.Cells(1, 1, grdSync.RowCount, grdSync.ColCount), True)
            grdSync.RowCount = myDataTable.Rows.Count
            grdSync.ColCount = lstColumns.Count - 1

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

            'Set the grid data
            For intTableRowIndex As Integer = 0 To myDataTable.Rows.Count - 1

                For intTableColIndex As Integer = 0 To myDataTable.Columns.Count - 1

                    dataTableArray(intTableRowIndex, intTableColIndex) = myDataTable.Rows(intTableRowIndex)(intTableColIndex)
                Next
            Next

            grdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, myDataTable.Rows.Count, myDataTable.Columns.Count), dataTableArray)
            grdSync(1, 2).CellValue = myDataTable.Rows.Count
            'Definition of visual styles
            headerStyle = New GridStyleInfo

            headerStyle.BackColor = Color.WhiteSmoke

            grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, 0, grdSync.ColCount), headerStyle, Syncfusion.Styles.StyleModifyType.ApplyNew)
            grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, grdSync.RowCount, 0), headerStyle)

            RaiseEvent SetDisplay()

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
            grdSync.Invalidate()
            grdSync.EndUpdate(True)
            grdSync.Refresh()
        End Try

        Return blnValidReturn
    End Function

#End Region
    'Private dataTableArray(,) As Object
    'Private mblnLoaded As Boolean
    'Private Sub GridQueryCellInfo(ByVal sender As Object, ByVal e As GridQueryCellInfoEventArgs)
    '    Dim blnValidReturn As Boolean = True
    '    Dim sqlCmd As MySqlCommand
    '    Dim mySQLReader As MySqlDataReader = Nothing
    '    Dim myDataTable As DataTable = New DataTable
    '    Dim strGridCaption As String = String.Empty
    '    Dim strSQL As String = String.Empty

    '    If Not mblnLoaded Then
    '        strSQL = strSQL & "  SELECT Company.Cy_ID, " & vbCrLf
    '        strSQL = strSQL & "         Company.Cy_Name " & vbCrLf
    '        strSQL = strSQL & "  FROM Company " & vbCrLf

    '        strSQL = strSQL & "  ORDER BY Company.Cy_Name " & vbCrLf

    '        'Retrieve grid data from database
    '        sqlCmd = New MySqlCommand(strSQL, gcAppControler.MySQLConnection)

    '        mySQLReader = sqlCmd.ExecuteReadersdfsdf

    '        myDataTable.Load(mySQLReader)

    '        dataTableArray = New Object(myDataTable.Rows.Count - 1, myDataTable.Columns.Count) {}

    '        For intTableRowIndex As Integer = 0 To myDataTable.Rows.Count - 1

    '            For intTableColIndex As Integer = 0 To myDataTable.Columns.Count - 1

    '                dataTableArray(intTableRowIndex, intTableColIndex) = myDataTable.Rows(intTableRowIndex)(intTableColIndex)
    '            Next
    '        Next
    '        mblnLoaded = True
    '    End If

    '    If ((e.RowIndex > 0) AndAlso (e.ColIndex > 0)) Then

    '        e.Style.CellValue = dataTableArray(e.RowIndex - 1, e.ColIndex - 1)

    '        e.Handled = True

    '    End If

    'End Sub

    Private Sub faitChier()

    End Sub

End Class

Public Class ColsSizeBehaviorsController

    Private grdSync As GridControlBase = Nothing
    Private _colsSizeBehavior As colsSizeBehaviors
    Private colRatios() As Double = Nothing
    Private blnLastColWidthChanged As Boolean

    Public Enum colsSizeBehaviors
        NONE = 0
        EXTEND_LAST_COL
        EXTEND_FIRST_COL
        ALL_COLS_EQUALS
    End Enum

    Public Property ColsSizeBehavior() As colsSizeBehaviors
        Get
            Return _colsSizeBehavior
        End Get

        Set(ByVal value As colsSizeBehaviors)
            _colsSizeBehavior = value
        End Set
    End Property

    Protected Friend Sub AttachGrid(ByVal grid As GridControlBase)
        Dim dblGridWidth As Double

        If Me.grdSync IsNot grid Then
            If Me.grdSync IsNot Nothing Then
                DetachGrid()
            End If

            Me.grdSync = grid

            dblGridWidth = Me.grdSync.ClientSize.Width

            If TypeOf grid Is GridDataBoundGrid Then
                CType(Me.grdSync, GridDataBoundGrid).SmoothControlResize = False
            ElseIf TypeOf grid Is GridControl Then
                CType(Me.grdSync, GridControl).SmoothControlResize = False
            End If

            'Save original col ratios
            colRatios = New Double(Me.grdSync.Model.ColCount) {}

            For col As Integer = 0 To Me.grdSync.Model.ColCount
                colRatios(col) = Me.grdSync.Model.ColWidths(col) / dblGridWidth
            Next col

            AddHandler grid.Model.QueryColWidth, AddressOf grid_QueryColWidth
            AddHandler grid.Model.ColWidthsChanged, AddressOf grid_ColWidthsChanged
            AddHandler grid.ResizingColumns, AddressOf grid_ResizingColumns
        End If
    End Sub

    Protected Friend Sub DetachGrid()
        RemoveHandler grdSync.Model.QueryColWidth, AddressOf grid_QueryColWidth
        RemoveHandler grdSync.Model.ColWidthsChanged, AddressOf grid_ColWidthsChanged
        RemoveHandler grdSync.ResizingColumns, AddressOf grid_ResizingColumns

        Me.grdSync = Nothing
    End Sub

    Private Sub grid_ResizingColumns(ByVal sender As Object, ByVal e As GridResizingColumnsEventArgs)

        If _colsSizeBehavior = colsSizeBehaviors.ALL_COLS_EQUALS Then
            e.Cancel = True
        ElseIf _colsSizeBehavior = colsSizeBehaviors.EXTEND_LAST_COL AndAlso e.Columns.Right = Me.grdSync.Model.ColCount Then
            e.Cancel = True
        ElseIf _colsSizeBehavior = colsSizeBehaviors.EXTEND_FIRST_COL AndAlso e.Columns.Left = Me.grdSync.Model.Cols.HeaderCount + 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub grid_QueryColWidth(ByVal sender As Object, ByVal e As GridRowColSizeEventArgs)

        Select Case _colsSizeBehavior
            Case colsSizeBehaviors.EXTEND_LAST_COL
                If e.Index = Me.grdSync.Model.ColCount Then
                    e.Size = Me.grdSync.ClientSize.Width - Me.grdSync.Model.ColWidths.GetTotal(0, Me.grdSync.Model.ColCount - 1)
                    e.Handled = True
                End If

            Case colsSizeBehaviors.EXTEND_FIRST_COL
                If e.Index = Me.grdSync.Model.Cols.FrozenCount + 1 Then
                    Dim leftPiece As Integer = Me.grdSync.Model.ColWidths.GetTotal(0, Me.grdSync.Model.Cols.FrozenCount)
                    Dim rightPiece As Integer = Me.grdSync.Model.ColWidths.GetTotal(Me.grdSync.Model.Cols.FrozenCount + 2, Me.grdSync.Model.ColCount)
                    e.Size = Me.grdSync.ClientSize.Width - leftPiece - rightPiece
                    e.Handled = True
                End If
                '				case GridColSizeBehavior.FixedProportional:
                '					if(e.Index == this.grid.Model.ColCount)
                '					{
                '						e.Size = this.grid.ClientSize.Width - this.grid.Model.ColWidths.GetTotal(0, this.grid.Model.ColCount - 1);
                '					}
                '					else
                '					{
                '						e.Size = (int) (this.colRatios[e.Index] * this.grid.ClientSize.Width);
                '					}
                '					e.Handled = true;
                '					break;

            Case colsSizeBehaviors.ALL_COLS_EQUALS
                If e.Index = Me.grdSync.Model.ColCount Then
                    e.Size = Me.grdSync.ClientSize.Width - Me.grdSync.Model.ColWidths.GetTotal(0, Me.grdSync.Model.ColCount - 1)
                Else
                    e.Size = CInt(Fix(Me.colRatios(e.Index) * Me.grdSync.ClientSize.Width))
                End If

                e.Handled = True

            Case Else
        End Select
    End Sub

    Private Sub grid_ColWidthsChanged(ByVal sender As Object, ByVal e As GridRowColSizeChangedEventArgs)
        Dim dWidth As Double = Me.grdSync.ClientSize.Width

        If Me.blnLastColWidthChanged Then
            Return
        End If

        blnLastColWidthChanged = True

        If Me._colsSizeBehavior <> colsSizeBehaviors.ALL_COLS_EQUALS Then
            Me.colRatios = New Double(Me.grdSync.Model.ColCount) {}

            For col As Integer = 0 To Me.grdSync.Model.ColCount
                Me.colRatios(col) = Me.grdSync.Model.ColWidths(col) / dWidth
            Next col
        End If

        blnLastColWidthChanged = False
    End Sub

End Class