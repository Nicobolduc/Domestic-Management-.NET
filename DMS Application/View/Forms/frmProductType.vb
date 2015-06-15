Public Class frmBrandProto

    'Private class members
    Private mcSQL As MySQLController


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " WHERE ProductType.ProT_ID = " & myFormControler.Item_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read Then

                txtName.Text = mySQLReader.Item("ProT_Name").ToString
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

        Return blnValidReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            mcSQL.bln_BeginTransaction()

            Select Case myFormControler.FormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    blnValidReturn = blnProductType_Insert()

                Case mConstants.Form_Modes.UPDATE_MODE
                    blnValidReturn = blnProductType_Update()

                Case mConstants.Form_Modes.DELETE_MODE
                    blnValidReturn = blnProductType_Delete()

            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
            mcSQL = Nothing
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductType_Insert() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("ProT_Name", txtName.Text, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOInsert("ProductType", myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductType_Update() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("ProT_Name", txtName.Text, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOUpdate("ProductType", "ProT_ID = " & myFormControler.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductType_Delete() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("ProductType", "ProT_ID = " & myFormControler.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnValidReturn As Boolean

        Select Case False
            Case myFormControler.FormMode <> mConstants.Form_Modes.INSERT_MODE
                blnValidReturn = True
            Case blnLoadData()
            Case Else
                blnValidReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules
        Select Case False
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        myFormControler.ChangeMade = True
    End Sub

#End Region

End Class