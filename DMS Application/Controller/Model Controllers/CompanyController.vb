Public Class CompanyController

    Public Function GetCompany(ByVal vintCompany_ID As Integer) As Model.Company
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim cCompany As Model.Company = Nothing

        Try
            strSQL = strSQL & " SELECT Company.Cy_Name, " & vbCrLf
            strSQL = strSQL & "        Company.CyT_ID " & vbCrLf
            strSQL = strSQL & " FROM Company " & vbCrLf
            strSQL = strSQL & " WHERE Company.Cy_ID = " & vintCompany_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                cCompany = New Model.Company
                cCompany.ID = vintCompany_ID
                cCompany.Name = mySQLReader.Item("Cy_Name").ToString
                cCompany.Type_ID = CInt(mySQLReader.Item("CyT_ID"))

                blnValidReturn = True
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

        Return cCompany
    End Function

End Class
