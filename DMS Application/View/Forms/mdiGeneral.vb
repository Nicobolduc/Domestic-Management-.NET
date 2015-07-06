Public Class mdiGeneral

    Private mChildsFormsCol As Collection


#Region "Properties"

    Public ReadOnly Property GetGenListChildCount As Integer
        Get
            Return mChildsFormsCol.Count
        End Get
    End Property

#End Region


#Region "Public Functions/Subs"

    Public Sub AddGenListHandle(ByRef rfrmGenListChild As Object, ByRef vintHandle As Int32)
        mChildsFormsCol.Add(rfrmGenListChild, vintHandle.ToString)
    End Sub

    Public Sub RemoveGenListHandle(ByRef vintHandle As Int32)
        mChildsFormsCol.Remove(vintHandle.ToString)
    End Sub

#End Region


#Region "Private Events"

    Private Sub mdiGeneral_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        gcAppController.MySQLConnection.Close()
        gcAppController.MySQLConnection.Dispose()
    End Sub

    Private Sub mdiGeneral_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        main()

        mChildsFormsCol = New Collection()

        lblStatusBD.Text = lblStatusBD.Text & gcAppController.MySQLConnection.Database
        lblStatusUser.Text = lblStatusUser.Text & " Nicolas"
    End Sub

    Private Sub mnuiBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiBudget.Click
        Dim frmGestionBudget As New frmBudgetManagement

        frmGestionBudget.MdiParent = Me

        frmGestionBudget.Width = 1024
        frmGestionBudget.Height = 768

        frmGestionBudget.formController.ShowForm(mConstants.Form_Mode.CONSULT_MODE)
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

    Private Sub mnuiCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiCompany.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.COMPANY_LIST_ID)
    End Sub

    Private Sub mnuiGrocery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiGrocery.Click
        mGeneralList.ShowGenList(mGeneralList.GeneralLists_ID.GROCERY_LIST_ID)
    End Sub

#End Region

    Private Sub TestsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestsToolStripMenuItem.Click
        Dim test = New test

        test.Show()
    End Sub
End Class