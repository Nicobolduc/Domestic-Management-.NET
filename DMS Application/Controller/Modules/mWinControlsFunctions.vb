Imports System.ComponentModel

Namespace mWinControlsFunctions
    Module mWinControlsFunctions

        Public Function blnComboBox_LoadFromSQL(ByVal vstrSQL As String, ByVal vstrValueMember As String, ByVal vstrDisplayMember As String, ByVal vblnAllowEmpty As Boolean, ByRef rcboToLoad As ComboBox) As Boolean
            Dim blnValidReturn As Boolean
            Dim mySQLCmd As MySqlCommand
            Dim mySQLReader As MySqlDataReader = Nothing
            Dim myBindingList As New BindingList(Of KeyValuePair(Of Integer, String))

            Try
                rcboToLoad.DataSource = Nothing

                mySQLCmd = New MySqlCommand(vstrSQL, gcAppCtrl.MySQLConnection)

                mySQLReader = mySQLCmd.ExecuteReader

                If vblnAllowEmpty Then
                    myBindingList.Add(New KeyValuePair(Of Integer, String)(0, ""))
                End If

                While mySQLReader.Read
                    If Not IsDBNull(mySQLReader(vstrValueMember)) Then
                        myBindingList.Add(New KeyValuePair(Of Integer, String)(CInt(mySQLReader(vstrValueMember)), CStr(mySQLReader(vstrDisplayMember))))
                    End If
                End While

                rcboToLoad.DataSource = myBindingList
                rcboToLoad.ValueMember = "Key"
                rcboToLoad.DisplayMember = "Value"
                rcboToLoad.SelectedIndex = 0

                blnValidReturn = True

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source & " - " & System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
            Finally
                If Not IsNothing(mySQLReader) Then
                    mySQLReader.Dispose()
                End If
            End Try

            Return blnValidReturn
        End Function

    End Module

End Namespace