﻿Public Class frmExpense

    'Private members
    Private Const mintGrdAmount_Action_col As Short = 1
    Private Const mintGrdAmount_DtBegin_col As Short = 2
    Private Const mintGrdAmount_DtEnd_col As Short = 3
    Private Const mintGrdAmount_Amount_col As Short = 4
    Private Const mintGrdAmount_Active_col As Short = 5

    Private mintSelectedRow As Integer

    'Messages
    Private Const mintDateBeginRestriction_msg As Short = 22
    Private Const mintDateEndRestriction_msg As Short = 23

    'Private class members
    Private mcSQL As MySQLController
    Private WithEvents mcGridAmountController As SyncfusionGridController
    Private mcExpenseModel As Model.Expense



#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcExpenseModel = gcAppController.GetCoreModelController.GetFinanceController.GetExpense(formController.Item_ID)

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
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdAmount_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT " & SyncfusionGridController.GridRowActions.NO_ACTION & " AS ActionCol, " & vbCrLf
            strSQL = strSQL & "         ExpenseAmount.ExpA_DtBegin, " & vbCrLf
            strSQL = strSQL & "         ExpenseAmount.ExpA_DtEnd, " & vbCrLf
            strSQL = strSQL & "         ExpenseAmount.ExpA_Amount, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN ExpenseAmount.ExpA_DtEnd IS NULL THEN 'TRUE' ELSE 'FALSE' END " & vbCrLf
            strSQL = strSQL & " FROM ExpenseAmount " & vbCrLf
            strSQL = strSQL & " WHERE ExpenseAmount.Exp_ID = " & mcExpenseModel.ID & vbCrLf
            strSQL = strSQL & " ORDER BY ExpenseAmount.ExpA_DtBegin ASC " & vbCrLf

            blnValidReturn = mcGridAmountController.bln_FillData(strSQL)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
            mcSQL = Nothing
        End Try

        Return blnValidReturn
    End Function

    Public Function blnSyncExpenseModel() As Boolean
        Dim blnValidReturn As Boolean
        Dim cExpAmount As Model.Expense.ExpenseAmount

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

            mcExpenseModel.LstExpAmount = New List(Of Model.Expense.ExpenseAmount)

            For intRowIdx As Integer = 1 To grdAmount.RowCount

                If CInt(grdAmount(intRowIdx, mintGrdAmount_Action_col).CellValue) <> SyncfusionGridController.GridRowActions.NO_ACTION Then

                    cExpAmount = New Model.Expense.ExpenseAmount

                    cExpAmount.Amount = Math.Round(Val(grdAmount(grdAmount.RowCount, mintGrdAmount_Amount_col).CellValue), 2)
                    cExpAmount.DateBegin = CDate(grdAmount(grdAmount.RowCount, mintGrdAmount_DtBegin_col).CellValue)

                    If Not mcGridAmountController(grdAmount.RowCount, mintGrdAmount_DtEnd_col) = String.Empty Then

                        cExpAmount.DateEnd = CDate(grdAmount(grdAmount.RowCount, mintGrdAmount_DtEnd_col).CellValue)
                    End If

                    cExpAmount.DLMCommand = CType(grdAmount(grdAmount.RowCount, mintGrdAmount_Action_col).CellValue, Form_Mode)

                    mcExpenseModel.LstExpAmount.Add(cExpAmount)
                End If
            Next
            
            mcExpenseModel.Type = New Model.ExpenseType
            mcExpenseModel.Type.ID = CInt(cboType.SelectedValue)

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region

#Region "Private events"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mcGridAmountController = New SyncfusionGridController
    End Sub

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        mintSelectedRow = 0

        Select Case False
            Case mcGridAmountController.bln_Init(grdAmount, btnAddRow)
            Case blnCboInterval_Load()
            Case blnCboType_Load()
            Case formController.FormMode <> mConstants.Form_Mode.INSERT_MODE
                mcGridAmountController.AddRow()
                blnValidReturn = True
            Case blnFormData_Load()
            Case blnGrdAmount_Load()
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
        Dim gridEventArgs As New ValidateGridEventArgs

        mcGridAmountController_ValidateData(gridEventArgs)

        Select Case False
            Case gridEventArgs.IsValid
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboInterval.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case cboType.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
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

    Private Sub dtpBillDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        formController.ChangeMade = True
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboType.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub chkFixe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFixed.CheckedChanged
        formController.ChangeMade = True
    End Sub

    Private Sub mcGridAmountController_SetDisplay() Handles mcGridAmountController.SetDisplay

        grdAmount.ColWidths(mintGrdAmount_Active_col) = 60

        mcGridAmountController.SetColType_CheckBox(mintGrdAmount_Active_col)
        mcGridAmountController.SetColType_DateTimePicker(mintGrdAmount_DtBegin_col, False)
        mcGridAmountController.SetColType_DateTimePicker(mintGrdAmount_DtEnd_col, True)

        grdAmount.ColStyles(mintGrdAmount_Amount_col).CellValueType = GetType(Double)
        grdAmount.ColStyles(mintGrdAmount_DtBegin_col).CellValueType = GetType(Date)
        grdAmount.ColStyles(mintGrdAmount_DtEnd_col).CellValueType = GetType(Date)

        grdAmount.ColStyles(mintGrdAmount_Amount_col).Format = mConstants.DataFormat.CURRENCY
        grdAmount.ColStyles(mintGrdAmount_DtBegin_col).Format = gcAppController.str_GetUserDateFormat
        grdAmount.ColStyles(mintGrdAmount_DtEnd_col).Format = gcAppController.str_GetUserDateFormat

        grdAmount.ColStyles(mintGrdAmount_Active_col).ReadOnly = True

        For intRowRdx As Integer = 1 To grdAmount.RowCount

            If intRowRdx <> grdAmount.RowCount Then

                grdAmount.Item(intRowRdx, mintGrdAmount_DtBegin_col).ReadOnly = True
                grdAmount.Item(intRowRdx, mintGrdAmount_DtEnd_col).ReadOnly = True
                grdAmount.Item(intRowRdx, mintGrdAmount_Amount_col).ReadOnly = True
            End If
        Next

    End Sub

    Private Sub grdAmount_CellClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs) Handles grdAmount.CellClick
        mintSelectedRow = e.RowIndex
    End Sub

    Private Sub grdAmount_CurrentCellAcceptedChanges(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdAmount.CurrentCellAcceptedChanges
        Dim strPaidExpense As String = String.Empty

        Select Case True
            Case String.IsNullOrEmpty(mcGridAmountController(mintSelectedRow, mcGridAmountController.GetSelectedCol))
            Case mcGridAmountController.GetSelectedCol <> mintGrdAmount_DtBegin_col
            Case mcGridAmountController.GetSelectedCol <> mintGrdAmount_DtEnd_col
            Case Else
                grdAmount(mintSelectedRow, mcGridAmountController.GetSelectedCol).CellValue = Format(CDate(grdAmount(mintSelectedRow, mcGridAmountController.GetSelectedCol).CellValue), gcAppController.str_GetUserDateFormat)
        End Select

        Select Case mcGridAmountController.GetSelectedCol
            Case mintGrdAmount_DtBegin_col

                Select Case False
                    Case mintSelectedRow > 1
                    Case Not String.IsNullOrEmpty(mcGridAmountController(mintSelectedRow, mintGrdAmount_DtBegin_col))
                    Case CDate(grdAmount(mintSelectedRow, mintGrdAmount_DtBegin_col).CellValue) <= CDate(grdAmount(mintSelectedRow - 1, mintGrdAmount_DtEnd_col).CellValue)
                    Case Else
                        gcAppController.ShowMessage(mintDateBeginRestriction_msg)
                        e.Cancel = True
                End Select

                If Not e.Cancel Then

                    strPaidExpense = MySQLController.str_ADOSingleLookUp("PExp_ID", "PaidExpense", "Exp_BilledDate <= " & gcAppController.str_FixStringForSQL(gcAppController.str_SetDateToMidnightServerFormat(grdAmount(grdAmount.RowCount, mintGrdAmount_DtBegin_col).CellValue.ToString).ToString) & " AND Exp_ID = " & formController.Item_ID) 'TODO Mettre dans un event avec valeur avant et apres ou ben you know

                    If strPaidExpense <> String.Empty Then

                        gcAppController.ShowMessage(mConstants.Error_Message.ERROR_ITEM_USED_MSG)
                        e.Cancel = True
                    End If
                End If

            Case mintGrdAmount_DtEnd_col

                If Not String.IsNullOrEmpty(mcGridAmountController(mintSelectedRow, mintGrdAmount_DtEnd_col)) AndAlso CDate(grdAmount(mintSelectedRow, mintGrdAmount_DtEnd_col).CellValue) < CDate(grdAmount(mintSelectedRow, mintGrdAmount_DtBegin_col).CellValue) Then

                    gcAppController.ShowMessage(mintDateEndRestriction_msg)
                    e.Cancel = True
                End If
        End Select

        If Not e.Cancel Then

            formController.ChangeMade = True
        End If
    End Sub

    Private Sub mcGridAmountController_ValidateData(eventArgs As ValidateGridEventArgs) Handles mcGridAmountController.ValidateData

        For intRowIdx As Integer = 1 To grdAmount.RowCount

            eventArgs.IsValid = False
            Select Case True
                Case String.IsNullOrEmpty(mcGridAmountController(intRowIdx, mintGrdAmount_Amount_col))
                    gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                    mcGridAmountController.SetSelectedCell(intRowIdx, mintGrdAmount_Amount_col)

                Case String.IsNullOrEmpty(mcGridAmountController(intRowIdx, mintGrdAmount_DtBegin_col))
                    gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                    mcGridAmountController.SetSelectedCell(intRowIdx, mintGrdAmount_DtBegin_col)

                Case intRowIdx < grdAmount.RowCount AndAlso String.IsNullOrEmpty(mcGridAmountController(intRowIdx, mintGrdAmount_DtEnd_col))
                    gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE)
                    mcGridAmountController.SetSelectedCell(intRowIdx, mintGrdAmount_DtEnd_col)

                Case Else
                    eventArgs.IsValid = True

            End Select

            If Not eventArgs.IsValid Then Exit For
        Next
    End Sub

    Private Sub btnAddRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRow.Click

        btnAddRow.Enabled = False
    End Sub

#End Region


End Class