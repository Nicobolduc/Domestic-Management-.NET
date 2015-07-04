Public Class ProductController

    Public Function GetProduct(ByVal vintProduct_ID As Integer) As Model.Product
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cProduct As Model.Product = Nothing

        Try
            strSQL = strSQL & " SELECT Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "        Product.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        Product.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProC_Name, " & vbCrLf
            strSQL = strSQL & "        Product.Pro_Taxable " & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductType ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "      " & vbCrLf
            strSQL = strSQL & " WHERE Product.Pro_ID = " & vintProduct_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read() Then

                cProduct = New Model.Product
                cProduct.ID = vintProduct_ID
                cProduct.Name = mySQLReader.Item("Pro_Name").ToString
                cProduct.IsTaxable = CBool(mySQLReader.Item("Pro_Taxable"))

                cProduct.Type.ID = CInt(mySQLReader.Item("ProT_ID"))
                cProduct.Type.Name = CStr(mySQLReader.Item("ProT_ID"))

                If Not IsDBNull(mySQLReader.Item("ProC_ID")) Then

                    cProduct.Category.ID = CInt(mySQLReader.Item("ProC_ID"))
                    cProduct.Category.Name = mySQLReader.Item("ProC_Name").ToString
                    cProduct.Category.Type_ID = cProduct.Type.ID
                Else
                    cProduct.Category.ID = 0
                End If
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
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
            End If

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return cProductType
    End Function

End Class
