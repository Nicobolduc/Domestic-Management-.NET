Public Class FinanceController

    Public Function GetExpense(ByVal vintExpense_ID As Integer) As Model.Expense
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cExpense As Model.Expense = Nothing

        Try
            strSQL = strSQL & " SELECT Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_BillingDate, " & vbCrLf
            strSQL = strSQL & "        TRUNCATE(Expense.Exp_Amount, 2) AS Exp_Amount, " & vbCrLf
            strSQL = strSQL & "        Expense.Per_ID " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            strSQL = strSQL & " WHERE Expense.Exp_ID = " & vintExpense_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cExpense = New Model.Expense
                cExpense.ID = vintExpense_ID
                cExpense.Name = mySQLReader.Item("Exp_Name").ToString
                cExpense.BillingDate = CType(IIf(IsDBNull(mySQLReader.Item("Exp_BillingDate")), Nothing, mySQLReader.Item("Exp_BillingDate")), Date?)
                cExpense.Amount = CDbl(mySQLReader.Item("Exp_Amount"))
                cExpense.Period = CType(mySQLReader.Item("Per_ID"), Period)

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return cExpense
    End Function

End Class
