Public Class frmManageBudget

    'Private members
    Private mintGrdBudget_Exp_ID_col As Short = 0
    Private mintGrdBudget_Exp_BillingDate_col As Short = 1
    Private mintGrdBudget_Exp_Name_col As Short = 2
    Private mintGrdBudget_Exp_Amount_col As Short = 3
    Private mintGrdBudget_Exp_PaidOn_col As Short = 4

    'Private class members
    Private WithEvents mcGridBudget As clsDataGridView
    Private WithEvents mcSQL As clsSQL_Transactions


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean

        Try

            dtpFrom.Value = Date.Today
            dtpTo.Value = DateAdd(DateInterval.Day, 14, Date.Today)

            blnReturn = True

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            blnReturn = False
        End Try

        Return blnReturn
    End Function

    Private Function blnGrdBudget_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & "  SELECT Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "         Expense.Exp_BillingDate, " & vbCrLf
            strSQL = strSQL & "         Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "         Expense.Exp_Amount, " & vbCrLf
            strSQL = strSQL & "         NULL AS PaidOn " & vbCrLf
            strSQL = strSQL & "  FROM Expense " & vbCrLf
            'strSQL = strSQL & "  WHERE Expense.Exp_BillingDate BETWEEN " & dtpFrom.

            blnReturn = mcGridBudget.bln_FillData(strSQL)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region


#Region "Private events"

    Private Sub mcGrid_SetDisplay() Handles mcGridBudget.SetDisplay

        'grdBudget.Rows.Add("20-08-2014", "Camping", "56.00$")

        'grdBudget.Rows(0).DefaultCellStyle.BackColor = Color.Yellow

        'grdBudget.Rows.Add("20-08-2014", "Ass. Auto", "250.50$")

        'grdBudget.Columns(0).HeaderText = "Échéance"
        'grdBudget.Columns(1).HeaderText = "Charge"
        'grdBudget.Columns(2).HeaderText = "Montant"


        grdBudget.Columns(mintGrdBudget_Exp_Amount_col).ValueType = GetType(Double)
        grdBudget.Columns(mintGrdBudget_Exp_BillingDate_col).ValueType = GetType(Date)

        grdBudget.Columns(mintGrdBudget_Exp_BillingDate_col).DefaultCellStyle.Format = gcAppControler.str_GetPCDateFormat
        grdBudget.Columns(mintGrdBudget_Exp_Amount_col).DefaultCellStyle.Format = gstrCurrencyFormat
    End Sub

    Private Sub rbtnHedo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnHebdo.CheckedChanged
        If rbtnHebdo.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnBiMensuel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnBiMensuel.CheckedChanged
        If rbtnBiMensuel.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnMensuelle_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnMensuelle.CheckedChanged
        If rbtnMensuelle.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnDeuxMois_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDeuxMois.CheckedChanged
        If rbtnDeuxMois.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)
        End If
    End Sub

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        mcGridBudget = New clsDataGridView

        Select Case False
            Case mcGridBudget.bln_Init(grdBudget)
            Case blnGrdBudget_Load()
            Case blnLoadData()
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        End If
    End Sub

#End Region

End Class
