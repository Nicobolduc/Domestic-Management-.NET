Public Class frmBudgetManagement

    'Private members
    Private mintGrdBudget_Action_col As Short = 1
    Private mintGrdBudget_Exp_ID_col As Short = 2
    Private mintGrdBudget_Income_Date_col As Short = 3
    Private mintGrdBudget_Exp_BillingDate_col As Short = 4
    Private mintGrdBudget_Exp_Name_col As Short = 5
    Private mintGrdBudget_Exp_Amount_col As Short = 6
    Private mintGrdBudget_PInc_Amount_Paid_col As Short = 7
    Private mintGrdBudget_PInc_PaidOn_col As Short = 8
    Private mintGrdBudget_ExpT_Name_col As Short = 9
    Private mintGrdBudget_ExpT_ArgbColor_col As Short = 10
    Private mintGrdBudget_Income_Name_col As Short = 11
    Private mintGrdBudget_Income_Amount_col As Short = 12
    Private mintGrdBudget_PInc_Comment_col As Short = 13
    Private mintGrdBudget_Sel_col As Short = 14

    'Messages
    Private mintMustDefineMainIncome_msg As Short = 18

    'Private class members
    Private WithEvents mcGridBudgetController As SyncfusionGridController
    Private WithEvents mcSQL As MySQLController


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            blnValidReturn = blnCalculateNextPayDate()

            If blnValidReturn Then
                dtpFrom.Value = Date.Today
                dtpFrom.CustomFormat = gcAppController.str_GetPCDateFormat

                dtpTo.Value = DateAdd(DateInterval.Month, 1, Date.Today)
                dtpTo.CustomFormat = gcAppController.str_GetPCDateFormat

                blnValidReturn = True
            Else
                gcAppController.ShowMessage(mintMustDefineMainIncome_msg, MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            blnValidReturn = False
        Finally
            If Not blnValidReturn Then Me.Close()
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdBudget_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        'SELECT  T2.Exp_ID,
        '	    T2.NextBillingDate,
        '		T2.Exp_Name,
        '        T2.Exp_Amount,
        '		CASE WHEN T2.NextBillingDate < T2.PreviousReceptionDate
        '			 THEN T2.PreviousReceptionDate
        '			 WHEN T2.NextBillingDate > T2.NextReceptionDate 
        '			 THEN DATE_ADD(Inc_ReceptDate, INTERVAL (CASE WHEN TIMESTAMPDIFF(WEEK, Inc_ReceptDate, NextBillingDate) = 0 THEN 2 ELSE TIMESTAMPDIFF(WEEK, Inc_ReceptDate, NextBillingDate) + IF(LEFT(TIMESTAMPDIFF(WEEK, Inc_ReceptDate, NextBillingDate), 1) % 2 <> 0, 1 , 2) END)  WEEK) 
        '             ELSE T2.NextReceptionDate 
        '		END AS NextPayToUseDate,
        '        T2.ExpT_ArgbColor,
        '        #T2.PInc_ID,
        '        T2.Amount_Paid,
        '        T2.PaidOn,
        '        T2.Comment,
        '        CASE WHEN T2.Exp_Fixed = 1 THEN 'TRUE' ELSE 'FALSE' END AS SelCol
        '                FROM()
        '	 (SELECT * ,
        '            Case T1.Per_ID
        '				 WHEN 1 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 1 WEEK) 
        '				 WHEN 2 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 2  WEEK) 
        '				 WHEN 3 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 1 MONTH) 
        '				 WHEN 4 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 2  MONTH) 
        '			 ELSE NULL 
        '			 END AS NextReceptionDate 
        '	 FROM (SELECT 0 AS Action, 
        '				 Expense.Exp_ID,
        '				 Income.Per_ID,
        '				 Income.Inc_ReceptDate,
        '            Case Income.Per_ID
        '					 WHEN 1 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, '2015-07-23 12:00:00'))  WEEK) 
        '					 WHEN 2 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, '2015-07-23 12:00:00') * 2)  WEEK) 
        '					 WHEN 3 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, '2015-07-23 12:00:00'))  MONTH) 
        '					 WHEN 4 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, '2015-07-23 12:00:00')* 2)  MONTH) 
        '				 ELSE NULL 
        '				 END AS PreviousReceptionDate,
        '				 CASE WHEN Expense.Exp_BillingDate < '2015-07-23 12:00:00' 
        '					  THEN CASE Expense.Per_ID 
        '						   WHEN 1 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(WEEK, Expense.Exp_BillingDate, '2015-07-23 12:00:00') + 1)  WEEK) 
        '						   WHEN 2 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(WEEK, Expense.Exp_BillingDate, '2015-07-23 12:00:00') * 2 + 1)  WEEK) 
        '						   WHEN 3 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(MONTH, Expense.Exp_BillingDate, '2015-07-23 12:00:00') + 1)  MONTH) 
        '						   WHEN 4 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(MONTH, Expense.Exp_BillingDate, '2015-07-23 12:00:00') * 2 + 1)  MONTH) 
        '						   ELSE NULL 
        '                End
        '					 ELSE Expense.Exp_BillingDate
        '				 END AS NextBillingDate, 
        '				 Expense.Exp_Name, 
        '				 Expense.Exp_Amount, 
        '				 Expense.Exp_Fixed,
        '				 NULL AS Amount_Paid, 
        '				 NULL As PaidOn, 
        '				 ExpenseType.ExpT_Name, 
        '				 ExpenseType.ExpT_ArgbColor, 
        '				 Income.Inc_Name, 
        '				 Income.Inc_Amount, 
        '				 NULL AS Comment
        '                FROM Expense
        '				INNER JOIN ExpenseType ON ExpenseType.ExpT_ID = Expense.ExpT_ID 
        '				INNER JOIN Income ON Income.Inc_IsMain = 1 
        '	 ) AS T1 

        '	 WHERE T1.NextBillingDate BETWEEN '2015-07-23 12:00:00' AND '2015-08-23 12:00:00'

        ') AS T2

        'ORDER BY T2.PreviousReceptionDate, T2.NextBillingDate 

        Try
            strSQL = strSQL & " SELECT * " & vbCrLf
            'strSQL = strSQL & "         CASE T1.IncomePeriod " & vbCrLf
            'strSQL = strSQL & "             WHEN 1 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 1 WEEK) " & vbCrLf
            'strSQL = strSQL & "             WHEN 2 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 2 WEEK) " & vbCrLf
            'strSQL = strSQL & "             WHEN 3 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 1 MONTH) " & vbCrLf
            'strSQL = strSQL & "             WHEN 4 THEN DATE_ADD(T1.PreviousReceptionDate, INTERVAL 2 MONTH) " & vbCrLf
            'strSQL = strSQL & "             ELSE NULL " & vbCrLf
            'strSQL = strSQL & "         END AS NextReceptionDate " & vbCrLf
            strSQL = strSQL & " FROM ( " & vbCrLf
            strSQL = strSQL & "     SELECT " & DataGridViewController.GridRowActions.CONSULT_ACTION & " AS Action, " & vbCrLf
            strSQL = strSQL & "             Expense.Exp_ID, " & vbCrLf
            'strSQL = strSQL & "             Income.Per_ID AS IncomePeriod, " & vbCrLf
            strSQL = strSQL & "             CASE Income.Per_ID " & vbCrLf
            strSQL = strSQL & "                 WHEN 1 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & "))  WEEK) " & vbCrLf
            strSQL = strSQL & "                 WHEN 2 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & ") * 2)  WEEK) " & vbCrLf
            strSQL = strSQL & "                 WHEN 3 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & "))  MONTH) " & vbCrLf
            strSQL = strSQL & "                 WHEN 4 THEN DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & ")* 2)  MONTH) " & vbCrLf
            strSQL = strSQL & "             ELSE NULL " & vbCrLf
            strSQL = strSQL & "             END AS PreviousReceptionDate, " & vbCrLf
            strSQL = strSQL & "             CASE Expense.Per_ID " & vbCrLf
            strSQL = strSQL & "                 WHEN 1 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(WEEK, Expense.Exp_BillingDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & "))  WEEK) " & vbCrLf
            strSQL = strSQL & "                 WHEN 2 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(WEEK, Expense.Exp_BillingDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & ") * 2)  WEEK) " & vbCrLf
            strSQL = strSQL & "                 WHEN 3 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(MONTH, Expense.Exp_BillingDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & "))  MONTH) " & vbCrLf
            strSQL = strSQL & "                 WHEN 4 THEN DATE_ADD(Expense.Exp_BillingDate, INTERVAL (TIMESTAMPDIFF(MONTH, Expense.Exp_BillingDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & ") * 2)  MONTH) " & vbCrLf
            strSQL = strSQL & "             ELSE NULL " & vbCrLf
            strSQL = strSQL & "             END AS NextBillingDate, " & vbCrLf
            strSQL = strSQL & "             Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "             Expense.Exp_Amount, " & vbCrLf
            strSQL = strSQL & "             NULL AS Amount_Paid, " & vbCrLf
            strSQL = strSQL & "             NULL As PaidOn, " & vbCrLf
            strSQL = strSQL & "             ExpenseType.ExpT_Name, " & vbCrLf
            strSQL = strSQL & "             ExpenseType.ExpT_ArgbColor, " & vbCrLf
            strSQL = strSQL & "             Income.Inc_Name, " & vbCrLf
            strSQL = strSQL & "             Income.Inc_Amount, " & vbCrLf
            strSQL = strSQL & "             NULL AS Comment, " & vbCrLf
            strSQL = strSQL & "             CASE WHEN Expense.Exp_Fixed = 1 THEN 'TRUE' ELSE 'FALSE' END AS SelCol " & vbCrLf
            strSQL = strSQL & "     FROM Expense " & vbCrLf
            strSQL = strSQL & "         INNER JOIN ExpenseType ON ExpenseType.ExpT_ID = Expense.ExpT_ID " & vbCrLf
            strSQL = strSQL & "         INNER JOIN Income ON Income.Inc_IsMain = 1 " & vbCrLf
            strSQL = strSQL & " ) AS T1 " & vbCrLf
            strSQL = strSQL & "  WHERE T1.NextBillingDate BETWEEN " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & " AND " & MySQLController.str_FixDateForSelect(dtpTo.Value) & vbCrLf
            strSQL = strSQL & " ORDER BY T1.PreviousReceptionDate, T1.NextBillingDate " & vbCrLf

            blnValidReturn = mcGridBudgetController.bln_FillData(strSQL)

            If blnValidReturn Then

                lblMainIncomeAmount.Text = grdBudget(grdBudget.RowCount, mintGrdBudget_Income_Amount_col).CellValue.ToString & " $"

                For intRowIdx As Integer = 2 To grdBudget.RowCount + 1

                    grdBudget(intRowIdx - 1, mintGrdBudget_Exp_Name_col).BackColor = Color.FromArgb(CInt(grdBudget(intRowIdx - 1, mintGrdBudget_ExpT_ArgbColor_col).CellValue))

                    If intRowIdx = grdBudget.RowCount + 1 Then

                        blnGrdBudget_AddSummaryRow(intRowIdx)

                    ElseIf CDate(grdBudget(intRowIdx - 1, mintGrdBudget_Income_Date_col).CellValue) < CDate(grdBudget(intRowIdx, mintGrdBudget_Income_Date_col).CellValue) Then

                        blnGrdBudget_AddSummaryRow(intRowIdx)

                    End If

                Next

            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCalculateNextPayDate() As Boolean
        Dim blnValidReturn As Boolean = True
        Dim strIncomeDate As String = String.Empty
        Dim strIncomeInterval As String = String.Empty

        Try
            strIncomeInterval = MySQLController.str_ADOSingleLookUp("Income.Per_ID", "Income", "Income.Inc_IsMain = 1")

            Select Case Val(strIncomeInterval)
                Case mConstants.Period.WEEKLY
                    strIncomeDate = MySQLController.str_ADOSingleLookUp("DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & "))  WEEK)", "Income", "Income.Inc_IsMain = 1")
                    lblIncomePeriodTo.Text = Format(DateAdd(DateInterval.Day, 7, CDate(strIncomeDate)), gcAppController.str_GetPCDateFormat)

                Case mConstants.Period.FORTNIGHTLY
                    strIncomeDate = MySQLController.str_ADOSingleLookUp("DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(WEEK, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & ") * 2)  WEEK)", "Income", "Income.Inc_IsMain = 1")
                    lblIncomePeriodTo.Text = Format(DateAdd(DateInterval.Day, 14, CDate(strIncomeDate)), gcAppController.str_GetPCDateFormat)

                Case mConstants.Period.MONTHLY
                    strIncomeDate = MySQLController.str_ADOSingleLookUp("DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(MONTH, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & "))  MONTH)", "Income", "Income.Inc_IsMain = 1")
                    lblIncomePeriodTo.Text = Format(DateAdd(DateInterval.Month, 1, CDate(strIncomeDate)), gcAppController.str_GetPCDateFormat)

                Case mConstants.Period.TWO_MONTHS
                    strIncomeDate = MySQLController.str_ADOSingleLookUp("DATE_ADD(Income.Inc_ReceptDate, INTERVAL (TIMESTAMPDIFF(MONTH, Income.Inc_ReceptDate, " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & ") * 2)  MONTH)", "Income", "Income.Inc_IsMain = 1")
                    lblIncomePeriodTo.Text = Format(DateAdd(DateInterval.Month, 2, CDate(strIncomeDate)), gcAppController.str_GetPCDateFormat)

                Case Else
                    blnValidReturn = False

            End Select

            lblIncomePeriodFrom.Text = Format(CDate(strIncomeDate), gcAppController.str_GetPCDateFormat)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdBudget_AddSummaryRow(ByVal vintRowIndexToAdd As Integer) As Boolean
        Dim blnValidReturn As Boolean
        Dim dblPeriodTotal As Double

        Try
            grdBudget.Rows.InsertRange(vintRowIndexToAdd, 1)

            grdBudget.RowStyles(vintRowIndexToAdd).CellType = "Default"

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Action_col).CellValue = SyncfusionGridController.GridRowActions.NO_ACTION

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).HorizontalAlignment = GridHorizontalAlignment.Right
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellValueType = GetType(String)
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellType = GridCellTypeName.FormulaCell
            'grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).Text = "=B4"

            For intRowIdx As Integer = 1 To vintRowIndexToAdd - 1

                dblPeriodTotal += Val(grdBudget(intRowIdx, mintGrdBudget_Exp_Amount_col).CellValue)
            Next

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Exp_Amount_col).CellValue = dblPeriodTotal

            grdBudget(vintRowIndexToAdd, mintGrdBudget_Income_Date_col).CellValue = lblIncomePeriodTo.Text
            grdBudget(vintRowIndexToAdd, mintGrdBudget_Income_Date_col).HorizontalAlignment = GridHorizontalAlignment.Center

            For intColIdx As Short = 1 To CShort(grdBudget.ColCount)

                grdBudget(vintRowIndexToAdd, intColIdx).ReadOnly = True
            Next

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

        mcGridBudgetController = New SyncfusionGridController
    End Sub

    Private Sub mcGrid_SetDisplay() Handles mcGridBudgetController.SetDisplay
        grdBudget.Model.Options.MergeCellsMode = GridMergeCellsMode.None
        grdBudget.ColStyles(mintGrdBudget_Income_Date_col).MergeCell = GridMergeCellDirection.None
        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).MergeCell = GridMergeCellDirection.None

        grdBudget.ColWidths(mintGrdBudget_Exp_BillingDate_col) = 110
        grdBudget.ColWidths(mintGrdBudget_Exp_Name_col) = 200
        grdBudget.ColWidths(mintGrdBudget_Exp_Amount_col) = 100
        grdBudget.ColWidths(mintGrdBudget_PInc_Amount_Paid_col) = 100
        grdBudget.ColWidths(mintGrdBudget_PInc_PaidOn_col) = 110
        grdBudget.ColWidths(mintGrdBudget_Income_Date_col) = 110
        grdBudget.ColWidths(mintGrdBudget_PInc_Comment_col) = 225

        mcGridBudgetController.blnSetColType_CheckBox(mintGrdBudget_Sel_col, False)

        mcGridBudgetController.blnSetColType_DateTimePicker(mintGrdBudget_PInc_PaidOn_col, True)

        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).CellValueType = GetType(Double)
        grdBudget.ColStyles(mintGrdBudget_PInc_Amount_Paid_col).CellValueType = GetType(Double)
        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).CellValueType = GetType(Date)

        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).Format = gcAppController.str_GetPCDateFormat
        grdBudget.ColStyles(mintGrdBudget_Income_Date_col).Format = gcAppController.str_GetPCDateFormat
        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).Format = mConstants.DataFormat.CURRENCY
        grdBudget.ColStyles(mintGrdBudget_PInc_Amount_Paid_col).Format = mConstants.DataFormat.CURRENCY

        grdBudget.Model.Options.MergeCellsMode = GridMergeCellsMode.OnDemandCalculation Or GridMergeCellsMode.MergeRowsInColumn
        grdBudget.ColStyles(mintGrdBudget_Income_Date_col).MergeCell = GridMergeCellDirection.RowsInColumn
        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).MergeCell = GridMergeCellDirection.RowsInColumn

        grdBudget.TableStyle.VerticalAlignment = GridVerticalAlignment.Middle

        mcGridBudgetController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.EXTEND_LAST_COL

        For intCol As Integer = 1 To grdBudget.ColCount
            If intCol <> mintGrdBudget_PInc_PaidOn_col And intCol <> mintGrdBudget_PInc_Comment_col And intCol <> mintGrdBudget_PInc_Amount_Paid_col And intCol <> mintGrdBudget_Sel_col Then
                grdBudget.ColStyles(intCol).ReadOnly = True
            End If
        Next

    End Sub

    Private Sub rbtnBiMensuel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnBiMensuel.CheckedChanged
        If rbtnBiMensuel.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppController.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnMensuelle_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnMensuelle.CheckedChanged
        If rbtnMensuelle.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppController.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnDeuxMois_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDeuxMois.CheckedChanged
        If rbtnDeuxMois.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppController.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnHebdo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnHebdo.CheckedChanged
        If rbtnHebdo.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppController.str_GetPCDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
        End If
    End Sub

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        btnAfter.Text = ChrW(9660)
        btnBefore.Text = ChrW(9650)

        Select Case False
            Case mcGridBudgetController.bln_Init(grdBudget)
            Case blnFormData_Load()
            Case blnGrdBudget_Load()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        blnGrdBudget_Load()
    End Sub

    Private Sub btnBefore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBefore.Click

        Select Case True
            Case rbtnHebdo.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, -7, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)

            Case rbtnBiMensuel.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, -14, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)

            Case rbtnMensuelle.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, -1, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)

            Case rbtnDeuxMois.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, -2, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)

        End Select

        blnCalculateNextPayDate()
    End Sub

    Private Sub btnAfter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAfter.Click

        Select Case True
            Case rbtnHebdo.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)

            Case rbtnBiMensuel.Checked
                dtpFrom.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)

            Case rbtnMensuelle.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)

            Case rbtnDeuxMois.Checked
                dtpFrom.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)
                dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)

        End Select

        blnCalculateNextPayDate()
    End Sub

    Private Sub rbtnNotPaid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnNotPaid.CheckedChanged

        btnPay.Enabled = rbtnNotPaid.Checked
    End Sub

#End Region

End Class
