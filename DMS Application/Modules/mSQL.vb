Module mSQL

#Region "Public functions"

    Public Function ADOSelect(ByVal vstrSQL As String) As MySqlDataReader
        Dim mySQLCmd As MySqlCommand = Nothing
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            mySQLCmd = New MySqlCommand(vstrSQL, gcAppControler.MySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLCmd) Then
                mySQLCmd.Dispose()
            End If
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

            mySQLCmd = New MySqlCommand(strSQL, gcAppControler.MySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

            If mySQLReader.Read Then
                strReturnValue = mySQLReader.Item(vstrField).ToString
            End If

            mySQLCmd.Dispose()

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return strReturnValue
    End Function

#End Region
    
End Module
