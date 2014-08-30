Public Class frmProduct

    Private mcSQL As clsSQL_Transactions


    Private Function blnCboType_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " ORDER BY ProductType.ProT_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "ProT_ID", "ProT_Name", True, cboType)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboCategory_Load(Optional ByVal vintSelectedValue As Integer = 0) As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductCategory " & vbCrLf
            strSQL = strSQL & " WHERE ProductCategory.ProT_ID = " & CInt(cboType.SelectedValue) & vbCrLf
            strSQL = strSQL & " ORDER BY ProductCategory.ProC_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "ProC_ID", "ProC_Name", True, cboCategory)

            cboCategory.SelectedValue = vintSelectedValue

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboBrand_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT Brand.Bra_ID, " & vbCrLf
            strSQL = strSQL & "        Brand.Bra_Name " & vbCrLf
            strSQL = strSQL & " FROM Brand " & vbCrLf
            strSQL = strSQL & " ORDER BY Brand.Bra_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "Bra_ID", "Bra_Name", True, cboBrand)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnReturn As Boolean

        Try
            mcSQL = New clsSQL_Transactions

            mcSQL.bln_BeginTransaction()

            Select Case myFormControler.FormMode
                Case clsConstants.Form_Modes.INSERT
                    blnReturn = blnProduct_Insert()

                Case clsConstants.Form_Modes.UPDATE
                    blnReturn = blnProduct_Update()

                Case clsConstants.Form_Modes.DELETE
                    blnReturn = blnProduct_Delete()

            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnReturn)
            mcSQL = Nothing
        End Try

        Return blnReturn
    End Function

    Private Function blnProduct_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Pro_Name", txtName.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("ProT_ID", CStr(cboType.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_AddField("ProC_ID", CStr(cboType.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_AddField("Bra_ID", CStr(cboBrand.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOInsert("Product", myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnProduct_Update() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Pro_Name", txtName.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("ProT_ID", CStr(cboType.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_AddField("ProC_ID", CStr(cboCategory.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_AddField("Bra_ID", CStr(cboBrand.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOUpdate("Product", "Pro_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnProduct_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Product", "Pro_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If Not myFormControler.FormIsLoading Then

            If cboType.SelectedIndex > 0 Then
                blnCboCategory_Load()
            Else
                cboCategory.DataSource = Nothing
            End If

            ChangeMade()
        Else
            'Do nothing
        End If
    End Sub

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        Select Case False
            Case blnCboType_Load()
            Case blnCboBrand_Load()
            Case myFormControler.FormMode <> clsConstants.Form_Modes.INSERT
                blnReturn = True
            Case blnLoadData()
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        Else
            'Do nothing
        End If

    End Sub

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim intProC_ID As Integer
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "        Product.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        Product.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        Product.Bra_ID " & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & " WHERE Product.Pro_ID = " & myFormControler.Item_ID & vbCrLf

            mySQLReader = mSQL.ADOSelect(strSQL)

            mySQLReader.Read()

            txtName.Text = mySQLReader.Item("Pro_Name").ToString

            cboType.SelectedValue = CInt(mySQLReader.Item("ProT_ID"))

            If Not IsDBNull(mySQLReader.Item("ProC_ID")) Then
                intProC_ID = CInt(mySQLReader.Item("ProC_ID"))
            Else
                intProC_ID = 0
            End If

            If Not IsDBNull(mySQLReader.Item("Bra_ID")) Then
                cboBrand.SelectedValue = CInt(mySQLReader.Item("Bra_ID"))
            Else
                cboBrand.SelectedIndex = 0
            End If

            mySQLReader.Close()

            blnReturn = blnCboCategory_Load(intProC_ID)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        ChangeMade()
    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub ChangeMade()
        Select Case False
            Case Not myFormControler.FormIsLoading
            Case Else
                myFormControler.ChangeMade = True

        End Select
    End Sub

    Private Sub cboBrand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ChangeMade()
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        ChangeMade()
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules
        Select Case False
            Case txtName.Text <> vbNullString
                txtName.Focus()

            Case cboType.SelectedIndex > -1
                cboType.DroppedDown = True
                cboType.Focus()

            Case cboCategory.SelectedIndex > -1
                cboType.DroppedDown = True
                cboType.Focus()

            Case cboBrand.SelectedIndex > -1
                cboType.DroppedDown = True
                cboType.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub
End Class