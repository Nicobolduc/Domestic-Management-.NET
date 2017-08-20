Public Class frmExpense

    'GrdPeriod
    Private Const mintGrdPeriod_Action_col As Short = 1
    Private Const mintGrdPeriod_DtBegin_col As Short = 2
    Private Const mintGrdPeriod_DtEnd_col As Short = 3
    Private Const mintGrdPeriod_Amount_col As Short = 4
    Private Const mintGrdPeriod_Current_col As Short = 5

    'GrdBudget
    Private Const mintGrdBudget_Action_col As Short = 1
    Private Const mintGrdBudget_Bud_ID_col As Short = 2
    Private Const mintGrdBudget_Name_col As Short = 3
    Private Const mintGrdBudget_Sel_col As Short = 4

    Private mintSelectedRow As Integer

    'Messages
    Private Const mintDateBeginRestriction_msg As Short = 22
    Private Const mintDateEndRestriction_msg As Short = 23
    Private Const mintDateBeginUsed_msg As Short = 24
    Private Const mintDateEndUsed_msg As Short = 25

    'Private class members
    Private mcSQL As MySQLController
    Private WithEvents mcGridPeriodController As SyncfusionGridController
    Private WithEvents mcGridBudgetController As SyncfusionGridController
    Private mcExpenseModel As Model.Expense


#Region "Constructors"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mcGridPeriodController = New SyncfusionGridController
        mcGridBudgetController = New SyncfusionGridController
    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcExpenseModel = gcAppCtrl.GetCoreModelController.GetFinanceController.GetExpense(formController.Item_ID)

            If Not mcExpenseModel Is Nothing Then

                txtName.Text = mcExpenseModel.Name
                chkFixed.Checked = mcExpenseModel.Fixed
                cboInterval.SelectedValue = CInt(mcExpenseModel.Period)
                cboType.SelectedValue = CInt(mcExpenseModel.Type.ID)
                cboType.BackColor = CType(IIf(mcExpenseModel.Type.ArgbColor <> 0, Color.FromArgb(mcExpenseModel.Type.ArgbColor), Control.DefaultBackColor), Color)

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdPeriod_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT " & SyncfusionGridController.GridRowActions.NO_ACTION & " AS ActionCol, " & vbCrLf
            strSQL = strSQL & "         ExpensePeriod.ExpP_DtBegin, " & vbCrLf
            strSQL = strSQL & "         ExpensePeriod.ExpP_DtEnd, " & vbCrLf
            strSQL = strSQL & "         ExpensePeriod.ExpP_Amount, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN ExpensePeriod.ExpP_DtEnd IS NULL THEN 'TRUE' ELSE 'FALSE' END " & vbCrLf
            strSQL = strSQL & " FROM ExpensePeriod " & vbCrLf
            strSQL = strSQL & " WHERE ExpensePeriod.Exp_ID = " & mcExpenseModel.ID & vbCrLf
            strSQL = strSQL & " ORDER BY ExpensePeriod.ExpP_DtBegin ASC " & vbCrLf

            blnValidReturn = mcGridPeriodController.bln_FillData(strSQL)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdBudget_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT " & SyncfusionGridController.GridRowActions.NO_ACTION & " AS ActionCol, " & vbCrLf
            strSQL = strSQL & "         Budget.Bud_ID, " & vbCrLf
            strSQL = strSQL & "         Budget.Bud_Name, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN Bud_Exp.Exp_ID IS NOT NULL THEN 'TRUE' ELSE 'FALSE' END AS SelCol " & vbCrLf
            strSQL = strSQL & " FROM Budget " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN Bud_Exp ON Bud_Exp.Bud_ID = Budget.Bud_ID AND Bud_Exp.Exp_ID = " & formController.Item_ID & vbCrLf
            strSQL = strSQL & " ORDER BY Budget.Bud_Name " & vbCrLf

            blnValidReturn = mcGridBudgetController.bln_FillData(strSQL)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboInterval_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT Period.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Period.Per_Name " & vbCrLf
            strSQL = strSQL & " FROM Period " & vbCrLf
            strSQL = strSQL & " ORDER BY Period.Per_ID " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "Per_ID", "Per_Name", False, cboInterval)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboType_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT ExpenseType.ExpT_ID, " & vbCrLf
            strSQL = strSQL & "        ExpenseType.ExpT_Name " & vbCrLf
            strSQL = strSQL & " FROM ExpenseType " & vbCrLf
            strSQL = strSQL & " ORDER BY ExpenseType.ExpT_ID " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "ExpT_ID", "ExpT_Name", False, cboType)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnFormData_Save() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            Select Case False
                Case blnSyncExpenseModel()
                Case mcSQL.bln_BeginTransaction()
                Case mcExpenseModel.blnExpense_Save()
                Case Else
                    formController.Item_ID = mcExpenseModel.ID
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
            mcSQL = Nothing
        End Try

        Return blnValidReturn
    End Function

    Public Function blnSyncExpenseModel() As Boolean
        Dim blnValidReturn As Boolean
        Dim cExpPeriod As Model.Expense.ExpensePeriod
        Dim cBudget As Model.Budget

        Try
            If mcExpenseModel Is Nothing Then

                mcExpenseModel = New Model.Expense
            End If

            mcExpenseModel.SQLController = mcSQL
            mcExpenseModel.DLMCommand = formController.FormMode
            mcExpenseModel.ID = formController.Item_ID
            mcExpenseModel.Name = txtName.Text
            mcExpenseModel.Period = CType(cboInterval.SelectedValue, Period)
            mcExpenseModel.Fixed = chkFixed.Checked

            mcExpenseModel.Type = New Model.ExpenseType
            mcExpenseModel.Type.ID = CInt(cboType.SelectedValue)

            mcExpenseModel.LstExpPeriod = New List(Of Model.Expense.ExpensePeriod)

            For intRowIdx As Integer = 1 To grdPeriod.RowCount

                If CInt(grdPeriod(intRowIdx, mintGrdPeriod_Action_col).CellValue) <> SyncfusionGridController.GridRowActions.NO_ACTION Then

                    cExpPeriod = New Model.Expense.ExpensePeriod

                    cExpPeriod.Amount = Math.Round(Val(grdPeriod(intRowIdx, mintGrdPeriod_Amount_col).CellValue), 2)
                    cExpPeriod.DateBegin = gcAppCtrl.GetFormatedDate(grdPeriod(intRowIdx, mintGrdPeriod_DtBegin_col).FormattedText)

                    If Not mcGridPeriodController(intRowIdx, mintGrdPeriod_DtEnd_col) = String.Empty Then

                        cExpPeriod.DateEnd = gcAppCtrl.GetFormatedDate(grdPeriod(intRowIdx, mintGrdPeriod_DtEnd_col).FormattedText)
                    End If

                    cExpPeriod.DLMCommand = CType(grdPeriod(intRowIdx, mintGrdPeriod_Action_col).CellValue, Form_Mode)

                    mcExpenseModel.LstExpPeriod.Add(cExpPeriod)
                End If
            Next

            mcExpenseModel.LstBudget = New List(Of Model.Budget)

            For intRowIdx As Integer = 1 To grdBudget.RowCount

                If mcGridBudgetController.CellIsChecked(intRowIdx, mintGrdBudget_Sel_col) Then

                    cBudget = New Model.Budget

                    cBudget.ID = CInt(grdBudget(intRowIdx, mintGrdBudget_Bud_ID_col).CellValue)
                    cBudget.Name = grdBudget(intRowIdx, mintGrdBudget_Name_col).CellValue.ToString

                    cBudget.DLMCommand = CType(grdBudget(grdBudget.RowCount, mintGrdBudget_Action_col).CellValue, Form_Mode)

                    mcExpenseModel.LstBudget.Add(cBudget)
                End If
            Next

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region

#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        mintSelectedRow = 0

        Select Case False
            Case mcGridPeriodController.bln_Init(grdPeriod, btnAddRow)
            Case mcGridBudgetController.bln_Init(grdBudget)
            Case blnCboInterval_Load()
            Case blnCboType_Load()
            Case blnGrdBudget_Load()
            Case formController.FormMode <> mConstants.Form_Mode.INSERT_MODE
                mcGridPeriodController.AddRow()
                blnValidReturn = True
            Case blnFormData_Load()
            Case blnGrdPeriod_Load()
            Case Else
                blnValidReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnFormData_Save()
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInterval.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub formController_SetReadRights() Handles formController.SetReadRights
        btnAddRow.Enabled = True
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Dim strOtherExpenseName As String = String.Empty

        strOtherExpenseName = MySQLController.str_ADOSingleLookUp("Expense.Exp_ID", "Expense", "Expense.Exp_Name = " & gcAppCtrl.str_FixStringForSQL(txtName.Text) & " AND Expense.Exp_ID <> " & formController.Item_ID)

        Select Case False
            Case mcGridPeriodController.bln_ValidateGridEvent
            Case txtName.Text <> String.Empty
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case strOtherExpenseName = String.Empty
                eventArgs.IsValid = False
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.UNIQUE_ATTRIBUTE)
                txtName.Focus()
                txtName.SelectAll()

            Case cboInterval.SelectedIndex > -1
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case cboType.SelectedIndex > -1
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case Else

                If formController.FormMode = Form_Mode.DELETE_MODE Then

                    eventArgs.IsValid = MySQLController.bln_CheckReferenceIntegrity("Expense", "Exp_ID", formController.Item_ID)
                Else
                    eventArgs.IsValid = True
                End If
        End Select
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboType.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub chkFixe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFixed.CheckedChanged
        formController.ChangeMade = True
    End Sub

    Private Sub mcGridAmountController_SetDisplay() Handles mcGridPeriodController.SetDisplay

        grdPeriod.ColWidths(mintGrdPeriod_Current_col) = 60

        mcGridPeriodController.SetColType_CheckBox(mintGrdPeriod_Current_col)
        mcGridPeriodController.SetColType_DateTimePicker(mintGrdPeriod_DtBegin_col, False)
        mcGridPeriodController.SetColType_DateTimePicker(mintGrdPeriod_DtEnd_col, True)

        grdPeriod.ColStyles(mintGrdPeriod_Amount_col).CellValueType = GetType(Double)

        grdPeriod.ColStyles(mintGrdPeriod_Amount_col).Format = mConstants.DataFormat.CURRENCY

        grdPeriod.ColStyles(mintGrdPeriod_Current_col).ReadOnly = True

        For intRowRdx As Integer = 1 To grdPeriod.RowCount

            If intRowRdx <> grdPeriod.RowCount Then

                grdPeriod.Item(intRowRdx, mintGrdPeriod_DtBegin_col).ReadOnly = True
                grdPeriod.Item(intRowRdx, mintGrdPeriod_DtEnd_col).ReadOnly = True
                grdPeriod.Item(intRowRdx, mintGrdPeriod_Amount_col).ReadOnly = True
            End If
        Next

    End Sub

    Private Sub grdAmount_CellClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs) Handles grdPeriod.CellClick
        mintSelectedRow = e.RowIndex
    End Sub

    Private Sub grdAmount_CurrentCellAcceptedChanges(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdPeriod.CurrentCellAcceptedChanges
        Dim strPaidExpense As String = String.Empty

        Select Case mcGridPeriodController.GetSelectedCol
            Case mintGrdPeriod_DtBegin_col

                If mintSelectedRow > 1 And Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtBegin_col)) Then

                    If gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) <= gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow - 1, mintGrdPeriod_DtEnd_col).FormattedText) Then

                        gcAppCtrl.ShowMessage(mintDateBeginRestriction_msg)
                        e.Cancel = True
                    End If
                End If

                If Not e.Cancel And Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtBegin_col)) Then

                    strPaidExpense = MySQLController.str_ADOSingleLookUp("PExp_ID", "PaidExpense", "Exp_BilledDate <= " & gcAppCtrl.str_FixDateForSQL(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) & " AND Exp_ID = " & formController.Item_ID & " AND NOT EXISTS(SELECT * FROM ExpensePeriod WHERE ExpensePeriod.ExpP_DtEnd < " & gcAppCtrl.str_FixDateForSQL(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) & ") LIMIT 1")

                    If strPaidExpense <> String.Empty Then

                        gcAppCtrl.ShowMessage(mintDateBeginUsed_msg)
                        e.Cancel = True
                    End If
                End If

            Case mintGrdPeriod_DtEnd_col

                If Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtEnd_col)) AndAlso gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow, mintGrdPeriod_DtEnd_col).FormattedText) < gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) Then

                    gcAppCtrl.ShowMessage(mintDateEndRestriction_msg)
                    e.Cancel = True
                End If

                If Not e.Cancel And Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtEnd_col)) Then

                    strPaidExpense = MySQLController.str_ADOSingleLookUp("PExp_ID", "PaidExpense", "Exp_BilledDate > " & gcAppCtrl.str_FixDateForSQL(grdPeriod(mintSelectedRow, mintGrdPeriod_DtEnd_col).FormattedText) & " AND Exp_ID = " & formController.Item_ID & " LIMIT 1")

                    If strPaidExpense <> String.Empty Then

                        gcAppCtrl.ShowMessage(mintDateEndUsed_msg)
                        e.Cancel = True
                    End If
                End If
        End Select

        If Not e.Cancel Then

            formController.ChangeMade = True
        End If
    End Sub

    Private Sub mcGridAmountController_ValidateData(ByVal eventArgs As ValidateGridEventArgs) Handles mcGridPeriodController.ValidateGridData

        For intRowIdx As Integer = 1 To grdPeriod.RowCount

            eventArgs.IsValid = False
            Select Case True
                Case String.IsNullOrEmpty(mcGridPeriodController(intRowIdx, mintGrdPeriod_Amount_col))
                    gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                    mcGridPeriodController.SetSelectedCell(intRowIdx, mintGrdPeriod_Amount_col)

                Case String.IsNullOrEmpty(mcGridPeriodController(intRowIdx, mintGrdPeriod_DtBegin_col))
                    gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                    mcGridPeriodController.SetSelectedCell(intRowIdx, mintGrdPeriod_DtBegin_col)

                Case intRowIdx < grdPeriod.RowCount AndAlso String.IsNullOrEmpty(mcGridPeriodController(intRowIdx, mintGrdPeriod_DtEnd_col))
                    gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                    mcGridPeriodController.SetSelectedCell(intRowIdx, mintGrdPeriod_DtEnd_col)

                Case Else
                    eventArgs.IsValid = True

            End Select

            If Not eventArgs.IsValid Then Exit For
        Next

    End Sub

    Private Sub btnAddRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRow.Click

        btnAddRow.Enabled = False
    End Sub

    Private Sub mcGridBudgetController_SetDisplay() Handles mcGridBudgetController.SetDisplay
        grdBudget.ColWidths(mintGrdBudget_Name_col) = 200

        mcGridBudgetController.SetColType_CheckBox(mintGrdBudget_Sel_col)

        grdBudget.ColStyles(mintGrdBudget_Name_col).ReadOnly = True

        mcGridBudgetController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.EXTEND_LAST_COL
    End Sub

#End Region


End Class