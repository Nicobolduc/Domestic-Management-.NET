Public Class frmIncome

    'Private members
    Private mcSQL As MySQLController
    Private mcIncomeModel As Model.Income


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcIncomeModel = gcAppController.GetCoreModelController.GetFinanceController.GetIncome(formController.Item_ID)

            If Not mcIncomeModel Is Nothing Then

                txtName.Text = mcIncomeModel.Name
                txtAmount.Text = mcIncomeModel.Amount.ToString
                cboFrequency.SelectedValue = CInt(mcIncomeModel.Period)
                chkMainIncome.Checked = mcIncomeModel.IsMainIncome

                If Not mcIncomeModel.ReceptionDate Is Nothing Then

                    dtpPayDate.Checked = True
                    dtpPayDate.Value = CDate(Format(AppController.GetAppController.str_GetPCDateFormat, mcIncomeModel.ReceptionDate.Value.ToString))
                Else
                    dtpPayDate.Checked = False
                End If

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
            mcSQL = Nothing
        End Try

        Return blnValidReturn
    End Function

    Public Function blnSyncIncomeModel() As Boolean
        Dim blnValidReturn As Boolean

        Try
            If mcIncomeModel Is Nothing Then

                mcIncomeModel = New Model.Income
            End If

            mcIncomeModel.SQLController = mcSQL
            mcIncomeModel.DLMCommand = formController.FormMode
            mcIncomeModel.ID = formController.Item_ID
            mcIncomeModel.Name = txtName.Text
            mcIncomeModel.Amount = Math.Round(Val(txtAmount.Text), 2)
            mcIncomeModel.Period = CType(cboFrequency.SelectedValue, Period)
            mcIncomeModel.IsMainIncome = chkMainIncome.Checked

            If Not IsDBNull(dtpPayDate.Value) And dtpPayDate.Checked Then

                mcIncomeModel.ReceptionDate = dtpPayDate.Value
            Else
                mcIncomeModel.ReceptionDate = Nothing
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

        dtpPayDate.Value = DateTime.Today

        Select Case False
            Case blnCboFrequency_Load()
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

    Private Sub cboInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFrequency.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Dim strMainIncome As String = String.Empty

        strMainIncome = MySQLController.str_ADOSingleLookUp("Inc_IsMain", "Income", "Inc_IsMain = 1 AND Income.Inc_ID <> " & mcIncomeModel.ID)

        Select Case False
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case txtAmount.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtAmount.Focus()

            Case IsNumeric(txtAmount.Text)
                gcAppController.ShowMessage(mConstants.Validation_Message.NUMERIC_VALUE, MsgBoxStyle.Information)
                txtAmount.Focus()

            Case cboFrequency.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboFrequency.DroppedDown = True
                cboFrequency.Focus()

            Case strMainIncome = String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Message.UNIQUE_ATTRIBUTE, MsgBoxStyle.Information)
                chkMainIncome.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub dtpBillDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpPayDate.ValueChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs)
        formController.ChangeMade = True
    End Sub

    Private Sub chkMainIncome_CheckedChanged(sender As Object, e As EventArgs) Handles chkMainIncome.CheckedChanged
        formController.ChangeMade = True
    End Sub

#End Region

End Class