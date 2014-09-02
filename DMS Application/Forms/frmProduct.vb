Public Class frmProduct

    'Private members
    Private mintGrdPrices_Action_col As Short = 0
    Private mintGrdPrices_Cy_ID_col As Short = 1
    Private mintGrdPrices_Cy_Name_col As Short = 2
    Private mintGrdPrices_ProB_ID_col As Short = 3
    Private mintGrdPrices_ProB_Name_col As Short = 4
    Private mintGrdPrices_Price_col As Short = 5


    'Private class members
    Private mcSQL As clsSQL_Transactions
    Private WithEvents mcGrdPrices As clsDataGridView


#Region "Functions / Subs"

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

            mySQLReader.Close()

            blnReturn = blnCboCategory_Load(intProC_ID)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Private Function blnGrdPrices_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & "  SELECT " & clsDataGridView.GridRowActions.CONSULT_ACTION & " AS Action, " & vbCrLf
            strSQL = strSQL & "         Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "         Company.Cy_Name, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_Name, " & vbCrLf
            strSQL = strSQL & "         ProductPrice.ProP_ID " & vbCrLf
            strSQL = strSQL & "  FROM ProductPrice " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Company ON Company.Cy_ID = ProductPrice.Cy_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ON ProductBrand.ProB_ID = ProductPrice.ProB_ID " & vbCrLf
            strSQL = strSQL & "  WHERE ProductPrice.Pro_ID = " & myFormControler.Item_ID & vbCrLf
            strSQL = strSQL & "  ORDER BY Company.Cy_name " & vbCrLf

            blnReturn = mcGrdPrices.bln_FillData(strSQL)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboType_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " ORDER BY ProductType.ProT_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "ProT_ID", "ProT_Name", False, cboType)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboCategory_Load(Optional ByVal vintSelectedValue As Integer = 0) As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            cboCategory.Enabled = True

            strSQL = strSQL & " SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductCategory " & vbCrLf
            strSQL = strSQL & " WHERE ProductCategory.ProT_ID = " & CInt(cboType.SelectedValue) & vbCrLf
            strSQL = strSQL & " ORDER BY ProductCategory.ProC_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "ProC_ID", "ProC_Name", True, cboCategory)

            cboCategory.SelectedValue = vintSelectedValue

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnReturn As Boolean

        Try
            mcSQL = New clsSQL_Transactions

            mcSQL.bln_BeginTransaction()

            Select Case myFormControler.FormMode
                Case clsConstants.Form_Modes.INSERT_MODE
                    blnReturn = blnProduct_Insert()

                Case clsConstants.Form_Modes.UPDATE_MODE
                    blnReturn = blnProduct_Update()

                Case clsConstants.Form_Modes.DELETE_MODE
                    blnReturn = blnProduct_Delete()

            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
                Case mcSQL.bln_AddField("ProC_ID", CStr(cboCategory.SelectedValue), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOInsert("Product", myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
                Case mcSQL.bln_ADOUpdate("Product", "Pro_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGrdPrices_SaveData() As Boolean
        Dim blnReturn As Boolean
        Dim intRowCpt As Integer

        Try
            For intRowCpt = 0 To grdPrices.Rows.Count - 1

                Select Case CInt(grdPrices.Rows(intRowCpt).Cells(mintGrdPrices_Action_col).Value)
                    Case clsDataGridView.GridRowActions.INSERT_ACTION
                        blnReturn = blnProductPrice_Insert(intRowCpt)

                    Case clsDataGridView.GridRowActions.UPDATE_ACTION


                    Case clsDataGridView.GridRowActions.DELETE_ACTION


                End Select
            Next

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn

    End Function

    Private Function blnProductPrice_Insert(ByVal intRowIndex As Integer) As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Cy_ID", grdPrices.Rows(intRowIndex).Cells(mintGrdPrices_Cy_ID_col).Value.ToString, clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_AddField("ProB_ID", grdPrices.Rows(intRowIndex).Cells(mintGrdPrices_ProB_ID_col).Value.ToString, clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_AddField("Pro_ID", myFormControler.Item_ID.ToString, clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOInsert("ProductPrice", myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region


#Region "Private events"

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If Not myFormControler.FormIsLoading Then

            If cboType.SelectedIndex > 0 Then
                blnCboCategory_Load()
            Else
                cboCategory.DataSource = Nothing
                cboCategory.Enabled = False
            End If

            myFormControler.ChangeMade = True
        End If
    End Sub

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        mcGrdPrices = New clsDataGridView

        Select Case False
            Case mcGrdPrices.bln_Init(grdPrices)
            Case blnCboType_Load()
            Case blnGrdPrices_Load()
            Case myFormControler.FormMode <> clsConstants.Form_Modes.INSERT_MODE
                blnReturn = True
            Case blnLoadData()
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        End If

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        myFormControler.ChangeMade = True
    End Sub

    Private Sub cboBrand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        myFormControler.ChangeMade = True
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        myFormControler.ChangeMade = True
    End Sub

    Private Sub myFormControler_SetReadRights() Handles myFormControler.SetReadRights
        Select Case myFormControler.FormMode
            Case clsConstants.Form_Modes.INSERT_MODE
                cboCategory.Enabled = False

        End Select
    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules
        Select Case False
            Case txtName.Text <> vbNullString
                gcAppControler.ShowMessage(clsConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboType.SelectedIndex > -1
                gcAppControler.ShowMessage(clsConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case cboCategory.SelectedIndex > -1
                gcAppControler.ShowMessage(clsConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub btnAddLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLine.Click
        mcGrdPrices.AddLine()
        grdPrices.Rows(0).Cells(mintGrdPrices_Price_col).ValueType = GetType(Double)
    End Sub

    Private Sub btnRemoveLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLine.Click
        mcGrdPrices.RemoveLine()
    End Sub

    Private Sub mcGrdPrices_SetDisplay() Handles mcGrdPrices.SetDisplay

        grdPrices.AutoGenerateColumns = False
        grdPrices.DataSource = Nothing
        grdPrices.AutoSizeColumnsMode = CType(DataGridViewAutoSizeColumnMode.Fill, DataGridViewAutoSizeColumnsMode)

    End Sub

    Private Sub grdPrices_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdPrices.CellFormatting
        grdPrices.Columns(mintGrdPrices_Price_col).DefaultCellStyle.Format = gstrCurrencyFormat
    End Sub

#End Region
    
    
    Private Sub grdPrices_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPrices.DoubleClick
        Dim cellRectangle As Rectangle

        cellRectangle = grdPrices.GetCellDisplayRectangle(5, grdPrices.Rows(0).Index, True)
     
        cboCompany.Location = New Point(cellRectangle.Location.X + 6, cellRectangle.Location.Y + (cellRectangle.Size.Height - 4))

        cboCompany.Size = cellRectangle.Size

        cboCompany.Visible = True
    End Sub
End Class