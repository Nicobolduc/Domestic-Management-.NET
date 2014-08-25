﻿Public Class mdiGeneral

    Private Sub mdiGeneral_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        gcApp.cMySQLConnection.Close()
        gcApp.cMySQLConnection.Dispose()
    End Sub

    Private Sub mdiGeneral_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        main()

        lblStatusBD.Text = gcApp.cMySQLConnection.Database
        lblStatusUser.Text = "Nicolas"
    End Sub

    Private Sub mnuiBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiBudget.Click
        Dim frmGestionBudget As New frmManageBudget

        frmGestionBudget.MdiParent = Me

        frmGestionBudget.Width = 1024
        frmGestionBudget.Height = 768

        frmGestionBudget.Show()
    End Sub

    Private Sub mnuiExpenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiExpense.Click
        mGeneralList.blnShowGenList(mGeneralList.GeneralLists_ID.EXPENSES_ID)
    End Sub

    Private Sub mnuiProductToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiProducts.Click
        mGeneralList.blnShowGenList(mGeneralList.GeneralLists_ID.PRODUCTS_ID)
    End Sub

    Private Sub mnuiProductCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiProductCategory.Click
        mGeneralList.blnShowGenList(mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_ID)
    End Sub

End Class