
Module mControlsFunctions

    Public Function blnComboBox_LoadFromSQL(ByVal vstrSQL As String, ByVal vstrValueMember As String, ByVal vstrDisplayMember As String, ByVal vblnAllowEmpty As Boolean, ByRef rcboToLoad As ComboBox) As Boolean
        Dim blnReturn As Boolean
        Dim mySQLCmd As MySqlCommand
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            rcboToLoad.Items.Clear()

            mySQLCmd = New MySqlCommand(vstrSQL, gcApp.cMySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

            If vblnAllowEmpty Then
                rcboToLoad.Items.Add(" ")
            Else
                'Do nothing
            End If

            While mySQLReader.Read
                rcboToLoad.Items.Add(New cboListItem(CInt(mySQLReader.Item(0)), CStr(mySQLReader.Item(1))))
            End While

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Private Class cboListItem

        Private Value As Object
        Private Text As String

        Protected Friend Sub New(ByVal strNewValue As Integer, ByVal strNewText As String)
            Value = strNewValue
            Text = strNewText
        End Sub

        Public Overrides Function ToString() As String
            Return Text
        End Function

    End Class

End Module
