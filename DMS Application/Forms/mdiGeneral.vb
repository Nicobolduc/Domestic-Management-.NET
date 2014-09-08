Public Class mdiGeneral

    Private Sub mdiGeneral_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        gcApplication.MySQLConnection.Close()
        gcApplication.MySQLConnection.Dispose()
    End Sub

    Private Sub mdiGeneral_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        main()

        lblStatusBD.Text = lblStatusBD.Text & gcApplication.MySQLConnection.Database
        lblStatusUser.Text = lblStatusUser.Text & " Nicolas"
    End Sub

    Private Sub mnuiBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiBudget.Click
        Dim frmGestionBudget As New frmManageBudget

        frmGestionBudget.MdiParent = Me

        frmGestionBudget.Width = 1024
        frmGestionBudget.Height = 768

        frmGestionBudget.myFormControler.ShowForm(clsConstants.Form_Modes.CONSULT_MODE)
    End Sub

    Private Sub mnuiExpenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiExpense.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.EXPENSES_LIST_ID)
    End Sub

    Private Sub mnuiProductToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiProducts.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.PRODUCTS_LIST_ID)
    End Sub

    Private Sub mnuiProductCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiProductCategory.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_LIST_ID)
    End Sub

    Private Sub mnuiProductType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiProductType.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.PRODUCT_TYPE_LIST_ID)
    End Sub

    Private Sub mnuiBrand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiBrand.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.PRODUCT_BRAND_LIST_ID)
    End Sub

    Private Sub CompagnieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompagnieToolStripMenuItem.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.COMPANY_LIST_ID)
    End Sub

    Private Sub ÉpicerieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÉpicerieToolStripMenuItem.Click
        Dim frmGestionBudget As New frmGrocery

        frmGestionBudget.MdiParent = Me

        frmGestionBudget.myFormControler.ShowForm(clsConstants.Form_Modes.CONSULT_MODE)
    End Sub
End Class