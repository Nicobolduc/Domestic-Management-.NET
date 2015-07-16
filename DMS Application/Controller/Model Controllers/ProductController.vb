Public Class ProductController

    Public Function GetProduct(ByVal vintProduct_ID As Integer) As Model.Product
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cProduct As Model.Product = Nothing
        Dim intProductType_ID As Integer
        Dim intProductCategory_ID As Integer

        Try
            strSQL = strSQL & " SELECT Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "        Product.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        Product.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProC_Name, " & vbCrLf
            strSQL = strSQL & "        Product.Pro_Taxable " & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductType ON ProductType.ProT_ID = Product.ProT_ID " & vbCrLf
            strSQL = strSQL & "      " & vbCrLf
            strSQL = strSQL & " WHERE Product.Pro_ID = " & vintProduct_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read() Then

                cProduct = New Model.Product
                cProduct.ID = vintProduct_ID
                cProduct.Name = mySQLReader.Item("Pro_Name").ToString
                cProduct.IsTaxable = CBool(mySQLReader.Item("Pro_Taxable"))

                intProductType_ID = CInt(mySQLReader.Item("ProT_ID"))
                intProductCategory_ID = CInt(IIf(Not IsDBNull(mySQLReader.Item("ProC_ID")), mySQLReader.Item("ProC_ID"), 0))

                mySQLReader.Dispose()

                cProduct.Type = GetProductType(intProductType_ID)

                If intProductCategory_ID > 0 Then

                    cProduct.Category = GetProductCategory(intProductCategory_ID)
                Else
                    cProduct.Category = Nothing
                End If
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cProduct
    End Function

    Public Function GetProductType(ByVal vintProductType_ID As Integer) As Model.ProductType
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cProductType As Model.ProductType = Nothing

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " WHERE ProductType.ProT_ID = " & vintProductType_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cProductType = New Model.ProductType
                cProductType.ID = vintProductType_ID
                cProductType.Name = mySQLReader.Item("ProT_Name").ToString

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cProductType
    End Function

    Public Function GetProductCategory(ByVal vintProductCategory_ID As Integer) As Model.ProductCategory
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cProductCategory As Model.ProductCategory = Nothing

        Try
            strSQL = strSQL & " SELECT ProductCategory.ProC_Name, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductCategory " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductType ON ProductType.ProT_ID = ProductCategory.ProT_ID " & vbCrLf
            strSQL = strSQL & " WHERE ProductCategory.ProC_ID = " & vintProductCategory_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read() Then

                cProductCategory = New Model.ProductCategory
                cProductCategory.Name = mySQLReader.Item("ProC_Name").ToString
                cProductCategory.Type.ID = CInt(mySQLReader.Item("ProT_ID"))
                cProductCategory.Type.Name = CStr(mySQLReader.Item("ProT_Name"))

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cProductCategory
    End Function

    Public Function GetProductBrand(ByVal vintProductType_ID As Integer) As Model.ProductBrand
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cProductBrand As Model.ProductBrand = Nothing

        Try
            strSQL = strSQL & " SELECT ProductBrand.ProB_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductBrand " & vbCrLf
            strSQL = strSQL & " WHERE ProductBrand.ProB_ID = " & vintProductType_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read() Then

                cProductBrand = New Model.ProductBrand
                cProductBrand.ID = vintProductType_ID
                cProductBrand.Name = CStr(mySQLReader.Item("ProB_Name"))

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return cProductBrand
    End Function

End Class
