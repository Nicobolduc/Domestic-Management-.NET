Option Strict Off

Imports System.ComponentModel

Public Class SyncfusionGridController

    'Col 0 is the RowHeader and Row 0 is the Header of cols

    'Private members
    Private Const mintDefaultActionCol As Short = 1
    Private Const mstrSelectionColName As String = "SelCol"

    'Private class members
    Private WithEvents mcGrdSync As GridControl
    Private WithEvents mbtnAddRow As Button
    Private WithEvents mbtnDeleteRow As Button
    Private WithEvents mfrmGridParent As Object
    Private mcGridColsSizeBehavior As ColsSizeBehaviorsController = Nothing

    'Public events
    Public Event SetDisplay()
    Public Event ValidateData(ByVal eventArgs As ValidateGridEventArgs)
    Public Event SaveGridData()

    'Public enums
    Public Enum GridRowActions
        NO_ACTION = mConstants.Form_Modes.CONSULT_MODE
        INSERT_ACTION = mConstants.Form_Modes.INSERT_MODE
        UPDATE_ACTION = mConstants.Form_Modes.UPDATE_MODE
        DELETE_ACTION = mConstants.Form_Modes.DELETE_MODE
    End Enum


#Region "Properties"

    Public WriteOnly Property SetColsSizeBehavior As ColsSizeBehaviorsController.colsSizeBehaviors
        Set(ByVal value As ColsSizeBehaviorsController.colsSizeBehaviors)
            mcGridColsSizeBehavior = New ColsSizeBehaviorsController
            mcGridColsSizeBehavior.AttachGrid(mcGrdSync)
            mcGridColsSizeBehavior.ColsSizeBehavior = value
        End Set
    End Property

    Public ReadOnly Property GetSelectedRowsCount As Integer
        Get
            Return mcGrdSync.Selections.GetSelectedRows(True, True).Count
        End Get
    End Property

    Public ReadOnly Property GetSelectedRow As Integer
        Get
            If mcGrdSync.Selections.GetSelectedRows(True, True).Count > 0 Then

                Return mcGrdSync.Selections.GetSelectedRows(True, True).Item(0).Top
            Else
                Return -1
            End If
        End Get
    End Property

    Public ReadOnly Property GetSelectedCol As Integer
        Get
            If mcGrdSync.Selections.GetSelectedCols(True, True).Count > 0 Then

                Return mcGrdSync.Selections.GetSelectedCols(True, True).Item(0).Left
            Else
                Return -1
            End If
        End Get
    End Property

    Public WriteOnly Property SetSelectedRow() As Integer
        Set(ByVal value As Integer)
            If value <= mcGrdSync.RowCount And value > 0 Then

                mcGrdSync.CurrentCell.MoveTo(GridRangeInfo.Row(value), GridSetCurrentCellOptions.SetFocus And GridSetCurrentCellOptions.ScrollInView)
            Else
                'Do nothing
            End If
        End Set
    End Property

    Public WriteOnly Property SetSelectedCol(Optional ByVal vblnShowDropDown As Boolean = False) As Integer
        Set(ByVal value As Integer)
            If value <= mcGrdSync.ColCount And value > 0 Then

                mcGrdSync.CurrentCell.MoveTo(GridRangeInfo.Cell(GetSelectedRow, value), GridSetCurrentCellOptions.SetFocus And GridSetCurrentCellOptions.ScrollInView)

                If vblnShowDropDown Then

                    mcGrdSync.CurrentCell.ShowDropDown()
                Else
                    mcGrdSync.CurrentCell.BeginEdit()
                End If
            Else
                'Do nothing
            End If
        End Set
    End Property

    Public WriteOnly Property ChangeMade As Boolean
        Set(ByVal value As Boolean)
            If mcGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue <> GridRowActions.INSERT_ACTION Then

                If value = True Then

                    mfrmGridParent.formController.ChangeMade = True

                    mcGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow
                    mcGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
                Else
                    mfrmGridParent.formController.ChangeMade = False

                    mcGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Empty
                    mcGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.NO_ACTION
                End If
            End If
        End Set
    End Property

#End Region


#Region "Functions / Subs"

    Public Function bln_Init(ByRef rgrdGrid As GridControl, Optional ByRef rbtnAddRow As Button = Nothing, Optional ByRef rbtnRemoveRow As Button = Nothing) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle

        Try
            mfrmGridParent = rgrdGrid.FindParentForm

            mcGrdSync = rgrdGrid
            mbtnAddRow = rbtnAddRow
            mbtnDeleteRow = rbtnRemoveRow

            mcGrdSync.BeginInit()
            mcGrdSync.ControllerOptions = GridControllerOptions.ClickCells Or GridControllerOptions.ResizeCells Or GridControllerOptions.SelectCells
            mcGrdSync.CommandStack.Enabled = True
            mcGrdSync.ResizeColsBehavior = GridResizeCellsBehavior.ResizeSingle Or GridResizeCellsBehavior.OutlineHeaders Or GridResizeCellsBehavior.InsideGrid
            mcGrdSync.ListBoxSelectionMode = SelectionMode.One
            mcGrdSync.Model.Options.ActivateCurrentCellBehavior = GridCellActivateAction.DblClickOnCell
            'AddHandler grdSync.QueryCellInfo, New GridQueryCellInfoEventHandler(AddressOf GridQueryCellInfo)

            mcGrdSync.ThemesEnabled = True
            mcGrdSync.UnHideColsOnDblClick = False
            mcGrdSync.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue
            mcGrdSync.Properties.BackgroundColor = Color.LightGray

            mcGrdSync.NumberedRowHeaders = False
            mcGrdSync.NumberedColHeaders = False
            mcGrdSync.DefaultGridBorderStyle = GridBorderStyle.Solid
            mcGrdSync.BorderStyle = BorderStyle.Fixed3D

            mcGrdSync.DefaultRowHeight = 18
            mcGrdSync.DefaultColWidth = 70
            mcGrdSync.SetColWidth(0, 0, 9)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcGrdSync.EndInit()
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_FillData(ByVal vstrSQL As String) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim sqlCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myDataTable As DataTable = New DataTable
        Dim strGridCaption As String = String.Empty
        Dim dataTableArray(,) As Object

        Try
            'Retrieve grid data from database
            sqlCmd = New MySqlCommand(vstrSQL, gcAppController.MySQLConnection)

            mySQLReader = sqlCmd.ExecuteReader

            myDataTable.Load(mySQLReader)

            dataTableArray = New Object(myDataTable.Rows.Count - 1, myDataTable.Columns.Count) {}

            'Set the grid data
            For intTableRowIndex As Integer = 0 To myDataTable.Rows.Count - 1

                For intTableColIndex As Integer = 0 To myDataTable.Columns.Count - 1

                    dataTableArray(intTableRowIndex, intTableColIndex) = myDataTable.Rows(intTableRowIndex)(intTableColIndex)
                Next
            Next

            'Reset the grid
            'grdSync.Model.Data.Clear()  
            mcGrdSync.Model.ResetVolatileData()
            mcGrdSync.RowCount = 0
            mcGrdSync.ColCount = 0
            mcGrdSync.BeginUpdate()

            mcGrdSync.RowCount = myDataTable.Rows.Count
            mcGrdSync.ColCount = myDataTable.Columns.Count

            mcGrdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, myDataTable.Rows.Count, myDataTable.Columns.Count), dataTableArray)

            blnValidReturn = bln_SetColsDisplay()

            RaiseEvent SetDisplay()

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If

            mcGrdSync.EndUpdate(True)

        End Try

        Return blnValidReturn
    End Function

    Public Function CellIsEmpty(ByVal vintRow As Integer, ByVal vintCol As Integer) As Boolean
        Dim blnIsEmpty As Boolean = True

        Try
            Select Case False
                Case Not IsDBNull(mcGrdSync(vintRow, vintCol).CellValue)
                Case Not IsNothing(mcGrdSync(vintRow, vintCol).CellValue)
                Case Not String.IsNullOrEmpty(Trim(mcGrdSync(vintRow, vintCol).CellValue.ToString))
                Case Else
                    blnIsEmpty = False
            End Select

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnIsEmpty
    End Function

    Public Function CurrentCellIsEmpty() As Boolean
        Dim blnIsEmpty As Boolean = True

        Try
            Select Case False
                Case Not IsDBNull(mcGrdSync(GetSelectedRow, GetSelectedCol).CellValue)
                Case Not IsNothing(mcGrdSync(GetSelectedRow, GetSelectedCol).CellValue)
                Case Not String.IsNullOrEmpty(Trim(mcGrdSync(GetSelectedRow, GetSelectedCol).CellValue.ToString))
                Case Else
                    blnIsEmpty = False
            End Select

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnIsEmpty
    End Function

    Private Function bln_SetColsDisplay() As Boolean
        Dim blnValidReturn As Boolean = True
        Dim strGridCaption As String = String.Empty
        Dim lstColumns As String()
        Dim individualColStyle As GridStyleInfo

        Try
            strGridCaption = gcAppController.str_GetCaption(CInt(mcGrdSync.Tag), gcAppController.cUser.GetLanguage)

            lstColumns = Split(strGridCaption.Insert(0, "|"), "|")

            'Definition of columns
            For colHeaderCpt As Integer = 1 To lstColumns.Count - 1

                individualColStyle = New GridStyleInfo
                individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

                If lstColumns(colHeaderCpt) = String.Empty Then
                    mcGrdSync.SetColHidden(colHeaderCpt, colHeaderCpt, True)
                Else

                    mcGrdSync(0, colHeaderCpt).Text = Microsoft.VisualBasic.Right(lstColumns(colHeaderCpt), lstColumns(colHeaderCpt).Length - 1)

                    Select Case lstColumns(colHeaderCpt).Chars(0)
                        Case CChar("<")
                            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Left


                        Case CChar("^")
                            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

                        Case CChar(">")
                            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Right

                    End Select

                    mcGrdSync.ChangeCells(GridRangeInfo.Cells(0, colHeaderCpt, mcGrdSync.RowCount, colHeaderCpt), individualColStyle)
                End If

            Next

            'Definition of visual styles
            'headerStyle = New GridStyleInfo

            'headerStyle.BackColor = Color.WhiteSmoke

            'grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, 0, grdSync.ColCount), headerStyle, Syncfusion.Styles.StyleModifyType.ApplyNew)
            'grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, grdSync.RowCount, 0), headerStyle)

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Public Function FindRow(ByVal vValueToFind As Object, ByVal vintColToSearch As Integer) As Integer
        Dim intReturnValue As Integer = -1

        For intRowIdx As Integer = 1 To mcGrdSync.RowCount

            If mcGrdSync(intRowIdx, vintColToSearch).CellValue = vValueToFind Then

                Return intRowIdx
            Else
                'Continue searching
            End If
        Next

        Return intReturnValue
    End Function

    Public Function FindCol(ByVal vValueToFind As Object, ByVal vintRowToSearch As Integer) As Integer
        Dim intReturnValue As Integer = -1

        For intColIdx As Integer = 1 To mcGrdSync.ColCount

            If mcGrdSync(intColIdx, vintRowToSearch).CellValue = vValueToFind Then

                Return intColIdx
            Else
                'Continue searching
            End If
        Next

        Return intReturnValue
    End Function

    Public Function blnSetColComboBox(ByVal vstrSQL As String, ByVal vintColumnIndex As Integer, ByVal vstrValueMember As String, ByVal vstrDisplayMember As String, ByVal vblnAllowEmpty As Boolean) As Boolean
        Dim blnValidReturn As Boolean
        Dim mySQLCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myBindingList As New BindingList(Of KeyValuePair(Of Integer, String))
        Dim style As New GridStyleInfo()

        Try
            mcGrdSync.ColStyles(vintColumnIndex).CellType = "ComboBox"
            mcGrdSync.ColStyles(vintColumnIndex).DataSource = Nothing
            mcGrdSync.ColStyles(vintColumnIndex).ExclusiveChoiceList = True

            mySQLCmd = New MySqlCommand(vstrSQL, gcAppController.MySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

            If vblnAllowEmpty Then
                myBindingList.Add(New KeyValuePair(Of Integer, String)(0, ""))
            End If
            'mySQLReader.resultset.fields(0).columnname
            While mySQLReader.Read
                If Not IsDBNull(mySQLReader(vstrValueMember)) Then
                    myBindingList.Add(New KeyValuePair(Of Integer, String)(CInt(mySQLReader(vstrValueMember)), CStr(mySQLReader(vstrDisplayMember))))
                End If
            End While

            mcGrdSync.ColStyles(vintColumnIndex).DataSource = myBindingList
            mcGrdSync.ColStyles(vintColumnIndex).ValueMember = "Key"
            mcGrdSync.ColStyles(vintColumnIndex).DisplayMember = "Value"

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source & " - " & System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private events"

    Private Sub btnAddRow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mbtnAddRow.Click

        mcGrdSync.RowCount += 1

        mcGrdSync.RowStyles(mcGrdSync.RowCount).BackColor = Color.LightGreen
        mcGrdSync(mcGrdSync.RowCount, mintDefaultActionCol).CellValue = GridRowActions.INSERT_ACTION
        SetSelectedRow = mcGrdSync.RowCount

        mfrmGridParent.formController.ChangeMade = True
    End Sub

    Private Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mbtnDeleteRow.Click
        Dim intSelectedRow As Integer = GetSelectedRow

        If intSelectedRow > 0 Then

            If mcGrdSync(intSelectedRow, mintDefaultActionCol).CellValue = SyncfusionGridController.GridRowActions.INSERT_ACTION Then

                mcGrdSync.Rows.RemoveRange(intSelectedRow, intSelectedRow)
                SetSelectedRow = IIf(intSelectedRow >= 2, intSelectedRow - 1, -1)
            Else
                mcGrdSync.RowStyles(intSelectedRow).BackColor = Color.Red
                mcGrdSync(intSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.DELETE_ACTION

                mfrmGridParent.formController.ChangeMade = True
            End If

        End If
    End Sub

    Private Sub GrdSyncController_CurrentCellChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mcGrdSync.CurrentCellChanged

        If mcGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue <> GridRowActions.INSERT_ACTION Then
            mcGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow
            mcGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
        End If
    End Sub

    Private Sub GrdSyncController_CurrentCellCloseDropDown(sender As Object, e As Syncfusion.Windows.Forms.PopupClosedEventArgs) Handles mcGrdSync.CurrentCellCloseDropDown
        mcGrdSync.CurrentCell.ConfirmChanges()
    End Sub

#End Region

End Class

Public Class ValidateGridEventArgs
    Inherits System.EventArgs

    Private mblnIsValid As Boolean

    Public Property IsValid As Boolean
        Get
            Return mblnIsValid
        End Get
        Set(ByVal value As Boolean)
            mblnIsValid = value
        End Set
    End Property

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