Module mSQL

    Public Function ADOSelect(ByVal vstrSQL As String) As MySqlDataReader
        Dim mySQLCmd As MySqlCommand = Nothing
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            mySQLCmd = New MySqlCommand(vstrSQL, gcApp.cMySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

            mySQLCmd.Dispose()

        Catch ex As Exception
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return mySQLReader
    End Function

    Public Function str_ADOSingleLookUp(ByVal vstrField As String, ByVal vstrTable As String, ByVal vstrWhere As String) As String
        Dim strReturnValue As String = vbNullString
        Dim mySQLCmd As MySqlCommand = Nothing
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim strSQL As String = vbNullString

        Try
            strSQL = "SELECT " & vstrField & " FROM " & vstrTable & " WHERE " & vstrWhere

            mySQLCmd = New MySqlCommand(strSQL, gcApp.cMySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

            If mySQLReader.Read Then
                strReturnValue = mySQLReader.Item(vstrField).ToString
            Else
                'Do nothing
            End If

            mySQLCmd.Dispose()

        Catch ex As Exception
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return strReturnValue
    End Function

End Module
