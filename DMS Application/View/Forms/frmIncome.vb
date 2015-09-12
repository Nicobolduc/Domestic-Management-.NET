Public Class frmIncome


    'Private class members
    Private mcSQL As MySQLController
    Private mcIncomeModel As Model.Income
    Private WithEvents mcGridPeriodController As SyncfusionGridController

    'GrdPeriod
    Private Const mintGrdPeriod_Action_col As Short = 1
    Private Const mintGrdPeriod_DtBegin_col As Short = 2
    Private Const mintGrdPeriod_DtEnd_col As Short = 3
    Private Const mintGrdPeriod_DtPeriod_col As Short = 4
    Private Const mintGrdPeriod_Amount_col As Short = 5
    Private Const mintGrdPeriod_Active_col As Short = 6

    'Messages
    Private Const mintDateBeginRestriction_msg As Short = 22
    Private Const mintDateEndRestriction_msg As Short = 23
    Private Const mintDateBeginUsed_msg As Short = 24
    Private Const mintDateEndUsed_msg As Short = 25

    'Private members
    Private mintSelectedRow As Integer


#Region "Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        mcGridPeriodController = New SyncfusionGridController
    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcIncomeModel = gcAppCtrl.GetCoreModelController.GetFinanceController.GetIncome(formController.Item_ID)

            If Not mcIncomeModel Is Nothing Then

                txtName.Text = mcIncomeModel.Name

                cboFrequency.SelectedValue = CInt(mcIncomeModel.Period)
                cboBudget.SelectedValue = CInt(mcIncomeModel.Budget_ID)
                chkMainIncome.Checked = mcIncomeModel.IsMainIncome

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboFrequency_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT Period.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Period.Per_Name " & vbCrLf
            strSQL = strSQL & " FROM Period " & vbCrLf
            strSQL = strSQL & " ORDER BY Period.Per_ID " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "Per_ID", "Per_Name", False, cboFrequency)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboBudget_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT Budget.Bud_ID, " & vbCrLf
            strSQL = strSQL & "        Budget.Bud_Name " & vbCrLf
            strSQL = strSQL & " FROM Budget " & vbCrLf
            strSQL = strSQL & " ORDER BY Budget.Bud_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "Bud_ID", "Bud_Name", False, cboBudget)

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
                Case blnSyncIncomeModel()
                Case mcSQL.bln_BeginTransaction()
                Case mcIncomeModel.blnIncome_Save()
                Case Else
                    formController.Item_ID = mcIncomeModel.ID
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

    Private Function blnGrdPeriod_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT " & SyncfusionGridController.GridRowActions.NO_ACTION & " AS ActionCol, " & vbCrLf
            strSQL = strSQL & "         IncomePeriod.IncP_DtBegin, " & vbCrLf
            strSQL = strSQL & "         IncomePeriod.IncP_DtEnd, " & vbCrLf
            strSQL = strSQL & "         IncomePeriod.IncP_DtPeriod, " & vbCrLf
            strSQL = strSQL & "         IncomePeriod.IncP_Amount, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN IncomePeriod.IncP_DtEnd IS NULL THEN 'TRUE' ELSE 'FALSE' END " & vbCrLf
            strSQL = strSQL & " FROM IncomePeriod " & vbCrLf
            strSQL = strSQL & " WHERE IncomePeriod.Inc_ID = " & mcIncomeModel.ID & vbCrLf
            strSQL = strSQL & " ORDER BY IncomePeriod.IncP_DtBegin ASC " & vbCrLf

            blnValidReturn = mcGridPeriodController.bln_FillData(strSQL)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Public Function blnSyncIncomeModel() As Boolean
        Dim blnValidReturn As Boolean
        Dim cIncPeriod As Model.Income.IncomePeriod

        Try
            If mcIncomeModel Is Nothing Then

                mcIncomeModel = New Model.Income
            End If

            mcIncomeModel.SQLController = mcSQL
            mcIncomeModel.DLMCommand = formController.FormMode
            mcIncomeModel.ID = formController.Item_ID
            mcIncomeModel.Name = txtName.Text
            mcIncomeModel.Period = CType(cboFrequency.SelectedValue, Period)
            mcIncomeModel.IsMainIncome = chkMainIncome.Checked
            mcIncomeModel.Budget_ID = CInt(cboBudget.SelectedValue)

            mcIncomeModel.LstIncPeriod = New List(Of Model.Income.IncomePeriod)

            For intRowIdx As Integer = 1 To grdPeriod.RowCount

                If CInt(grdPeriod(intRowIdx, mintGrdPeriod_Action_col).CellValue) <> SyncfusionGridController.GridRowActions.NO_ACTION Then

                    cIncPeriod = New Model.Income.IncomePeriod

                    cIncPeriod.Amount = Math.Round(Val(grdPeriod(grdPeriod.RowCount, mintGrdPeriod_Amount_col).CellValue), 2)
                    cIncPeriod.DateBegin = gcAppCtrl.GetFormatedDate(grdPeriod(grdPeriod.RowCount, mintGrdPeriod_DtBegin_col).FormattedText)

                    If Not mcGridPeriodController(grdPeriod.RowCount, mintGrdPeriod_DtEnd_col) = String.Empty Then

                        cIncPeriod.DateEnd = gcAppCtrl.GetFormatedDate(grdPeriod(grdPeriod.RowCount, mintGrdPeriod_DtEnd_col).FormattedText)
                    End If

                    cIncPeriod.DLMCommand = CType(grdPeriod(grdPeriod.RowCount, mintGrdPeriod_Action_col).CellValue, Form_Mode)

                    mcIncomeModel.LstIncPeriod.Add(cIncPeriod)
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

    Private Sub myFormController_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        Select Case False
            Case mcGridPeriodController.bln_Init(grdPeriod, btnAddRow, btnRemoveRow)
            Case blnCboFrequency_Load()
            Case blnCboBudget_Load()
            Case formController.FormMode <> mConstants.Form_Mode.INSERT_MODE
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

    Private Sub cboInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFrequency.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub formController_SetReadRights() Handles formController.SetReadRights
        btnAddRow.Enabled = True
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Dim strMainIncome As String = String.Empty

        If chkMainIncome.Checked Then
            strMainIncome = MySQLController.str_ADOSingleLookUp("Inc_IsMain", "Income", "Inc_IsMain = 1 AND Income.Inc_ID <> " & formController.Item_ID)
        End If

        Select Case False
            Case txtName.Text <> String.Empty
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboFrequency.SelectedIndex > -1
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboFrequency.DroppedDown = True
                cboFrequency.Focus()

            Case cboBudget.SelectedIndex > -1
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboBudget.DroppedDown = True
                cboBudget.Focus()

            Case strMainIncome = String.Empty
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.UNIQUE_ATTRIBUTE, MsgBoxStyle.Information)
                chkMainIncome.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub chkMainIncome_CheckedChanged(sender As Object, e As EventArgs) Handles chkMainIncome.CheckedChanged
        formController.ChangeMade = True
    End Sub

    Private Sub mcGridPeriodController_SetDisplay() Handles mcGridPeriodController.SetDisplay

        mcGridPeriodController.SetColType_CheckBox(mintGrdPeriod_Active_col)

        mcGridPeriodController.SetColType_DateTimePicker(mintGrdPeriod_DtBegin_col, False)
        mcGridPeriodController.SetColType_DateTimePicker(mintGrdPeriod_DtEnd_col, True)
        mcGridPeriodController.SetColType_DateTimePicker(mintGrdPeriod_DtPeriod_col, True)

        grdPeriod.ColWidths(mintGrdPeriod_Active_col) = 60
        grdPeriod.ColWidths(mintGrdPeriod_DtBegin_col) = 75
        grdPeriod.ColWidths(mintGrdPeriod_DtEnd_col) = 75
        grdPeriod.ColWidths(mintGrdPeriod_DtPeriod_col) = 75
        grdPeriod.ColWidths(mintGrdPeriod_Active_col) = 40

        grdPeriod.ColStyles(mintGrdPeriod_Amount_col).CellValueType = GetType(Double)
        grdPeriod.ColStyles(mintGrdPeriod_Amount_col).Format = mConstants.DataFormat.CURRENCY

        grdPeriod.ColStyles(mintGrdPeriod_Active_col).ReadOnly = True

        For intRowRdx As Integer = 1 To grdPeriod.RowCount

            If intRowRdx <> grdPeriod.RowCount Then

                grdPeriod.Item(intRowRdx, mintGrdPeriod_DtBegin_col).ReadOnly = True
                grdPeriod.Item(intRowRdx, mintGrdPeriod_DtEnd_col).ReadOnly = True
                grdPeriod.Item(intRowRdx, mintGrdPeriod_Amount_col).ReadOnly = True
            End If
        Next

    End Sub

#End Region



    Private Sub grdPeriod_CellClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs) Handles grdPeriod.CellClick
        mintSelectedRow = e.RowIndex
    End Sub

    Private Sub grdPeriod_CurrentCellAcceptedChanges(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdPeriod.CurrentCellAcceptedChanges
        Dim strPaidExpense As String = String.Empty

        Select Case mcGridPeriodController.GetSelectedCol
            Case mintGrdPeriod_DtBegin_col

                If mintSelectedRow > 1 And Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtBegin_col)) Then

                    If gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) <= gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow - 1, mintGrdPeriod_DtEnd_col).FormattedText) Then

                        gcAppCtrl.ShowMessage(mintDateBeginRestriction_msg)
                        e.Cancel = True
                    End If
                End If

                'If Not e.Cancel And Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtBegin_col)) Then

                '    strPaidExpense = MySQLController.str_ADOSingleLookUp("PExp_ID", "PaidExpense", "Exp_BilledDate <= " & gcAppCtrl.str_FixDateForSQL(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) & " AND Exp_ID = " & formController.Item_ID & " AND NOT EXISTS(SELECT * FROM ExpensePeriod WHERE ExpensePeriod.ExpA_DtEnd < " & gcAppCtrl.str_FixDateForSQL(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) & ") LIMIT 1")

                '    If strPaidExpense <> String.Empty Then

                '        gcAppCtrl.ShowMessage(mintDateBeginUsed_msg)
                '        e.Cancel = True
                '    End If
                'End If

            Case mintGrdPeriod_DtEnd_col

                If Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtEnd_col)) AndAlso gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow, mintGrdPeriod_DtEnd_col).FormattedText) < gcAppCtrl.GetFormatedDate(grdPeriod(mintSelectedRow, mintGrdPeriod_DtBegin_col).FormattedText) Then

                    gcAppCtrl.ShowMessage(mintDateEndRestriction_msg)
                    e.Cancel = True
                End If

                'If Not e.Cancel And Not String.IsNullOrEmpty(mcGridPeriodController(mintSelectedRow, mintGrdPeriod_DtEnd_col)) Then

                '    strPaidExpense = MySQLController.str_ADOSingleLookUp("PExp_ID", "PaidExpense", "Exp_BilledDate > " & gcAppCtrl.str_FixDateForSQL(grdPeriod(mintSelectedRow, mintGrdPeriod_DtEnd_col).FormattedText) & " AND Exp_ID = " & formController.Item_ID & " LIMIT 1")

                '    If strPaidExpense <> String.Empty Then

                '        gcAppCtrl.ShowMessage(mintDateEndUsed_msg)
                '        e.Cancel = True
                '    End If
                'End If
        End Select

        If Not e.Cancel Then

            formController.ChangeMade = True
        End If
    End Sub

    Private Sub btnAddRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRow.Click
        btnAddRow.Enabled = False
    End Sub

    Private Sub mcGridPeriodController_ValidateData(ByVal eventArgs As ValidateGridEventArgs) Handles mcGridPeriodController.ValidateData

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

    Private Sub cboBudget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBudget.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub
End Class