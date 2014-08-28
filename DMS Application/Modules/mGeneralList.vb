Namespace mGeneralList

    Public Module mGeneralList

        Public Enum GeneralLists_ID
            EXPENSES_ID = 1
            PRODUCTS_ID = 2
            PRODUCT_TYPE_ID = 3
            PRODUCT_CATEGORY_ID = 4
        End Enum

        Public Enum GeneralList_AppCapID
            EXPENSES_CAP = 1
            PRODUCTS_CAP = 2
            PRODUCT_TYPE_CAP = 3
            PRODUCT_CATEGORY_CAP = 4
        End Enum

        Public Function blnShowGenList(ByVal vList_ID As mGeneralList.GeneralLists_ID) As Boolean
            Dim blnReturn As Boolean
            Dim strSQL As String = vbNullString
            Dim frmGenList As New frmGeneralList(vList_ID)

            Try
                Select Case vList_ID
                    Case mGeneralList.GeneralLists_ID.EXPENSES_ID
                        strSQL = strGetExpense_SQL()
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.EXPENSES_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCTS_ID
                        strSQL = strGetProducts_SQL()
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCTS_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCT_TYPE_ID
                        strSQL = strGetProductType_SQL()
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCT_TYPE_CAP)

                    Case mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_ID
                        strSQL = strGetProductCategory_SQL()
                        frmGenList.mintGridTag = CStr(GeneralList_AppCapID.PRODUCT_CATEGORY_CAP)

                    Case Else
                        'Do nothing

                End Select

                If strSQL <> vbNullString Then

                    frmGenList.mstrGridSQL = strSQL
                    frmGenList.MdiParent = My.Forms.mdiGeneral

                    frmGenList.Show()
                Else
                    'Do nothing
                End If

            Catch ex As Exception
                blnReturn = False
                gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnReturn
        End Function

#Region "SQL Queries"

        Private Function strGetExpense_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT Expense.Exp_ID, " & vbCrLf
            strSQL = strSQL & "         Expense.Exp_Code, " & vbCrLf
            strSQL = strSQL & "         Period.Per_Desc " & vbCrLf
            strSQL = strSQL & "  FROM Expense " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Period ON Period.Per_ID = Expense.Per_ID " & vbCrLf

            If vstrWhere <> vbNullString Then

            Else
                'Do nothing
            End If

            Return strSQL
        End Function

        Private Function strGetProducts_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT Product.Pro_ID, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & "  FROM Product " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductType ON ProductType.ProT_ID = ProductCategory.ProT_ID " & vbCrLf

            If vstrWhere <> vbNullString Then

            Else
                'Do nothing
            End If

            Return strSQL
        End Function

        Private Function strGetProductType_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductType " & vbCrLf

            If vstrWhere <> vbNullString Then

            Else
                'Do nothing
            End If

            Return strSQL
        End Function

        Private Function strGetProductCategory_SQL(Optional ByVal vstrWhere As String = vbNullString) As String
            Dim strSQL As String = vbNullString

            strSQL = strSQL & "  SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & "  FROM ProductCategory " & vbCrLf

            If vstrWhere <> vbNullString Then

            Else
                'Do nothing
            End If

            Return strSQL
        End Function

#End Region

    End Module

End Namespace


