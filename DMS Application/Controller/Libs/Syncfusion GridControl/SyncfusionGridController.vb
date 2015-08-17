Option Strict Off

Imports System.ComponentModel


Public Class SyncfusionGridController

    'Col 0 is the RowHeader and Row 0 is the Header of cols

    'Private members
    Private Const mintDefaultActionCol As Short = 1
    Private Const mstrSelectionColName As String = "SelCol"
    Private mintPreviousCellChangedRow As Integer
    Private mstrUndeterminedCheckBoxState As String = "TO_DEFINE"
    Private mblnHasNoActionColumn As Boolean

    'Private class members
    Private WithEvents mGrdSync As GridControl
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
        NO_ACTION = mConstants.Form_Mode.CONSULT_MODE
        INSERT_ACTION = mConstants.Form_Mode.INSERT_MODE
        UPDATE_ACTION = mConstants.Form_Mode.UPDATE_MODE
        DELETE_ACTION = mConstants.Form_Mode.DELETE_MODE
    End Enum


#Region "Properties"

    Default Public Property Item(intRowIndex As Integer, intColIndex As Integer) As String
        Get
            Return mGrdSync(intRowIndex, intColIndex).CellValue.ToString
        End Get
        Set(value As String)
            mGrdSync(intRowIndex, intColIndex).CellValue = value
        End Set
    End Property


    Public WriteOnly Property SetColsSizeBehavior As ColsSizeBehaviorsController.colsSizeBehaviors
        Set(ByVal value As ColsSizeBehaviorsController.colsSizeBehaviors)
            mcGridColsSizeBehavior = New ColsSizeBehaviorsController
            mcGridColsSizeBehavior.AttachGrid(mGrdSync)
            mcGridColsSizeBehavior.ColsSizeBehavior = value
        End Set
    End Property

    Public ReadOnly Property GetSelectedRowsCount As Integer
        Get
            Return mGrdSync.Selections.GetSelectedRows(True, True).Count
        End Get
    End Property

    Public ReadOnly Property GetSelectedRow As Integer
        Get
            If mGrdSync.Selections.GetSelectedRows(True, True).Count > 0 Then

                Return mGrdSync.Selections.GetSelectedRows(True, True).Item(0).Top
            Else
                Return -1
            End If
        End Get
    End Property

    Public ReadOnly Property GetSelectedCol As Integer
        Get
            If mGrdSync.Selections.GetSelectedCols(True, True).Count > 0 Then

                Return mGrdSync.Selections.GetSelectedCols(True, True).Item(0).Left
            Else
                Return -1
            End If
        End Get
    End Property

    Public WriteOnly Property SetSelectedRow() As Integer
        Set(ByVal value As Integer)
            If value <= mGrdSync.RowCount And value > 0 Then

                mGrdSync.CurrentCell.MoveTo(GridRangeInfo.Row(value), GridSetCurrentCellOptions.SetFocus And GridSetCurrentCellOptions.ScrollInView)
            Else
                'Do nothing
            End If
        End Set
    End Property

    Public WriteOnly Property SetSelectedCol(Optional ByVal vblnShowDropDown As Boolean = False) As Integer
        Set(ByVal value As Integer)
            If value <= mGrdSync.ColCount And value > 0 Then

                mGrdSync.CurrentCell.MoveTo(GridRangeInfo.Cell(GetSelectedRow, value), GridSetCurrentCellOptions.SetFocus And GridSetCurrentCellOptions.ScrollInView)

                If vblnShowDropDown Then

                    mGrdSync.CurrentCell.ShowDropDown()
                Else
                    mGrdSync.CurrentCell.BeginEdit()
                End If
            Else
                'Do nothing
            End If
        End Set
    End Property

    Public WriteOnly Property ChangeMade As Boolean
        Set(ByVal value As Boolean)
            If mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue <> GridRowActions.INSERT_ACTION Then

                If value = True Then

                    mfrmGridParent.formController.ChangeMade = True

                    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow
                    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
                Else
                    mfrmGridParent.formController.ChangeMade = False

                    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Empty
                    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.NO_ACTION
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property GetUndeterminedCheckBoxState As String
        Get
            Return mstrUndeterminedCheckBoxState
        End Get
    End Property

    Public WriteOnly Property SetNoActionColumn As Boolean
        Set(value As Boolean)
            mblnHasNoActionColumn = value
        End Set
    End Property
#End Region

#Region "Functions / Subs"

    Public Function bln_Init(ByRef rgrdGrid As GridControl, Optional ByRef rbtnAddRow As Button = Nothing, Optional ByRef rbtnRemoveRow As Button = Nothing) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle

        Try
            mfrmGridParent = rgrdGrid.FindParentForm

            mGrdSync = rgrdGrid
            mbtnAddRow = rbtnAddRow
            mbtnDeleteRow = rbtnRemoveRow

            mGrdSync.BeginInit()
            mGrdSync.ControllerOptions = GridControllerOptions.ClickCells Or GridControllerOptions.ResizeCells Or GridControllerOptions.SelectCells
            mGrdSync.CommandStack.Enabled = True
            mGrdSync.ResizeColsBehavior = GridResizeCellsBehavior.ResizeSingle Or GridResizeCellsBehavior.OutlineHeaders Or GridResizeCellsBehavior.InsideGrid
            mGrdSync.ListBoxSelectionMode = SelectionMode.One
            mGrdSync.Model.Options.ActivateCurrentCellBehavior = GridCellActivateAction.DblClickOnCell
            mGrdSync.TableStyle.VerticalAlignment = GridVerticalAlignment.Middle

            mGrdSync.ThemesEnabled = True
            mGrdSync.UnHideColsOnDblClick = False
            mGrdSync.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue
            mGrdSync.Properties.BackgroundColor = Color.LightGray
            mGrdSync.TableStyle.Font = New GridFontInfo(New Font("Tahoma", 10.0F))

            mGrdSync.NumberedRowHeaders = False
            mGrdSync.NumberedColHeaders = False
            mGrdSync.DefaultGridBorderStyle = GridBorderStyle.Solid
            mGrdSync.BorderStyle = BorderStyle.Fixed3D

            mGrdSync.DefaultRowHeight = 20
            mGrdSync.DefaultColWidth = 70
            mGrdSync.SetColWidth(0, 0, 9)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mGrdSync.EndInit()
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
        Dim blnBrowseOnly As Boolean

        Try
            blnBrowseOnly = mGrdSync.BrowseOnly
            mGrdSync.BrowseOnly = False

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
            mGrdSync.Model.Data.Clear()
            mGrdSync.Model.ResetVolatileData()
            mGrdSync.CellModels.Clear()
            mGrdSync.RowCount = 0
            mGrdSync.ColCount = 0
            mGrdSync.BeginUpdate()

            mGrdSync.RowCount = myDataTable.Rows.Count
            mGrdSync.ColCount = myDataTable.Columns.Count

            mGrdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, myDataTable.Rows.Count, myDataTable.Columns.Count), dataTableArray)

            mySQLReader.Dispose()

            blnValidReturn = blnSetColsDisplay()

            RaiseEvent SetDisplay()

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If

            mGrdSync.EndUpdate(True)

            mGrdSync.BrowseOnly = blnBrowseOnly
        End Try

        Return blnValidReturn
    End Function

    Public Sub AddRow(Optional ByVal vintPosition As Integer = -1)
        mGrdSync.IgnoreReadOnly = True

        mGrdSync.Rows.InsertRange(IIf(vintPosition - 1, mGrdSync.RowCount + 1, vintPosition), 1)
        mGrdSync(mGrdSync.RowCount, -1) = mGrdSync(1, -1)

        If Not mblnHasNoActionColumn Then

            mGrdSync(mGrdSync.RowCount, mintDefaultActionCol).CellValue = SyncfusionGridController.GridRowActions.INSERT_ACTION
        End If

        mGrdSync.RowStyles(mGrdSync.RowCount).BackColor = Color.LightGreen

        mGrdSync.IgnoreReadOnly = False
    End Sub

    Public Function CellIsEmpty(ByVal vintRow As Integer, ByVal vintCol As Integer) As Boolean
        Dim blnIsEmpty As Boolean = True

        Try
            Select Case False
                Case Not IsDBNull(mGrdSync(vintRow, vintCol).CellValue)
                Case Not IsNothing(mGrdSync(vintRow, vintCol).CellValue)
                Case Not String.IsNullOrEmpty(Trim(mGrdSync(vintRow, vintCol).CellValue.ToString))
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
                Case Not IsDBNull(mGrdSync(GetSelectedRow, GetSelectedCol).CellValue)
                Case Not IsNothing(mGrdSync(GetSelectedRow, GetSelectedCol).CellValue)
                Case Not String.IsNullOrEmpty(Trim(mGrdSync(GetSelectedRow, GetSelectedCol).CellValue.ToString))
                Case Else
                    blnIsEmpty = False
            End Select

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnIsEmpty
    End Function

    Private Function blnSetColsDisplay() As Boolean
        Dim blnValidReturn As Boolean = True
        Dim strGridCaption As String = String.Empty
        Dim lstColumns As String()
        Dim individualColStyle As GridStyleInfo

        Try
            strGridCaption = gcAppController.str_GetCaption(CInt(mGrdSync.Tag), gcAppController.cUser.GetLanguage)

            lstColumns = Split(strGridCaption.Insert(0, "|"), "|")

            'Definition of columns
            For colHeaderCpt As Integer = 1 To lstColumns.Count - 1

                individualColStyle = New GridStyleInfo
                individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

                If lstColumns(colHeaderCpt) = String.Empty Then
                    mGrdSync.SetColHidden(colHeaderCpt, colHeaderCpt, True)
                Else

                    mGrdSync(0, colHeaderCpt).Text = Microsoft.VisualBasic.Right(lstColumns(colHeaderCpt), lstColumns(colHeaderCpt).Length - 1)

                    Select Case lstColumns(colHeaderCpt).Chars(0)
                        Case CChar("<")
                            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Left


                        Case CChar("^")
                            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center

                        Case CChar(">")
                            individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Right

                    End Select

                    'mGrdSync.ChangeCells(GridRangeInfo.Cells(0, colHeaderCpt, mGrdSync.RowCount, colHeaderCpt), individualColStyle)
                    mGrdSync.ColStyles(colHeaderCpt) = individualColStyle
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

        For intRowIdx As Integer = 1 To mGrdSync.RowCount

            If mGrdSync(intRowIdx, vintColToSearch).CellValue = vValueToFind Then

                Return intRowIdx
            Else
                'Continue searching
            End If
        Next

        Return intReturnValue
    End Function

    Public Function FindCol(ByVal vValueToFind As Object, ByVal vintRowToSearch As Integer) As Integer
        Dim intReturnValue As Integer = -1

        For intColIdx As Integer = 1 To mGrdSync.ColCount

            If mGrdSync(intColIdx, vintRowToSearch).CellValue = vValueToFind Then

                Return intColIdx
            Else
                'Continue searching
            End If
        Next

        Return intReturnValue
    End Function

    Public Sub SetSelectedCell(ByVal vintRowIndex As Integer, ByVal vintColIndex As Integer, Optional ByVal vblnShowDropDown As Boolean = False)

        If vintRowIndex <= mGrdSync.RowCount AndAlso vintColIndex <= mGrdSync.ColCount AndAlso vintRowIndex > 0 And vintColIndex > 0 Then

            mGrdSync.CurrentCell.MoveTo(GridRangeInfo.Cell(vintRowIndex, vintColIndex), GridSetCurrentCellOptions.SetFocus And GridSetCurrentCellOptions.ScrollInView)

            If vblnShowDropDown Then

                mGrdSync.CurrentCell.ShowDropDown()
            Else
                mGrdSync.CurrentCell.BeginEdit()
            End If
        Else
            'Do nothing
        End If
    End Sub

    Public Sub SetColType_CheckBox(ByVal vintColumnIndex As Integer, Optional ByVal vblnAllowTriStates As Boolean = False)

        mGrdSync.ColStyles(vintColumnIndex).CellType = "CheckBox"
        mGrdSync.ColStyles(vintColumnIndex).CheckBoxOptions = New GridCheckBoxCellInfo(True.ToString(), False.ToString(), mstrUndeterminedCheckBoxState, False)
        mGrdSync.ColStyles(vintColumnIndex).TriState = vblnAllowTriStates
    End Sub

    Public Sub SetColType_ComboBox(ByVal vstrSQL As String, ByVal vintColumnIndex As Integer, ByVal vstrValueMember As String, ByVal vstrDisplayMember As String, ByVal vblnAllowEmpty As Boolean)
        Dim mySQLCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim myBindingList As New BindingList(Of KeyValuePair(Of Integer, String))
        Dim style As New GridStyleInfo()

        Try
            mGrdSync.ColStyles(vintColumnIndex).CellType = "ComboBox"
            mGrdSync.ColStyles(vintColumnIndex).DataSource = Nothing
            mGrdSync.ColStyles(vintColumnIndex).ExclusiveChoiceList = True

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

            mGrdSync.ColStyles(vintColumnIndex).DataSource = myBindingList
            mGrdSync.ColStyles(vintColumnIndex).ValueMember = "Key"
            mGrdSync.ColStyles(vintColumnIndex).DisplayMember = "Value"

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source & " - " & System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try
    End Sub

    Public Sub SetColType_DateTimePicker(ByVal vintColumnIndex As Integer, ByVal vblnNullable As Boolean)
        Dim dtPickerCell As New DateTimePickerCell.DateTimePickerCellModel(mGrdSync.Model, vblnNullable)

        Try
            If Not mGrdSync.CellModels.ContainsKey(Syncfusion.GridHelperClasses.CustomCellTypes.DateTimePicker.ToString) Then
                Syncfusion.GridHelperClasses.RegisterCellModel.GridCellType(mGrdSync, Syncfusion.GridHelperClasses.CustomCellTypes.DateTimePicker)
            End If

            mGrdSync.ColStyles(vintColumnIndex).CellType = Syncfusion.GridHelperClasses.CustomCellTypes.DateTimePicker.ToString()
            'If Not mGrdSync.CellModels.ContainsKey("DateTimePicker") Then
            '    mGrdSync.CellModels.Add("DateTimePicker", dtPickerCell)
            'End If

            'mGrdSync.ColStyles(vintColumnIndex).CellType = "DateTimePicker"
            mGrdSync.ColStyles(vintColumnIndex).CellValueType = GetType(DateTime)
            mGrdSync.ColStyles(vintColumnIndex).CellValue = String.Empty
            mGrdSync.ColStyles(vintColumnIndex).Format = gcAppController.str_GetUserDateFormat

            mGrdSync.ColWidths(vintColumnIndex) = 85

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source & " - " & System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        End Try
    End Sub

#End Region

#Region "Private events"

    Private Sub btnAddRow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mbtnAddRow.Click

        mGrdSync.RowCount += 1

        mGrdSync.RowStyles(mGrdSync.RowCount).BackColor = Color.LightGreen
        mGrdSync(mGrdSync.RowCount, mintDefaultActionCol).CellValue = GridRowActions.INSERT_ACTION
        SetSelectedRow = mGrdSync.RowCount

        mfrmGridParent.formController.ChangeMade = True
    End Sub

    Private Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mbtnDeleteRow.Click
        Dim intSelectedRow As Integer = GetSelectedRow

        If intSelectedRow > 0 Then

            If mGrdSync(intSelectedRow, mintDefaultActionCol).CellValue = SyncfusionGridController.GridRowActions.INSERT_ACTION Then

                mGrdSync.Rows.RemoveRange(intSelectedRow, intSelectedRow)
                SetSelectedRow = IIf(intSelectedRow >= 2, intSelectedRow - 1, -1)
            Else
                mGrdSync.RowStyles(intSelectedRow).BackColor = Color.Red
                mGrdSync(intSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.DELETE_ACTION

                mfrmGridParent.formController.ChangeMade = True
            End If

        End If
    End Sub

    Private Sub mGrdSync_CellsChanged(sender As Object, e As GridCellsChangedEventArgs) Handles mGrdSync.CellsChanged

        'If Not mfrmGridParent.formController.FormIsLoading And e.Range.Top > 0 And mGrdSync(e.Range.Top, mintDefaultActionCol).CellValue <> GridRowActions.INSERT_ACTION Then
        '    mGrdSync.BeginUpdate()

        '    If mintPreviousCellChangedRow <> e.Range.Top Then
        '        mintPreviousCellChangedRow = e.Range.Top
        '        mGrdSync.RowStyles(e.Range.Top).BackColor = Color.Yellow
        '        mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
        '    End If

        '    mGrdSync.EndUpdate()
        'End If
    End Sub

    Private Sub mGrdSync_CurrentCellAcceptedChanges(sender As Object, e As CancelEventArgs) Handles mGrdSync.CurrentCellAcceptedChanges

        If Not mblnHasNoActionColumn And Val(mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue) <> GridRowActions.INSERT_ACTION And Not mGrdSync(GetSelectedRow, GetSelectedCol).ReadOnly Then

            mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow
            mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
        End If
    End Sub

    Private Sub mGrdSync_CurrentCellActivating(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCurrentCellActivatingEventArgs) Handles mGrdSync.CurrentCellActivating

        If mGrdSync(e.RowIndex, e.ColIndex).CellModel.Description = Syncfusion.GridHelperClasses.CustomCellTypes.DateTimePicker.ToString AndAlso mGrdSync(e.RowIndex, e.ColIndex).ReadOnly Then 'TODO DESCRIPTION EST EMPTY

            e.Cancel = True
        End If
    End Sub

    Private Sub GrdSyncController_CurrentCellChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mGrdSync.CurrentCellChanged

        'If Not mblnHasNoActionColumn And Val(mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue) <> GridRowActions.INSERT_ACTION Then
        '    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow
        '    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
        'End If
    End Sub

    Private Sub GrdSyncController_CurrentCellCloseDropDown(sender As Object, e As Syncfusion.Windows.Forms.PopupClosedEventArgs) Handles mGrdSync.CurrentCellCloseDropDown
        mGrdSync.CurrentCell.ConfirmChanges()
    End Sub

#End Region

End Class

#Region "Custom events"

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

#End Region