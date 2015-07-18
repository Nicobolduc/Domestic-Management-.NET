Public Class FinanceController

    Public Function GetExpense(ByVal vintExpense_ID As Integer) As Model.Expense
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cExpense As Model.Expense = Nothing
        Dim intExpType As Integer

        Try
            strSQL = strSQL & " SELECT Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_BillingDate, " & vbCrLf
            strSQL = strSQL & "        TRUNCATE(Expense.Exp_Amount, 2) AS Exp_Amount, " & vbCrLf
            strSQL = strSQL & "        Expense.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Expense.ExpT_ID, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Fixed " & vbCrLf
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
                cExpense.Fixed = CBool(mySQLReader.Item("Exp_Fixed"))
                intExpType = CInt(mySQLReader.Item("ExpT_ID"))

                mySQLReader.Dispose()

                cExpense.Type = GetExpenseType(intExpType)

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cExpense
    End Function

    Public Function GetExpenseType(ByVal vintExpenseType_ID As Integer) As Model.ExpenseType
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cExpenseType As Model.ExpenseType = Nothing

        Try
            strSQL = strSQL & " SELECT ExpenseType.ExpT_Name, " & vbCrLf
            strSQL = strSQL & "        ExpenseType.ExpT_ArgbColor " & vbCrLf
            strSQL = strSQL & " FROM ExpenseType " & vbCrLf
            strSQL = strSQL & " WHERE ExpenseType.ExpT_ID = " & vintExpenseType_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cExpenseType = New Model.ExpenseType
                cExpenseType.ID = vintExpenseType_ID
                cExpenseType.Name = mySQLReader.Item("ExpT_Name").ToString
                cExpenseType.ArgbColor = CInt(IIf(IsDBNull(mySQLReader.Item("ExpT_ArgbColor")), 0, mySQLReader.Item("ExpT_ArgbColor")))

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cExpenseType
    End Function

    Public Function GetIncome(ByVal vintIncome_ID As Integer) As Model.Income
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cIncome As Model.Income = Nothing

        Try
            strSQL = strSQL & " SELECT Income.Inc_ID, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_Name, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_ReceptDate, " & vbCrLf
            strSQL = strSQL & "        TRUNCATE(Income.Inc_Amount, 2) AS Inc_Amount, " & vbCrLf
            strSQL = strSQL & "        Income.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_IsMain " & vbCrLf
            strSQL = strSQL & " FROM Income " & vbCrLf
            strSQL = strSQL & " WHERE Income.Inc_ID = " & vintIncome_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cIncome = New Model.Income
                cIncome.ID = vintIncome_ID
                cIncome.Name = mySQLReader.Item("Inc_Name").ToString
                cIncome.ReceptionDate = CType(IIf(IsDBNull(mySQLReader.Item("Inc_ReceptDate")), Nothing, mySQLReader.Item("Inc_ReceptDate")), Date?)
                cIncome.Amount = CDbl(mySQLReader.Item("Inc_Amount"))
                cIncome.Period = CType(mySQLReader.Item("Per_ID"), Period)
                cIncome.IsMainIncome = CBool(mySQLReader.Item("Inc_IsMain"))

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cIncome
    End Function

End Class
