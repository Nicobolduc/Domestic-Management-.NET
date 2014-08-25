Public Class frmExpense

    Public mintExp_ID As Integer
    Public mintFormMode As clsConstants.Form_Modes

    Private mcSQL As clsSQL_Transactions

    Private Sub frmDepense_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim blnReturn As Boolean

        Select Case False
            Case blnSetReadRights()
            Case blnCboInterval_Load()
            Case mintFormMode <> clsConstants.Form_Modes.INSERT
                blnReturn = True
            Case blnGrdExpense_Load()
            Case Else
                blnReturn = True
        End Select

    End Sub

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
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGrdExpense_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT Expense.Exp_Code, " & vbCrLf
            strSQL = strSQL & "        Expense.Per_ID " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            strSQL = strSQL & " WHERE Expense.Exp_ID = " & mintExp_ID & vbCrLf
            strSQL = strSQL & " ORDER BY Expense.Exp_Code " & vbCrLf

            mySQLReader = mSQL.ADOSelect(strSQL)

            While mySQLReader.Read
                txtCode.Text = mySQLReader.Item("Exp_Code").ToString

                cboInterval.SelectedIndex = CInt(mySQLReader.Item("Per_ID")) - 1
            End While

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnReturn As Boolean

        Try
            mcSQL = New clsSQL_Transactions

            mcSQL.bln_BeginTransaction()

            Select Case mintFormMode
                Case clsConstants.Form_Modes.INSERT
                    blnReturn = blnExpense_Insert()

                Case clsConstants.Form_Modes.UPDATE
                    blnReturn = blnExense_Update()

                Case clsConstants.Form_Modes.DELETE
                    blnReturn = blnExpense_Delete()

            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnReturn)
            mcSQL = Nothing
        End Try

        Return blnReturn
    End Function

    Private Function blnSetReadRights() As Boolean
        Dim blnReturn As Boolean = True

        Try
            Select Case mintFormMode
                Case clsConstants.Form_Modes.INSERT
                    btnApply.Text = "Enregistrer"

                Case clsConstants.Form_Modes.UPDATE
                    btnApply.Text = "Appliquer"

                Case clsConstants.Form_Modes.DELETE
                    btnApply.Text = "Supprimer"

                    Dim ctrl As Control
                    For Each ctrl In Me.Controls
                        If Not TypeOf ctrl Is Windows.Forms.Button And Not TypeOf ctrl Is Windows.Forms.Label Then
                            ctrl.Enabled = False
                        End If
                    Next

            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnExpense_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Exp_Code", txtCode.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("Per_ID", CStr(cboInterval.SelectedIndex + 1), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOInsert("Expense")
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnExense_Update() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Exp_Code", txtCode.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("Per_ID", CStr(cboInterval.SelectedIndex + 1), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOUpdate("Expense", "Exp_ID = " & mintExp_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnExpense_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Expense", "Exp_ID = " & mintExp_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    'Private Function () As Boolean
    '    Dim blnReturn As Boolean

    '    Try

    '    Catch ex As Exception
    '       blnReturn = False
    '       gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
    '    End Try

    '    Return blnReturn
    'End Function

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        blnSaveData()
        Me.Close()
    End Sub
End Class