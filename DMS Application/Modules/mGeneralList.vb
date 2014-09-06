Namespace mGeneralList

    Public Module mGeneralList

        'Enums
        Public Enum GeneralLists_ID
            EXPENSES_LIST_ID = 1
            PRODUCTS_LIST_ID = 2
            PRODUCT_TYPE_LIST_ID = 3
            PRODUCT_CATEGORY_LIST_ID = 4
            PRODUCT_BRAND_LIST_ID = 5
            COMPANY_LIST_ID = 6
        End Enum

        Public Enum GeneralList_AppCapID
            EXPENSES_CAP = 1
            PRODUCTS_CAP = 2
            PRODUCT_TYPE_CAP = 3
            PRODUCT_CATEGORY_CAP = 4
            PRODUCT_BRAND_CAP = 7
            COMPANY_CAP = 8
        End Enum


#Region "Functions / Subs"

        Public Sub ShowGenList(ByVal vList_ID As mGeneralList.GeneralLists_ID)
            Dim strListName As String = vbNullString
            Dim strSQL As String = vbNullString
            Dim frmGenList As New frmGeneralList(vList_ID)

            Try
                Select Case vList_ID
                    Case mGeneralList.GeneralLists_ID.EXPENSES_LIST_ID
                        strSQL = strGetExpenseList_SQL()
                        strListName = " - Dépenses"
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.EXPENSES_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCTS_LIST_ID
                        strSQL = strGetProductsList_SQL()
                        strListName = " - Produits"
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCTS_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCT_TYPE_LIST_ID
                        strSQL = strGetProductTypeList_SQL()
                        strListName = " - Types de produit"
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCT_TYPE_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_LIST_ID
                        strSQL = strGetProductCategoryList_SQL()
                        strListName = " - Catégories de produit"
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCT_CATEGORY_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCT_BRAND_LIST_ID
                        strSQL = strGetProductBrandList_SQL()
                        strListName = " - Marques de produit"
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCT_BRAND_CAP)

                    Case mGeneralList.GeneralLists_ID.COMPANY_LIST_ID
                        strSQL = strGetCompanyList_SQL()
                        strListName = " - Compagnies"
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.COMPANY_CAP)

                    Case Else
                        'Do nothing

                End Select

                If strSQL <> vbNullString Then

                    frmGenList.mstrGridSQL = strSQL
                    frmGenList.Text = frmGenList.Text & strListName
                    frmGenList.MdiParent = My.Forms.mdiGeneral

                    frmGenList.myFormControler.ShowForm(clsConstants.Form_Modes.CONSULT_MODE)
                End If

            Catch ex As Exception
                gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

        End Sub

#End Region


#Region "SQL Queries"

        Private Function strGetExpenseList_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "         Expense.Exp_Code, " & vbCrLf
            strSQL = strSQL & "         Period.Per_Desc " & vbCrLf
            strSQL = strSQL & "  FROM Expense " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Period ON Period.Per_ID = Expense.Per_ID " & vbCrLf

            If vstrWhere <> vbNullString Then


            End If

            strSQL = strSQL & "  ORDER BY Expense.Exp_Code " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductsList_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT Product.Pro_ID, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & "  FROM Product " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductType ON ProductType.ProT_ID = Product.ProT_ID " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID AND ProductType.ProT_ID = ProductCategory.ProT_ID " & vbCrLf

            If vstrWhere <> vbNullString Then


            End If

            strSQL = strSQL & "  ORDER BY Product.Pro_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductTypeList_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductType " & vbCrLf

            If vstrWhere <> vbNullString Then

            End If

            strSQL = strSQL & "  ORDER BY ProductType.ProT_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductCategoryList_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductCategory " & vbCrLf

            If vstrWhere <> vbNullString Then


            End If

            strSQL = strSQL & "  ORDER BY ProductCategory.ProC_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetProductBrandList_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductBrand " & vbCrLf

            If vstrWhere <> vbNullString Then

            End If

            strSQL = strSQL & "  ORDER BY ProductBrand.ProB_Name " & vbCrLf

            Return strSQL
        End Function

        Private Function strGetCompanyList_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "         Company.Cy_Name " & vbCrLf
            strSQL = strSQL & "  FROM Company " & vbCrLf

            If vstrWhere <> vbNullString Then

            End If

            strSQL = strSQL & "  ORDER BY Company.Cy_Name " & vbCrLf

            Return strSQL
        End Function

#End Region

    End Module

End Namespace


