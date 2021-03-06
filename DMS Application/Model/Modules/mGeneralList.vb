﻿Namespace mGeneralList

    Public Module mGeneralList

        'Enums
        Public Enum GeneralLists_ID
            EXPENSE_LIST_ID = 1
            PRODUCT_LIST_ID = 2
            PRODUCT_TYPE_LIST_ID = 3
            PRODUCT_CATEGORY_LIST_ID = 4
            PRODUCT_BRAND_LIST_ID = 5
            COMPANY_LIST_ID = 6
            COMPANY_TYPE_LIST_ID = 7
            GROCERY_LIST_ID = 8
            EXPENSE_TYPE_LIST_ID = 9
            INCOME_LIST_ID = 10
            BUDGET_LIST_ID = 11
        End Enum

        Public Enum GeneralList_GridCapID
            EXPENSE_CAP = 1
            PRODUCT_CAP = 2
            PRODUCT_TYPE_CAP = 3
            PRODUCT_CATEGORY_CAP = 4
            PRODUCT_BRAND_CAP = 7
            COMPANY_CAP = 8
            GROCERY_CAP = 13
            EXPENSE_TYPE_CAP = 14
            INCOME_CAP = 16
            BUDGET_CAP = 14
        End Enum


#Region "Functions / Subs"

        Public Sub ShowGenList(ByVal vList_ID As mGeneralList.GeneralLists_ID)
            Dim strListGenTitle As String = String.Empty
            Dim strSQL As String = String.Empty
            Dim frmGenList As New frmGeneralList(vList_ID)

            Try

                Select Case vList_ID
                    Case mGeneralList.GeneralLists_ID.EXPENSE_LIST_ID
                        strSQL = strGetExpenseList_SQL()
                        strListGenTitle = " - Dépenses" 'TODO caption pour ca
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.EXPENSE_CAP)
                        frmGenList.SetFormToOpenName = frmExpense.Name

                    Case mGeneralList.GeneralLists_ID.EXPENSE_TYPE_LIST_ID
                        strSQL = strGetExpenseTypeList_SQL()
                        strListGenTitle = " - Types de dépense"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.GROCERY_CAP)
                        frmGenList.SetFormToOpenName = frmExpenseType.Name

                    Case mGeneralList.GeneralLists_ID.PRODUCT_LIST_ID
                        strSQL = strGetProductsList_SQL()
                        strListGenTitle = " - Produits"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.PRODUCT_CAP)
                        frmGenList.SetFormToOpenName = frmProduct.Name

                    Case mGeneralList.GeneralLists_ID.PRODUCT_TYPE_LIST_ID
                        strSQL = strGetProductTypeList_SQL()
                        strListGenTitle = " - Types de produit"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.PRODUCT_TYPE_CAP)
                        frmGenList.SetFormToOpenName = frmProductType.Name

                    Case mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_LIST_ID
                        strSQL = strGetProductCategoryList_SQL()
                        strListGenTitle = " - Catégories de produit"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.PRODUCT_CATEGORY_CAP)
                        frmGenList.SetFormToOpenName = frmProductCategory.Name

                    Case mGeneralList.GeneralLists_ID.PRODUCT_BRAND_LIST_ID
                        strSQL = strGetProductBrandList_SQL()
                        strListGenTitle = " - Marques de produit"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.PRODUCT_BRAND_CAP)
                        frmGenList.SetFormToOpenName = frmProductBrand.Name

                    Case mGeneralList.GeneralLists_ID.COMPANY_LIST_ID
                        strSQL = strGetCompanyList_SQL()
                        strListGenTitle = " - Compagnies"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.COMPANY_CAP)
                        frmGenList.SetFormToOpenName = frmCompany.Name

                    Case mGeneralList.GeneralLists_ID.GROCERY_LIST_ID
                        strSQL = strGetGroceryList_SQL()
                        strListGenTitle = " - Épiceries"
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.GROCERY_CAP)
                        frmGenList.SetFormToOpenName = frmGrocery.Name

                    Case mGeneralList.GeneralLists_ID.INCOME_LIST_ID
                        strSQL = strGetIncomeList_SQL()
                        strListGenTitle = " - Revenus" 'TODO caption pour ca
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.INCOME_CAP)
                        frmGenList.SetFormToOpenName = frmIncome.Name

                    Case mGeneralList.GeneralLists_ID.BUDGET_LIST_ID
                        strSQL = strGetBudgetList_SQL()
                        strListGenTitle = " - Budgets" 'TODO caption pour ca
                        frmGenList.mintGridTag = CStr(GeneralList_GridCapID.BUDGET_CAP)
                        frmGenList.SetFormToOpenName = frmBudgetManagement.Name
                        frmGenList.ChildFormIsModal = False

                    Case Else
                        'Do nothing

                End Select

                If strSQL <> String.Empty Then

                    frmGenList.mstrGridSQL = strSQL
                    frmGenList.Text = frmGenList.Text & strListGenTitle
                    frmGenList.MdiParent = My.Forms.mdiGeneral

                    frmGenList.formController.ShowForm(mConstants.Form_Mode.CONSULT_MODE)

                    If My.Forms.mdiGeneral.GetGenListChildCount = 0 Then
                        frmGenList.Location = New Point(0, 0)
                    Else
                        frmGenList.Location = New Point(My.Forms.mdiGeneral.GetGenListChildCount * 25, My.Forms.mdiGeneral.GetGenListChildCount * 25)
                    End If

                    My.Forms.mdiGeneral.AddGenListHandle(DirectCast(frmGenList, Object), frmGenList.Handle.ToInt32)
                End If

            Catch ex As Exception
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

        End Sub

#End Region


#Region "SQL Queries"

        Private Function strGetExpenseList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "         Expense.Exp_Name, " & vbCrLf
            strSQL = strSQL & "         Period.Per_Name " & vbCrLf
            strSQL = strSQL & "  FROM Expense " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Period ON Period.Per_ID = Expense.Per_ID " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY Expense.Exp_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetExpenseTypeList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT ExpenseType.ExpT_ID, " & vbCrLf
            strSQL = strSQL & "         ExpenseType.ExpT_Name " & vbCrLf
            strSQL = strSQL & "  FROM ExpenseType " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY ExpenseType.ExpT_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductsList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT Product.Pro_ID, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Name, " & vbCrLf
            'strSQL = strSQL & "         ProductType.ProT_Name, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN ProductCategory.ProC_Name IS NULL THEN '' ELSE ProductCategory.ProC_Name END AS ProC_Name" & vbCrLf
            strSQL = strSQL & "  FROM Product " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductType ON ProductType.ProT_ID = Product.ProT_ID " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID AND ProductType.ProT_ID = ProductCategory.ProT_ID " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY Product.Pro_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductTypeList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductType " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY ProductType.ProT_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductCategoryList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductCategory " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY ProductCategory.ProC_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductBrandList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductBrand " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY ProductBrand.ProB_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetCompanyList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "         Company.Cy_Name " & vbCrLf
            strSQL = strSQL & "  FROM Company " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY Company.Cy_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetGroceryList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT Grocery.Gro_ID, " & vbCrLf
            strSQL = strSQL & "         Grocery.Gro_Name, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN SUM(TProPrice.ProP_Price) = 0 THEN NULL ELSE ROUND(SUM(TProPrice.ProP_Price),2) END As TotalCost " & vbCrLf
            strSQL = strSQL & "  FROM Grocery " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN Gro_Pro ON Gro_Pro.Gro_ID = Grocery.Gro_ID " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN (SELECT ProP_Price, Pro_ID, ProB_ID " & vbCrLf
            strSQL = strSQL & "                 FROM ProductPrice " & vbCrLf
            strSQL = strSQL & "                ) As TProPrice ON TProPrice.Pro_ID = Gro_Pro.Pro_ID AND TProPrice.ProB_ID = Gro_Pro.ProB_ID " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & " GROUP BY Grocery.Gro_ID " & vbCrLf
            'strSQL = strSQL & " HAVING TotalCost IS NOT NULL " & vbCrLf
            strSQL = strSQL & " ORDER BY Grocery.Gro_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetIncomeList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT Income.Inc_ID, " & vbCrLf
            strSQL = strSQL & "         Income.Inc_Name, " & vbCrLf
            strSQL = strSQL & "         Period.Per_Name, " & vbCrLf
            strSQL = strSQL & "         Budget.Bud_Name " & vbCrLf
            strSQL = strSQL & "  FROM Income " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Period ON Period.Per_ID = Income.Per_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Budget ON Budget.Bud_ID = Income.Bud_ID " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY Income.Inc_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetBudgetList_SQL(Optional ByVal vstrWhere As String = Nothing) As String
            Dim strSQL As String = String.Empty

            strSQL = strSQL & "  SELECT Budget.Bud_ID, " & vbCrLf
            strSQL = strSQL & "         Budget.Bud_Name " & vbCrLf
            strSQL = strSQL & "  FROM Budget " & vbCrLf

            If vstrWhere <> String.Empty Then

            End If

            strSQL = strSQL & "  ORDER BY Budget.Bud_Name " & vbCrLf

            Return strSQL
        End Function

#End Region

    End Module

End Namespace


