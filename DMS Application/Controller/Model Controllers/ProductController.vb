Public Class ProductController

    Public Function GetProductFromID(ByVal vintProduct_ID As Integer) As Model.Product
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cProduct As New Model.Product

        vintProduct_ID = vintProduct_ID

        Try
            strSQL = strSQL & " SELECT Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "        Product.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        Product.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        Product.Pro_Taxable " & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & " WHERE Product.Pro_ID = " & vintProduct_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            mySQLReader.Read()

            cProduct.Name = mySQLReader.Item("Pro_Name").ToString

            cProduct.isTaxable = CBool(mySQLReader.Item("Pro_Taxable"))

            cProduct.Type_ID = CInt(mySQLReader.Item("ProT_ID"))

            If Not IsDBNull(mySQLReader.Item("ProC_ID")) Then
                cProduct.Category_ID = CInt(mySQLReader.Item("ProC_ID"))
            Else
                cProduct.Category_ID = 0
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

End Class
