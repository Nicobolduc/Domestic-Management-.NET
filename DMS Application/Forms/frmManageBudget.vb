Public Class frmManageBudget

    'Private class members
    Private WithEvents mcGridBudget As clsDataGridView
    Private WithEvents mcSQL As clsSQL_Transactions


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean = True

        dtpFrom.Value = Date.Today
        dtpTo.Value = DateAdd(DateInterval.Day, 14, Date.Today)

        Return blnReturn
    End Function

#End Region
   

#Region "Private events"

    Private Sub frmGestionBudget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mcGridBudget = New clsDataGridView

        Try
            Select Case False
                Case blnLoadData()
                Case mcGridBudget.bln_Init(grdBudget)
                    'Case mcGrid.bln_FillData(
                Case Else

            End Select

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub mcGrid_SetDisplay() Handles mcGridBudget.SetDisplay

        grdBudget.Rows.Add("20-08-2014", "Camping", "56.00$")

        grdBudget.Rows(0).DefaultCellStyle.BackColor = Color.Yellow

        grdBudget.Rows.Add("20-08-2014", "Ass. Auto", "250.50$")

        grdBudget.Columns(0).HeaderText = "Échéance"
        grdBudget.Columns(1).HeaderText = "Charge"
        grdBudget.Columns(2).HeaderText = "Montant"


        'grdBudget.ColumnHeadersDefaultCellStyle.Font = New Font(grdBudget.Font.Name, FontStyle.Bold)


    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub rbtnHedo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnHebdo.CheckedChanged
        If rbtnHebdo.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 7, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnBiMensuel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnBiMensuel.CheckedChanged
        If rbtnBiMensuel.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Day, 14, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnMensuelle_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnMensuelle.CheckedChanged
        If rbtnMensuelle.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 1, dtpFrom.Value)
        End If
    End Sub

    Private Sub rbtnDeuxMois_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDeuxMois.CheckedChanged
        If rbtnDeuxMois.Checked Then
            dtpFrom.Value = CDate(Format(Date.Today, gcAppControler.str_GetDateFormat))
            dtpTo.Value = DateAdd(DateInterval.Month, 2, dtpFrom.Value)
        End If
    End Sub

#End Region
    
End Class
