Public Class frmBudgetManagement

    'Private members
    Private mintGrdBudget_Action_col As Short = 1
    Private mintGrdBudget_Exp_ID_col As Short = 2
    Private mintGrdBudget_Income_Date_col As Short = 3
    Private mintGrdBudget_Exp_BillingDate_col As Short = 4
    Private mintGrdBudget_Exp_Name_col As Short = 5
    Private mintGrdBudget_Exp_Amount_col As Short = 6
    Private mintGrdBudget_Amount_Paid_col As Short = 7
    Private mintGrdBudget_Exp_PaidOn_col As Short = 8
    Private mintGrdBudget_ExpT_Name_col As Short = 9
    Private mintGrdBudget_ExpT_ArgbColor_col As Short = 10
    Private mintGrdBudget_Income_Name_col As Short = 11
    Private mintGrdBudget_Income_Amount_col As Short = 12
    Private mintGrdBudget_Period_Name_col As Short = 13
    Private mintGrdBudget_Comment_col As Short = 14
    Private mintGrdBudget_Sel_col As Short = 15

    'Private class members
    Private WithEvents mcGridBudgetController As SyncfusionGridController
    Private WithEvents mcSQL As MySQLController


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            dtpFrom.Value = Date.Today
            dtpFrom.CustomFormat = gcAppController.str_GetPCDateFormat

            dtpTo.Value = DateAdd(DateInterval.Day, 14, Date.Today)
            dtpTo.CustomFormat = gcAppController.str_GetPCDateFormat

            blnValidReturn = True

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            blnValidReturn = False
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdBudget_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        
        Try
            strSQL = strSQL & " SELECT " & DataGridViewController.GridRowActions.CONSULT_ACTION & " AS Action, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_ReceptDate, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_BillingDate, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Amount, " & vbCrLf
            strSQL = strSQL & "        NULL AS Amount_Paid, " & vbCrLf
            strSQL = strSQL & "        NULL As PaidOn, " & vbCrLf
            strSQL = strSQL & "        ExpenseType.ExpT_Name, " & vbCrLf
            strSQL = strSQL & "        ExpenseType.ExpT_ArgbColor, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_Name, " & vbCrLf
            strSQL = strSQL & "        Income.Inc_Amount, " & vbCrLf
            strSQL = strSQL & "        Period.Per_Name, " & vbCrLf
            strSQL = strSQL & "        NULL AS Comment, " & vbCrLf
            strSQL = strSQL & "        CASE WHEN Expense.Exp_Fixed = 1 THEN 'TRUE' ELSE 'FALSE' END AS SelCol " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ExpenseType ON ExpenseType.ExpT_ID = Expense.ExpT_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Income ON Income.Inc_IsMain = 1 " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Period ON Period.Per_ID = Income.Per_ID " & vbCrLf
            strSQL = strSQL & "  WHERE Expense.Exp_BillingDate BETWEEN " & MySQLController.str_FixDateForSelect(dtpFrom.Value) & " AND " & MySQLController.str_FixDateForSelect(dtpTo.Value) & vbCrLf
            strSQL = strSQL & " ORDER BY Income.Inc_ReceptDate, Expense.Exp_BillingDate " & vbCrLf

            blnValidReturn = mcGridBudgetController.bln_FillData(strSQL)

            If blnValidReturn Then

                For intRowIdx As Integer = 1 To grdBudget.RowCount

                    grdBudget(intRowIdx, mintGrdBudget_Exp_Name_col).BackColor = Color.FromArgb(CInt(grdBudget(intRowIdx, mintGrdBudget_ExpT_ArgbColor_col).CellValue))
                Next

                lblMainIncomeDate.Text = Format(grdBudget(grdBudget.RowCount, mintGrdBudget_Income_Date_col).CellValue, gcAppController.str_GetPCDateFormat)
                lblMainIncomeAmount.Text = grdBudget(grdBudget.RowCount, mintGrdBudget_Income_Amount_col).CellValue.ToString & " $"

            End If

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
        grdBudget.ColWidths(mintGrdBudget_Amount_Paid_col) = 100
        grdBudget.ColWidths(mintGrdBudget_Exp_PaidOn_col) = 110
        grdBudget.ColWidths(mintGrdBudget_Income_Date_col) = 110
        grdBudget.ColWidths(mintGrdBudget_Comment_col) = 230

        mcGridBudgetController.blnSetColType_CheckBox(mintGrdBudget_Sel_col, False)

        mcGridBudgetController.blnSetColType_DateTimePicker(mintGrdBudget_Exp_PaidOn_col, True)

        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).CellValueType = GetType(Double)
        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).CellValueType = GetType(Date)

        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).Format = gcAppController.str_GetPCDateFormat
        grdBudget.ColStyles(mintGrdBudget_Income_Date_col).Format = gcAppController.str_GetPCDateFormat
        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).Format = mConstants.DataFormat.CURRENCY
        grdBudget.ColStyles(mintGrdBudget_Amount_Paid_col).Format = mConstants.DataFormat.CURRENCY

        grdBudget.Model.Options.MergeCellsMode = GridMergeCellsMode.OnDemandCalculation Or GridMergeCellsMode.MergeRowsInColumn
        grdBudget.ColStyles(mintGrdBudget_Income_Date_col).MergeCell = GridMergeCellDirection.RowsInColumn
        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).MergeCell = GridMergeCellDirection.RowsInColumn

        grdBudget.TableStyle.VerticalAlignment = GridVerticalAlignment.Middle

        mcGridBudgetController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.EXTEND_LAST_COL

        For intCol As Integer = 1 To grdBudget.ColCount
            If intCol <> mintGrdBudget_Exp_PaidOn_col And intCol <> mintGrdBudget_Comment_col And intCol <> mintGrdBudget_Amount_Paid_col And intCol <> mintGrdBudget_Sel_col Then
                grdBudget.ColStyles(intCol).ReadOnly = True
            End If
        Next

    End Sub

    Private Sub rbtnHedo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnHebdo.CheckedChanged
        Dim format() = {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy"}

        If rbtnHebdo.Checked Then
            'dtpFrom.Value = CDate(format(DateTime.Today, gcAppController.str_GetPCDateTimeFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
        End If
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

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        btnAfter.Text = ChrW(9660)
        btnBefore.Text = ChrW(9650)

        Select Case False
            Case mcGridBudgetController.bln_Init(grdBudget)
            Case blnGrdBudget_Load()
            Case blnFormData_Load()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        blnGrdBudget_Load()
    End Sub

    Private Sub btnBefore_Click(sender As Object, e As EventArgs) Handles btnBefore.Click

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

    End Sub

    Private Sub btnAfter_Click(sender As Object, e As EventArgs) Handles btnAfter.Click

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
    End Sub

#End Region

End Class
