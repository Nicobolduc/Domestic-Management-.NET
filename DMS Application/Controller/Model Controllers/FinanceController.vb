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
            strSQL = strSQL & "        ExpensePeriod.ExpP_DtBegin, " & vbCrLf
            strSQL = strSQL & "        ExpensePeriod.ExpP_DtEnd, " & vbCrLf
            strSQL = strSQL & "        TRUNCATE(ExpensePeriod.ExpP_Amount, 2) AS ExpAmount, " & vbCrLf
            strSQL = strSQL & "        Expense.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Expense.ExpT_ID, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Fixed " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            strSQL = strSQL & "     INNER JOIN (SELECT ExpensePeriod.* " & vbCrLf
            strSQL = strSQL & "                 FROM (SELECT MAX(ExpensePeriod.ExpP_DtBegin) AS ExpP_DtBegin, ExpensePeriod.Exp_ID " & vbCrLf
            strSQL = strSQL & "                       FROM ExpensePeriod " & vbCrLf
            strSQL = strSQL & "                       WHERE ExpensePeriod.Exp_ID = " & vintExpense_ID & vbCrLf
            strSQL = strSQL & "                       GROUP BY ExpensePeriod.Exp_ID " & vbCrLf
            strSQL = strSQL & "                     ) AS TMax " & vbCrLf
            strSQL = strSQL & "                  		INNER JOIN ExpensePeriod USING(Exp_ID, ExpP_DtBegin) " & vbCrLf
            strSQL = strSQL & "                 ) AS ExpensePeriod " & vbCrLf
            strSQL = strSQL & " WHERE Expense.Exp_ID = " & vintExpense_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cExpense = New Model.Expense
                cExpense.ID = vintExpense_ID
                cExpense.Name = mySQLReader.Item("Exp_Name").ToString
                cExpense.Period = CType(mySQLReader.Item("Per_ID"), Period)
                cExpense.Fixed = CBool(mySQLReader.Item("Exp_Fixed"))

                intExpType = CInt(mySQLReader.Item("ExpT_ID"))

                cExpense.CurrentExpPeriod = New Model.Expense.ExpensePeriod
                cExpense.CurrentExpPeriod.DateBegin = CDate(mySQLReader.Item("ExpP_DtBegin"))

                If IsDBNull(mySQLReader.Item("ExpP_DtEnd")) Then
                    cExpense.CurrentExpPeriod.DateEnd = Nothing
                Else
                    cExpense.CurrentExpPeriod.DateEnd = CDate(mySQLReader.Item("ExpP_DtEnd"))
                End If

                cExpense.CurrentExpPeriod.Amount = CDbl(mySQLReader.Item("ExpAmount"))

                mySQLReader.Dispose()

                cExpense.Type = GetExpenseType(intExpType)

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            strSQL = strSQL & "        IncomePeriod.IncP_DtBegin, " & vbCrLf
            strSQL = strSQL & "        IncomePeriod.IncP_DtEnd, " & vbCrLf
            strSQL = strSQL & "        TRUNCATE(IncomePeriod.IncP_Amount, 2) AS IncP_Amount, " & vbCrLf
            strSQL = strSQL & "        Income.Per_ID, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_IsMain, " & vbCrLf
            strSQL = strSQL & "        Income.Bud_ID " & vbCrLf
            strSQL = strSQL & " FROM Income " & vbCrLf
            strSQL = strSQL & "     INNER JOIN (SELECT IncomePeriod.* " & vbCrLf
            strSQL = strSQL & "                 FROM (SELECT MAX(IncomePeriod.IncP_DtBegin) AS IncP_DtBegin, IncomePeriod.Inc_ID " & vbCrLf
            strSQL = strSQL & "                       FROM IncomePeriod " & vbCrLf
            strSQL = strSQL & "                       WHERE IncomePeriod.Inc_ID = " & vintIncome_ID & vbCrLf
            strSQL = strSQL & "                       GROUP BY IncomePeriod.Inc_ID " & vbCrLf
            strSQL = strSQL & "                     ) AS TMax " & vbCrLf
            strSQL = strSQL & "                  		INNER JOIN IncomePeriod USING(Inc_ID, IncP_DtBegin) " & vbCrLf
            strSQL = strSQL & "                 ) AS IncomePeriod " & vbCrLf
            strSQL = strSQL & " WHERE Income.Inc_ID = " & vintIncome_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cIncome = New Model.Income
                cIncome.ID = vintIncome_ID
                cIncome.Name = mySQLReader.Item("Inc_Name").ToString
                cIncome.Period = CType(mySQLReader.Item("Per_ID"), Period)
                cIncome.IsMainIncome = CBool(mySQLReader.Item("Inc_IsMain"))
                cIncome.Budget_ID = CInt(mySQLReader.Item("Bud_ID"))

                cIncome.CurrentIncAmount = New Model.Income.IncomePeriod
                cIncome.CurrentIncAmount.Amount = CDbl(mySQLReader.Item("IncP_Amount"))
                cIncome.CurrentIncAmount.DateBegin = CDate(mySQLReader.Item("IncP_DtBegin"))
                cIncome.CurrentIncAmount.DateEnd = CType(IIf(IsDBNull(mySQLReader.Item("IncP_DtEnd")), Nothing, mySQLReader.Item("IncP_DtEnd")), Date?)

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cIncome
    End Function

End Class
