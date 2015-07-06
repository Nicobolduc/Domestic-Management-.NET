Public Class frmBudgetManagement

    'Private members
    Private mintGrdBudget_Action_col As Short = 1
    Private mintGrdBudget_Exp_ID_col As Short = 2
    Private mintGrdBudget_Exp_BillingDate_col As Short = 3
    Private mintGrdBudget_Exp_Name_col As Short = 4
    Private mintGrdBudget_Exp_Amount_col As Short = 5
    Private mintGrdBudget_Exp_PaidOn_col As Short = 6

    'Private class members
    Private WithEvents mcGridBudget As SyncfusionGridController
    Private WithEvents mcSQL As MySQLController


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            dtpFrom.Value = Date.Today
            dtpTo.Value = DateAdd(DateInterval.Day, 14, Date.Today)

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
            strSQL = strSQL & "        Expense.Exp_BillingDate, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "        Expense.Exp_Amount, " & vbCrLf
            strSQL = strSQL & "        NULL AS PaidOn " & vbCrLf
            strSQL = strSQL & " FROM Expense " & vbCrLf
            'strSQL = strSQL & "  WHERE Expense.Exp_BillingDate BETWEEN " & dtpFrom.

            blnValidReturn = mcGridBudget.bln_FillData(strSQL)

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

        mcGridBudget = New SyncfusionGridController

    End Sub

    Private Sub mcGrid_SetDisplay() Handles mcGridBudget.SetDisplay

        grdBudget.ColWidths(mintGrdBudget_Exp_BillingDate_col) = 200
        grdBudget.ColWidths(mintGrdBudget_Exp_Name_col) = 200
        grdBudget.ColWidths(mintGrdBudget_Exp_Amount_col) = 200

        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).CellValueType = GetType(Double)
        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).CellValueType = GetType(Date)

        grdBudget.ColStyles(mintGrdBudget_Exp_BillingDate_col).Format = gcAppController.str_GetPCDateFormat
        grdBudget.ColStyles(mintGrdBudget_Exp_Amount_col).Format = mConstants.DataFormat.CURRENCY
    End Sub

    Private Sub rbtnHedo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnHebdo.CheckedChanged
        If rbtnHebdo.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppController.str_GetPCDateFormat))
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

        Select Case False
            Case mcGridBudget.bln_Init(grdBudget)
            Case blnGrdBudget_Load()
            Case blnFormData_Load()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If
    End Sub

#End Region

End Class
