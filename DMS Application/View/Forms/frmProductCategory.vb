Public Class frmProductCategory

    'Private class members
    Private mcSQL As MySQLController


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT ProductCategory.ProC_Name, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProT_ID " & vbCrLf
            strSQL = strSQL & " FROM ProductCategory " & vbCrLf
            strSQL = strSQL & " WHERE ProductCategory.ProC_ID = " & formController.Item_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            mySQLReader.Read()

            txtName.Text = mySQLReader.Item("ProC_Name").ToString

            cboType.SelectedValue = CInt(mySQLReader.Item("ProT_ID"))

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

    Private Function blnCboType_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " ORDER BY ProductType.ProT_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "ProT_ID", "ProT_Name", False, cboType)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            mcSQL.bln_BeginTransaction()

            Select Case formController.FormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    blnValidReturn = blnProductCategory_Insert()

                Case mConstants.Form_Modes.UPDATE_MODE
                    blnValidReturn = blnProductCategory_Update()

                Case mConstants.Form_Modes.DELETE_MODE
                    blnValidReturn = blnProductCategory_Delete()

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

    Private Function blnProductCategory_Insert() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("ProC_Name", txtName.Text, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("ProT_ID", CStr(cboType.SelectedValue), MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_ADOInsert("ProductCategory", formController.Item_ID)
                Case formController.Item_ID > 0
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductCategory_Update() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("ProC_Name", txtName.Text, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("ProT_ID", CStr(cboType.SelectedValue), MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_ADOUpdate("ProductCategory", "ProC_ID = " & formController.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductCategory_Delete() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("ProductCategory", "ProC_ID = " & formController.Item_ID)
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

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        Select Case False
            Case blnCboType_Load()
            Case formController.FormMode <> mConstants.Form_Modes.INSERT_MODE
                blnValidReturn = True
            Case blnLoadData()
            Case Else
                blnValidReturn = True
        End Select
    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Select Case False
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboType.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

#End Region
    
End Class