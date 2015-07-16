Public Class frmExpense

    'Private members
    Private mcSQL As MySQLController
    Private mcExpenseModel As Model.Expense


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcExpenseModel = gcAppController.GetCoreModelController.GetFinanceController.GetExpense(formController.Item_ID)

            If Not mcExpenseModel Is Nothing Then

                txtName.Text = mcExpenseModel.Name
                txtAmount.Text = mcExpenseModel.Amount.ToString
                cboInterval.SelectedValue = CInt(mcExpenseModel.Period)
                cboType.SelectedValue = CInt(mcExpenseModel.Type.ID)
                cboType.BackColor = Color.FromArgb(mcExpenseModel.Type.ArgbColor)

                If Not mcExpenseModel.BillingDate Is Nothing Then

                    dtpBillDate.Checked = True
                    dtpBillDate.Value = CDate(Format(AppController.GetAppController.str_GetPCDateFormat, mcExpenseModel.BillingDate.Value.ToString))
                Else
                    dtpBillDate.Checked = False
                End If

                blnValidReturn = True
            End If

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

        Try
            If mcExpenseModel Is Nothing Then

                mcExpenseModel = New Model.Expense
            End If

            mcExpenseModel.SQLController = mcSQL
            mcExpenseModel.DLMCommand = formController.FormMode
            mcExpenseModel.ID = formController.Item_ID
            mcExpenseModel.Name = txtName.Text
            mcExpenseModel.Amount = Math.Round(Val(txtAmount.Text), 2)
            mcExpenseModel.Period = CType(cboInterval.SelectedValue, Period)

            mcExpenseModel.Type = New Model.ExpenseType
            mcExpenseModel.Type.ID = CInt(cboType.SelectedValue)

            If Not IsDBNull(dtpBillDate.Value) And dtpBillDate.Checked Then

                mcExpenseModel.BillingDate = dtpBillDate.Value
            Else
                mcExpenseModel.BillingDate = Nothing
            End If

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region

#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        dtpBillDate.Value = DateTime.Today

        Select Case False
            Case blnCboInterval_Load()
            Case blnCboType_Load()
            Case formController.FormMode <> mConstants.Form_Mode.INSERT_MODE
                blnValidReturn = True
            Case blnFormData_Load()
            Case Else
                blnValidReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnFormData_Save()
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged, txtAmount.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInterval.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Select Case False
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

                'Case txtAmount.Text <> String.Empty
                '    gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                '    txtAmount.Focus()

                'Case IsNumeric(txtAmount.Text)
                '    gcAppController.ShowMessage(mConstants.Validation_Message.NUMERIC_VALUE, MsgBoxStyle.Information)
                '    txtAmount.Focus()

            Case cboInterval.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case cboType.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub dtpBillDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpBillDate.ValueChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

#End Region

End Class