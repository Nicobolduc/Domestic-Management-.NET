﻿Public Class frmExpense

    'Private members
    Private mcSQL As clsSQL_Transactions


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT Expense.Exp_Code, " & vbCrLf
            strSQL = strSQL & "        Expense.Per_ID " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            strSQL = strSQL & " WHERE Expense.Exp_ID = " & myFormControler.Item_ID & vbCrLf

            mySQLReader = mSQL.ADOSelect(strSQL)

            While mySQLReader.Read
                txtCode.Text = mySQLReader.Item("Exp_Code").ToString

                cboInterval.SelectedValue = CInt(mySQLReader.Item("Per_ID"))
            End While

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Private Function blnCboInterval_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT Period.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Period.Per_Desc " & vbCrLf
            strSQL = strSQL & " FROM Period " & vbCrLf
            strSQL = strSQL & " ORDER BY Period.Per_ID " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "Per_ID", "Per_Desc", False, cboInterval)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnReturn As Boolean

        Try
            mcSQL = New clsSQL_Transactions

            mcSQL.bln_BeginTransaction()

            Select Case myFormControler.FormMode
                Case clsConstants.Form_Modes.INSERT_MODE
                    blnReturn = blnExpense_Insert()

                Case clsConstants.Form_Modes.UPDATE_MODE
                    blnReturn = blnExense_Update()

                Case clsConstants.Form_Modes.DELETE_MODE
                    blnReturn = blnExpense_Delete()

            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnReturn)
            mcSQL = Nothing
        End Try

        Return blnReturn
    End Function

    Private Function blnExpense_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Exp_Code", txtCode.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("Per_ID", CStr(cboInterval.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOInsert("Expense", myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnExense_Update() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Exp_Code", txtCode.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("Per_ID", CStr(cboInterval.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOUpdate("Expense", "Exp_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnExpense_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Expense", "Exp_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region


#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        Select Case False
            Case blnCboInterval_Load()
            Case myFormControler.FormMode <> clsConstants.Form_Modes.INSERT_MODE
                blnReturn = True
            Case blnLoadData()
            Case Else
                blnReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        myFormControler.ChangeMade = True
    End Sub

    Private Sub cboInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInterval.SelectedIndexChanged
        myFormControler.ChangeMade = True
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules
        Select Case False
            Case txtCode.Text <> vbNullString
                gcAppControler.ShowMessage(clsConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtCode.Focus()

            Case cboInterval.SelectedIndex > -1
                gcAppControler.ShowMessage(clsConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboInterval.DroppedDown = True
                cboInterval.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

#End Region
    
End Class