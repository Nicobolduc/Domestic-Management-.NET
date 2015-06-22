Public Class frmExpense

    'Private members
    Private mcSQL As MySQLController


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_BillingDate, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Amount, " & vbCrLf
            strSQL = strSQL & "        Expense.Per_ID " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            strSQL = strSQL & " WHERE Expense.Exp_ID = " & formController.Item_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            While mySQLReader.Read
                txtCode.Text = mySQLReader.Item("Exp_Name").ToString

                txtAmount.Text = mySQLReader.Item("Exp_Amount").ToString

                If Not IsDBNull(mySQLReader.Item("Exp_BillingDate")) Then
                    dtpBillDate.Value = CDate(mySQLReader.Item("Exp_BillingDate"))
                End If

                cboInterval.SelectedValue = CInt(mySQLReader.Item("Per_ID"))
            End While

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboInterval_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT Period.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Period.Per_Desc " & vbCrLf
            strSQL = strSQL & " FROM Period " & vbCrLf
            strSQL = strSQL & " ORDER BY Period.Per_ID " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "Per_ID", "Per_Desc", False, cboInterval)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            mcSQL.bln_BeginTransaction()

            Select Case formController.FormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    blnValidReturn = blnExpense_Insert()

                Case mConstants.Form_Modes.UPDATE_MODE
                    blnValidReturn = blnExense_Update()

                Case mConstants.Form_Modes.DELETE_MODE
                    blnValidReturn = blnExpense_Delete()

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

    Private Function blnExpense_Insert() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Exp_Name", txtCode.Text, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("Exp_BillingDate", CStr(IIf(IsDBNull(dtpBillDate.Value), "", dtpBillDate.Value.ToString)), MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                Case mcSQL.bln_AddField("Exp_Amount", txtAmount.Text, MySQLController.MySQL_FieldTypes.DOUBLE_TYPE)
                Case mcSQL.bln_AddField("Per_ID", CStr(cboInterval.SelectedValue), MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_ADOInsert("Expense", formController.Item_ID)
                Case formController.Item_ID > 0
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnExense_Update() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Exp_Name", txtCode.Text, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("Exp_Amount", txtAmount.Text, MySQLController.MySQL_FieldTypes.DOUBLE_TYPE)
                Case mcSQL.bln_AddField("Per_ID", CStr(cboInterval.SelectedValue), MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_ADOUpdate("Expense", "Exp_ID = " & formController.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnExpense_Delete() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Expense", "Exp_ID = " & formController.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

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
            Case formController.FormMode <> mConstants.Form_Modes.INSERT_MODE
                blnValidReturn = True
            Case blnLoadData()
            Case Else
                blnValidReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txtAmount.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInterval.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Select Case False
            Case txtCode.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtCode.Focus()

            Case txtAmount.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtAmount.Focus()

            Case IsNumeric(txtAmount.Text)
                gcAppController.ShowMessage(mConstants.Validation_Messages.NUMERIC_VALUE, MsgBoxStyle.Information)
                txtAmount.Focus()

            Case cboInterval.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub dtpBillDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpBillDate.ValueChanged
        formController.ChangeMade = True
    End Sub

#End Region
    

End Class