Public Class frmBudgetManagement

    'Private members
    Private mintBudget_ID As Integer

    'GrdBudget grid
    Private mintGrdBudget_Action_col As Short = 1
    Private mintGrdBudget_Exp_ID_col As Short = 2
    Private mintGrdBudget_NextIncomeDateToUse_col As Short = 3
    Private mintGrdBudget_Exp_NextBillingDate_col As Short = 4
    Private mintGrdBudget_Exp_Name_col As Short = 5
    Private mintGrdBudget_Exp_Amount_col As Short = 6
    Private mintGrdBudget_PExp_AmountPaid_col As Short = 7
    Private mintGrdBudget_PExp_DatePaid_col As Short = 8 
    Private mintGrdBudget_PExp_Comment_col As Short = 9
    Private mintGrdBudget_ExpT_ArgbColor_col As Short = 10
    Private mintGrdBudget_Income_Amount_col As Short = 11
    Private mintGrdBudget_Sel_col As Short = 12

    'Messages
    Private mintMustDefineMainIncome_msg As Short = 18
    Private mintDtFromToRestrictions_msg As Short = 26

    'Private class members
    Private WithEvents mcGridBudgetController As SyncfusionGridController
    Private WithEvents mcSQL As MySQLController

#Region "Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        mcGridBudgetController = New SyncfusionGridController
    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = "          SELECT Budget.Bud_Name " & vbCrLf
            strSQL = strSQL & " FROM Budget " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Income ON Income.Inc_IsMain = 1 AND Income.Bud_ID = " & mintBudget_ID & vbCrLf
            strSQL = strSQL & " WHERE Budget.Bud_ID = " & mintBudget_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                dtpFrom.Value = Date.Today
                dtpFrom.CustomFormat = gcAppCtrl.str_GetUserDateFormat

                dtpTo.Value = DateAdd(DateInterval.Month, 1, Date.Today)
                dtpTo.CustomFormat = gcAppCtrl.str_GetUserDateFormat

                txtName.Text = mySQLReader.Item("Bud_Name").ToString

                blnValidReturn = True
            Else
                mySQLReader.Close()
                gcAppCtrl.ShowMessage(mintMustDefineMainIncome_msg, MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            blnValidReturn = False
        Finally
            mySQLReader.Dispose()
            If Not blnValidReturn Then Me.Close()
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdBudget_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim intRowIdx As Integer = 2
        Dim intLastRowAddedIndex As Integer = 1

        Try
            strSQL = strSQL & " CALL sp_LoadBudgetGrid (" & _
                gcAppCtrl.str_FixStringForSQL(Format(dtpFrom.Value, gcAppCtrl.str_GetServerDateFormat) & " " & Format(dtpFromHr.Value, gcAppCtrl.str_GetServerTimeFormat)) & ", " & _
                gcAppCtrl.str_FixStringForSQL(Format(dtpTo.Value, gcAppCtrl.str_GetServerDateFormat) & " " & Format(dtpToHr.Value, gcAppCtrl.str_GetServerTimeFormat)) & ", " & _
                formController.Item_ID & ");"

            blnValidReturn = mcGridBudgetController.bln_FillData(strSQL)

            If blnValidReturn Then

                While intRowIdx <= grdBudget.RowCount + 1

                    grdBudget(intRowIdx - 1, mintGrdBudget_Exp_Name_col).BackColor = Color.FromArgb(CInt(grdBudget(intRowIdx - 1, mintGrdBudget_ExpT_ArgbColor_col).CellValue))

                    If intRowIdx = grdBudget.RowCount + 1 Then

                        blnGrdBudget_AddSummaryRow(intRowIdx, intLastRowAddedIndex)

                        intRowIdx = intLastRowAddedIndex

                    ElseIf CDate(grdBudget(intRowIdx - 1, mintGrdBudget_NextIncomeDateToUse_col).CellValue) < CDate(grdBudget(intRowIdx, mintGrdBudget_NextIncomeDateToUse_col).CellValue) Then

                        blnGrdBudget_AddSummaryRow(intRowIdx, intLastRowAddedIndex)

                        intRowIdx = intLastRowAddedIndex

                    End If

                    intRowIdx += 1
                End While

            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdBudget_AddSummaryRow(ByVal vintRowIndexToAdd As Integer, ByRef rintLastRowAddedIndex As Integer) As Boolean
        Dim blnValidReturn As Boolean
        Dim dblPeriodTotalToPay As Double
        Dim intNbRowsAdded As Byte = 2
        Dim newCellsStyle As New GridStyleInfo
        Dim newRowBorders As New GridBorder(GridBorderStyle.Dashed, Color.LightSkyBlue, GridBorderWeight.Medium)

        Try
            grdBudget.IgnoreReadOnly = True

            'Row for total to pay column
            grdBudget.Rows.InsertRange(vintRowIndexToAdd, 1)

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Action_col).CellValue = SyncfusionGridController.GridRowActions.NO_ACTION

            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Borders.All = newRowBorders
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).HorizontalAlignment = GridHorizontalAlignment.Right
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellValueType = GetType(String)
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellType = GridCellTypeName.FormulaCell
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Text = "=B4"

            For intRowIdx As Integer = rintLastRowAddedIndex To vintRowIndexToAdd - 1

                dblPeriodTotalToPay += Val(grdBudget(intRowIdx, mintGrdBudget_Exp_Amount_col).CellValue)
            Next

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellValue = dblPeriodTotalToPay
            grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Font.Bold = True


            'Row for total of deficit or extra
            vintRowIndexToAdd += 1

            grdBudget.Rows.InsertRange(vintRowIndexToAdd, 1)
            grdBudget(vintRowIndexToAdd, mintGrdBudget_Action_col).CellValue = SyncfusionGridController.GridRowActions.NO_ACTION

            If CDbl(grdBudget(vintRowIndexToAdd - 1, mintGrdBudget_Exp_Amount_col).CellValue) < CDbl(grdBudget(vintRowIndexToAdd - intNbRowsAdded, mintGrdBudget_Income_Amount_col).CellValue) Then

                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Name_col).HorizontalAlignment = GridHorizontalAlignment.Right
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellValue = "+" & CStr(Val(grdBudget(vintRowIndexToAdd - intNbRowsAdded, mintGrdBudget_Income_Amount_col).CellValue) - Val(grdBudget(vintRowIndexToAdd - 1, mintGrdBudget_Exp_Amount_col).CellValue))
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).TextColor = Color.Green
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Font.Bold = True
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Format = mConstants.DataFormat.CURRENCY

            ElseIf CDbl(grdBudget(vintRowIndexToAdd - 1, mintGrdBudget_Exp_Amount_col).CellValue) > CDbl(grdBudget(vintRowIndexToAdd - intNbRowsAdded, mintGrdBudget_Income_Amount_col).CellValue) Then

                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Name_col).HorizontalAlignment = GridHorizontalAlignment.Right
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellValue = CDbl(grdBudget(vintRowIndexToAdd - intNbRowsAdded, mintGrdBudget_Income_Amount_col).CellValue) - CDbl(grdBudget(vintRowIndexToAdd - 1, mintGrdBudget_Exp_Amount_col).CellValue)
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).TextColor = Color.Red
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Font.Bold = True
                grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Format = mConstants.DataFormat.CURRENCY
            Else
                grdBudget.Rows.RemoveRange(vintRowIndexToAdd, vintRowIndexToAdd)
                vintRowIndexToAdd -= 1
                intNbRowsAdded = 1
            End If

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Name_col).CellValue = "Revenus: " & grdBudget(vintRowIndexToAdd - intNbRowsAdded, mintGrdBudget_Income_Amount_col).CellValue.ToString
            grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Name_col).Font.Bold = True

            newCellsStyle.CellType = "Default"
            newCellsStyle.ReadOnly = True
            newCellsStyle.MergeCell = GridMergeCellDirection.Both

            grdBudget.ChangeCells(GridRangeInfo.Cells(CInt(IIf(intNbRowsAdded = 2, vintRowIndexToAdd - 1, vintRowIndexToAdd)), 1, vintRowIndexToAdd, grdBudget.ColCount), newCellsStyle)

            rintLastRowAddedIndex = vintRowIndexToAdd + 1

            grdBudget.IgnoreReadOnly = False

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnPayExpenses() As Boolean
        Dim blnValidReturn As Boolean
        Dim cPaidExpense As Model.PaidExpense
        Dim questionResult As MsgBoxResult

        questionResult = MsgBox("Vous ne pourrez plus revenir en arrière. Voulez-vous continuer?", MsgBoxStyle.YesNo, "Attention")

        If questionResult = MsgBoxResult.Yes Then

            Me.Cursor = Cursors.WaitCursor

            mcSQL = New MySQLController

            blnValidReturn = mcSQL.bln_BeginTransaction()

            Try
                For intRowIdx As Integer = 1 To grdBudget.RowCount

                    If Not blnValidReturn Then Exit For

                    If mcGridBudgetController.CellIsChecked(intRowIdx, mintGrdBudget_Sel_col) And CInt(grdBudget(intRowIdx, mintGrdBudget_Action_col).CellValue) = SyncfusionGridController.GridRowActions.UPDATE_ACTION Then

                        cPaidExpense = New Model.PaidExpense
                        cPaidExpense.DLMCommand = Form_Mode.INSERT_MODE
                        cPaidExpense.SQLController = mcSQL

                        cPaidExpense.Expense_ID = CInt(grdBudget(intRowIdx, mintGrdBudget_Exp_ID_col).CellValue)
                        cPaidExpense.ExpenseBillingDate_ID = CDate(grdBudget(intRowIdx, mintGrdBudget_Exp_NextBillingDate_col).CellValue)
                        cPaidExpense.AmountPaid = CDbl(IIf(grdBudget(intRowIdx, mintGrdBudget_PExp_AmountPaid_col).CellValue.ToString <> String.Empty, grdBudget(intRowIdx, mintGrdBudget_PExp_AmountPaid_col).CellValue, grdBudget(intRowIdx, mintGrdBudget_Exp_Amount_col).CellValue))
                        cPaidExpense.Comment = grdBudget(intRowIdx, mintGrdBudget_PExp_Comment_col).CellValue.ToString
                        cPaidExpense.Bud_ID = formController.Item_ID

                        If mcGridBudgetController.CellIsEmpty(intRowIdx, mintGrdBudget_PExp_DatePaid_col) Then

                            cPaidExpense.DatePaid = CDate(grdBudget(intRowIdx, mintGrdBudget_Exp_NextBillingDate_col).CellValue)
                        Else
                            cPaidExpense.DatePaid = CDate(grdBudget(intRowIdx, mintGrdBudget_PExp_DatePaid_col).CellValue)
                        End If

                        blnValidReturn = cPaidExpense.blnPaidExpense_Save()
                    End If
                Next

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            Finally
                mcSQL.bln_EndTransaction(blnValidReturn)
                Me.Cursor = Cursors.Default
            End Try

            If blnValidReturn Then

                blnValidReturn = blnGrdBudget_Load()
            End If
        Else
            blnValidReturn = True
        End If

        Return blnValidReturn
    End Function

    Private Sub CheckUncheckAll(ByVal vblnCheckAll As Boolean, ByVal vblnCheckTriStateOnly As Boolean)
        Dim strCurrentPeriod As String = mcGridBudgetController(mcGridBudgetController.GetSelectedRow, mintGrdBudget_NextIncomeDateToUse_col)

        Try
            For intRow As Integer = 1 To grdBudget.RowCount

                If mcGridBudgetController(intRow, mintGrdBudget_NextIncomeDateToUse_col) = strCurrentPeriod Then

                    grdBudget(intRow, mintGrdBudget_Sel_col).CellValue = vblnCheckAll 'TODO bln_UpdateRow
                End If
            Next

        Catch ex As Exception
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Function blnFormData_Save() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            If mcSQL.bln_BeginTransaction Then

                mcSQL.bln_RefreshFields()

                Select Case formController.FormMode
                    Case Form_Mode.INSERT_MODE
                        Select Case False
                            Case mcSQL.bln_AddField("Bud_Name", txtName.Text, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                            Case mcSQL.bln_ADOInsert("Budget", mintBudget_ID)
                            Case mintBudget_ID > 0
                            Case Else
                                blnValidReturn = True
                        End Select

                    Case Form_Mode.UPDATE_MODE
                        Select Case False
                            Case mcSQL.bln_AddField("Bud_Name", txtName.Text, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                            Case mcSQL.bln_ADOUpdate("Budget", "Budget.Bud_ID = " & mintBudget_ID)
                            Case Else
                                blnValidReturn = True
                        End Select

                    Case Form_Mode.DELETE_MODE
                        'TODO: Not allowed?

                End Select
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
        End Try

        Return blnValidReturn
    End Function

#End Region

#Region "Private events"

    Private Sub mcGrid_SetDisplay() Handles mcGridBudgetController.SetDisplay

        grdBudget.Model.Options.MergeCellsMode = GridMergeCellsMode.None
        grdBudget.ColStyles(mintGrdBudget_NextIncomeDateToUse_col).MergeCell = GridMergeCellDirection.None
        grdBudget.ColStyles(mintGrdBudget_Exp_NextBillingDate_col).MergeCell = GridMergeCellDirection.None

        grdBudget.ColWidths(mintGrdBudget_Exp_NextBillingDate_col) = 105
        grdBudget.ColWidths(mintGrdBudget_Exp_Name_col) = 205
        grdBudget.ColWidths(mintGrdBudget_Exp_Amount_col) = 100
        grdBudget.ColWidths(mintGrdBudget_PExp_AmountPaid_col) = 85
        grdBudget.ColWidths(mintGrdBudget_PExp_DatePaid_col) = 95
        grdBudget.ColWidths(mintGrdBudget_NextIncomeDateToUse_col) = 105
        grdBudget.ColWidths(mintGrdBudget_PExp_Comment_col) = 195
        grdBudget.ColWidths(mintGrdBudget_Sel_col) = 40

        mcGridBudgetController.SetColType_CheckBox(mintGrdBudget_Sel_col, False)

        mcGridBudgetController.SetColType_DateTimePicker(mintGrdBudget_PExp_DatePaid_col, True)

        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).CellValueType = GetType(Double)
        grdBudget.ColStyles(mintGrdBudget_PExp_AmountPaid_col).CellValueType = GetType(Double)
        grdBudget.ColStyles(mintGrdBudget_Exp_NextBillingDate_col).CellValueType = GetType(Date)

        grdBudget.ColStyles(mintGrdBudget_Exp_NextBillingDate_col).Format = gcAppCtrl.str_GetUserDateFormat
        grdBudget.ColStyles(mintGrdBudget_NextIncomeDateToUse_col).Format = gcAppCtrl.str_GetUserDateFormat
        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).Format = mConstants.DataFormat.CURRENCY
        grdBudget.ColStyles(mintGrdBudget_PExp_AmountPaid_col).Format = mConstants.DataFormat.CURRENCY

        grdBudget.Model.Options.MergeCellsMode = GridMergeCellsMode.SkipHiddencells Or GridMergeCellsMode.OnDemandCalculation Or GridMergeCellsMode.MergeRowsInColumn 'Or GridMergeCellsMode.MergeColumnsInRow
        grdBudget.ColStyles(mintGrdBudget_NextIncomeDateToUse_col).MergeCell = GridMergeCellDirection.RowsInColumn
        grdBudget.ColStyles(mintGrdBudget_Exp_NextBillingDate_col).MergeCell = GridMergeCellDirection.RowsInColumn

        grdBudget.TableStyle.VerticalAlignment = GridVerticalAlignment.Middle

        mcGridBudgetController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.EXTEND_LAST_COL

        For intCol As Integer = 1 To grdBudget.ColCount

            If intCol <> mintGrdBudget_PExp_DatePaid_col And intCol <> mintGrdBudget_PExp_Comment_col And intCol <> mintGrdBudget_PExp_AmountPaid_col And intCol <> mintGrdBudget_Sel_col Then

                grdBudget.ColStyles(intCol).ReadOnly = True
            End If
        Next

    End Sub

    Private Sub rbtnBiMensuel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnBiMensuel.CheckedChanged
        If rbtnBiMensuel.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppCtrl.str_GetUserDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnMensuelle_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnMensuelle.CheckedChanged
        If rbtnMensuelle.Checked And Not formController.FormIsLoading Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppCtrl.str_GetUserDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnDeuxMois_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDeuxMois.CheckedChanged
        If rbtnDeuxMois.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppCtrl.str_GetUserDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnHebdo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnHebdo.CheckedChanged
        If rbtnHebdo.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppCtrl.str_GetUserDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
        End If
    End Sub

    Private Sub myFormController_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        btnAfter.Text = ChrW(9660)
        btnBefore.Text = ChrW(9650)

        btnRefresh.SetToRefresh = False

        mintBudget_ID = formController.Item_ID

        Select Case False
            Case mcGridBudgetController.bln_Init(grdBudget)
            Case formController.FormMode <> Form_Mode.INSERT_MODE
                blnValidReturn = True

            Case blnFormData_Load()
            Case blnGrdBudget_Load()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If
    End Sub

    Private Sub btnRefresh_Click() Handles btnRefresh.Click

        If dtpFrom.Value > dtpTo.Value Then

            gcAppCtrl.ShowMessage(mintDtFromToRestrictions_msg)
        Else
            blnGrdBudget_Load()
        End If
    End Sub

    Private Sub btnBefore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBefore.Click

        Select Case True
            Case rbtnHebdo.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, -7, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)

            Case rbtnBiMensuel.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, -14, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)

            Case rbtnMensuelle.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, -1, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)

            Case rbtnDeuxMois.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, -2, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)

        End Select

    End Sub

    Private Sub btnAfter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAfter.Click

        Select Case True
            Case rbtnHebdo.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)

            Case rbtnBiMensuel.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)

            Case rbtnMensuelle.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)

            Case rbtnDeuxMois.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)

        End Select

    End Sub

    Private Sub rbtnNotPaid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnNotPaid.CheckedChanged

        btnPay.Enabled = rbtnNotPaid.Checked

        btnRefresh.SetToRefresh = True
    End Sub

    Private Sub grdBudget_CellDoubleClick(sender As Object, e As EventArgs) Handles grdBudget.CellDoubleClick
        Dim frmExpense As frmExpense

        Select Case mcGridBudgetController.GetSelectedCol
            Case mintGrdBudget_Exp_Name_col

                If Not String.IsNullOrEmpty(grdBudget(mcGridBudgetController.GetSelectedRow, mintGrdBudget_Exp_ID_col).CellValue.ToString) Then

                    frmExpense = New frmExpense
                    frmExpense.formController.ShowForm(formController.FormMode, CInt(grdBudget(mcGridBudgetController.GetSelectedRow, mintGrdBudget_Exp_ID_col).CellValue), True)
                    frmExpense = Nothing
                End If
        End Select
    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        blnPayExpenses()
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        CheckUncheckAll(True, True)
    End Sub

    Private Sub btnUnselectAll_Click(sender As Object, e As System.EventArgs) Handles btnUnselectAll.Click
        CheckUncheckAll(False, True)
    End Sub

    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged

        btnRefresh.SetToRefresh = True
    End Sub

    Private Sub rbtnAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAll.CheckedChanged

        If rbtnAll.IsHandleCreated Then

            btnRefresh.SetToRefresh = True

        End If
    End Sub

    Private Sub rbtnPaid_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnPaid.CheckedChanged
        btnRefresh.SetToRefresh = True
    End Sub

    Private Sub formController_SaveData(eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnFormData_Save()
    End Sub

    Private Sub formController_SetReadRights() Handles formController.SetReadRights
        Select Case formController.FormMode
            Case Form_Mode.INSERT_MODE
                btnRefresh.Enabled = False
                gbFilter.Enabled = False

        End Select
    End Sub

    Private Sub formController_ValidateForm(eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Dim strBudgetName As String = String.Empty

        eventArgs.IsValid = False
        Select Case formController.FormMode
            Case mConstants.Form_Mode.INSERT_MODE, mConstants.Form_Mode.UPDATE_MODE
                strBudgetName = MySQLController.str_ADOSingleLookUp("Budget.Bud_ID", "Budget", "Budget.Bud_Name = " & gcAppCtrl.str_FixStringForSQL(txtName.Text) & " AND Budget.Bud_ID <> " & formController.Item_ID)

                Select Case False
                    Case txtName.Text <> String.Empty
                        gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                        txtName.Focus()
                        txtName.SelectAll()

                    Case strBudgetName = String.Empty
                        gcAppCtrl.ShowMessage(mConstants.Validation_Message.UNIQUE_ATTRIBUTE)
                        txtName.Focus()
                        txtName.SelectAll()

                    Case Else
                        eventArgs.IsValid = True

                End Select

            Case mConstants.Form_Mode.DELETE_MODE
                MsgBox("Pas encore disponible", MsgBoxStyle.Information)

        End Select
        
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub grdBudget_CheckBoxClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs) Handles grdBudget.CheckBoxClick

        If mcGridBudgetController.CellIsChecked(e.RowIndex, e.ColIndex) Then

            formController.ChangeMade = False
            e.Cancel = True
        Else
            formController.ChangeMade = True
        End If
    End Sub

#End Region



End Class
